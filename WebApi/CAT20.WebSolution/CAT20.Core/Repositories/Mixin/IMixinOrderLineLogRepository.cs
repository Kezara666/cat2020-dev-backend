using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.Mixin;

namespace CAT20.Core.Repositories.Mixin
{
    public interface IMixinOrderLineLogRepository : IRepository<MixinOrderLineLog>
    {
        Task<IEnumerable<MixinOrderLineLog>> GetAllForOrderId(int mixinOrderId);
        Task<MixinOrderLineLog> GetById(int id);
        Task<IEnumerable<MixinOrderLineLog>> GetAllForVoteAssignmentDetailId(int Id);
        Task<IEnumerable<MixinOrderLineLog>> GetAllForVoteAssignmentId(int Id);
        Task<IEnumerable<MixinOrderLineLog>> GetAllForOfficeDate(int officeid, DateTime orderdate);
        Task<IEnumerable<MixinOrderLineLog>> GetAllForVoteIdAndVoteorBal(int voteid, int voteorbal);
        //Task<IEnumerable<MixinOrderLineLog>> GetAllForVoteIdAndVoteorBalAndSessionId(int voteid, int voteorbal, int sessionid);
        //Task<IEnumerable<MixinOrderLineLog>> GetAllForVoteIdAndVoteorBalAndSessionIdAndState(int voteid, int voteorbal, int sessionid, OrderStatus state);


        Task<bool> IsRelationShipExist(int voteAssignmentDetailId);
    }
}
