using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Repositories.FinalAccount;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Data.Repositories.FinalAccount
{
    public class IndustrialCreditorsRepository :Repository<IndustrialCreditors>,IIndustrialCreditorsRepository
    {
        public IndustrialCreditorsRepository(DbContext context) : base(context)
        {
        }
        private VoteAccDbContext voteAccDbContext
        {
            get { return Context as VoteAccDbContext; }
        }

        public async Task<(int totalCount, IEnumerable<IndustrialCreditors> list)> GetAllIndustrialCreditorsForSabha(int sabhaId, Nullable<int> ledgerAccountId, int pageNo, int pageSize, string? filterKeyword)
        {
            var result = voteAccDbContext.IndustrialCreditors
                    //.Include(m => m.FundSource)
                    .Include(m => m.CreditorType)
                    .Where(m => m.SabhaId == sabhaId && m.Status == 1)
                    .OrderByDescending(m => m.Id);


            int totalCount = await result.CountAsync();


            ////var pageSize = 10;
            int skipAmount = (pageNo - 1) * pageSize;

            var list = await result.Skip(skipAmount).Take(pageSize).ToListAsync();


            return (totalCount, list);
            //return (0, new List<Stores>());
        }
    }
}
