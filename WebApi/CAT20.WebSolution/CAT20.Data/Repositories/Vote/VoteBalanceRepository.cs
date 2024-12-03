using CAT20.Core.HelperModels;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.Vote;
using CAT20.Core.Repositories.Vote;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.Vote
{
    public class VoteBalanceRepository : Repository<VoteBalance>, IVoteBalanceRepository
    {
        public VoteBalanceRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<VoteBalance>> GetAllWithVoteAllocationAsync()
        {
            return await voteAccDbContext.VoteBalances
                .Where(m => m.Status == VoteBalanceStatus.Active)
                .ToListAsync();
        }

        public async Task<VoteBalance> GetWithVoteAllocationByIdAsync(int id)
        {
            return await voteAccDbContext.VoteBalances
                .Where(m => m.Id == id && m.Status == VoteBalanceStatus.Active)
                .FirstOrDefaultAsync(); 
        }

        public async Task<IEnumerable<VoteBalance>> GetAllWithVoteAllocationByVoteAllocationIdAsync(int Id)
        {
            return await voteAccDbContext.VoteBalances
                .Where(m => m.Id == Id)
                .ToListAsync();
        }
        public async Task<VoteBalance> getVoteAllocationByVoteDetailId(int Id)
        {
            return await voteAccDbContext.VoteBalances
                .Where(m => m.VoteDetailId == Id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<VoteBalance>> GetAllWithVoteAllocationByVoteDetailIdAsync(int VoteDetailId)
        {
            return await voteAccDbContext.VoteBalances.Where(m => m.VoteDetailId == VoteDetailId && m.Status == VoteBalanceStatus.Active)
                .ToListAsync();
        }
        public async Task<VoteBalance> GetWithVoteAllocationByVoteDetailIdAsync(int VoteDetailId)
        {
            return await voteAccDbContext.VoteBalances.Where(m => m.VoteDetailId == VoteDetailId && m.Status == VoteBalanceStatus.Active).OrderByDescending(m => m.Id)
                .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<VoteBalance>> GetAllWithVoteAllocationBySabhaIdAsync(int SabhaId)
        {
            return await voteAccDbContext.VoteBalances.Where(m => m.SabhaId == SabhaId && m.Status == VoteBalanceStatus.Active)
                .ToListAsync();
        }

        public async Task<(int totalCount, IEnumerable<VoteBalance> list)> GetVoteAllocationForSabhaByYearAndProgram(int sabhaId,int year, int? classificationId, int? programId, int? voteDetailId, int pageNo, int pageSize, string? filterKeyWord,HTokenClaim token)
        {

            if (filterKeyWord != "undefined")
            {
                filterKeyWord = "%" + filterKeyWord + "%";
            }
            else if (filterKeyWord == "undefined")
            {
                filterKeyWord = null;
            }

            var keyword = filterKeyWord ?? "";

            var result = voteAccDbContext.VoteBalances
                 .Include(m => m.VoteDetail)
                 .Where(m => EF.Functions.Like(m.VoteDetail.Code!, "%" + filterKeyWord + "%"))
                 .Where(m => m.SabhaId == sabhaId && m.Year == year )
                 .Where(m => programId.HasValue? m.VoteDetail.ProgrammeID == programId: true)
                 .Where(m => voteDetailId.HasValue? m.VoteDetail.ID == voteDetailId: true)
                 .Where(m=> classificationId.HasValue? m.ClassificationId== classificationId : (m.ClassificationId==1 || m.ClassificationId ==2));

            int totalCount = await result.CountAsync();


            //var pageSize = 10;
            int skipAmount = (pageNo - 1) * pageSize;

            var list = await result.Skip(skipAmount).Take(pageSize).ToListAsync();


            return (totalCount, list);
        }

        public async Task<(int totalCount, IEnumerable<VoteBalance>)> GetAllWithVoteAllocationHasTakeHoldBySabhaIdAsync(int sabhaId, int pageNo, int pageSize, string? filterKeyWord)
        {

            if (filterKeyWord != "undefined")
            {
                filterKeyWord = "%" + filterKeyWord + "%";
            }
            else if (filterKeyWord == "undefined")
            {
                filterKeyWord = null;
            }

            var keyword = filterKeyWord ?? "";


           var result = voteAccDbContext.VoteBalances
                .Include(m => m.VoteDetail)
                .Where(m=> EF.Functions.Like(m.VoteDetail.Code!, "%" + filterKeyWord + "%"))
                .Where(m => m.SabhaId == sabhaId && m.Status == VoteBalanceStatus.Active && m.TakeHoldAmount > 0m);

            int totalCount = await result.CountAsync();


            //var pageSize = 10;
            int skipAmount = (pageNo - 1) * pageSize;

            var list = await result.Skip(skipAmount).Take(pageSize).ToListAsync();


            return (totalCount, list);
        }

        public async Task<IEnumerable<VoteBalance>> GetAllWithVoteAllocationByVoteDetailIdSabhaIdAsync(int VoteDetailId, int SabhaId)
        {
            return await voteAccDbContext.VoteBalances.Where(m => m.VoteDetailId == VoteDetailId && m.SabhaId == SabhaId && m.Status == VoteBalanceStatus.Active)
                .ToListAsync();
        }
        public async Task<IEnumerable<VoteBalance>> GetAllVoteAllocationsForVoteDetailIdandSabhaIdandYearAsync(int VoteDetailId, int SabhaId, int Year)
        {
            return await voteAccDbContext.VoteBalances.Where(m => m.VoteDetailId == VoteDetailId && m.SabhaId == SabhaId && m.Status == VoteBalanceStatus.Active && m.Year==Year)
                .ToListAsync();
        }

        public async Task<IEnumerable<VoteBalance>> GetAllVoteAllocationsForSabhaIdAsync(int Id)
        {
            return await voteAccDbContext.VoteBalances.Where(m => m.Id == Id && m.Status == VoteBalanceStatus.Active).ToListAsync();
        }

        public async Task<VoteBalance> GetActiveVoteBalance(int voteBalanceId)
        {
            return await voteAccDbContext.VoteBalances
                .Where(m => m.Id == voteBalanceId && m.Status == VoteBalanceStatus.Active)
                .FirstOrDefaultAsync();
        }

        public async Task<VoteBalance> GetActiveVoteBalance(int VoteDetailId, int sabhaId, int year)
        {
            return await voteAccDbContext.VoteBalances
                .Where(m => m.VoteDetailId == VoteDetailId && m.SabhaId== sabhaId && m.Year == year && m.Status == VoteBalanceStatus.Active)
                .FirstOrDefaultAsync();
        }

        public async Task<VoteBalance> GetExpiredVoteBalance(int VoteDetailId, int sabhaId, int year)
        {
            return await voteAccDbContext.VoteBalances
                .Where(m => m.VoteDetailId == VoteDetailId && m.SabhaId == sabhaId && m.Year == year && m.Status == VoteBalanceStatus.Expired)
                .FirstOrDefaultAsync();
        }
        public async Task<bool> HasTransactionsOccurred(int voteBalanceId)
        {
            return await voteAccDbContext.VoteBalancesLogs
                .Where(m => m.VoteBalanceId == voteBalanceId)
                .Where(m => m.TransactionType != VoteBalanceTransactionTypes.Inti)
                .AnyAsync();
        }
        public async Task<VoteBalance> GetLastVoteBalance(int VoteDetailId, int sabhaId, int year)
        {
            return await voteAccDbContext.VoteBalances
                .Where(m => m.VoteDetailId == VoteDetailId && m.SabhaId == sabhaId && m.Status == VoteBalanceStatus.Active)
                .OrderByDescending(m=>m.Id)
                .FirstOrDefaultAsync();
        }

        private VoteAccDbContext voteAccDbContext
        {
            get { return Context as VoteAccDbContext; }
        }
    }
}