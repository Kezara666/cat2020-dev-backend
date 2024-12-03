using AutoMapper;
using CAT20.Api.Validators;
using CAT20.Core.DTO;
using CAT20.Core.DTO.Final;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Vote;
using CAT20.Core.Services.Control;
using CAT20.Core.Services.Mixin;
using CAT20.Core.Services.Vote;
using CAT20.Services.Control;
using CAT20.WebApi.Controllers.Control;
using CAT20.WebApi.Resources.Control;
using CAT20.WebApi.Resources.Vote;
using CAT20.WebApi.Resources.Vote.Save;
using Microsoft.AspNetCore.Mvc;

namespace CAT20.Api.Controllers
{
    [Route("api/vote/AccountDetails")]
    [ApiController]
    public class AccountDetailsController : BaseController
    {
        private readonly IAccountDetailService _accountDetailService;
        private readonly IVoteDetailService _voteDetailService;
        private readonly IBankDetailService _bankDetailService;
        private readonly IMapper _mapper;
        private readonly IVoteAssignmentService _voteAssignmentService;


        public AccountDetailsController(IAccountDetailService accountDetailService, IVoteDetailService voteDetailService, IBankDetailService bankDetailService, IMapper mapper, IVoteAssignmentService voteAssignmentService)
        {
            this._mapper = mapper;
            this._accountDetailService = accountDetailService;
            _voteDetailService = voteDetailService;
            _bankDetailService = bankDetailService;
            _voteAssignmentService = voteAssignmentService;
        }

        [HttpGet("getAllAccountDetails")]
        public async Task<ActionResult<IEnumerable<AccountDetail>>> GetAllAccountDetails()
        {
            var accountDetails = await _accountDetailService.GetAllAccountDetails();
            var accountDetailResources = _mapper.Map<IEnumerable<AccountDetail>, IEnumerable<AccountDetailResource>>(accountDetails);

            return Ok(accountDetailResources);
        }

        [HttpGet]
        [Route("getAccountDetailById/{id}")]
        public async Task<ActionResult<AccountDetailResource>> GetAccountDetailById([FromRoute] int id)
        {
            var accountDetail = await _accountDetailService.GetAccountDetailById(id);
            var accountDetailResource = _mapper.Map<AccountDetail, AccountDetailResource>(accountDetail);
            if (accountDetailResource != null)
            {
                if (accountDetailResource.BankID != null)
                {
                    var bankDetail = await _bankDetailService.GetBankDetailById(accountDetail.BankID!.Value);
                    accountDetailResource.BankDetail = _mapper.Map<BankDetail, BankDetailResource>(bankDetail);
                }


                if (accountDetailResource.VoteId != null)
                {
                    var voteDetail = await _voteDetailService.GetVoteDetailById(accountDetail.VoteId.Value);
                    accountDetailResource.VoteDetail = _mapper.Map<VoteDetail, VoteDetailLimitedresource>(voteDetail);
                }

            }
            return Ok(accountDetailResource);
        }

        [HttpPost("saveAccountDetail")]
        public async Task<ActionResult<AccountDetailResource>> CreateAccountDetail([FromBody] SaveAccountDetailResource saveAccountDetailResource)
        {
            //var validator = new SaveAccountDetailResourceValidator();
            //var validationResult = await validator.ValidateAsync(saveAccountDetailResource);

            //if (!validationResult.IsValid)
            //    return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var accountDetailToCreate = _mapper.Map<SaveAccountDetailResource, AccountDetail>(saveAccountDetailResource);

            var newAccountDetail = await _accountDetailService.CreateAccountDetail(accountDetailToCreate);

            //var accountDetail = await _accountDetailService.GetAccountDetailById(newAccountDetail.ID);

            var accountDetailResource = _mapper.Map<AccountDetail, AccountDetailResource>(newAccountDetail);

            return Ok(accountDetailResource);
        }

        [HttpPost("updateAccountDetail")]
        public async Task<ActionResult<AccountDetailResource>> UpdateAccountDetail(SaveAccountDetailResource saveAccountDetailResource)
        {
            var validator = new SaveAccountDetailResourceValidator();
            var validationResult = await validator.ValidateAsync(saveAccountDetailResource);

            var requestIsInvalid = saveAccountDetailResource.ID == 0 || !validationResult.IsValid;

            if (requestIsInvalid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var accountDetailToBeUpdate = await _accountDetailService.GetAccountDetailById(saveAccountDetailResource.ID);

            if (accountDetailToBeUpdate == null)
                return NotFound();

            var product = _mapper.Map<SaveAccountDetailResource, AccountDetail>(saveAccountDetailResource);

            await _accountDetailService.UpdateAccountDetail(accountDetailToBeUpdate, product);

            var updatedAccountDetail = await _accountDetailService.GetAccountDetailById(saveAccountDetailResource.ID);
            var updatedAccountDetailResource = _mapper.Map<AccountDetail, AccountDetailResource>(updatedAccountDetail);

            return Ok(updatedAccountDetailResource);
        }

        [HttpPost]
        [Route("deleteAccountDetail/{id}")]
        public async Task<IActionResult> DeleteAccountDetail([FromRoute] int id)
        {
            if (id == 0)
                return BadRequest(new ApiResponseModel<object>
                {
                    Status=400,
                    Message ="Not found"
                });

            var accountDetail = await _accountDetailService.GetAccountDetailById(id);

            if (accountDetail == null)
                return NotFound();

            bool hasVoteAssignments = await _voteAssignmentService.HasVoteAssignmentsForAccountDetailId(id);
            if (accountDetail != null && hasVoteAssignments != true)
            {
                await _accountDetailService.DeleteAccountDetail(accountDetail);
            }
            else
            {
                return BadRequest(new ApiResponseModel<object>
                {
                    Status =400,
                    Message ="you cannot delete this ... already assign votes"
                });
            }
            return NoContent();
        }

        [HttpGet("getAllAccountDetailsForBankId")]
        public async Task<ActionResult<IEnumerable<AccountDetail>>> GetAllWithAccountDetailByBankId(int id)
        {
            var accountDetails = await _accountDetailService.GetAllWithAccountDetailByBankId(id);
            var accountDetailResources = _mapper.Map<IEnumerable<AccountDetail>, IEnumerable<AccountDetailResource>>(accountDetails);

            return Ok(accountDetailResources);
        }

        [HttpGet]
        [Route("getAllAccountDetailsForOfficeId/{id}/")]
        public async Task<ActionResult<IEnumerable<AccountDetail>>> GetAllAccountDetailByOfficeId([FromRoute] int id)
        {
            try
            {
                var accountDetails = await _accountDetailService.GetAllAccountDetailByOfficeId(id);
                var accountDetailResources = _mapper.Map<IEnumerable<AccountDetail>, IEnumerable<AccountDetailResource>>(accountDetails);

                foreach (var accountDetail in accountDetailResources)
                {

                    if(accountDetail.BankID != null )
                    {
                        var bankDetail = await _bankDetailService.GetBankDetailById(accountDetail.BankID!.Value);
                        accountDetail.BankDetail = _mapper.Map<BankDetail, BankDetailResource>(bankDetail);
                    }


                    if(accountDetail.VoteId != null)
                    {
                        var voteDetail = await _voteDetailService.GetVoteDetailById(accountDetail.VoteId.Value);
                        accountDetail.VoteDetail = _mapper.Map<VoteDetail, VoteDetailLimitedresource>(voteDetail);
                    }
                }

            

                return Ok(accountDetailResources);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("getAllAccountDetailsForBankIdandOfficeId/{BankId}/{OfficeId}")]
        public async Task<ActionResult<IEnumerable<AccountDetail>>> GetAllWithAccountDetailByBankIdandOfficeId([FromRoute] int BankId, [FromRoute] int OfficeId)
        {
            var accountDetails = await _accountDetailService.GetAllWithAccountDetailByBankIdandOfficeId(BankId, OfficeId);
            var accountDetailResources = _mapper.Map<IEnumerable<AccountDetail>, IEnumerable<AccountDetailResource>>(accountDetails);

            return Ok(accountDetailResources);
        }
    }
}
