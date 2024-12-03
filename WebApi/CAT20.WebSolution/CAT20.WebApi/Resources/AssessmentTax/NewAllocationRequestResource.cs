using CAT20.Core.Models.AssessmentTax;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CAT20.WebApi.Resources.AssessmentTax
{
    public class NewAllocationRequestResource
    {
        public int? Id { get; set; }
        public decimal? AllocationAmount { get; set; }
        public string? AllocationDescription { get; set; }
        public int? AssessmentId { get; set; }
        //public virtual Assessment? Assessment { get; set; }

        public int ActivationYear { get; set; }
        public int ActivationQuarter { get; set; }

        public int? Status { get; set; }
        // mandatory fields
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
