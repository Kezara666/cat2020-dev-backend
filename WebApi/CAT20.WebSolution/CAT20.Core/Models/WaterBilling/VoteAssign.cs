using CAT20.Core.Models.Mixin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.WaterBilling
{
    public class VoteAssign
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        public int? WaterProjectId { get; set; }

        [Required]
        public int? PaymentCategoryId { get; set; }
        [Required]
        public int? vote { get; set; }
        public virtual PaymentCategory? PaymentCategory { get; set; }
        public VoteAssignmentDetails? voteAssignmentDetails { get; set; }

        // mandatory fields

        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public int VoteDetailsId { get; set; }
    }
}
