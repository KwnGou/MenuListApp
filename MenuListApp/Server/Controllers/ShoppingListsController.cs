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
using MenuListApp.Client.Pages;
using Radzen;

namespace MenuListApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingListsController : ControllerBase
    {
        private readonly MenuListDBContext _context;
        private readonly IMapper _mapper;

        public ShoppingListsController(MenuListDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/ShoppingLists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoppingList>>> GetShoppingLists()
        {
            if (_context.ShoppingLists == null)
            {
                return NotFound();
            }

            var result = await _context.ShoppingLists.OrderByDescending(s => s.Date).ToListAsync();

            var mapped = _mapper.Map<IEnumerable<ShoppingList>>(result);

            return Ok(mapped);

        }

        // GET: api/ShoppingLists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShoppingList_DetailsDTO>> GetShoppingList(int id)
        {
            if (_context.ShoppingLists == null)
            {
                return NotFound();
            }
            var entity = await _context.ShoppingLists.FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            await _context.Entry(entity).Collection(sl => sl.ShoppingListDetails).LoadAsync();

            var dto = _mapper.Map<ShoppingList_DetailsDTO>(entity);

            var ingredients = await _context.Ingredients.ToListAsync();
            foreach (var item in dto.ShoppingListDetails)
            {
                item.ObjectTypeName = ((ShoppingListObjectType)item.RelatedObjectType).ToString();
                item.ObjectName = ingredients.First(i => i.Id == item.RelatedObjectId).Name;
            }

            return Ok(dto);
        }

        // PUT: api/ShoppingLists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShoppingList(int id, ShoppingList_EditDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var entity = _mapper.Map<ShoppingList>(dto);

            _context.Entry(entity).State = EntityState.Modified;

            // do line items
            var existing = await _context.ShoppingListDetails.Where(sld => sld.ShoppingListId == entity.Id).ToListAsync();
            if (existing.Any())
            {
                _context.ShoppingListDetails.RemoveRange(existing);
            }
            var newListItems = _mapper.Map<ShoppingListDetail[]>(dto.ShoppingListDetails);
            _context.ShoppingListDetails.AddRange(newListItems);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingListExists(id))
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

        // POST: api/ShoppingLists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShoppingList_DetailsDTO>> PostShoppingList(MenuDateRange dto)
        {
            if (_context.ShoppingLists == null)
            {
                return Problem("Entity set 'MenuListDBContext.ShoppingLists'  is null.");
            }

            var entity = new ShoppingList { Date = DateTimeOffset.Now.Date };
            if (dto.NoMenuSelection) { 
                _context.ShoppingLists.Add(entity);
            }
            else
            {
                // get plate ingerdients
                var data = await _context.Menus
                    .Where(m => m.Date > dto.From.Date && m.Date < dto.To.Date.AddDays(1))
                    .Include(m => m.PlateNavigation)
                    .ThenInclude(p => p.PlateIngredients)
                    .SelectMany(m => m.PlateNavigation.PlateIngredients)
                    .Select(i => new ShoppingListDetail
                    {
                        RelatedObjectId = i.IngredientId,
                        RelatedObjectType = (int)ShoppingListObjectType.Ingredient,
                        ShoppingList = entity
                    })
                    .Distinct()
                    .ToListAsync();

                //var ingList = await _context.Menus
                //    .Where(m => m.Date > dto.From.Date && m.Date < dto.To.Date.AddDays(1))
                //    .Include(m => m.PlateNavigation)
                //    .ThenInclude(p => p.PlateIngredients)
                //    .SelectMany(m => m.PlateNavigation.PlateIngredients)
                //    .ToListAsync();

                //var data = new List<ShoppingListDetail>();
                //foreach (var item in ingList)
                //{
                //    var existing = data.FirstOrDefault(ex =>
                //        ex.RelatedObjectType == (int)ShoppingListObjectType.Ingredient
                //        && ex.RelatedObjectId == item.IngredientId);
                //    if (existing != null)
                //    {
                //        if (string.IsNullOrWhiteSpace(existing.Remarks))
                //        {
                //            existing.Remarks = "(2)";
                //        }
                //        else
                //        {
                //            var cnt = int.Parse(existing.Remarks[1..^1]);
                //            existing.Remarks = $"({cnt+1})";
                //        }
                //    }
                //    else
                //    {
                //        var sld = new ShoppingListDetail
                //        {
                //            RelatedObjectId = item.IngredientId,
                //            RelatedObjectType = (int)ShoppingListObjectType.Ingredient,
                //            ShoppingList = entity
                //        };
                //        data.Add(sld);
                //    }
                //}

                _context.ShoppingListDetails.AddRange(data);

                _context.ShoppingLists.Add(entity);
            }

            _context.ShoppingLists.Add(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return BadRequest($"{ex.Message}: {ex?.InnerException?.Message}");
            }

            var slDto = _mapper.Map<ShoppingList_DetailsDTO>(entity);
            var ingredients = await _context.Ingredients.ToListAsync();
            foreach (var item in slDto.ShoppingListDetails) {
                item.ObjectTypeName = ((ShoppingListObjectType)item.RelatedObjectType).ToString();
                item.ObjectName = ingredients.First(i => i.Id == item.RelatedObjectId).Name;
            }

            return CreatedAtAction("GetShoppingList", new { id = slDto.Id }, slDto);
        }

        //// DELETE: api/ShoppingLists/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteShoppingList(int id)
        //{
        //    if (_context.ShoppingLists == null)
        //    {
        //        return NotFound();
        //    }
        //    var entity = await _context.ShoppingLists.FindAsync(id);
        //    if (entity == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.ShoppingLists.Remove(entity);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool ShoppingListExists(int id)
        {
            return (_context.ShoppingLists?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
