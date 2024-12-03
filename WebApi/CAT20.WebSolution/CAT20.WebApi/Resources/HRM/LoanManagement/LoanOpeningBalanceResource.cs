using CAT20.Core.Models.HRM.LoanManagement;

namespace CAT20.WebApi.Resources.HRM.LoanManagement
{
    public class LoanOpeningBalanceResource
    {
        public int Id { get; set; }
        public int LoanId { get; set; }
        public decimal SettledInstallment { get; set; }
        public decimal? SettledInterest { get; set; }
        public decimal OpeningBalance { get; set; }

    }
}
