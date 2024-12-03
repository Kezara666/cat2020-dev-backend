using CAT20.Core.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CAT20.Core.Models.FinalAccount.logs;

public class VoucherLog
{
    [Key]
    public int? Id { set; get; }
    public int? VoucherId { get; set; }
    [JsonIgnore]
    public virtual Voucher? Voucher { get; set; }

    public int CommitmentId { get; set; }
    public int SabhaID { get; set; }
    [Precision(18, 2)]
    public decimal VATTotal { get; set; }
    [Precision(18, 2)]
    public decimal NBTTotal { get; set; }
    [Precision(18, 2)]
    public decimal TotalAmount { get; set; }
    [Precision(18, 2)]
    public decimal Amount { get; set; }
    [Precision(18, 2)]
    public decimal? Stamp { get; set; }
    public Enums.FinalAccountActionStates? ActionState { get; set; }
    public VoucherCategory? VoucherCategory { get; set; }
    public string? VoucherSequenceNumber { get; set; }

    public List<VoucherLineLog>? voucherLineLog { get; set; }

    public int SessionId { get; set; }

    public int PayeeId { get; set; }
    // mandatory fields
    public int? RowStatus { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }

    public DateTime? SystemActionAt { get; set; }

    [Required]
    public DateTime? SystemCreateAt { get; set; }

    public DateTime? SystemUpdateAt { get; set; }

    [ConcurrencyCheck]
    [Timestamp]
    public byte[] RowVersion { get; set; }


    public VoucherLog()
    {
        voucherLineLog = new List<VoucherLineLog>();
    }

}