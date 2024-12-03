using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CAT20.Api.Validators;
using CAT20.Core.Models.Control;
using CAT20.Core.Services.Control;
using CAT20.WebApi.Resources.Control;
using Microsoft.AspNetCore.Mvc;
using CAT20.WebApi.Resources.Vote.Save;
using CAT20.WebApi.Resources.Vote;
using Microsoft.AspNetCore.Authorization;

namespace CAT20.WebApi.Controllers.Control
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankBranchesController : BaseController
    {
        private readonly IBankBranchService _BankBranchService;
        private readonly IMapper _mapper;

        public BankBranchesController(IBankBranchService BankBranchService, IMapper mapper)
        {
            _mapper = mapper;
            _BankBranchService = BankBranchService;
        }

        [HttpGet]
        [Route("getAllBankBranches")]
        public async Task<ActionResult<IEnumerable<BankBranch>>> GetAllBankBranches()
        {
            var BankBranches = await _BankBranchService.GetAllBankBranches();
            var BankBranchResources = _mapper.Map<IEnumerable<BankBranch>, IEnumerable<BankBranchResource>>(BankBranches);

            return Ok(BankBranchResources);
        }

        [HttpGet]
        [Route("getBankBranchById/{id}")]
        public async Task<ActionResult<BankBranchResource>> GetBankBranchById([FromRoute] int id)
        {
            var BankBranch = await _BankBranchService.GetBankBranchById(id);
            var BankBranchResource = _mapper.Map<BankBranch, BankBranchResource>(BankBranch);
            return Ok(BankBranchResource);
        }

        [HttpGet]
        [Route("GetBankBranchByBankCodeAndBranchCode/{bankcode}/{branchcode}")]
        public async Task<ActionResult<BankBranchResource>> GetBankBranchById([FromRoute] int bankcode, int branchcode)
        {
            var BankBranch = await _BankBranchService.GetBankBranchByBankCodeAndBranchCode(bankcode, branchcode);
            var BankBranchResource = _mapper.Map<BankBranch, BankBranchResource>(BankBranch);
            return Ok(BankBranchResource);
        }

        [HttpGet]
        [Route("GetAllBankBranchesForBankCode/{bankcode}")]
        public async Task<ActionResult<IEnumerable<BankBranch>>> GetAllBankBranchesForBankCode(int bankcode)
        {
            var BankBranches = await _BankBranchService.GetAllBankBranchesForBankCode(bankcode);
            var BankBranchResources = _mapper.Map<IEnumerable<BankBranch>, IEnumerable<BankBranchResource>>(BankBranches);

            return Ok(BankBranchResources);
        }
    }
}