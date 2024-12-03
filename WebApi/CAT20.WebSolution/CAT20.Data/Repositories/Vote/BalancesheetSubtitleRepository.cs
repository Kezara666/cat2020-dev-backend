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
    public class BalancesheetSubtitleRepository : Repository<BalancesheetSubtitle>, IBalancesheetSubtitleRepository
    {
        public BalancesheetSubtitleRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<BalancesheetSubtitle>> GetAllWithBalancesheetSubtitleAsync()
        {
            return await voteAccDbContext.BalancesheetSubtitles
                .Include(m => m.balancesheetTitle)
                .Where(m => m.Status == 1)
                .ToListAsync();
        }

        public async Task<BalancesheetSubtitle> GetWithBalancesheetSubtitleByIdAsync(int id)
        {
            return await voteAccDbContext.BalancesheetSubtitles
                .Include(m => m.balancesheetTitle)
                .Where(m => m.ID == id && m.Status == 1)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<BalancesheetSubtitle>> GetAllWithBalancesheetSubtitleByBalancesheetSubtitleIdAsync(int Id)
        {
            return await voteAccDbContext.BalancesheetSubtitles.Include(m => m.balancesheetTitle)
                .Where(m => m.ID == Id && m.Status == 1)
                .ToListAsync();
        }
        public async Task<IEnumerable<BalancesheetSubtitle>> GetAllWithBalancesheetSubtitleByBalancesheetTitleIdAsync(int Id)
        {
            return await voteAccDbContext.BalancesheetSubtitles.Include(m => m.balancesheetTitle).Where(m => m.BalsheetTitleID == Id && m.Status == 1)
                .ToListAsync();
        }
        public async Task<IEnumerable<BalancesheetSubtitle>> GetAllWithBalancesheetSubtitleByBalancesheetTitleIdandSabhaIdAsync(int BalancesheetTitleId, int SabhaId)
        {
            return await voteAccDbContext.BalancesheetSubtitles.Include(m => m.balancesheetTitle)
                .Where(m => m.BalsheetTitleID == BalancesheetTitleId && m.SabhaID == SabhaId && m.Status == 1)
                .ToListAsync();
        }

        public async Task<IEnumerable<BalancesheetSubtitle>> GetAllBalancesheetSubtitlesForSabhaIdAsync(int Id)
        {
            return await voteAccDbContext.BalancesheetSubtitles.Include(m => m.balancesheetTitle)
                .Where(m => m.SabhaID == Id && m.Status == 1).ToListAsync();
        }

        public async Task<IEnumerable<BalancesheetSubtitle>> GetAllBalancesheetSubtitlesForTitleIDAsync(int Id)
        {
            var query = voteAccDbContext.BalancesheetSubtitles
                .Include(m => m.balancesheetTitle)
                .Where(m => m.BalsheetTitleID == Id && m.Status == 1);

            if (query.Any(m => m.SubLedgerAccounts != null))
            {
                query = query.Include(m => m.SubLedgerAccounts.Where(a => a.StatusID == 1));
            }

            return await query.ToListAsync();
        }


        public async Task<IEnumerable<BalancesheetSubtitle>> GetAllBalancesheetSubtitlesForTitleIDAndAccountDetailIDAsync(int Id, int accountdetailid)
        {
            return await voteAccDbContext.BalancesheetSubtitles.Include(m => m.balancesheetTitle).Where(m => m.BalsheetTitleID == Id && m.Status == 1 && m.BankAccountID == accountdetailid).ToListAsync();
        }


        public async Task<IEnumerable<BalancesheetSubtitle>> GetAllBalancesheetSubtitlesForTitleClassificationIDAsync(int TitleID, int ClassificationID, int sabhaid)
        {
            return await voteAccDbContext.BalancesheetSubtitles
                .Include(m => m.balancesheetTitle)
                .Where(m => m.BalsheetTitleID == TitleID && m.Status == 1 && m.balancesheetTitle.ClassificationID == ClassificationID && m.SabhaID == sabhaid).ToListAsync();
        }

        public async Task<IEnumerable<BalancesheetSubtitle>> GetAllBalancesheetSubtitlesByClassificationIDMainLedgerAccountId(int ClassificationID, int mainledgeraccid, int sabhaid)
        {
            var query = voteAccDbContext.BalancesheetSubtitles
                .Include(m => m.balancesheetTitle)
                .Where(m => m.balancesheetTitle.MainLedgerAccountID == mainledgeraccid
                            && m.Status == 1
                            && m.balancesheetTitle.ClassificationID == ClassificationID
                            && m.SabhaID == sabhaid);

            if (query.Any(m => m.SubLedgerAccounts != null))
            {
                query = query.Include(m => m.SubLedgerAccounts.Where(a => a.StatusID == 1));
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<BalancesheetSubtitle>> GetAllBalancesheetSubtitlesForClassificationIDAsync(int ClassificationID, int sabhaid)
        {
            var query = voteAccDbContext.BalancesheetSubtitles
                .Include(m => m.balancesheetTitle)
                .Where(m => m.Status == 1
                            && m.balancesheetTitle.ClassificationID == ClassificationID
                            && m.SabhaID == sabhaid);

            if (query.Any(m => m.SubLedgerAccounts != null))
            {
                query = query.Include(m => m.SubLedgerAccounts.Where(a => a.StatusID == 1));
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<BalancesheetSubtitle>> GetAllDepositSubCategoriesForSabha(int sabhaId)
        {
            try
            {
                var x = await voteAccDbContext.BalancesheetSubtitles
                    .Include(m => m.balancesheetTitle)
                    .Where(m => (m.balancesheetTitle.Code == "250" || m.balancesheetTitle.Code == "7200") && m.balancesheetTitle.Status == 1 && m.balancesheetTitle.ClassificationID == 4 && m.SabhaID == sabhaId && m.Status == 1).ToListAsync();
                return x;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private VoteAccDbContext voteAccDbContext
        {
            get { return Context as VoteAccDbContext; }
        }
    }
}