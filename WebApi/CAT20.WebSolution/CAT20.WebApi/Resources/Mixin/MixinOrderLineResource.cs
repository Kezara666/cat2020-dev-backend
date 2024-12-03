using CAT20.Core.Models;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Mixin;
using CAT20.Core.Models.Vote;
using CAT20.WebApi.Resources.Vote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAT20.WebApi.Resources.Mixin
{
    public partial class MixinOrderLineResource
    {
        public int Id { get; set; }
        public string CustomVoteName { get; set; }
        public string Description { get; set; }
        public decimal? Amount { get; set; }
        public decimal? PaymentVatAmount { get; set; }
        public decimal? PaymentNbtAmount { get; set; }
        public decimal? StampAmount { get; set; }
        public decimal? TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual VoteAssignmentDetails VoteAssignmentDetails { get; set; }
        public int MixinVoteAssignmentDetailId { get; set; }
        public int? VoteDetailId { get; set; }
        public int? ClassificationId { get; set; }
        public virtual PaymentVat PaymentVat { get; set; }
        public int? PaymentVatId { get; set; }
        public virtual PaymentNbt PaymentNbt { get; set; }
        public int? PaymentNbtId { get; set; }
        public virtual MixinOrder MixinOrder { get; set; }
        public int MixinOrderId { get; set; }
        public int VoteOrBal { get; set; }

        
        
        /*linking model*/

        public virtual VoteDetailResource? VoteDetail { get; set; }
    }
}
