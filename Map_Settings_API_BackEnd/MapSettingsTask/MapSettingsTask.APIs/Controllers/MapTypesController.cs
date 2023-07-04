using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MapConfig.APIs.Data;
using MapSettingsTask.APIs.Data.Ccontext;

namespace MapSettingsTask.APIs.Controllers;


[Route("api/[controller]")]
    [ApiController]
    public class MapTypesController : ControllerBase
    {
        private readonly MapContext _context;

        public MapTypesController(MapContext context)
        {
            _context = context;
        }

        // GET: api/MapTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MapType>>> GetMapTypes()
        {
          if (_context.MapTypes == null)
          {
              return NotFound();
          }
            return await _context.MapTypes.ToListAsync();
        }

        // GET: api/MapTypes/5
        [HttpGet("GetMapSubType/{id}")]
        public async Task<ActionResult<IEnumerable<MapSubType>>>GetMapSubType(int id)
        {
          if (_context.MapTypes == null)
          {
              return NotFound();
          }
            var mapType = await _context.MapSubType.Where(x => x.MapTypeId == id).ToListAsync();
            //var mapType = await _context.MapSubType.FindAsync(id);
            if (mapType == null)
            {
                return NotFound();
            }

            return mapType;
        }

        // PUT: api/MapTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMapType(int id, MapType mapType)
        {
            if (id != mapType.Id)
            {
                return BadRequest();
            }

            _context.Entry(mapType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MapTypeExists(id))
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

        // POST: api/MapTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MapType>> PostMapType(MapType mapType)
        {
          if (_context.MapTypes == null)
          {
              return Problem("Entity set 'MapContext.MapTypes'  is null.");
          }
            _context.MapTypes.Add(mapType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMapType", new { id = mapType.Id }, mapType);
        }

        // DELETE: api/MapTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMapType(int id)
        {
            if (_context.MapTypes == null)
            {
                return NotFound();
            }
            var mapType = await _context.MapTypes.FindAsync(id);
            if (mapType == null)
            {
                return NotFound();
            }

            _context.MapTypes.Remove(mapType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MapTypeExists(int id)
        {
            return (_context.MapTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }

