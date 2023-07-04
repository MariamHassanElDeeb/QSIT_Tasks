using MapSettingsTask.APIs.Data.Model;
using System.ComponentModel.DataAnnotations;

namespace MapConfig.APIs.Data;

    public class MapType
    {
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "MapType is required.")]
    public required string Type { get; set; }
    public List<MapSubType> SubTypes { get; }= new List<MapSubType>();
    }

