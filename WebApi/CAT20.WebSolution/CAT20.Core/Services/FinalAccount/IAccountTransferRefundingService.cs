using CAT20.Core.HelperModels;
using CAT20.Core.Models.FinalAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.FinalAccount
{
    public interface IAccountTransferRefundingService
    {
        Task<(bool, string)> Create(AccountTransferRefunding newRefunding, HTokenClaim token);

        Task<bool> WithdrawAccountRefundingTransfer(int Id, HTokenClaim token);

    }
}
