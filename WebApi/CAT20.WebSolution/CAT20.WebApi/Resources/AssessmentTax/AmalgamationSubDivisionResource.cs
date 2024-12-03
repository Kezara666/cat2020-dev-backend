using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace CAT20.WebApi.Resources.AssessmentTax
{
    public class AmalgamationSubDivisionResource
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int? SabhaId { get; set; }
        public int? ParentAssessmentId { get; set; }
        public int? DerivedAssessmentId { get; set; }

        public ICollection<AmalgamationSubDivisionActionsResource>? AmalgamationSubDivisionActions { get; set; }
        //public ICollection<AmalgamationSubDivisionDocuments>? AmalgamationSubDivisionDocuments { get; set; }
        public ICollection<AmalgamationResource>? Amalgamations { get; set; }
        public ICollection<SubDivisionResource>? SubDivisions { get; set; }

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
