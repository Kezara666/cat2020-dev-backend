using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.OnlienePayment;
using Microsoft.AspNetCore.Mvc;

namespace CAT20.Core.Services.OnlinePayment
{
    public interface IOnlineBookingService
    {
        Task<(bool, string?, OnlineBooking?)> CreateOnlineBooking(OnlineBooking newBooking, BookingDate[] bookingDate);
        Task<IEnumerable<OnlineBooking>> GetAllOnlineBookingsForSabha(int sabhaId);
        Task<IEnumerable<OnlineBooking>> GetAllOnlineBookingsForSubPropertyId(int subPropertyId);
        Task<IEnumerable<BookingProperty>> GetAllBookingProperty(int sabhaId);
        Task<OnlineBooking> GetBookingById(int Id);
        Task<IEnumerable<BookingSubProperty>> GetAllSubBookingProperty(int mainProp);
        Task<IEnumerable<BookingTimeSlot>> GetSpecificSubBookingPropertyBookingTimeSlots(int mainProp);
        Task<OnlineBooking> EditBookingStatus(int id, int statusId, int approvedById, string reson);
        Task<OnlineBooking> getOnlineBookingsForById(int bookingId);


    }
}
