using CAT20.Core.Models.Control;

namespace CAT20.WebApi.Resources.Control;

public class PartnerMobileResource
{
    public int? Id { get; set; }
    public string? NIC { get; set; }
    
    public string? NickName { get; set; }
    public string? MobileNo { get; set; }
    
    public int? PartnerId { get; set; }
    public virtual Partner? Partner { get; set; }
}