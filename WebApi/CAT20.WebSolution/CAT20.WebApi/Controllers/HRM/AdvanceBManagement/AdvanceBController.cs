using AutoMapper;
using CAT20.Core.DTO;
using CAT20.Core.DTO.HRM;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.HRM.LoanManagement;
using CAT20.Core.Services.HRM.LoanManagement;
using CAT20.Core.Services.HRM.PersonalFile;
using CAT20.Services.HRM.PersonalFile;
using CAT20.WebApi.Controllers.Control;
using CAT20.WebApi.Resources.HRM.LoanManagement;
using CAT20.WebApi.Resources.HRM.PersonalFile;
using Microsoft.AspNetCore.Mvc;

namespace CAT20.WebApi.Controllers.HRM.AdvanceBManagement
{
    //[Route("api/HRM/LoanManagement/Loans")]
    [Route("api/HRM/advanceBManagement/advanceb")]
    [ApiController]
    public class AdvanceBController : BaseController
    {
        private readonly IAdvanceBService _advanceBService;
        private readonly IMapper _mapper;

        public AdvanceBController(IAdvanceBService advanceBService, IMapper mapper)
        {
            _advanceBService = advanceBService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("getLoanById/{id}")]
        public async Task<ActionResult<AdvanceBResource>> GetLoanById(int id)
        {
            var loan = await _advanceBService.GetLoanById(id);
            var loanResource = _mapper.Map<AdvanceB, AdvanceBResource>(loan);

            if (loan == null)
            {
                return NotFound("Not Found");
            }

            return Ok(loanResource);
        }

     

        [HttpGet]
        [Route("getAllAdvanceBsForSabha/{sabhaId}")]
        public async Task<ActionResult<IEnumerable<AdvanceBResource>>> getAllAdvanceBsForSabha(int sabhaId)
        {
            var _token = new HTokenClaim
            {
                sabhaId = int.Parse(HttpContext.User.FindFirst("sabhaId")!.Value),
            };

            var loans = await _advanceBService.GetAllLoansBySabha(_token.sabhaId);
            var loanResources = _mapper.Map<IEnumerable<AdvanceB>, IEnumerable<AdvanceBResource>>(loans);

            if (loans == null)
            {
                return NotFound("No loans found for the specified sabha");
            }

            return Ok(loanResources);
        }

        [HttpGet]
        [Route("getAllLoansByOffice")]
        public async Task<ActionResult<IEnumerable<AdvanceBResource>>> GetAllLoansByOffice()
        {
            var _token = new HTokenClaim
            {
                officeId = int.Parse(HttpContext.User.FindFirst("officeId")!.Value),
            };

            var loans = await _advanceBService.GetAllLoansByOffice(_token.officeId);
            var loanResources = _mapper.Map<IEnumerable<AdvanceB>, IEnumerable<AdvanceBResource>>(loans);

            if (loans == null)
            {
                return NotFound("No loans found for the specified office");
            }

            return Ok(loanResources);
        }

        [HttpGet]
        [Route("getAllLoansByEMPId/{empId}")]
        public async Task<ActionResult<IEnumerable<AdvanceBResource>>> GetAllLoansByEMPId(int empId)
        {
            var loans = await _advanceBService.GetAllLoansByEMPId(empId);
            var loanResources = _mapper.Map<IEnumerable<AdvanceB>, IEnumerable<AdvanceBResource>>(loans);

            if (loans == null)
            {
                return NotFound("No loans found for the specified employee");
            }

            return Ok(loanResources);
        }

        [HttpGet]
        [Route("getAllLoansByEMPIdAndLoanTypeId/{empId}/{loantypeid}")]
        public async Task<ActionResult<IEnumerable<AdvanceBResource>>> GetAllLoansByEMPIdAndLoanTypeId(int empId, int loantypeid)
        {
            var loans = await _advanceBService.GetAllLoansByEMPIdAndLoanTypeId(empId, loantypeid);
            var loanResources = _mapper.Map<IEnumerable<AdvanceB>, IEnumerable<AdvanceBResource>>(loans);

            if (loans == null)
            {
                return NotFound("No loans found for the specified employee and loan type");
            }

            return Ok(loanResources);
        }

        [HttpGet]
        [Route("getAllNewLoansBySabhaId")]
        public async Task<ActionResult<IEnumerable<AdvanceBResource>>> GetAllNewLoansBySabhaId()
        {
            var _token = new HTokenClaim
            {
                sabhaId = int.Parse(HttpContext.User.FindFirst("sabhaId")!.Value),
            };

            var loans = await _advanceBService.GetAllNewLoansBySabhaId(_token.sabhaId);
            var loanResources = _mapper.Map<IEnumerable<AdvanceB>, IEnumerable<AdvanceBResource>>(loans);
            if (loans == null)
            {
                return NotFound("No new loans found for the specified sabha");
            }

            return Ok(loanResources);
        }

        [HttpPost]
        [Route("saveAdvanceB")]
        public async Task<ActionResult<AdvanceBResource>> SaveAdvanceB([FromBody] SaveAdvanceBCombinedResource advanceBCombinedResource)
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

            var loan = _mapper.Map<SaveAdvanceBResource, AdvanceB>(advanceBCombinedResource.AdvanceB!);
            var openSettlement = _mapper.Map<SaveAdvanceBSettlementResource, AdvanceBSettlement>(advanceBCombinedResource.AdvanceBSettlement!);
            var result = await _advanceBService.Create(loan, openSettlement, _token);

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
                    Status = 500,
                    Message = "Internal Server Error!!."
                });

            }
        }

        [HttpPost]
        [Route("updateadvanceB")]
        public async Task<ActionResult<AdvanceBResource>> UpdateAdvanceB([FromBody] AdvanceBResource loanResource)
        {
            var _token = new HTokenClaim
            {
                userId = int.Parse(HttpContext.User.FindFirst("userid")!.Value),
                sabhaId = int.Parse(HttpContext.User.FindFirst("sabhaId")!.Value),
                officeId = int.Parse(HttpContext.User.FindFirst("officeID")!.Value),
            };

            var loan = _mapper.Map<AdvanceBResource, AdvanceB>(loanResource);
            var result = await _advanceBService.UpdateAdvanceB(loan, _token);

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
                        Status = 500,
                        Message = "Internal Server Error!!."
                    });

                }

        }

        [HttpGet]
        [Route("getAllAdvanceBForSettlementSabha/{sabhaId}")]
        public async Task<ActionResult<IEnumerable<AdvanceBResource>>> GetAllAdvanceBForSettlementSabhaId(int sabhaId)
        {
            var _token = new HTokenClaim
            {
                sabhaId = int.Parse(HttpContext.User.FindFirst("sabhaId")!.Value),
            };

            var advanceBs = await _advanceBService.GetAllAdvanceBForSettlementSabhaId(sabhaId, _token);
            var advanceBResources = _mapper.Map<IEnumerable<AdvanceB>, IEnumerable<AdvanceBResource>>(advanceBs);

            return Ok(advanceBResources);
        }
    }
}
