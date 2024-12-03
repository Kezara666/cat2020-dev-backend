using CAT20.Core.Models.OnlienePayment;

namespace CAT20.WebApi.Resources.OnlinePayment
{
    public class BookingSubPropertyResource
    {
        public int? ID { get; set; }
        public string SubPropertyName { get; set; }

        public string? Code { get; set; }

      //  public int PropertyId { get; set; }
        public int Status { get; set; }
        public int? SabhaID { get; set; }
        public int PropertyID { get; set; }
        public string Address { get; set; }
        public int TelephoneNumber { get; set; }
        public string Latitude { get; set; }

        public string Longitude { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? CreatedAt { get; set; } // Change data type to DateTime
        public DateTime? UpdatedAt { get; set; }
        public virtual ICollection<BookingTimeSlotResource> BookingTimeSlots { get; set; }

        //  public virtual BookingProperty bookingProperty { get; set; }

    }
}
