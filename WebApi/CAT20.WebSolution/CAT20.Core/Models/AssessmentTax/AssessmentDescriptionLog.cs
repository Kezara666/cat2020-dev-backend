using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CAT20.Core.Models.AssessmentTax
{
    public class AssessmentDescriptionLog
    {
        [Key]
        public int? Id { get; set; }

        public int DescriptionId { get; set; }
        [Required]
        [StringLength(150)]
        public string? Comment { get; set; }
        [JsonIgnore]
        public virtual Description? Description { get; set; }

        public int? ActionBy { get; set; }

        [StringLength(150)]
        public string? ActionNote { get; set; }

        public DateTime? ActivatedDate { get; set; }

        [Required]
        public int? AssessmentId { get; set; }

        [JsonIgnore]
        public virtual Assessment? Assessment { get; set; }

        public int ActivationYear { get; set; }
        public int ActivationQuarter { get; set; }

        // mandatory fields
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
