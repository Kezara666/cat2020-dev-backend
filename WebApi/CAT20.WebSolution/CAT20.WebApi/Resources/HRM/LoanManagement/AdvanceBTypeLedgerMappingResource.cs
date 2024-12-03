using CAT20.Core.Models.Vote;

namespace CAT20.WebApi.Resources.HRM.LoanManagement
{
    public class AdvanceBTypeLedgerMappingResource
    {
        public int Id { get; set; }
        public int AdvanceBTypeId { get; set; }
        public string LedgerCode { get; set; }
        public int LedgerId { get; set; }
        public string Prefix { get; set; }
        public int LastIndex { get; set; }
        public int SabhaId { get; set; }

        public virtual VoteDetail VoteDetail { get; set; }
        // Mandatory fields
        //public int StatusId { get; set; }
    }
}
