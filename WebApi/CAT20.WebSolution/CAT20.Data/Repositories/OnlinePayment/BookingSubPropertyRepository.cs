using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.OnlienePayment;
using CAT20.Core.Repositories.OnlinePayment;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Data.Repositories.OnlinePayment
{
    public class BookingSubPropertyRepository :Repository<BookingSubProperty>,IBookingSubPropertyRepository
    {
        public BookingSubPropertyRepository(DbContext context) : base(context)
        {
        }
        private OnlinePaymentDbContext onlinePaymentDbContext
        {
            get { return Context as OnlinePaymentDbContext; }
        }
        public async Task<IEnumerable<BookingSubProperty>> GetAllBookingSubPropertyBySabhaIdAsync(int sabhaId)
        {
            return await onlinePaymentDbContext.BookingSubProperty
        .Include(b => b.BookingTimeSlots)
        .Where(m => m.SabhaID == sabhaId && m.Status == 1)
        .Select(b => new BookingSubProperty
        {
            ID = b.ID,
            SubPropertyName = b.SubPropertyName,
            Code = b.Code,
            Status = b.Status,
            SabhaID = b.SabhaID,
            PropertyID = b.PropertyID,
            Address = b.Address,
            TelephoneNumber = b.TelephoneNumber,
            Latitude = b.Latitude,
            Longitude = b.Longitude,
            CreatedBy = b.CreatedBy,
            UpdatedBy = b.UpdatedBy,
            CreatedAt = b.CreatedAt,
            UpdatedAt = b.UpdatedAt,
            BookingTimeSlots = b.BookingTimeSlots
                .OrderBy(t => t.OrderLevel)
                .ToList()
        })
        .ToListAsync();
        }

        public async Task<IEnumerable<BookingSubProperty>> GetAllBookingSubPropertyByPropertyId(int propertyId)
        {
            return await onlinePaymentDbContext.BookingSubProperty

                .Where(m => m.PropertyID == propertyId && m.Status == 1)
                .ToListAsync();
        }
        public async Task<BookingSubProperty> GetBookingSubPropertyByIdAsync(int id)
        {
            return await onlinePaymentDbContext.BookingSubProperty.Where(m => m.ID == id && m.Status == 1).FirstOrDefaultAsync();

        }
    }
}
