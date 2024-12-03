using CAT20.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Vote;
using CAT20.Core.Services.Vote;

namespace CAT20.Services.Vote
{
    public class SubProjectService : ISubProjectService
    {
        private readonly IVoteUnitOfWork _unitOfWork;
        public SubProjectService(IVoteUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<SubProject> CreateSubProject(SubProject newSubProject)
        {
            await _unitOfWork.SubProjects
                .AddAsync(newSubProject);
            await _unitOfWork.CommitAsync();

            return newSubProject;
        }
        public async Task DeleteSubProject(SubProject subProject)
        {
            subProject.Status = 0;
            await _unitOfWork.CommitAsync();
        }
        public async Task<IEnumerable<SubProject>> GetAllSubProjects()
        {
            return await _unitOfWork.SubProjects.GetAllAsync();
        }
        public async Task<SubProject> GetSubProjectById(int id)
        {
            return await _unitOfWork.SubProjects.GetByIdAsync(id);
        }
        public async Task UpdateSubProject(SubProject subProjectToBeUpdated, SubProject subProject)
        {
            subProjectToBeUpdated.ProjectID = subProject.ProjectID;
            subProjectToBeUpdated.NameSinhala = subProject.NameSinhala;
            subProjectToBeUpdated.NameTamil = subProject.NameTamil;
            subProjectToBeUpdated.NameEnglish = subProject.NameEnglish;
            subProjectToBeUpdated.Code = subProject.Code;

            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<SubProject>> GetAllWithSubProjectByProjectId(int Id)
        {
            return await _unitOfWork.SubProjects.GetAllWithSubProjectByProjectIdAsync(Id);
        }

        public async Task<IEnumerable<SubProject>> GetAllWithSubProjectByProjectIdandSabhaId(int ProjectId, int SabhaId)
        {
            return await _unitOfWork.SubProjects.GetAllWithSubProjectByProjectIdandSabhaIdAsync(ProjectId, SabhaId);
        }

        public async Task<IEnumerable<SubProject>> GetAllSubProjectsForSabhaId(int SabhaId)
        {
            return await _unitOfWork.SubProjects.GetAllSubProjectsForSabhaIdAsync(SabhaId);
        }

        public async Task<IEnumerable<SubProject>> GetAllSubProjectsForProgrammeId(int ProgrammeId)
        {
            return await _unitOfWork.SubProjects.GetAllSubProjectsForProgrammeIdAsync(ProgrammeId);
        }
    }
}