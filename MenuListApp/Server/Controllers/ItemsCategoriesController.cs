using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MenuListApp.Server.Model;

namespace MenuListApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsCategoriesController : ControllerBase
    {
        private readonly MenuListDBContext _context;

        public ItemsCategoriesController(MenuListDBContext context)
        {
            _context = context;
        }

        // GET: api/ItemsCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemsCategory>>> GetItemsCategories()
        {
            return await _context.ItemsCategories.ToListAsync();
        }

        // GET: api/ItemsCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemsCategory>> GetItemsCategory(int id)
        {
            var itemsCategory = await _context.ItemsCategories.FindAsync(id);

            if (itemsCategory == null)
            {
                return NotFound();
            }

            return itemsCategory;
        }

        // PUT: api/ItemsCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemsCategory(int id, ItemsCategory itemsCategory)
        {
            if (id != itemsCategory.Id)
            {
                return BadRequest();
            }

            _context.Entry(itemsCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemsCategoryExists(id))
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

        // POST: api/ItemsCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ItemsCategory>> PostItemsCategory(ItemsCategory itemsCategory)
        {
            _context.ItemsCategories.Add(itemsCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItemsCategory", new { id = itemsCategory.Id }, itemsCategory);
        }

        // DELETE: api/ItemsCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemsCategory(int id)
        {
            var itemsCategory = await _context.ItemsCategories.FindAsync(id);
            if (itemsCategory == null)
            {
                return NotFound();
            }

            _context.ItemsCategories.Remove(itemsCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemsCategoryExists(int id)
        {
            return _context.ItemsCategories.Any(e => e.Id == id);
        }
    }
}
