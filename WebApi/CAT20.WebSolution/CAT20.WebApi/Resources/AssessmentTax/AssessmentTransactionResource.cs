namespace CAT20.WebApi.Resources.AssessmentTax
{
    public class AssessmentTransactionResource
    {
        public int? Id { get; set; }
        public DateTime? DateTime { get; set; }
        //public AssessmentTransactionsType Type { get; set; }
        public string? Type { get; set; }
        public decimal? LYArrears { get; set; }
        public decimal? LYWarrant { get; set; }
        public decimal? TYArrears { get; set; }
        public decimal? TYWarrant { get; set; }
        public decimal? Q1 { get; set; }
        public decimal? Q2 { get; set; }
        public decimal? Q3 { get; set; }
        public decimal? Q4 { get; set; }

        public decimal? RunningOverPay { get; set; }
        public decimal? RunningDiscount { get; set; }
        public decimal? RunningTotal { get; set; }
    }
}
