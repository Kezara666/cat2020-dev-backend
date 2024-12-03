using CAT20.WebApi.Resources.User;
using Microsoft.EntityFrameworkCore;

namespace CAT20.WebApi.Resources.AssessmentTax
{
    public class AssessmentJournalResource
    {
        public int? Id { get; set; }

        public int? AssessmentId { get; set; }

        public virtual AssessmentResource? Assessment { get; set; }

        public decimal? OldAllocation { get; set; }


        public decimal? NewAllocation { get; set; }

        public decimal? OldExcessPayment { get; set; }


        public decimal? NewExcessPayment { get; set; }

        public decimal? OldLYArrears { get; set; }


        public decimal? NewLYArrears { get; set; }

        public decimal? OldLYWarrant { get; set; }


        public decimal? NewLYWarrant { get; set; }

        public decimal? OldTYArrears { get; set; }


        public decimal? NewTYArrears { get; set; }
        public decimal? NewTYWarrant { get; set; }

        public decimal? OldTYWarrant { get; set; }


        public string? changingProperties { get; set; }

        public int? DraftApproveReject { get; set; }


        public int? OldPropertyTypeId { get; set; }
        public int? NewPropertyTypeId { get; set; }

        public virtual AssessmentPropertyTypeResource? OldPropertyType { get; set; }
        public virtual AssessmentPropertyTypeResource? NewPropertyType { get; set; }



        public DateTime? RequestDate { get; set; }

        public int? RequestBy { get; set; }
        public UserActionByResources? UserRequestBy { get; set; }

        public string? RequestNote { get; set; }


        public DateTime? ActionDate { get; set; }

        public int? ActionBy { get; set; }

        public UserActionByResources? UserActionBy { get; set; }

        public string? ActionNote { get; set; }

        [Precision(18, 2)]
        public decimal? Q1Adjustment { get; set; }

        [Precision(18, 2)]
        public decimal? Q2Adjustment { get; set; }
        [Precision(18, 2)]
        public decimal? Q3Adjustment { get; set; }
        [Precision(18, 2)]
        public decimal? Q4Adjustment { get; set; }
    }
}
