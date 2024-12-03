using CAT20.Core.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace CAT20.Core.Models.AssessmentTax
{
    public class AssessmentAuditLog
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        public int? AssessmentId { get; set; }
        public virtual Assessment? Assessment { get; set; }
        public AssessmentAuditLogAction Action { get; set; }
        public DateTime Timestamp { get; set; }
        public int? ActionBy { get; set; }
        public AssessmentRelatedEntityType EntityType { get; set; }
    }
}
