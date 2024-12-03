using AutoMapper;
using CAT20.Api.Validators;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.Vote;
using CAT20.Core.Services.AssessmentTax;
using CAT20.Core.Services.User;
using CAT20.Core.Services.Vote;
using CAT20.Services.AssessmentTax;
using CAT20.Services.User;
using CAT20.Services.Vote;
using CAT20.WebApi.Configuration;
using CAT20.WebApi.Resources.FInalAccount.Save;
using CAT20.WebApi.Resources.Vote;
using CAT20.WebApi.Resources.Vote.Save;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace CAT20.WebApi.Controllers.Vote
{
   
    [Route("api/expenditureVoteAllocation")]
    [ApiController]
    public class ExpenditureVoteAllocationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private IExpenditureVoteAllocationService _expenditureVoteAllocationService;

        public ExpenditureVoteAllocationController(IMapper mapper, IExpenditureVoteAllocationService expenditureVoteAllocationService)

        {
            this._mapper = mapper;
            this._expenditureVoteAllocationService = expenditureVoteAllocationService;
            
        }

        [HttpPost("saveExpenditureVoteAllocation")]
        public async Task<ActionResult<ExpenditureVoteAllocationResource>> CreateExpenditureVoteAllocation([FromBody] ExpenditureVoteAllocationResource expenditureVoteAllocation)
        {
            try
            {
                var expenditureVoteAllocationToCreate = _mapper.Map<ExpenditureVoteAllocationResource, ExpenditureVoteAllocation>(expenditureVoteAllocation);

                var newExpenditureVoteAllocation = await _expenditureVoteAllocationService.CreateExpenditureVoteAllocation(expenditureVoteAllocationToCreate);

                var expenditureVoteAllocationResource = _mapper.Map<ExpenditureVoteAllocation, ExpenditureVoteAllocationResource>(newExpenditureVoteAllocation);

                return Ok(expenditureVoteAllocationResource);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Failed to create commitment", Details = ex.Message });
            }

        }

        [HttpPost("updateExpenditureVoteAllocation")]
        public async Task<ActionResult<ExpenditureVoteAllocationResource>> UpdateExpenditureVoteAllocation([FromBody] ExpenditureVoteAllocationResource expenditureVoteAllocationResource)
        {
            var expenditureVoteAllocationToBeUpdate = await _expenditureVoteAllocationService.GetExpenditureVoteAllocationById(expenditureVoteAllocationResource.ID);

            if (expenditureVoteAllocationToBeUpdate == null)
                return NotFound();

            var product = _mapper.Map<ExpenditureVoteAllocationResource, ExpenditureVoteAllocation>(expenditureVoteAllocationResource);

            await _expenditureVoteAllocationService.UpdateExpenditureVoteAllocation(expenditureVoteAllocationToBeUpdate, product);

            var updatedExpenditureVoteAllocation = await _expenditureVoteAllocationService.GetExpenditureVoteAllocationById(expenditureVoteAllocationResource.ID);
            var updatedExpenditureVoteAllocationResource = _mapper.Map<ExpenditureVoteAllocation, ExpenditureVoteAllocationResource>(updatedExpenditureVoteAllocation);

            return Ok(updatedExpenditureVoteAllocationResource);
        }

        [HttpPost]
        [Route("deleteExpenditureVoteAllocation/{id}")]
        public async Task<IActionResult> DeleteExpenditureVoteAllocation([FromRoute] int id)
        {
            if (id == 0)
                return BadRequest();

            var voteAllocation = await _expenditureVoteAllocationService.GetExpenditureVoteAllocationById(id);

            if (voteAllocation == null)
                return NotFound();

            await _expenditureVoteAllocationService.DeleteExpenditureVoteAllocation(voteAllocation);

            return NoContent();
        }

        [HttpGet]
        [Route("getAllExpenditureVoteAllocationsForVoteDetailIdandSabhaId/{VoteDetailId}/{SabhaId}")]
        public async Task<ActionResult<IEnumerable<ExpenditureVoteAllocation>>> GetAllWithExpenditureVoteAllocationByVoteDetailIdSabhaId([FromRoute] int VoteDetailId, [FromRoute] int SabhaId)
        {
            var expenditureVoteAllocations = await _expenditureVoteAllocationService.GetAllWithExpenditureVoteAllocationByVoteDetailIdSabhaId(VoteDetailId, SabhaId);
            var expenditureVoteAllocationResources = _mapper.Map<IEnumerable<ExpenditureVoteAllocation>, IEnumerable<ExpenditureVoteAllocationResource>>(expenditureVoteAllocations);

            return Ok(expenditureVoteAllocationResources);
        }

        [HttpGet]
        [Route("getAllExpenditureVoteAllocationsForVoteDetailIdandSabhaIdandYear/{VoteDetailId}/{SabhaId}/{Year}")]
        public async Task<ActionResult<IEnumerable<ExpenditureVoteAllocation>>> GetAllExpenditureVoteAllocationsForVoteDetailIdandSabhaIdandYear([FromRoute] int VoteDetailId, [FromRoute] int SabhaId, [FromRoute] int Year)
        {
            var expenditureVoteAllocations = await _expenditureVoteAllocationService.GetAllExpenditureVoteAllocationsForVoteDetailIdandSabhaIdandYear(VoteDetailId, SabhaId, Year);
            var expenditureVoteAllocationResources = _mapper.Map<IEnumerable<ExpenditureVoteAllocation>, IEnumerable<ExpenditureVoteAllocationResource>>(expenditureVoteAllocations);

            return Ok(expenditureVoteAllocationResources);
        }

    } 
}
