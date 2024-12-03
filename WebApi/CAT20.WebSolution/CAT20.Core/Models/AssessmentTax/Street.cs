using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CAT20.Core.Models.AssessmentTax
{
    public class Street
    {

        [Key]
        public int? Id { get; set; }
        [Required]
        [StringLength(250)]
        public string? StreetName { get; set; }
        [StringLength(5)]
        [Required]
        public string? StreetNo { get; set; }
        [StringLength(10)]
        public string? StreetCode { get; set; }
        [Required]
        public int WardId { get; set; }
        public virtual Ward? Ward { get; set; }
        public virtual ICollection<Assessment>? Assessments { get; set; }

        // mandatory fields
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

    }
}
