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
using CAT20.WebApi;
using CAT20.WebApi.Controllers.Control;
using CAT20.Core.Models.Control;

namespace CAT20.Api.Controllers
{
    [Route("api/vote/incomeTitle")]
    [ApiController]
    public class IncomeTitlesController : BaseController
    {
        private readonly IIncomeTitleService _incomeTitleService;
        private readonly IIncomeSubtitleService _incomeSubtitleService;
        private readonly IMapper _mapper;

        public IncomeTitlesController(IIncomeTitleService incomeTitleService, IMapper mapper, IIncomeSubtitleService incomeSubtitleService)
        {
            this._mapper = mapper;
            this._incomeTitleService = incomeTitleService;
            _incomeSubtitleService = incomeSubtitleService;
        }

        [HttpGet("getAllIncomeTitles")]
        public async Task<ActionResult<IEnumerable<IncomeTitle>>> GetAllIncomeTitles()
        {
            var incomeTitles = await _incomeTitleService.GetAllIncomeTitles();
            var incomeTitleResources = _mapper.Map<IEnumerable<IncomeTitle>, IEnumerable<IncomeTitleResource>>(incomeTitles);

            return Ok(incomeTitleResources);
        }
        [HttpGet]
        [Route("getIncomeTitleById/{id}")]
        public async Task<ActionResult<IncomeTitleResource>> GetIncomeTitleById([FromRoute] int id)
        {
            var incomeTitle = await _incomeTitleService.GetIncomeTitleById(id);
            var incomeTitleResource = _mapper.Map<IncomeTitle, IncomeTitleResource>(incomeTitle);
            return Ok(incomeTitleResource);
        }

        [HttpPost("saveIncomeTitle")]
        public async Task<ActionResult<IncomeTitleResource>> CreateIncomeTitle(SaveIncomeTitleResource saveIncomeTitleResource)
        {
            var validator = new SaveIncomeTitleResourceValidator();
            var validationResult = await validator.ValidateAsync(saveIncomeTitleResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var incomeTitleToCreate = _mapper.Map<SaveIncomeTitleResource, IncomeTitle>(saveIncomeTitleResource);

            var newIncomeTitle = await _incomeTitleService.CreateIncomeTitle(incomeTitleToCreate);

            //var incomeTitle = await _incomeTitleService.GetIncomeTitleById(newIncomeTitle.ID);

            var incomeTitleResource = _mapper.Map<IncomeTitle, IncomeTitleResource>(newIncomeTitle);

            return Ok(incomeTitleResource);
        }

        [HttpPost("updateIncomeTitle")]
        public async Task<ActionResult<IncomeTitleResource>> UpdateIncomeTitle(SaveIncomeTitleResource saveIncomeTitleResource)
        {
            var validator = new SaveIncomeTitleResourceValidator();
            var validationResult = await validator.ValidateAsync(saveIncomeTitleResource);

            var requestIsInvalid = saveIncomeTitleResource.ID == 0 || !validationResult.IsValid;

            if (requestIsInvalid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var incomeTitleToBeUpdate = await _incomeTitleService.GetIncomeTitleById(saveIncomeTitleResource.ID);

            if (incomeTitleToBeUpdate == null)
                return NotFound();

            var product = _mapper.Map<SaveIncomeTitleResource, IncomeTitle>(saveIncomeTitleResource);

            await _incomeTitleService.UpdateIncomeTitle(incomeTitleToBeUpdate, product);

            var updatedIncomeTitle = await _incomeTitleService.GetIncomeTitleById(saveIncomeTitleResource.ID);
            var updatedIncomeTitleResource = _mapper.Map<IncomeTitle, IncomeTitleResource>(updatedIncomeTitle);

            return Ok(updatedIncomeTitleResource);
        }

        [HttpPost]
        [Route("deleteIncomeTitle/{id}")]
        public async Task<IActionResult> DeleteIncomeTitle([FromRoute] int id)
        {
            if (id == 0)
                return BadRequest();

            var incomeTitle = await _incomeTitleService.GetIncomeTitleById(id);

            if (incomeTitle == null)
                return NotFound();

            var incomeSubtitles = await _incomeSubtitleService.GetAllIncomeSubTitlesForTitleId(incomeTitle.ID);

            if ((incomeSubtitles.Count() == 0))
            {
                await _incomeTitleService.DeleteIncomeTitle(incomeTitle);
                return NoContent();
            }
            else
            {
                return Ok("Please remove all child records first..!" + " Income Titles count: " + incomeSubtitles.Count().ToString());
            }
        }

        [HttpGet]
        [Route("getAllWithIncomeTitleByProgrammeId/{programmeid}")]
        public async Task<ActionResult<IEnumerable<IncomeTitle>>> GetAllWithIncomeTitleByProgrammeId([FromRoute] int programmeid)
        {
            var incomeTitles = await _incomeTitleService.GetAllWithIncomeTitleByProgrammeId(programmeid);
            var incomeTitleResources = _mapper.Map<IEnumerable<IncomeTitle>, IEnumerable<IncomeTitleResource>>(incomeTitles);

            return Ok(incomeTitleResources);
        }
        [HttpGet]
        [Route("getAllIncometitleByProgrammeClassificationMainLedgerAccountID/{programmeid}/{classificationid}/{mainLedgerAccountID}/{sabhaId}")]
        public async Task<ActionResult<IEnumerable<IncomeTitle>>> GetAllWithIncomeTitleByProgrammeClassificationCategoryId([FromRoute] int programmeid, [FromRoute] int classificationid, [FromRoute] int mainLedgerAccountID, int sabhaId)
        {
            var chartOfAccountVersionId = int.Parse(HttpContext.User.FindFirst("ChartOfAccountVersionId")!.Value);
            sabhaId = chartOfAccountVersionId switch { 1 => -10, 2 => -20, 3 => -30, _ => sabhaId };

            IEnumerable<IncomeTitle> incomeTitles;
            
            incomeTitles = await _incomeTitleService.GetAllWithIncomeTitleByProgrammeClassificationMainLedgerAccountId(0, classificationid, mainLedgerAccountID, sabhaId);

            var incomeTitleResources = _mapper.Map<IEnumerable<IncomeTitle>, IEnumerable<IncomeTitleResource>>(incomeTitles);

            return Ok(incomeTitleResources);
        }


        [HttpGet]
        [Route("getAllWithIncomeTitleByProgrammeIdandSabhaId/{programmeid}/{SabhaId}")]
        public async Task<ActionResult<IEnumerable<IncomeTitle>>> GetAllWithIncomeTitleByProgrammeIdandSabhaId([FromRoute] int ProgrammeId, [FromRoute] int SabhaId)
        {
            var incomeTitles = await _incomeTitleService.GetAllWithIncomeTitleByProgrammeIdandSabhaId(ProgrammeId, SabhaId);
            var incomeTitleResources = _mapper.Map<IEnumerable<IncomeTitle>, IEnumerable<IncomeTitleResource>>(incomeTitles);

            return Ok(incomeTitleResources);
        }

        [HttpGet]
        [Route("getAllIncomeTitlesForSabhaId/{SabhaId}")]
        public async Task<ActionResult<IEnumerable<IncomeTitle>>> GetAllIncomeTitlesForSabhaId([FromRoute] int SabhaId)
        {
            var incomeTitles = await _incomeTitleService.GetAllIncomeTitlesForSabhaId(SabhaId);
            var incomeTitleResources = _mapper.Map<IEnumerable<IncomeTitle>, IEnumerable<IncomeTitleResource>>(incomeTitles);

            return Ok(incomeTitleResources);
        }
        //[HttpGet]
        //[Route("getAllIncomeTitlesForClassificationIdSabhaId/{ClassificationId}/{SabhaId}")]
        //public async Task<ActionResult<IEnumerable<IncomeTitle>>> GetAllIncomeTitlesForClassificationIdSabhaId([FromRoute] int ClassificationId, [FromRoute] int SabhaId)
        //{
        //    var incomeTitles = await _incomeTitleService.GetAllIncomeTitlesForClassificationIdSabhaId(ClassificationId, SabhaId);
        //    var incomeTitleResources = _mapper.Map<IEnumerable<IncomeTitle>, IEnumerable<IncomeTitleResource>>(incomeTitles);

        //    return Ok(incomeTitleResources);
        //}
    }
}
