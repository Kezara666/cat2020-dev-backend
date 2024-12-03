using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CAT20.Core.Models.Vote;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Core.Models.FinalAccount
{
    public class CommitmentLineCustomVotes
    {
        [Key]
        public int? Id { get; set; }
        public int CommitmentLineVotesLineId { get; set; }
        public string? Comment { get; set; }
        [Required]
        public int? CustomVoteDetailId { get; set; }
        public int? CustomVoteDetailIdParentId { get; set; }
        public int Depth { get; set; }
        public int? ParentId { get; set; }
        public bool? IsSubLevel { get; set; }


        [Required]
        [Precision(18, 2)]
        public decimal Amount { get; set; }
        [Required]
        [Precision(18, 2)]
        public decimal RemainingAmount { get; set; }
        public Enums.PaymentStatus? PaymentStatus { get; set; }

        public int? RowStatus { get; set; }

        [JsonIgnore]
        public virtual CommitmentLineVotes? CommitmentLineVotesLine { get; set; }


        //[ConcurrencyCheck]
        //[Timestamp]
        //public byte[] RowVersion { get; set; }
    }
}