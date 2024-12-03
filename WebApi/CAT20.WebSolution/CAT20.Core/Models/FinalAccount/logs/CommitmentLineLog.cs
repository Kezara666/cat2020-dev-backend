using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.FinalAccount.logs
{
    public class CommitmentLineLog
    {

        public int? Id { get; set; }
        public int CommitmentLogId { get; set; }
        public int? CommitmentLineId { get; set; }
        [Precision(18, 2)]
        public decimal? Amount { get; set; }
        public string? Comment { get; set; }
        [JsonIgnore]
        public virtual CommitmentLog? CommitmentLog { get; set; }

        public virtual List<CommitmentLineVotesLog>? CommitmentLineVotesLog { get; set; }

        public CommitmentLineLog()
        {
            CommitmentLineVotesLog = new List<CommitmentLineVotesLog>();
        }
    }
}
