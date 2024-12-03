using CAT20.Core.HelperModels;
using CAT20.Core.Models.ShopRental;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.ShopRental
{
    public interface IShopRentalBalanceService
    {
        //*******
        //2024/06/13
        Task<HShopRentalCalculator> CalculateShopRentalPaymentBalanceRowWise(int shopId, int currentYear, int currentMonth, decimal inputPayAmount);
        //*******













        Task<IEnumerable<ShopRentalBalance>> GetAllNotCompletedBalanceInformationOfShop(int shopId);

        Task<(decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal)> calculateTotalPayableAmountByYearMonth(int propertyId, int shopId, int rowYear, int rowMonth);
        //****



        Task<ShopRentalBalance> GetCurrentMonthBalanceInfo(int shopId, int year, int monthId);

        Task<ShopRentalBalance> GetLastBalanceInfo(int propertyId, int shopId);


        (decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal) UpdatedCalculateShopRentalPaymentBalance(HShopRentalBalance shBalanceSummary, decimal inputPayAmount);

        Task<(decimal, decimal, decimal, decimal, decimal)> calculateTotalTyLyArreasAmountForYearMonth(int shopId, int inputYear, int inputMonth);

        //Task ProcessShopRentalPayment(int mixinOrderId, int cashierId, int shopId);

        //Task<bool> Update(int propertyId, int shopId, decimal PayingAmount_lastYear_fine, decimal PayingAmount_lastYear_arreas, decimal PayingAmount_thisYear_fine, decimal PayingAmount_thisYear_arreas, decimal PayingAmount_rental, decimal PayingAmount_serviceChargeArreas, decimal PayingAmount_serviceCharge, decimal next_overpayment, decimal currentRentalAmount, decimal currentServiceChargeAmount, int cashierId);

        //Task<bool> UpdateCurrentMonthRecord(int currentYear, int currentMonth, int propertyId, int shopId, decimal PayingAmount_lastYear_fine, decimal PayingAmount_lastYear_arreas, decimal PayingAmount_thisYear_fine, decimal PayingAmount_thisYear_arreas, decimal PayingAmount_rental, decimal PayingAmount_serviceChargeArreas, decimal PayingAmount_serviceCharge, decimal next_overpayment, decimal currentRentalAmount, decimal currentServiceChargeAmount, int cashierId);

        //Task<bool> CreateMonthendForShop(int propertyId, int shopId, int year, int month, int userId);

        //Task<bool> ArreasTransferForShop(int propertyId, int shopId, int year, int month, int userId);

        //Task<bool> CreateFineForShop(int propertyId, int shopId, DateOnly currentSessionDate);

        //Task<bool> ProcessShopRentalFineProcess(int propertyId, int shopId, DateOnly currentSessionDate, int fineRateTypeId, int fineCalTypeId, int rentalPaymentDateTypeId, int fineChargingMethodId, int fineProcessDate, decimal fineDailyRate, decimal fineMonthlyRate, decimal fine1stMonthRate, decimal fine2ndMonthRate, decimal fine3rdMonthRate, decimal currentRentalAmount);

        //decimal CalculateShopRentalFine(DateOnly currentSessionDate, int fineRateTypeId, int fineCalTypeId, int rentalPaymentDateTypeId, int fineChargingMethodId, int fineProcessDate, decimal fineDailyRate, decimal fineMonthlyRate, decimal fine1stMonthRate, decimal fine2ndMonthRate, decimal fine3rdMonthRate, decimal currentRentalAmount, int rowYear, int rowMonth, decimal rowMonthArreasAmount, decimal rowMonthArreasPaidAmount);

        //Task ReverseProcessShopRentalPayment(int mixinOrderId, int cashierId, int shopId);

        //Task<bool> ReverseUpdate(int propertyId, int shopId, decimal RevePayingAmount_lastYear_fine, decimal RevPayingAmount_lastYear_arreas, decimal RevPayingAmount_thisYear_fine, decimal RevPayingAmount_thisYear_arreas, decimal RevPayingAmount_rental, decimal RevPayingAmount_serviceChargeArreas, decimal RevPayingAmount_serviceCharge, decimal Revnext_overpayment, decimal currentRentalAmount, decimal currentServiceChargeAmount, int cashierId);

        Task<IEnumerable<ShopRentalBalance>> GetAllNotCompletedBalanceByShopId(int shopId);
        //*********************************************************************************************


















        //-----
        Task<ShopRentalBalance> GetShopRentalBalanceByYearMonth(int year, int month, int shopId);

        //-----
        Task<IEnumerable<ShopRentalBalance>> GetAllBalanceOfYear(int propertyId, int shopId, int inputYear);


        Task<IEnumerable<ShopRentalBalance>> GetAllBalanceInYearUpTOMonth(int propertyId, int shopId, int inputYear, int inputMonth);


        //-------
        Task<ShopRentalBalance> GetMonthlyBalanceInfo(int propertyId, int shopId, int year, int monthId);
        //-------

        //----
        Task<IEnumerable<ShopRentalBalance>> GetNotCompletedBalancesByShopIdsByYearMonth(List<int?> shopKeyIds, int year, int month);
        //---
        Task<decimal> calculateTotalArrerseAmount(int shopId);

        //---

        //---
    }
}
