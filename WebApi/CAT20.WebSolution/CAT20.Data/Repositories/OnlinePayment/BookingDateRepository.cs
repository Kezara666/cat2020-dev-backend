using CAT20.Core.Models.OnlienePayment;
using CAT20.Core.Repositories.OnlinePayment;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.OnlinePayment
{
    public class BookingDateRepository:Repository<BookingDate> , IBookingDateRepository
    {
        public BookingDateRepository(DbContext context) : base(context) { }

        private OnlinePaymentDbContext onlinePaymentDbContext
        {
            get { return Context as OnlinePaymentDbContext; }
        }

        public async Task<IEnumerable<BookingDate>> GetAllBookingDatesForSpecificMainPropRelatedSubProp(int mainPropId, int subPropId)
        {
            var obj = await onlinePaymentDbContext.BookingDates
                .Where(m => m.SubPropertyId == subPropId && m.PropertyId == mainPropId)
                .ToListAsync();

            return obj;
        }

        public async Task<IEnumerable<BookingDate>> GetBookingDateByBookingIdAsync(int id)
        {
            

            return await onlinePaymentDbContext.BookingDates
                .Where(m => m.OnlineBookingId ==id)
                .ToListAsync();
        }




    }
}
