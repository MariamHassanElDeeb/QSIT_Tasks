namespace MapSettingsTask.APIs.Dtos;

public class MapSettingsDto
    {
        public decimal ClusterRedius { get; set; }
        public bool IsGeofenced { get; set; }
        public int TimeBuffer { get; set; }
        public decimal LocationBuffer { get; set; }
        public int Duration { get; set; }
        public int MapSubtypeID { get; set; }
    }

