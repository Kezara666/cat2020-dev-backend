using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.OnlienePayment;

namespace CAT20.Core.Services.OnlinePayment
{
    public interface IBookingSubPropertyService
    {
        Task<(bool, string?)> CreateBookingSubProperty(BookingSubProperty newBookingSubProperty, HTokenClaim token);
        Task<(bool, string?)> DeleteBookingSubProperty(BookingSubProperty bookingSubProperty);
        Task<BookingSubProperty> GetBookingSubPropertyByIdAsync(int id);
        Task<IEnumerable<BookingSubProperty>> GetAllBookingSubPropertyBySabhaIdAsync(int SabahId);
        Task<IEnumerable<BookingSubProperty>> GetAllBookingSubPropertyByPropertiIdAsync(int propertyId);

    }
}
