using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CAT20.Api.Validators;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Vote;
using CAT20.Core.Services.Vote;
using CAT20.Services.Vote;
using CAT20.WebApi.Controllers.Control;
using CAT20.WebApi.Resources.Vote;
using CAT20.WebApi.Resources.Vote.Save;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CAT20.Api.Controllers
{
    [Route("api/vote/programmes")]
    [ApiController]
    public class ProgrammesController : BaseController
    {
        private readonly IProgrammeService _programmeService;
        private readonly IIncomeTitleService _incomeTitleService;
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;

        public ProgrammesController(IProgrammeService programmeService, IMapper mapper, IIncomeTitleService incomeTitleService, IProjectService projectService)
        {
            this._mapper = mapper;
            this._programmeService = programmeService;
            _incomeTitleService = incomeTitleService;
            _projectService = projectService;
        }

        [HttpGet("getAllProgrammes")]
        public async Task<ActionResult<IEnumerable<Programme>>> GetAllProgrammes()
        {
            var programmes = await _programmeService.GetAllProgrammes();
            var programmeResources = _mapper.Map<IEnumerable<Programme>, IEnumerable<ProgrammeResource>>(programmes);

            return Ok(programmeResources);
        }
        [HttpGet]
        [Route("getProgrammeById/{id}")]
        public async Task<ActionResult<ProgrammeResource>> GetProgrammeById([FromRoute] int id)
        {
            var programme = await _programmeService.GetProgrammeById(id);
            var programmeResource = _mapper.Map<Programme, ProgrammeResource>(programme);
            return Ok(programmeResource);
        }

        [HttpPost("saveProgramme")]
        public async Task<ActionResult<ProgrammeResource>> CreateProgramme([FromBody] SaveProgrammeResource saveProgrammeResource)
        {
            var validator = new SaveProgrammeResourceValidator();
            var validationResult = await validator.ValidateAsync(saveProgrammeResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var programmeToCreate = _mapper.Map<SaveProgrammeResource, Programme>(saveProgrammeResource);

            var newProgramme = await _programmeService.CreateProgramme(programmeToCreate);

            var programme = await _programmeService.GetProgrammeById(newProgramme.ID);

            var programmeResource = _mapper.Map<Programme, ProgrammeResource>(programme);

            return Ok(programmeResource);
        }

        [HttpPost]
        [Route("updateProgramme")]
        public async Task<ActionResult<ProgrammeResource>> UpdateProgramme(SaveProgrammeResource saveProgrammeResource)
        {
            var validator = new SaveProgrammeResourceValidator();
            var validationResult = await validator.ValidateAsync(saveProgrammeResource);

            var requestIsInvalid = saveProgrammeResource.ID == 0 || !validationResult.IsValid;

            if (requestIsInvalid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var programmeToBeUpdate = await _programmeService.GetProgrammeById(saveProgrammeResource.ID);

            if (programmeToBeUpdate == null)
                return NotFound();

            var programme = _mapper.Map<SaveProgrammeResource, Programme>(saveProgrammeResource);

            await _programmeService.UpdateProgramme(programmeToBeUpdate, programme);

            var updatedProgramme = await _programmeService.GetProgrammeById(saveProgrammeResource.ID);
            var updatedProgrammeResource = _mapper.Map<Programme, ProgrammeResource>(updatedProgramme);

            return Ok(updatedProgrammeResource);
        }

        [HttpPost]
        [Route("updateProgramme2/{id}")]
        public async Task<ActionResult<ProgrammeResource>> UpdateProgramme2([FromRoute]int id, [FromBody]SaveProgrammeResource saveProgrammeResource)
        {
            var validator = new SaveProgrammeResourceValidator();
            var validationResult = await validator.ValidateAsync(saveProgrammeResource);

            var requestIsInvalid = id == 0 || !validationResult.IsValid;

            if (requestIsInvalid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var programmeToBeUpdate = await _programmeService.GetProgrammeById(id);

            if (programmeToBeUpdate == null)
                return NotFound();

            var programme = _mapper.Map<SaveProgrammeResource, Programme>(saveProgrammeResource);

            await _programmeService.UpdateProgramme(programmeToBeUpdate, programme);

            var updatedProgramme = await _programmeService.GetProgrammeById(id);
            var updatedProgrammeResource = _mapper.Map<Programme, ProgrammeResource>(updatedProgramme);

            return Ok(updatedProgrammeResource);
        }

        //[HttpPost]
        //[Route("deleteProgramme/{id}")]
        //public async Task<IActionResult> DeleteProgramme([FromRoute] int id)
        //{
        //    if (id == 0)
        //        return BadRequest();

        //    var programme = await _programmeService.GetProgrammeById(id);

        //    if (programme == null)
        //        return NotFound();

        //    await _programmeService.DeleteProgramme(programme);

        //    return NoContent();
        //}

        [HttpPost]
        [Route("deleteProgramme/{id}")]
        public async Task<IActionResult> DeleteProgramme(int id)
        {
            if (id == 0)
                return BadRequest();

            var programme = await _programmeService.GetProgrammeById(id);

            if (programme == null)
                return NotFound();

            var incomeTitles = await _incomeTitleService.GetAllWithIncomeTitleByProgrammeId(programme.ID);
            var projects = await _projectService.GetAllProjectsForProgrammeId(programme.ID);

            if ((incomeTitles.Count() == 0) && (projects.Count() == 0))
            {
                await _programmeService.DeleteProgramme(programme);
                return NoContent();
            }
            else
            { 
                return Ok("Please remove all child records first..!" + "Income Titles count:" + incomeTitles.Count().ToString() + "Projects count:" + projects.Count().ToString()) ;
            }
        }

        //[HttpGet]
        //[Route("getAllProgrammesForSabhaId/{sabhaid}")]
        //public async Task<ActionResult<IEnumerable<Programme>>> GetAllProgrammesForSabhaId([FromRoute]int sabhaid)
        //{
        //    var programmes = await _programmeService.GetAllProgrammesForSabhaId(sabhaid);
        //    var programmeResources = _mapper.Map<IEnumerable<Programme>, IEnumerable<ProgrammeResource>>(programmes);

        //    return Ok(programmeResources);
        //}

        [HttpGet]
        [Route("getAllProgrammesForSabhaId/{sabhaid}")]
        public async Task<ActionResult<IEnumerable<Programme>>> GetAllProgrammesForSabhaId([FromRoute] int sabhaid)
        {
            var chartOfAccountVersionId = int.Parse(HttpContext.User.FindFirst("ChartOfAccountVersionId")!.Value);

            sabhaid = chartOfAccountVersionId switch { 1 => -10, 2 => -20, 3 => -30, _ => sabhaid };

            var programmes = await _programmeService.GetAllProgrammesForSabhaId(sabhaid);
            var programmeResources = _mapper.Map<IEnumerable<Programme>, IEnumerable<ProgrammeResource>>(programmes);

            return Ok(programmeResources);
        }

    }
}
