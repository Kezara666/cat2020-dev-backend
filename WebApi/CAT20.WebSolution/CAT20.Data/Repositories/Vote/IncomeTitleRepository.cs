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
    public class IncomeTitleRepository : Repository<IncomeTitle>, IIncomeTitleRepository
    {
        public IncomeTitleRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<IncomeTitle>> GetAllWithIncomeTitleAsync()
        {
            return await voteAccDbContext.IncomeTitles
                .Where(m => m.Status == 1)
                .ToListAsync();
        }

        public async Task<IncomeTitle> GetWithIncomeTitleByIdAsync(int id)
        {
            return await voteAccDbContext.IncomeTitles
                .Where(m => m.Status == 1)
                .SingleAsync(m => m.ID == id); ;
        }

        public async Task<IEnumerable<IncomeTitle>> GetAllWithIncomeTitleByIncomeTitleIdAsync(int Id)
        {
            return await voteAccDbContext.IncomeTitles
                .Where(m => m.ID == Id && m.Status == 1)
                .ToListAsync();
        }

        public async Task<IEnumerable<IncomeTitle>> GetAllWithIncomeTitleByProgrammeIdAsync(int Id)
        {
            return await voteAccDbContext.IncomeTitles.Where(m => m.ProgrammeID == Id && m.Status == 1)
                .ToListAsync();
        }
        public async Task<IEnumerable<IncomeTitle>> GetAllWithIncomeTitleByMainLedgerAccountIdAsync(int Id, int sabhaId)
        {
            return await voteAccDbContext.IncomeTitles.Where(m => m.MainLedgerAccountID == Id && m.Status == 1 && m.SabhaID == sabhaId)
                .ToListAsync();
        }
        public async Task<IEnumerable<IncomeTitle>> GetAllWithIncomeTitleByClassificationIdAsync(int Id, int sabhaId)
        {
            return await voteAccDbContext.IncomeTitles.Where(m => m.ClassificationID == Id && m.Status == 1 && m.SabhaID == sabhaId)
                .ToListAsync();
        }

        public async Task<IEnumerable<IncomeTitle>> GetAllWithIncomeTitleByClassificationIdandMainLedgerAccountIdAsync(int ClassificationId, int CategoryId, int sabhaId)
        {
            return await voteAccDbContext.IncomeTitles.Where(m => m.ClassificationID == ClassificationId && m.MainLedgerAccountID == CategoryId && m.Status == 1 && m.SabhaID == sabhaId)
                .ToListAsync();
        }

        public async Task<IEnumerable<IncomeTitle>> GetAllWithIncomeTitleByProgrammeIdAndClassificationIdAsync(int ProgrammeId, int ClassificationId, int sabhaId)
        {
            return await voteAccDbContext.IncomeTitles.Where(m => m.ProgrammeID == ProgrammeId && m.ClassificationID == ClassificationId && m.Status == 1 && m.SabhaID == sabhaId)
                 .ToListAsync();
        }
        public async Task<IEnumerable<IncomeTitle>> GetAllWithIncomeTitleByProgrammeIdandSabhaIdAsync(int ProgrammeId, int SabhaId)
        {
            return await voteAccDbContext.IncomeTitles.Where(m => m.ProgrammeID == ProgrammeId && m.SabhaID == SabhaId && m.Status == 1)
                .ToListAsync();
        }
        public async Task<IEnumerable<IncomeTitle>> GetAllIncomeTitlesForSabhaIdAsync(int Id)
        {
            return await voteAccDbContext.IncomeTitles.Where(m => m.SabhaID == Id && m.Status == 1).ToListAsync();
        }

        public async Task<IEnumerable<IncomeTitle>> GetAllWithIncomeTitleByProgrammeClassificationMainLedgerAccountIdAsync(int ProgrammeId, int ClassificationId, int MainLedgerAccountID, int sabhaId)
        {
            if (ProgrammeId != 0 && ClassificationId == 0 && MainLedgerAccountID == 0)
            {
                var result = await voteAccDbContext.IncomeTitles.Where(m => m.ProgrammeID == ProgrammeId && m.SabhaID == sabhaId && m.Status == 1).ToListAsync();
                return result;
            }
            else if (ProgrammeId != 0 && ClassificationId != 0 && MainLedgerAccountID == 0)
            {
                var result = await voteAccDbContext.IncomeTitles.Where(m => m.ClassificationID == ClassificationId && m.ProgrammeID == ProgrammeId && m.SabhaID == sabhaId && m.Status == 1).ToListAsync();
                return result;
            }
            else if (ProgrammeId == 0 && ClassificationId != 0 && MainLedgerAccountID == 0)
            {
                var result = await voteAccDbContext.IncomeTitles.Where(m => m.ClassificationID == ClassificationId && m.SabhaID == sabhaId && m.Status == 1).ToListAsync();
                return result;
            }
            else if (ProgrammeId == 0 && ClassificationId != 0 && MainLedgerAccountID != 0)
            {
                var result = await voteAccDbContext.IncomeTitles.Where(m => m.ClassificationID == ClassificationId && m.MainLedgerAccountID == MainLedgerAccountID && m.SabhaID == sabhaId && m.Status == 1).ToListAsync();
                return result;
            }
            else if (ProgrammeId != 0 && ClassificationId != 0 && MainLedgerAccountID != 0)
            {
                var result = await voteAccDbContext.IncomeTitles.Where(m => m.ProgrammeID == ProgrammeId && m.ClassificationID == ClassificationId && m.MainLedgerAccountID == MainLedgerAccountID && m.SabhaID == sabhaId && m.Status==1).ToListAsync();
                return result;
            }
            else
            {
                return null;
            }
            
            }

        public async Task<IEnumerable<IncomeTitle>> GetAllIncomeTitlesForClassificationIdSabhaIdAsync(int classificationId, int sabhaId)
        {
            return await voteAccDbContext.IncomeTitles.Where(m =>m.SabhaID == sabhaId && m.Status == 1 && m.ClassificationID == classificationId).ToListAsync();
        }

        private VoteAccDbContext voteAccDbContext
        {
            get { return Context as VoteAccDbContext; }
        }
    }
}