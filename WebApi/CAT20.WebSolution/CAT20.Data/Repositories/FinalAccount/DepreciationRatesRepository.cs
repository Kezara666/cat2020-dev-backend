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
    public class DepreciationRatesRepository : Repository<DepreciationRates>, IDepreciationRatesRepository
    {
        public DepreciationRatesRepository(DbContext context) : base(context)
        {
        }

        public async Task<DepreciationRates> GetDepreciationRate(string subtitle, int? sabhaId)
        {
            return await voteAccDbContext.DepreciationRates
                .Where(m => m.SubTitleCode == subtitle && m.SabhaId == sabhaId)
                .FirstOrDefaultAsync();
        }

        private VoteAccDbContext voteAccDbContext
        {
            get { return Context as VoteAccDbContext; }
        }
    }
}
