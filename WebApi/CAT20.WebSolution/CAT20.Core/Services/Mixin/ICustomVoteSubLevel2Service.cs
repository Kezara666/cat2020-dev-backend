using CAT20.Core.HelperModels;
using CAT20.Core.Models.Mixin;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Mixin
{
    public interface ICustomVoteSubLevel2Service
    {
        Task<(bool, string?)> SaveCustomVoteSubLevel2sAsync(List<CustomVoteSubLevel2> subLevel1s, HTokenClaim _token);
        Task<(bool, string?)> DeleteCustomVoteSubLevel2Async(int id, HTokenClaim _token);
        Task<CustomVoteSubLevel2> GetCustomVoteSubLevel2ByIdAsync(int id);
        Task<IEnumerable<CustomVoteSubLevel2>> GetCustomVoteSubLevel2sBySubLevel1Async(int subLevel1Id);
    }
}
