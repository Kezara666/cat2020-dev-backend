using CAT20.Core.Models.FinalAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.FinalAccount
{
    public interface ISingleOpenBalanceRepository:IRepository<SingleOpenBalance>
    {
        Task<(int totalCount, IEnumerable<SingleOpenBalance> list)> GetAllSingleOpenBalancesForSabha(int sabhaId, Nullable<int> ledgerAccountId, int pageNo, int pageSize, string? filterKeyword);
    }
}
