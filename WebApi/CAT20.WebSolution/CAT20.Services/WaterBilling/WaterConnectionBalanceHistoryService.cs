using CAT20.Core;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.WaterBilling;
using CAT20.Core.Services.WaterBilling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.WaterBilling
{
    public class WaterConnectionBalanceHistoryService : IWaterConnectionBalanceHistoryService
    {
        private readonly IWaterBillingUnitOfWork _wb_unitOfWork;

        public WaterConnectionBalanceHistoryService(IWaterBillingUnitOfWork wb_unitOfWork)
        {
            _wb_unitOfWork = wb_unitOfWork;
        }

        public async Task<bool> CreateBalanceHistory(WaterConnectionBalance balance, WbTransactionsType transactionsType, int actionBy)
        {
            try
            {
                var history = new WaterConnectionBalanceHistory
                {
                    Id = null,
                    BalanceId = balance.Id,
                    WcPrimaryId = balance.WcPrimaryId,
                    TransactionType = transactionsType,
                    ActionAt = DateTime.Now,
                    ActionBy = actionBy,
                    ConnectionNo = balance.ConnectionNo,
                    BarCode = balance.BarCode,
                    InvoiceNo = balance.InvoiceNo,
                    Year = balance.Year,
                    Month = balance.Month,
                    FromDate = balance.FromDate,
                    ToDate = balance.ToDate,
                    ReadBy = balance.ReadBy,
                    BillProcessDate = balance.BillProcessDate,
                    MeterNo = balance.MeterNo,
                    PreviousMeterReading = balance.PreviousMeterReading,
                    ThisMonthMeterReading = balance.ThisMonthMeterReading,
                    
                    WaterCharge = balance.WaterCharge,
                    FixedCharge = balance.FixedCharge,
                    VATRate = balance.VATRate,
                    VATAmount = balance.VATAmount,
                    ThisMonthCharge = balance.ThisMonthCharge,
                    ThisMonthChargeWithVAT = balance.ThisMonthChargeWithVAT,
                    TotalDue = balance.TotalDue,
                    MeterCondition = balance.MeterCondition,

                    ByExcessDeduction = balance.ByExcessDeduction,
                    OnTimePaid = balance.OnTimePaid,
                    LatePaid = balance.LatePaid,
                    OverPay = balance.OverPay,
                    Payments = balance.Payments,
                    
                    IsCompleted = balance.IsCompleted,
                    IsFilled = balance.IsFilled,
                    IsProcessed = balance.IsProcessed,

                    NoOfPayments = balance.NoOfPayments,
                    NoOfCancels = balance.NoOfCancels,


                    PrintLastBalance = balance.PrintLastBalance,
                    CalculationString = balance.CalculationString,
                    LastBillYearMonth = balance.LastBillYearMonth,
                    PrintBillingDetails = balance.PrintBillingDetails,

                    PrintBalanceBF = balance.PrintBalanceBF,
                    PrintLastMonthPayments = balance.PrintLastMonthPayments,
                    NumPrints = balance.NumPrints,

                };

                await _wb_unitOfWork.BalanceHistory.AddAsync(history);

                return true;

            }catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> InitBalanceHistory(List<WaterConnectionBalance> balances, int actionBy)
        {
            try {          
                
                foreach (var balance in balances)
                {
                    await CreateBalanceHistory(balance, WbTransactionsType.MonthlyBill, actionBy);
                    
                }
               await  _wb_unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<bool> CreateBalanceHistory(List<WaterConnectionBalance> balances, WbTransactionsType transactionsType, int actionBy)
        {
            try
            {

                foreach (var balance in balances)
                {
                    await CreateBalanceHistory(balance, transactionsType, actionBy);

                }
                //await _wb_unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
