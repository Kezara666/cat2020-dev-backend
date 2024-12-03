using CAT20.Core.Models.Mixin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Mixin
{
    public interface IMixinOrderLineLogService
    {
        Task<MixinOrderLineLog> GetById(int id);

        Task<IEnumerable<MixinOrderLineLog>> GetAllForOrderId(int mixinOrderId);
        Task<MixinOrderLineLog> Create(MixinOrderLineLog newMixinOrderLineLog);
        Task Update(MixinOrderLineLog MixinOrderLineLogToBeUpdated, MixinOrderLineLog MixinOrderLineLog);
        Task Delete(MixinOrderLineLog MixinOrderLineLog);
        Task<IEnumerable<MixinOrderLineLog>> GetAllForVoteAssignmentDetailId(int id);
        Task<IEnumerable<MixinOrderLineLog>> GetAllForVoteAssignmentId(int id);
        Task<IEnumerable<MixinOrderLineLog>> GetAllForOfficeDate(int office, DateTime date);
        Task<IEnumerable<MixinOrderLineLog>> GetAllForVoteIdAndVoteorBal(int voteid, int voteorbal);
    }
}

