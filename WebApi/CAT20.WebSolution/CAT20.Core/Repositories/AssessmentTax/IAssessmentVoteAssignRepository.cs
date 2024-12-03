using CAT20.Core.Models.AssessmentTax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.AssessmentTax
{
    public interface IAssessmentVoteAssignRepository : IRepository<AssessmentVoteAssign>
    {
        Task<IEnumerable<AssessmentVoteAssign>> GetAllForSabha(int sabhaid);
    }
}
