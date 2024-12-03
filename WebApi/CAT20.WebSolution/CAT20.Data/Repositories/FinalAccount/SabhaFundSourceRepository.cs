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
    public class SabhaFundSourceRepository : Repository<SabhaFundSource>, ISabhaFundSourceRepository
    {
        public SabhaFundSourceRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<SabhaFundSource>> GetAllActive()
        {
            return await voteAccDbContext.SabhaFundSource.Where(x => x.Status==1).ToListAsync();
        }

        private VoteAccDbContext voteAccDbContext
        {
            get { return Context as VoteAccDbContext; }
        }
    }
}
