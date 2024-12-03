using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Mixin;

namespace CAT20.Core.Repositories.Mixin
{
    public interface ICustomVoteSubLevel1Repository : IRepository<CustomVoteSubLevel1>
    {
        Task<CustomVoteSubLevel1> GetByCustomVoteSubLevel1IdAsysc(int id);
        Task<IEnumerable<CustomVoteSubLevel1>> GetCustomVoteSubLevel1sByCustomVoteAsync(int customVoteId);
    }
}
