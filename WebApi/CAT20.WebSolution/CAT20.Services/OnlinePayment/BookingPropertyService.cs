using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.OnlienePayment;

using CAT20.Core.Services.OnlinePayment;

using Microsoft.Extensions.Logging;

namespace CAT20.Services.OnlinePayment
{
    public class BookingPropertyService :IBookingPropertyService
    {
        private readonly IOnlinePaymentUnitOfWork _unitOfWork;
        public BookingPropertyService( IOnlinePaymentUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

      public async   Task<IEnumerable<BookingProperty>> GetAllBookingPropertyBySabhaIdAsync(int SabahId)
        {
            return await _unitOfWork.BookingProperty.GetAllBookingPropertyBySabhaIdAsync(SabahId);
        }
       public async Task<BookingProperty> GetBookingPropertyByIdAsync(int id)
        {
            return await _unitOfWork.BookingProperty.GetBookingPropertyByIdAsync(id);
        }

        public async Task<(bool,string?)> CreateBookingProperty(BookingProperty newBookingProperty , HTokenClaim token)
        {
            try
            {
                if (newBookingProperty.ID != null)
                {
                    var bookingProperty = await _unitOfWork.BookingProperty.GetByIdAsync(newBookingProperty.ID);
                    if (bookingProperty != null)
                    {
                        bookingProperty.PropertyName = newBookingProperty.PropertyName;
                        bookingProperty.Code = newBookingProperty.Code;
                    //  await _unitOfWork.BookingProperty.AddAsync(newBookingProperty);
                        await _unitOfWork.CommitAsync();
                        return (true, "Sucessfully Updated !");
                    }
                }
                else
                {
                    newBookingProperty.ID = null;
                    newBookingProperty.Status = 1;
                    newBookingProperty.CreatedBy = token.userId;
                    newBookingProperty.SabhaID = token.sabhaId;
                    await _unitOfWork.BookingProperty.AddAsync(newBookingProperty);
                    await _unitOfWork.CommitAsync();
                }
             

                return (true,"Sucessfully Saved !");
            }
            catch (Exception ex)
            {
                return (false,"error occoured "+ex.Message);
            }
        }
        public async Task<(bool,string?)> DeleteBookingProperty(BookingProperty bookingProperty)
        {
            try
            {
                _unitOfWork.BookingProperty.Remove(bookingProperty);
                await _unitOfWork.CommitAsync();
                return (true, "Deleted Successfully");
            }
            catch (Exception ex) {
                return (false, "error Ocoured"+ex.Message);
            }
        }
    }
}
