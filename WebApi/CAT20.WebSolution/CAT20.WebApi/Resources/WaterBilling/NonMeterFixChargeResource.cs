using CAT20.Core.Models.WaterBilling;
using System.ComponentModel.DataAnnotations;

namespace CAT20.WebApi.Resources.WaterBilling
{
    public class NonMeterFixChargeResource
    {
        public int? Id { get; set; }
        public int? WaterProjectId { get; set; }
        public WaterProjectNatureResource? WaterProjectNature { get; set; }
        public int? NatureId { get; set; }

        public decimal? FixedCharge { get; set; }

        // mandatory fields
        public int? Status { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

    }
}
