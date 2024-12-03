using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.OnlienePayment
{
    public class DateTimeSlot
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int[] BookingTimeSlotIds { get; set; }
    }
}
