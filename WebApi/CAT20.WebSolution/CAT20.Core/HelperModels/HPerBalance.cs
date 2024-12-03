namespace CAT20.Core.HelperModels
{
    public class HPerBalance
    {
        public int AssessmentId { get; set; }

        public decimal? ExcessPayment { get; set; } = 0;
        public decimal? LYWarrant { get; set; } = 0;
        public decimal? LYArrears { get; set; } = 0;

        public decimal? TYWarrant { get; set; } = 0;
        public decimal? TYArrears { get; set; } = 0;

        public decimal? CurrentBalance { get; set; } = 0;
        public decimal? Total { get; set; } = 0;
        public decimal? DiscountRate { get; set; } = 0;
        public decimal? DiscountTotal { get; set; } = 0;

        public decimal? Q1 { get; set; } = 0;
        public decimal? Q2 { get; set; } = 0;
        public decimal? Q3 { get; set; } = 0;
        public decimal? Q4 { get; set; } = 0;
    }
}
