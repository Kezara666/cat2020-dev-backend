using AutoMapper;
using CAT20.Core.DTO;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.OnlienePayment;
using CAT20.Core.Services.OnlinePayment;
using CAT20.WebApi.Resources.OnlinePayment;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CAT20.WebApi.Controllers.OnlinePayment
{
    [Route("api/onlinePayments/[controller]")]
    [ApiController]
    public class ChargingSchemeController : ControllerBase
    {
        private readonly IChargingSchemeService _chargingSchemeService;
        private readonly IMapper _mapper;

        public ChargingSchemeController(IChargingSchemeService chargingSchemeService, IMapper mapper)
        {
            _chargingSchemeService = chargingSchemeService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("getAllChargingSchemesForSubPropertyId/{subPropertyId}")]
        public async Task<ActionResult<IEnumerable<ChargingSchemeResource>>> GetAllChargingSchemesBySubPropertyId([FromRoute] int subPropertyId)
        {
            var chargingSchemes = await _chargingSchemeService.GetAllChargingSchemeBySubPropertyId(subPropertyId);
            var chargingSchemeResources = _mapper.Map<IEnumerable<ChargingScheme>, IEnumerable<ChargingSchemeResource>>(chargingSchemes);
            return Ok(chargingSchemeResources);
        }

        [HttpGet]
        [Route("getChargingSchemeById/{id}")]
        public async Task<ActionResult<ChargingSchemeResource>> GetChargingSchemeById(int id)
        {
            var chargingScheme = await _chargingSchemeService.GetChargingSchemeById(id);
            var chargingSchemeResource = _mapper.Map<ChargingScheme, ChargingSchemeResource>(chargingScheme);
            return Ok(chargingSchemeResource);
        }

        [HttpPost]
        [Route("saveChargingScheme")]
        public async Task<IActionResult> Post([FromBody] SaveChargingSchemeResource chargingSchemeResource)
        {
            try
            {
                // Verify claims exist before accessing them
                //var userIdClaim = HttpContext.User.FindFirst("userid");
                //var sabhaIdClaim = HttpContext.User.FindFirst("sabhaId");
                //var officeIdClaim = HttpContext.User.FindFirst("officeID");

                //if (userIdClaim == null || sabhaIdClaim == null || officeIdClaim == null)
                //{
                //    return BadRequest(new ApiResponseModel<object>
                //    {
                //        Status = 400,
                //        Message = "Missing required claims"
                //    });
                //}

                //var _token = new HTokenClaim
                //{
                //    userId = int.Parse(userIdClaim.Value),
                //    sabhaId = int.Parse(sabhaIdClaim.Value),
                //    officeId = int.Parse(officeIdClaim.Value),
                //};

                var userIdClaim = "2330";
                var sabhaIdClaim = "3";
                var officeIdClaim = "118";

                var _token = new HTokenClaim
                {
                    userId = int.Parse(userIdClaim),
                    sabhaId = int.Parse(sabhaIdClaim),
                    officeId = int.Parse(officeIdClaim),
                };

                // Map the resource to the entity
                var chargingScheme = _mapper.Map<SaveChargingSchemeResource, ChargingScheme>(chargingSchemeResource);

                // Call service to save the charging scheme
                var result = await _chargingSchemeService.SaveChargingScheme(chargingScheme, _token);

                // Check result and return appropriate response
                if (result.Item1)
                {
                    return StatusCode(201, new ApiResponseModel<object>
                    {
                        Status = 201,
                        Message = result.Item2!,
                    });
                }
                else if (!result.Item1 && result.Item2 != null)
                {
                    return BadRequest(new ApiResponseModel<object>
                    {
                        Status = 400,
                        Message = result.Item2!
                    });
                }
                else
                {
                    return StatusCode(500, new ApiResponseModel<object>
                    {
                        Status = 500,
                        Message = "Internal Server Error"
                    });
                }
            }
            catch (Exception ex)
            {
                // Consider logging the error here
                return StatusCode(500, new ApiResponseModel<object>
                {
                    Status = 500,
                    Message = "An unexpected error occurred: " + ex.Message
                });
            }
        }


        [HttpDelete]
        [Route("deleteChargingScheme/{id}")]
        public async Task<IActionResult> DeleteChargingScheme(int id)
        {
            try
            {
                if (id == 0)
                    return BadRequest("No Records Found");

                var chargingScheme = await _chargingSchemeService.GetChargingSchemeById(id);

                if (chargingScheme == null)
                    return BadRequest("No Records Found");

                var result = await _chargingSchemeService.DeleteChargingScheme(chargingScheme);

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
    }
}
