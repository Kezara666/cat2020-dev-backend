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
    public class IndustrialCreditorsDebtorsTypesRepository : Repository<IndustrialCreditorsDebtorsTypes>, IIndustrialCreditorsDebtorsTypesRepository
    {
        public IndustrialCreditorsDebtorsTypesRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<IndustrialCreditorsDebtorsTypes>> GetAllCreditorsTypesActive()
        {
            /*status 1  used for creditor types*/
            return await voteAccDbContext.IndustrialCreditorsDebtorsTypes.Where(x => x.Status==1).ToListAsync();
        }

        public async Task<IEnumerable<IndustrialCreditorsDebtorsTypes>> GetAllDebtorsTypesActive()
        {
                /*status 2  used for debtor types*/
            return await voteAccDbContext.IndustrialCreditorsDebtorsTypes.Where(x => x.Status == 2).ToListAsync();
        }

        private VoteAccDbContext voteAccDbContext
        {
            get { return Context as VoteAccDbContext; }
        }
    }
}
