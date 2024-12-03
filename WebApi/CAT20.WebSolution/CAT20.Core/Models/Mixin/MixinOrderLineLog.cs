using CAT20.Core.Models.Control;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CAT20.Core.Models.Mixin
{
    public partial class MixinOrderLineLog
    {
        public int Id { get; set; }
        public string CustomVoteName { get; set; }
        public string Description { get; set; }
        public decimal? Amount { get; set; }
        public decimal? PaymentVatAmount { get; set; }
        public decimal? PaymentNbtAmount { get; set; }
        public decimal? StampAmount { get; set; }
        public decimal? TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        [JsonIgnore]
        public virtual VoteAssignmentDetails VoteAssignmentDetails { get; set; }
        public int MixinVoteAssignmentDetailId { get; set; }
        public int? VoteDetailId { get; set; }
        public int? ClassificationId { get; set; }
        [JsonIgnore]
        public virtual PaymentVat PaymentVat { get; set; }
        public int? PaymentVatId { get; set; }
        [JsonIgnore]
        public virtual PaymentNbt PaymentNbt { get; set; }
        public int? PaymentNbtId { get; set; }
        [JsonIgnore]
        public virtual MixinOrderLog MixinOrderLog { get; set; }
        public int MixinOrderId { get; set; }
        public int VoteOrBal { get; set; }
        public int? VotePaymentTypeId { get; set; }


        //just for reporting



        [Precision(18, 2)]
        public decimal? AssmtGrossAmount { get; set; }
        [Precision(18, 2)]
        public decimal? AssmtDiscountAmount { get; set; }
        [Precision(18, 2)]
        public decimal? AssmtDiscountRate { get; set; }
    }
}