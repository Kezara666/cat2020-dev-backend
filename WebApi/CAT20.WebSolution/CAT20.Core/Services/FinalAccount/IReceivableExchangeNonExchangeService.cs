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
    public interface IReceivableExchangeNonExchangeService
    {
        Task<(int totalCount, IEnumerable<ReceivableExchangeNonExchgangeResource> list)> GetAllReceivableExchangeNonExchangeForSabha(int sabhaId, int ledgerAccountId, int pageNo, int pageSize, string? filterKeyword, HTokenClaim _token);
        Task<(bool, string?)> CreateUpdateReceivableExchangeNonExchange(SaveReceivableExchangeNonExchangeResource newReceivableExchangeResource, HTokenClaim token);

        Task<(bool, string?)> DeleteReceivableExchangeNonExchange(int receivableExchangeNonExchangeId, HTokenClaim token);
    }
}
