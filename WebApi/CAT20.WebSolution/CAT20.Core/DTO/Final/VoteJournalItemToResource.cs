using CAT20.Core.Models.FinalAccount;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Core.DTO.Final
{

    public class VoteJournalItemToResource
    {

        public int Id { get; set; }

        public int VoteJournalAdjustmentId { get; set; }
        public int ToVoteBalanceId { get; set; }

        public int ToVoteDetailId { get; set; }

        public decimal ToAmount { get; set; }

        //public VoteJournalAdjustment? VoteJournalAdjustment { get; set; }
        // mandatory fields
        //public int? RowStatus { get; set; }

        public VoteDetailLimitedresource? ToVoteDetail { get; set; }
    }
}
