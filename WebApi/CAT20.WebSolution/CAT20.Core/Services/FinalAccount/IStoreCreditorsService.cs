using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.DTO.Final;
using CAT20.Core.DTO.Final.Save;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.FinalAccount;

namespace CAT20.Core.Services.FinalAccount
{
    public interface IStoreCreditorsService
    {
        Task<(int totalCount, IEnumerable<StoresCreditorResource> list)> GetAllStoreCreditorsServiceForSabha(int sabhaId, int ledgerAccountId, int pageNo, int pageSize, string? filterKeyword, HTokenClaim _token);
        Task<(bool, string?)> CreateUpdateStoreCreditor(SaveStoresCreditor storeCreditorResource, HTokenClaim token);

        Task<(bool, string?)> DeleteStoreCreditor(int storeCreditorId, HTokenClaim token);
    }
}

