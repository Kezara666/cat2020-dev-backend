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

namespace CAT20.Api.Controllers
{
    [Route("api/vote/incomeSubtitle")]
    [ApiController]
    public class IncomeSubtitlesController : BaseController
    {
        private readonly IIncomeSubtitleService _incomeSubtitleService;
        private readonly IVoteDetailService _voteDetailService;
        private readonly IMapper _mapper;

        public IncomeSubtitlesController(IIncomeSubtitleService incomeSubtitleService, IMapper mapper, IVoteDetailService voteDetailService)
        {
            this._mapper = mapper;
            this._incomeSubtitleService = incomeSubtitleService;
            _voteDetailService = voteDetailService;
        }

        [HttpGet("getAllIncomeSubtitles")]
        public async Task<ActionResult<IEnumerable<IncomeSubtitle>>> GetAllIncomeSubtitles()
        {
            var incomeSubtitles = await _incomeSubtitleService.GetAllIncomeSubtitles();
            var incomeSubtitleResources = _mapper.Map<IEnumerable<IncomeSubtitle>, IEnumerable<IncomeSubtitleResource>>(incomeSubtitles);

            return Ok(incomeSubtitleResources);
        }
        [HttpGet]
        [Route("getIncomeSubtitleById/{id}")]
        public async Task<ActionResult<IncomeSubtitleResource>> GetIncomeSubtitleById(int id)
        {
            var incomeSubtitle = await _incomeSubtitleService.GetIncomeSubtitleById(id);
            var incomeSubtitleResource = _mapper.Map<IncomeSubtitle, IncomeSubtitleResource>(incomeSubtitle);
            return Ok(incomeSubtitleResource);
        }

        [HttpPost("saveIncomeSubtitle")]
        public async Task<ActionResult<IncomeSubtitleResource>> CreateIncomeSubtitle([FromBody] SaveIncomeSubtitleResource saveIncomeSubtitleResource)
        {
            var validator = new SaveIncomeSubtitleResourceValidator();
            var validationResult = await validator.ValidateAsync(saveIncomeSubtitleResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var incomeSubtitleToCreate = _mapper.Map<SaveIncomeSubtitleResource, IncomeSubtitle>(saveIncomeSubtitleResource);

            var newIncomeSubtitle = await _incomeSubtitleService.CreateIncomeSubtitle(incomeSubtitleToCreate);

            var incomeSubtitle = await _incomeSubtitleService.GetIncomeSubtitleById(newIncomeSubtitle.ID);

            var incomeSubtitleResource = _mapper.Map<IncomeSubtitle, IncomeSubtitleResource>(incomeSubtitle);

            return Ok(incomeSubtitleResource);
        }

        [HttpPost("updateIncomeSubtitle")]
        public async Task<ActionResult<IncomeSubtitleResource>> UpdateIncomeSubtitle(SaveIncomeSubtitleResource saveIncomeSubtitleResource)
        {
            var validator = new SaveIncomeSubtitleResourceValidator();
            var validationResult = await validator.ValidateAsync(saveIncomeSubtitleResource);

            var requestIsInvalid = saveIncomeSubtitleResource.ID == 0 || !validationResult.IsValid;

            if (requestIsInvalid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var incomeSubtitleToBeUpdate = await _incomeSubtitleService.GetIncomeSubtitleById(saveIncomeSubtitleResource.ID);

            if (incomeSubtitleToBeUpdate == null)
                return NotFound();

            var product = _mapper.Map<SaveIncomeSubtitleResource, IncomeSubtitle>(saveIncomeSubtitleResource);

            await _incomeSubtitleService.UpdateIncomeSubtitle(incomeSubtitleToBeUpdate, product);

            var updatedIncomeSubtitle = await _incomeSubtitleService.GetIncomeSubtitleById(saveIncomeSubtitleResource.ID);
            var updatedIncomeSubtitleResource = _mapper.Map<IncomeSubtitle, IncomeSubtitleResource>(updatedIncomeSubtitle);

            return Ok(updatedIncomeSubtitleResource);
        }

        [HttpPost]
        [Route("deleteIncomeSubtitle/{id}")]
        public async Task<IActionResult> DeleteIncomeSubtitle([FromRoute] int id)
        {
            if (id == 0)
                return BadRequest();

            var incomeSubtitle = await _incomeSubtitleService.GetIncomeSubtitleById(id);

            if (incomeSubtitle == null)
                return NotFound();

            var voteDetail = await _voteDetailService.GetAllWithVoteDetailByIncomeSubTitleId(incomeSubtitle.ID);
            if ((voteDetail.Count() == 0))
            {
                await _incomeSubtitleService.DeleteIncomeSubtitle(incomeSubtitle);
                return NoContent();
            }
            else
            {
                return Ok("Please remove all child records first..!" + " Income Vote Details count: " + voteDetail.Count().ToString());
            }
        }

        [HttpGet]
        [Route("getAllWithIncomeSubtitleByIncometitleId/{IncometitleId}")]
        public async Task<ActionResult<IEnumerable<IncomeSubtitle>>> GetAllWithIncomeSubtitleByIncometitleId([FromRoute] int IncometitleId)
        {
            var incomeSubtitles = await _incomeSubtitleService.GetAllWithIncomeSubtitleByIncometitleId(IncometitleId);
            var incomeSubtitleResources = _mapper.Map<IEnumerable<IncomeSubtitle>, IEnumerable<IncomeSubtitleResource>>(incomeSubtitles);

            return Ok(incomeSubtitleResources);
        }

        [HttpGet]
        [Route("getAllIncomeSubtitlesForIncometitleandIdSabhaId/{IncometitleId}/{SabhaId}")]
        public async Task<ActionResult<IEnumerable<IncomeSubtitle>>> GetAllWithIncomeSubtitleByIncometitleIdSabhaId([FromRoute] int IncometitleId, [FromRoute] int SabhaId)
        {
            var incomeSubtitles = await _incomeSubtitleService.GetAllWithIncomeSubtitleByIncometitleIdSabhaId(IncometitleId, SabhaId);
            var incomeSubtitleResources = _mapper.Map<IEnumerable<IncomeSubtitle>, IEnumerable<IncomeSubtitleResource>>(incomeSubtitles);

            return Ok(incomeSubtitleResources);
        }

        [HttpGet]
        [Route("getAllIncomeSubTitlesForSabhaId/{SabhaId}")]
        public async Task<ActionResult<IEnumerable<IncomeSubtitle>>> GetAllIncomeSubTitlesForSabhaId([FromRoute] int SabhaId)
        {
            var incomeSubtitles = await _incomeSubtitleService.GetAllIncomeSubTitlesForSabhaId(SabhaId);
            var incomeSubtitleResources = _mapper.Map<IEnumerable<IncomeSubtitle>, IEnumerable<IncomeSubtitleResource>>(incomeSubtitles);

            return Ok(incomeSubtitleResources);
        }

        [HttpGet]
        [Route("getAllIncomeSubTitlesForTitleId/{TitleId}")]
        public async Task<ActionResult<IEnumerable<IncomeSubtitle>>> GetAllIncomeSubTitlesForTitleId([FromRoute] int TitleId)
        {
            var incomeSubtitles = await _incomeSubtitleService.GetAllIncomeSubTitlesForTitleId(TitleId);
            var incomeSubtitleResources = _mapper.Map<IEnumerable<IncomeSubtitle>, IEnumerable<IncomeSubtitleResource>>(incomeSubtitles);

            return Ok(incomeSubtitleResources);

        }

        [HttpGet]
        [Route("getAllIncomeSubTitlesForProgrammeId/{ProgrammeId}")]
        public async Task<ActionResult<IEnumerable<IncomeSubtitle>>> GetAllIncomeSubTitlesForProgrammeId([FromRoute] int ProgrammeId)
        {
            var incomeSubtitles = await _incomeSubtitleService.GetAllIncomeSubTitlesForProgrammeId(ProgrammeId);
            var incomeSubtitleResources = _mapper.Map<IEnumerable<IncomeSubtitle>, IEnumerable<IncomeSubtitleResource>>(incomeSubtitles);
            return Ok(incomeSubtitles);
        }



        [HttpGet]
        [Route("getAllIncomeSubTitlesForProgrammeId/{ProgrammeId}/{ClassificationId}")]
        public async Task<ActionResult<IEnumerable<IncomeSubtitle>>> GetAllIncomeSubTitlesForProgrammeClassificationVoteIncomeTitleId([FromRoute] int ProgrammeId, [FromRoute] int ClassificationId)
        {
            var incomeSubtitles = await _incomeSubtitleService.GetAllIncomeSubTitlesForProgrammeClassificationId(0, ClassificationId);
            var incomeSubtitleResources = _mapper.Map<IEnumerable<IncomeSubtitle>, IEnumerable<IncomeSubtitleResource>>(incomeSubtitles);

            return Ok(incomeSubtitleResources);
        }

        [HttpGet]
        [Route("getAllIncomeSubTitlesForTitleIDAndClassificationIDAndMainLedgerAccountId/{ClassificationID}/{mainLedgerAccId}/{TitleID}/{sabhaId}")]
        public async Task<ActionResult<IEnumerable<IncomeSubtitle>>> GetAllIncomeSubTitlesForTitleIDAndClassificationIDAndMainLedgerAccountId([FromRoute] int ClassificationID, [FromRoute] int mainLedgerAccId, [FromRoute] int TitleID, [FromRoute] int sabhaId)
        {
            var chartOfAccountVersionId = int.Parse(HttpContext.User.FindFirst("ChartOfAccountVersionId")!.Value);
            sabhaId = chartOfAccountVersionId switch { 1 => -10, 2 => -20, 3 => -30, _ => sabhaId };

            IEnumerable<IncomeSubtitle> incomeSubtitles;
            incomeSubtitles = await _incomeSubtitleService.GetAllIncomeSubTitlesForTitleIDAndClassificationIDAndMainLedgerAccountId(ClassificationID, mainLedgerAccId, TitleID, sabhaId);
            var incomeSubtitleResources = _mapper.Map<IEnumerable<IncomeSubtitle>, IEnumerable<IncomeSubtitleResource>>(incomeSubtitles);

            return Ok(incomeSubtitleResources);
        }

    }
}
