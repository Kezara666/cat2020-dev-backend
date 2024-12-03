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
    public class AccountTransferRepository : Repository<AccountTransfer>, IAccountTransferRepository
    {
        public AccountTransferRepository(DbContext context) : base(context)
        {
        }

        public async Task<(int totalCount, IEnumerable<AccountTransfer> list)> GetAllAccountTransferForSabha(int sabhaId, bool? type, int pageNo, int pageSize, string? filterKeyWord)
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


            var result = voteAccDbContext.AccountTransfer
                .Include(m => m.AccountTransferRefunding)
                //.Where(m => (m.SabhaId == sabhaId && m.ActionState == VoteTransferActions.Init)
                //            && (string.IsNullOrEmpty(keyword) ||
                //                (EF.Functions.Like(m.Id, keyword)) ||
                //                (EF.Functions.Like(m.VoucherId, keyword))
                //            ))
                .Where(m => type.HasValue ? m.IsRefund == type : true)
                .OrderByDescending(m => m.Id);



            int totalCount = await result.CountAsync();

            //var pageSize = 10;
            int skipAmount = (pageNo - 1) * pageSize;

            var list = await result.Skip(skipAmount).Take(pageSize).ToListAsync();


            return (totalCount, list);

        }

        public Task<AccountTransfer> GetAccountTransferById(int id)
        {
            return voteAccDbContext.AccountTransfer
                .Include(m => m.AccountTransferRefunding)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public Task<AccountTransfer> GetAccountTransferByVoucherId(int voucherId)
        {
            return voteAccDbContext.AccountTransfer
                .Include(m => m.AccountTransferRefunding)
                .FirstOrDefaultAsync(m => m.VoucherId == voucherId);
        }

        private VoteAccDbContext voteAccDbContext
        {
            get { return Context as VoteAccDbContext; }
        }

    }
}
