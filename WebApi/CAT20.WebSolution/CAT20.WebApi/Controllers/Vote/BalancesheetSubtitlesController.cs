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
using CAT20.WebApi.Resources.Mixin;
using CAT20.Services.Mixin;
using CAT20.Core.Models.Control;

namespace CAT20.Api.Controllers
{
    [Route("api/vote/balancesheetSubtitles")]
    [ApiController]
    public class BalancesheetSubtitlesController : BaseController
    {
        private readonly IBalancesheetSubtitleService _balancesheetSubtitleService;
        private readonly IAccountDetailService _accountDetailService;
        private readonly IMixinOrderLineService _mixinOrderLineService;
        private readonly IMapper _mapper;

        public BalancesheetSubtitlesController(IBalancesheetSubtitleService balancesheetSubtitleService, IMapper mapper, IAccountDetailService accountDetailService, IMixinOrderLineService mixinOrderLineService)
        {
            this._mapper = mapper;
            this._balancesheetSubtitleService = balancesheetSubtitleService;
            _accountDetailService = accountDetailService;
            _mixinOrderLineService = mixinOrderLineService;
        }

        [HttpGet("getAllBalancesheetSubtitles")]
        public async Task<ActionResult<IEnumerable<BalancesheetSubtitle>>> GetAllBalancesheetSubtitles()
        {
            var balancesheetSubtitles = await _balancesheetSubtitleService.GetAllBalancesheetSubtitles();

            foreach (var balsheetsubtitle in balancesheetSubtitles)
            {
                if (balsheetsubtitle.BankAccountID != 0 || balsheetsubtitle.BankAccountID != null)
                {
                    var accountdetail = await _accountDetailService.GetAccountDetailById(balsheetsubtitle.BankAccountID.Value);
                    balsheetsubtitle.accountDetail = accountdetail;
                }
            }

            var balancesheetSubtitleResources = _mapper.Map<IEnumerable<BalancesheetSubtitle>, IEnumerable<BalancesheetSubtitleResource>>(balancesheetSubtitles);

            return Ok(balancesheetSubtitleResources);
        }

        [HttpGet]
        [Route("getBalancesheetSubtitleById/{id}")]
        public async Task<ActionResult<BalancesheetSubtitleResource>> GetBalancesheetSubtitleById([FromRoute] int id)
        {
            var balancesheetSubtitle = await _balancesheetSubtitleService.GetBalancesheetSubtitleById(id);
            var balancesheetSubtitleResource = _mapper.Map<BalancesheetSubtitle, BalancesheetSubtitleResource>(balancesheetSubtitle);
            return Ok(balancesheetSubtitleResource);
        }

        [HttpPost("saveBalancesheetSubtitle")]
        public async Task<ActionResult<BalancesheetSubtitleResource>> CreateBalancesheetSubtitle([FromBody] SaveBalancesheetSubtitleResource saveBalancesheetSubtitleResource)
        {
            var validator = new SaveBalancesheetSubtitleResourceValidator();
            var validationResult = await validator.ValidateAsync(saveBalancesheetSubtitleResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var balancesheetSubtitleToCreate = _mapper.Map<SaveBalancesheetSubtitleResource, BalancesheetSubtitle>(saveBalancesheetSubtitleResource);

            var newBalancesheetSubtitle = await _balancesheetSubtitleService.CreateBalancesheetSubtitle(balancesheetSubtitleToCreate);

            var balancesheetSubtitle = await _balancesheetSubtitleService.GetBalancesheetSubtitleById(newBalancesheetSubtitle.ID);

            var balancesheetSubtitleResource = _mapper.Map<BalancesheetSubtitle, BalancesheetSubtitleResource>(balancesheetSubtitle);

            return Ok(balancesheetSubtitleResource);
        }

        [HttpPost("updateBalancesheetSubtitle")]
        public async Task<ActionResult<BalancesheetSubtitleResource>> UpdateBalancesheetSubtitle(SaveBalancesheetSubtitleResource saveBalancesheetSubtitleResource)
        {
            var validator = new SaveBalancesheetSubtitleResourceValidator();
            var validationResult = await validator.ValidateAsync(saveBalancesheetSubtitleResource);

            var requestIsInvalid = saveBalancesheetSubtitleResource.ID == 0 || !validationResult.IsValid;

            if (requestIsInvalid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var balancesheetSubtitleToBeUpdate = await _balancesheetSubtitleService.GetBalancesheetSubtitleById(saveBalancesheetSubtitleResource.ID);

            if (balancesheetSubtitleToBeUpdate == null)
                return NotFound();

            var product = _mapper.Map<SaveBalancesheetSubtitleResource, BalancesheetSubtitle>(saveBalancesheetSubtitleResource);

            await _balancesheetSubtitleService.UpdateBalancesheetSubtitle(balancesheetSubtitleToBeUpdate, product);

            var updatedBalancesheetSubtitle = await _balancesheetSubtitleService.GetBalancesheetSubtitleById(saveBalancesheetSubtitleResource.ID);
            var updatedBalancesheetSubtitleResource = _mapper.Map<BalancesheetSubtitle, BalancesheetSubtitleResource>(updatedBalancesheetSubtitle);

            return Ok(updatedBalancesheetSubtitleResource);
        }

        [HttpPost]
        [Route("deleteBalancesheetSubtitle/{id}")]
        public async Task<IActionResult> DeleteBalancesheetSubtitle([FromRoute] int id)
        {
            if (id == 0)
                return BadRequest();

            var balancesheetSubtitle = await _balancesheetSubtitleService.GetBalancesheetSubtitleById(id);

            if (balancesheetSubtitle == null)
                return NotFound();

            try
            {
                var mixinOrderLines = await _mixinOrderLineService.GetAllForVoteIdAndVoteorBal(balancesheetSubtitle.ID, 2);
                if (mixinOrderLines != null)
                {
                    return BadRequest();
                }
                else
                {
                    await _balancesheetSubtitleService.DeleteBalancesheetSubtitle(balancesheetSubtitle);
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
        [Route("getAllBalancesheetSubtitlesForSabhaId/{SabhaId}")]
        public async Task<ActionResult<IEnumerable<BalancesheetSubtitle>>> GetAllBalancesheetSubtitlesForSabhaId([FromRoute] int SabhaId)
        {
            var balancesheetSubtitles = await _balancesheetSubtitleService.GetAllBalancesheetSubtitlesForSabhaId(SabhaId);
            var balancesheetSubtitleResources = _mapper.Map<IEnumerable<BalancesheetSubtitle>, IEnumerable<BalancesheetSubtitleResource>>(balancesheetSubtitles);

            return Ok(balancesheetSubtitleResources);
        }


        [HttpGet]
        [Route("getAllBalancesheetSubtitlesForTitleID/{TitleID}")]
        public async Task<ActionResult<IEnumerable<BalancesheetSubtitle>>> GetAllBalancesheetSubtitlesForTitleID([FromRoute] int TitleID)
        {
            var balancesheetSubtitles = await _balancesheetSubtitleService.GetAllBalancesheetSubtitlesForTitleID(TitleID);

            foreach (var balsheetsubtitle in balancesheetSubtitles)
            {
                if (balsheetsubtitle.BankAccountID != 0 || balsheetsubtitle.BankAccountID != null)
                {
                    var accountdetail = await _accountDetailService.GetAccountDetailById(balsheetsubtitle.BankAccountID.Value);
                    balsheetsubtitle.accountDetail = accountdetail;
                }
            }
            var balancesheetSubtitleResources = _mapper.Map<IEnumerable<BalancesheetSubtitle>, IEnumerable<BalancesheetSubtitleResource>>(balancesheetSubtitles);

            return Ok(balancesheetSubtitleResources);
        }

        [HttpGet]
        [Route("getAllBalancesheetSubtitlesForTitleIDAndClassificationIDAndMainLedgerAccountId/{TitleID}/{ClassificationID}/{mainLedgerAccId}/{sabhaId}")]
        public async Task<ActionResult<IEnumerable<BalancesheetSubtitle>>> GetAllBalancesheetSubtitlesForTitleClassificationID([FromRoute] int TitleID, [FromRoute] int ClassificationID, [FromRoute] int mainLedgerAccId, [FromRoute] int sabhaId)
        {
            var chartOfAccountVersionId = int.Parse(HttpContext.User.FindFirst("ChartOfAccountVersionId")!.Value);
            sabhaId = chartOfAccountVersionId switch { 1 => -10, 2 => -20, 3 => -30, _ => sabhaId };

            IEnumerable<BalancesheetSubtitle> balancesheetSubtitles;


            if (ClassificationID != 0 && mainLedgerAccId == 0 && TitleID == 0)
            {
                balancesheetSubtitles = await _balancesheetSubtitleService.GetAllBalancesheetSubtitlesByClassificationID(ClassificationID, sabhaId);
            }
            else if (ClassificationID != 0 && mainLedgerAccId != 0 && TitleID == 0)
            {
                balancesheetSubtitles = await _balancesheetSubtitleService.GetAllBalancesheetSubtitlesByClassificationIDMainLedgerAccountId(ClassificationID, mainLedgerAccId, sabhaId);
            }
            else if (ClassificationID != 0 && mainLedgerAccId != 0 && TitleID != 0)
            {
                balancesheetSubtitles = await _balancesheetSubtitleService.GetAllBalancesheetSubtitlesForTitleID(TitleID);
            }
            else
            {
                balancesheetSubtitles = null;
            }

            foreach (var balsheetsubtitle in balancesheetSubtitles)
            {
                if (balsheetsubtitle.BankAccountID != 0 && balsheetsubtitle.BankAccountID != null)
                {
                    var accountdetail = await _accountDetailService.GetAccountDetailById(balsheetsubtitle.BankAccountID.Value);
                    balsheetsubtitle.accountDetail = accountdetail;
                }
            }

            var balancesheetSubtitleResources = _mapper.Map<IEnumerable<BalancesheetSubtitle>, IEnumerable<BalancesheetSubtitleResource>>(balancesheetSubtitles);

            return Ok(balancesheetSubtitleResources);
        }

        [HttpGet]
        [Route("getAllDepositSubCategoriesForSabha/{sabhaId}")]
        public async Task<ActionResult<IEnumerable<BalancesheetSubtitle>>> GetAllDepositSubCategoriesForSabha([FromRoute] int sabhaId)
        {
            var chartOfAccountVersionId = int.Parse(HttpContext.User.FindFirst("ChartOfAccountVersionId")!.Value);
            sabhaId = chartOfAccountVersionId switch { 1 => -10, 2 => -20, 3 => -30, _ => sabhaId };

            IEnumerable<BalancesheetSubtitle> balancesheetSubtitles;

            balancesheetSubtitles = await _balancesheetSubtitleService.GetAllDepositSubCategoriesForSabha(sabhaId);
 
            var balancesheetSubtitleResources = _mapper.Map<IEnumerable<BalancesheetSubtitle>, IEnumerable<BalancesheetSubtitleResource>>(balancesheetSubtitles);

            return Ok(balancesheetSubtitleResources);
        }


        [HttpGet]
        [Route("getAllBalancesheetSubtitlesForTitleIDAndAccountDetailID/{TitleID}/{AccountDetailID}")]
        public async Task<ActionResult<IEnumerable<BalancesheetSubtitle>>> GetAllBalancesheetSubtitlesForTitleIDAndAccountDetailID([FromRoute] int TitleID, int AccountDetailID)
        {
            var balancesheetSubtitles = await _balancesheetSubtitleService.GetAllBalancesheetSubtitlesForTitleIDAndAccountDetailID(TitleID, AccountDetailID);

            foreach (var balsheetsubtitle in balancesheetSubtitles)
            {
                if (balsheetsubtitle.BankAccountID != 0 || balsheetsubtitle.BankAccountID != null)
                {
                    var accountdetail = await _accountDetailService.GetAccountDetailById(balsheetsubtitle.BankAccountID.Value);
                    balsheetsubtitle.accountDetail = accountdetail;
                }
            }
            var balancesheetSubtitleResources = _mapper.Map<IEnumerable<BalancesheetSubtitle>, IEnumerable<BalancesheetSubtitleResource>>(balancesheetSubtitles);

            return Ok(balancesheetSubtitleResources);
        }

    }
}
