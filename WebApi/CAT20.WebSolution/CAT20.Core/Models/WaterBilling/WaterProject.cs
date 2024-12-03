using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CAT20.Core.Models.WaterBilling
{
    public class WaterProject
    {
        public WaterProject()
        {

            WaterProjectGnDivisions = new HashSet<WaterProjectGnDivision>();
            //MainRoads = new HashSet<WaterProjectMainRoad>();
            SubRoads = new HashSet<WaterProjectSubRoad>();
            Natures = new HashSet<WaterProjectNature>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string? Name { get; set; }
        [Required]
        public int OfficeId { get; set; }

        [JsonIgnore]
        public ICollection<WaterProjectGnDivision>? WaterProjectGnDivisions { get; set; }
        //public ICollection<WaterProjectMainRoad>? MainRoads { get; set; }

        // Navigation property to represent the one-to-many relationship
        [JsonIgnore]
        public ICollection<WaterProjectSubRoad>? SubRoads { get; set; }

        // Navigation property to represent the many-to-many relationship
        [JsonIgnore]
        public ICollection<WaterProjectNature>? Natures { get; set; }


        // mandatory fields

        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
