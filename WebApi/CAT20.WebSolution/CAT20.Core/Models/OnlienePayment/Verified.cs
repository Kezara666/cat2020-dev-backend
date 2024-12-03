namespace CAT20.Core.Models.OnlienePayment;

public class Verified
{
    public String? MobileNo { get; set; }
    public String? Email { get; set; }

    public string? Text { get; set; }
    public string? Module { get; set; }
    public string? Subject { get; set; }
    public int? SabhaId { get; set; }
    
    public int? OTP { get; set; }
    
    public Boolean? isIntheSysterm { get; set;}
    
    public int? id { get; set; }
    public int? pNumber { get; set; }


}