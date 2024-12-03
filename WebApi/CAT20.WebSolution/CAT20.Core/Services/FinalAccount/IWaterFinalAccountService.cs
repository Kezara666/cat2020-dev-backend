using CAT20.Core.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.FinalAccount
{
    public interface IWaterFinalAccountService
    {

        Task<bool> UpdateVoteAssignForFinalAccounting(int sabhaId, HTokenClaim token);
        Task<bool> WaterInit(int sabhaId,HTokenClaim token);
    }
}
