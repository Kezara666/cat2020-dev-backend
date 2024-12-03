using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CAT20.Core.Models.AssessmentTax.Quarter
{
    public class Q1 : ICloneable
    {
        [Key]
        public int? Id { get; set; }
        [Required]
        [Precision(18, 2)]
        public decimal? Amount { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal? ByExcessDeduction { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal? Paid { get; set; }
        [Required]
        [Precision(18, 2)]
        public decimal? Discount { get; set; }
        [Required]
        [Precision(18, 2)]
        public decimal? Warrant { get; set; }
        [Range(1, 2, ErrorMessage = "Value must be either 1 or 2")]
        public int? WarrantMethod { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [Required]
        public bool IsCompleted { get; set; }
        [Required]
        public bool IsOver { get; set; }
        [Required]
        public int? BalanceId { get; set; }
        public virtual AssessmentBalance? AssessmentBalance { get; set; }

        public int? WarrantBy { get; set; }
        [Precision(18, 2)]
        public decimal? DiscountRate { get; set; }
        [Precision(18, 2)]
        public decimal? WarrantRate { get; set; }


        [ConcurrencyCheck]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        [Precision(18, 2)]
        public decimal? Adjustment { get; set; }
        [Precision(18, 2)]
        public decimal? QReportAdjustment { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public object RestClone()
        {
            return new Q1
            {
                Id = this.Id,
                Amount = this.Amount,
                ByExcessDeduction = 0,
                Paid = 0,
                Discount = 0,
                Warrant = 0,
                WarrantMethod = 0,
                StartDate = this.StartDate,
                EndDate = this.EndDate,
                IsCompleted = this.IsCompleted,
                IsOver = this.IsOver,
                BalanceId = 0,
                AssessmentBalance = this.AssessmentBalance,
                WarrantBy = 0,
                DiscountRate = 0,
                WarrantRate = 0,
                RowVersion = this.RowVersion
            };

        }

    }
}
