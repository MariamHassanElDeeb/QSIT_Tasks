
using System.ComponentModel.DataAnnotations;

namespace MapSettingsTask.APIs.Data;

    public class MapType
    {
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "MapType is required.")]
    public required string Type { get; set; }
    public List<MapSubType> SubTypes { get; }= new List<MapSubType>();
    }

