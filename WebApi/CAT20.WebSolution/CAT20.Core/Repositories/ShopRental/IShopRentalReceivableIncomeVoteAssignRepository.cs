using CAT20.Core.Models.ShopRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.ShopRental
{
    public interface IShopRentalReceivableIncomeVoteAssignRepository : IRepository<ShopRentalRecievableIncomeVoteAssign>
    {
        Task<IEnumerable<ShopRentalRecievableIncomeVoteAssign>> GetAllForOffice(int officeid);

        Task<IEnumerable<ShopRentalRecievableIncomeVoteAssign>> GetAllForSabha(int sabhaid);

        Task<ShopRentalRecievableIncomeVoteAssign> GetByShopId(int shopId);
    }
}
