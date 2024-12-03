using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Vote;
using CAT20.Core.Repositories.Vote;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Data.Repositories.Vote
{
    public class ClassificationRepository :Repository<Classification>,IClassificationRepository
    {
        public ClassificationRepository(DbContext context) : base(context)
        {
        }

        private VoteAccDbContext voteAccDbContext
        {
            get { return Context as VoteAccDbContext; }
        }

        public async  Task<IEnumerable<Classification>> GetAllClassifications()
        {
              return await voteAccDbContext.Classifications.Include(m=>m.MainLedgerAccount)
        .Where(m => m.Status == 1)
        .ToListAsync();
        }
       public async Task<Classification> GetClassificationById(int id)
        {
            return await voteAccDbContext.Classifications
        .Where(c => c.Status == 1 && c.Id == id)
        .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ClassificationAdvancedModel>> GetAllClassificationsWithLedgerAccountsForSabha(int sabhaId)
        {
            List<ClassificationAdvancedModel> ClassificationList = new List<ClassificationAdvancedModel>();
                
            var classifications = await voteAccDbContext.Classifications
              .Where(m => m.Status == 1)
              .ToListAsync();

            foreach (var classification in classifications)
            {
                var votesforclassificationId = await voteAccDbContext.VoteDetails
              .Where(m => m.Status == 1 && m.ClassificationID==classification.Id && m.SabhaID==sabhaId)
              .OrderBy(m => m.ProgrammeCode).OrderBy(m => m.IncomeSubtitleCode)
              .ToListAsync();

                ClassificationList.Add(new ClassificationAdvancedModel
                {
                    Id = classification.Id,
                    Code = classification.Code,
                    Description = classification.Description,
                    VoteDetails = votesforclassificationId.Select(v => new VoteDetailBasicModel
                    {
                        ID = v.ID,
                        Code = v.Code,
                        Description = v.NameEnglish,
                    }).ToList()
                });
            }
            return ClassificationList;
        }
    }
}
