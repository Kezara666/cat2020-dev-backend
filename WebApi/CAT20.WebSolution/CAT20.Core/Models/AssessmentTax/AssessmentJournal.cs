using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CAT20.Core.Models.AssessmentTax
{
    public class AssessmentJournal
    {
        [Key]
        public int? Id { get; set; }

        public int? AssessmentId { get; set; }

        [JsonIgnore]
        public virtual Assessment? Assessment { get; set; }

        [Precision(18, 2)]
        public decimal? OldAllocation { get; set; }


        [Precision(18, 2)]
        public decimal? NewAllocation { get; set; }

        [Precision(18, 2)]
        public decimal? OldExcessPayment { get; set; }


        [Precision(18, 2)]
        public decimal? NewExcessPayment { get; set; }

        [Precision(18, 2)]
        public decimal? OldLYArrears { get; set; }


        [Precision(18, 2)]
        public decimal? NewLYArrears { get; set; }

        [Precision(18, 2)]
        public decimal? OldLYWarrant { get; set; }


        [Precision(18, 2)]
        public decimal? NewLYWarrant { get; set; }

        [Precision(18, 2)]
        public decimal? OldTYArrears { get; set; }


        [Precision(18, 2)]
        public decimal? NewTYArrears { get; set; }
        [Precision(18, 2)]
        public decimal? NewTYWarrant { get; set; }

        [Precision(18, 2)]
        public decimal? OldTYWarrant { get; set; }


        public string? changingProperties { get; set; }

        [Required]
        public int? DraftApproveReject { get; set; }

        public int? OldPropertyTypeId { get; set; }
        public int? NewPropertyTypeId { get; set; }


        [Required]
        public DateTime? RequestDate { get; set; }

        [Required]
        public int? RequestBy { get; set; }

        public string? RequestNote { get; set; }


        public DateTime? ActionDate { get; set; }

        public int? ActionBy { get; set; }

        public string? ActionNote { get; set; }

        [Precision(18, 2)]
        public decimal?  Q1Adjustment { get; set; }

        [Precision(18, 2)]
        public decimal?  Q2Adjustment { get; set; }
        [Precision(18, 2)]
        public decimal?  Q3Adjustment { get; set; }
        [Precision(18, 2)]
        public decimal?  Q4Adjustment { get; set; }
    }
}
