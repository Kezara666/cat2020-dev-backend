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
    public class CustomVoteBalanceRepository : Repository<CustomVoteBalance>, ICustomVoteBalanceRepository
    {
        public CustomVoteBalanceRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<CustomVoteBalance>> GetAllWithCustomVoteAllocationAsync()
        {
            return (IEnumerable<CustomVoteBalance>)await voteAccDbContext.CustomVoteBalances
                .Where(m => m.Status == CustomVoteBalanceStatus.Active)
                .ToListAsync();
        }

        public async Task<CustomVoteBalance> GetWithVoteAllocationByIdAsync(int id)
        {
            return await voteAccDbContext.CustomVoteBalances
                .Where(m => m.Id == id && m.Status == CustomVoteBalanceStatus.Active)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CustomVoteBalance>> GetAllWithVoteAllocationByVoteAllocationIdAsync(int Id)
        {
            return await voteAccDbContext.CustomVoteBalances
                .Where(m => m.Id == Id)
                .ToListAsync();
        }
        public async Task<CustomVoteBalance> getVoteAllocationByVoteDetailId(int Id)
        {
            return await voteAccDbContext.CustomVoteBalances
                .Where(m => m.CustomVoteDetailId == Id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CustomVoteBalance>> GetAllWithVoteAllocationByVoteDetailIdAsync(int CustomVoteId)
        {
            return await voteAccDbContext.CustomVoteBalances.Where(m => m.CustomVoteDetailId == CustomVoteId && m.Status == CustomVoteBalanceStatus.Active)
                .ToListAsync();
        }
        public async Task<CustomVoteBalance> GetWithVoteAllocationByVoteDetailIdAsync(int CustomVoteId)
        {
            return await voteAccDbContext.CustomVoteBalances.Where(m => m.CustomVoteDetailId == CustomVoteId && m.Status == CustomVoteBalanceStatus.Active).OrderByDescending(m => m.Id)
                .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<CustomVoteBalance>> GetAllWithVoteAllocationBySabhaIdAsync(int SabhaId)
        {
            return await voteAccDbContext.CustomVoteBalances.Where(m => m.SabhaId == SabhaId && m.Status == CustomVoteBalanceStatus.Active)
                .ToListAsync();
        }

        public async Task<(int totalCount, IEnumerable<CustomVoteBalance> list)> GetVoteAllocationForSabhaByYearAndProgram(int sabhaId, int year, int? classificationId, int? programId, int? CustomVoteId, int pageNo, int pageSize, string? filterKeyWord, HTokenClaim token)
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

            var result = voteAccDbContext.CustomVoteBalances
                 .Include(m => m.VoteAssignmentDetails)
                 .Where(m => EF.Functions.Like(m.VoteAssignmentDetails.Code!, "%" + filterKeyWord + "%"))
                 .Where(m => m.SabhaId == sabhaId && m.Year == year)
                 //.Where(m => programId.HasValue ? m.VoteAssignmentDetails.ProgrammeId == programId : true)
                 .Where(m => CustomVoteId.HasValue ? m.VoteAssignmentDetails.Id == CustomVoteId : true)
                 .Where(m => classificationId.HasValue ? m.ClassificationId == classificationId : (m.ClassificationId == 1 || m.ClassificationId == 2));

            int totalCount = await result.CountAsync();


            //var pageSize = 10;
            int skipAmount = (pageNo - 1) * pageSize;

            var list = await result.Skip(skipAmount).Take(pageSize).ToListAsync();


            return (totalCount, list);
        }

        public async Task<(int totalCount, IEnumerable<CustomVoteBalance>)> GetAllWithVoteAllocationHasTakeHoldBySabhaIdAsync(int sabhaId, int pageNo, int pageSize, string? filterKeyWord)
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


            var result = voteAccDbContext.CustomVoteBalances
                 .Include(m => m.VoteAssignmentDetails)
                 .Where(m => EF.Functions.Like(m.VoteAssignmentDetails.Code!, "%" + filterKeyWord + "%"))
                 .Where(m => m.SabhaId == sabhaId && m.Status == CustomVoteBalanceStatus.Active && m.TakeHoldAmount > 0m);

            int totalCount = await result.CountAsync();


            //var pageSize = 10;
            int skipAmount = (pageNo - 1) * pageSize;

            var list = await result.Skip(skipAmount).Take(pageSize).ToListAsync();


            return (totalCount, list);
        }

        public async Task<IEnumerable<CustomVoteBalance>> GetAllWithVoteAllocationByVoteDetailIdSabhaIdAsync(int CustomVoteId, int SabhaId)
        {
            return await voteAccDbContext.CustomVoteBalances.Where(m => m.CustomVoteDetailId == CustomVoteId && m.SabhaId == SabhaId && m.Status == CustomVoteBalanceStatus.Active)
                .ToListAsync();
        }
        public async Task<IEnumerable<CustomVoteBalance>> GetAllVoteAllocationsForVoteDetailIdandSabhaIdandYearAsync(int CustomVoteId, int SabhaId, int Year)
        {
            return await voteAccDbContext.CustomVoteBalances.Where(m => m.CustomVoteDetailId == CustomVoteId && m.SabhaId == SabhaId && m.Status == CustomVoteBalanceStatus.Active && m.Year == Year)
                .ToListAsync();
        }

        public async Task<IEnumerable<CustomVoteBalance>> GetAllVoteAllocationsForSabhaIdAsync(int Id)
        {
            return await voteAccDbContext.CustomVoteBalances.Where(m => m.Id == Id && m.Status == CustomVoteBalanceStatus.Active).ToListAsync();
        }

        public async Task<CustomVoteBalance> GetActiveCustomVoteBalance(int customVoteBalanceId)
        {
            return await voteAccDbContext.CustomVoteBalances
                .Where(m => m.Id == customVoteBalanceId && m.Status == CustomVoteBalanceStatus.Active)
                .FirstOrDefaultAsync();
        }

        public async Task<CustomVoteBalance> GetActiveCustomVoteBalance(int CustomVoteId, int sabhaId, int year)
        {
            return await voteAccDbContext.CustomVoteBalances
                .Where(m => m.CustomVoteDetailId == CustomVoteId && m.SabhaId == sabhaId && m.Year == year && m.Status == CustomVoteBalanceStatus.Active)
                .FirstOrDefaultAsync();
        }
        public async Task<bool> HasActiveCustomVoteBalance(int CustomVoteId, int sabhaId, int year)
        {
            return await voteAccDbContext.CustomVoteBalances
                .Where(m => m.CustomVoteDetailId == CustomVoteId && m.SabhaId == sabhaId && m.Year == year && m.Status == CustomVoteBalanceStatus.Active)
                .AnyAsync();
        }

        public async Task<bool> HasTransactionsOccurred(int customVoteBalanceId)
        {
            return await voteAccDbContext.CustomVoteBalancesLogs
                .Where(m => m.CustomVoteBalanceId == customVoteBalanceId)
                .Where(m => m.TransactionType != VoteBalanceTransactionTypes.Inti)
                .AnyAsync();
        }
        public async Task<CustomVoteBalance> GetLastVoteBalance(int CustomVoteId, int sabhaId, int year)
        {
            return await voteAccDbContext.CustomVoteBalances
                .Where(m => m.CustomVoteDetailId == CustomVoteId && m.SabhaId == sabhaId && m.Status == CustomVoteBalanceStatus.Active)
                .OrderByDescending(m => m.Id)
                .FirstOrDefaultAsync();
        }

        private VoteAccDbContext voteAccDbContext
        {
            get { return Context as VoteAccDbContext; }
        }

        //Task<CustomVoteBalance> ICustomVoteBalanceRepository.GetWithVoteAllocationByIdAsync(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<IEnumerable<CustomVoteBalance>> ICustomVoteBalanceRepository.GetAllWithVoteAllocationByVoteAllocationIdAsync(int Id)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<CustomVoteBalance> ICustomVoteBalanceRepository.getVoteAllocationByVoteDetailId(int Id)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<IEnumerable<CustomVoteBalance>> ICustomVoteBalanceRepository.GetAllWithVoteAllocationByVoteDetailIdAsync(int CustomVoteId)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<CustomVoteBalance> ICustomVoteBalanceRepository.GetWithVoteAllocationByVoteDetailIdAsync(int CustomVoteId)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<IEnumerable<CustomVoteBalance>> ICustomVoteBalanceRepository.GetAllWithVoteAllocationBySabhaIdAsync(int SabhaId)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<(int totalCount, IEnumerable<CustomVoteBalance> list)> ICustomVoteBalanceRepository.GetVoteAllocationForSabhaByYearAndProgram(int sabhaId, int year, int? classificationId, int? programId, int? voteDetailId, int pageNo, int pageSize, string? filterKeyword, HTokenClaim token)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<(int totalCount, IEnumerable<CustomVoteBalance>)> ICustomVoteBalanceRepository.GetAllWithVoteAllocationHasTakeHoldBySabhaIdAsync(int sabhaId, int pageNo, int pageSize, string? filterKeyWord)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<IEnumerable<CustomVoteBalance>> ICustomVoteBalanceRepository.GetAllWithVoteAllocationByVoteDetailIdSabhaIdAsync(int CustomVoteId, int SabhaId)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<IEnumerable<CustomVoteBalance>> ICustomVoteBalanceRepository.GetAllVoteAllocationsForSabhaIdAsync(int Id)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<IEnumerable<CustomVoteBalance>> ICustomVoteBalanceRepository.GetAllVoteAllocationsForVoteDetailIdandSabhaIdandYearAsync(int CustomVoteId, int SabhaId, int Year)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<CustomVoteBalance> ICustomVoteBalanceRepository.GetActiveVoteBalance(int CustomVoteId)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<CustomVoteBalance> ICustomVoteBalanceRepository.GetActiveVoteBalance(int CustomVoteId, int SabhaId, int Year)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<CustomVoteBalance> ICustomVoteBalanceRepository.GetLastVoteBalance(int CustomVoteId, int SabhaId, int Year)
        //{
        //    throw new NotImplementedException();
        //}


    }
}