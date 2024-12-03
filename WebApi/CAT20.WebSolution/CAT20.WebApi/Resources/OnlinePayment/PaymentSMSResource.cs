namespace CAT20.WebApi.Resources.OnlinePayment;

public class PaymentSMSResource
{
    public String? MobileNo { get; set; }
    public string Text { get; set; }
    public int SabhaId { get; set; }
    public int OTP { get; set; }

    public string? Module { get; set; }
    public string? Subject { get; set; }
    
    public int? id { get; set; }
    public int? pNumber { get; set; }
}