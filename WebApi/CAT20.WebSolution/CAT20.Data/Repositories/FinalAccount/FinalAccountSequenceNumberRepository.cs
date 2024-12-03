using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Repositories.FinalAccount;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.FinalAccount
{
    public class FinalAccountSequenceNumberRepository : Repository<FinalAccountSequenceNumber>, IFinalAccountSequenceNumberRepository
    {
        public FinalAccountSequenceNumberRepository(DbContext context) : base(context)
        {
        }

        private VoteAccDbContext voteAccDbContext
        {
            get { return Context as VoteAccDbContext; }
        }

        public async Task<FinalAccountSequenceNumber> GetNextSequenceNumberForYearSabhaModuleType(int year, int? sabhaId, FinalAccountModuleType moduleType)
        {
            return await voteAccDbContext.FinalAccountSequenceNumber
                .Where(m => m.Year == year && m.SabhaId == sabhaId && m.ModuleType==moduleType)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> HasSequenceNumberForCurrentYearAndModule(int year, int? sabhaId, FinalAccountModuleType moduleType)
        {
            return await voteAccDbContext.FinalAccountSequenceNumber
                                        .AnyAsync(m => m.Year == year && m.SabhaId == sabhaId && m.ModuleType == moduleType);
        }
    }
}
