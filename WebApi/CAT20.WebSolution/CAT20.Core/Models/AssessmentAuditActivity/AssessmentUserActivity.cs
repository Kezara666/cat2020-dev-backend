using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.AssessmentAuditActivity
{
    public class AssessmentUserActivity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int? key { get; set; }

        public AssessmentRelatedEntityType? Entity { get; set; }
        public int? serviceNo { get; set; }
        public AssessmentAuditLogAction Action { get; set; }
        public DateTime Timestamp { get; set; }
        public int? ActionBy { get; set; }

    }
}
