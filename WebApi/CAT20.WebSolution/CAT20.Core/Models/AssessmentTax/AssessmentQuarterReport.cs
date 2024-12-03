using CAT20.Core.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.AssessmentTax
{
    public class AssessmentQuarterReport
    {
        [Key]
        public int? Id { get; set; }
        public DateTime? DateTime { get; set; }
        public int Year { get; set; }
        public int QuarterNo { get; set; }

        [Precision(18, 2)]
        public decimal? AnnualAmount { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal? QAmount { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal? QWarrant { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal? QDiscount { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal? M1Paid { get; set; }
        [Required]
        [Precision(18, 2)]
        public decimal? M2Paid { get; set; }
        [Required]
        [Precision(18, 2)]
        public decimal? M3Paid { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal? LYArrears { get; set; }
        [Required]
        [Precision(18, 2)]
        public decimal? LYWarrant { get; set; }
        [Required]
        [Precision(18, 2)]
        public decimal? TYArrears { get; set; }
        [Required]
        [Precision(18, 2)]
        public decimal? TYWarrant { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal? RunningBalance { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal? Adjustment { get; set; }

        public int? AssessmentId { get; set; }
        public virtual Assessment? Assessment { get; set; }

        public AssessmentTransactionsType UseTransactionsType { get; set; }


        public ICollection<AssessmentQuarterReportLog>? AssessmentQuarterReportLogs { get; set; }

        // mandatory fields

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        [ConcurrencyCheck]
        [Timestamp]
        public byte[] RowVersion { get; set; }


    }
}
