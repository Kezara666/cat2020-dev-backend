using System.ComponentModel.DataAnnotations;
using CAT20.Core.Models.Control;

namespace CAT20.Core.Models.OnlienePayment;

public class PartnerMobile
{
    
    public int? Id { get; set; }
    public string? NIC { get; set; }

    public string? NickName { get; set; }
    public string? MobileNo { get; set; }
    
    public int? PartnerId { get; set; }
    public virtual Partner? Partner { get; set; }

    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

}