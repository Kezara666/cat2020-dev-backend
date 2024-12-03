using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.OnlienePayment;

namespace CAT20.Core.Services.OnlinePayment
{
    public interface IBookingTimeSlotService 
    {
        Task<IEnumerable<BookingTimeSlot>> GetAllBookingTimeSlotsForSabha(int sabhaId);
        Task<IEnumerable<BookingTimeSlot>> GetBookingTimeSlotBySpecificId(int id);
        Task<(bool, string?)> saveBookingTimeSlot(BookingTimeSlot newBookingTimeSlot, HTokenClaim token);
        Task<(bool, string?)> DeleteBookingTimeSlot(BookingTimeSlot bookingTimeSlot);
        Task<IEnumerable<BookingTimeSlot>> GetAllBookingTimeSlotsForSubPropertyId(int subpropertyId);
        Task <BookingTimeSlot> GetBookingTimeSlotById(int id) ;
    }
}
