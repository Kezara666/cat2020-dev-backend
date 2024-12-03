using CAT20.Core.Models.Enums;
using CAT20.Core.Models.OnlienePayment;

namespace CAT20.WebApi.Resources.OnlinePayment
{
    public class BookingTimeSlotResource
    {
        public int? Id { get; set; }
        public int SubPropertyId { get; set; }
        public string Description { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public BookingTimeSlotStatus BookingTimeSlotStatus { get; set; }
        public int? OrderLevel { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual BookingSubProperty bookingSubProperty { get; set; }
    }
}
