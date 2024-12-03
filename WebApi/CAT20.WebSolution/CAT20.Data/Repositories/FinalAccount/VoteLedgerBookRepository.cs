using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Repositories;
using CAT20.Core.Repositories.FinalAccount;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.FinalAccount
{
    public class VoteLedgerBookRepository : Repository<VoteLedgerBook>, IVoteLedgerBookRepository
    {
        public async Task<IEnumerable<VoteLedgerBookDailyBalance>> GetDailyBalancesAsync(int officeId, int sessionId, DateTime date, int createdby)
        {
            return await voteAccDbContext.VoteLedgerBook
                .Where(lb => lb.OfiiceId == officeId && lb.SessionId == sessionId && lb.Date.Date == date.Date.Date)
                .GroupBy(lb => new { lb.SabhaId, lb.OfiiceId, lb.SessionId, lb.Date.Date, lb.VoteDetailId, lb.VoteBalanceId })
                .Select(group => new VoteLedgerBookDailyBalance
                {
                    SabhaId = group.Key.SabhaId,
                    OfficeId = group.Key.OfiiceId,
                    SessionId = group.Key.SessionId,
                    Date = group.Key.Date.Date,
                    CreatedBy = createdby,
                    CreatedAt = DateTime.Now,
                    VoteBalanceId = group.Key.VoteBalanceId,
                    VoteDetailId = group.Key.VoteDetailId,
                    TotalDailyCredit = group.Sum(lb => lb.Credit),
                    TotalDailyDebit = group.Sum(lb => lb.Debit),
                    RunningTotal = group.Sum(lb => lb.Debit - lb.Credit),
                })
                .ToListAsync();
        }
        public VoteLedgerBookRepository(DbContext context) : base(context)
        {
        }
        private VoteAccDbContext voteAccDbContext
        {
            get { return Context as VoteAccDbContext; }
        }
    }
}
