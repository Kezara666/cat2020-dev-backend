using CAT20.Core.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace CAT20.WebApi.Resources.OnlinePayment
{
    public class SaveChargingSchemeResource
    {
        public int? ID { get; set; }
        public int SubPropertyId { get; set; }
        public BookingPropertyChargingType ChargingType { get; set; }

        [Precision(18, 2)]
        public decimal Amount { get; set; }
    }
}
