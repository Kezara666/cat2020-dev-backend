using CAT20.Core.Models.FinalAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.FinalAccount
{
    public interface IVoteLedgerBookService
    {
        Task<IEnumerable<VoteLedgerBookDailyBalance>> GetDailyBalances(int officeId, int sessionId, DateTime date, int createdby);
    }
}
