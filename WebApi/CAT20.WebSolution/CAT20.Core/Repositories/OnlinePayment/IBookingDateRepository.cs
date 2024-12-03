using CAT20.Core.Models.OnlienePayment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.OnlinePayment
{
    public interface IBookingDateRepository : IRepository<BookingDate>
    {
        public Task<IEnumerable<BookingDate>> GetAllBookingDatesForSpecificMainPropRelatedSubProp(int mainPropId, int subPropId);
        public Task<IEnumerable<BookingDate>> GetBookingDateByBookingIdAsync(int id);
    }
}
