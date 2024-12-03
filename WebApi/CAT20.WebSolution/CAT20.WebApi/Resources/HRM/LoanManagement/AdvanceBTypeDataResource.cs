using CAT20.Core.Models.HRM.LoanManagement;

namespace CAT20.WebApi.Resources.HRM.LoanManagement
{
    public class AdvanceBTypeDataResource
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal? Interest { get; set; }
        public int? MaxInstalment { get; set; }
        public bool HasInterest { get; set; }
        public int AccountSystemVersionId { get; set; }

        // Navigation property
        public ICollection<AdvanceBTypeLedgerMappingResource>? AdvanceBTypeLedgerMapping { get; set; }

    }
}
