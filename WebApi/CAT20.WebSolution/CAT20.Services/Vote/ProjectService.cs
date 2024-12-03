using CAT20.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Vote;
using CAT20.Core.Services.Vote;

namespace CAT20.Services.Vote
{
    public class ProjectService : IProjectService
    {
        private readonly IVoteUnitOfWork _unitOfWork;
        public ProjectService(IVoteUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Project> CreateProject(Project newProject)
        {
            await _unitOfWork.Projects
                .AddAsync(newProject);
            await _unitOfWork.CommitAsync();

            return newProject;
        }
        public async Task DeleteProject(Project project)
        {
            project.Status = 0;
            await _unitOfWork.CommitAsync();
        }
        public async Task<IEnumerable<Project>> GetAllProjects()
        {
            return await _unitOfWork.Projects.GetAllAsync();
        }
        public async Task<Project> GetProjectById(int id)
        {
            return await _unitOfWork.Projects.GetByIdAsync(id);
        }
        public async Task UpdateProject(Project projectToBeUpdated, Project project)
        {
            projectToBeUpdated.NameSinhala = project.NameSinhala;
            projectToBeUpdated.NameTamil = project.NameTamil;
            projectToBeUpdated.NameEnglish = project.NameEnglish;
            projectToBeUpdated.Code = project.Code;

            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Project>> GetAllProjectsForProgrammeId(int id)
        {
            return await _unitOfWork.Projects.GetAllProjectsForProgrammeIdAsync(id);
        }
        
        public async Task<IEnumerable<Project>> GetAllProjectsForProgrammeIdandSabhaId(int programmeId, int sabhaId)
        {
            return await _unitOfWork.Projects.GetAllProjectsForProgrammeIdandSabhaIdAsync(programmeId, sabhaId);
        }

        public async Task<IEnumerable<Project>> GetAllProjectsForSabhaId( int sabhaId)
        {
            return await _unitOfWork.Projects.GetAllProjectsForSabhaIdAsync( sabhaId);
        }
    }
}