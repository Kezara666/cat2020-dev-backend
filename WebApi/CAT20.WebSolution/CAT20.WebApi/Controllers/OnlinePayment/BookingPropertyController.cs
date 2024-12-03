using AutoMapper;
using CAT20.Core.DTO.Final.Save;
using CAT20.Core.DTO;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.OnlienePayment;
using CAT20.Core.Models.TradeTax;
using CAT20.Core.Services.OnlinePayment;
using CAT20.WebApi.Resources.OnlinePayment;
using CAT20.WebApi.Resources.TradeTax;
using CAT20.WebApi.Resources.Vote;
using Microsoft.AspNetCore.Mvc;
using CAT20.WebApi.Controllers.Control;

namespace CAT20.WebApi.Controllers.OnlinePayment
{
    [Route("api/onlinePayments/[controller]")]
    [ApiController]
    public class BookingPropertyController : BaseController
    {
        private readonly IBookingPropertyService _bookingPropertyService;
        private readonly IBookingSubPropertyService _bookingSubPropertyService;
        private readonly IMapper _mapper;

        public BookingPropertyController(IBookingPropertyService bookingPropertyService, IMapper mapper , IBookingSubPropertyService bookingSubPropertyService) { 
        
            _bookingPropertyService = bookingPropertyService;
            _bookingSubPropertyService = bookingSubPropertyService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("getAllBookingPropertiesForSabhaId/{SabhaId}")]
        public async Task<ActionResult<IEnumerable<BookingProperty>>> GetAllBookingPropertiesBySabhaId([FromRoute] int SabhaId)
        {
            var bookingProperty = await _bookingPropertyService.GetAllBookingPropertyBySabhaIdAsync(SabhaId);
            var bookingPropertyResources = _mapper.Map<IEnumerable<BookingProperty>, IEnumerable<BookingPropertyResource>>(bookingProperty);
            return Ok(bookingPropertyResources);
        }

        [HttpGet]
        [Route("getBookingPropertyById/{id}")]
        public async Task<ActionResult<BookingPropertyResource>> GetBookingPropertyById(int id)
        {
            var bookingProperty = await _bookingPropertyService.GetBookingPropertyByIdAsync(id);
            var bookingPropertyResource = _mapper.Map<BookingProperty, BookingPropertyResource>(bookingProperty);
            return Ok(bookingPropertyResource);
        }
        [HttpPost]
        [Route("saveBookingProperty")]
        public async Task<IActionResult> Post([FromBody] SaveBookingPropertyResource bookingPropertyResource)
        {
            try
            { 
                var _token = new HTokenClaim
                {
                    userId = int.Parse(HttpContext.User.FindFirst("userid")!.Value),
                    sabhaId = int.Parse(HttpContext.User.FindFirst("sabhaId")!.Value),
                    officeId = int.Parse(HttpContext.User.FindFirst("officeID")!.Value),
                };

                var bookingProperty = _mapper.Map<SaveBookingPropertyResource, BookingProperty>(bookingPropertyResource);
                var result = await _bookingPropertyService.CreateBookingProperty(bookingProperty, _token);

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
        [Route("deleteBookingProperty/{id}")]
        public async Task<IActionResult> DeleteBookingProperty(int id)
        {
            try
            {
                if (id == 0)
                    return BadRequest("No Records Found");

                var businessNature = await _bookingPropertyService.GetBookingPropertyByIdAsync(id);

                if (businessNature == null)
                    return BadRequest("No Records Found");

                var businessSubNature = await _bookingSubPropertyService.GetBookingSubPropertyByIdAsync(businessNature.ID.Value);

                if (businessSubNature != null)
                    return BadRequest("Can't Delete. This has child records");

               var result = await  _bookingPropertyService.DeleteBookingProperty(businessNature);

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
            catch (Exception ex) { 
              return BadRequest(ex);      
            }
        }
    }
}
