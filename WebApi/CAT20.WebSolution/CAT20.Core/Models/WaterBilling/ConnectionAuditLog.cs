using CAT20.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.WaterBilling
{
    public class ConnectionAuditLog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int? WaterConnectionId { get; set; }
        public virtual WaterConnection? WaterConnection { get; set; }
        public WbAuditLogAction Action { get; set; }
        public DateTime Timestamp { get; set; }
        public int? ActionBy { get; set; }
        public WbEntityType EntityType { get; set; }


        public int? PartnerId { get; set; }
        public int? BillingId { get; set; }
        public int ActiveStatus { get; set; }
        public int ActiveNatureId { get; set; }

        public int? SubRoadId { get; set; } 
        public int? OfficeId { get; set; } 

    }


    //public enum AuditLogAction
    //{
    //    Create,
    //    Update,
    //    Delete,
    //    Request,
    //    Reject,
    //    Approve,
    //}

    //public enum EntityType
    //{
    //    WaterConnection,
    //    NatureLog,
    //    SatusLog,

    //}
}
