using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.OnlienePayment;
using CAT20.Core.Repositories.OnlinePayment;
using CAT20.Core.Services.OnlinePayment;
using CAT20.Data;

namespace CAT20.Services.OnlinePayment
{
    public class BookingTimeSlotsService : IBookingTimeSlotService
    {
        private readonly IOnlinePaymentUnitOfWork _unitOfWork;

        public BookingTimeSlotsService(IOnlinePaymentUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<BookingTimeSlot>> GetAllBookingTimeSlotsForSabha(int sabhaId)
        {
                return await _unitOfWork.BookingTimeSlots.GetAllBookingTimeSlotsForSabha(sabhaId);
        }

        public async Task<IEnumerable<BookingTimeSlot>> GetAllBookingTimeSlotsForSubPropertyId(int subpropertyId)
        {
            return await _unitOfWork.BookingTimeSlots.GetAllBookingTimeSlotsForSubPropertyId(subpropertyId);
        }
        public async Task<IEnumerable<BookingTimeSlot>> GetBookingTimeSlotById(int id) { 
                return await _unitOfWork.BookingTimeSlots.GetBookingTimeSlotById(id);
        }
        public async Task<(bool, string?)> saveBookingTimeSlot(BookingTimeSlot newBookingTimeSlot , HTokenClaim token) {

            try
            {
                if (newBookingTimeSlot.Id != null) { 
                       var BookingTimeSlot = await _unitOfWork.BookingTimeSlots.GetByIdAsync(newBookingTimeSlot.Id);
                       if (BookingTimeSlot != null) {
                            BookingTimeSlot.Id = newBookingTimeSlot.Id;
                            BookingTimeSlot.SubPropertyId = newBookingTimeSlot.SubPropertyId;
                            BookingTimeSlot.Description = newBookingTimeSlot.Description;
                            BookingTimeSlot.From = newBookingTimeSlot.From;
                            BookingTimeSlot.To = newBookingTimeSlot.To;
                            BookingTimeSlot.OrderLevel = newBookingTimeSlot.OrderLevel;
                            BookingTimeSlot.UpdatedBy = token.userId;
                            newBookingTimeSlot.BookingTimeSlotStatus = BookingTimeSlotStatus.Available;

                        await _unitOfWork.CommitAsync();
                        return (true, "Time Slot Updated Successfully");
                        }
                        else
                        {
                        return (false, "No Time Slot Found");
                        } 
                }
                newBookingTimeSlot.Id = null;
         
                newBookingTimeSlot.UpdatedBy= token.userId;
                newBookingTimeSlot.SabhaId = token.sabhaId;
                newBookingTimeSlot.BookingTimeSlotStatus = BookingTimeSlotStatus.Available;
                await _unitOfWork.BookingTimeSlots.AddAsync(newBookingTimeSlot);
                await _unitOfWork.CommitAsync();
                return (true, "Time slot saved successfully");
            }
            catch (Exception ex) {
                return (false, "Error Occured" + ex.Message);
            }
        }

        public async Task<(bool,string?)> DeleteBookingTimeSlot(BookingTimeSlot bookingTimeSlot)
        {
            try
            {
                var TimeSlot = await _unitOfWork.BookingTimeSlots.GetByIdAsync(bookingTimeSlot.Id);
                if (TimeSlot != null)
                {
                    _unitOfWork.BookingTimeSlots.Remove(bookingTimeSlot);
                    await _unitOfWork.CommitAsync();
                    return (true, "Time Slot Deleted Successfully");
                }
                else {
                    return (false, "No Time Slot Found");
                }
            }
            catch (Exception ex)
            {
                return (false, "Error Ocured " + ex.Message);
            }
        }

        public async Task<IEnumerable<BookingTimeSlot>> GetBookingTimeSlotBySpecificId(int id)
        {
            return await _unitOfWork.BookingTimeSlots.GetAllAsync();
        }

        async Task<BookingTimeSlot> IBookingTimeSlotService.GetBookingTimeSlotById(int id)
        {
            var bookingSlot = await _unitOfWork.BookingTimeSlots.GetByIdAsync(id);
            return bookingSlot;
        }
    }
}
