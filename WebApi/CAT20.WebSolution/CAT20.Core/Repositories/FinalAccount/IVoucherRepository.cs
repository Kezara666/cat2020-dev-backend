using CAT20.Core.DTO.Final;
using CAT20.Core.Models.FinalAccount;
using CAT20.WebApi.Resources.Final;

namespace CAT20.Core.Repositories.FinalAccount;

public interface IVoucherRepository: IRepository<Voucher>
{
    Task<(int totalCount,IEnumerable<Voucher> list)> getVoucherForApproval(int sabhaId, List<int?> excludedIds, int? category, int stage, int pageNo, int  pageSize, string? filterKeyWord);

    Task<(int totalCount, IEnumerable<Voucher> list)> searchVoucherByKeywordForSurcharge(int sabhaId, int pageNo, int pageSize, string? filterKeyWord);

    Task<(int totalCount,IEnumerable<Voucher> list)> getVoucherProgressRejected(int sabhaId, List<int?> stages, int pageNo, int  pageSize, string? filterKeyWord);
    Task<Voucher> getVoucherById(int id);

    Task<IEnumerable<Voucher>> getVoucherForPsReport(int sabhaId, int year, int month);

    Task<IEnumerable<Voucher>> GetVoucherBySubVouchers(List<int> subVoucherIds);

}