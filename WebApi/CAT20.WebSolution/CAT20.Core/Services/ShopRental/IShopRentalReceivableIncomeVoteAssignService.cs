using CAT20.Core.Models.ShopRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.ShopRental
{
    public interface IShopRentalReceivableIncomeVoteAssignService
    {
        Task<ShopRentalRecievableIncomeVoteAssign> Create(ShopRentalRecievableIncomeVoteAssign obj);

        Task<ShopRentalRecievableIncomeVoteAssign> GetById(int id);

        Task<IEnumerable<ShopRentalRecievableIncomeVoteAssign>> CreateMultiple(List<ShopRentalRecievableIncomeVoteAssign> objs);

        Task Update(ShopRentalRecievableIncomeVoteAssign objToBeUpdated, ShopRentalRecievableIncomeVoteAssign obj);

        Task Delete(ShopRentalRecievableIncomeVoteAssign obj);

        Task<ShopRentalRecievableIncomeVoteAssign> GetByShopId(int shopId);

        Task<IEnumerable<ShopRentalRecievableIncomeVoteAssign>> GetAllForOffice(int officeid);

        Task<IEnumerable<ShopRentalRecievableIncomeVoteAssign>> GetAllForSabha(int sabhaid);
    }
}
