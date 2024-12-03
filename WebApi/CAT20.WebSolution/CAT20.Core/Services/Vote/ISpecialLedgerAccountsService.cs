using CAT20.Core.DTO.Vote;
using CAT20.Core.DTO.Vote.Save;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.Vote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Vote
{
    public interface ISpecialLedgerAccountsService
    {
        Task<IEnumerable<SpecialLedgerAccountTypes>> GetSpecialLedgerAccountTypes();
        Task<IEnumerable<SpecialLedgerAccountsResource>> GetSpecialLedgerAccountsForSabaha(int sabahId);

        Task<(bool, string?)> AssignSpecialLedgerAccount(AssignSpecialLedgerAccount assignSpecialLedgerAccountResource, HTokenClaim token);
    }
}
