using CAT20.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Vote;
using CAT20.Core.Services.Vote;

namespace CAT20.Services.Vote
{
    public class BalancesheetBalanceService : IBalancesheetBalanceService
    {
        private readonly IVoteUnitOfWork _unitOfWork;
        public BalancesheetBalanceService(IVoteUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BalancesheetBalance> CreateBalancesheetBalance(BalancesheetBalance newBalancesheetBalance)
        {
            await _unitOfWork.BalancesheetBalances
                .AddAsync(newBalancesheetBalance);
            await _unitOfWork.CommitAsync();

            return newBalancesheetBalance;
        }
        public async Task DeleteBalancesheetBalance(BalancesheetBalance balancesheetBalance)
        {
            balancesheetBalance.Status = 0;
            await _unitOfWork.CommitAsync();
        }
        public async Task<IEnumerable<BalancesheetBalance>> GetAllBalancesheetBalances()
        {
            return await _unitOfWork.BalancesheetBalances.GetAllAsync();
        }
        public async Task<BalancesheetBalance> GetBalancesheetBalanceById(int id)
        {
            return await _unitOfWork.BalancesheetBalances.GetByIdAsync(id);
        }
        public async Task UpdateBalancesheetBalance(BalancesheetBalance balancesheetBalanceToBeUpdated, BalancesheetBalance balancesheetBalance)
        {
            balancesheetBalanceToBeUpdated.Year = balancesheetBalance.Year;
            balancesheetBalanceToBeUpdated.Balance = balancesheetBalance.Balance;
            balancesheetBalanceToBeUpdated.Comment = balancesheetBalance.Comment;
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<BalancesheetBalance>> GetAllWithBalancesheetBalanceByVoteDetailId(int Id)
        {
            return await _unitOfWork.BalancesheetBalances.GetAllWithBalancesheetBalanceByVoteDetailIdAsync(Id);
        }

        public async Task<IEnumerable<BalancesheetBalance>> GetAllWithBalancesheetBalanceByVoteDetailIdandSabhaId(int VoteDetailId, int SabhaId)
        {
            return await _unitOfWork.BalancesheetBalances.GetAllWithBalancesheetBalanceByVoteDetailIdandSabhaIdAsync(VoteDetailId, SabhaId);
        }

        public async Task<IEnumerable<BalancesheetBalance>> GetAllBalancesheetBalancesForVoteDetailIdandYear(int VoteDetailId, int year)
        {
            return await _unitOfWork.BalancesheetBalances.GetAllBalancesheetBalancesForVoteDetailIdandYearAsync(VoteDetailId, year);
        }

        
        public async Task<IEnumerable<BalancesheetBalance>> GetAllBalancesheetBalancesForSabhaId( int SabhaId)
        {
            return await _unitOfWork.BalancesheetBalances.GetAllBalancesheetBalancesForSabhaIdAsync(SabhaId);
        }
    }
}