using CAT20.Core.Models.Enums;

namespace CAT20.WebApi.Resources.Vote.Save
{
    public class SaveBudgetResource
    {
        public int? Id { get; set; }
        public int? Year { get; set; }
        //public BudgetType BudgetType { get; set; }
        public int VoteDetailId { get; set; }
        //public int CustomVoteId { get; set; }
        public decimal? BudgetTotal { get; set; }
        public decimal? Q1Amount { get; set; }
        public decimal? Q2Amount { get; set; }
        public decimal? Q3Amount { get; set; }
        public decimal? Q4Amount { get; set; }
        public decimal? AnnualAmount { get; set; }
        public decimal? January { get; set; }
        public decimal? February { get; set; }
        public decimal? March { get; set; }
        public decimal? April { get; set; }
        public decimal? May { get; set; }
        public decimal? June { get; set; }
        public decimal? July { get; set; }
        public decimal? August { get; set; }
        public decimal? September { get; set; }
        public decimal? October { get; set; }
        public decimal? November { get; set; }
        public decimal? December { get; set; }
        public int Status { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int SabhaID { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
