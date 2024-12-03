namespace CAT20.WebApi.Resources.Mixin.Save
{
    public class NewSaveMixinOrderLineResource
    {
        public int Id { get; set; }
        public string? CustomVoteName { get; set; }
        public string Description { get; set; }
        public decimal? Amount { get; set; }
        public decimal? PaymentVatAmount { get; set; }
        public decimal? PaymentNbtAmount { get; set; }
        public decimal? StampAmount { get; set; }
        public decimal? TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? MixinVoteAssignmentDetailId { get; set; }
        public int VoteDetailId { get; set; }
        public int? ClassificationId { get; set; }
        public int? PaymentVatId { get; set; }
        public int? PaymentNbtId { get; set; }
        public int MixinOrderId { get; set; }
        public int VoteOrBal { get; set; }

        public int? VotePaymentTypeId { get; set; }


        public decimal? AssmtGrossAmount { get; set; }
        public decimal? AssmtDiscountAmount { get; set; }
        public decimal? AssmtDiscountRate { get; set; }

    }
}
