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
    public class ItemsController : ControllerBase
    {
        private readonly MenuListDBContext _context;
        private readonly IMapper _mapper;

        public ItemsController(MenuListDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Items_GridDTO>>> GetItems()
        {
            if (_context.Items == null)
            {
                return NotFound();
            }

            var result = await _context.Items
            .Include(c => c.ItemCategoryNavigation)
            .ToListAsync();

            var mapped = _mapper.Map<IEnumerable<Items_GridDTO>>(result);

            return Ok(mapped);
        }

        // GET: api/Items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Items_GridDTO>>> GetItem(int id)
        {
            if (_context.Items == null)
            {
                return NotFound();
            }
            var item = await _context.Items.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            await _context.Entry(item).Reference(c => c.ItemCategoryNavigation).LoadAsync();
  
            var mapped = _mapper.Map<Items_GridDTO>(item);

            return Ok(mapped);

        }

        // PUT: api/Items/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutItem(int id, Items_GridDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }
            // Data validation

            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                return BadRequest("Item name is required");
            }

            dto.Name.Trim();

            if (await _context.Items.AnyAsync(i => i.Name == dto.Name))
            {
                return BadRequest("Specified item name already exist");
            }

            if (dto.ItemCategory >= 0)
            {
                if (!(await _context.ItemsCategories.AnyAsync(c => c.Id == dto.ItemCategory)))
                {
                    return BadRequest("Specified category does not exist");
                }
            }

            var entity = _mapper.Map<Item>(dto);

            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
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

        // POST: api/Items
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Items_GridDTO>> PostItem(Items_GridDTO dto)
        {
            if (_context.Items == null)
            {
                return Problem("Entity set 'MenuListDBContext.Item'  is null.");
            }
            var entity = _mapper.Map<Item>(dto);

            // Data validation

            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                return BadRequest("Item name is required");
            }

            dto.Name.Trim();

            if (await _context.Items.AnyAsync(i => i.Name == dto.Name))
            {
                return BadRequest("Specified Item name already exist");
            }

            if (dto.ItemCategory > 0)
            {
                if (!(await _context.ItemsCategories.AnyAsync(c => c.Id == dto.ItemCategory)))
                {
                    return BadRequest("Specified category does not exist");
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(dto.ItemCategoryName))
                {
                    var newCat = new ItemsCategory { Name = dto.ItemCategoryName.Trim() };
                    _context.ItemsCategories.Add(newCat);

                    entity.ItemCategoryNavigation = newCat;
                }
            }

            _context.Items.Add(entity);
            await _context.SaveChangesAsync();

            await _context.Entry(entity).Reference(c => c.ItemCategoryNavigation).LoadAsync();

            var mapped = _mapper.Map<Items_GridDTO>(entity);

            return CreatedAtAction("GetItem", new { id = mapped.Id }, mapped);
        }

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            if (_context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.Id == id);
        }
    }
}
