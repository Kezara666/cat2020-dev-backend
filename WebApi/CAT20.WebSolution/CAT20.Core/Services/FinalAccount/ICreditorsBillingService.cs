using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.DTO.Final;
using CAT20.Core.DTO.Final.Save;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.FinalAccount;
using Newtonsoft.Json.Linq;

namespace CAT20.Core.Services.FinalAccount
{
    public interface ICreditorsBillingService
    {
        Task<(int totalCount, IEnumerable<CreditorBillingResource> list)> GetAllCreditorsForSabha(int sabhaId, Nullable<int> ledgerAccountId, int pageNo, int pageSize, string? filterKeyword, HTokenClaim _token);
        Task<(bool, string?)> CreateUpdateCreditor(SaveCreditorBillingResource newCreditor, HTokenClaim token);
        Task<CreditorBilling> GetCreditorById(int Id);

        Task<(bool, string?)> DeleteCreditorBilling(int Id, HTokenClaim token);
  
    }
}
