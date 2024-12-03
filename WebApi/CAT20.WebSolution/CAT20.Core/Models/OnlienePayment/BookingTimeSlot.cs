using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Enums;

namespace CAT20.Core.Models.OnlienePayment
{
    public class BookingTimeSlot
    {
        public int? Id { get; set; }
        public int SubPropertyId { get; set; }
        public string Description { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public BookingTimeSlotStatus BookingTimeSlotStatus { get; set; }
        public int OrderLevel { get; set; }
        public int? SabhaId { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? CreatedAt { get; set; } 
        public DateTime? UpdatedAt { get; set; }
        public virtual BookingSubProperty bookingSubProperty {get; set; }
    }
}
