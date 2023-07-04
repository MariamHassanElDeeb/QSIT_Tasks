using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MapSettingsTask.APIs.Data;
using MapSettingsTask.APIs.Dtos;
using MapSettingsTask.APIs.Data.Ccontext;
using MapConfig.APIs.Data;

namespace MapSettingsTask.APIs.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class MapSettingsController : ControllerBase
    {
        private readonly MapContext _context;

        public MapSettingsController(MapContext context)
        {
            _context = context;
        }

        // GET: api/MapSettings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MapSettings>>> GetSettings()
        {
          if (_context.Settings == null)
          {
              return NotFound();
          }
            return await _context.Settings.ToListAsync();
        }

        // GET: api/MapSettings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MapSettings>> GetMapSettings(int id)
        {
          if (_context.Settings == null)
          {
              return NotFound();
          }
            var mapSettings = await _context.Settings.FindAsync(id);

            if (mapSettings == null)
            {
                return NotFound();
            }

            return mapSettings;
        }

        // PUT: api/MapSettings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMapSettings(int id, MapSettings mapSettings)
        {
            if (id != mapSettings.Id)
            {
                return BadRequest();
            }

            _context.Entry(mapSettings).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MapSettingsExists(id))
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

        // POST: api/MapSettings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MapSettingsDto>> PostMapSettings(MapSettingsDto mapSettingsDto)
        {
          if (_context.Settings == null)
          {
              return Problem("Entity set 'MapContext.Settings'  is null.");
          }
            var settings = new MapSettings()
            {
                ClusterRedius= mapSettingsDto.ClusterRedius,
                Duration=mapSettingsDto.Duration,
                IsGeofenced=mapSettingsDto.IsGeofenced,
               LocationBuffer=mapSettingsDto.LocationBuffer,
               TimeBuffer=mapSettingsDto.TimeBuffer,
               MapSubTypeId=mapSettingsDto.MapSubtypeID
            };
            _context.Settings.Add(settings);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMapSettings", new { id = settings.Id }, settings);
        }

        // DELETE: api/MapSettings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMapSettings(int id)
        {
            if (_context.Settings == null)
            {
                return NotFound();
            }
            var mapSettings = await _context.Settings.FindAsync(id);
            if (mapSettings == null)
            {
                return NotFound();
            }

            _context.Settings.Remove(mapSettings);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MapSettingsExists(int id)
        {
            return (_context.Settings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }

