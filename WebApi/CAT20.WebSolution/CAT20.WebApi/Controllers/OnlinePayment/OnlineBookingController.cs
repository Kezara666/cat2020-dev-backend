using AutoMapper;
using CAT20.Core.DTO;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.OnlienePayment;
using CAT20.Core.Services.OnlinePayment;
using CAT20.Services.OnlinePayment;
using CAT20.WebApi.Controllers.Control;
using CAT20.WebApi.Resources.OnlinePayment;
using Microsoft.AspNetCore.Mvc;

namespace CAT20.WebApi.Controllers.OnlinePayment
{

    [Route("api/onlinePayments/[controller]")]
    [ApiController]
    public class OnlineBookingController : ControllerBase

    {
        IOnlineBookingService _onlineBookingService;
        IMapper _mapper;
        private readonly IBookingTimeSlotService _bookingTimeSlotService;
        private readonly IBookingDateService _bookingDateService;

        public OnlineBookingController(IOnlineBookingService onlineBookingService, IMapper mapper, IBookingTimeSlotService bookingTimeSlotService , IBookingDateService bookingDateService)
        {
            _onlineBookingService = onlineBookingService;
            _bookingDateService = bookingDateService;
            _mapper = mapper;
            _bookingTimeSlotService = bookingTimeSlotService;
        }

        [HttpPost]
        [Route("saveOnlineBooking")]
        public async Task<IActionResult> Post([FromBody] SaveOnlineBookingResource onlineBookingResource)
        {
            try
            {
                var onlineBooking = _mapper.Map<SaveOnlineBookingResource, OnlineBooking>(onlineBookingResource);

                List<BookingDate> bookingDates = new List<BookingDate>();
                foreach (var item in onlineBookingResource.DateTimeSlot)
                {
                    var newBookingDate = new BookingDate
                    {
                        PropertyId = onlineBookingResource.PropertyId, // Replace with actual property ID
                        SubPropertyId = onlineBookingResource.SubPropertyId, // Replace with actual sub-property ID
                        BookingTimeSlotIds = item.BookingTimeSlotIds.Select(x => x.ToString()).ToArray(), // Replace with actual time slot IDs
                        BookingStatus = OnlineBookingStatus.pending, // Use appropriate status enum
                        StartDate = item.StartDate, // Replace with actual start date in string format
                        EndDate = item.EndDate, // Replace with actual end date in string format
                        CreatedBy = 1, // Replace with actual user ID
                        CreatedAt = DateTime.UtcNow,
                        OnlineBookingId = 0 // Replace with actual booking ID
                    };
                    bookingDates.Add(newBookingDate);
                }


                var result = await _onlineBookingService.CreateOnlineBooking(onlineBooking,bookingDates.ToArray());

                if (result.Item1)
                {
                    return Ok(new ApiResponseModel<object>
                    {
                        Status = 200,
                        Message = result.Item2!,
                        Data = result.Item3
                        
                    });

                }
                else if (!result.Item1 && result.Item2 != null)
                {
                    return Ok(new ApiResponseModel<object>
                    {
                        Status = 400,
                        Message = result.Item2!
                    });
                }
                else
                {
                    return BadRequest(new ApiResponseModel<object>
                    {
                        Status = 400,
                        Message = "Internal Server Error"
                    });
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getAllOnlineBookingsForSabhaId/{SabhaId}")]
        public async Task<ActionResult<IEnumerable<OnlineBooking>>> GetAllOnlineBookingsForSabha([FromRoute] int SabhaId)
         {
            var onlineBooking = await _onlineBookingService.GetAllOnlineBookingsForSabha(SabhaId);

            // Map the OnlineBooking to OnlineBookingResource and convert to a list for indexed access
            var onlineBookingResources = _mapper.Map<IEnumerable<OnlineBooking>, List<OnlineBookingResource>>(onlineBooking);

            if (onlineBookingResources != null)
            {
                for (int i = 0; i < onlineBookingResources.Count; i++)
                {
                    //Add Booking Dates
                    var bookingDates = await _bookingDateService.GetBookingDateByBookingIdAsync(onlineBookingResources[i].Id ?? 0);
                    // Map BookingDate to BookingTimeSlot
                    onlineBookingResources[i].BookingDates = bookingDates.ToArray();

                    if (onlineBookingResources[i].BookingTimeSlotIds != null)
                    {
                        var tempTimeSlots = new List<BookingTimeSlot>();

                        foreach (int timeSlotId in onlineBookingResources[i].BookingTimeSlotIds)
                        {
                            try
                            {
                                // Fetch the time slot and check for null before adding to tempTimeSlots
                                var timeSlot = await _bookingTimeSlotService.GetBookingTimeSlotById(timeSlotId);
                                if (timeSlot != null)
                                {
                                    tempTimeSlots.Add(timeSlot);
                                }
                            }
                            catch (Exception ex)
                            {
                                // Handle exception or log it
                                throw ex;
                            }
                        }
                        onlineBookingResources[i].BookingTimeSlots = bookingDates.Select(date => new BookingTimeSlot
                        {
                            // Map properties accordingly
                            Id = date.Id, // or any other property mappings                   
                                          // Set other properties from date to BookingTimeSlot as needed
                        }).ToArray();

                        // Assign the list of time slots to BookingTimeSlots
                        onlineBookingResources[i].BookingTimeSlots = tempTimeSlots.ToArray();
                    }
                }
            }
                return Ok(onlineBookingResources);

        }



        [HttpGet]
        [Route("getAllOnlineBookingsForSubPropertyId/{SabhaId}")]
        public async Task<ActionResult<IEnumerable<OnlineBooking>>> GetAllOnlineBookingsForSubPropertyId([FromRoute] int subPropertyId)
        {
            var onlineBooking = await _onlineBookingService.GetAllOnlineBookingsForSubPropertyId(subPropertyId);
            var OnlineBookingResources = _mapper.Map<IEnumerable<OnlineBooking>, IEnumerable<OnlineBookingResource>>(onlineBooking);
            return Ok(OnlineBookingResources);

        }

        [HttpGet]
        [Route("getOnlineBookingsForById/{bookingId}")]
        public async Task<ActionResult<OnlineBookingResource>> GetOnlineBookingsForById([FromRoute] int bookingId)
        {
            // Fetch online bookings for the given bookingId
            var onlineBookings = await _onlineBookingService.getOnlineBookingsForById(bookingId);

            // Check if no bookings were found
            if (onlineBookings == null)
            {
                return NotFound(); // Return 404 if no bookings are found
            }

            // Map the entity to the resource model
            var onlineBookingResources = _mapper.Map<OnlineBooking, OnlineBookingResource>(onlineBookings);

            // Return the resources with a 200 OK response
            return Ok(onlineBookingResources);
        }



        [HttpGet]
        [Route("getBookingProperty/{SabhaId}")]
        public async Task<ActionResult<IEnumerable<BookingProperty>>> getBookingProperty([FromRoute] int SabhaId)
        {
            var bookingTimeSlot = await _onlineBookingService.GetAllBookingProperty(SabhaId);
            var bookingTimeSlotResource = _mapper.Map<IEnumerable<BookingProperty>, IEnumerable<BookingPropertyResource>>(bookingTimeSlot);
            return Ok(bookingTimeSlotResource);

        }

        [HttpGet]
        [Route("getBookingSubProperty/{mainPropId}")]
        public async Task<ActionResult<IEnumerable<BookingProperty>>> getSubBookingProperty([FromRoute] int mainPropId)
        {
            var bookingTimeSlot = await _onlineBookingService.GetAllSubBookingProperty(mainPropId);
            var bookingTimeSlotResource = _mapper.Map<IEnumerable<BookingSubProperty>, IEnumerable<BookingSubPropertyResource>>(bookingTimeSlot);
            return Ok(bookingTimeSlotResource);

        }

        [HttpGet]
        [Route("getSubPropotiesTimeSlots/{subPropId}")]
        public async Task<ActionResult<IEnumerable<BookingTimeSlot>>> getSubPropotiesTimeSlots([FromRoute] int subPropId)
        {
            var bookingTimeSlot = await _onlineBookingService.GetSpecificSubBookingPropertyBookingTimeSlots(subPropId);
            var bookingTimeSlotResource = _mapper.Map<IEnumerable<BookingTimeSlot>, IEnumerable<BookingTimeSlotResource>>(bookingTimeSlot);
            return Ok(bookingTimeSlotResource);

        }

        [HttpGet]
        [Route("getAllOnlineBookingsForSabhaId/{mainPropId}/{subPropId}")]
        public async Task<ActionResult<IEnumerable<BookingDate>>> GetAllOnlineBookingsForSpecificMainProSubProp([FromRoute] int mainPropId, [FromRoute] int subPropId)
        {
            var onlineBooking = await _bookingDateService.GetAllBookingDatesForSpecificSpecificMainPropRelatedSubProp(mainPropId, subPropId);
            /*var OnlineBookingResources = _mapper.Map<IEnumerable<OnlineBooking>, IEnumerable<OnlineBookingResource>>(onlineBooking);*/
            return Ok(onlineBooking);

        }

        [HttpPut]
        [Route("editBookingStatus/{id}/{statusId}/{approvedById}/{reason}")]
        public async Task<ActionResult<OnlineBooking>> EditBookingStatus(
        [FromRoute] int id,
        [FromRoute] int statusId,
        [FromRoute] int approvedById,
        [FromRoute] string reason)
        {
            // Check if the booking existss
            var existingBooking = await _onlineBookingService.GetBookingById(id);
            if (existingBooking == null)
            {
                return NotFound(new { Message = "Booking not found." });
            }

            // Update the booking status
            var updatedBooking = await _onlineBookingService.EditBookingStatus(id, statusId, approvedById, reason);
            if (updatedBooking == null)
            {
                return StatusCode(500, new { Message = "Failed to update booking status." });
            }

            return Ok(updatedBooking);
        }
    }
}
