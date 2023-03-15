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
                .Include (p => p.PlateCategoryNavigation)
                .Include(p => p.PlateIngredients)
                .ThenInclude(i => i.Ingredient)
                .FirstOrDefaultAsync();

            if (plate == null)
            {
                return NotFound();
            }

            var mapped = _mapper.Map<Plate_DetailsDTO>(plate);

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

            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                return BadRequest("Plate name is required");
            }

            dto.Name.Trim();

            if (await _context.Plates.AnyAsync(i => i.Name == dto.Name))
            {
                return BadRequest("Specified Plate name already exist");
            }

            if (dto.PlateCategory > 0)
            {
                if (!(await _context.PlateCategories.AnyAsync(c => c.Id == dto.PlateCategory)))
                {
                    return BadRequest("Specified category does not exist");
                }
            }

            var entity = _mapper.Map<Plate>(dto);

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

            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                return BadRequest("Plate name is required");
            }

            dto.Name.Trim();

            if (await _context.Plates.AnyAsync(p => p.Name == dto.Name))
            {
                return BadRequest("Specified Plate name already exist");
            }

            if (dto.PlateCategory > 0)
            {
                if (!(await _context.PlateCategories.AnyAsync(c => c.Id == dto.PlateCategory)))
                {
                    return BadRequest("Specified category does not exist");
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(dto.PlateCategoryName))
                {
                    var newCat = new PlateCategory { Name = dto.PlateCategoryName.Trim() };
                    _context.PlateCategories.Add(newCat);

                    entity.PlateCategoryNavigation = newCat;
                }
            }

            _context.Plates.Add(entity);
            await _context.SaveChangesAsync();

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

            _context.Plates.Remove(plate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlateExists(int id)
        {
            return _context.Plates.Any(p => p.Id == id);
        }
    }
}
