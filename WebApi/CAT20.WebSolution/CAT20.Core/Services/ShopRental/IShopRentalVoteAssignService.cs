using CAT20.Core.Models.ShopRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.ShopRental
{
    public interface IShopRentalVoteAssignService
    {
        Task<ShopRentalVoteAssign> Create(ShopRentalVoteAssign obj);

        Task<ShopRentalVoteAssign> GetById(int id);

        Task Update(ShopRentalVoteAssign objToBeUpdated, ShopRentalVoteAssign obj);

        Task<IEnumerable<ShopRentalVoteAssign>> GetAllVoteAssigns();

        Task<IEnumerable<ShopRentalVoteAssign>> CreateMultiple(List<ShopRentalVoteAssign> objs);

        Task<IEnumerable<ShopRentalVoteAssign>> GetAllForOffice(int officeid);

        Task<IEnumerable<ShopRentalVoteAssign>> GetAllForSabha(int sabhaid);

        //Task<ShopRentalVoteAssign> GetByPropertyId(int propertyId);

        Task<ShopRentalVoteAssign> GetByShopId(int shopId);

        //----
        Task Delete(ShopRentalVoteAssign obj);
        //----
    }
}
