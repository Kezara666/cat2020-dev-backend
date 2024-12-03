using AutoMapper;
using CAT20.Core.DTO;
using CAT20.Core.HelperModels;
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
    public class BookingSubPropertyController :BaseController

    {
        private readonly IBookingSubPropertyService _bookingSubPropertyService;
        private readonly IMapper _mapper;

        public BookingSubPropertyController(IBookingSubPropertyService bookingSubPropertyService, IMapper mapper)
        {

            _bookingSubPropertyService = bookingSubPropertyService;
            _mapper = mapper;

        }
        [HttpPost("saveBookingSubProperty")]
        public async Task<IActionResult> Post([FromBody] SaveBookingSubPropertyResource bookingPropertySubResource)
        {
            try
            {
                var _token = new HTokenClaim
                {
                    userId = int.Parse(HttpContext.User.FindFirst("userid")!.Value),
                    sabhaId = int.Parse(HttpContext.User.FindFirst("sabhaId")!.Value),
                    officeId = int.Parse(HttpContext.User.FindFirst("officeID")!.Value),
                };

                var bookingSubProperty = _mapper.Map<SaveBookingSubPropertyResource, BookingSubProperty>(bookingPropertySubResource);
                var result = await _bookingSubPropertyService.CreateBookingSubProperty(bookingSubProperty, _token);

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
        [Route("deleteBookingSubProperty/{id}")]
        public async Task<IActionResult> DeleteBusinessSubNature([FromRoute] int id)
        {
            try
            {
                if (id == 0)
                    return BadRequest("No Records Found");

                var bookingSubProperty = await _bookingSubPropertyService.GetBookingSubPropertyByIdAsync(id);

                if (bookingSubProperty == null)
                    return BadRequest("No record Found");
                
                  var result =   await _bookingSubPropertyService.DeleteBookingSubProperty(bookingSubProperty);

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
               return BadRequest(ex.Message);
            }
       }

        [HttpGet]
        [Route("getAllBookingSubPropertiesForSabhaId/{SabhaId}")]
        public async Task<ActionResult<IEnumerable<BookingSubProperty>>> GetAllBookingPropertiesBySabhaId([FromRoute] int SabhaId)
        {
            var bookingSubProperty = await _bookingSubPropertyService.GetAllBookingSubPropertyBySabhaIdAsync(SabhaId);
            var bookingSubPropertyResources = _mapper.Map<IEnumerable<BookingSubProperty>, IEnumerable<BookingSubPropertyResource>>(bookingSubProperty);
            return Ok(bookingSubPropertyResources);
        }


        [HttpGet]
        [Route("getAllBookingSubPropertiesForPropertyId/{propertyId}")]
        public async Task<ActionResult<IEnumerable<BookingSubProperty>>> GetAllBookingPropertiesByPropertyId([FromRoute] int propertyId)
        {
            var bookingSubProperty = await _bookingSubPropertyService.GetAllBookingSubPropertyByPropertiIdAsync(propertyId);
            var bookingSubPropertyResources = _mapper.Map<IEnumerable<BookingSubProperty>, IEnumerable<BookingSubPropertyResource>>(bookingSubProperty);
            return Ok(bookingSubPropertyResources);
        }


    }
}
