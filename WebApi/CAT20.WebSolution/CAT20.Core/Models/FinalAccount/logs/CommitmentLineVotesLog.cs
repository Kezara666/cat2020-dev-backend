using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.FinalAccount.logs
{
    public class CommitmentLineVotesLog
    {
        public int? Id { get; set; }
        public int CommitmentLineLogId { get; set; }
        public int? CommitmentLineVoteId { get; set; }
        public int VoteId { get; set; }
        public string VoteCode { get; set; }
        public int? VoteAllocationId { get; set; }
        [Precision(18, 2)]
        public decimal Amount { get; set; }
        [Precision(18, 2)]
        public decimal RemainingAmount { get; set; }
        public Enums.PaymentStatus? PaymentStatus { get; set; }

        [JsonIgnore]
        public virtual CommitmentLineLog? CommitmentLineLog { get; set; }
    }
}
