using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Vote;

namespace CAT20.Core.Repositories.Vote
{
    public interface IProgrammeRepository : IRepository<Programme>
    {
        Task<IEnumerable<Programme>> GetAllWithProgrammeAsync();
        Task<Programme> GetWithProgrammeByIdAsync(int id);
        Task<IEnumerable<Programme>> GetAllWithProgrammeByProgrammeIdAsync(int Id);
        Task<IEnumerable<Programme>> GetAllProgrammesForSabhaIdAsync(int Id);

    }
}
