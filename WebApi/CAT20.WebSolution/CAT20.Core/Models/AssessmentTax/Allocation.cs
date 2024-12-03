using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CAT20.Core.Models.AssessmentTax
{
    public partial class Allocation
    {

        [Key]
        public int? Id { get; set; }
        [Required]
        [Precision(18, 2)]
        public decimal? AllocationAmount { get; set; }
        public DateOnly? ChangedDate { get; set; }
        public string? AllocationDescription { get; set; }
        [Required]
        public int? AssessmentId { get; set; }
        [JsonIgnore]
        public virtual Assessment? Assessment { get; set; }

        [JsonIgnore]
        public virtual ICollection<AllocationLog>? AllocationLogs { get; set; }


        [Required]
        [Precision(18, 2)]
        public decimal? NextYearAllocationAmount { get; set; }

        public string? NextYearAllocationDescription { get; set; }


        public int? Status { get; set; }

        // mandatory fields
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

    }
}
