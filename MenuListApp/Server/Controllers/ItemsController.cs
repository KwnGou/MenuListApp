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
        public async Task<ActionResult<IEnumerable<Items_GridDTO>>> GetItems([FromQuery] int? categoryId)
        {
            if (_context.Items == null)
            {
                return NotFound();
            }

            List<Item> result;

            if (categoryId.HasValue)
            {
                result = await _context.Items
                    .Where(i => i.ItemCategory == categoryId.Value)
                    .Include(c => c.ItemCategoryNavigation)
                    .ToListAsync();
            }
            else
            {
                result = await _context.Items
                    .Include(c => c.ItemCategoryNavigation)
                    .ToListAsync();
            }


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
            var (res, msg) = await ValidateData(dto);
            if (!res)
            {
                return BadRequest(msg);
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
            catch (DbUpdateException ex)
            {
                return BadRequest($"{ex.Message}: {ex?.InnerException?.Message}");
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
                return Problem("Entity set 'MenuListDBContext.Items'  is null.");
            }
            var entity = _mapper.Map<Item>(dto);

            // Data validation
            var (res, msg) = await ValidateData(dto);
            if (!res)
            {
                return BadRequest(msg);
            }

            _context.Items.Add(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return BadRequest($"{ex.Message}: {ex?.InnerException?.Message}");
            }


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

            if (await _context.ShoppingListDetails.AnyAsync(s => s.RelatedObjectId == id && s.RelatedObjectType == (int)ShoppingListObjectType.Item))
            {
                return BadRequest("Item is in use");
            }

            _context.Items.Remove(item);
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

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.Id == id);
        }

        private async Task<(bool result, string message)> ValidateData(Items_GridDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                return (false, "Item name is required");
            }

            dto.Name.Trim();

            if (await _context.Items.AnyAsync(i => i.Name == dto.Name))
            {
                return (false, "Specified item name already exist");
            }

            if (!(await _context.ItemsCategories.AnyAsync(c => c.Id == dto.ItemCategory)))
            {
                return (false, "Specified category does not exist");
            }

            return (true, string.Empty);
        }
    }
}
