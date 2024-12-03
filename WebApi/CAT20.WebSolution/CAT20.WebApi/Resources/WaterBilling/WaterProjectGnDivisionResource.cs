using CAT20.Core.Models.Control;
using CAT20.Core.Models.WaterBilling;
using CAT20.WebApi.Resources.Control;
using System.ComponentModel.DataAnnotations;

namespace CAT20.WebApi.Resources.WaterBilling
{
    public class WaterProjectGnDivisionResource
    {

     
        public int? Id { get; set; }
        public int WaterProjectId { get; set; }
        public virtual WaterProject? WaterProject { get; set; }

        public int ExternalGnDivisionId { get; set; } // ID from the external source/API
        public virtual GnDivisionsResource? GnDivision { get; set; }

        // mandatory fields
        public int? Status { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
