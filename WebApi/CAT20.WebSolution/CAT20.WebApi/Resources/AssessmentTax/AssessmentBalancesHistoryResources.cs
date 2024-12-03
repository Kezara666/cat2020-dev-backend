using System.ComponentModel.DataAnnotations;

namespace CAT20.WebApi.Resources.AssessmentTax
{
    public class AssessmentBalancesHistoryResources
    {

        [Key]
        public int? Id { get; set; }
        public int? AssessmentId { get; set; }
        public DateOnly? Year { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndData { get; set; }


        public decimal? LYArrears { get; set; }
        public decimal? LYWarrant { get; set; }
        public decimal? TYArrears { get; set; }
        public decimal? TYWarrant { get; set; }

        public decimal? Paid { get; set; }
        public decimal? OverPayment { get; set; }
        public decimal? DiscountRate { get; set; }
        public decimal? Discount { get; set; }
        public bool? IsCompleted { get; set; }


        public virtual QResource? QH1 { get; set; }
        public virtual QResource? QH2 { get; set; }
        public virtual QResource? QH3 { get; set; }
        public virtual QResource? QH4 { get; set; }



        //public virtual AssessmentResource? Assessment { get; set; }


        // mandatory fields
        public int? Status { get; set; }

        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
