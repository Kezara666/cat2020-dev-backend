using System.ComponentModel.DataAnnotations;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.AssessmentTax;
using CAT20.WebApi.Resources.OnlinePayment;

namespace CAT20.Core.Models.OnlienePayment;

public class PermittedThirdPartyAssessments
{
    [Key]
    public int? Id { get; set; }
    public int PartnerId { get; set; }
    public int AssessmentId { get; set; }
    public virtual Assessment? Assessment { get; set; }
    public virtual Partner? Partner { get; set; }
    public int Status { get; set; }
    public int CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}