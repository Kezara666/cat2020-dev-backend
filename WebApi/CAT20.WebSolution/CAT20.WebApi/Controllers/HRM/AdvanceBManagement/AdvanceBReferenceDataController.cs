using AutoMapper;
using CAT20.Core.DTO.Vote.Save;
using CAT20.Core.DTO;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.HRM.LoanManagement;
using CAT20.Core.Services.HRM.LoanManagement;
using CAT20.Services.HRM.LoanManagement;
using CAT20.WebApi.Controllers.Control;
using CAT20.WebApi.Resources.HRM.LoanManagement;
using Microsoft.AspNetCore.Mvc;
using CAT20.Core.DTO.HRM;
using CAT20.Data.Repositories.HRM.LoanManagement;

namespace CAT20.WebApi.Controllers.HRM.AdvanceBManagement
{
    //[Route("api/HRM/LoanManagement/ReferenceData")]
    [Route("api/HRM/advanceBManagement/ReferenceData")]
    [ApiController]
    public class AdvanceBReferenceDataController : BaseController
    {
        private readonly IAdvanceBTypeDataService _advanceBTypeDataService;
        private readonly IMapper _mapper;

        public AdvanceBReferenceDataController(IAdvanceBTypeDataService advanceVTypeDataService, IMapper mapper)
        {
            _advanceBTypeDataService = advanceVTypeDataService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("getAdvanceBTypeDataById/{id}")]
        public async Task<ActionResult<AdvanceBTypeDataResource>> GetAdvanceBTypeById(int id)
        {
            var advanceBTypeData = await _advanceBTypeDataService.GetAdvanceBTypeDataById(id);
            var advanceBTypeDataResource = _mapper.Map<AdvanceBTypeData, AdvanceBTypeDataResource>(advanceBTypeData);

            if (advanceBTypeData == null)
            {
                return NotFound("Not Found");
            }

            return Ok(advanceBTypeDataResource);
        }


        [HttpGet]
        [Route("getLoanTypeData")]
        public async Task<ActionResult<IEnumerable<AdvanceBTypeDataResource>>> GetAllLoanTypeData()
        {
            var advanceBTypeData = await _advanceBTypeDataService.GetAllLoanTypeData();
            var advanceBTypeDataResource = _mapper.Map<IEnumerable<AdvanceBTypeData>, IEnumerable<AdvanceBTypeDataResource>>(advanceBTypeData);

            if (advanceBTypeData == null)
            {
                return NotFound("Not Found");
            }

            return Ok(advanceBTypeDataResource);
        }

        [HttpGet]
        [Route("getAllAdvancedLedgerTypesMappingForSabha/{sabhaId}")]
        public async Task<ActionResult<IEnumerable<AdvanceBTypeLedgerMappingResource>>> GetAllAdvancedLedgerTypesMappingForSabha([FromRoute] int sabhaId)
        {
            var advanceBTypeLedgerMapping = await _advanceBTypeDataService.GetAllAdvancedLedgerTypesMappingForSabha(sabhaId);
            var advanceBTypeLedgerMappingResource = _mapper.Map<IEnumerable<AdvanceBTypeLedgerMapping>, IEnumerable<AdvanceBTypeLedgerMappingResource>>(advanceBTypeLedgerMapping);

            if (advanceBTypeLedgerMappingResource == null)
            {
                return NotFound("Not Found");
            }

            return Ok(advanceBTypeLedgerMappingResource);
        }



        [HttpGet]
        [Route("getAdvanceBTypeDataByAccountSystemVersionAndSabha")]
        public async Task<ActionResult<IEnumerable<AdvanceBTypeDataResource>>> GetLoanTypeDataByAccountSystemVersionAndSabha()
        {
            var _token = new HTokenClaim
            {
                sabhaId = int.Parse(HttpContext.User.FindFirst("sabhaId")!.Value),
                AccountSystemVersionId = int.Parse(HttpContext.User.FindFirst("accountSystemVersionId")!.Value),
            };

            var advanceBTypeData = await _advanceBTypeDataService.GetAdvanceBTypeDataByAccountSystemVersionAndSabhaAsync(_token.AccountSystemVersionId, _token.sabhaId);
            var advanceBTypeDataResource = _mapper.Map<IEnumerable<AdvanceBTypeData>, IEnumerable<AdvanceBTypeDataResource>>(advanceBTypeData);

            return Ok(advanceBTypeDataResource);
        }


        [HttpPost("saveAdvancedBLoanVoteAssignment")]
        public async Task<IActionResult> saveAdvancedBLoanVoteAssignment([FromBody] SaveAdvancedBLoanLedgerTypeMapping saveAdvancedBLoanLedgerTypeMappingResource)
        {


            var _token = new HTokenClaim
            {

                userId = int.Parse(HttpContext.User.FindFirst("userid")!.Value),
                sabhaId = int.Parse(HttpContext.User.FindFirst("sabhaId")!.Value),
                officeId = int.Parse(HttpContext.User.FindFirst("officeID")!.Value),
            };

            var result = await _advanceBTypeDataService.saveAdvancedBLoanVoteAssignment(saveAdvancedBLoanLedgerTypeMappingResource, _token);


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
