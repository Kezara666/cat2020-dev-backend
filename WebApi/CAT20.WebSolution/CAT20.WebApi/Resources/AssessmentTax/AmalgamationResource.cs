using CAT20.Core.Models.AssessmentTax;
using System.ComponentModel.DataAnnotations;

namespace CAT20.WebApi.Resources.AssessmentTax
{
    public class AmalgamationResource
    {
        public int? Id { get; set; }
        public int? AmalgamationSubDivisionId { get; set; }

        public int? KFormId { get; set; }

        public virtual AssessmentResource? Assessment { get; set; }
    }
}
