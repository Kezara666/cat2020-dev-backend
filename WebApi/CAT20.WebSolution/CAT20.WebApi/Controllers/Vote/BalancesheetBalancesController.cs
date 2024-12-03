using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CAT20.Api.Validators;
using CAT20.Core.Models.Vote;
using CAT20.WebApi.Resources.Vote.Save;
using CAT20.WebApi.Resources.Vote;
using CAT20.Core.Services.Vote;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CAT20.WebApi.Controllers.Control;

namespace CAT20.Api.Controllers
{
    [Route("api/vote/BalancesheetBalances")]
    [ApiController]
    public class BalancesheetBalancesController : BaseController
    {
        private readonly IBalancesheetBalanceService _balancesheetBalanceService;
        private readonly IMapper _mapper;

        public BalancesheetBalancesController(IBalancesheetBalanceService balancesheetBalanceService, IMapper mapper)
        {
            this._mapper = mapper;
            this._balancesheetBalanceService = balancesheetBalanceService;
        }

        [HttpGet("GetAllBalancesheetBalances")]
        public async Task<ActionResult<IEnumerable<BalancesheetBalance>>> GetAllBalancesheetBalances()
        {
            var balancesheetBalances = await _balancesheetBalanceService.GetAllBalancesheetBalances();
            var balancesheetBalanceResources = _mapper.Map<IEnumerable<BalancesheetBalance>, IEnumerable<BalancesheetBalanceResource>>(balancesheetBalances);

            return Ok(balancesheetBalanceResources);
        }

        [HttpGet]
        [Route("GetBalancesheetBalanceById/{id}")]
        public async Task<ActionResult<BalancesheetBalanceResource>> GetBalancesheetBalanceById([FromRoute]int id)
        {
            var balancesheetBalance = await _balancesheetBalanceService.GetBalancesheetBalanceById(id);
            var balancesheetBalanceResource = _mapper.Map<BalancesheetBalance, BalancesheetBalanceResource>(balancesheetBalance);
            return Ok(balancesheetBalanceResource);
        }

        [HttpPost("saveBalancesheetBalance")]
        public async Task<ActionResult<BalancesheetBalanceResource>> CreateBalancesheetBalance(SaveBalancesheetBalanceResource saveBalancesheetBalanceResource)
        {
            //var validator = new SaveBalancesheetBalanceResourceValidator();
            //var validationResult = await validator.ValidateAsync(saveBalancesheetBalanceResource);

            //if (!validationResult.IsValid)
            //    return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var balancesheetBalanceToCreate = _mapper.Map<SaveBalancesheetBalanceResource, BalancesheetBalance>(saveBalancesheetBalanceResource);

            var newBalancesheetBalance = await _balancesheetBalanceService.CreateBalancesheetBalance(balancesheetBalanceToCreate);

            var balancesheetBalance = await _balancesheetBalanceService.GetBalancesheetBalanceById(newBalancesheetBalance.ID);

            var balancesheetBalanceResource = _mapper.Map<BalancesheetBalance, BalancesheetBalanceResource>(balancesheetBalance);

            return Ok(balancesheetBalanceResource);
        }

        [HttpPost("updateBalancesheetBalance")]
        public async Task<ActionResult<BalancesheetBalanceResource>> UpdateBalancesheetBalance(SaveBalancesheetBalanceResource saveBalancesheetBalanceResource)
        {
            //var validator = new SaveBalancesheetBalanceResourceValidator();
            //var validationResult = await validator.ValidateAsync(saveBalancesheetBalanceResource);

            //var requestIsInvalid = saveBalancesheetBalanceResource.ID == 0 || !validationResult.IsValid;

            //if (requestIsInvalid)
            //    return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var balancesheetBalanceToBeUpdate = await _balancesheetBalanceService.GetBalancesheetBalanceById(saveBalancesheetBalanceResource.ID);

            if (balancesheetBalanceToBeUpdate == null)
                return NotFound();

            var product = _mapper.Map<SaveBalancesheetBalanceResource, BalancesheetBalance>(saveBalancesheetBalanceResource);

            await _balancesheetBalanceService.UpdateBalancesheetBalance(balancesheetBalanceToBeUpdate, product);

            var updatedBalancesheetBalance = await _balancesheetBalanceService.GetBalancesheetBalanceById(saveBalancesheetBalanceResource.ID);
            var updatedBalancesheetBalanceResource = _mapper.Map<BalancesheetBalance, BalancesheetBalanceResource>(updatedBalancesheetBalance);

            return Ok(updatedBalancesheetBalanceResource);
        }

        [HttpPost]
        [Route("deleteBalancesheetBalance/{id}")]
        public async Task<IActionResult> DeleteBalancesheetBalance([FromRoute]int id)
        {
            if (id == 0)
                return BadRequest();

            var balancesheetBalance = await _balancesheetBalanceService.GetBalancesheetBalanceById(id);

            if (balancesheetBalance == null)
                return NotFound();

            await _balancesheetBalanceService.DeleteBalancesheetBalance(balancesheetBalance);

            return NoContent();
        }

        [HttpGet]
        [Route("GetAllBalancesheetBalancesForVoteDetailId/{VoteDetailId}")]
        public async Task<ActionResult<IEnumerable<BalancesheetBalance>>> GetAllWithBalancesheetBalanceByVoteDetailId([FromRoute]int VoteDetailId)
        {
            var balancesheetBalances = await _balancesheetBalanceService.GetAllWithBalancesheetBalanceByVoteDetailId(VoteDetailId);
            var balancesheetBalanceResources = _mapper.Map<IEnumerable<BalancesheetBalance>, IEnumerable<BalancesheetBalanceResource>>(balancesheetBalances);

            return Ok(balancesheetBalanceResources);
        }

        [HttpGet]
        [Route("GetAllBalancesheetBalancesForVoteDetailIdandSabhaId/{voteId}/{sabhaId}")]
        public async Task<ActionResult<IEnumerable<BalancesheetBalance>>> GetAllWithBalancesheetBalanceByVoteDetailIdandSabhaId([FromRoute]int voteId,[FromRoute] int sabhaId)
        {
            var balancesheetBalances = await _balancesheetBalanceService.GetAllWithBalancesheetBalanceByVoteDetailIdandSabhaId(voteId, sabhaId);
            var balancesheetBalanceResources = _mapper.Map<IEnumerable<BalancesheetBalance>, IEnumerable<BalancesheetBalanceResource>>(balancesheetBalances);

            return Ok(balancesheetBalanceResources);
        }

        [HttpGet]
        [Route("GetAllBalancesheetBalancesForVoteDetailIdandYear/{voteId}/{year}")]
        public async Task<ActionResult<IEnumerable<BalancesheetBalance>>> GetAllBalancesheetBalancesForVoteDetailIdandYear([FromRoute] int voteId, [FromRoute] int year)
        {
            var balancesheetBalances = await _balancesheetBalanceService.GetAllBalancesheetBalancesForVoteDetailIdandYear(voteId, year);
            var balancesheetBalanceResources = _mapper.Map<IEnumerable<BalancesheetBalance>, IEnumerable<BalancesheetBalanceResource>>(balancesheetBalances);

            return Ok(balancesheetBalanceResources);
        }

        [HttpGet]
        [Route("getAllBalancesheetBalancesForSabhaId/{sabhaId}")]
        public async Task<ActionResult<IEnumerable<BalancesheetBalance>>> GetAllBalancesheetBalancesForSabhaId([FromRoute] int sabhaId)
        {
            var balancesheetBalances = await _balancesheetBalanceService.GetAllBalancesheetBalancesForSabhaId( sabhaId);
            var balancesheetBalanceResources = _mapper.Map<IEnumerable<BalancesheetBalance>, IEnumerable<BalancesheetBalanceResource>>(balancesheetBalances);

            return Ok(balancesheetBalanceResources);
        }
    }
}
