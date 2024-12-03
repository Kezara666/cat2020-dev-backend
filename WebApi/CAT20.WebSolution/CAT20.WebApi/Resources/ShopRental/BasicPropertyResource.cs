using CAT20.WebApi.Resources.Control;

namespace CAT20.WebApi.Resources.ShopRental
{
    public partial class BasicPropertyResource
    {
        public int? Id { get; set; }
        public string? PropertyNo { get; set; }
        public int? Status { get; set; }  //0-not assigned,  1- assigned
        public int PropertyTypeId { get; set; }
        //public int RentalPlaceId { get; set; }
        public int? FloorId { get; set; }
        public int? OfficeId { get; set; }
        public int? SabhaId { get; set; }
        public int PropertyNatureId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual OfficeResource? Office { get; set; }
        public virtual BasicFloorResource? Floor { get; set; }
        //public virtual BasicRentalPlaceResource? RentalPlace { get; set; }
        public virtual PropertyTypeResource? PropertyType { get; set; }
        public virtual PropertyNatureResource? PropertyNature { get; set; }
    }
}
