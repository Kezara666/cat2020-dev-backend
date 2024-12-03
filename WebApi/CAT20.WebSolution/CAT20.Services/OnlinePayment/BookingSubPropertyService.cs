using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.OnlienePayment;
using CAT20.Core.Repositories.OnlinePayment;
using CAT20.Core.Services.OnlinePayment;
using CAT20.Services.Mixin;
using Microsoft.Extensions.Logging;

namespace CAT20.Services.OnlinePayment
{
    public class BookingSubPropertyService : IBookingSubPropertyService
    {
        private readonly IOnlinePaymentUnitOfWork _unitOfWork;
        private readonly ILogger<MixinOrderService> _logger;

        public BookingSubPropertyService(ILogger<MixinOrderService> logger, IOnlinePaymentUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<(bool, string?)> CreateBookingSubProperty(BookingSubProperty newBookingSubProperty, HTokenClaim token)
        {
            try
            {
                if (newBookingSubProperty.ID != null)
                {
                    var bookingSubProperty = await _unitOfWork.BookingSubProperty.GetByIdAsync(newBookingSubProperty.ID.Value);
                    if (bookingSubProperty != null)
                    {
                        bookingSubProperty.SubPropertyName =newBookingSubProperty.SubPropertyName;
                        bookingSubProperty.Code = newBookingSubProperty.Code;
                        bookingSubProperty.PropertyID = newBookingSubProperty.PropertyID;
                        bookingSubProperty.Status = 1;
                        bookingSubProperty.Address = newBookingSubProperty.Address;
                        bookingSubProperty.TelephoneNumber = newBookingSubProperty.TelephoneNumber;
                        bookingSubProperty.Latitude = newBookingSubProperty.Latitude;
                        bookingSubProperty.Longitude = newBookingSubProperty.Longitude;

                      //  await _unitOfWork.BookingSubProperty.AddAsync(newBookingSubProperty);
                        await _unitOfWork.CommitAsync();
                        return (true, "Sucessfully Updated !");
                    }
                    else
                    {
                        return (false, "Booking Sub Property not found.");
                    }
                }
                else
                {
                    newBookingSubProperty.ID = null;
                    newBookingSubProperty.Status = 1;
                    newBookingSubProperty.CreatedBy = token.userId;
                    newBookingSubProperty.SabhaID = token.sabhaId;
                    await _unitOfWork.BookingSubProperty.AddAsync(newBookingSubProperty);
                    await _unitOfWork.CommitAsync();
                   return (true, "Sucessfully Saved !");
                }
            }
            catch (Exception ex)
            {
                return (false, "error occoutred " + ex.Message);
            }
        }

        public async Task<IEnumerable<BookingSubProperty>> GetAllBookingSubPropertyBySabhaIdAsync(int SabahId)
        {
            try
            {
                return await _unitOfWork.BookingSubProperty.GetAllBookingSubPropertyBySabhaIdAsync(SabahId);
            }
            catch (Exception ex)
                {
                    return new List<BookingSubProperty>();
                }
         }

        public async Task<IEnumerable<BookingSubProperty>> GetAllBookingSubPropertyByPropertiIdAsync(int propertyId)
        {
            try
            {
                return await _unitOfWork.BookingSubProperty.GetAllBookingSubPropertyByPropertyId(propertyId);
            }
            catch (Exception ex)
            {
                return new List<BookingSubProperty>();
            }
        }

        public async Task<BookingSubProperty> GetBookingSubPropertyByIdAsync(int id)
        {
            return await _unitOfWork.BookingSubProperty.GetBookingSubPropertyByIdAsync(id);
        }

        public async Task<(bool,string?)> DeleteBookingSubProperty(BookingSubProperty bookingSubProperty)
        {
            try
            {
                _unitOfWork.BookingSubProperty.Remove(bookingSubProperty);
                await _unitOfWork.CommitAsync();

                return (true, "Deleted Successfully");
            }
            catch (Exception ex) {
                return (false, "Error Occoured"+ex.Message);
            }
        }
    }
}
