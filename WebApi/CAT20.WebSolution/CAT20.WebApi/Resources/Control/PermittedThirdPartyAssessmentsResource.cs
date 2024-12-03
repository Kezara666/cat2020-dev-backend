using CAT20.Core.Models.Control;
using CAT20.WebApi.Resources.Control;
using CAT20.WebApi.Resources.AssessmentTax;

namespace CAT20.WebApi.Resources.Control;

public class PermittedThirdPartyAssessmentsResource
{
    public int? Id { get; set; }
    public int PartnerId { get; set; }
    public int AssessmentId { get; set; }
    public virtual AssessmentResource? Assessment { get; set; }
    public virtual PartnerResource? Partner { get; set; }

    public int Status { get; set; }
    public int CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}