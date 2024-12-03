using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Core.Models.OnlienePayment
{
    public class OnlineBooking
    {
        [Key]
        public int? Id { get; set; }
        [Required]
        public int? PropertyId { get; set; }
        public BookingProperty? Property { get; set; }

        public int SubPropertyId { get; set; }
        public BookingSubProperty? SubProperty { get; set; }
        public int CustomerId { get; set; }
       
        //public string BookingStartDate { get; set; }
        //public string BookingEndDate { get; set; }

        public string CreationDate{ get; set; }

        public string[] BookingTimeSlotIds { get; set; }
        public OnlineBookingStatus BookingStatus { get; set; }

        [Precision(18,2)]
        public decimal TotalAmount { get; set; }
        public BookingPaymentStatus PaymentStatus { get; set; }
        public int TransactionId { get; set; }
        public int ApprovedBy { get; set; }

        public int SabhaId { get; set; }
        public DateTime ApprovedAt { get; set; }
        public string RejectionReason {  get; set; }
        public string CancellatioReason { get; set; }
        public string BookingNotes { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        



    }
}
