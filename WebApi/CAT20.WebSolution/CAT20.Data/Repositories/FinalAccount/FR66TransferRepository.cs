using CAT20.Core.HelperModels;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.Vote;
using CAT20.Core.Repositories.FinalAccount;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.FinalAccount
{
    public class FR66TransferRepository : Repository<FR66Transfer>, IFR66TransferRepository
    {
        public FR66TransferRepository(DbContext context) : base(context)
        {
        }

   

        public async Task<(int totalCount, IEnumerable<FR66Transfer> list)> GetFR66TransferFroApproval(int sabhaId, int pageNo, int pageSize, string? filterKeyWord)
        {
            if (filterKeyWord != "undefined")
            {
                filterKeyWord = "%" + filterKeyWord + "%";
            }
            else if (filterKeyWord == "undefined")
            {
                filterKeyWord = null;
            }

            var keyword = filterKeyWord ?? "";


            var result = voteAccDbContext.FR66Transfer
                .Include(m => m.FR66FromItems)
                .Include(m => m.FR66ToItems)
                .Where(m => (m.SabahId == sabhaId && m.ActionState == VoteTransferActions.Init)
                            && (string.IsNullOrEmpty(keyword) ||
                                (EF.Functions.Like(m.Id, keyword)) ||
                                (EF.Functions.Like(m.FR66No, keyword))
                            )).OrderByDescending(m => m.Id);

            
            
            int totalCount = await result.CountAsync();

            //var pageSize = 10;
            int skipAmount = (pageNo - 1) * pageSize;

            var list = await result.Skip(skipAmount).Take(pageSize).ToListAsync();


            return (totalCount, list);
        }

        public async Task<(int totalCount, IEnumerable<FR66Transfer> list)> GetAllFR66TransferForSabha(int sabhaId, int pageNo, int pageSize, string? filterKeyWord)
        {
            if (filterKeyWord != "undefined")
            {
                filterKeyWord = "%" + filterKeyWord + "%";
            }
            else if (filterKeyWord == "undefined")
            {
                filterKeyWord = null;
            }

            var keyword = filterKeyWord ?? "";


            var result = voteAccDbContext.FR66Transfer
                //.Include(m => m.VoucherLog.Where(cl => cl.Action == VoucherStatus.Created))
                .Include(m => m.FR66FromItems)
                .Include(m => m.FR66ToItems)
                .Where(m => (m.SabahId == sabhaId && m.ActionState != VoteTransferActions.withdraw)
                            && (string.IsNullOrEmpty(keyword) ||
                                (EF.Functions.Like(m.Id, keyword)) ||
                                (EF.Functions.Like(m.FR66No, keyword))
                            )).OrderByDescending(m => m.Id);

            int totalCount = await result.CountAsync();

            //var pageSize = 10;
            int skipAmount = (pageNo - 1) * pageSize;

            var list = await result.Skip(skipAmount).Take(pageSize).ToListAsync();


            return (totalCount, list);
        }

        public async Task<FR66Transfer> GetFR66TransferById(int id, HTokenClaim token)
        {
            return await voteAccDbContext.FR66Transfer
                 .Include(m => m.FR66FromItems)
                .Include(m => m.FR66ToItems)
                .Where(j => j.Id == id && j.SabahId == token.sabhaId).FirstOrDefaultAsync();
        }

        private VoteAccDbContext voteAccDbContext
        {
            get { return Context as VoteAccDbContext; }
        }
    }
}
