﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Repositories.FinalAccount;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Data.Repositories.FinalAccount
{
    public class IndustrialDebtorsRepository : Repository<IndustrialDebtors>, IIndustrialDebtorsRepository
    {
        public IndustrialDebtorsRepository(DbContext context) : base(context)
        {
        }
        private VoteAccDbContext voteAccDbContext
        {
            get { return Context as VoteAccDbContext; }
        }
        public async Task<(int totalCount, IEnumerable<IndustrialDebtors> list)> GetAllIndustrialDebtorsForSabha(int sabhaId, Nullable<int> ledgerAccountId, int pageNo, int pageSize, string? filterKeyword)
        {
            var result = voteAccDbContext.IndustrialDebtors
                    .Include(m => m.DebtorType)
                    //.Include(m => m.FundSource)
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