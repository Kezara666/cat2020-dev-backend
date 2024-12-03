using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.Mixin;

namespace CAT20.Core.Repositories.Mixin
{
    public interface IMixinOrderLineRepository : IRepository<MixinOrderLine>
    {
        Task<IEnumerable<MixinOrderLine>> GetAllForOrderId(int mixinOrderId);
        Task<MixinOrderLine> GetById(int id);
        Task<IEnumerable<MixinOrderLine>> GetAllForVoteAssignmentDetailId(int Id);
        Task<IEnumerable<MixinOrderLine>> GetAllForVoteAssignmentId(int Id);
        Task<IEnumerable<MixinOrderLine>> GetAllForOfficeDate(int officeid, DateTime orderdate);
        Task<IEnumerable<MixinOrderLine>> GetAllForVoteIdAndVoteorBal(int voteid, int voteorbal);
        //Task<IEnumerable<MixinOrderLine>> GetAllForVoteIdAndVoteorBalAndSessionId(int voteid, int voteorbal, int sessionid);
        //Task<IEnumerable<MixinOrderLine>> GetAllForVoteIdAndVoteorBalAndSessionIdAndState(int voteid, int voteorbal, int sessionid, OrderStatus state);

        Task<bool> IsRelationShipExist(int voteAssignmentDetailId); 
    }
}
