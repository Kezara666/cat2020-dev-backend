using CAT20.Core.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Mixin
{
    public interface IMixFinalAccountCorrectionService
    {

        Task<(bool, string?)> AlignLedgerAccountAndCashBooksForOlderReceipts(int month, HTokenClaim token);
    }
}
