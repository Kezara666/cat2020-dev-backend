using CAT20.Core.Models.OnlienePayment;

namespace CAT20.WebApi.Resources.OnlinePayment
{
    public class BookingPropertyResource
    {
        public int? ID { get; set; }
        public string PropertyName { get; set; }
        public string? Code { get; set; }
        public int? Status { get; set; }
        public int? SabhaID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual ICollection<BookingSubProperty> bookingSubProperties { get; set; }

    }
}
