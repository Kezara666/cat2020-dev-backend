using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.AssessmentTax
{
    public class NewAllocationRequest
    {

        [Key]
        public int? Id { get; set; }
        [Required]
        [Precision(18, 2)]
        public decimal? AllocationAmount { get; set; }
        public string? AllocationDescription { get; set; }
        [Required]
        public int? AssessmentId { get; set; }
        [JsonIgnore]
        public virtual Assessment? Assessment { get; set; }

      public  int ActivationYear { get; set; }
      public  int ActivationQuarter { get; set; }

        public int? Status { get; set; }
        // mandatory fields
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
