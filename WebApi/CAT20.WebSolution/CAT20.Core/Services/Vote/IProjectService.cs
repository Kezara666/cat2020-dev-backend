using CAT20.Core.Models.Vote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Vote
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetAllProjects();
        Task<Project> GetProjectById(int id);
        Task<Project> CreateProject(Project newProject);
        Task UpdateProject(Project projectToBeUpdated, Project project);
        Task DeleteProject(Project project);

        Task<IEnumerable<Project>> GetAllProjectsForProgrammeId(int id);
        Task<IEnumerable<Project>> GetAllProjectsForProgrammeIdandSabhaId(int programmeId, int sabhaId);
        Task<IEnumerable<Project>> GetAllProjectsForSabhaId( int sabhaId);
    }
}

