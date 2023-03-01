using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyModel = MenuListApp.Server.Model;
using AutoMapper;
using MenuListApp.Shared.MenuListDTOs;
using MenuListApp.Server.Model;

namespace MenuListApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsCategoriesController : ControllerBase
    {
        //private readonly MenuListDBContext _context;

        private readonly MyModel.MenuListDBContext _context;
        private readonly IMapper _mapper;

        public ItemsCategoriesController(MyModel.MenuListDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/ItemsCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemsCategories_GridDTO>>> GetItemsCategories()
        {

            if (_context.ItemsCategories == null)
            {
                return NotFound();
            }

            var result = await _context.ItemsCategories
                   .ToListAsync();

            var mapped = _mapper.Map<IEnumerable<ItemsCategories_GridDTO>>(result);

            return Ok(mapped);

        }

        // GET: api/ItemsCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemsCategories_GridDTO>> GetItemsCategory(int id)
        {
            if (_context.ItemsCategories == null)
            {
                return NotFound();
            }

            var itemsCategory = await _context.ItemsCategories.FindAsync(id);

            if (itemsCategory == null)
            {
                return NotFound();
            }

            var mapped = _mapper.Map<ItemsCategories_GridDTO>(itemsCategory);

            return Ok(mapped);
        }

        // PUT: api/ItemsCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutItemsCategory(int id, ItemsCategories_GridDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            // Data validation

            if (!(await _context.ItemsCategories.AnyAsync(c => c.Id == dto.Id)))
            {
                return BadRequest("Specified category id does not exist");
            }

            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                return BadRequest("Category name is required");
            }

            dto.Name.Trim();

            if (await _context.ItemsCategories.AnyAsync(c => c.Name == dto.Name))
            {
                return BadRequest("Specified category name already exist");
            }

            var entity = _mapper.Map<ItemsCategory>(dto);

            _context.Entry(entity).State = EntityState.Modified;

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
        public async Task<ActionResult<ItemsCategories_GridDTO>> PostItemsCategory(ItemsCategories_GridDTO dto)
        {
            if (_context.ItemsCategories == null)
            {
                return Problem("Entity set 'MenuListDBContext.ItemsCategories'  is null.");
            }
            // Validation
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                return BadRequest("Category name is required");
            }

            dto.Name.Trim();

            if (await _context.ItemsCategories.AnyAsync(c => c.Name == dto.Name))
            {
                return BadRequest("Specified category name already exist");
            }

            var entity = _mapper.Map<ItemsCategory>(dto);

            _context.ItemsCategories.Add(entity);

            await _context.SaveChangesAsync();

            var mapped = _mapper.Map<ItemsCategories_GridDTO>(entity);

            return CreatedAtAction("GetItemsCategory", new { id = mapped.Id }, mapped);

            //_context.ItemsCategories.Add(itemsCategory);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetItemsCategory", new { id = itemsCategory.Id }, itemsCategory);
        }

        // DELETE: api/ItemsCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemsCategory(int id)
        {
            if (_context.ItemsCategories == null)
            {
                return NotFound();
            }

            var itemsCategory = await _context.ItemsCategories.FindAsync(id);
            if (itemsCategory == null)
            {
                return NotFound();
            }

            if (await _context.Items.AnyAsync(i => i.Id == id))
            {
                return BadRequest("Category is in use");
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
