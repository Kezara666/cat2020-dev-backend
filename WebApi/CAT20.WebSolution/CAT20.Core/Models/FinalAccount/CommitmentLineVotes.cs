using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CAT20.Core.Models.Vote;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Core.Models.FinalAccount
{
    public class CommitmentLineVotes
    {
        [Key]
        public int? Id { get; set; }
        public int CommitmentLineId { get; set; }
        public string? Comment { get; set; }
        public int VoteId { get; set; }
        public string VoteCode { get; set; }
        public int? VoteAllocationId { get; set; }
        [Required]
        [Precision(18, 2)]
        public decimal Amount { get; set; }
        [Required]
        [Precision(18, 2)]
        public decimal RemainingAmount { get; set; }
        public Enums.PaymentStatus? PaymentStatus { get; set; }

        public int? RowStatus { get; set; }

        [JsonIgnore]
        public virtual CommitmentLine? CommitmentLine { get; set; }
        //public virtual List<CommitmentLineCustomVotes>? CommitmentLineCustomVotes { get; set; }


        // [JsonIgnore]
        // public  VoteAllocation? VoteAllocation { get; set; }


        //[ConcurrencyCheck]
        //[Timestamp]
        //public byte[] RowVersion { get; set; }
    }
}