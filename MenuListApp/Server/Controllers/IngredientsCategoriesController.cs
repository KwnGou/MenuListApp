﻿using System;
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
    public class IngredientsCategoriesController : ControllerBase
    {
        private readonly MenuListDBContext _context;
        private readonly IMapper _mapper;

        public IngredientsCategoriesController(MenuListDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/IngredientsCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IngredientCategory_GridDTO>>> GetIngredientsCategories()
        {
            if (_context.IngredientsCategories == null)
            {
                return NotFound();
            }
            var result = await _context.IngredientsCategories
                .ToListAsync();
               

            var mapped = _mapper.Map<IEnumerable<IngredientCategory_GridDTO>>(result);

            return Ok(mapped);
        }

        // GET: api/IngredientsCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<IngredientCategory_GridDTO>>> GetIngredientsCategory(int id)
        {
            if (_context.IngredientsCategories == null)
            {
                return NotFound();
            }

            var ingredientsCategory = await _context.IngredientsCategories.FindAsync(id);

            if (ingredientsCategory == null)
            {
                return NotFound();
            }

            var mapped = _mapper.Map<IngredientCategory_GridDTO>(ingredientsCategory);

            return Ok(mapped);
        }

        // PUT: api/IngredientsCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIngredientsCategory(int id, IngredientCategory_GridDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            // Data validation

            if (!(await _context.IngredientsCategories.AnyAsync(c => c.Id == dto.Id)))
            {
                return BadRequest("Specified category id does not exist");
            }

            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                return BadRequest("Category name is required");
            }

            dto.Name.Trim();

            if (await _context.IngredientsCategories.AnyAsync(c => c.Name == dto.Name))
            {
                return BadRequest("Specified category name already exist");
            }

            var entity = _mapper.Map<IngredientsCategory>(dto);

            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredientsCategoryExists(id))
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

        // POST: api/IngredientsCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<IngredientCategory_GridDTO>> PostIngredientsCategory(IngredientCategory_GridDTO dto)
        {
            if (_context.IngredientsCategories == null)
            {
                return Problem("Entity set 'MenuListDBContext.IngredientsCategory'  is null.");
            }
            // Validation
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                return BadRequest("Category name is required");
            }

            dto.Name.Trim();

            if (await _context.IngredientsCategories.AnyAsync(c => c.Name == dto.Name))
            {
                return BadRequest("Specified category name already exist");
            }

            var entity = _mapper.Map<IngredientsCategory>(dto);

            _context.IngredientsCategories.Add(entity);

            await _context.SaveChangesAsync();

            var mapped = _mapper.Map<IngredientCategory_GridDTO>(entity);

            return CreatedAtAction("GetIngredientsCategory", new { id = mapped.Id }, mapped);
        }

        // DELETE: api/IngredientsCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredientsCategory(int id)
        {
            if (_context.IngredientsCategories == null)
            {
                return NotFound();
            }

            var ingredientsCategory = await _context.IngredientsCategories.FindAsync(id);
            if (ingredientsCategory == null)
            {
                return NotFound();
            }

            if (await _context.Items.AnyAsync(i => i.Id == id))
            {
                return BadRequest("Category is in use");
            }

            _context.IngredientsCategories.Remove(ingredientsCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IngredientsCategoryExists(int id)
        {
            return _context.IngredientsCategories.Any(i => i.Id == id);
        }
    }
}