using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.OnlienePayment;

namespace CAT20.Core.Services.OnlinePayment
{
    public interface IBookingPropertyService
    {
        Task<IEnumerable<BookingProperty>> GetAllBookingPropertyBySabhaIdAsync(int SabahId);
        Task<BookingProperty> GetBookingPropertyByIdAsync(int id);
        Task<(bool, string?)> CreateBookingProperty(BookingProperty newBookingProperty, HTokenClaim token);
        Task<(bool, string?)> DeleteBookingProperty(BookingProperty bookingProperty);
    }
}
