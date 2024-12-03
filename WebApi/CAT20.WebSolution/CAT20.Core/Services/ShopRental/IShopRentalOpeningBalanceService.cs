using CAT20.Core.HelperModels;
using CAT20.Core.Models.ShopRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.ShopRental
{
    public interface IShopRentalOpeningBalanceService
    {
        Task<OpeningBalance> GetOpeningBalanceByShopId(int shopId);

        Task<OpeningBalance> GetById(int id);

        Task<OpeningBalance> Create(OpeningBalance obj);

        Task Update(OpeningBalance objToBeUpdated, OpeningBalance obj);

        Task<IEnumerable<OpeningBalance>> GetOpeningBalancesByShopIds(List<int?> shopKeyIds);

        Task<IEnumerable<OpeningBalance>> GetAprovalPendingOpeningBalancesByShopIds(List<int?> shopKeyIds);
        Task<IEnumerable<OpeningBalance>> GetAprovalRejectedOpeningBalancesByShopIds(List<int?> shopKeyIds);
        Task<IEnumerable<OpeningBalance>> GetAprovalApprovedOpeningBalancesByShopIds(List<int?> shopKeyIds);

        Task RejectShopRentalOpeningBalance(OpeningBalance objToBeUpdated, OpeningBalance obj);

        Task<(bool,string?)> ApproveShopRentalOpeningBalance(List<int?> openingBalanceIds, int approvedby,HTokenClaim token);

        Task IsProcessedStatusUpdate(OpeningBalance objToBeStatusUpdated);
        //------


        //----
        Task<IEnumerable<OpeningBalance>> GetAllByPropertyIdShopId(int propertyId, int shopId);
        //----
    }
}
