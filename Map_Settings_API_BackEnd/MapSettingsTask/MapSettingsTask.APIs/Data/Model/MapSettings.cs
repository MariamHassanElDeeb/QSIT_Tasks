using MapSettingsTask.APIs.Data.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MapConfig.APIs.Data;

public class MapSettings
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "ClusterRedius is required.")]
    [Range(0.1, 99, ErrorMessage = "ClusterRedius must be between 0.1 and 99")]
    [Column(TypeName = "decimal(5,3)")]
    public decimal ClusterRedius { get; set; }
    public bool IsGeofenced { get; set; }
    [Required(ErrorMessage = "TimeBuffer is required.")]
    public int TimeBuffer { get; set; }
    [Required(ErrorMessage = "LocationBuffer is required.")]
    [Range(0.1, 99, ErrorMessage = "LocationBuffer must be between 0.1 and 99")]
    [Column(TypeName = "decimal(5,3)")]
    public decimal LocationBuffer { get; set; }
    [Required(ErrorMessage = "Duration is required.")]
    public int Duration { get; set; }
    public int MapSubTypeId { get; set; }
    public virtual MapSubType? MapSubType { get; set; }

    public string MapCreatorId { get; set; } = string.Empty;
    public virtual MapCreator? MapCreator { get; set; }

}
