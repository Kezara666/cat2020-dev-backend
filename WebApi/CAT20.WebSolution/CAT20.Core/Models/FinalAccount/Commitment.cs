using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount.logs;
using CAT20.Core.Models.User;
using CAT20.Core.Models.Vote;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CAT20.Core.Models.FinalAccount
{
    public partial class Commitment
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        public int? SabhaId { get; set; }

        [Required]
        public int? BankId { get; set; }

        [Required]
        public VoucherPayeeCategory PayeeCategory { get; set; }

        [Required]
        public int? PayeeId { get; set; }

        [Required]
        public string? PayeeName { get; set; }
        [Precision(18, 2)]
        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public string? CommitmentSequenceNumber { get; set; }

        [Required]
        public FinalAccountActionStates? ActionState { get; set; }
        public PaymentStatus? PaymentStatus { get; set; }

        public bool HasVoucher { get; set; }


        public virtual List<CommitmentActionsLog>? ActionLog { get; set; }

        public virtual List<CommitmentLog>? CommitmentLog { get; set; }

        public virtual List<CommitmentLine>? CommitmentLine { get; set; }


        [Required]
        public int? SessionId { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public int Month { get; set; }

        // mandatory fields
        public int? RowStatus { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        [Required]
        public DateTime? SystemCreateAt { get; set; }

        public DateTime? SystemUpdateAt { get; set; }

        [ConcurrencyCheck]
        [Timestamp]
        public byte[] RowVersion { get; set; }


    }
}