using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.OnlienePayment;

namespace CAT20.Core.Repositories.OnlinePayment
{
    public interface IOnlineBookingRepository : IRepository<OnlineBooking>

    {
        Task<IEnumerable<OnlineBooking>> GetAllOnlineBookingsForSabha(int sabhaId);
        Task<IEnumerable<OnlineBooking>> GetAllOnlineBookingsForSubPropertyId(int subPropertyId);
        Task<OnlineBooking> GetBookingById(int Id);
    }
}
