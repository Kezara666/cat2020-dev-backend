using CAT20.Core.HelperModels;
using CAT20.Core.Models.Vote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.Vote
{
    public interface ICustomVoteBalanceRepository : IRepository<CustomVoteBalance>
    {
        Task<IEnumerable<CustomVoteBalance>> GetAllWithCustomVoteAllocationAsync();
        Task<CustomVoteBalance> GetWithVoteAllocationByIdAsync(int id);
        Task<IEnumerable<CustomVoteBalance>> GetAllWithVoteAllocationByVoteAllocationIdAsync(int Id);
        Task<CustomVoteBalance> getVoteAllocationByVoteDetailId(int Id);
        Task<IEnumerable<CustomVoteBalance>> GetAllWithVoteAllocationByVoteDetailIdAsync(int CustomVoteId);
        Task<CustomVoteBalance> GetWithVoteAllocationByVoteDetailIdAsync(int CustomVoteId);
        Task<IEnumerable<CustomVoteBalance>> GetAllWithVoteAllocationBySabhaIdAsync(int SabhaId);
        Task<(int totalCount, IEnumerable<CustomVoteBalance> list)> GetVoteAllocationForSabhaByYearAndProgram(int sabhaId, int year, int? classificationId, int? programId, int? voteDetailId, int pageNo, int pageSize, string? filterKeyword, HTokenClaim token);
        Task<(int totalCount, IEnumerable<CustomVoteBalance>)> GetAllWithVoteAllocationHasTakeHoldBySabhaIdAsync(int sabhaId, int pageNo, int pageSize, string? filterKeyWord);
        Task<IEnumerable<CustomVoteBalance>> GetAllWithVoteAllocationByVoteDetailIdSabhaIdAsync(int CustomVoteId, int SabhaId);
        Task<IEnumerable<CustomVoteBalance>> GetAllVoteAllocationsForSabhaIdAsync(int Id);
        Task<IEnumerable<CustomVoteBalance>> GetAllVoteAllocationsForVoteDetailIdandSabhaIdandYearAsync(int CustomVoteId, int SabhaId, int Year);
        Task<CustomVoteBalance> GetActiveCustomVoteBalance(int CustomVoteId);
        Task<CustomVoteBalance> GetActiveCustomVoteBalance(int CustomVoteId, int SabhaId, int Year);
        Task<bool> HasActiveCustomVoteBalance(int CustomVoteId, int SabhaId, int Year);
        Task<bool> HasTransactionsOccurred(int CustomVoteId);
        Task<CustomVoteBalance> GetLastVoteBalance(int CustomVoteId, int SabhaId, int Year);
    }
}
