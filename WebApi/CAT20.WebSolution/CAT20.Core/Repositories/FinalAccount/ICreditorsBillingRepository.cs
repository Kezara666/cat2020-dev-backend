using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.FinalAccount;

namespace CAT20.Core.Repositories.FinalAccount
{
    public interface ICreditorsBillingRepository : IRepository<CreditorBilling>
    {

        Task<(int totalCount, IEnumerable<CreditorBilling> list)> GetAllCreditorsForSabha(int sabhaId, Nullable<int> ledgerAccountId, int pageNo, int pageSize, string? filterKeyword);

    }
}
