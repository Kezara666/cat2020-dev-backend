
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.OnlienePayment;
using Microsoft.EntityFrameworkCore;

namespace CAT20.WebApi.Resources.OnlinePayment
{
    public class SaveOnlineBookingResource
    {
        public int? Id { get; set; }
        public int PropertyId { get; set; }
        public int SubPropertyId { get; set; }
        public int CustomerId { get; set; }
        ////public string BookingStartDate { get; set; }
        ////public string BookingEndDate { get; set; }
        public string CreationDate { get; set; }
        public int[] BookingTimeSlotIds { get; set; }
        public int SabhaId { get; set; }
        public string BookingNotes { get; set; }
        public OnlineBookingStatus BookingStatus { get; set; }
        [Precision(18, 2)]
        public decimal TotalAmount { get; set; }
        public BookingPaymentStatus PaymentStatus { get; set; }
        public int TransactionId { get; set; }
        public int ApprovedBy { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public string RejectionReason { get; set; }
        public string CancellatioReason { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTimeSlot[] DateTimeSlot { get; set; } 

    }
}
