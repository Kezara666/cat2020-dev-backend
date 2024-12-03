using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.ComTypes;
using CAT20.Core.Models.OnlinePayment;

namespace CAT20.Core.Models.OnlienePayment;

public class PaymentDetails
{
    public int? PaymentDetailId { get; set; }
    public string? TransactionId { get; set; }
    public string? SessionId { get; set; }
    public string? ResultIndicator { get; set; }
    public int? Status { get; set; }
    
    public int? Error { get; set; }
    public string? Description { get; set; }
    public decimal? InputAmount { get; set; }
    public decimal? Discount { get; set; }
    public decimal? TotalAmount { get; set; }
    
    public decimal? ServicePercentage { get; set; }
    public decimal? ServiceCharges { get; set; }
    public int? OrderId { get; set; }
    
    public string? AccountNo { get; set; }

    public int? PartnerId { get; set; }
    public string? PartnerName { get; set; }
    public string? PartnerNIC { get; set; }
    public string? PartnerMobileNo { get; set; }
    
    public string? PartnerEmail { get; set; }
    
    public int? SabhaId { get; set; }
    public int? OfficeId { get; set; }
    public DateTime? CreatedAt { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
    
    
    public int? OfficeSessionId { get; set; }
    
    public int? Check { get; set; }
    
    public int? CashierId { get; set; } 
    public DateTime? CashierUpdatedAt { get; set; } 

    
    // public int? MixinId { get; set; }
    
    public OtherDescription? OtherDescription { get; set; }
    public Dispute? Dispute { get; set; }


}