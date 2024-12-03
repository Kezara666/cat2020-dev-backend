using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.FinalAccount
{
    public class FR66ToItem
    {
        public int Id { get; set; }

        public int FR66Id { get; set; }
        public int ToVoteBalanceId { get; set; }

        public int ToVoteDetailId { get; set; }

        [Precision(18, 2)]
        public decimal ToAmount { get; set; }

        [Precision(18, 2)]
        public decimal? RequestSnapshotAllocation { get; set; }
        [Precision(18, 2)]
        public decimal? ActionSnapshotAllocation { get; set; }

        [Precision(18, 2)]
        public decimal? RequestSnapshotBalance { get; set; }
        [Precision(18, 2)]
        public decimal? ActionSnapshotBalance { get; set; }

        public FR66Transfer? FR66Transfer { get; set; }
        // mandatory fields
        public int? RowStatus { get; set; }
    }
}
