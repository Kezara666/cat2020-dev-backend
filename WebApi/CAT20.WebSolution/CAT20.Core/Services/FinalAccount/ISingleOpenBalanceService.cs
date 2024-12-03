using CAT20.Core.DTO.Final;
using CAT20.Core.DTO.Final.Save;
using CAT20.Core.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.FinalAccount
{
    public interface ISingleOpenBalanceService
    {
        Task<(bool, string?)> CreateUpdate(SaveSingleOpenBalanceResource newBalanceResource, HTokenClaim token);
        Task<(int totalCount, IEnumerable<SingleOpenBalanceResource> list)> GetAllSingleOpenBalancesForSabha(int sabhaId, Nullable<int> ledgerAccountId, int pageNo, int pageSize, string? filterKeyword, HTokenClaim token);
        Task<(bool, string?)> DeleteSingleOpenBalances(int Id, HTokenClaim token);
    }
}
