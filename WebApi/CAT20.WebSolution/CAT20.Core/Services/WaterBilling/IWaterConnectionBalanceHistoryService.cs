using CAT20.Core.Models.Enums;
using CAT20.Core.Models.WaterBilling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.WaterBilling
{
    public interface IWaterConnectionBalanceHistoryService
    {
        Task<bool> CreateBalanceHistory(WaterConnectionBalance balance, WbTransactionsType transactionsType, int actionBy);
        Task<bool> InitBalanceHistory(List<WaterConnectionBalance> balances, int actionBy);
        Task<bool> CreateBalanceHistory(List<WaterConnectionBalance> balances, WbTransactionsType transactionsType , int actionBy);
    }
}
