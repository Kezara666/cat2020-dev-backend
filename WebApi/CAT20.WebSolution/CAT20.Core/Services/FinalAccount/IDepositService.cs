using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.Mixin;
using CAT20.Core.Models.Vote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.WebApi.Resources.Final;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.Control;
using CAT20.Core.DTO.Final;
using CAT20.WebApi.Resources.FInalAccount.Save;

namespace CAT20.Core.Services.FinalAccount
{
    public interface  IDepositService 
    {

        Task<(int totalCount, IEnumerable<DepositResource> list)> GetAllDepositsForSabha(int sabhaId, List<int?> excludedIds, Nullable<int> depositSubCategoryId, Nullable<int> ledgerAccountId, int pageNo, int pageSize, string? filterKeyword, HTokenClaim token);
        Task<(int totalCount, IEnumerable<DepositResource> list)> GetAllDepositsToRepaymentForSabha(int sabhaId, List<int?> excludedIds, Nullable<int> depositSubCategoryId, Nullable<int> ledgerAccountId, int pageNo, int pageSize, string? filterKeyword, HTokenClaim token);
        Task<Deposit> GetDepositById(int Id);
        Task<(bool,string?)> CreateUpdateDeposit(SaveDepositResource deposit,HTokenClaim token);
        Task<bool> DeleteDeposit(int depositId,HTokenClaim token);

    }
}
