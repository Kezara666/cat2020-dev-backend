using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.TradeTax;

namespace CAT20.Core.Models.OnlienePayment
{
    public  class BookingProperty
    {
        public BookingProperty() {
            bookingSubProperties = new HashSet<BookingSubProperty>();
        }
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
