using System;
using System.Collections.Generic;
using CAT20.Core.Models.Interfaces;
using CAT20.Core.Models.Vote;
using Newtonsoft.Json;

namespace CAT20.Core.Models.AuditTrails
{
    public partial class AuditTrailDetail
    {
        private State _state = State.Unchanged;

        public int? ID { get; set; }
        [JsonIgnore]
        public int UserID { get; set; }
        public DateTime? TimeStamp { get; set; }
        [JsonIgnore]
        public State State { get { return _state; } set { _state = value; } }
        public DateTime? CreatedAt { get; set; }
        [JsonIgnore]
        public DateTime? UpdatedAt { get; set; }
        public string AuditReference { get; set; }

        public AuditTrail AuditTrail { get; set; }
        public int? AuditTrailID { get; set; }
        public Enums.EntityType EntityType { get; set; }
        public int? EntityID { get; set; }
        public string? Property { get; set; }
        public string? PreviousValue { get; set; }
        public string? NewValue { get; set; }
        public Enums.AuditTrailAction Action { get; set; }
    }
}