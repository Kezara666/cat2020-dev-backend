using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Mixin;

namespace CAT20.Core.Repositories.Mixin
{
    public interface IMixinCancelOrderRepository : IRepository<MixinCancelOrder>
    {
        Task<IEnumerable<MixinCancelOrder>> GetAll();
        //Task<IEnumerable<MixinOrder>> GetAllForOfficeId(int officeid);
        Task<MixinCancelOrder> GetById(int id);
        Task<MixinCancelOrder> GetByOrderId(int id);
        //Task<IEnumerable<MixinOrder>> GetAllForVoteId(int Id);
    }
}
