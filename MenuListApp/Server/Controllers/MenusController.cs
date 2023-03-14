using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MenuListApp.Shared.MenuListDTOs;
using MenuListApp.Server.Model;

namespace MenuListApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenusController : ControllerBase
    {
        private readonly MenuListDBContext _context;
        private readonly IMapper _mapper;

        public MenusController(MenuListDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Menus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Menu_GridDTO>>> GetMenus([FromQuery] int? plateId)
        { 
            if (_context.Menus == null)
            {
                return NotFound();
            }

            List<Menu> result;

            if (plateId.HasValue)
            {
                result = await _context.Menus
                    .Where(m => m.Plate == plateId.Value)
                    .Include(m => m.PlateNavigation)
                    .ToListAsync();
            }
            else
            {
                result = await _context.Menus
                    .Include(m => m.PlateNavigation)
                    .ToListAsync();
            }


            var mapped = _mapper.Map<IEnumerable<Menu_GridDTO>>(result);

            return Ok(mapped);
        }

        // GET: api/Menus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Menu_GridDTO>>> GetMenu(int id)
        {
            if (_context.Menus == null)
            {
                return NotFound();
            }
            var menu = await _context.Menus.FindAsync(id);

            if (menu == null)
            {
                return NotFound();
            }

            await _context.Entry(menu).Reference(m => m.PlateNavigation).LoadAsync();

            var mapped = _mapper.Map<Menu_GridDTO>(menu);

            return Ok(mapped);
        }

        // PUT: api/Menus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutMenu(int id, Menu_GridDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            // Data validation

            if (!await _context.Menus.AnyAsync(m => m.Id == dto.Id))
            {
                return BadRequest("Specified menu id does not exist");
            }

            if (!await _context.Menus.AnyAsync(m => m.PlateNavigation.Id == dto.Plate))
            {
                return BadRequest("Specified plate id does not exist");
            }
            //DateTimeOffset currentDate = DateTimeOffset.Now;

            // check if date is before today
            //if (dto.Date < currentDate)
            //{
            //    return BadRequest("Menu date is before today");
            //}

            var entity = _mapper.Map<Menu>(dto);

            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Menus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Menu_GridDTO>> PostMenu(Menu_GridDTO dto)
        {
            if (_context.Menus == null)
            {
                return Problem("Entity set 'MenuListDBContext.Menu'  is null.");
            }

            DateTimeOffset currentDate = DateTimeOffset.Now;

            
            // Validation
            if (await _context.Menus.AnyAsync(m => m.Id == dto.Id))
            {
                return BadRequest("Menu id is in use");
            }

            if (!await _context.Menus.AnyAsync(m => m.PlateNavigation.Id == dto.Plate))
            {
                return BadRequest("Specified plate id does not exist");
            }

            //check if date is before today
            if (dto.Date < currentDate)
            {
                return BadRequest("Menu date is before today");
            }


            var entity = _mapper.Map<Menu>(dto);

            _context.Menus.Add(entity);
            await _context.SaveChangesAsync();

            await _context.Entry(entity).Reference(m => m.PlateNavigation).LoadAsync();

            var mapped = _mapper.Map<Menu_GridDTO>(entity);

            return CreatedAtAction("GetMenu", new { id = mapped.Id }, mapped);

            //_context.Menus.Add(entity);

            //await _context.SaveChangesAsync();

            //var mapped = _mapper.Map<Menu_GridDTO>(entity);

            //return CreatedAtAction("GetMenu", new { id = mapped.Id }, mapped);
        }

        // DELETE: api/Menus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            if (_context.Menus == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }

            //if (await _context.Menus.AnyAsync(m => m.Id == id))
            //{
            //    return BadRequest("Menu is in use");
            //}

            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MenuExists(int id)
        {
            return _context.Menus.Any(e => e.Id == id);
        }
    }
}
