using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.OnlienePayment;

namespace CAT20.Core.Repositories.OnlinePayment
{
    public interface IBookingSubPropertyRepository:IRepository<BookingSubProperty>
    {
        Task<IEnumerable<BookingSubProperty>> GetAllBookingSubPropertyBySabhaIdAsync(int SabahId);
        Task<BookingSubProperty> GetBookingSubPropertyByIdAsync(int id);
        Task<IEnumerable<BookingSubProperty>> GetAllBookingSubPropertyByPropertyId(int propertyId);
    }
}
