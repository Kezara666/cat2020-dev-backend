using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CAT20.Core.Models.WaterBilling
{
    public class WaterProjectNature
    {
        public WaterProjectNature()
        {
            WaterProjects = new HashSet<WaterProject>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(25)]
        public string? Type { get; set; }

        [Required]
        public int? CType { get; set; }

        [Required]
        public int? SabhaId { get; set; }

        // Navigation property to represent the many-to-many relationship
        [JsonIgnore]
        public virtual ICollection<WaterProject>? WaterProjects { get; set; }

        // Navigation property to represent the one-to-many relationship
        [JsonIgnore]
        public virtual ICollection<NonMeterFixCharge>? NonMeterFixCharges { get; set; }
        [JsonIgnore]
        public virtual ICollection<WaterTariff>? WaterTariffs { get; set; }

        // Navigation property to represent the one-to-many relationship
        [JsonIgnore]
        public virtual ICollection<ApplicationForConnection>? ApplicationForConnections { get; set; }
        public virtual ICollection<WaterConnection>? WaterConnections { get; set; }

        //// Navigation property to represent the one-to-many relationship
        //[JsonIgnore]
        //public virtual ICollection<WaterConnection>? WaterConnections { get; set; }


        // mandatory fields
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
