namespace CAT20.WebApi.Resources.OnlinePayment;

public class PaymentGatewayResource
{
    public int? Id { get; set; }

    public int? SabhaId { get; set; }
    public int? ProvinceId { get; set; }

    public string BankName { get; set;}
    public string Description { get; set; }
    public string MerchantId { get; set; }
    public string APIKey { get; set; }
    public string? ReportAPIKey { get; set; }
    public decimal ServicePercentage { get; set; }

    //for Peoplesbank
    public string ProfileID { get; set; }
    public string AccessKey { get; set; }
    public string SecretKey { get; set; }

    public int? Status { get; set; }
}