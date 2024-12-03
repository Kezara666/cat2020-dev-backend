using CAT20.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.AssessmentTax
{
    public class AmalgamationSubDivision
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int? SabhaId { get; set; }
        public int? ParentAssessmentId { get; set; }
        public int? DerivedAssessmentId { get; set; }

        public ICollection<AmalgamationSubDivisionActions>? AmalgamationSubDivisionActions { get; set; }
        public ICollection<AmalgamationSubDivisionDocuments>? AmalgamationSubDivisionDocuments { get; set; }
        public ICollection<Amalgamation>? Amalgamations { get; set; }
        public ICollection<SubDivision>? SubDivisions { get; set; }

        public AssessmentStatus? RequestedAction { get; set; }
        public int? Status { get; set; }

        [Range(1, 2, ErrorMessage = "Value must be either 1 or 2")]
        [Required]
        public int? Type { get; set; }

        // mandatory fields
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
