using Microsoft.EntityFrameworkCore;

namespace CAT20.WebApi.Resources.AssessmentTax
{
    public class QResource
    {
        public int? Id { get; set; }
        public decimal? Amount { get; set; }
        public decimal? ByExcessDeduction { get; set; }
        public decimal? Paid { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Warrant { get; set; }
        public decimal? WarrantMethod { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsOver { get; set; }

        public decimal? DiscountRate { get; set; }
        public decimal? WarrantRate { get; set; }

        public int? BalanceId { get; set; }
        //public virtual AssessmentBalanceResource? AssessmentBalance { get; set; }
    }
}
