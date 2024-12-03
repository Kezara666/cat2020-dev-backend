using CAT20.Core.DTO.Final;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.FinalAccount;
using CAT20.WebApi.Resources.Final;

namespace CAT20.Core.Services.FinalAccount;

public interface IVoucherChequeService
{
    Task<(int totalCount,IEnumerable<VoucherChequeResource> list)> getChequeForSabha(int sabhaId, bool stage, int pageNo, int  pageSize, string? filterKeyWord);
    Task<VoucherCheque> payVoucher(int id, bool status,MakeVoucherApproveRejectResource approval);

    Task<bool> PrintCheque(int id, HTokenClaim token);

}