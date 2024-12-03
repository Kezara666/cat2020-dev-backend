using CAT20.Core.HelperModels;
using CAT20.Core.Models.FinalAccount;

namespace CAT20.Core.Repositories.FinalAccount;

public interface IVoucherChequeRepository: IRepository<VoucherCheque>
{

    Task<bool> HasChequeeNumberExist(string chequeNumber,HTokenClaim token);
    Task<VoucherCheque> getVoucherById(int id);
    Task<(int totalCount,IEnumerable<VoucherCheque> list)> getChequeForSabha(int sabhaId, bool stage, int pageNo, int  pageSize, string? filterKeyWord);

}