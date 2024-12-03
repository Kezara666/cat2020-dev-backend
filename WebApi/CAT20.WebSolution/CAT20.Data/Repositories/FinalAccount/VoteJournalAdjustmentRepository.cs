using CAT20.Core.DTO.Final;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.Vote;
using CAT20.Core.Repositories.FinalAccount;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.FinalAccount
{
    public class VoteJournalAdjustmentRepository : Repository<VoteJournalAdjustment>, IVoteJournalAdjustmentRepository
    {
        public VoteJournalAdjustmentRepository(DbContext context) : base(context)
        {
        }

        public Task<VoteBalance> GetForJournalAdjustment(int voteDetailId, int year)
        {
           return voteAccDbContext.VoteBalances.Where(vb=>vb.VoteDetailId== voteDetailId && vb.Year== year && vb.Status == VoteBalanceStatus.Active).FirstOrDefaultAsync();
        }

        public async Task<(int totalCount, IEnumerable<VoteJournalAdjustment> list)> GetJournalAdjustmentFroApproval(int sabhaId,  int pageNo, int pageSize, string? filterKeyWord)
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


            var result = voteAccDbContext.VoteJournalAdjustments
                //.Include(m => m.VoucherLog.Where(cl => cl.Action == VoucherStatus.Created))
                .Include(m => m.VoteJournalItemsFrom)
                .Include(m => m.VoteJournalItemsTo)
                .Where(m => (m.SabahId == sabhaId && m.ActionState == VoteJournalAdjustmentActions.Init)
                            && (string.IsNullOrEmpty(keyword) ||
                                (EF.Functions.Like(m.Id, keyword)) ||
                                (EF.Functions.Like(m.JournalNo, keyword))
                            )).OrderByDescending(m => m.Id);

            int totalCount = await result.CountAsync();

            //var pageSize = 10;
            int skipAmount = (pageNo - 1) * pageSize;

            var list = await result.Skip(skipAmount).Take(pageSize).ToListAsync();


            return (totalCount, list);
        }

        public async Task<(int totalCount, IEnumerable<VoteJournalAdjustment> list)> GetAllJournalAdjustmentForSabha(int sabhaId,  int pageNo, int pageSize, string? filterKeyWord)
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


            var result =  voteAccDbContext.VoteJournalAdjustments
                //.Include(m => m.VoucherLog.Where(cl => cl.Action == VoucherStatus.Created))
                .Include(m => m.VoteJournalItemsFrom)
                .Include(m => m.VoteJournalItemsTo)
                .Where(m => (m.SabahId == sabhaId && m.ActionState != VoteJournalAdjustmentActions.withdraw)
                            && (string.IsNullOrEmpty(keyword) ||
                                (EF.Functions.Like(m.Id, keyword)) ||
                                (EF.Functions.Like(m.JournalNo, keyword))
                            )).OrderByDescending(m => m.Id);

            int totalCount = await result.CountAsync();

            //var pageSize = 10;
            int skipAmount = (pageNo - 1) * pageSize;

            var list = await result.Skip(skipAmount).Take(pageSize).ToListAsync();


            return (totalCount, list);

        }

        public async Task<VoteJournalAdjustment> GetJournalAdjustmentById(int id, HTokenClaim token)
        {
            return await voteAccDbContext.VoteJournalAdjustments
                 .Include(m => m.VoteJournalItemsFrom)
                .Include(m => m.VoteJournalItemsTo)
                .Where(j => j.Id == id && j.SabahId==token.sabhaId).FirstOrDefaultAsync();
        }

        private VoteAccDbContext voteAccDbContext
        {
            get { return Context as VoteAccDbContext; }
        }
    }
}
