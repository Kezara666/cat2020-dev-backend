using CAT20.Core.Models.Control;
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
    public class IncomeSubtitleRepository : Repository<IncomeSubtitle>, IIncomeSubtitleRepository
    {
        public IncomeSubtitleRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<IncomeSubtitle>> GetAllWithIncomeSubtitleAsync()
        {
            return await voteAccDbContext.IncomeSubtitles
                .Where(m => m.Status == 1)
                .ToListAsync();
        }

        public async Task<IncomeSubtitle> GetWithIncomeSubtitleByIdAsync(int id)
        {
            return await voteAccDbContext.IncomeSubtitles
                .Where(m => m.ID == id && m.Status == 1)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<IncomeSubtitle>> GetAllWithIncomeSubtitleByIncomeSubtitleIdAsync(int Id)
        {
            return await voteAccDbContext.IncomeSubtitles
                .Where(m => m.ID == Id && m.Status == 1)
                .ToListAsync();
        }

        public async Task<IEnumerable<IncomeSubtitle>> GetAllWithIncomeSubtitleByIncometitleIdAsync(int Id)
        {
            return await voteAccDbContext.IncomeSubtitles.Where(m => m.IncomeTitleID == Id && m.Status == 1)
                .ToListAsync();
        }
        public async Task<IEnumerable<IncomeSubtitle>> GetAllWithIncomeSubtitleByIncometitleIdSabhaIdAsync(int IncomeTitleId, int SabhaId)
        {
            return await voteAccDbContext.IncomeSubtitles.Where(m => m.IncomeTitleID == IncomeTitleId && m.SabhaID == SabhaId && m.Status == 1)
                .ToListAsync();
        }

        public async Task<IEnumerable<IncomeSubtitle>> GetAllIncomeSubTitlesForSabhaIdAsync(int Id)
        {
            return await voteAccDbContext.IncomeSubtitles.Where(m => m.SabhaID == Id && m.Status == 1).ToListAsync();
        }

        public async Task<IEnumerable<IncomeSubtitle>> GetAllIncomeSubTitlesForTitleIdAsync(int Id)
        {
            return await voteAccDbContext.IncomeSubtitles.Where(m => m.IncomeTitleID == Id && m.Status == 1).ToListAsync();
        }

        public async Task<IEnumerable<IncomeSubtitle>> GetAllIncomeSubTitlesForProgrammeIdAsync(int Id)
        {
            return await voteAccDbContext.IncomeSubtitles.Where(m => m.ProgrammeID == Id && m.Status == 1).ToListAsync();
        }

        public async Task<IEnumerable<IncomeSubtitle>> GetAllIncomeSubTitlesForProgrammeClassificationIdAsync(int ProgrammeId, int ClassificationId)
        {
            return await voteAccDbContext.IncomeSubtitles.Include(m => m.incomeTitle).Where(m => m.ProgrammeID == ProgrammeId && m.incomeTitle.ClassificationID == ClassificationId && m.Status == 1).ToListAsync();
        }
        public async Task<IEnumerable<IncomeSubtitle>> GetAllIncomeSubTitlesForTitleIDAndClassificationIDAndMainLedgerAccountId(int ClassificationID, int mainLedgerAccId, int TitleID, int sabhaId)
        {
            try
            {
                if (ClassificationID != 0 && mainLedgerAccId == 0 && TitleID == 0)
                {
                    var x = await voteAccDbContext.IncomeSubtitles
                        .Include(m => m.incomeTitle)
                        .Include(m => m.SubLedgerAccounts.Where(a => a.StatusID == 1))
                        .Where(m => m.incomeTitle.ClassificationID == ClassificationID 
                                    && m.SabhaID == sabhaId
                                    && m.Status == 1)
                        .ToListAsync();
                    return x;
                }
                else if (ClassificationID != 0 && mainLedgerAccId != 0 && TitleID == 0)
                {
                    var x = await voteAccDbContext.IncomeSubtitles
                        .Include(m => m.incomeTitle)
                        .Include(m => m.SubLedgerAccounts.Where(a => a.StatusID == 1))
                        .Where(m => m.incomeTitle.ClassificationID == ClassificationID 
                                    && m.incomeTitle.MainLedgerAccountID == mainLedgerAccId 
                                    && m.SabhaID == sabhaId
                                    && m.Status == 1)
                        .ToListAsync();
                    return x;
                }
                else if (ClassificationID != 0 && mainLedgerAccId != 0 && TitleID != 0)
                {
                    var x = await voteAccDbContext.IncomeSubtitles
                        .Include(m => m.incomeTitle)
                        .Include(m => m.SubLedgerAccounts.Where(a => a.StatusID == 1))
                        .Where(m => m.incomeTitle.ClassificationID == ClassificationID
                                     && m.incomeTitle.MainLedgerAccountID == mainLedgerAccId
                                     && m.IncomeTitleID == TitleID
                                     && m.SabhaID == sabhaId
                                     && m.Status == 1)
                        .ToListAsync();

                    return x;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        //public async Task<IEnumerable<IncomeExpenditureSubledgerAccount>> GetAllIncomeExpenditureSubLedgerAccountsForSubTitleId(int SubTitleID, int sabhaId)
        //{
        //    try
        //    {
        //        var x = await voteAccDbContext.SubLedgerAccounts
        //            .Where(m => m.IncomeExpenditureLedgerAccountId==SubTitleID && m.StatusID == 1).ToListAsync();
        //        return x;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}


        private VoteAccDbContext voteAccDbContext
        {
            get { return Context as VoteAccDbContext; }
        }
    }
}