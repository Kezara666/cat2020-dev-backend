using System;

namespace CAT20.Core.DTO.Final.Save
{
    public class SaveCommitmentLineVotesResource
    {

        public int? Id { get; set; }
        public int? CommitmentLineId { get; set; }
        public string? Comment { get; set; }
        public int VoteId { get; set; }
        public string VoteCode { get; set; }
        public int? VoteAllocationId { get; set; }
        public decimal Amount { get; set; }
        public decimal RemainingAmount { get; set; }
        public Core.Models.Enums.PaymentStatus? PaymentStatus { get; set; }
    }
}
