using CAT20.Core.Models.AssessmentTax.AssessmentBalanceHistory.AssessmentBalanceHistory;
using CAT20.Core.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CAT20.Core.Models.AssessmentTax
{
    public class AssessmentBalancesHistory
    {

        [Key]
        public int? Id { get; set; }
        public int? AssessmentId { get; set; }
        public int Year { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndData { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal? ExcessPayment { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal? LYWarrant { get; set; }
        [Required]
        [Precision(18, 2)]
        public decimal? LYArrears { get; set; }
        [Required]
        [Precision(18, 2)]
        public decimal? TYWarrant { get; set; }
        [Required]
        [Precision(18, 2)]
        public decimal? TYArrears { get; set; }
        [Required]
        [Precision(18, 2)]
        public decimal? ByExcessDeduction { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal? Paid { get; set; }

        [Required]
        public int? NumberOfPayments { get; set; }


        [Required]
        public int? NumberOfCancels { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal? OverPayment { get; set; }
        [Required]
        [Precision(18, 2)]
        public decimal? DiscountRate { get; set; }
        [Required]
        [Precision(18, 2)]
        public decimal? Discount { get; set; }
        [Required]
        [Precision(18, 2)]
        public bool? IsCompleted { get; set; }


        public virtual QH1? QH1 { get; set; }
        public virtual QH2? QH2 { get; set; }
        public virtual QH3? QH3 { get; set; }
        public virtual QH4? QH4 { get; set; }



        public virtual Assessment? Assessment { get; set; }

        public AssessmentTransactionsType TransactionsType { get; set; }
        public DateTime SessionDate { get; set; }

        // mandatory fields
        public int? Status { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        [ConcurrencyCheck]
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
