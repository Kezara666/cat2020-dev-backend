namespace CAT20.Core.Models.WaterBilling
{
    public class WaterProjectSubRoadResource
    {

        public int? Id { get; set; }

        public string? Name { get; set; }
        public int MainRoadId { get; set; } // Foreign key
        public WaterProjectMainRoad? MainRoad { get; set; } // Foreign key
        public int WaterProjectId { get; set; } // Foreign Key
        public WaterProject? WaterProject { get; set; }

        // mandatory fields
        public int? Status { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }


}

