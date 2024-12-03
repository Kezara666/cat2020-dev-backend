using CAT20.Core.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.FinalAccount
{
    public interface IShopRentalFinalAccountService
    {
        Task<bool> UpdateVoteAssignForFinalAccounting(int sabhaId, HTokenClaim token);
        Task<(bool, string)> ShopInit(int sabhaId, HTokenClaim token);
    }
}
