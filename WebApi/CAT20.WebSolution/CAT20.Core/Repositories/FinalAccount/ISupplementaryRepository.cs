using CAT20.Core.DTO.Final;
using CAT20.Core.Models.FinalAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.FinalAccount
{
    public interface ISupplementaryRepository : IRepository<Supplementary>
    {

        Task<(int totalCount, IEnumerable<Supplementary> list)> GetSupplementaryForApproval(int sabhaId, int pageNo, int pageSize, string? filterKeyWord);
        Task<(int totalCount, IEnumerable<Supplementary> list)> GetAllSupplementaryForSabha(int sabhaId, int pageNo, int pageSize, string? filterKeyWord);
    }
}
