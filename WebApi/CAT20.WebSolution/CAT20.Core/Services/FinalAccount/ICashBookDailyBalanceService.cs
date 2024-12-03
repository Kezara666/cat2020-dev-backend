using CAT20.Core.Models.FinalAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.FinalAccount
{
    public interface ICashBookDailyBalanceService
    {
        Task<bool> Create(CashBookDailyBalance newCashBookDailyBalance);
        Task<bool> BulkCreate(IEnumerable<CashBookDailyBalance> newCashBookDailyBalanceList);
        Task<bool> GetTotalsAndCreateDailyCashBookDailyBalances(int officeId, int sessionId, DateTime date, int createdby);
    }
}
