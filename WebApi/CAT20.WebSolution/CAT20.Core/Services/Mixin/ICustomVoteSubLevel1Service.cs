using CAT20.Core.HelperModels;
using CAT20.Core.Models.Mixin;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Mixin
{
    public interface ICustomVoteSubLevel1Service
    {
        Task<(bool, string?)> SaveCustomVoteSubLevel1sAsync(List<CustomVoteSubLevel1> CustomVoteSubLeve1s, HTokenClaim _token);
        Task<(bool, string?)> DeleteCustomVoteSubLevel1Async(int id);
        Task<CustomVoteSubLevel1> GetByCustomVoteSubLevel1IdAsync(int id);
        Task<IEnumerable<CustomVoteSubLevel1>> GetCustomVoteSubLevel1sByCustomVoteAsync(int customVoteId);
    }
}
