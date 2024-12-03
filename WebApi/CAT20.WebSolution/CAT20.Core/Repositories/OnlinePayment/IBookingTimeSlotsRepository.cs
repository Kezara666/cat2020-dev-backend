using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.OnlienePayment;

namespace CAT20.Core.Repositories.OnlinePayment
{
    public interface IBookingTimeSlotsRepository : IRepository<BookingTimeSlot>
    {
        Task<IEnumerable<BookingTimeSlot>> GetAllBookingTimeSlotsForSabha(int sabhaId);
        Task<IEnumerable<BookingTimeSlot>> GetBookingTimeSlotById(int id);
        Task<IEnumerable<BookingTimeSlot>> GetAllBookingTimeSlotsForSubPropertyId(int subPropertyId);


    }
}
