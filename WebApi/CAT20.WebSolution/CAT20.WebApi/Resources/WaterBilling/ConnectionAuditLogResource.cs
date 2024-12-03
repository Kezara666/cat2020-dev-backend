using CAT20.Core.Models.WaterBilling;
using CAT20.WebApi.Resources.User;

namespace CAT20.WebApi.Resources.WaterBilling
{
    public class ConnectionAuditLogResource
    {
        public int Id { get; set; }
        public int? WaterConnectionId { get; set; }
        public virtual WaterConnection? WaterConnection { get; set; }
        public string Action { get; set; }
        public DateTime Timestamp { get; set; }
        public int? ActionBy { get; set; }
        public UserActionByResources? UserActionBy { get; set; }
        public string EntityType { get; set; }
    }
}
