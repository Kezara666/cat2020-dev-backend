using CAT20.Core.Models.Mixin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Mixin
{
    public interface IMixinCancelOrderService
    {
        //Task<IEnumerable<MixinCancelOrder>> GetAllForSabhaId(int sabhaid);
        //Task<IEnumerable<MixinCancelOrder>> GetAllForOfficeId(int officeid);
        Task<MixinCancelOrder> GetById(int id);
        Task<MixinCancelOrder> GetByOrderId(int id);
        Task<MixinCancelOrder> Create(MixinCancelOrder newMixinCancelOrder);
        Task Update(MixinCancelOrder mixinCancelOrderToBeUpdated, MixinCancelOrder mixinCancelOrder);
        Task Delete(MixinCancelOrder mixinCancelOrder);
        //Task<IEnumerable<MixinCancelOrder>> GetAllForVoteId(int id);
    }
}

