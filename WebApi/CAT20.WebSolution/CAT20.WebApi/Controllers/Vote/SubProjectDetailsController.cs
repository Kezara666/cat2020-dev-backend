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
    [Route("api/vote/subProject")]
    [ApiController]
    public class SubProjectsController : BaseController
    {
        private readonly ISubProjectService _subProjectService;
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;

        public SubProjectsController(ISubProjectService subProjectService, IMapper mapper, IProjectService projectService)
        {
            this._mapper = mapper;
            this._subProjectService = subProjectService;
            this._projectService = projectService;
        }

        [HttpGet("getAllSubProjects")]
        public async Task<ActionResult<IEnumerable<SubProject>>> GetAllSubProjects()
        {
            var subProjects = await _subProjectService.GetAllSubProjects();
            var subProjectResources = _mapper.Map<IEnumerable<SubProject>, IEnumerable<SubProjectResource>>(subProjects);

            return Ok(subProjectResources);
        }

        [HttpGet]
        [Route("getSubProjectById/{id}")]
        public async Task<ActionResult<SubProjectResource>> GetSubProjectById([FromRoute]int id)
        {
            var subProject = await _subProjectService.GetSubProjectById(id);
            var subProjectResource = _mapper.Map<SubProject, SubProjectResource>(subProject);
            return Ok(subProjectResource);
        }

        [HttpPost("saveSubProject")]
        public async Task<ActionResult<SubProjectResource>> CreateSubProject([FromBody] SaveSubProjectResource saveSubProjectResource)
        {
            var validator = new SaveSubProjectResourceValidator();
            var validationResult = await validator.ValidateAsync(saveSubProjectResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var subProjectToCreate = _mapper.Map<SaveSubProjectResource, SubProject>(saveSubProjectResource);

            var newSubProject = await _subProjectService.CreateSubProject(subProjectToCreate);

            var subProject = await _subProjectService.GetSubProjectById(newSubProject.ID);

            var subProjectResource = _mapper.Map<SubProject, SubProjectResource>(subProject);

            return Ok(subProjectResource);
        }

        [HttpPost("updateSubProject")]
        public async Task<ActionResult<SubProjectResource>> UpdateSubProject(SaveSubProjectResource saveSubProjectResource)
        {
            var validator = new SaveSubProjectResourceValidator();
            var validationResult = await validator.ValidateAsync(saveSubProjectResource);

            var requestIsInvalid = saveSubProjectResource.ID == 0 || !validationResult.IsValid;

            if (requestIsInvalid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var subProjectToBeUpdate = await _subProjectService.GetSubProjectById(saveSubProjectResource.ID);

            if (subProjectToBeUpdate == null)
                return NotFound();

            var product = _mapper.Map<SaveSubProjectResource, SubProject>(saveSubProjectResource);

            await _subProjectService.UpdateSubProject(subProjectToBeUpdate, product);

            var updatedSubProject = await _subProjectService.GetSubProjectById(saveSubProjectResource.ID);
            var updatedSubProjectResource = _mapper.Map<SubProject, SubProjectResource>(updatedSubProject);

            return Ok(updatedSubProjectResource);
        }

        [HttpPost]
        [Route("deleteSubProject/{id}")]
        public async Task<IActionResult> DeleteSubProject([FromRoute]int id)
        {
            if (id == 0)
                return BadRequest();

            var subProject = await _subProjectService.GetSubProjectById(id);

            if (subProject == null)
                return NotFound();

            await _subProjectService.DeleteSubProject(subProject);

            return NoContent();
        }

        [HttpGet]
        [Route("getAllSubProjectsForProjectId/{projectid}")]
        public async Task<ActionResult<IEnumerable<SubProject>>> GetAllWithSubProjectByProjectId([FromRoute]int projectid)
        {
            var subProjects = await _subProjectService.GetAllWithSubProjectByProjectId(projectid);
            var subProjectResources = _mapper.Map<IEnumerable<SubProject>, IEnumerable<SubProjectResource>>(subProjects);

            return Ok(subProjectResources);
        }

        [HttpGet()]
        [Route("GetAllWithSubProjectByProjectIdandSabhaId/{projectid}/{SabhaId}")]
        public async Task<ActionResult<IEnumerable<SubProject>>> GetAllWithSubProjectByProjectIdandSabhaId([FromRoute] int ProjectId, [FromRoute]int SabhaId)
        {
            var subProjects = await _subProjectService.GetAllWithSubProjectByProjectIdandSabhaId(ProjectId, SabhaId);
            var subProjectResources = _mapper.Map<IEnumerable<SubProject>, IEnumerable<SubProjectResource>>(subProjects);

            return Ok(subProjectResources);
        }

        [HttpGet()]
        [Route("getAllSubProjectsForSabhaId/{SabhaId}")]
        public async Task<ActionResult<IEnumerable<SubProject>>> GetAllSubProjectsForSabhaId([FromRoute] int SabhaId)
        {
            var subProjects = await _subProjectService.GetAllSubProjectsForSabhaId(SabhaId);
            var subProjectResources = _mapper.Map<IEnumerable<SubProject>, IEnumerable<SubProjectResource>>(subProjects);

            return Ok(subProjectResources);
        }

        [HttpGet()]
        [Route("getAllSubProjectsForProgrammeId/{ProgrammeId}")]
        public async Task<ActionResult<IEnumerable<SubProject>>> getAllSubProjectsForProgrammeId([FromRoute] int ProgrammeId)
        {
            var subProjects = await _subProjectService.GetAllSubProjectsForProgrammeId(ProgrammeId);
            var subProjectResources = _mapper.Map<IEnumerable<SubProject>, IEnumerable<SubProjectResource>>(subProjects);

            return Ok(subProjectResources);
        }

        
    }
}
