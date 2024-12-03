using CAT20.Core.Models.FinalAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.FinalAccount
{
    public interface IVoteLedgerBookDailyBalanceService
    {
        Task<bool> Create(VoteLedgerBookDailyBalance newVoteLedgerBookDailyBalance);
        Task<bool> BulkCreate(IEnumerable<VoteLedgerBookDailyBalance> newVoteLedgerBookDailyBalanceList);
        Task<bool> GetTotalsAndCreateDailyVoteLedgerBookDailyBalances(int officeId, int sessionId, DateTime date, int createdby);
    }
}
