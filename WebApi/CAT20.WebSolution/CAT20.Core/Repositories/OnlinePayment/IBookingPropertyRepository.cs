using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.OnlienePayment;

namespace CAT20.Core.Repositories.OnlinePayment
{
    public interface IBookingPropertyRepository :IRepository<BookingProperty>
    {
        Task<IEnumerable<BookingProperty>> GetAllBookingPropertyBySabhaIdAsync(int SabahId);
        Task<BookingProperty> GetBookingPropertyByIdAsync(int id);
    }
}
