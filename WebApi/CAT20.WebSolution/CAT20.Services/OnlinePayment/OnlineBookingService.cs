using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core;
using CAT20.Core.DTO;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.OnlienePayment;
using CAT20.Core.Repositories.OnlinePayment;
using CAT20.Core.Services.OnlinePayment;
using DocumentFormat.OpenXml.Office2010.Excel;
using SixLabors.ImageSharp.Processing;

namespace CAT20.Services.OnlinePayment
{
    public class OnlineBookingService : IOnlineBookingService
    {
        private readonly IOnlinePaymentUnitOfWork _unitOfWork;

        public OnlineBookingService(IOnlinePaymentUnitOfWork unitOfWork)
        {

        _unitOfWork = unitOfWork; 
        }


        public async Task<(bool, string?, OnlineBooking?)> CreateOnlineBooking(OnlineBooking newBooking, BookingDate[] bookingDate)
        {
            try
            {
                if (newBooking.Id != null)
                {
                    var onlineBooking = await _unitOfWork.OnlineBooking.GetByIdAsync(newBooking.Id.Value);
                    if (onlineBooking != null)
                    {
                        onlineBooking.Id  = newBooking.Id;
                        onlineBooking.SubPropertyId = newBooking.SubPropertyId;
                        onlineBooking.CreationDate = newBooking.CreationDate;
                        onlineBooking.BookingTimeSlotIds = newBooking.BookingTimeSlotIds;
                        onlineBooking.BookingStatus = OnlineBookingStatus.pending;
                        onlineBooking.SabhaId = newBooking.SabhaId;
                        onlineBooking.CustomerId = newBooking.CustomerId;
                      //  onlineBooking.UpdatedAt = DateTime.Now;



                        await _unitOfWork.CommitAsync();
                        return (true, "Sucessfully Updated !", onlineBooking);
                    }
                    else
                    {
                        return (false, "Booking Sub Property not found.", onlineBooking);
                    }
                }
                else
                {
                  newBooking.Id = null;
                    // newBooking.CreatedAt = DateTime.Now;


                    try
                    {
                        bool hasOverlapBooking = false;
                        var bookingDates = await _unitOfWork.BookingDate.GetAllAsync();
                        foreach (var item in bookingDate)
                        {
                            // Check if there exists any other booking date with the same StartDate, EndDate, and overlapping BookingTimeSlotIds
                            bool hasOverlap = bookingDates.Any(other =>
                                
                                other.StartDate == item.StartDate &&
                                other.EndDate == item.EndDate &&
                                other.BookingTimeSlotIds.Any(id => item.BookingTimeSlotIds.Contains(id)) // Check for overlapping time slots
                            );

                            if (hasOverlap)
                            {
                                // Handle the case where an overlapping booking date is found
                                throw new Exception();
                            }
                            
                        }

                        await _unitOfWork.OnlineBooking.AddAsync(newBooking);

                        await _unitOfWork.CommitAsync();

                        foreach (BookingDate item in bookingDate)
                        {
                            try
                            {
                                var newBookingDate = new BookingDate
                                {
                                    PropertyId = newBooking.PropertyId, // Replace with actual property ID
                                    SubPropertyId = newBooking.SubPropertyId, // Replace with actual sub-property ID
                                    BookingTimeSlotIds = item.BookingTimeSlotIds, // Replace with actual time slot IDs
                                    BookingStatus = OnlineBookingStatus.pending, // Use appropriate status enum
                                    StartDate = item.StartDate, // Replace with actual start date in string format
                                    EndDate = item.EndDate, // Replace with actual end date in string format
                                    CreatedBy = 1, // Replace with actual user ID
                                    CreatedAt = DateTime.UtcNow,
                                    OnlineBookingId = newBooking.Id ?? 0 // Replace with actual booking ID
                                };

                                await _unitOfWork.BookingDate.AddAsync(newBookingDate);
                                await _unitOfWork.CommitAsync();
                            }
                            catch (Exception ex)
                            {

                                throw ex;
                            }
                        }



                        return (true, "Successfully Saved!", newBooking);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }

                    
                }
            }
            catch (Exception ex)
            {
                return (false, "error occoured " + ex.Message, new OnlineBooking());
            }
        }



        public async Task<IEnumerable<BookingProperty>> GetAllBookingProperty(int sabhaId)
        {
            try
            {
                var properties = await _unitOfWork.BookingProperty.GetAllAsync();
                return properties.Where(item => item.SabhaID == sabhaId);
            }
            catch (Exception ex)
            {
                // Log exception here if needed
                return null;
            }
        }

        public async Task<IEnumerable<OnlineBooking>> GetAllOnlineBookingsForSabha(int sabhaId)
        {
            try
            {
                return await _unitOfWork.OnlineBooking.GetAllOnlineBookingsForSabha(sabhaId);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<IEnumerable<OnlineBooking>> GetAllOnlineBookingsForSubPropertyId(int subPropertyId)
        {
            return await _unitOfWork.OnlineBooking.GetAllOnlineBookingsForSubPropertyId(subPropertyId);
        }

        public async Task<IEnumerable<BookingSubProperty>> GetAllSubBookingProperty(int mainPropId)
        {
            try
            {
                var properties = await _unitOfWork.BookingSubProperty.GetAllAsync();
                return properties.Where(item => item.PropertyID == mainPropId);
            }
            catch (Exception ex)
            {
                // Log exception here if needed
                return null;
            }
        }

        public async Task<IEnumerable<BookingTimeSlot>> GetSpecificSubBookingPropertyBookingTimeSlots(int subPropId)
        {
            try
            {
                var properties = await _unitOfWork.BookingTimeSlots.GetAllAsync();
                return properties.Where(item => item.SubPropertyId == subPropId);
            }
            catch (Exception ex)
            {
                // Log exception here if needed
                return null;
            }
        }

        public async Task<OnlineBooking> GetBookingById(int Id)
        {
            var booking = await _unitOfWork.OnlineBooking.GetByIdAsync(Id);
            return booking;
        }

        public async Task<OnlineBooking> EditBookingStatus(int id, int statusId , int approvedById, string reson)
        {
            var booking = await _unitOfWork.OnlineBooking.GetByIdAsync(id);

            if (statusId == 1)
            {
                booking.BookingStatus = OnlineBookingStatus.Approved;
            }
            else
            {
                booking.BookingStatus = OnlineBookingStatus.Rejected;
            }
            booking.ApprovedBy = approvedById;
            booking.CancellatioReason = reson;
           
            await _unitOfWork.CommitAsync();
            return booking;
        }

        public async Task<OnlineBooking> getOnlineBookingsForById(int bookingId)
        {
            try
            {
                var bookings = await _unitOfWork.OnlineBooking.GetBookingById(bookingId);
                return bookings;
            }
            catch (Exception ex)
            {
                // Log exception here if needed
                return null; // Consider logging the exception
            }
        }
    }
}
