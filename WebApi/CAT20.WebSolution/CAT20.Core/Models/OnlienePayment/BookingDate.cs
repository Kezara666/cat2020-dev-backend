using CAT20.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.OnlienePayment
{
    public class BookingDate
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        public int? PropertyId { get; set; }
        [ForeignKey(nameof(PropertyId))]
        public virtual BookingProperty ? Property { get; set; } // Foreign key relationship with BookingProperty

        [Required]
        public int SubPropertyId { get; set; }
        [ForeignKey(nameof(SubPropertyId))]
        public virtual BookingSubProperty? SubProperty { get; set; } // Foreign key relationship with BookingSubProperty

        [Required]
        public string[] BookingTimeSlotIds { get; set; } // Assuming JSON or another handling for array-based relationship

        public OnlineBookingStatus BookingStatus { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public int OnlineBookingId { get; set; }
        [ForeignKey(nameof(OnlineBookingId))]
        public virtual OnlineBooking? OnlineBooking { get; set; } // Foreign key relationship with OnlineBooking
    }

}
