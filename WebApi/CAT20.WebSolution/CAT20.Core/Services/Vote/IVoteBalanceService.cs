using CAT20.Core.DTO.Final;
using CAT20.Core.DTO.Vote;
using CAT20.Core.DTO.Vote.Save;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.Vote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Vote
{
    public interface IVoteBalanceService
    {
        Task<IEnumerable<VoteBalance>> GetAllVoteAllocations();
        Task<VoteBalance> GetVoteAllocationById(int id);
        Task<VoteBalance> getVoteAllocationByVoteDetailId(int id);

        Task<(int totalCount, IEnumerable<VoteBalance> list)> GetVoteAllocationForSabhaByYearAndProgram(int sabhaId, int year,int? classificationId, int? programId, int? voteDetailId, int pageNo, int pageSize, string? filterKeyword, HTokenClaim token);
        Task<(VoteBalance,string?)> GetActiveVoteBalance(int VoteDetailId, HTokenClaim token);

        Task<(bool, string)> CreateVoteAllocation(VoteBalance newVoteAllocation, HTokenClaim token);
        Task<(bool,string?)> TakeHold(SaveVoteBalanceTakeHold voteBalance, HTokenClaim token);
        Task<(bool,string?)> ReleaseTakeHold(ReleaseVoteBalanceTakeHold releaseVoteBalanceTake, HTokenClaim token);
        Task<(bool, string)> UpdateVoteAllocation(VoteBalance newVoteAllocation, HTokenClaim token);
        Task DeleteVoteAllocation(VoteBalance voteAllocation);

        Task<IEnumerable<VoteBalance>> GetAllWithVoteAllocationByVoteDetailIdAsync(int VoteDetailId);
        Task<IEnumerable<VoteBalance>> GetAllWithVoteAllocationBySabhaId(int SabhaId);
        Task<(int totalCount, IEnumerable<VoteBalance> list)> GetAllWithVoteAllocationHasTakeHoldBySabhaIdAsync(int sabhaId,  int pageNo, int pageSize, string? filterKeyWord);
        Task<IEnumerable<VoteBalance>> GetAllWithVoteAllocationByVoteDetailIdSabhaId(int VoteDetailId, int SabhaId);
        Task<IEnumerable<VoteBalance>> GetAllVoteAllocationsForVoteDetailIdandSabhaIdandYear(int VoteDetailId, int SabhaId,int Year);
        Task<IEnumerable<VoteBalance>> GetAllVoteAllocationsForSabhaId(int SabhaId);

        Task<VoteBalance> CreateNewVoteBalance(int VoteDetailId,Session session,HTokenClaim token);

        Task<bool> UpdateVoteBalanceForOpenBalances(int voteDetailId, decimal Amount, int? year, int? month, string? code, string? subCode, FAMainTransactionMethod transactionMethod, Session session, HTokenClaim token);

        Task<bool> UpdateVoteBalance(HVoteBalanceTransaction voteBalanceTransaction, HTokenClaim token);

        Task<(bool, string)> SaveComparativeFiguresBalance(List<saveCompartiveFigureBalance> balance, HTokenClaim token);
    }
}

