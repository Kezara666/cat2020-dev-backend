using CAT20.Core.Models.Vote;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace CAT20.Core.Models.WaterBilling
{
    public class WaterProjectSubRoad
    {
        [Key]
        public int? Id { get; set; }
        [Required]
        [StringLength(50)]
        public string? Name { get; set; }
        [Required]
        public int MainRoadId { get; set; } // Foreign key
        //[JsonIgnore]
        public virtual WaterProject? WaterProject { get; set; }
        public int WaterProjectId { get; set; } // Foreign Key

        //[JsonIgnore]
        public virtual WaterProjectMainRoad? MainRoad { get; set; }


        // Navigation property to represent the one-to-many relationship
        [JsonIgnore]
        public virtual ICollection<MeterConnectInfo>? MeterConnectInfos { get; set; }

        // Navigation property to represent the one-to-many relationship
        [JsonIgnore]
        public virtual ICollection<ApplicationForConnection>? ApplicationForConnections { get; set; }

        // Navigation property to represent the one-to-many relationship
        [JsonIgnore]
        public virtual ICollection<WaterConnection>? WaterConnections { get; set; }

      

        // mandatory fields
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
