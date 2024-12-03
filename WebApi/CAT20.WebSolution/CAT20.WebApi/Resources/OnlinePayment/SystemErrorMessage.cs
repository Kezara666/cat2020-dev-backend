using CAT20.Core.Models.Enums;

namespace CAT20.WebApi.Resources.OnlinePayment;

public class SystemErrorMessage
{
    public string? PartnerMobileNo { get; set; }
    public int? SabhaId { get; set; }
    public string? TransactionId { get; set; }
    public int? Id { get; set; }
    public int? PaymentDetailId { get; set; }
    public DisputeReason? Reason { get; set; }
    public string? Message { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? UserId { get; set; }

}