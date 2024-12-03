using System;
using System.Collections.Generic;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Interfaces;
using CAT20.Core.Models.Vote;
using Newtonsoft.Json;

namespace CAT20.Core.Models.AuditTrails
{
    public partial class AuditTrail : EntityBase
    {
        public Enums.EntityType EntityType { get; set; }
        public int? EntityID { get; set; }
        
        //public int? UserID { get; set; }
        public DateTime? Date { get; set; }
        public Enums.AuditTrailAction Action { get; set; }

        public IList<AuditTrailDetail> DetailList { get; set; }

    }
}