using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Mixin;

namespace CAT20.Core.Repositories.Mixin
{
    public interface ICustomVoteSubLevel2Repository : IRepository<CustomVoteSubLevel2>
    {
        Task<CustomVoteSubLevel2> GetCustomVoteSubLevel2ByIdAsync(int id);
        Task<IEnumerable<CustomVoteSubLevel2>> GetCustomVoteSubLevel2sBySubLevel1Async(int subLevel1Id);
    }
}
