using CAT20.Core.Models.Enums;
using CAT20.Core.Models.ShopRental;
using CAT20.WebApi.Resources.Control;

namespace CAT20.WebApi.Resources.ShopRental
{
    public partial class BasicShopResource
    {
        public int? Id { get; set; }
        public int? PropertyId { get; set; } //FK
        public string? BusinessName { get; set; }
        public string? BusinessNature { get; set; }
        public string? BusinessRegistrationNo { get; set; }
        public int? CustomerId { get; set; }
        public string? CustomerDesigntion { get; set; }
        public string? AgreementNo { get; set; }
        public DateOnly? AgreementStartDate { get; set; }
        public DateOnly? AgreementEndDate { get; set; }
        public DateOnly? AgreementCloseDate { get; set; } //new (Shop agreement change request)
        public decimal? Rental { get; set; }
        public decimal? KeyMoney { get; set; }
        public decimal? SecurityDeposit { get; set; }
        public decimal? ServiceCharge { get; set; }
        public ShopStatus? Status { get; set; }
        public int? IsApproved { get; set; }
        public int? ApprovedBy { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? OfficeId { get; set; }
        public int? SabhaId { get; set; }

        public virtual BasicPropertyResource? Property { get; set; } //Mapping 1(property): many (shop)

        public virtual OfficeResource? Office { get; set; } //Ignore field //modified 2024/04/04
        public virtual PartnerResource? Customer { get; set; } //Ignore field //modified 2024/04/04
    }
}
