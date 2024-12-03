using CAT20.Core.DTO.Final;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.FinalAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.FinalAccount
{
    public interface IAccountTransferService
    {
        Task<(bool,string)> Create(AccountTransfer newTransfer, HTokenClaim token);

        Task<AccountTransferResource> GetAccountTransferById(int Id, HTokenClaim token);

        Task<(int totalCount, IEnumerable<AccountTransferResource> list)> GetAllAccountTransferForSabha(int sabhaId,bool? type, int pageNo, int pageSize, string? filterKeyWord);

        Task<bool> WithdrawAccountTransfer(int Id, HTokenClaim token);
    }
}
