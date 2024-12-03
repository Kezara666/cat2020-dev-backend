using CAT20.Core.Models.Enums;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using CAT20.WebApi.Resources.Final;

namespace CAT20.Core.Repositories.FinalAccount
{
    public interface IDepositRepository : IRepository<Deposit>
    {
        Task<(int totalCount, IEnumerable<Deposit> list)> GetAllDepositsForSabha(int sabhaId, List<int?> excludedIds, Nullable<int> depositSubCategoryId, Nullable<int> ledgerAccountId, int pageNo, int pageSize, string? filterKeyword);
        Task<(int totalCount, IEnumerable<Deposit> list)> GetAllDepositsToRepaymentForSabha(int sabhaId, List<int?> excludedIds, Nullable<int> depositSubCategoryId, Nullable<int> ledgerAccountId, int pageNo, int pageSize, string? filterKeyword);
        Task<Deposit> GetDepositByIdAsync(int? id);

        Task<IEnumerable<Deposit>> ClearDepots(int mxOrderId);

    }
}
