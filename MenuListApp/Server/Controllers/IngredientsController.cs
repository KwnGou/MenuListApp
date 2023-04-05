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
    public class IngredientsController : ControllerBase
    {
        private readonly MenuListDBContext _context;
        private readonly IMapper _mapper;

        public IngredientsController(MenuListDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Ingredients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ingredient_GridDTO>>> GetIngredients([FromQuery] int? categoryId)
        {
            if (_context.Ingredients == null)
            {
                return NotFound();
            }

            List<Ingredient> result;

            if (categoryId.HasValue)
            {
                result = await _context.Ingredients
                    .Where(i => i.IngredientCategory == categoryId.Value)
                    .Include(c => c.IngredientCategoryNavigation)
                    .ToListAsync();
            }
            else
            {
                result = await _context.Ingredients
                    .Include(c => c.IngredientCategoryNavigation)
                    .ToListAsync();
            }

            var mapped = _mapper.Map<IEnumerable<Ingredient_GridDTO>>(result);

            return Ok(mapped);
        }

        // GET: api/Ingredients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Ingredient_GridDTO>>> GetIngredient(int id)
        {
            if (_context.Ingredients == null)
            {
                return NotFound();
            }

            var ingredient = await _context.Ingredients.FindAsync(id);

            if (ingredient == null)
            {
                return NotFound();
            }

            await _context.Entry(ingredient).Reference(c => c.IngredientCategoryNavigation).LoadAsync();

            var mapped = _mapper.Map<Ingredient_GridDTO>(ingredient);

            return Ok(mapped);
        }

        // PUT: api/Ingredients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIngredient(int id, Ingredient_GridDTO dto)
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

            var entity = _mapper.Map<Ingredient>(dto);

            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredientExists(id))
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

        // POST: api/Ingredients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ingredient_GridDTO>> PostIngredient(Ingredient_GridDTO dto)
        {
            if (_context.Ingredients == null)
            {
                return Problem("Entity set 'MenuListDBContext.Ingredients' is null");
            }

            var entity = _mapper.Map<Ingredient>(dto);

            // Data validation
            var (res, msg) = await ValidateData(dto);
            if (!res)
            {
                return BadRequest(msg);
            }

            _context.Ingredients.Add(entity);
            try 
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException) 
            {
                if (!IngredientExists(dto.Id))
                {
                    return NotFound();
                }
                else 
                {
                    throw;
                }
            }

            await _context.Entry(entity).Reference(c => c.IngredientCategoryNavigation).LoadAsync();

            var mapped = _mapper.Map<Ingredient_GridDTO>(entity);

            return CreatedAtAction("GetIngredient", new { id = mapped.Id }, mapped);
        }

        // DELETE: api/Ingredients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredient(int id)
        {
            if (_context.Ingredients == null)
            {
                return NotFound();
            }

            var ingredient = await _context.Ingredients.FindAsync(id);
            if (ingredient == null)
            {
                return NotFound();
            }

            _context.Ingredients.Remove(ingredient);
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

        private bool IngredientExists(int id)
        {
            return _context.Ingredients.Any(i => i.Id == id);
        }
        private async Task<(bool result, string message)> ValidateData(Ingredient_GridDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                return (false, "Ingredient name is required");
            }

            dto.Name.Trim();

            if (await _context.Ingredients.AnyAsync(i => i.Name == dto.Name))
            {
                return (false, "Specified ingredient name already exist");
            }

            if (!(await _context.IngredientsCategories.AnyAsync(c => c.Id == dto.IngredientCategory)))
            {
                return (false, "Specified category does not exist");
            }

            return (true, string.Empty);
        }
    }
}
