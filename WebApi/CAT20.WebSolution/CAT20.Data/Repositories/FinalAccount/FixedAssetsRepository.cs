using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Repositories.FinalAccount;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Data.Repositories.FinalAccount
{
    public class FixedAssetsRepository : Repository<FixedAssets>,IFixedAssetsRepository
    {
        public FixedAssetsRepository(DbContext context) : base(context)
        {
        }
        private VoteAccDbContext voteAccDbContext
        {
            get { return Context as VoteAccDbContext; }
        }

        public async Task<(int totalCount, IEnumerable<FixedAssets> list)> GetAllDepreciatedFixedAssetsForSabha(int sabhaId, int? ledgerAccountId, int? year, int pageNo, int pageSize, string? filterKeyword)
        {
            var result = voteAccDbContext.FixedAssets
                   .Where(m => m.SabhaId == sabhaId && m.Status == 2)


                  .OrderByDescending(m => m.Id);


            int totalCount = await result.CountAsync();

            int skipAmount = (pageNo - 1) * pageSize;

            var list = await result.Skip(skipAmount).Take(pageSize).ToListAsync();


            return (totalCount, list);
        }

        public async Task<(int totalCount, IEnumerable<FixedAssets> list)> GetAllDisposesFixedAssetsForSabha(int sabhaId, int? ledgerAccountId, int? year, int pageNo, int pageSize, string? filterKeyword)
        {
            var result = voteAccDbContext.FixedAssets
                  .Where(m => m.SabhaId == sabhaId && m.Status == 3)


                 .OrderByDescending(m => m.Id);


            int totalCount = await result.CountAsync();

            int skipAmount = (pageNo - 1) * pageSize;

            var list = await result.Skip(skipAmount).Take(pageSize).ToListAsync();


            return (totalCount, list);

        }

        public async Task<(int totalCount, IEnumerable<FixedAssets> list)> GetAllFixedAssetsForSabha(int sabhaId, Nullable<int> ledgerAccountId, int pageNo, int pageSize, string? filterKeyword)
        {
            var result = voteAccDbContext.FixedAssets
                    .Where(m => m.SabhaId == sabhaId && m.Status == 1)


                   .OrderByDescending(m => m.Id);


            int totalCount = await result.CountAsync();


            ////var pageSize = 10;
            int skipAmount = (pageNo - 1) * pageSize;

            var list = await result.Skip(skipAmount).Take(pageSize).ToListAsync();


            return (totalCount, list);
            //return (0, new List<Stores>());
        }

        public async Task<FixedAssets> GetByIdAsync(int id)
        {
            return await voteAccDbContext.FixedAssets
                    .Where(m => m.Id == id && m.Status == 1)
                    .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<FixedAssets>> GetForDepreciation(int? fixAssetsId, int sabhaId)
        {
            return await voteAccDbContext.FixedAssets
                   .Where(m => fixAssetsId.HasValue ? m.Id == fixAssetsId:true)
                   .Where(m => m.SabhaId == sabhaId && m.Status == 1)
                   .ToListAsync(); 
        }
    }
}
