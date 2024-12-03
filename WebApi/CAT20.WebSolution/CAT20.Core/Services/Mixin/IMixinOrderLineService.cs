using CAT20.Core.Models.Mixin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Mixin
{
    public interface IMixinOrderLineService
    {
        Task<MixinOrderLine> GetById(int id);

        Task<IEnumerable<MixinOrderLine>> GetAllForOrderId(int mixinOrderId);
        Task<MixinOrderLine> Create(MixinOrderLine newMixinOrderLine);
        Task Update(MixinOrderLine mixinOrderLineToBeUpdated, MixinOrderLine mixinOrderLine);
        Task Delete(MixinOrderLine mixinOrderLine);
        Task<IEnumerable<MixinOrderLine>> GetAllForVoteAssignmentDetailId(int id);
        Task<IEnumerable<MixinOrderLine>> GetAllForVoteAssignmentId(int id);
        Task<IEnumerable<MixinOrderLine>> GetAllForOfficeDate(int office, DateTime date);
        Task<IEnumerable<MixinOrderLine>> GetAllForVoteIdAndVoteorBal(int voteid, int voteorbal);
    }
}

