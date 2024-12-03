using CAT20.Core.Models.ShopRental;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.ShopRental
{
    public interface IShopRentalBalanceRepository : IRepository<ShopRentalBalance>
    {
        Task<IEnumerable<ShopRentalBalance>> GetAllNotCompletedBalanceByYear(int shopId, int inputYear);

        Task<IEnumerable<ShopRentalBalance>> GetForOrderTransaction(List<int?> shopIds);
        Task<IEnumerable<ShopRentalBalance>> GetForOrderTransaction(int shopIds);
        //***











        Task<ShopRentalBalance> GetNotCompletedBalanceInfoByYearMonth(int propertyId, int shopId, int year, int month);

        Task<IEnumerable<ShopRentalBalance>> GetAllNotCompletedBalanceInformationOfShop(int shopId);
        










        Task<IEnumerable<ShopRentalBalance>> GetAllBalanceForShopProperty(int shopId);

        Task<ShopRentalBalance> GetCurrentMonthBalanceInfo(int shopId, int year, int monthId);

        Task<ShopRentalBalance> GetLastBalanceInfo(int shopId);

        Task<IEnumerable<ShopRentalBalance>> GetAllNotCompletedBalanceInformationUptoYearMonth(int propertyId, int shopId, int inputYear, int inputMonth);

        Task<IEnumerable<ShopRentalBalance>> GetAllNotCompletedBalanceByShopId(int shopId);

        Task<IEnumerable<ShopRentalBalance>> GetAllBalanceByShopId(int shopId);

        Task<IEnumerable<ShopRentalBalance>> GetForMonthendProcessBackup(int sabhaId);

        Task<IEnumerable<ShopRentalBalance>> GetForFineProcessBackup(int sabhaId, int ProcessConfigId);

        Task<bool> HasShoprentalBalanceForOffice(int officeId);

        Task<bool> HasShoprentalBalanceForSabha(int sabhaId);

        Task<ShopRentalBalance> GetShopRentalBalanceByYearMonth(int year, int month, int propertyId, int shopId);
        //********************************************
























        Task<ShopRentalBalance> GetShopRentalNotCompletedBalanceByYearMonth(int year, int month, int shopId);

        

        //---------- update
        Task<IEnumerable<ShopRentalBalance>> GetAllBalancesOfYear(int propertyId, int shopId, int inputYear);

        Task<IEnumerable<ShopRentalBalance>> GetAllBalanceInYearUpTOMonth(int propertyId, int shopId, int inputYear, int inputMonth);

        Task<ShopRentalBalance> GetBalanceInfo(int propertyId, int shopId);
        //-----------------

        //-----
        Task<ShopRentalBalance> GetMonthlyBalanceInfo(int propertyId, int shopId, int year, int monthId);
        //-----


        //---
        Task<IEnumerable<ShopRentalBalance>> GetNotCompletedBalancesByShopIdsByYearMonth(List<int?> ShopIds, int year, int month);
        //---


        //---
    
        //---


        //---

        //---


        //---
        Task<ShopRentalBalance> GetShopRentalBalanceByYearMonthForReversePayment(int year, int month, int shopId);


        
        //---
    }
}
