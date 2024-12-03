using CAT20.Core.Models;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Mixin;
using CAT20.Core.Models.Vote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAT20.WebApi.Resources.Mixin
{
    public partial class MixinCancelOrderResource
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int CreatedBy { get; set; }
        public int SessionId { get; set; }
        public int MixinOrderId { get; set; }
        public int? ApprovedBy { get; set; }
        public string? ApprovalComment { get; set; }

        //public virtual MixinOrder MixinOrder { get; set; }
    }
}
