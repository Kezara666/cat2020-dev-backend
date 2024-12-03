using CAT20.Core.Models.Vote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.Vote
{
    public interface ISpecialLedgerAccountsRepository : IRepository<SpecialLedgerAccounts>
    {
        Task<IEnumerable<SpecialLedgerAccountTypes>> GetSpecialLedgerAccountTypes();
        Task<IEnumerable<SpecialLedgerAccounts>> GetSpecialLedgerAccountsForSabaha(int sabahId);

        Task<SpecialLedgerAccounts> GetAccumulatedFundLedgerAccount(int sabahId);
        Task<SpecialLedgerAccounts> GetStampLedgerAccount(int sabahId);

        Task<SpecialLedgerAccounts> GetOtherReceiptsLedgerAccount(int sabahId);
    }
}
