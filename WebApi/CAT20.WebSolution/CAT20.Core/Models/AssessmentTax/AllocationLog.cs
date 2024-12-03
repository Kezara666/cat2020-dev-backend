using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CAT20.Core.Models.AssessmentTax
{
    public class AllocationLog
    {
        [Key]
        public int? Id { get; set; }
        [Required]
        [Precision(18, 2)]
        public decimal? Amount { get; set; }
        public DateOnly? FromDate { get; set; }
        public DateOnly? ToDate { get; set; }
        public string? Description { get; set; }
        [Required]
        public int? AllocationId { get; set; }
        [JsonIgnore]
        public virtual Allocation? Allocation { get; set; }


        public int? Status { get; set; }

        // mandatory fields
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
