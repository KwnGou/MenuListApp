using System;
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
    public class PlateCategoriesController : ControllerBase
    {
        private readonly MenuListDBContext _context;
        private readonly IMapper _mapper;

        public PlateCategoriesController(MenuListDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/PlateCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlateCategory_GridDTO>>> GetPlateCategories()
        {
            if (_context.PlateCategories == null)
            {
                return NotFound();
            }

            var result = await _context.PlateCategories
                   .ToListAsync();

            var mapped = _mapper.Map<IEnumerable<PlateCategory_GridDTO>>(result);

            return Ok(mapped);
        }

        // GET: api/PlateCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlateCategory_GridDTO>> GetPlateCategory(int id)
        {
            if (_context.PlateCategories == null)
            {
                return NotFound();
            }

            var plateCategory = await _context.PlateCategories.FindAsync(id);

            if (plateCategory == null)
            {
                return NotFound();
            }

            var mapped = _mapper.Map<PlateCategory_GridDTO>(plateCategory);

            return Ok(mapped);
        }

        // PUT: api/PlateCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutPlateCategory(int id, PlateCategory_GridDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            // Data validation
            if (!(await _context.PlateCategories.AnyAsync(c => c.Id == dto.Id)))
            {
                return BadRequest("Specified category id does not exist");
            }

            var (res, msg) = await ValidateData(dto);
            if (!res)
            {
                return BadRequest(msg);
            }


            var entity = _mapper.Map<PlateCategory>(dto);

            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlateCategoryExists(id))
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

        // POST: api/PlateCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PlateCategory_GridDTO>> PostPlateCategory(PlateCategory_GridDTO dto)
        {
            if (_context.PlateCategories == null)
            {
                return Problem("Entity set 'MenuListDBContext.PlateCategories'  is null.");
            }
            // Validation
            var (res, msg) = await ValidateData(dto);
            if (!res)
            {
                return BadRequest(msg);
            }

            var entity = _mapper.Map<PlateCategory>(dto);

            _context.PlateCategories.Add(entity);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return BadRequest($"{ex.Message}: {ex?.InnerException?.Message}");
            }

            var mapped = _mapper.Map<PlateCategory_GridDTO>(entity);

            return CreatedAtAction("GetPlateCategory", new { id = mapped.Id }, mapped);
        }

        // DELETE: api/PlateCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlateCategory(int id)
        {
            if (_context.PlateCategories == null)
            {
                return NotFound();
            }

            var plateCategories = await _context.PlateCategories.FindAsync(id);
            if (plateCategories == null)
            {
                return NotFound();
            }

            if (await _context.Plates.AnyAsync(p => p.Id == id))
            {
                return BadRequest("Category is in use");
            }

            _context.PlateCategories.Remove(plateCategories);
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

        private bool PlateCategoryExists(int id)
        {
            return _context.PlateCategories.Any(p => p.Id == id);
        }
        private async Task<(bool result, string message)> ValidateData(PlateCategory_GridDTO dto)
        {

            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                return (false, "Category name is required");
            }

            dto.Name.Trim();

            if (await _context.PlateCategories.AnyAsync(c => c.Name == dto.Name))
            {
                return (false, "Specified category name already exist");
            }

            return (true, string.Empty);
        }
    }
}
