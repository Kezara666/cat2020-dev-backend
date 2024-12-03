using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Vote;

namespace CAT20.Core.Repositories.Vote
{
    public interface ISubProjectRepository : IRepository<SubProject>
    {
        Task<IEnumerable<SubProject>> GetAllWithSubProjectAsync();
        Task<SubProject> GetWithSubProjectByIdAsync(int id);
        Task<IEnumerable<SubProject>> GetAllWithSubProjectBySubProjectIdAsync(int Id);

        Task<IEnumerable<SubProject>> GetAllWithSubProjectByProjectIdAsync(int Id);
        Task<IEnumerable<SubProject>> GetAllWithSubProjectByProjectIdandSabhaIdAsync(int ProjectId, int SabhaId);
        Task<IEnumerable<SubProject>> GetAllSubProjectsForSabhaIdAsync(int Id);
        Task<IEnumerable<SubProject>> GetAllSubProjectsForProgrammeIdAsync(int Id);
    }
}
