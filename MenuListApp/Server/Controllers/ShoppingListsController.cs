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

            var dto = _mapper.Map<ShoppingList_DetailsDTO>(entity);


            return Ok(dto);
        }

        //// PUT: api/ShoppingLists/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutShoppingList(int id, ShoppingList entity)
        //{
        //    if (id != entity.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(entity).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ShoppingListExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

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