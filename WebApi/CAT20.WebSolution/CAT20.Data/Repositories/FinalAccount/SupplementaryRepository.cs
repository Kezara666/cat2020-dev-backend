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
    public class SupplementaryRepository : Repository<Supplementary>, ISupplementaryRepository
    {
        public SupplementaryRepository(DbContext context) : base(context)
        {
        }

        public  async Task<(int totalCount, IEnumerable<Supplementary> list)> GetAllSupplementaryForSabha(int sabhaId, int pageNo, int pageSize, string? filterKeyWord)
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


            var result = voteAccDbContext.Supplementary
                .Where(m => (m.SabahId == sabhaId && m.ActionState != VoteTransferActions.withdraw)
                            && (string.IsNullOrEmpty(keyword) ||
                                (EF.Functions.Like(m.Id, keyword)) ||
                                (EF.Functions.Like(m.SPLNo, keyword))
                            )).OrderByDescending(m => m.Id);



            int totalCount = await result.CountAsync();

            //var pageSize = 10;
            int skipAmount = (pageNo - 1) * pageSize;

            var list = await result.Skip(skipAmount).Take(pageSize).ToListAsync();


            return (totalCount, list);
        }

        public async Task<(int totalCount, IEnumerable<Supplementary> list)> GetSupplementaryForApproval(int sabhaId, int pageNo, int pageSize, string? filterKeyWord)
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


            var result = voteAccDbContext.Supplementary
                .Where(m => (m.SabahId == sabhaId && m.ActionState == VoteTransferActions.Init)
                            && (string.IsNullOrEmpty(keyword) ||
                                (EF.Functions.Like(m.Id, keyword)) ||
                                (EF.Functions.Like(m.SPLNo, keyword))
                            )).OrderByDescending(m => m.Id);



            int totalCount = await result.CountAsync();

            //var pageSize = 10;
            int skipAmount = (pageNo - 1) * pageSize;

            var list = await result.Skip(skipAmount).Take(pageSize).ToListAsync();


            return (totalCount, list);
        }

        private VoteAccDbContext voteAccDbContext
        {
            get { return Context as VoteAccDbContext; }
        }
    }
}
