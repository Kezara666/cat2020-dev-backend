using AutoMapper;
using CAT20.Core.DTO;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.Control;
using CAT20.Core.Services.Control;
using CAT20.WebApi.Resources.Control;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CAT20.WebApi.Controllers.Control
{
    //[Route("api/[controller]")]
    //[ApiController]

    [Route("api/control/agents")]
    [ApiController]
    public class AgentsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IAgentsService _agentsService;

        public AgentsController(IMapper mapper,IAgentsService agentsService)
        {
            _mapper = mapper;
            _agentsService = agentsService;
        }


        [HttpGet("getAllAgentsForSabha/{sabhaId}")]
        public async Task<IActionResult> GetAgentsForSabha(int sabhaId)
        {
            var agents = await _agentsService.GetAgentsForSabha(sabhaId);
            var agentsResource = _mapper.Map<IEnumerable<Agents>,IEnumerable< AgentsResource >> (agents);
            return Ok(agentsResource);
        }

        [HttpPost("save")]
        public async Task<IActionResult> CreateAgent([FromBody] SaveAgentsResource saveAgentsResource)
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

            var agentResource = _mapper.Map<SaveAgentsResource,Agents>(saveAgentsResource);

            var result = await _agentsService.Craete(agentResource, _token);

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
                    Message = result.Item2!,
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

        [HttpPost("update")]
        public async Task<IActionResult> UpdateAgent([FromBody] SaveAgentsResource saveAgentsResource)
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

            var agentResource = _mapper.Map<Agents>(saveAgentsResource);

            var result = await _agentsService.Update(agentResource, _token);

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
                    Message = result.Item2!,
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

/*        [HttpPost("delete")]
        public async Task<IActionResult> DeleteAgent(int agentId)
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

            var result = await _agentsService.Delete(agentId, _token);

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
                    Message = result.Item2!,
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
*/

        
    }
}
