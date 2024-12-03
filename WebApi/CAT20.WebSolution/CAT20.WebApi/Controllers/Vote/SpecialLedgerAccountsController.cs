using AutoMapper;
using CAT20.Core.DTO.Final;
using CAT20.Core.DTO;
using CAT20.Core.DTO.Vote;
using CAT20.Core.DTO.Vote.Save;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.Vote;
using CAT20.Core.Services.Vote;
using CAT20.WebApi.Resources.Vote;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CAT20.WebApi.Controllers.Control;

namespace CAT20.WebApi.Controllers.Vote
{
    [Route("api/vote/voteDetail")]
    [ApiController]

    //[Route("api/[controller]")]
    //[ApiController]
    public class SpecialLedgerAccountsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ISpecialLedgerAccountsService _specialLedgerAccountsService;

        public SpecialLedgerAccountsController(IMapper mapper , ISpecialLedgerAccountsService specialLedgerAccountsService)
        {
            _mapper = mapper;
            _specialLedgerAccountsService = specialLedgerAccountsService;
        }

        [HttpGet("specialLedgerAccountTypes")]
        public async Task<IActionResult> GetSpecialLedgerAccountTypes()
        {
            var specialLedgerAccountTypes = await _specialLedgerAccountsService.GetSpecialLedgerAccountTypes();

            var specialLedgerAccountTypesResource = _mapper.Map<IEnumerable<SpecialLedgerAccountTypes>, IEnumerable<SpecialLedgerAccountTypesResource>>(specialLedgerAccountTypes);

            return Ok(specialLedgerAccountTypesResource);
        }


        [HttpGet("specialLedgerAccountsForSabaha/{sabahId}")]
        public async Task<IActionResult> GetSpecialLedgerAccountsForSabaha(int sabahId)
        {
            var specialLedgerAccountsResource = await _specialLedgerAccountsService.GetSpecialLedgerAccountsForSabaha(sabahId);

           

            return Ok(specialLedgerAccountsResource);
        }


        [HttpPost("assignSpecialLedgerAccount")]
        public async Task<IActionResult> AssignSpecialLedgerAccount([FromBody] AssignSpecialLedgerAccount assignSpecialLedgerAccountResource)
        {
            

            var _token = new HTokenClaim
            {

                userId = int.Parse(HttpContext.User.FindFirst("userid")!.Value),
                sabhaId = int.Parse(HttpContext.User.FindFirst("sabhaId")!.Value),
                officeId = int.Parse(HttpContext.User.FindFirst("officeID")!.Value),
            };

            var result = await _specialLedgerAccountsService.AssignSpecialLedgerAccount(assignSpecialLedgerAccountResource, _token);


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

    }


}
