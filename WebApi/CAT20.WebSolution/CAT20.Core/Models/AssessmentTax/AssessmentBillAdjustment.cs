using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.AssessmentTax
{
    public class AssessmentBillAdjustment
    {
        public int Id { get; set; }
        public int AssessmentId { get; set; }
        public int MixOrderId { get; set; }

        [Required]
        public DateTime? RequestDate { get; set; }

        [Required]
        public int? RequestBy { get; set; }

        public string? RequestNote { get; set; }

        [Required]
        public int? DraftApproveRejectWithdraw { get; set; }


        public DateTime? ActionDate { get; set; }

        public int? ActionBy { get; set; }

        public string? ActionNote { get; set; }

        public virtual Assessment? Assessment { get; set; }

    }
}
