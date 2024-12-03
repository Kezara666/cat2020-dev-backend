namespace CAT20.Core.HelperModels
{
    public class HAssessmentBalance
    {
        public int AssessmentId { get; set; }
        public decimal? LYWarrant { get; set; } = 0;
        public decimal? LYArrears { get; set; } = 0;

        public decimal? TYWarrant { get; set; } = 0;
        public decimal? TYArrears { get; set; } = 0;

        public decimal? Total { get; set; } = 0;
        public decimal? Discount { get; set; } = 0;
        public decimal? OverPayment { get; set; } = 0;

        public decimal? Q1 { get; set; } = 0;
        public decimal? Q2 { get; set; } = 0;
        public decimal? Q3 { get; set; } = 0;
        public decimal? Q4 { get; set; } = 0;
    }
}
