using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MapSettingsTask.APIs.Data;
using MapSettingsTask.APIs.Dtos;
using MapSettingsTask.APIs.Data.Ccontext;
using Microsoft.AspNetCore.Identity;
using MapSettingsTask.APIs.Data.Model;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace MapSettingsTask.APIs.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class MapSettingsController : ControllerBase
    {
        private readonly MapContext _context;
    private readonly UserManager<MapCreator> _userManager;
    private readonly HttpContextAccessor _HttpContextAccessor;
    public MapSettingsController(MapContext context, UserManager<MapCreator> userManager, HttpContextAccessor _httpContextAccessor)
        {
            _context = context;
        _userManager = userManager;
        _HttpContextAccessor = _httpContextAccessor;

    }

    private string GetCurrentUserId()
    {
        var userId = _HttpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return userId ?? "82712fd4-3d4c-4569-bbb7-a29e65de36ec";
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
        [Authorize]
        public async Task<ActionResult<MapSettings>> PostMapSettings(MapSettingsDto mapSettingsDto)
        {
          if (_context.Settings == null)
          {
              return Problem("Entity set 'MapContext.Settings'  is null.");
          }
            string cid = GetCurrentUserId();
            var settings = new MapSettings()
            {
                ClusterRedius= mapSettingsDto.ClusterRedius,
                Duration=mapSettingsDto.Duration,
                IsGeofenced=mapSettingsDto.IsGeofenced,
               LocationBuffer=mapSettingsDto.LocationBuffer,
               TimeBuffer=mapSettingsDto.TimeBuffer,
               MapSubTypeId=mapSettingsDto.MapSubtypeID
            };
        settings.MapCreatorId = GetCurrentUserId();
        Console.WriteLine($"current id :{ GetCurrentUserId()}");
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

