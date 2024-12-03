using CAT20.Core.Models.Vote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Vote
{
    public interface ISubProjectService
    {
        Task<IEnumerable<SubProject>> GetAllSubProjects();
        Task<SubProject> GetSubProjectById(int id);
        Task<SubProject> CreateSubProject(SubProject newSubProject);
        Task UpdateSubProject(SubProject subProjectToBeUpdated, SubProject subProject);
        Task DeleteSubProject(SubProject subProject);

        Task<IEnumerable<SubProject>> GetAllWithSubProjectByProjectId(int Id);
        Task<IEnumerable<SubProject>> GetAllWithSubProjectByProjectIdandSabhaId(int ProjectId, int SabhaId);
        Task<IEnumerable<SubProject>> GetAllSubProjectsForSabhaId( int SabhaId);
        Task<IEnumerable<SubProject>> GetAllSubProjectsForProgrammeId( int ProgrammeId);
    }
}

