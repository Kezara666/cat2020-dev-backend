using CAT20.Core.HelperModels;
using CAT20.Core.Models.AssessmentTax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.AssessmentTax
{
    public interface IAmalgamationSubDivisionRepository:IRepository<AmalgamationSubDivision>
    {
        Task<AmalgamationSubDivision> GetById(int id,HTokenClaim token);
        Task<(int totalCount, IEnumerable<AmalgamationSubDivision> list)> GetAllAmalgamationSubDivisionForSabha(int sabhaId, int pageNo, int pageSize, string? filterKeyword, HTokenClaim token);
        Task<(int totalCount, IEnumerable<AmalgamationSubDivision> list)> GetAllPendingAmalgamationSubDivisionForSabha(int sabhaId, int pageNo, int pageSize, string? filterKeyword, HTokenClaim token);
    }
}
