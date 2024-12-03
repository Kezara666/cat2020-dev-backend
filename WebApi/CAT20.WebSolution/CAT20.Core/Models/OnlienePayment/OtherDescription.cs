namespace CAT20.Core.Models.OnlienePayment;

public class OtherDescription
{
    public int? Id { get; set; }
    public string? Description { get; set; }
    
    public virtual PaymentDetails PaymentDetails { get; set; }
}