using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.Vote;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Core.DTO.Final
{

    public class VoteJournalItemFromResource
    {
        public int Id { get; set; }

        public int VoteJournalAdjustmentId { get; set; }
        public int FromVoteBalanceId { get; set; }
        public int FromVoteDetailId { get; set; }

        public decimal FromAmount { get; set; }

        //public VoteJournalAdjustment? VoteJournalAdjustment { get; set; }
        // mandatory fields
        //public int? RowStatus { get; set; }

        public VoteDetailLimitedresource? FromVoteDetail { get; set; }

    }
}
