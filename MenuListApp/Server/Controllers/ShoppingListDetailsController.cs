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
    public class ShoppingListDetailsController : ControllerBase
    {
        private readonly MenuListDBContext _context;

        public ShoppingListDetailsController(MenuListDBContext context)
        {
            _context = context;
        }

        // GET: api/ShoppingListDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoppingListDetail>>> GetShoppingListDetails()
        {
          if (_context.ShoppingListDetails == null)
          {
              return NotFound();
          }
            return await _context.ShoppingListDetails.ToListAsync();
        }

        // GET: api/ShoppingListDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShoppingListDetail>> GetShoppingListDetail(int id)
        {
          if (_context.ShoppingListDetails == null)
          {
              return NotFound();
          }
            var shoppingListDetail = await _context.ShoppingListDetails.FindAsync(id);

            if (shoppingListDetail == null)
            {
                return NotFound();
            }

            return shoppingListDetail;
        }

        // PUT: api/ShoppingListDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShoppingListDetail(int id, ShoppingListDetail shoppingListDetail)
        {
            if (id != shoppingListDetail.Id)
            {
                return BadRequest();
            }

            _context.Entry(shoppingListDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingListDetailExists(id))
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

        // POST: api/ShoppingListDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShoppingListDetail>> PostShoppingListDetail(ShoppingListDetail shoppingListDetail)
        {
          if (_context.ShoppingListDetails == null)
          {
              return Problem("Entity set 'MenuListDBContext.ShoppingListDetails'  is null.");
          }
            _context.ShoppingListDetails.Add(shoppingListDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShoppingListDetail", new { id = shoppingListDetail.Id }, shoppingListDetail);
        }

        // DELETE: api/ShoppingListDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoppingListDetail(int id)
        {
            if (_context.ShoppingListDetails == null)
            {
                return NotFound();
            }
            var shoppingListDetail = await _context.ShoppingListDetails.FindAsync(id);
            if (shoppingListDetail == null)
            {
                return NotFound();
            }

            _context.ShoppingListDetails.Remove(shoppingListDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShoppingListDetailExists(int id)
        {
            return (_context.ShoppingListDetails?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
