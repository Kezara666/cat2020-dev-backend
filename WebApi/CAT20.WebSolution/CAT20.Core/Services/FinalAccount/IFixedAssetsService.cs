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
    public interface IFixedAssetsService
    {
        Task<(int totalCount, IEnumerable<FixedAssetsResource> list)> GetAllFixedAssetsForSabha(int sabhaId, int ledgerAccountId, int pageNo, int pageSize, string? filterKeyword, HTokenClaim _token);
        Task<(bool, string?)> CreateUpdateFixedAssets(FixedAssets newFixAssests, HTokenClaim token);

        Task<(bool, string?)> DeleteFixedAssets(int fixedAssetsId, HTokenClaim token);
        Task<(bool, string?)> Depreciation(int? fixedAssetsId, HTokenClaim token);
        Task<(bool, string?)> Disposal(SaveFixedAssetsDisposalResource disposalRequest, HTokenClaim token);
    }
}
