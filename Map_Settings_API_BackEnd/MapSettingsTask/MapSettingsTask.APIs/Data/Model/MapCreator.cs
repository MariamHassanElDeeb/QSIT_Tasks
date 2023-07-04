using MapConfig.APIs.Data;
using Microsoft.AspNetCore.Identity;

namespace MapSettingsTask.APIs.Data.Model
{
    public class MapCreator : IdentityUser
    {
        public string Department { get; set; } = string.Empty;
         public List<MapSettings> MapSetting { get; } = new List<MapSettings>();
    }
}
