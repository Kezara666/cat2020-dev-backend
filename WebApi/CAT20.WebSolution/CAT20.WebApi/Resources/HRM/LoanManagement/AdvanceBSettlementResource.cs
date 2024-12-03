using CAT20.Core.Models.HRM.LoanManagement;

namespace CAT20.WebApi.Resources.HRM.LoanManagement
{
    public class AdvanceBSettlementResource
    {
        public int Id { get; set; }
        public int LoanId { get; set; }
        public int PayMonth { get; set; }
        public decimal InstallmentAmount { get; set; }
        public decimal InterestAmount { get; set; }
        public decimal Balance { get; set; }

    }
}
