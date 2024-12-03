using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CAT20.Api.Validators;
using CAT20.Core.Models.Vote;
using CAT20.Core.Models.TradeTax;
using CAT20.WebApi.Resources.Vote;
using CAT20.Core.Services.Vote;
using CAT20.WebApi.Resources.TradeTax;
using CAT20.Core.Services.TradeTax;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CAT20.WebApi.Controllers.Control;
using CAT20.WebApi.Resources.Mixin;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Mixin;
using CAT20.Services.Vote;
using CAT20.Services.TradeTax;
using CAT20.WebApi.Resources.Control;
using CAT20.Core.Services.Control;
using CAT20.Core.Services.Mixin;
using System.IdentityModel.Tokens.Jwt;

namespace CAT20.WebApi.Controllers.TradeTax
{
    [Route("api/tradetax/tradeTaxVoteAssignments")]
    [ApiController]
    public class TradeTaxVoteAssignmentsController : BaseController
    {
        private readonly ITradeTaxVoteAssignmentService _tradeTaxVoteAssignmentService;
        private readonly IAccountDetailService _accountDetailService;
        private readonly IMapper _mapper;
        private readonly IVoteAssignmentDetailsService _voteAssignmentDetailsService;
        private readonly IVoteDetailService _voteDetailService;
        private readonly ITaxTypeService _taxTypeService;

        public TradeTaxVoteAssignmentsController(ITradeTaxVoteAssignmentService tradeTaxVoteAssignmentService, IMapper mapper, IAccountDetailService accountDetailService, IVoteAssignmentDetailsService voteAssignmentDetailsService, IVoteDetailService voteDetailService, ITaxTypeService taxTypeService)
        {
            _mapper = mapper;
            _tradeTaxVoteAssignmentService = tradeTaxVoteAssignmentService;
            _accountDetailService = accountDetailService;
            _voteAssignmentDetailsService = voteAssignmentDetailsService;
            _voteDetailService = voteDetailService;
            _taxTypeService = taxTypeService;
        }

        [HttpGet("getAllTradeTaxVoteAssignments")]
        public async Task<ActionResult<IEnumerable<TradeTaxVoteAssignment>>> GetAllTradeTaxVoteAssignments()
        {
            var tradeTaxVoteAssignments = await _tradeTaxVoteAssignmentService.GetAllTradeTaxVoteAssignments();

            var tradeTaxVoteAssignmentResources = _mapper.Map<IEnumerable<TradeTaxVoteAssignment>, IEnumerable<TradeTaxVoteAssignmentResource>>(tradeTaxVoteAssignments);

            return Ok(tradeTaxVoteAssignmentResources);
        }

        [HttpGet]
        [Route("getTradeTaxVoteAssignmentById/{id}")]
        public async Task<ActionResult<TradeTaxVoteAssignmentResource>> GetTradeTaxVoteAssignmentById([FromRoute] int id)
        {
            var tradeTaxVoteAssignment = await _tradeTaxVoteAssignmentService.GetTradeTaxVoteAssignmentById(id);
            var tradeTaxVoteAssignmentResource = _mapper.Map<TradeTaxVoteAssignment, TradeTaxVoteAssignmentResource>(tradeTaxVoteAssignment);
            return Ok(tradeTaxVoteAssignmentResource);
        }

        [HttpPost("saveTradeTaxVoteAssignment")]
        public async Task<ActionResult<TradeTaxVoteAssignmentResource>> CreateTradeTaxVoteAssignment([FromBody] TradeTaxVoteAssignmentResource saveTradeTaxVoteAssignmentResource)
        {

            var tradeTaxVoteAssignmentToCreate = _mapper.Map<TradeTaxVoteAssignmentResource, TradeTaxVoteAssignment>(saveTradeTaxVoteAssignmentResource);

            var newTradeTaxVoteAssignment = await _tradeTaxVoteAssignmentService.CreateTradeTaxVoteAssignment(tradeTaxVoteAssignmentToCreate);

            var tradeTaxVoteAssignment = await _tradeTaxVoteAssignmentService.GetTradeTaxVoteAssignmentById(newTradeTaxVoteAssignment.ID);

            var tradeTaxVoteAssignmentResource = _mapper.Map<TradeTaxVoteAssignment, TradeTaxVoteAssignmentResource>(tradeTaxVoteAssignment);

            return Ok(tradeTaxVoteAssignmentResource);
        }

        [HttpPost("updateTradeTaxVoteAssignment")]
        public async Task<ActionResult<TradeTaxVoteAssignmentResource>> UpdateTradeTaxVoteAssignment(TradeTaxVoteAssignmentResource saveTradeTaxVoteAssignmentResource)
        {
            var tradeTaxVoteAssignmentToBeUpdate = await _tradeTaxVoteAssignmentService.GetTradeTaxVoteAssignmentById(saveTradeTaxVoteAssignmentResource.ID);

            if (tradeTaxVoteAssignmentToBeUpdate == null)
                return NotFound();

            var product = _mapper.Map<TradeTaxVoteAssignmentResource, TradeTaxVoteAssignment>(saveTradeTaxVoteAssignmentResource);

            await _tradeTaxVoteAssignmentService.UpdateTradeTaxVoteAssignment(tradeTaxVoteAssignmentToBeUpdate, product);

            var updatedTradeTaxVoteAssignment = await _tradeTaxVoteAssignmentService.GetTradeTaxVoteAssignmentById(saveTradeTaxVoteAssignmentResource.ID);
            var updatedTradeTaxVoteAssignmentResource = _mapper.Map<TradeTaxVoteAssignment, TradeTaxVoteAssignmentResource>(updatedTradeTaxVoteAssignment);

            return Ok(updatedTradeTaxVoteAssignmentResource);
        }

        [HttpPost]
        [Route("deleteTradeTaxVoteAssignment/{id}")]
        public async Task<IActionResult> DeleteTradeTaxVoteAssignment([FromRoute] int id)
        {
            if (id == 0)
                return BadRequest();

            var tradeTaxVoteAssignment = await _tradeTaxVoteAssignmentService.GetTradeTaxVoteAssignmentById(id);

            if (tradeTaxVoteAssignment == null)
                return NotFound();

            await _tradeTaxVoteAssignmentService.DeleteTradeTaxVoteAssignment(tradeTaxVoteAssignment);

            return NoContent();
        }

        [HttpGet]
        [Route("getAllTradeTaxVoteAssignmentsForSabhaId/{SabhaId}")]
        public async Task<ActionResult> GetAllTradeTaxVoteAssignmentsForSabhaId([FromRoute] int SabhaId)
        {
            var tradeTaxVoteAssignments = await _tradeTaxVoteAssignmentService.GetAllTradeTaxVoteAssignmentsForSabhaId(SabhaId);
            var tradeTaxVoteAssignmentResources = _mapper.Map<IEnumerable<TradeTaxVoteAssignment>, IEnumerable<TradeTaxVoteAssignmentResource>>(tradeTaxVoteAssignments);

            if (tradeTaxVoteAssignmentResources != null && tradeTaxVoteAssignmentResources.Count() > 0)
            {
                var customizedtradeTaxVoteAssignmentList = new List<dynamic>();

                for (int i = 0; i < tradeTaxVoteAssignmentResources.Count(); i++)
                {
                    var tradeTaxVoteAssignmentResource = tradeTaxVoteAssignmentResources.ToList()[i];

                    //var voteDetail = await _voteDetailService.GetVoteDetailById(tradeTaxVoteAssignmentResource.VoteDetailID);
                    //var voteDetailResource = _mapper.Map<VoteDetail, VoteDetailResource>(voteDetail);
                    var voteAssignmentDetail = await _voteAssignmentDetailsService.GetById(tradeTaxVoteAssignmentResource.VoteAssignmentDetailID);
                    var voteAssignmentDetailResource = _mapper.Map<VoteAssignmentDetails, VoteAssignmentDetailsResource>(voteAssignmentDetail);

                    var voteDetail = await _voteDetailService.GetVoteDetailById(voteAssignmentDetailResource.voteAssignment.VoteId);
                    var voteDetailResource = _mapper.Map<VoteDetail, VoteDetailResource>(voteDetail);

                    voteAssignmentDetailResource.voteAssignment.voteDetail = voteDetailResource;

                    var bankAccountDetail = await _accountDetailService.GetAccountDetailById(voteAssignmentDetail.voteAssignment.BankAccountId);
                    var bankAccountDetailResource = _mapper.Map<AccountDetail, AccountDetailResource>(bankAccountDetail);

                    var taxType = await _taxTypeService.GetTaxTypeById(tradeTaxVoteAssignmentResource.TaxTypeID);
                    var taxTypeResource = _mapper.Map<TaxType, TaxTypeResource>(taxType);

                    customizedtradeTaxVoteAssignmentList.Add(new
                    {
                        id = tradeTaxVoteAssignmentResource.ID,
                        taxTypeID = tradeTaxVoteAssignmentResource.TaxTypeID,
                        voteAssignmentDetailID = tradeTaxVoteAssignmentResource.VoteAssignmentDetailID,
                        bankAccountID = voteAssignmentDetail.voteAssignment.BankAccountId,
                        voteAssignmentDetail = voteAssignmentDetailResource,
                        accountDetail = bankAccountDetailResource,
                        taxType = taxTypeResource,
                        activeStatus = tradeTaxVoteAssignmentResource.ActiveStatus,
                        sabhaID = tradeTaxVoteAssignmentResource.SabhaID,
                    });
                }
                return Ok(customizedtradeTaxVoteAssignmentList);
            }

            return Ok("No Data Found");
        }

        [HttpGet]
        [Route("getTradeTaxVoteAssignmentForTaxTypeIDAndSabhaId/{taxTypeID}/{sabhaId}")]
        public async Task<ActionResult> GetTradeTaxVoteAssignmentsForTaxTypeIDAndSabhaId([FromRoute] int taxTypeID, int sabhaId)
        {
            var tradeTaxVoteAssignment = await _tradeTaxVoteAssignmentService.GetTradeTaxVoteAssignmentsForTaxTypeIDAndSabhaId(taxTypeID, sabhaId);
            var tradeTaxVoteAssignmentResource = _mapper.Map<TradeTaxVoteAssignment, TradeTaxVoteAssignmentResource>(tradeTaxVoteAssignment);

            if (tradeTaxVoteAssignmentResource != null && tradeTaxVoteAssignmentResource.ID > 0)
            {
                    var voteAssignmentDetail = await _voteAssignmentDetailsService.GetById(tradeTaxVoteAssignmentResource.VoteAssignmentDetailID);
                    var voteAssignmentDetailResource = _mapper.Map<VoteAssignmentDetails, VoteAssignmentDetailsResource>(voteAssignmentDetail);

                    var voteDetail = await _voteDetailService.GetVoteDetailById(voteAssignmentDetailResource.voteAssignment.VoteId);
                    var voteDetailResource = _mapper.Map<VoteDetail, VoteDetailResource>(voteDetail);

                    voteAssignmentDetailResource.voteAssignment.voteDetail = voteDetailResource;

                    var bankAccountDetail = await _accountDetailService.GetAccountDetailById(voteAssignmentDetail.voteAssignment.BankAccountId);
                    var bankAccountDetailResource = _mapper.Map<AccountDetail, AccountDetailResource>(bankAccountDetail);

                    var taxType = await _taxTypeService.GetTaxTypeById(tradeTaxVoteAssignmentResource.TaxTypeID);
                    var taxTypeResource = _mapper.Map<TaxType, TaxTypeResource>(taxType);

                return Ok(new
                {
                    id = tradeTaxVoteAssignmentResource.ID,
                    taxTypeID = tradeTaxVoteAssignmentResource.TaxTypeID,
                    voteAssignmentDetailID = tradeTaxVoteAssignmentResource.VoteAssignmentDetailID,
                    bankAccountID = voteAssignmentDetail.voteAssignment.BankAccountId,
                    voteAssignmentDetail = voteAssignmentDetailResource,
                    accountDetail = bankAccountDetailResource,
                    taxType = taxTypeResource,
                    activeStatus = tradeTaxVoteAssignmentResource.ActiveStatus,
                    sabhaID = tradeTaxVoteAssignmentResource.SabhaID,
                });
            }

            return Ok("No Data Found");
        }

        [HttpGet]
        [Route("getAllTradeTaxVoteAssignmentsForTaxTypeID/{TaxTypeID}")]
        public async Task<ActionResult<IEnumerable<TradeTaxVoteAssignment>>> GetAllTradeTaxVoteAssignmentsForTaxTypeID([FromRoute] int TaxTypeID)
        {
            var tradeTaxVoteAssignments = await _tradeTaxVoteAssignmentService.GetAllTradeTaxVoteAssignmentsForTaxTypeID(TaxTypeID);

            var tradeTaxVoteAssignmentResources = _mapper.Map<IEnumerable<TradeTaxVoteAssignment>, IEnumerable<TradeTaxVoteAssignmentResource>>(tradeTaxVoteAssignments);

            return Ok(tradeTaxVoteAssignmentResources);
        }
    }
}
