using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.OnlienePayment;
using CAT20.Core.Repositories.OnlinePayment;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Data.Repositories.OnlinePayment
{
    public class BookingTimeSlotsRepository : Repository<BookingTimeSlot> , IBookingTimeSlotsRepository
    {
        public BookingTimeSlotsRepository(DbContext context) : base(context) { }
        private OnlinePaymentDbContext onlinePaymentDbContext
        {
            get { return Context as OnlinePaymentDbContext; }
        }
        public async Task<IEnumerable<BookingTimeSlot>> GetAllBookingTimeSlotsForSabha(int sabhaId)
        {
            return await onlinePaymentDbContext.BookingTimeSlots
                .Include(x => x.bookingSubProperty)
                .Where(b=>b.SabhaId == sabhaId && b.BookingTimeSlotStatus == 0)
                .ToListAsync();
        }

        public async Task<IEnumerable<BookingTimeSlot>> GetAllBookingTimeSlotsForSubPropertyId(int subPropertyId)
        {
            return await onlinePaymentDbContext.BookingTimeSlots
                  .Include(x => x.bookingSubProperty)
                .Where(b => b.SubPropertyId == subPropertyId && b.BookingTimeSlotStatus == 0).ToListAsync();
        }

        public async Task<IEnumerable<BookingTimeSlot>> GetBookingTimeSlotById(int id)
        {
            return await onlinePaymentDbContext.BookingTimeSlots
                   .Include(x => x.bookingSubProperty)
                 .Where(b => b.Id == id).ToListAsync();
        }


    }
  
}
