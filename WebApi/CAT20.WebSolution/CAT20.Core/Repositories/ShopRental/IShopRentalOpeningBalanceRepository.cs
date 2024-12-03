using CAT20.Core.Models.ShopRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.ShopRental
{
    public interface IShopRentalOpeningBalanceRepository: IRepository<OpeningBalance>
    {
        Task<OpeningBalance> GetOpeningBalanceByShopId(int shopId);

        Task<IEnumerable<OpeningBalance>> GetOpeningBalancesByShopIds(List<int?> ShopIds);

        Task<IEnumerable<OpeningBalance>> GetAprovalPendingOpeningBalancesByShopIds(List<int?> ShopIds);
        Task<IEnumerable<OpeningBalance>> GetAprovalRejectedOpeningBalancesByShopIds(List<int?> ShopIds);
        Task<IEnumerable<OpeningBalance>> GetAprovalApprovedOpeningBalancesByShopIds(List<int?> ShopIds);

        Task<IEnumerable<OpeningBalance>> GetAllByOpeningBalanceIds(List<int?> openingBalanceIds); //Aprove bulk
                                                                                                   //--------------------------------------------------------

        //----
        Task<IEnumerable<OpeningBalance>> GetAllByPropertyIdShopId(int propertyId, int shopId);
        //----
    }
}
