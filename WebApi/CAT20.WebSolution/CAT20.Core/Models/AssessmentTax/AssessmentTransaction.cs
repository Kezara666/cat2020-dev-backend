using CAT20.Core.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CAT20.Core.Models.AssessmentTax
{
    public partial class AssessmentTransaction
    {
        public int? Id { get; set; }
        public DateTime? DateTime { get; set; }
        public DateTime? SessionDate { get; set; }
        public AssessmentTransactionsType Type { get; set; }
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
        [Required]
        [Precision(18, 2)]
        public decimal? Q1 { get; set; }
        [Required]
        [Precision(18, 2)]
        public decimal? Q2 { get; set; }
        [Required]
        [Precision(18, 2)]
        public decimal? Q3 { get; set; }
        [Required]
        [Precision(18, 2)]
        public decimal? Q4 { get; set; }
        [Required]
        [Precision(18, 2)]

        public decimal? RunningOverPay { get; set; }
        [Required]
        [Precision(18, 2)]
        public decimal? DiscountRate { get; set; }
        [Required]
        [Precision(18, 2)]


        public decimal? RunningDiscount { get; set; }
        [Required]
        [Precision(18, 2)]
        public decimal? RunningTotal { get; set; }






        public int? AssessmentId { get; set; }
        public virtual Assessment? Assessment { get; set; }

    }
}
