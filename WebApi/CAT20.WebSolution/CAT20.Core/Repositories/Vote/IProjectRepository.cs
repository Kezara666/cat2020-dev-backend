using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Vote;

namespace CAT20.Core.Repositories.Vote
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<IEnumerable<Project>> GetAllWithProjectAsync();
        Task<Project> GetWithProjectByIdAsync(int id);
        Task<IEnumerable<Project>> GetAllWithProjectByProjectIdAsync(int Id);
        Task<IEnumerable<Project>> GetAllProjectsForProgrammeIdAsync(int Id);
        Task<IEnumerable<Project>> GetAllProjectsForProgrammeIdandSabhaIdAsync(int programmeId, int sabhaId);

        Task<IEnumerable<Project>> GetAllProjectsForSabhaIdAsync(int Id);
    }
}
