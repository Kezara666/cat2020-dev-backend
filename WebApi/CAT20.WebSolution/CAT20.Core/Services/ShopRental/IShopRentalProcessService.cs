using CAT20.Core.HelperModels;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.ShopRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.ShopRental
{
    public interface IShopRentalProcessService
    {
        Task<(int totalCount, IEnumerable<ShopRentalProcess> list)> getAllProcessForSabha(int sabhaId, int pageNo);

        Task<(bool, string)> MonthEndProcess(ShopRentalProcess shopRentalProcess, object environment, string _backupFolder, HTokenClaim token);
        
        Task<(bool, string)> FineProcess(ShopRentalProcess shopRentalProcess, object environment, string _backupFolder, HTokenClaim token);
      
        Task<(bool, string)> DailyFineProcess(Session session, HTokenClaim token);
        Task<(bool, string?)> SkipFineProcess(ShopRentalProcess shopRentalProcess, HTokenClaim token);
        Task<bool> IsCompetedMonthendProcess(int SbahaId, DateOnly date);

        Task<bool> IsCompetedFineProcess(int SbahaId, DateOnly date);

        Task<ShopRentalProcess> getLastDailyFineRateShopRentalprocess(int SbahaId);

        Task<ShopRentalProcess> getLastDailyFineFixedAmountShopRentalprocess(int SbahaId);
    }
}
