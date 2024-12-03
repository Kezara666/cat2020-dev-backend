using CAT20.Core.Models.Control;
using CAT20.Core.Models.Vote;
using CAT20.Core.Repositories.Vote;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.Vote
{
    public class BalancesheetTitleRepository : Repository<BalancesheetTitle>, IBalancesheetTitleRepository
    {
        public BalancesheetTitleRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<BalancesheetTitle>> GetAllWithBalancesheetTitleAsync()
        {
            return await voteAccDbContext.BalancesheetTitles
                .Where(m => m.Status == 1 && (m.ClassificationID==3 || m.ClassificationID == 4 || m.ClassificationID == 5))
                .ToListAsync();
        }

        public async Task<BalancesheetTitle> GetWithBalancesheetTitleByIdAsync(int id)
        {
            return await voteAccDbContext.BalancesheetTitles
                .Where(m => m.ID == id && m.Status == 1 && (m.ClassificationID == 3 || m.ClassificationID == 4 || m.ClassificationID == 5))
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<BalancesheetTitle>> GetAllWithBalancesheetTitleByBalancesheetTitleIdAsync(int Id)
        {
            return await voteAccDbContext.BalancesheetTitles
                .Where(m => m.ID == Id && m.Status == 1 && (m.ClassificationID == 3 || m.ClassificationID == 4 || m.ClassificationID == 5))
                .ToListAsync();
        }
        public async Task<IEnumerable<BalancesheetTitle>> GetAllWithBalancesheetTitleBySabhaIdAsync(int SabhaId)
        {
            try
            {
                return await voteAccDbContext.BalancesheetTitles
                    .Where(m => m.SabhaID == SabhaId && m.Status == 1 && (m.ClassificationID == 3 || m.ClassificationID == 4 || m.ClassificationID == 5))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"An error occurred while retrieving balance sheet titles: {ex.Message}");
                throw; // Re-throw the exception to propagate it up the call stack
            }
        }


        public async Task<IEnumerable<BalancesheetTitle>> GetAllWithBalancesheetTitleByAccountDetailIdAsync(int AccountDetailId)
        {
            //return await voteAccDbContext.BalancesheetTitles.Join(voteAccDbContext.BalancesheetSubtitles,
            //             s => s.ID,
            //             sa => sa.BalsheetTitleID,
            //             (s, sa) => new { BalancesheetTitles = s, BalancesheetSubtitles = sa })
            //        .Where(ssa => ssa.BalancesheetSubtitles.BankAccountID == AccountDetailId).GroupBy(m => m.BalancesheetTitles.ID)
            //        .Select(ssa => ssa.BalancesheetTitles).ToListAsync();
            var result = await voteAccDbContext.BalancesheetTitles.Join(voteAccDbContext.BalancesheetSubtitles,
                     s => s.ID,
                     sa => sa.BalsheetTitleID,
                     (s, sa) => new { BalancesheetTitles = s, BalancesheetSubtitles = sa })
                .Where(ssa => ssa.BalancesheetSubtitles.BankAccountID == AccountDetailId)
                .Select(ssa => ssa.BalancesheetTitles).Distinct().ToListAsync();

            return result;
        }

        public async Task<IEnumerable<BalancesheetTitle>> GetAllWithBalancesheetTitleByClassificationCategoryIdAsync(int classificationId, int categoryId, int sabhaid)
        {
            return await voteAccDbContext.BalancesheetTitles.Where(m => m.ClassificationID == classificationId && m.SabhaID == sabhaid && m.MainLedgerAccountID == categoryId && m.Status == 1).ToListAsync();
        }

        public async Task<IEnumerable<BalancesheetTitle>> GetAllWithBalancesheetTitleByClassificationIdAsync(int classificationId, int sabhaid)
        {
            return await voteAccDbContext.BalancesheetTitles.Where(m => m.ClassificationID == classificationId && m.Status == 1 && m.SabhaID == sabhaid).ToListAsync();
        }

        public async Task<IEnumerable<BalancesheetTitle>> GetAllWithBalancesheetTitleByCategoryIdAsync(int categoryId, int sabhaid)
        {
            return await voteAccDbContext.BalancesheetTitles.Where(m => m.MainLedgerAccountID == categoryId && m.Status == 1 && m.SabhaID == sabhaid).ToListAsync();
        }

        public async Task<IEnumerable<BalancesheetTitle>> GetAllWithBalancesheetTitleByClassificationCategorySabhaId(int classificationId, int sabhaId)
        {
            return await voteAccDbContext.BalancesheetTitles.Where(m => m.ClassificationID == classificationId && m.Status == 1 && m.SabhaID == sabhaId).ToListAsync();
        }

        private VoteAccDbContext voteAccDbContext
        {
            get { return Context as VoteAccDbContext; }
        }
    }
}