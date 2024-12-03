using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Models.Mixin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CAT20.Core.Models.ShopRental
{
    public class ShopRentalVoteAssign
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        public int? PropertyId { get; set; } //FK

        [Required]
        public int ShopId { get; set; } //FK

        //---------------- [Start - vote assignment detail id] -------------------------
        [Required]
        public int PropertyRentalVoteId { get; set; }
        public VoteAssignmentDetails? PropertyRentalVote { get; set; } //entity-ignore

        [Required]
        public int LastYearArreasAmountVoteId { get; set; }
        public VoteAssignmentDetails? LastYearArreasAmountVote { get; set; } //entity-ignore

        [Required]
        public int ThisYearArrearsAmountVoteId { get; set; }
        public VoteAssignmentDetails? ThisYearArrearsAmountVote { get; set; } //entity-ignore

        [Required]
        public int LastYearFineAmountVoteId { get; set; }
        public VoteAssignmentDetails? LastYearFineAmountVote { get; set; } //entity-ignore

        [Required]
        public int ThisYearFineAmountVoteId { get; set; }
        public VoteAssignmentDetails? ThisYearFineAmountVote { get; set; } //entity-ignore

        [Required]
        public int ServiceChargeArreasAmountVoteId { get; set; }
        public VoteAssignmentDetails? ServiceChargeArreasAmountVote { get; set; } //entity-ignore

        [Required]
        public int ServiceChargeAmountVoteId { get; set; }
        public VoteAssignmentDetails? ServiceChargeAmountVote { get; set; } //entity-ignore

        [Required]
        public int OverPaymentAmountVoteId { get; set; }
        public VoteAssignmentDetails? OverPaymentAmountVote { get; set; } //entity-ignore
        //---------------- [End - vote assignment detail id] -------------------------



        //--------- [Start - vote detail id fields] ------------
        [Required]
        public int PropertyRentalVoteDetailId { get; set; }

        [Required]
        public int LastYearArreasAmountVoteDetailId { get; set; }

        [Required]
        public int ThisYearArrearsAmountVoteDetailId { get; set; }

        [Required]
        public int LastYearFineAmountVoteDetailId { get; set; }

        [Required]
        public int ThisYearFineAmountVoteDetailId { get; set; }

        [Required]
        public int ServiceChargeArreasAmountVoteDetailId { get; set; }

        [Required]
        public int ServiceChargeAmountVoteDetailId { get; set; }

        [Required]
        public int OverPaymentAmountVoteDetailId { get; set; }
        //--------- [End - vote detail id fields] --------------



        //Mapping 1(property): many (ShopRentalVoteAssign)
        public virtual Property? Property { get; set; }

        //Mapping 1(Shop): 1 (ShopRentalVoteAssign)
        public virtual Shop? Shop { get; set; }  

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
