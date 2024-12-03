using CAT20.Core.Models.Enums;
using CAT20.Core.Models.ShopRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.ShopRental
{
    public interface IShopRentalProcessRepository: IRepository<ShopRentalProcess>
    {
        Task<(int totalCount, IEnumerable<ShopRentalProcess> list)> getAllProcessForSabha(int sabhaId, int pageNo);

        Task<bool> IsCompetedProcess(int SbahaId, DateOnly date, ShopRentalProcessType processType);

        Task<bool> IsCompetedMonthendProcess(int SbahaId, DateOnly date);

        Task<bool> IsCompetedFineProcess(int SbahaId, DateOnly date);
        Task<ShopRentalProcess> getLastDailyFineRateShopRentalprocess(int SbahaId);
        Task<ShopRentalProcess> getLastDailyFineFixedAmountShopRentalprocess(int SbahaId);
    }
}
