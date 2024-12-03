using CAT20.WebApi.Resources.Mixin;

namespace CAT20.WebApi.Resources.ShopRental
{
    public partial class BasicShopRentalVoteAssignResource
    {
        public int? Id { get; set; }

        public int? PropertyId { get; set; } //FK

        public int ShopId { get; set; } //FK

        //---------------- [Start - vote assignment detail id] -------------------------
        public int PropertyRentalVoteId { get; set; }
        public VoteAssignmentDetailsResource? PropertyRentalVote { get; set; } //entity-ignore

        public int LastYearArreasAmountVoteId { get; set; }
        public VoteAssignmentDetailsResource? LastYearArreasAmountVote { get; set; } //entity-ignore

        public int ThisYearArrearsAmountVoteId { get; set; }
        public VoteAssignmentDetailsResource? ThisYearArrearsAmountVote { get; set; } //entity-ignore

        public int LastYearFineAmountVoteId { get; set; }
        public VoteAssignmentDetailsResource? LastYearFineAmountVote { get; set; } //entity-ignore

        public int ThisYearFineAmountVoteId { get; set; }
        public VoteAssignmentDetailsResource? ThisYearFineAmountVote { get; set; } //entity-ignore

        public int ServiceChargeArreasAmountVoteId { get; set; }
        public VoteAssignmentDetailsResource? ServiceChargeArreasAmountVote { get; set; } //entity-ignore

        public int ServiceChargeAmountVoteId { get; set; }
        public VoteAssignmentDetailsResource? ServiceChargeAmountVote { get; set; } //entity-ignore

        public int OverPaymentAmountVoteId { get; set; }
        public VoteAssignmentDetailsResource? OverPaymentAmountVote { get; set; } //entity-ignore
        //---------------- [End - vote assignment detail id] -------------------------



        //---------------- [Start - vote detail id fields] -------------------------
        public int PropertyRentalVoteDetailId { get; set; }

        public int LastYearArreasAmountVoteDetailId { get; set; }

        public int ThisYearArrearsAmountVoteDetailId { get; set; }

        public int LastYearFineAmountVoteDetailId { get; set; }

        public int ThisYearFineAmountVoteDetailId { get; set; }

        public int ServiceChargeArreasAmountVoteDetailId { get; set; }

        public int ServiceChargeAmountVoteDetailId { get; set; }

        public int OverPaymentAmountVoteDetailId { get; set; }
        //---------------- [End - vote detail id fields] -------------------------



        //Mapping 1(Shop): 1 (ShopRentalVoteAssign)
        public virtual BasicShopResource? Shop { get; set; }

        //Mapping 1(property): many (ShopRentalVoteAssign)
        public virtual BasicPropertyResource? Property { get; set; }

        // mandatory fields
        public int? Status { get; set; }
        public int? OfficeId { get; set; }
        public int? SabhaId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
