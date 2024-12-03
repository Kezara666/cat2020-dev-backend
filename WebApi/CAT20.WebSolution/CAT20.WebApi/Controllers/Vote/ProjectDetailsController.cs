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
using CAT20.Core.HelperModels;

namespace CAT20.Api.Controllers
{
    [Route("api/vote/projects")]
    [ApiController]
    public class ProjectsController : BaseController
    {
        private readonly IProjectService _projectService;
        private readonly ISubProjectService _subProjectService;
        private readonly IMapper _mapper;

        public ProjectsController(IProjectService projectService, IMapper mapper, ISubProjectService subProjectService)
        {
            this._mapper = mapper;
            this._projectService = projectService;
            _subProjectService = subProjectService;
        }

        [HttpGet("getAllProjects")]
        public async Task<ActionResult<IEnumerable<Project>>> GetAllProjects()
        {
            var projects = await _projectService.GetAllProjects();
            var projectResources = _mapper.Map<IEnumerable<Project>, IEnumerable<ProjectResource>>(projects);

            return Ok(projectResources);
        }
        [HttpGet]
        [Route("GetProjectById/{id}")]
        public async Task<ActionResult<ProjectResource>> GetProjectById([FromRoute] int id)
        {
            var project = await _projectService.GetProjectById(id);
            var projectResource = _mapper.Map<Project, ProjectResource>(project);
            return Ok(projectResource);
        }

        [HttpPost("saveProject")]
        public async Task<ActionResult<ProjectResource>> CreateProject([FromBody] SaveProjectResource saveProjectResource)
        {
            var validator = new SaveProjectResourceValidator();
            var validationResult = await validator.ValidateAsync(saveProjectResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var projectToCreate = _mapper.Map<SaveProjectResource, Project>(saveProjectResource);

            var newProject = await _projectService.CreateProject(projectToCreate);

            var project = await _projectService.GetProjectById(newProject.ID);

            var projectResource = _mapper.Map<Project, ProjectResource>(project);

            return Ok(projectResource);
        }

        [HttpPost("updateProject")]
        public async Task<ActionResult<ProjectResource>> UpdateProject(SaveProjectResource saveProjectResource)
        {
            var validator = new SaveProjectResourceValidator();
            var validationResult = await validator.ValidateAsync(saveProjectResource);

            var requestIsInvalid = saveProjectResource.ID == 0 || !validationResult.IsValid;

            if (requestIsInvalid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var projectToBeUpdate = await _projectService.GetProjectById(saveProjectResource.ID);

            if (projectToBeUpdate == null)
                return NotFound();

            var project = _mapper.Map<SaveProjectResource, Project>(saveProjectResource);

            await _projectService.UpdateProject(projectToBeUpdate, project);

            var updatedProject = await _projectService.GetProjectById(saveProjectResource.ID);
            var updatedProjectResource = _mapper.Map<Project, ProjectResource>(updatedProject);

            return Ok(updatedProjectResource);
        }

        [HttpPost]
        [Route("deleteProject/{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            if (id == 0)
                return BadRequest();

            var project = await _projectService.GetProjectById(id);

            if (project == null)
                return NotFound();

            var subProjects = await _subProjectService.GetAllWithSubProjectByProjectId(project.ID);

            if ((subProjects.Count() == 0))
            {
                await _projectService.DeleteProject(project);
                return NoContent();
            }
            else
            {
                return Ok("Please remove all child records first..!" + " Sub Projects count : " + subProjects.Count().ToString());
            }
        }

        [HttpGet]
        [Route("GetAllProjectsForProgrammeId/{programmeid}")]
        public async Task<ActionResult<IEnumerable<Project>>> GetAllProjectsForProgrammeId([FromRoute] int programmeid)
        {
            var _token = new HTokenClaim
            {
                userId = int.Parse(HttpContext.User.FindFirst("userid")!.Value),
                sabhaId = int.Parse(HttpContext.User.FindFirst("sabhaId")!.Value),
                officeId = int.Parse(HttpContext.User.FindFirst("officeID")!.Value),
            };
            var projects = await _projectService.GetAllProjectsForProgrammeIdandSabhaId(programmeid, _token.sabhaId);
            var projectResources = _mapper.Map<IEnumerable<Project>, IEnumerable<ProjectResource>>(projects);

            return Ok(projectResources);
        }

        [HttpGet]
        [Route("getAllProjectsForProgrammeIdandSabhaId/{programmeId}/{sabhaId}")]
        public async Task<ActionResult<IEnumerable<Project>>> GetAllProjectsForProgrammeIdandSabhaId([FromRoute]int programmeId, [FromRoute] int sabhaId)
        {
            var projects = await _projectService.GetAllProjectsForProgrammeIdandSabhaId(programmeId, sabhaId);
            var projectResources = _mapper.Map<IEnumerable<Project>, IEnumerable<ProjectResource>>(projects);

            return Ok(projectResources);
        }

        [HttpGet]
        [Route("getAllProjectsForSabhaId/{sabhaId}")]
        public async Task<ActionResult<IEnumerable<Project>>> GetAllProjectsForSabhaId([FromRoute] int sabhaId)
        {
            var projects = await _projectService.GetAllProjectsForSabhaId( sabhaId);
            var projectResources = _mapper.Map<IEnumerable<Project>, IEnumerable<ProjectResource>>(projects);

            return Ok(projectResources);
        }
    }
}
