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
using System.Data;

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
                    .OrderBy(m => m.Date)
                    .Include(m => m.PlateNavigation)
                    .ToListAsync();
            }
            else
            {
                result = await _context.Menus
                    .OrderBy(m => m.Date)
                    .Include(m => m.PlateNavigation)
                    .ToListAsync();
            }


            var mapped = _mapper.Map<IEnumerable<Menu_GridDTO>>(result);

            return Ok(mapped);
        }

        // GET: api/Menus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Menu_GridDTO>> GetMenu(int id)
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

        // GET: api/Menus/CountInDateRange
        [HttpGet("CountInDateRange", Name = nameof(GetCountInDateRange))]
        public async Task<ActionResult<int>> GetCountInDateRange([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            var result = await _context.Menus.Where(m => m.Date >= from.Date && m.Date < to.AddDays(1).Date).CountAsync();
            return Ok(result);
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

            var (res, msg) = await ValidateData(dto);
            if (!res)
            {
                return BadRequest(msg);
            }

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
            catch (DbUpdateException ex)
            {
                return BadRequest($"{ex.Message}: {ex?.InnerException?.Message}");
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
            if (dto.Date < currentDate)
            {
                return BadRequest("Menu date is before today");
            }

            var (res, msg) = await ValidateData(dto);
            if (!res)
            {
                return BadRequest(msg);
            }

            var entity = _mapper.Map<Menu>(dto);

            _context.Menus.Add(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return BadRequest($"{ex.Message}: {ex?.InnerException?.Message}");
            }

            await _context.Entry(entity).Reference(m => m.PlateNavigation).LoadAsync();

            var mapped = _mapper.Map<Menu_GridDTO>(entity);

            return CreatedAtAction("GetMenu", new { id = mapped.Id }, mapped);

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

            _context.Menus.Remove(menu);
            try 
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return BadRequest($"{ex.Message}: {ex?.InnerException?.Message}");
            }

            return NoContent();
        }

        private bool MenuExists(int id)
        {
            return _context.Menus.Any(m => m.Id == id);
        }

        private async  Task<(bool result, string message)> ValidateData(Menu_GridDTO dto)
        {
            if (await _context.Menus.AnyAsync(m => m.Id == dto.Id))
            {
                return (false, "Menu id is in use");
            }

            if (!(await _context.Plates.AnyAsync(p => p.Id == dto.Plate)))
            {
                return (false, "Specified plate id does not exist");
            }

            if (await _context.Menus.AnyAsync(m => m.Date.Date == dto.Date.Date) &&
                await _context.Menus.AnyAsync(m => m.PlateNavigation.Id == dto.Plate))
            {
                return (false, "Specific plate is already in menu for this date");
            }

            return (true,string.Empty);
        }
    }
}
