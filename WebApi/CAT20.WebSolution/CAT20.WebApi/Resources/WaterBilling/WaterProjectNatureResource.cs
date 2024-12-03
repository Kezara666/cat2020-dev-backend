using CAT20.WebApi.Resources.WaterBilling;

namespace CAT20.Core.Models.WaterBilling
{
    public class WaterProjectNatureResource
    {

        public int? Id { get; set; }
        public string? Type { get; set; }
        public int? SabhaId { get; set; }

        public int? CType { get; set; }

        // Navigation property to represent the many-to-many relationship
        public virtual ICollection<WaterProjectResource>? WaterProjects { get; set; }

        // Navigation property to represent the one-to-many relationship
        public virtual ICollection<NonMeterFixChargeResource>? NonMeterFixCharges { get; set; }
        public virtual ICollection<WaterTariffResource>? WaterTariffs { get; set; }



        // mandatory fields
        public int? Status { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
