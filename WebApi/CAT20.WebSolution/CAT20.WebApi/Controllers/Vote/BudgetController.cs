using AutoMapper;
using CAT20.Core.DTO;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.OnlienePayment;
using CAT20.Core.Services.OnlinePayment;
using CAT20.Core.Services.Vote;
using CAT20.WebApi.Controllers.Control;
using CAT20.WebApi.Resources.OnlinePayment;
using Microsoft.AspNetCore.Mvc;
using CAT20.WebApi.Resources.Vote;
using CAT20.Core.Models.Vote;
using CAT20.WebApi.Resources.Vote.Save;
using App.Metrics.Formatters.Prometheus;

namespace CAT20.WebApi.Controllers.Vote
{
    [Route("api/vote/budget")]
    [ApiController]
    public class BudgetController : BaseController
    {
        private readonly IBudgetService _budgetService;
        private readonly IMapper _mapper;

        public BudgetController(IBudgetService budgetService, IMapper mapper)
        {
            _budgetService = budgetService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("getAllBudgetForVoteDetailId/{voteDetailId}")]
        public async Task<ActionResult<IEnumerable<BudgetResource>>> getAllBudgetForVoteDetailId([FromRoute] int voteDetailId)
        {
            var budget = await _budgetService.GetAllBudgetsByVoteDetailId(voteDetailId);
            var budgetResource = _mapper.Map<IEnumerable<Budget>, IEnumerable<BudgetResource>>(budget);
            return Ok(budgetResource);
        }

        [HttpGet]
        [Route("getAllBudgetForSabhaId/{sabhaId}")]
        public async Task<ActionResult<IEnumerable<BudgetResource>>> getAllBudgetForSabhaId([FromRoute] int sabhaId)
        {
            var budget = await _budgetService.GetAllBudgetsBySabhaID(sabhaId);
            var budgetResource = _mapper.Map<IEnumerable<Budget>, IEnumerable<BudgetResource>>(budget);
            return Ok(budgetResource);
        }

        [HttpGet]
        [Route("getBudgetById/{id}")]
        public async Task<ActionResult<BudgetResource>> GetBudgetById(int id)
        {
            var budget = await _budgetService.GetBudgetById(id);
            var budgetResource = _mapper.Map<Budget, BudgetResource>(budget);
            return Ok(budgetResource);
        }

        [HttpPost]
        [Route("saveBudget")]
        public async Task<IActionResult> Post([FromBody] SaveBudgetResource budgetResource)
        {
            try
            {
                var _token = new HTokenClaim
                {
                    userId = int.Parse(HttpContext.User.FindFirst("userid")!.Value),
                    sabhaId = int.Parse(HttpContext.User.FindFirst("sabhaId")!.Value),
                    officeId = int.Parse(HttpContext.User.FindFirst("officeID")!.Value),
                };

                var budget = _mapper.Map<SaveBudgetResource, Budget>(budgetResource);


                var result = await _budgetService.SaveBudget(budget, _token);

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
                return StatusCode(500, new ApiResponseModel<object>
                {
                    Status = 500,
                    Message = "An unexpected error occurred: " + ex.Message
                });
            }
        }

        [HttpPost]
        [Route("saveBudgetList")]
        public async Task<IActionResult> saveBulkBudgets([FromBody] List<SaveBudgetResource> budgetResourceList)
        {
            try
            {
                var _token = new HTokenClaim
                {
                    userId = int.Parse(HttpContext.User.FindFirst("userid")!.Value),
                    sabhaId = int.Parse(HttpContext.User.FindFirst("sabhaId")!.Value),
                    officeId = int.Parse(HttpContext.User.FindFirst("officeID")!.Value),
                };

                var budgetList = _mapper.Map<List<SaveBudgetResource>, List<Budget>>(budgetResourceList);


                var result = await _budgetService.SaveBudgetList(budgetList, _token);

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
                return StatusCode(500, new ApiResponseModel<object>
                {
                    Status = 500,
                    Message = "An unexpected error occurred: " + ex.Message
                });
            }
        }



        [HttpDelete]
        [Route("deleteBudget/{id}")]
        public async Task<IActionResult> DeleteBudget(int id)
        {
            try
            {
                if (id == 0)
                    return BadRequest("No Records Found");

                var budget = await _budgetService.GetBudgetById(id);

                if (budget == null)
                    return BadRequest("No Records Found");

                var result = await _budgetService.DeleteBudget(budget);

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
