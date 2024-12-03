using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.FinalAccount;

namespace CAT20.Core.Repositories.FinalAccount
{
    public interface IFixedAssetsRepository : IRepository<FixedAssets>
    {
        Task<FixedAssets> GetByIdAsync(int id);
        Task<(int totalCount, IEnumerable<FixedAssets> list)> GetAllFixedAssetsForSabha(int sabhaId, Nullable<int> ledgerAccountId, int pageNo, int pageSize, string? filterKeyword);
        Task<(int totalCount, IEnumerable<FixedAssets> list)> GetAllDepreciatedFixedAssetsForSabha(int sabhaId, Nullable<int> ledgerAccountId, int? year, int pageNo, int pageSize, string? filterKeyword);
        Task<(int totalCount, IEnumerable<FixedAssets> list)> GetAllDisposesFixedAssetsForSabha(int sabhaId, Nullable<int> ledgerAccountId,int? year, int pageNo, int pageSize, string? filterKeyword);
        Task<IEnumerable<FixedAssets>> GetForDepreciation(int? fixAssetsId, int sabhaId);

    }
}
