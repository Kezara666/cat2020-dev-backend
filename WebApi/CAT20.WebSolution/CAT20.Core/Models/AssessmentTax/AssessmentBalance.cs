using CAT20.Core.Models.AssessmentTax.Quarter;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CAT20.Core.Models.AssessmentTax
{
    public partial class AssessmentBalance :ICloneable
    {
        [Key]
        public int? Id { get; set; }
        public int AssessmentId { get; set; }
        public int Year { get; set; }
        public DateOnly? StartDate { get; set; }
        [Required]
        [Precision(18, 2)]
        public decimal? ExcessPayment { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal? LYArrears { get; set; }
        [Required]
        [Precision(18, 2)]
        public decimal? LYWarrant { get; set; }
        [Required]
        [Precision(18, 2)]
        public decimal? TYArrears { get; set; }
        [Required]
        [Precision(18, 2)]
        public decimal? TYWarrant { get; set; }

        [Precision(18, 2)]
        public decimal? AnnualAmount { get; set; }
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

        [Range(1, 4, ErrorMessage = "CurrentQuarter must be between 1 and 4.")]
        public int? CurrentQuarter { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? HasTransaction { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal? ReportBalance { get; set; }


        public virtual Q1? Q1 { get; set; }
        public virtual Q2? Q2 { get; set; }
        public virtual Q3? Q3 { get; set; }
        public virtual Q4? Q4 { get; set; }

        //public double? LQArrears { get; set; }
        //public double? LQWarrant { get; set; }
        //public double? LQCArrears { get; set; }
        //public double? LQCWarrant { get; set; }
        //public double? HaveToQPay { get; set; }
        //public double? QPay { get; set; }
        //public double? QDiscount { get; set; }
        //public double? QTotal { get; set; }
        //public double? FullTotal { get; set; }
        //public double? ProcessUpdateWarrant { get; set; }
        //public double? ProcessUpdateArrears { get; set; }
        //public string? ProcessUpdateComment { get; set; }
        //public double? OldArrears { get; set; }
        //public double? OldWarrant { get; set; }



        public virtual NQ1? NQ1 { get; set; }
        public virtual NQ2? NQ2 { get; set; }
        public virtual NQ3? NQ3 { get; set; }
        public virtual NQ4? NQ4 { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal? LYArrearsAdjustment { get; set; }
        [Required]
        [Precision(18, 2)]
        public decimal? LYWarrantAdjustment { get; set; }
        [Required]
        [Precision(18, 2)]
        public decimal? TYArrearsAdjustment { get; set; }
        [Required]
        [Precision(18, 2)]
        public decimal? TYWarrantAdjustment { get; set; }
        [Required]
        [Precision(18, 2)]
        public decimal? OverPayAdjustment { get; set; }


        public virtual Assessment? Assessment { get; set; }


        // mandatory fields
        public int? Status { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }


        [ConcurrencyCheck]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public object DeepClone()
        {
            return new AssessmentBalance
            {
                Id = this.Id,
                AssessmentId = this.AssessmentId,
                Year = this.Year,
                StartDate = this.StartDate,
                ExcessPayment = this.ExcessPayment,
                LYArrears = this.LYArrears,
                LYWarrant = this.LYWarrant,
                TYArrears = this.TYArrears,
                TYWarrant = this.TYWarrant,
                AnnualAmount = this.AnnualAmount,
                ByExcessDeduction = this.ByExcessDeduction,
                Paid = this.Paid,
                NumberOfPayments = this.NumberOfPayments,
                NumberOfCancels = this.NumberOfCancels,
                OverPayment = this.OverPayment,
                DiscountRate = this.DiscountRate,
                Discount = this.Discount,
                CurrentQuarter = this.CurrentQuarter,
                IsCompleted = this.IsCompleted,
                HasTransaction = this.HasTransaction,
                ReportBalance = this.ReportBalance,
                Q1 = (Q1)this.Q1?.RestClone(),
                Q2 = (Q2)this.Q2?.RestClone(),
                Q3 = (Q3)this.Q3?.RestClone(),
                Q4 = (Q4)this.Q4?.RestClone(),

            };


      }      }
}
