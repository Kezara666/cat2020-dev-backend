namespace CAT20.WebApi.Resources.OnlinePayment;

public class PaymentGatewayBasicResource
{
    public int? Id { get; set; }
    public int? SabhaId { get; set; }
    public int? ProvinceId { get; set; }
    public string BankName { get; set;}
    public decimal ServicePercentage { get; set; }
}