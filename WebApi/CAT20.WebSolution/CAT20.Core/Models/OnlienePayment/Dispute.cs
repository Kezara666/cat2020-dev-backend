
using CAT20.Core.Models.OnlienePayment;
using Newtonsoft.Json;

namespace CAT20.Core.Models.OnlinePayment;

public class Dispute
{
    public int? Id { get; set; }
    public int? PaymentDetailId { get; set; }
    public Enums.DisputeReason? Reason { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? UserId { get; set; }
    public string? Message { get; set; }
    
    [JsonIgnore]
    public PaymentDetails PaymentDetails { get; set; }
}