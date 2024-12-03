using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.FinalAccount
{
    public class VoteJournalItemTo
    {

        public int Id { get; set; }

        public int VoteJournalAdjustmentId { get; set; }
        public int ToVoteBalanceId { get; set; }

        public int ToVoteDetailId { get; set; }

        [Precision(18, 2)]
        public decimal ToAmount { get; set; }

        public VoteJournalAdjustment? VoteJournalAdjustment { get; set; }
        // mandatory fields
        public int? RowStatus { get; set; }
    }
}
