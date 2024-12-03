using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Models.Enums;
using CAT20.WebApi.Resources.User;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CAT20.WebApi.Resources.AssessmentTax
{
    public class AssessmentATDResource
    {
        [Key]
        public int? Id { get; set; }

        public int? AssessmentId { get; set; }

        public virtual AssessmentResource? Assessment { get; set; }

        public ATDRequestStatus ATDRequestStatus { get; set; }

        public DateTime? RequestDate { get; set; }

        public int? RequestBy { get; set; }
        public UserActionByResources? UserRequestBy { get; set; }

        public string? RequestNote { get; set; }

        public DateTime? ActionDate { get; set; }

        public int? ActionBy { get; set; }
        public UserActionByResources? UserActionBy { get; set; }

        public string? ActionNote { get; set; }

        public int? OfficeId { get; set; }
        public int? SabhaId { get; set; }

        public int? Status { get; set; }

        public virtual ICollection<AssessmentATDOwnerslog> AssessmentATDOwnerslogs { get; set; }
    }
}
