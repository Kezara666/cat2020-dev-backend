using CAT20.Core.DTO.Final;
using CAT20.Core.Models.FinalAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.FinalAccount
{
    public interface ICutProvisionRepository:IRepository<CutProvision>
    {
        Task<(int totalCount, IEnumerable<CutProvision> list)> GetCutProvisionForApproval(int sabhaId, int pageNo, int pageSize, string? filterKeyWord);
        Task<(int totalCount, IEnumerable<CutProvision> list)> GetAllCutProvisionSabha(int sabhaId, int pageNo, int pageSize, string? filterKeyWord);
    }
}
