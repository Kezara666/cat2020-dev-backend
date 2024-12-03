using CAT20.Core.Models.ShopRental;
using CAT20.Core.Models.WaterBilling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.ShopRental
{
    public interface IShopRentalVoteAssignRepository : IRepository<ShopRentalVoteAssign>
    {
       Task<IEnumerable<ShopRentalVoteAssign>> GetAllForOffice(int officeid);

        Task<IEnumerable<ShopRentalVoteAssign>> GetAllForSabha(int sabhaid);

        //Task<ShopRentalVoteAssign> GetByPropertyIdAsync(int propertyId);

        Task<ShopRentalVoteAssign> GetByShopId(int shopId);
    }
}
