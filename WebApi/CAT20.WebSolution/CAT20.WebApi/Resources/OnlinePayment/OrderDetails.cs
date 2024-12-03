using CAT20.Core.Models.OnlienePayment;

namespace CAT20.WebApi.Resources.OnlinePayment;

public class OrderDetails
{
    // public string? descripton { get; set; }
    // public string? partnerId { get; set; }
    // public string? paymentOptions { get; set; }
    // public string? orderId { get; set; }
    // public string? ammount { get; set; }
    // public string? status { get; set; }
    // public string? session { get; set; }
    //
    // public string? generatedID { get; set; }
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
    public DateTime? Time { get; set; }

    public int? OfficeSessionId { get; set; }
    
    public int? Check { get; set; }
    
    public int? CashierId { get; set; } 
    public OtherDescription? OtherDescription { get; set; }

    
}