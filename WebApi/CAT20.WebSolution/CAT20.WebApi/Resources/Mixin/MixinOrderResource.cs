using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.User;
using CAT20.WebApi.Resources.Vote;

namespace CAT20.WebApi.Resources.Mixin
{
    public partial class MixinOrderResource
    {
        public MixinOrderResource()
        {
            MixinOrderLine = new HashSet<MixinOrderLineResource>();
        }
        public int Id { get; set; }
        public string Code { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNicNumber { get; set; }
        public string CustomerMobileNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal? DiscountRate { get; set; }
        public decimal? DiscountAmount { get; set; }
        public string? ChequeNumber { get; set; }
        public DateTime? ChequeDate { get; set; }
        public string ChequeBankName { get; set; }
        public OrderStatus State { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int SessionId { get; set; }
        public int PaymentMethodId { get; set; }
        public GnDivisions GnDivisions { get; set; }
        public int GnDivisionId { get; set; }
        public UserDetail Cashier { get; set; }
        public int? CashierId { get; set; }
        public Partner Partner { get; set; }
        public int? PartnerId { get; set; }
        public Office Office { get; set; }
        public int? OfficeId { get; set; }
        public int? AccountDetailId { get; set; }
        public int? BusinessId { get; set; }
        public int? AppCategoryId { get; set; }
        public int? BusinessTaxId { get; set; }
        public TradeLicenseStatus TradeLicenseStatus { get; set; }
        public int? TaxTypeId { get; set; }
        public MixinCancelOrderResource MixinCancelOrder { get; set; }
        public AccountDetailResource? AccountDetail { get; set; }
        public virtual ICollection<MixinOrderLineResource> MixinOrderLine { get; set; }

        public decimal? AssmtBalByExcessDeduction { get; set; }
    }
}
