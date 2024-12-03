using CAT20.Core.Models.ShopRental;

namespace CAT20.WebApi.Resources.ShopRental
{
    public partial class ProcessConfigurationSettingAssignResource
    {
        public int? Id { get; set; }

        public int ShopId { get; set; } //FK

        public int ShopRentalProcessConfigarationId { get; set; } //FK


        //Mapping 1(Shop): 1 (ProcessConfigurationSettingAssign)
        public virtual Shop? Shop { get; set; }

        //Mapping 1(ShopRentalProcessConfigaration): Many (ProcessConfigurationSettingAssign)
        public virtual ShopRentalProcessConfigarationResource? ShopRentalProcessConfigaration { get; set; }

        // mandatory fields
        public int? Status { get; set; }
        public int? SabhaId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
