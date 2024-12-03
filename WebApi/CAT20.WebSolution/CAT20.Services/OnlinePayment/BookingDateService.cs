using CAT20.Core;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.OnlienePayment;
using CAT20.Core.Services.OnlinePayment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.OnlinePayment
{
    public class BookingDateService : IBookingDateService
    {
        private readonly IOnlinePaymentUnitOfWork _unitOfWork;

        public BookingDateService(IOnlinePaymentUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<(bool, string?)> CreateBookingDate(BookingDate newBookingDate)
        {
            try
            {
                _unitOfWork.BookingDate.AddAsync(newBookingDate);
                await _unitOfWork.CommitAsync();
                return (true, "Charging Scheme Updated Successfully");
            }
            catch (Exception ex)
            {
                return (false, "Charging Scheme Updated Unsuccessfull");
                throw ex;
            }
        }

        public Task<(bool, string?)> DeleteBookingDate(BookingDate bookingDate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BookingDate>> GetAllBookingDateBySabhaIdAsync(int SabahId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BookingDate>> GetAllBookingDatesForSpecificSpecificMainPropRelatedSubProp(int mainPropId, int subPropId)
        {
            return await _unitOfWork.BookingDate.GetAllBookingDatesForSpecificMainPropRelatedSubProp(mainPropId, subPropId);
        }

        public async Task<IEnumerable<BookingDate>> GetBookingDateByBookingIdAsync(int id)
        {

            return await _unitOfWork.BookingDate.GetBookingDateByBookingIdAsync(id);

        }
    }
}
