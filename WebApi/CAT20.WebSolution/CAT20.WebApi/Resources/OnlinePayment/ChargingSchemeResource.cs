using CAT20.Core.Models.Enums;
using CAT20.Core.Models.OnlienePayment;
using Microsoft.EntityFrameworkCore;

namespace CAT20.WebApi.Resources.OnlinePayment
{
    public class ChargingSchemeResource
    {
        public int? ID { get; set; }
        public int SubPropertyId { get; set; }
        public BookingPropertyChargingType ChargingType { get; set; }

        [Precision(18, 2)]
        public decimal Amount { get; set; }
        public int? Status { get; set; }
        public int? SabhaID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual BookingSubProperty BookingSubProperty { get; set; }
    }
}
