// Ignore Spelling: Sabha

using System.ComponentModel.DataAnnotations;

namespace CAT20.Core.Models.AssessmentTax
{
    public class Ward
    {


        [Key]
        public int? Id { get; set; }
        [Required]
        [StringLength(50)]
        public string? WardName { get; set; }
        [StringLength(5)]
        [Required]
        public string? WardNo { get; set; }
        [StringLength(10)]
        public string? WardCode { get; set; }
        [Required]
        public int OfficeId { get; set; }
        [Required]
        public int SabhaId { get; set; }
        public virtual ICollection<Street>? Streets { get; set; }

        // mandatory fields
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }


    }
}
