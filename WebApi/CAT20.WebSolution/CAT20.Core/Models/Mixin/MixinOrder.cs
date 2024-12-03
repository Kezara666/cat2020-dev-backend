using CAT20.Core.Models.Control;
using CAT20.Core.Models.User;
using CAT20.Core.Models.Vote;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace CAT20.Core.Models.Mixin
{
    public partial class MixinOrder
    {
        public MixinOrder()
        {
            MixinOrderLine = new HashSet<MixinOrderLine>();
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
        public string? ChequeBankName { get; set; }
        public Enums.OrderStatus State { get; set; }
        public UserDetail UserDetail { get; set; }

        public int SessionId { get; set; }
        public int PaymentMethodId { get; set; }
        public GnDivisions GnDivisions { get; set; }
        public int GnDivisionId { get; set; }
        public UserDetail Cashier { get; set; }
        public int? CashierId { get; set; }
        public Partner Partner { get; set; }
        public int? PartnerId { get; set; }
        public int? EmployeeId { get; set; }

        public Office Office { get; set; }
        public int? OfficeId { get; set; }
        public int? AccountDetailId { get; set; }
        public int? BusinessId { get; set; }
        public int? AppCategoryId { get; set; }
        public int? BusinessTaxId { get; set; }
        public Enums.TradeLicenseStatus TradeLicenseStatus { get; set; }
        public int? TaxTypeId { get; set; }

        [JsonIgnore]
        public MixinCancelOrder MixinCancelOrder { get; set; }

        [JsonIgnore]
        public AccountDetail? AccountDetail { get; set; }

        [JsonIgnore]
        public virtual ICollection<MixinOrderLine> MixinOrderLine { get; set; }

        public int? PaymentDetailId { get; set; }

        // Navigation property for the many-to-zero-or-one relationship
        public int? AssessmentId { get; set; }
        public int? ShopId { get; set; }
        public int? WaterConnectionId { get; set; }


        //just for reporting



        [Precision(18, 2)]
        public decimal? AssmtBalByExcessDeduction { get; set; }

        // mandatory field

        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }


    }
}