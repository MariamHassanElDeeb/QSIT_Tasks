using System.ComponentModel.DataAnnotations;

namespace MapSettingsTask.APIs.Data;


public class MapSubType
    {
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "MapSubType is required.")]
    public required string SubType { get; set; }
    public int MapTypeId { get; set; }
    public MapType? MapType { get; set; }
    public List<MapSettings> MapSettings { get; } = new List<MapSettings>();
    }

