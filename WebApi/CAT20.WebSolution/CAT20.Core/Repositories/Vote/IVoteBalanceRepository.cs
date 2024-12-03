using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.Vote;

namespace CAT20.Core.Repositories.Vote
{
    public interface IVoteBalanceRepository : IRepository<VoteBalance>
    {
        Task<IEnumerable<VoteBalance>> GetAllWithVoteAllocationAsync();
        Task<VoteBalance> GetWithVoteAllocationByIdAsync(int id);
        Task<IEnumerable<VoteBalance>> GetAllWithVoteAllocationByVoteAllocationIdAsync(int Id);
        Task<VoteBalance> getVoteAllocationByVoteDetailId(int Id);
        Task<IEnumerable<VoteBalance>> GetAllWithVoteAllocationByVoteDetailIdAsync(int VoteDetailId);
        Task<VoteBalance> GetWithVoteAllocationByVoteDetailIdAsync(int VoteDetailId);
        Task<IEnumerable<VoteBalance>> GetAllWithVoteAllocationBySabhaIdAsync(int SabhaId);
        Task<(int totalCount, IEnumerable<VoteBalance> list)> GetVoteAllocationForSabhaByYearAndProgram(int sabhaId, int year,int? classificationId, int? programId, int? voteDetailId, int pageNo, int pageSize, string? filterKeyword, HTokenClaim token);
        Task<(int totalCount, IEnumerable<VoteBalance>)> GetAllWithVoteAllocationHasTakeHoldBySabhaIdAsync(int sabhaId, int pageNo, int pageSize, string? filterKeyWord);
        Task<IEnumerable<VoteBalance>> GetAllWithVoteAllocationByVoteDetailIdSabhaIdAsync(int VoteDetailId, int SabhaId);
        Task<IEnumerable<VoteBalance>> GetAllVoteAllocationsForSabhaIdAsync(int Id);
        Task<IEnumerable<VoteBalance>> GetAllVoteAllocationsForVoteDetailIdandSabhaIdandYearAsync(int VoteDetailId, int SabhaId, int Year);
        Task<VoteBalance> GetActiveVoteBalance(int voteBalanceId);
        Task<VoteBalance> GetActiveVoteBalance(int VoteDetailId, int SabhaId, int Year);
        Task<VoteBalance> GetExpiredVoteBalance(int VoteDetailId, int SabhaId, int Year);
        Task<bool> HasTransactionsOccurred(int voteBalanceId);
        Task<VoteBalance> GetLastVoteBalance(int VoteDetailId, int SabhaId, int Year);
    }
}
