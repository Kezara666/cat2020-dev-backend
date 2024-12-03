using CAT20.Core.Models.ShopRental;

namespace CAT20.WebApi.Resources.ShopRental
{
    public partial class BasicFloorResource
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? Number { get; set; }
        public int? Status { get; set; }
        public int RentalPlaceId { get; set; }
        public string? Code { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual BasicRentalPlaceResource? RentalPlace { get; set; }
        public virtual ICollection<Property>? Properties { get; set; }
    }
}
