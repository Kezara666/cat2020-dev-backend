using CAT20.Core.HelperModels;
using CAT20.Core.Models.FinalAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.FinalAccount
{
    public interface IFR66TransferRepository:IRepository<FR66Transfer>
    {
        Task<FR66Transfer> GetFR66TransferById(int id, HTokenClaim token);
        Task<(int totalCount, IEnumerable<FR66Transfer> list)> GetFR66TransferFroApproval(int sabhaId, int pageNo, int pageSize, string? filterKeyWord);
        Task<(int totalCount, IEnumerable<FR66Transfer> list)> GetAllFR66TransferForSabha(int sabhaId, int pageNo, int pageSize, string? filterKeyWord);
    }
}
