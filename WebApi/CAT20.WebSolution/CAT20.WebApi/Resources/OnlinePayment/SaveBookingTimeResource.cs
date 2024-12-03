using CAT20.Core.Models.Enums;

namespace CAT20.WebApi.Resources.OnlinePayment
{
    public class SaveBookingTimeResource
    {
        public int? Id { get; set; }
        public int SubPropertyId { get; set; }
        public string Description { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    
        public int? OrderLevel { get; set; }
        public BookingTimeSlotStatus BookingTimeSlotStatus { get; set; }
    }
}
