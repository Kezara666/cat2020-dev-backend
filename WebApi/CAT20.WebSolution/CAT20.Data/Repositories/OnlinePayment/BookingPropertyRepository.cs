using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.OnlienePayment;
using CAT20.Core.Models.TradeTax;
using CAT20.Core.Repositories.OnlinePayment;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Data.Repositories.OnlinePayment
{
    public class BookingPropertyRepository : Repository<BookingProperty> ,IBookingPropertyRepository
    {
        public BookingPropertyRepository(DbContext context) : base(context)
        {
        }
        private OnlinePaymentDbContext onlinePaymentDbContext
        {
            get { return Context as OnlinePaymentDbContext; }
        }
        public async Task<IEnumerable<BookingProperty>> GetAllBookingPropertyBySabhaIdAsync(int SabahId)
        {
            return await onlinePaymentDbContext.BookingProperty
                .Include(m=> m.bookingSubProperties)
                .Where(m => m.SabhaID == SabahId && m.Status == 1)
                .ToListAsync();
        }
        public async Task<BookingProperty> GetBookingPropertyByIdAsync(int id)
        {
            return await onlinePaymentDbContext.BookingProperty.Where(m => m.ID == id && m.Status == 1).FirstOrDefaultAsync();
                
        }
    }
}
