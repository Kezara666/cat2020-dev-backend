using CAT20.Core.DTO.Final.Save;
using CAT20.Core.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.FinalAccount
{
    public interface IFinalAccountAdjustmentService 
    {
        Task<(bool, string?)> CreateUpdateStockExpenditureAdjustment(SaveStockExpenditureAdjustment adjustment, HTokenClaim token);
    }
}
