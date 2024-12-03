using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CAT20.Core.Models.Mixin
{
    public partial class MixinCancelOrder
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
        [JsonIgnore]
        public virtual MixinOrder MixinOrder { get; set; }
    }
}