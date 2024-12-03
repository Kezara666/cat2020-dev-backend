using CAT20.Core.Models.AssessmentTax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.AssessmentTax
{
    public interface IAssmtVoteAssignService
    {
        Task<IEnumerable<AssessmentVoteAssign>> GetAllAssmtVoteAssigns();
        Task<IEnumerable<AssessmentVoteAssign>> GetAllForSabha(int sabhaId);

        Task<bool> SetVoteId(int sabhaId);

        Task<AssessmentVoteAssign> GetById(int id);
        Task<AssessmentVoteAssign> Create(AssessmentVoteAssign obj);

        Task Update(AssessmentVoteAssign objToBeUpdated, AssessmentVoteAssign obj);

        Task Delete(AssessmentVoteAssign obj);
    }
}
