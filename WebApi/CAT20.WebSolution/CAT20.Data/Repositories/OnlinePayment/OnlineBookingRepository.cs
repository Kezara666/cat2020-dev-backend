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
    public class OnlineBookingRepository : Repository<OnlineBooking> , IOnlineBookingRepository
    {
        public OnlineBookingRepository(DbContext context) : base(context) { }
        private OnlinePaymentDbContext onlinePaymentDbContext
        {
            get { return Context as OnlinePaymentDbContext; }
        }

        public async Task<IEnumerable<OnlineBooking>> GetAllOnlineBookingsForSabha(int sabhaId)
        {
            return await onlinePaymentDbContext.OnlineBookings
               
                .Where(b => b.SabhaId == sabhaId)
                .Include(b => b.Property)
                .Include(b => b.SubProperty)
                .ToListAsync();
        }

        public async Task<IEnumerable<OnlineBooking>> GetAllOnlineBookingsForSubPropertyId(int subPropertyId)
        {
            return await onlinePaymentDbContext.OnlineBookings.Where(o=>o.SubPropertyId == subPropertyId).ToListAsync();
        }

        public async Task<OnlineBooking> GetBookingById(int id)
        {
            try
            {
                var booking = await onlinePaymentDbContext.OnlineBookings
                    .Include(b => b.Property)       // Include the BookingProperty
                    .Include(b => b.SubProperty)    // Include the BookingSubProperty
                    .FirstOrDefaultAsync(b => b.Id == id); // Use FirstOrDefaultAsync

                return booking; // Returns null if not found
            }
            catch (Exception ex)
            {
                // Log the exception here if needed
                // For example: _logger.LogError(ex, "Error fetching booking with ID {BookingId}", id);
                throw ex; // Rethrow the exception if necessary or return null
            }
        }




    }
}
