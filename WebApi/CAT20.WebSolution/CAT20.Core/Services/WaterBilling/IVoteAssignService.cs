using CAT20.Core.Models.WaterBilling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.WaterBilling
{
    public interface IVoteAssignService
    {
        Task<IEnumerable<VoteAssign>> GetAllVoteAssigns();
        Task<IEnumerable<VoteAssign>> GetAllForWaterProject(int waterProjectId);

        Task<VoteAssign> GetById(int id);
        Task<VoteAssign> Create(VoteAssign obj);

        Task Update(VoteAssign objToBeUpdated, VoteAssign obj);

        Task Delete(VoteAssign obj);
    }
}
