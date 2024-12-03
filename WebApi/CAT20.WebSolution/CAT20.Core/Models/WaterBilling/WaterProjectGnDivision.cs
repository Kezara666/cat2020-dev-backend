using CAT20.Core.Models.Control;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.WaterBilling
{
    public class WaterProjectGnDivision
    {
        [Key]
        public int Id { get; set; }

        //[JsonIgnore]
        public virtual WaterProject? WaterProject { get; set; }

        public int? WaterProjectId { get; set; }
        
        //[JsonIgnore]
        public virtual GnDivisions? GnDivision { get; set; }
        public int? ExternalGnDivisionId { get; set; } // ID from the external source/API

       

        // mandatory fields

        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
