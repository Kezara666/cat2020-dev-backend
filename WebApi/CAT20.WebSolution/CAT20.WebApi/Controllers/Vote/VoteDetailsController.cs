using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CAT20.Api.Validators;
using CAT20.Core.Models.Vote;
using CAT20.Core.Models.Mixin;
using CAT20.WebApi.Resources.Vote.Save;
using CAT20.WebApi.Resources.Vote;
using CAT20.Core.Services.Vote;
using CAT20.Core.Services.Mixin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CAT20.WebApi.Controllers.Control;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.Control;
using CAT20.WebApi.Resources.Mixin;

namespace CAT20.Api.Controllers
{
    [Route("api/vote/voteDetail")]
    [ApiController]
    public class VoteDetailsController : BaseController
    {
        private readonly IVoteDetailService _voteDetailService;
        private readonly IVoteAssignmentService _voteAssignmentService;
        private readonly IMapper _mapper;

        public VoteDetailsController(IVoteDetailService voteDetailService, IMapper mapper, IVoteAssignmentService voteAssignmentService)
        {
            this._mapper = mapper;
            this._voteDetailService = voteDetailService;
            _voteAssignmentService = voteAssignmentService;
        }

        //[HttpGet("getAllVoteDetail")]
        //public async Task<ActionResult<IEnumerable<VoteDetail>>> GetAllVoteDetail()
        //{
        //    var voteDetails = await _voteDetailService.GetAllVoteDetails();
        //    var voteDetailResources = _mapper.Map<IEnumerable<VoteDetail>, IEnumerable<VoteDetailResource>>(voteDetails);

        //    // Sort the voteDetailResources by Code in ascending order
        //    var sortedVoteDetailResources = voteDetailResources.OrderBy(v => v.Code).ToList();

        //    return Ok(sortedVoteDetailResources);
        //}

        [HttpGet]
        [Route("getVoteDetailById/{id}")]
        public async Task<ActionResult<VoteDetailResource>> GetVoteDetailById([FromRoute] int id)
        {
            var voteDetail = await _voteDetailService.GetVoteDetailById(id);
            var voteDetailResource = _mapper.Map<VoteDetail, VoteDetailResource>(voteDetail);
            return Ok(voteDetailResource);
        }

        [HttpPost("saveVoteDetail")]
        public async Task<ActionResult<VoteDetailResource>> CreateVoteDetail([FromBody] SaveVoteDetailResource saveVoteDetailResource)
        {
            try
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


                var validator = new SaveVoteDetailResourceValidator();
                var validationResult = await validator.ValidateAsync(saveVoteDetailResource);

                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

                var voteDetailToCreate = _mapper.Map<SaveVoteDetailResource, VoteDetail>(saveVoteDetailResource);
                voteDetailToCreate.ID = null;
                var result = await _voteDetailService.CreateVoteDetail(voteDetailToCreate, _token);

                //var voteDetail = await _voteDetailService.GetVoteDetailById(newVoteDetail.ID);

                var voteDetailResource = _mapper.Map<VoteDetail, VoteDetailResource>(result.Item3);

                return Ok(voteDetailResource);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("updateVoteDetail")]
        public async Task<ActionResult<VoteDetailResource>> UpdateVoteDetail(SaveVoteDetailResource saveVoteDetailResource)
        {
            try
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


                if (_token.IsFinalAccountsEnabled == 1)
                {
                    return BadRequest();
                }


                var validator = new SaveVoteDetailResourceValidator();
                var validationResult = await validator.ValidateAsync(saveVoteDetailResource);

                var requestIsInvalid = saveVoteDetailResource.ID == 0 || !validationResult.IsValid;

                if (requestIsInvalid)
                    return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

                var voteDetailToBeUpdate = await _voteDetailService.GetVoteDetailById(saveVoteDetailResource.ID.Value);

                if (voteDetailToBeUpdate == null)
                    return NotFound();

                var product = _mapper.Map<SaveVoteDetailResource, VoteDetail>(saveVoteDetailResource);

                await _voteDetailService.UpdateVoteDetail(voteDetailToBeUpdate, product);

                var updatedVoteDetail = await _voteDetailService.GetVoteDetailById(saveVoteDetailResource.ID.Value);
                var updatedVoteDetailResource = _mapper.Map<VoteDetail, VoteDetailResource>(updatedVoteDetail);

                return Ok(updatedVoteDetailResource);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("deleteVoteDetail/{id}")]
        public async Task<IActionResult> DeleteVoteDetail([FromRoute] int id)
        {
            if (id == 0)
                return BadRequest();

            var voteDetail = await _voteDetailService.GetVoteDetailById(id);

            if (voteDetail == null)
                return NotFound();

            try
            {
                bool hasvoteAssignments = await _voteAssignmentService.HasVoteAssignmentsForVoteId(voteDetail.ID.Value);

                if (hasvoteAssignments == true)
                {
                    return BadRequest();
                }
                else
                {
                    await _voteDetailService.DeleteVoteDetail(voteDetail);
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("getAllVoteDetailForSabhaId/{sabhaId}")]
        public async Task<ActionResult<IEnumerable<VoteDetail>>> GetAllWithVoteDetailBySabhaId([FromRoute] int sabhaId)
        {
            var voteDetails = await _voteDetailService.GetAllWithVoteDetailBySabhaId(sabhaId);
            var voteDetailResources = _mapper.Map<IEnumerable<VoteDetail>, IEnumerable<VoteDetailResource>>(voteDetails);

            // Sort the voteDetailResources by Code in ascending order
            var sortedVoteDetailResources = voteDetailResources.OrderBy(v => v.Code).ToList();

            return Ok(sortedVoteDetailResources);
        }

        [HttpGet]
        [Route("getAllVoteDetailForProgrammeId/{ProgrammeId}")]
        public async Task<ActionResult<IEnumerable<VoteDetail>>> GetAllVoteDetailForProgrammeId([FromRoute] int ProgrammeId)
        {
            var voteDetails = await _voteDetailService.GetAllWithVoteDetailByProgrammeId(ProgrammeId);
            var voteDetailResources = _mapper.Map<IEnumerable<VoteDetail>, IEnumerable<VoteDetailResource>>(voteDetails);

            // Sort the voteDetailResources by Code in ascending order
            var sortedVoteDetailResources = voteDetailResources.OrderBy(v => v.Code).ToList();

            return Ok(sortedVoteDetailResources);
        }

        [HttpGet]
        [Route("getAllVoteDetailForProgrammeIdandSabhaId/{ProgrammeId}/{SabhaId}")]
        public async Task<ActionResult<IEnumerable<VoteDetail>>> GetAllVoteDetailForProgrammeIdandSabhaId([FromRoute] int ProgrammeId, [FromRoute] int SabhaId)
        {
            var voteDetails = await _voteDetailService.GetAllWithVoteDetailByProgrammeIdandSabhaId(ProgrammeId, SabhaId);
            var voteDetailResources = _mapper.Map<IEnumerable<VoteDetail>, IEnumerable<VoteDetailResource>>(voteDetails);

            // Sort the voteDetailResources by Code in ascending order
            var sortedVoteDetailResources = voteDetailResources.OrderBy(v => v.Code).ToList();

            return Ok(sortedVoteDetailResources);
        }

        [HttpGet]
        [Route("getAllVoteDetailBySabhaId/{SabhaId}")]
        public async Task<ActionResult<IEnumerable<VoteDetail>>> GetAllVoteDetailBySabhaId([FromRoute] int SabhaId)
        {
            var voteDetails = await _voteDetailService.GetAllWithVoteDetailBySabhaId(SabhaId);
            var voteDetailResources = _mapper.Map<IEnumerable<VoteDetail>, IEnumerable<VoteDetailResource>>(voteDetails);

            // Sort the voteDetailResources by Code in ascending order
            var sortedVoteDetailResources = voteDetailResources.OrderBy(v => v.Code).ToList();

            return Ok(sortedVoteDetailResources);
        }

        [HttpGet]
        [Route("getAllVoteDetailsProgammesByClassificationId/{ClassificationId}")]
        public async Task<ActionResult<IEnumerable<VoteDetail>>> getAllVoteDetailsProgammesByClassificationId([FromRoute] int ClassificationId)
        {
            var voteDetails = await _voteDetailService.GetAllVoteDetailsProgammesByClassificationId(ClassificationId);
            var voteDetailResources = _mapper.Map<IEnumerable<VoteDetail>, IEnumerable<VoteDetailResource>>(voteDetails);

            // Sort the voteDetailResources by Code in ascending order
            var sortedVoteDetailResources = voteDetailResources.OrderBy(v => v.Code).ToList();

            return Ok(sortedVoteDetailResources);
        }

        [HttpGet]
        [Route("getAllVoteDetailsByProgrammeIdClassificationIdMainLedgerAccIdLedgerCategoryIdLedgerAccountId/{ProgrammeId}/{ClassificationId}/{MainLedgerAccId}/{LedgerCategoryId}/{LedgerAccountId}/{SabhaId}")]
        public async Task<ActionResult<IEnumerable<VoteDetail>>> getAllVoteDetailsByProgrammeIdClassificationIdMainLedgerAccIdLedgerCategoryIdLedgerAccountId([FromRoute] int ProgrammeId, [FromRoute] int ClassificationId, [FromRoute] int MainLedgerAccId, [FromRoute] int LedgerCategoryId, [FromRoute] int LedgerAccountId, [FromRoute] int SabhaId)
        {
            try
            {
                var _token = new HTokenClaim
                {

                    userId = int.Parse(HttpContext.User.FindFirst("userid")!.Value),
                    sabhaId = int.Parse(HttpContext.User.FindFirst("sabhaId")!.Value),
                    officeId = int.Parse(HttpContext.User.FindFirst("officeID")!.Value),
                };

                var voteDetails = await _voteDetailService.GetAllVoteDetailsByProgrammeIdClassificationIdMainLedgerAccIdLedgerCategoryIdLedgerAccountId(ProgrammeId, ClassificationId, MainLedgerAccId, LedgerCategoryId, LedgerAccountId, SabhaId);
                var voteDetailResources = _mapper.Map<IEnumerable<VoteDetail>, IEnumerable<VoteDetailResource>>(voteDetails);

                foreach (var voteDetail in voteDetailResources)
                {
                    voteDetail.AccountId = await _voteAssignmentService.GetAccountIdByVoteId(voteDetail.ID!.Value, _token);
                }

                // Sort the voteDetailResources by Code in ascending order
                var sortedVoteDetailResources = voteDetailResources.OrderBy(v => v.Code).ToList();

                return Ok(sortedVoteDetailResources);

            }
            catch (Exception ex)
            {
                return BadRequest();

            }

        }

        [HttpGet]
        [Route("GetAllLedgerAccountsForDepositSubCategoryId/{depositSubCategoryId}/{sabhaid}")]
        public async Task<ActionResult<IEnumerable<VoteDetail>>> GetAllLedgerAccountsForDepositSubCategoryId([FromRoute] int depositSubCategoryId, int sabhaid)
        {
            var voteDetails = await _voteDetailService.GetAllLedgerAccountsForDepositSubCategoryId(depositSubCategoryId, sabhaid);
            var voteDetailResources = _mapper.Map<IEnumerable<VoteDetail>, IEnumerable<VoteDetailResource>>(voteDetails);

            // Sort the voteDetailResources by Code in ascending order
            var sortedVoteDetailResources = voteDetailResources.OrderBy(v => v.Code).ToList();

            return Ok(sortedVoteDetailResources);
        }

        [HttpGet]
        [Route("getAllImprestLedgerAccountsForSabhaId/{sabhaid}")]
        public async Task<ActionResult<IEnumerable<VoteDetail>>> GetAllImprestLedgerAccountsForSabhaId([FromRoute] int sabhaid)
        {
            var voteDetails = await _voteDetailService.GetAllImprestLedgerAccountsForSabhaId(sabhaid);
            var voteDetailResources = _mapper.Map<IEnumerable<VoteDetail>, IEnumerable<VoteDetailResource>>(voteDetails);

            // Sort the voteDetailResources by Code in ascending order
            var sortedVoteDetailResources = voteDetailResources.OrderBy(v => v.Code).ToList();

            return Ok(sortedVoteDetailResources);
        }
        //new route final account

        [HttpGet]
        [Route("getLALeggerAccountsForSabhaId/{sabhaid}")]
        public async Task<ActionResult<IEnumerable<VoteDetail>>> GetLAbankLoanLeggerAccountForSabhaId([FromRoute] int sabhaid)
        {
            var voteDetails = await _voteDetailService.GetLAbankLoanLeggerAccountForSabhaId(sabhaid);
            var voteDetailResources = _mapper.Map<IEnumerable<VoteDetail>, IEnumerable<VoteDetailResource>>(voteDetails);

            // Sort the voteDetailResources by Code in ascending order
            var sortedVoteDetailResources = voteDetailResources.OrderBy(v => v.Code).ToList();

            return Ok(sortedVoteDetailResources);
        }

        [HttpGet]
        [Route("getOtherAccountsOpeningBalancessLeggerAccountForSabhaId/{sabhaid}")]
        public async Task<ActionResult<IEnumerable<VoteDetail>>> GetOtherAccountsOpeningBalancessLeggerAccountForSabhaId([FromRoute] int sabhaid)
        {
            var voteDetails = await _voteDetailService.GetOtherAccountsOpeningBalancessLeggerAccountForSabhaId(sabhaid);
            var voteDetailResources = _mapper.Map<IEnumerable<VoteDetail>, IEnumerable<VoteDetailResource>>(voteDetails);

            // Sort the voteDetailResources by Code in ascending order
            var sortedVoteDetailResources = voteDetailResources.OrderBy(v => v.Code).ToList();

            return Ok(sortedVoteDetailResources);
        }

        [HttpGet]
        [Route("getIndustrialCreditorsLeggerAccountForSabhaId/{sabhaid}")]
        public async Task<ActionResult<IEnumerable<VoteDetail>>> GetIndustrialCreditorsLeggerAccountForSabhaId([FromRoute] int sabhaid)
        {
            var voteDetails = await _voteDetailService.GetIndustrialCreditorsLeggerAccountForSabhaId(sabhaid);
            var voteDetailResources = _mapper.Map<IEnumerable<VoteDetail>, IEnumerable<VoteDetailResource>>(voteDetails);

            // Sort the voteDetailResources by Code in ascending order
            var sortedVoteDetailResources = voteDetailResources.OrderBy(v => v.Code).ToList();

            return Ok(sortedVoteDetailResources);
        }

        [HttpGet]
        [Route("GetFixAssests2ForSabhaId/{sabhaid}")]
        public async Task<ActionResult<IEnumerable<VoteDetail>>> GetFixAssests2ForSabhaId([FromRoute] int sabhaid)
        {
            var voteDetails = await _voteDetailService.GetFixAssests2ForSabhaId(sabhaid);
            var voteDetailResources = _mapper.Map<IEnumerable<VoteDetail>, IEnumerable<VoteDetailResource>>(voteDetails);

            // Sort the voteDetailResources by Code in ascending order
            var sortedVoteDetailResources = voteDetailResources.OrderBy(v => v.Code).ToList();

            return Ok(sortedVoteDetailResources);
        }

        [HttpGet]
        [Route("getFixAssestsForSabhaId/{sabhaid}")]
        public async Task<ActionResult<IEnumerable<VoteDetail>>> GetFixAssestsForSabhaId([FromRoute] int sabhaid)
        {
            var voteDetails = await _voteDetailService.GetFixAssestsForSabhaId(sabhaid);
            var voteDetailResources = _mapper.Map<IEnumerable<VoteDetail>, IEnumerable<VoteDetailResource>>(voteDetails);

            // Sort the voteDetailResources by Code in ascending order
            var sortedVoteDetailResources = voteDetailResources.OrderBy(v => v.Code).ToList();

            return Ok(sortedVoteDetailResources);
        }





        [HttpGet]
        [Route("getIndustrialDebitorsLeggerAccountForSabhaId/{sabhaid}")]
        public async Task<ActionResult<IEnumerable<VoteDetail>>> GetIndustrialDebitorsLeggerAccountForSabhaId([FromRoute] int sabhaid)
        {
            var voteDetails = await _voteDetailService.GetIndustrialDebitorsLeggerAccountForSabhaId(sabhaid);
            var voteDetailResources = _mapper.Map<IEnumerable<VoteDetail>, IEnumerable<VoteDetailResource>>(voteDetails);

            // Sort the voteDetailResources by Code in ascending order
            var sortedVoteDetailResources = voteDetailResources.OrderBy(v => v.Code).ToList();

            return Ok(sortedVoteDetailResources);
        }

        [HttpGet]
        [Route("getStoresAdvanceAccountsLeggerAccountForSabhaId/{sabhaid}")]
        public async Task<ActionResult<IEnumerable<VoteDetail>>> GetStoresAdvanceAccountsLeggerAccountForSabhaId([FromRoute] int sabhaid)
        {
            var voteDetails = await _voteDetailService.GetStoresAdvanceAccountsLeggerAccountForSabhaId(sabhaid);
            var voteDetailResources = _mapper.Map<IEnumerable<VoteDetail>, IEnumerable<VoteDetailResource>>(voteDetails);

            // Sort the voteDetailResources by Code in ascending order
            var sortedVoteDetailResources = voteDetailResources.OrderBy(v => v.Code).ToList();

            return Ok(sortedVoteDetailResources);
        }


        [HttpGet]
        [Route("getPrepayableLeggerAccountForSabhaId/{sabhaid}")]
        public async Task<ActionResult<IEnumerable<VoteDetail>>> GetPrepayableLeggerAccountForSabhaId([FromRoute] int sabhaid)
        {
            var voteDetails = await _voteDetailService.GetPrepayableLeggerAccountForSabhaId(sabhaid);
            var voteDetailResources = _mapper.Map<IEnumerable<VoteDetail>, IEnumerable<VoteDetailResource>>(voteDetails);

            // Sort the voteDetailResources by Code in ascending order
            var sortedVoteDetailResources = voteDetailResources.OrderBy(v => v.Code).ToList();

            return Ok(sortedVoteDetailResources);
        }


        [HttpGet]
        [Route("getFixedDepositLeggerAccountForSabhaId/{sabhaid}")]
        public async Task<ActionResult<IEnumerable<VoteDetail>>> GetFixedDepositLeggerAccountForSabhaId([FromRoute] int sabhaid)
        {
            var voteDetails = await _voteDetailService.GetFixedDepositLeggerAccountForSabhaId(sabhaid);
            var voteDetailResources = _mapper.Map<IEnumerable<VoteDetail>, IEnumerable<VoteDetailResource>>(voteDetails);

            // Sort the voteDetailResources by Code in ascending order
            var sortedVoteDetailResources = voteDetailResources.OrderBy(v => v.Code).ToList();

            return Ok(sortedVoteDetailResources);
        }



        [HttpGet]
        [Route("getReceivableExchangeLeggerAccountForSabhaId/{sabhaid}")]
        public async Task<ActionResult<IEnumerable<VoteDetail>>> GetReceivableExchangeLeggerAccountForSabhaId([FromRoute] int sabhaid)
        {
            var voteDetails = await _voteDetailService.GetReceivableExchangeLeggerAccountForSabhaId(sabhaid);
            var voteDetailResources = _mapper.Map<IEnumerable<VoteDetail>, IEnumerable<VoteDetailResource>>(voteDetails);

            // Sort the voteDetailResources by Code in ascending order
            var sortedVoteDetailResources = voteDetailResources.OrderBy(v => v.Code).ToList();

            return Ok(sortedVoteDetailResources);
        }

        [HttpGet]
        [Route("getReceivableNonExchangeLeggerAccountForSabhaId/{sabhaid}")]
        public async Task<ActionResult<IEnumerable<VoteDetail>>> GetReceivableNonExchangeLeggerAccountForSabhaId([FromRoute] int sabhaid)
        {
            var voteDetails = await _voteDetailService.GetReceivableNonExchangeLeggerAccountForSabhaId(sabhaid);
            var voteDetailResources = _mapper.Map<IEnumerable<VoteDetail>, IEnumerable<VoteDetailResource>>(voteDetails);

            // Sort the voteDetailResources by Code in ascending order
            var sortedVoteDetailResources = voteDetailResources.OrderBy(v => v.Code).ToList();

            return Ok(sortedVoteDetailResources);
        }

        //..............................//

        [HttpGet]
        [Route("getAllAccountTransferLedgerAccountsForSabhaId/{sabhaid}")]
        public async Task<ActionResult<IEnumerable<VoteDetail>>> GetAllAccountTransferLedgerAccountsForSabhaId([FromRoute] int sabhaid)
        {
            var voteDetails = await _voteDetailService.GetAllAccountTransferLedgerAccountsForSabhaId(sabhaid);
            var voteDetailResources = _mapper.Map<IEnumerable<VoteDetail>, IEnumerable<VoteDetailResource>>(voteDetails);

            // Sort the voteDetailResources by Code in ascending order
            var sortedVoteDetailResources = voteDetailResources.OrderBy(v => v.Code).ToList();

            return Ok(sortedVoteDetailResources);
        }


        [HttpGet]
        [Route("getAllShopRentalExpectedIncomeAccountsForSabhaId/{sabhaid}")]
        public async Task<ActionResult<IEnumerable<VoteDetail>>> GetAllShopRentalExpectedIncomeAccountsForSabhaId([FromRoute] int sabhaid)
        {
            var voteDetails = await _voteDetailService.GetAllShopRentalExpectedIncomeAccountsForSabhaId(sabhaid);
            var voteDetailResources = _mapper.Map<IEnumerable<VoteDetail>, IEnumerable<VoteDetailResource>>(voteDetails);

            // Sort the voteDetailResources by Code in ascending order
            var sortedVoteDetailResources = voteDetailResources.OrderBy(v => v.Code).ToList();

            return Ok(sortedVoteDetailResources);
        }

        [HttpGet]
        [Route("getPayRollAccountsForSabha/{sabhaid}")]
        public async Task<ActionResult<IEnumerable<VoteDetail>>> getPayRollAccountsForSabha([FromRoute] int sabhaid)
        {
            var voteDetails = await _voteDetailService.GetPayRollAccountsForSabha(sabhaid);
            var voteDetailResources = _mapper.Map<IEnumerable<VoteDetail>, IEnumerable<VoteDetailResource>>(voteDetails);

            // Sort the voteDetailResources by Code in ascending order
            var sortedVoteDetailResources = voteDetailResources.OrderBy(v => v.Code).ToList();

            return Ok(sortedVoteDetailResources);
        }

        [HttpGet]
        [Route("getStoreAdvanceAssetsAccountsForSabha/{sabhaid}")]
        public async Task<ActionResult<IEnumerable<VoteDetail>>> getStoreAdvanceAssetsAccountsForSabha([FromRoute] int sabhaid)
        {
            var voteDetails = await _voteDetailService.GetStoreAdvanceAssetsAccountsForSabha(sabhaid);
            var voteDetailResources = _mapper.Map<IEnumerable<VoteDetail>, IEnumerable<VoteDetailResource>>(voteDetails);

            // Sort the voteDetailResources by Code in ascending order
            var sortedVoteDetailResources = voteDetailResources.OrderBy(v => v.Code).ToList();

            return Ok(sortedVoteDetailResources);
        }

        [HttpGet]
        [Route("getSabhaFundingSourceForIndustrialCreditorsDebtorsTypes/{sabhaId}/{isCreditor}/{isDebtor}/{creditorsDebtorsType}")]
        public async Task<ActionResult<IEnumerable<VoteDetail>>> GetSabhaFundingSourceForIndustrialCreditorsDebtorsTypes(int sabhaId, bool isCreditor, bool isDebtor, int creditorsDebtorsType)
        {
            var _token = new HTokenClaim
            {
                userId = int.Parse(HttpContext.User.FindFirst("userid")!.Value),
                sabhaId = int.Parse(HttpContext.User.FindFirst("sabhaId")!.Value),
                officeId = int.Parse(HttpContext.User.FindFirst("officeID")!.Value),
                IsFinalAccountsEnabled = int.Parse(HttpContext.User.FindFirst("IsFinalAccountsEnabled")!.Value),
                ChartOfAccountVersionId = int.Parse(HttpContext.User.FindFirst("ChartOfAccountVersionId")!.Value),
                officeCode = HttpContext.User.FindFirst("officeCode")!.Value,
                officeTypeID = int.Parse(HttpContext.User.FindFirst("officeTypeID")!.Value),
            };
            if (_token.ChartOfAccountVersionId == 3)
            {
                var voteDetails = await _voteDetailService.GetSabhaFundingSourceForIndustrialCreditorsDebtorsTypes(sabhaId, isCreditor, isDebtor, creditorsDebtorsType);
                var voteDetailResources = _mapper.Map<IEnumerable<VoteDetail>, IEnumerable<VoteDetailResource>>(voteDetails);

                var sortedVoteDetailResources = voteDetailResources.OrderBy(v => v.Code).ToList();

                return Ok(sortedVoteDetailResources);
            }
            else if (_token.ChartOfAccountVersionId == 1 || _token.ChartOfAccountVersionId == 2)
            {
                var voteDetails = await _voteDetailService.GetSabhaFundingSourceForIndustrialCreditorsDebtorsTypesofOLDAccSystem(sabhaId, isCreditor, isDebtor, creditorsDebtorsType);
                var voteDetailResources = _mapper.Map<IEnumerable<VoteDetail>, IEnumerable<VoteDetailResource>>(voteDetails);

                var sortedVoteDetailResources = voteDetailResources.OrderBy(v => v.Code).ToList();

                return Ok(sortedVoteDetailResources);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("getSingleOpeningBalanceLedgerAccount/{sabhaid}")]
        public async Task<ActionResult<IEnumerable<VoteDetail>>> GetSingleOpeningBalanceLedgerAccount([FromRoute] int sabhaid)
        {
            var voteDetails = await _voteDetailService.GetSingleOpeningBalanceLedgerAccount(sabhaid);
            var voteDetailResources = _mapper.Map<IEnumerable<VoteDetail>, IEnumerable<VoteDetailResource>>(voteDetails);

            // Sort the voteDetailResources by Code in ascending order
            var sortedVoteDetailResources = voteDetailResources.OrderBy(v => v.Code).ToList();

            return Ok(sortedVoteDetailResources);
        }

        [HttpGet]
        [Route("getAllLedgerAccountsByOfficeId/{OfficeId}")]
        public async Task<ActionResult<IEnumerable<VoteDetail>>> GetAllLedgerAccountsByOfficeId([FromRoute] int OfficeId)
        {
            var _token = new HTokenClaim
            {
                userId = int.Parse(HttpContext.User.FindFirst("userid")!.Value),
                sabhaId = int.Parse(HttpContext.User.FindFirst("sabhaId")!.Value),
                officeId = int.Parse(HttpContext.User.FindFirst("officeID")!.Value),
            };

            var voteAssignments = await _voteAssignmentService.GetAllForOfficeId(_token.officeId);
            var voteAssignmentsResources = _mapper.Map<IEnumerable<VoteAssignment>, IEnumerable<VoteAssignmentBasicResource>>(voteAssignments);

            var voteDetails = await _voteDetailService.GetAllWithVoteDetailBySabhaId(_token.sabhaId);
            var voteDetailResources = _mapper.Map<IEnumerable<VoteDetail>, IEnumerable<VoteDetailFullResource>>(voteDetails);

            var matchingVoteDetails = voteDetailResources
                .Where(vd => voteAssignmentsResources.Any(va => va.VoteId == vd.ID))
                .OrderBy(v => v.Code) 
                .ToList();

            foreach (var vote in matchingVoteDetails)
            {
                var assignment = voteAssignmentsResources.Where(va => va.VoteId == vote.ID && va.OfficeId== _token.officeId).FirstOrDefault();
                vote.VoteAssignment = assignment;
            }

            return Ok(matchingVoteDetails);
        }

        [HttpGet("getVoteDetailsByFilterValues/{sabhaId}")]
        public async Task<ActionResult<IEnumerable<VoteDetail>>> GetVoteDetailByFilterValues(
            [FromRoute] int sabhaId,
            [FromQuery] int? programmeId,
            [FromQuery] int? classificationId,
            [FromQuery] int? mainLedgerAccId,
            [FromQuery] int? ledgerCategoryId,
            [FromQuery] int? ledgerAccountId,
            [FromQuery] int? subLedgerId,
            [FromQuery] int? projectId,
            [FromQuery] int? subProject
         
            )
        {
            var voteDetails = await _voteDetailService.getVoteDetalByFilterValues(
                programmeId,
                classificationId,
                mainLedgerAccId,
                ledgerCategoryId,
                ledgerAccountId,
                subLedgerId,
                projectId,
                subProject,
                sabhaId);

            var voteDetailResources = _mapper.Map<IEnumerable<VoteDetail>, IEnumerable<VoteDetailResource>>(voteDetails);

            // Sort the voteDetailResources by Code in ascending order
            var sortedVoteDetailResources = voteDetailResources.OrderBy(v => v.Code).ToList();

            return Ok(sortedVoteDetailResources);
        }

        [HttpGet("getVoteDetailsByFilterValuesWithBalanceForComparativeFigure/{sabhaId}")]
        public async Task<ActionResult<IEnumerable<VoteDetail>>> getVoteDetailsByFilterValuesWithBalanceForComparativeFigure(
            [FromRoute] int sabhaId,
            [FromQuery] int? programmeId,
            [FromQuery] int? classificationId,
            [FromQuery] int? mainLedgerAccId,
            [FromQuery] int? ledgerCategoryId,
            [FromQuery] int? ledgerAccountId,
            [FromQuery] int? subLedgerId,
            [FromQuery] int? projectId,
            [FromQuery] int? subProject

            )
        {

            var _token = new HTokenClaim
            {

                userId = int.Parse(HttpContext.User.FindFirst("userid")!.Value),
                sabhaId = int.Parse(HttpContext.User.FindFirst("sabhaId")!.Value),
                officeId = int.Parse(HttpContext.User.FindFirst("officeID")!.Value),
            };

            var voteDetails = await _voteDetailService.getVoteDetailsByFilterValuesWithBalanceForComparativeFigure(
                programmeId,
                classificationId,
                mainLedgerAccId,
                ledgerCategoryId,
                ledgerAccountId,
                subLedgerId,
                projectId,
                subProject,
                sabhaId,
                _token
                );


            var voteDetailResources = _mapper.Map<IEnumerable<VoteDetail>, IEnumerable<VoteDetailResource>>(voteDetails);

            // Sort the voteDetailResources by Code in ascending order
            var sortedVoteDetailResources = voteDetailResources.OrderBy(v => v.Code).ToList();

            return Ok(sortedVoteDetailResources);
        }

    }
}
