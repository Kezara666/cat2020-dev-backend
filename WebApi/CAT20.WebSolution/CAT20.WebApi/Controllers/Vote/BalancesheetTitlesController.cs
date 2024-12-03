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
using CAT20.Services.Vote;
using CAT20.WebApi.Controllers.Control;
using CAT20.Core.Models.Control;

namespace CAT20.Api.Controllers
{
    [Route("api/vote/balancesheetTitles")]
    [ApiController]
    public class BalancesheetTitlesController : BaseController
    {
        private readonly IBalancesheetTitleService _balancesheetTitleService;
        private readonly IBalancesheetSubtitleService _balancesheetSubtitleService;
        private readonly IMapper _mapper;
        private readonly ILogger<BalancesheetTitlesController> _logger;

        public BalancesheetTitlesController(IBalancesheetTitleService balancesheetTitleService, IMapper mapper, IBalancesheetSubtitleService balancesheetSubtitleService, ILogger<BalancesheetTitlesController> logger)
        {
            this._mapper = mapper;
            this._balancesheetTitleService = balancesheetTitleService;
            _balancesheetSubtitleService = balancesheetSubtitleService;
            _logger = logger;
            _logger.LogInformation("balancesheetTitles controller called ");
        }

        [HttpGet("getAllBalancesheetTitles")]
        public async Task<ActionResult<IEnumerable<BalancesheetTitle>>> GetAllBalancesheetTitles()
        {
            _logger.LogInformation("getAllBalancesheetTitles method Starting..");
            var balancesheetTitles = await _balancesheetTitleService.GetAllBalancesheetTitles();
            var balancesheetTitleResources = _mapper.Map<IEnumerable<BalancesheetTitle>, IEnumerable<BalancesheetTitleResource>>(balancesheetTitles);

            return Ok(balancesheetTitleResources);
        }
        [HttpGet]
        [Route("getBalancesheetTitleById/{id}")]
        public async Task<ActionResult<BalancesheetTitleResource>> GetBalancesheetTitleById(int id)
        {
            var balancesheetTitle = await _balancesheetTitleService.GetBalancesheetTitleById(id);
            var balancesheetTitleResource = _mapper.Map<BalancesheetTitle, BalancesheetTitleResource>(balancesheetTitle);
            return Ok(balancesheetTitleResource);
        }

        [HttpPost("saveBalancesheetTitle")]
        public async Task<ActionResult<BalancesheetTitleResource>> CreateBalancesheetTitle([FromBody] SaveBalancesheetTitleResource saveBalancesheetTitleResource)
        {
            var validator = new SaveBalancesheetTitleResourceValidator();
            var validationResult = await validator.ValidateAsync(saveBalancesheetTitleResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var balancesheetTitleToCreate = _mapper.Map<SaveBalancesheetTitleResource, BalancesheetTitle>(saveBalancesheetTitleResource);

            var newBalancesheetTitle = await _balancesheetTitleService.CreateBalancesheetTitle(balancesheetTitleToCreate);

            var balancesheetTitle = await _balancesheetTitleService.GetBalancesheetTitleById(newBalancesheetTitle.ID);

            var balancesheetTitleResource = _mapper.Map<BalancesheetTitle, BalancesheetTitleResource>(balancesheetTitle);

            return Ok(balancesheetTitleResource);
        }

        [HttpPost("updateBalancesheetTitle")]
        public async Task<ActionResult<BalancesheetTitleResource>> UpdateBalancesheetTitle(SaveBalancesheetTitleResource saveBalancesheetTitleResource)
        {
            var validator = new SaveBalancesheetTitleResourceValidator();
            var validationResult = await validator.ValidateAsync(saveBalancesheetTitleResource);

            var requestIsInvalid = saveBalancesheetTitleResource.ID == 0 || !validationResult.IsValid;

            if (requestIsInvalid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var balancesheetTitleToBeUpdate = await _balancesheetTitleService.GetBalancesheetTitleById(saveBalancesheetTitleResource.ID);

            if (balancesheetTitleToBeUpdate == null)
                return NotFound();

            var product = _mapper.Map<SaveBalancesheetTitleResource, BalancesheetTitle>(saveBalancesheetTitleResource);

            await _balancesheetTitleService.UpdateBalancesheetTitle(balancesheetTitleToBeUpdate, product);

            var updatedBalancesheetTitle = await _balancesheetTitleService.GetBalancesheetTitleById(saveBalancesheetTitleResource.ID);
            var updatedBalancesheetTitleResource = _mapper.Map<BalancesheetTitle, BalancesheetTitleResource>(updatedBalancesheetTitle);

            return Ok(updatedBalancesheetTitleResource);
        }

        [HttpPost]
        [Route("deleteBalancesheetTitle/{id}")]
        public async Task<IActionResult> DeleteBalancesheetTitle(int id)
        {
            if (id == 0)
                return BadRequest();

            var balancesheetTitle = await _balancesheetTitleService.GetBalancesheetTitleById(id);

            if (balancesheetTitle == null)
                return NotFound();

            var balancesheetSubtitle = (await _balancesheetSubtitleService.GetAllBalancesheetSubtitlesForTitleID(balancesheetTitle.ID)).Count();

            if (balancesheetSubtitle > 0)
                return BadRequest();

            await _balancesheetTitleService.DeleteBalancesheetTitle(balancesheetTitle);

            return NoContent();
        }

        [HttpGet]
        [Route("getAllBalancesheetTitlesForSabhaId/{SabhaId}")]
        public async Task<ActionResult<IEnumerable<BalancesheetTitle>>> GetAllBalancesheetTitleBySabhaId([FromRoute] int SabhaId)
        {
            var balancesheetTitles = await _balancesheetTitleService.GetAllBalancesheetTitleBySabhaId(SabhaId);
            var balancesheetTitleResources = _mapper.Map<IEnumerable<BalancesheetTitle>, IEnumerable<BalancesheetTitleResource>>(balancesheetTitles);

            return Ok(balancesheetTitleResources);
        }

        [HttpGet]
        [Route("getAllBalancesheetTitlesForAccountDetailId/{AccountDetailId}")]
        public async Task<ActionResult<IEnumerable<BalancesheetTitle>>> GetAllWithBalancesheetTitleByAccountDetailId([FromRoute] int AccountDetailId)
        {
            var balancesheetTitles = await _balancesheetTitleService.GetAllWithBalancesheetTitleByAccountDetailId(AccountDetailId);
            var balancesheetTitleResources = _mapper.Map<IEnumerable<BalancesheetTitle>, IEnumerable<BalancesheetTitleResource>>(balancesheetTitles);

            return Ok(balancesheetTitleResources);
        }

        [HttpGet]
        [Route("getAllBalancesheetTitlesByClassificationMainLedgerAccountId/{ClassificationId}/{MainLedgerAccountId}/{sabhaid}")]
        public async Task<ActionResult<IEnumerable<BalancesheetTitle>>> GetAllWithBalancesheetTitleByClassificationCategoryId([FromRoute] int ClassificationId, [FromRoute] int MainLedgerAccountId, [FromRoute] int sabhaid)
        {
            var chartOfAccountVersionId = int.Parse(HttpContext.User.FindFirst("ChartOfAccountVersionId")!.Value);
            sabhaid = chartOfAccountVersionId switch { 1 => -10, 2 => -20, 3 => -30, _ => sabhaid };

            IEnumerable<BalancesheetTitle> balancesheetTitles;
            if (ClassificationId != 0 && MainLedgerAccountId == 0)
            {
                balancesheetTitles = await _balancesheetTitleService.GetAllWithBalancesheetTitleByClassificationId(ClassificationId, sabhaid);
            }
            else if (ClassificationId != 0 && MainLedgerAccountId != 0)
            {
                balancesheetTitles = await _balancesheetTitleService.GetAllWithBalancesheetTitleByClassificationCategoryId(ClassificationId, MainLedgerAccountId, sabhaid);
            }
            else
            {
                balancesheetTitles = null;
            }

            var balancesheetTitleResources = _mapper.Map<IEnumerable<BalancesheetTitle>, IEnumerable<BalancesheetTitleResource>>(balancesheetTitles);

            return Ok(balancesheetTitleResources);
        }

        //[HttpGet]
        //[Route("getAllBalancesheetTitlesForClassificationIdSabhaId/{ClassificationId}/{SabhaId}")]
        //public async Task<ActionResult<IEnumerable<BalancesheetTitle>>> GetAllWithBalancesheetTitleByClassificationCategorySabhaId([FromRoute] int ClassificationId , [FromRoute] int SabhaId)
        //{

        //       var  balancesheetTitles = await _balancesheetTitleService.GetAllWithBalancesheetTitleByClassificationCategorySabhaId(ClassificationId, SabhaId);


        //    var balancesheetTitleResources = _mapper.Map<IEnumerable<BalancesheetTitle>, IEnumerable<BalancesheetTitleResource>>(balancesheetTitles);

        //    return Ok(balancesheetTitleResources);
        //}

    }
}
