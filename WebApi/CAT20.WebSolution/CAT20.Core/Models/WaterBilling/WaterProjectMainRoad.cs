using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.WaterBilling
{
    public class WaterProjectMainRoad
    {
        [Key]
        public int? Id { get; set; }
        [Required]
        [StringLength(50)]
        public string? Name { get; set; }

        [Required]
        public int? SabhaId { get; set; }

        // Navigation property to represent the one-to-many relationship
        [JsonIgnore]
        public virtual ICollection<WaterProjectSubRoad>? SubRoads { get; set; }


        // mandatory fields
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
