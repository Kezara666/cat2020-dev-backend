// Ignore Spelling: Sabha

using CAT20.Core.Models.Mixin;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CAT20.Core.Models.AssessmentTax
{
    public class AssessmentVoteAssign
    {
        [Key]
        public int? Id { get; set; }
        [Required]
        public int? SabhaId { get; set; }
        [Required]
        public int? PaymentTypeId { get; set; }
        [Required]
        public int? VoteAssignmentDetailId { get; set; }
        public int? VoteDetailId { get; set; }
        [JsonIgnore]
        public virtual VotePaymentType? VotePaymentType { get; set; }
        [JsonIgnore]
        public virtual VoteAssignmentDetails? VoteAssignmentDetails { get; set; }

        // mandatory fields

        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
