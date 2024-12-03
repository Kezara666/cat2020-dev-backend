using CAT20.Core.HelperModels;
using CAT20.Core.Models.OnlienePayment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.OnlinePayment
{
    public interface IBookingDateService
    {
        Task<IEnumerable<BookingDate>> GetAllBookingDateBySabhaIdAsync(int SabahId);

        Task<IEnumerable<BookingDate>> GetBookingDateByBookingIdAsync(int id);
        Task<(bool, string?)> CreateBookingDate(BookingDate newBookingDate);
        Task<(bool, string?)> DeleteBookingDate(BookingDate bookingDate);

        public Task<IEnumerable<BookingDate>> GetAllBookingDatesForSpecificSpecificMainPropRelatedSubProp(int mainPropId, int subPropId);

    }
}
