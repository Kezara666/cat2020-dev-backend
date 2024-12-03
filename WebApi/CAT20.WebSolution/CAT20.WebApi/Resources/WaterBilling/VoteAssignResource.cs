using CAT20.Core.Models.Mixin;
using CAT20.Core.Models.WaterBilling;
using CAT20.WebApi.Resources.Mixin;
using System.ComponentModel.DataAnnotations;

namespace CAT20.WebApi.Resources.WaterBilling
{
    public class VoteAssignResource
    {

     
        public int? Id { get; set; }
        public int? WaterProjectId { get; set; }
        public int? PaymentCategoryId { get; set; }
        public int? vote { get; set; }
        public virtual PaymentCategory? PaymentCategory { get; set; }
        public VoteAssignmentDetailsResource? voteAssignmentDetails { get; set; }

        // mandatory fields

        public int? Status { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
