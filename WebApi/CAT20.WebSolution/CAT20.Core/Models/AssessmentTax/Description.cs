// Ignore Spelling: Sabha

using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CAT20.Core.Models.AssessmentTax
{
    public partial class Description
    {

        [Key]
        public int? Id { get; set; }
        [Required]
        public int? DescriptionNo { get; set; }
        public string? DescriptionText { get; set; }
        public int? SabhaId { get; set; }

        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        [JsonIgnore]
        public virtual ICollection<Assessment>? Assessments { get; set; }
    }
}
