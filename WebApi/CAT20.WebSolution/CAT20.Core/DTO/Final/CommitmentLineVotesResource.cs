using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.Vote;
using CAT20.WebApi.Resources.Final;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Final
{
    public class CommitmentLineVotesResource
    {
        public int? Id { get; set; }
        public int? CommitmentLineId { get; set; }
        public int VoteId { get; set; }
        public string VoteCode { get; set; }
        public int? VoteAllocationId { get; set; }
        public decimal Amount { get; set; }
        public decimal RemainingAmount { get; set; }
        public Models.Enums.PaymentStatus? PaymentStatus { get; set; }

        public virtual CommitmentLineResource? CommitmentLine { get; set; }

        public VoteBalance? VoteAllocation { get; set; }
    }
}
