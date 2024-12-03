using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Repositories.FinalAccount;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Vote;
using CAT20.Core.Models.Control;
using CAT20.WebApi.Resources.Final;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CAT20.Data.Repositories.FinalAccount
{
    public class DepositRepository : Repository<Deposit>, IDepositRepository
    {
        public DepositRepository(DbContext context) : base(context)
        {
        }
        private VoteAccDbContext voteAccDbContext
        {
            get { return Context as VoteAccDbContext; }
        }



        public async  Task<(int totalCount, IEnumerable<Deposit> list)> GetAllDepositsForSabha(int sabhaId, List<int?> excludedIds, Nullable<int> depositSubCategoryId, Nullable<int> ledgerAccountId, int pageNo, int pageSize, string? filterKeyword)
        {
            var result = voteAccDbContext.Deposit
                   .Where(m => m.SabhaId == sabhaId && m.Status == 1)
                   .Where(a => !excludedIds.Contains(a.Id))
                   .Where(a => depositSubCategoryId.HasValue ? a.DepositSubCategoryId == depositSubCategoryId.Value : true)
                   .Where(a => ledgerAccountId.HasValue ? a.LedgerAccountId == ledgerAccountId.Value : true)
                    .Where(pt => EF.Functions.Like(pt.ReceiptNo!, "%" + filterKeyword + "%") || EF.Functions.Like(pt.DepositDate!, "%" + filterKeyword + "%") || EF.Functions.Like(pt.InitialDepositAmount, "%" + filterKeyword + "%"))
                  .OrderByDescending(m => m.Id);


            int totalCount = await result.CountAsync();


            //var pageSize = 10;
            int skipAmount = (pageNo - 1) * pageSize;

            var list = await result.Skip(skipAmount).Take(pageSize).ToListAsync();


            return (totalCount, list);
        }

        public async Task<(int totalCount, IEnumerable<Deposit> list)> GetAllDepositsToRepaymentForSabha(int sabhaId, List<int?> excludedIds, Nullable<int> depositSubCategoryId, Nullable<int> ledgerAccountId, int pageNo, int pageSize, string? filterKeyword)
        {
            var result = voteAccDbContext.Deposit
                   .Where(m => m.SabhaId == sabhaId && m.Status == 1)
                   .Where(a => !excludedIds.Contains(a.Id))
                   .Where(a=>a.InitialDepositAmount != a.ReleasedAmount)
                   .Where(a => depositSubCategoryId.HasValue ? a.DepositSubCategoryId == depositSubCategoryId.Value : true)
                   .Where(a => ledgerAccountId.HasValue ? a.LedgerAccountId == ledgerAccountId.Value : true)
                    .Where(pt => EF.Functions.Like(pt.ReceiptNo!, "%" + filterKeyword + "%") || EF.Functions.Like(pt.DepositDate!, "%" + filterKeyword + "%") || EF.Functions.Like(pt.InitialDepositAmount, "%" + filterKeyword + "%"))
                  .OrderByDescending(m => m.Id);


            int totalCount = await result.CountAsync();


            //var pageSize = 10;
            int skipAmount = (pageNo - 1) * pageSize;

            var list = await result.Skip(skipAmount).Take(pageSize).ToListAsync();


            return (totalCount, list);
        }


        public async Task<Deposit> GetDepositByIdAsync(int? id)
        {
            return await voteAccDbContext.Deposit
                .Where(m => m.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Deposit>> ClearDepots(int mxOrderId)
        {
            return await voteAccDbContext.Deposit
                .Where(m => m.MixOrderId == mxOrderId && m.Status==1)
                .ToListAsync();
        }

    }
    
   
}

