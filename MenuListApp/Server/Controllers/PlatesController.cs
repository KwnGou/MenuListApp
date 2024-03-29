﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MenuListApp.Server.Model;
using AutoMapper;
using MenuListApp.Shared.MenuListDTOs;

namespace MenuListApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatesController : ControllerBase
    {
        private readonly MenuListDBContext _context;
        private readonly IMapper _mapper;

        public PlatesController(MenuListDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Plates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Plate_ListDTO>>> GetPlates([FromQuery] int? categoryId)
        {
            if (_context.Plates == null)
            {
                return NotFound();
            }

            List<Plate> result;

            if (categoryId.HasValue)
            {
                result = await _context.Plates
                    .Where(p => p.PlateCategory == categoryId.Value)
                    .Include(c => c.PlateCategoryNavigation)
                    .ToListAsync();
            }
            else
            {
                result = await _context.Plates
                    .Include(c => c.PlateCategoryNavigation)
                    .ToListAsync();
            }


            var mapped = _mapper.Map<IEnumerable<Plate_EditDTO>>(result);

            return Ok(mapped);
        }

        // GET: api/Plates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Plate_DetailsDTO>> GetPlate(int id)
        {
            if (_context.Plates == null)
            {
                return NotFound();
            }
            var plate = await _context.Plates
                .Where(p => p.Id == id)
                .Include(p => p.PlateCategoryNavigation)
                .Include(p => p.PlateIngredients)
                .ThenInclude(i => i.Ingredient)
                .ThenInclude(ing => ing.IngredientCategoryNavigation)
                .FirstOrDefaultAsync();

            if (plate == null)
            {
                return NotFound();
            }
            Plate_DetailsDTO mapped = null;
            try
            {
                mapped = _mapper.Map<Plate_DetailsDTO>(plate);
            }
            catch (Exception ex)
            {
                var i = ex.Message;
            }

            return Ok(mapped);
        }

        // PUT: api/Plates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutPlate(int id, Plate_EditDTO dto)
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

            var entity = _mapper.Map<Plate>(dto);

            // remove existing ingredients
            var currentIngredients = await _context.PlateIngredients
                .Where(pi => pi.PlateId == entity.Id)
                .ToArrayAsync();

            _context.PlateIngredients.RemoveRange(currentIngredients);

            // add new ingredients
            foreach (var item in entity.PlateIngredients)
            {
                _context.PlateIngredients.Add(item);
            }

            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlateExists(id))
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

        // POST: api/Plates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Plate_EditDTO>> PostPlate(Plate_EditDTO dto)
        {
            if (_context.Plates == null)
            {
                return Problem("Entity set 'MenuListDBContext.Plates'  is null.");
            }
            var entity = _mapper.Map<Plate>(dto);

            // Data validation
            var (res, msg) = await ValidateData(dto);
            if (!res)
            {
                return BadRequest(msg);
            }

            _context.Plates.Add(entity);
            // Add ingredients
            foreach (var item in entity.PlateIngredients)
            {
                _context.PlateIngredients.Add(item);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return BadRequest($"{ex.Message}: {ex?.InnerException?.Message}");
            }

            await _context.Entry(entity).Reference(c => c.PlateCategoryNavigation).LoadAsync();

            var mapped = _mapper.Map<Plate_EditDTO>(entity);
            return CreatedAtAction("GetPlate", new { id = mapped.Id }, mapped);
        }

        // DELETE: api/Plates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlate(int id)
        {
            if (_context.Plates == null)
            {
                return NotFound();
            }

            var plate = await _context.Plates.FindAsync(id);
            if (plate == null)
            {
                return NotFound();
            }

            if (await _context.Menus.AnyAsync(m => m.Plate == id))
            {
                return BadRequest("Plate is in use");
            }

            _context.Plates.Remove(plate);
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

        private bool PlateExists(int id)
        {
            return _context.Plates.Any(p => p.Id == id);
        }

        private async Task<(bool result, string message)> ValidateData(Plate_EditDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                return (false, "Plate name is required");
            }

            dto.Name.Trim();

            if (await _context.Plates.AnyAsync(i => i.Name == dto.Name && i.Id != dto.Id))
            {
                return (false, "Specified Plate name already exist");
            }

            if (!(await _context.PlateCategories.AnyAsync(c => c.Id == dto.PlateCategory)))
            {
                return (false, "Specified category does not exist");
            }

            return (true, string.Empty);
        }
    }
}
