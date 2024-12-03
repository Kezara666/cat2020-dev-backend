using AutoMapper;
using CAT20.Core.DTO;
using CAT20.Core.HelperModels;
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
    public class BookingTimeSlotsController : BaseController
    {
        private readonly IBookingTimeSlotService _bookingTimeSlotService;
        private readonly IMapper _mapper;
        public BookingTimeSlotsController(IBookingTimeSlotService bookingTimeSlotService, IMapper mapper) {
        
            _mapper = mapper;
            _bookingTimeSlotService = bookingTimeSlotService;
        }
        [HttpGet]
        [Route("GetAllBookingTimeSlotsForSabha/{sabhaId}")]
        public async Task<ActionResult<IEnumerable<BookingTimeSlot>>> GetAllBookingTimeSlotsForSabha([FromRoute] int sabhaId)
        {
            var bookingTimeSlot = await _bookingTimeSlotService.GetAllBookingTimeSlotsForSabha(sabhaId);
            var bookingTimeSlotResource = _mapper.Map<IEnumerable<BookingTimeSlot>,IEnumerable< BookingTimeSlotResource >> (bookingTimeSlot);
            return Ok(bookingTimeSlotResource);
        }

        [HttpGet]
        [Route("GetAllBookingTimeSlotsForSubPropertyId/{subpropertyId}")]
        public async Task<ActionResult<IEnumerable<BookingTimeSlot>>> GetAllBookingTimeSlotsForSubPropertyId([FromRoute] int subpropertyId)
        {
            var bookingTimeSlot = await _bookingTimeSlotService.GetAllBookingTimeSlotsForSubPropertyId(subpropertyId);
            var bookingTimeSlotResource = _mapper.Map<IEnumerable<BookingTimeSlot>, IEnumerable<BookingTimeSlotResource>>(bookingTimeSlot);
            return Ok(bookingTimeSlotResource);
        }

        [HttpGet]
        [Route("getAllBookingTimeSlotBuId/{Id}")]
        public async Task<ActionResult<BookingTimeSlot>> GetAllBookingTimeSlotBuId([FromRoute] int Id)
        {
            var bookingTimeSlot = await _bookingTimeSlotService.GetBookingTimeSlotById(Id);
            var bookingTimeSlotResource = _mapper.Map<BookingTimeSlot, BookingTimeSlotResource>(bookingTimeSlot);
            return Ok(bookingTimeSlotResource);
        }

        [HttpPost]
        [Route("saveBookingProperty")]
        public async Task<IActionResult> Post([FromBody] SaveBookingTimeResource bookingTimeResource)
        {
            try
            {
                var _token = new HTokenClaim
                {
                    userId = int.Parse(HttpContext.User.FindFirst("userid")!.Value),
                    sabhaId = int.Parse(HttpContext.User.FindFirst("sabhaId")!.Value),
                    officeId = int.Parse(HttpContext.User.FindFirst("officeID")!.Value),
                    IsFinalAccountsEnabled = int.Parse(HttpContext.User.FindFirst("IsFinalAccountsEnabled")!.Value),
                    officeCode = HttpContext.User.FindFirst("officeCode")!.Value,
                    officeTypeID = int.Parse(HttpContext.User.FindFirst("officeTypeID")!.Value),
                };

                var bookingTimeSlot = _mapper.Map<SaveBookingTimeResource, BookingTimeSlot>(bookingTimeResource);
                var result = await _bookingTimeSlotService.saveBookingTimeSlot(bookingTimeSlot, _token);

                if (result.Item1)
                {
                    return Ok(new ApiResponseModel<object>
                    {
                        Status = 200,
                        Message = result.Item2!,

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
        [HttpDelete]
        [Route("deleteBookingTimeSlot/{id}")]
        public async Task<IActionResult> DeleteBookingTimeSlot(int id)
        {
            try
            {
                if (id == 0)
                    return BadRequest("No Records Found");

                var bookingTimeSlot = await _bookingTimeSlotService.GetBookingTimeSlotById(id);

                if (bookingTimeSlot == null)
                    return BadRequest("No Records Found");

                var result = await _bookingTimeSlotService.DeleteBookingTimeSlot(bookingTimeSlot);

                if (result.Item1)
                {
                    return Ok(new ApiResponseModel<object>
                    {
                        Status = 200,
                        Message = result.Item2!,
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
                return BadRequest(ex);
            }
        }
    }
}
