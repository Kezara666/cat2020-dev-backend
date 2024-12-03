using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Models.Mixin;
using CAT20.WebApi.Resources.Mixin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.WebApi.Resources.AssessmentTax
{
    public class AssessmentVoteAssignResource
    {
        public int? Id { get; set; }
        public int? SabhaId { get; set; }
        public int? PaymentTypeId { get; set; }
        public int? VoteAssignmentDetailId { get; set; }
        public virtual VotePaymentTypeResource? VotePaymentType { get; set; }
        public virtual VoteAssignmentDetailsResource? VoteAssignmentDetails { get; set; }

        // mandatory fields

        public int? Status { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
