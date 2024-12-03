using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.TradeTax;

namespace CAT20.Core.Models.OnlienePayment
{
    public class BookingSubProperty
    {
        public BookingSubProperty()
        {
            chargingSchemes = new HashSet<ChargingScheme>();
        }
        public int? ID { get; set; }
        public string SubPropertyName { get; set; }
        public string? Code { get; set; }
        public int Status { get; set; }
        public int? SabhaID { get; set; }
        public int PropertyID { get; set; }
        public string Address { get; set; }
        public int TelephoneNumber  { get; set; }
        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? CreatedAt { get; set; } // Change data type to DateTime
        public DateTime? UpdatedAt { get; set; }
        public virtual BookingProperty? bookingProperty { get; set; }
        public virtual ICollection<ChargingScheme> chargingSchemes { get; set; }
        public virtual ICollection<BookingTimeSlot> BookingTimeSlots { get; set; }
    }
}
