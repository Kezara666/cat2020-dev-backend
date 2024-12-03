using CAT20.Core.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CAT20.Core.Models.FinalAccount.logs;

public class CommitmentLog
{
    public int? Id { set; get; }
    public int? CommitmentId { get; set; }
    
    [JsonIgnore]
    public virtual Commitment? Commitment { get; set; }

    public int sabhaId { get; set; }

    public int BankId { get; set; }

    public VoucherPayeeCategory PayeeCategory { get; set; }

    public int PayeeId { get; set; }

    public string PayeeName { get; set; }

    public decimal TotalAmount { get; set; }

    public string Description { get; set; }

    public string? CommitmentSequenceNumber { get; set; }


    public Enums.FinalAccountActionStates? ActionState { get; set; }

    public bool HasVoucher { get; set; }

    public virtual List<CommitmentLineLog>? CommitmentLineLog { get; set; }

    public int? SessionId { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }

    // mandatory fields
    public int? RowStatus { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }

    [Required]
    public DateTime? SystemUpdateAt { get; set; }

    [ConcurrencyCheck]
    [Timestamp]
    public byte[] RowVersion { get; set; }
    public CommitmentLog()
    {
        CommitmentLineLog = new List<CommitmentLineLog>(); 
    }
}