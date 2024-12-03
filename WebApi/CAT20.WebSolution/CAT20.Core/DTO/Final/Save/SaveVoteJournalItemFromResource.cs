using CAT20.Core.Models.FinalAccount;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Core.DTO.Final.Save
{
    public class SaveVoteJournalItemFromResource
    {

        public int? Id { get; set; }

        public int? VoteJournalAdjustmentId { get; set; }
        public int FromVoteBalanceId { get; set; }
        public int FromVoteDetailId { get; set; }

        public decimal FromAmount { get; set; }

        //public VoteJournalAdjustmentResource? VoteJournalAdjustment { get; set; }
    }
}
