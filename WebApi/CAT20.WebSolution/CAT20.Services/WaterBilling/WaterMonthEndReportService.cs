using CAT20.Core;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.WaterBilling;
using CAT20.Core.Services.WaterBilling;
using CAT20.Data.MixinDb;
using DocumentFormat.OpenXml.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.WaterBilling
{
    public class WaterMonthEndReportService : IWaterMonthEndReportService
    {

        private readonly IWaterBillingUnitOfWork _wb_unitOfWork;
        public WaterMonthEndReportService(IWaterBillingUnitOfWork wb_unitOfWork)
        {
            _wb_unitOfWork = wb_unitOfWork;
        }


        public async Task<bool> CreateInitilReport(int officeId)
        {
            try
            {
                var wcs = await _wb_unitOfWork.WaterMonthEndReport.GetConnectionsForInti(111, 491);


                foreach (var wc in wcs)
                {

                    //if (wc.Id == 1979 || wc.Id == 3324) { 

                        var rpt = new WaterMonthEndReport
                        {
                            //Id
                            WcPrimaryId = wc.Id!.Value,
                            Year = wc.OpeningBalanceInformation!.Year,
                            Month = wc.OpeningBalanceInformation!.Month,

                            LastYearArrears = wc.OpeningBalanceInformation.LastYearArrears.HasValue ? wc.OpeningBalanceInformation.LastYearArrears : 0,
                            ThisYearArrears = 0,
                            TMCharge = wc.OpeningBalanceInformation.MonthlyBalance > 0 ? (wc.OpeningBalanceInformation.MonthlyBalance - (wc.OpeningBalanceInformation.LastYearArrears.HasValue ? wc.OpeningBalanceInformation.LastYearArrears : 0)) : 0,
                            OverPaying = wc.OpeningBalanceInformation.MonthlyBalance < 0 ? Math.Abs((decimal)wc.OpeningBalanceInformation.MonthlyBalance) : 0,
                            OverPaymentWithVat = 0,
                            LYArrearsPaying = 0,
                            TYArrearsPaying = 0,
                            TMPaying = 0,
                            RemainOverPay = wc.OpeningBalanceInformation.MonthlyBalance < 0 ? Math.Abs((decimal)wc.OpeningBalanceInformation.MonthlyBalance) : 0,
                            RemainOverPayVat = 0,
                            CreatedBy = -1,
                            CreatedAt = DateTime.Now,
                        };

                    Console.WriteLine("Creating report for " + rpt);

                    await _wb_unitOfWork.WaterMonthEndReport.AddAsync(rpt);
                //}

                }




                await _wb_unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> CreateMonthlyReport(int officeId)
        {
            try
            {
                var wcs = await _wb_unitOfWork.WaterMonthEndReport.GetConnectionsForReport(111, 491);
                var month = 4;
                var year = 2024;

                //foreach (var wc in wcs)
                //{

                foreach (var (wc, index) in wcs.Select((value, i) => (value, i)))
                {
                    //if (wc.Id == 1979 || wc.Id == 3324)
                    //{

                    if (!await _wb_unitOfWork.WaterMonthEndReport.HasReportForGivenMonth(wc.Id!.Value, year, month))
                    {
                        var balance = await _wb_unitOfWork.WaterMonthEndReport.GetBalanceBillYearAndMonth(wc.Id!.Value, year, month);

                        if (balance == null)
                        {
                            balance = new WaterConnectionBalance();
                            balance.ThisMonthCharge = 0;
                            balance.VATAmount = 0;

                        }


                        if (balance != null)
                        {
                            var lastReport = await _wb_unitOfWork.WaterMonthEndReport.GetLastReport(wc.Id!.Value);

                            var recipts = await _wb_unitOfWork.MixinOrders.GetForWaterBillReport(wc.Id!.Value, year, month);

                            decimal totalPaid = 0m;
                            decimal? LYArrearsPaying = 0m;
                            decimal? LYArrearsPayingVAT = 0m;
                            decimal? TYArrearsPaying = 0m;
                            decimal? TYArrearsPayingVAT = 0m;
                            decimal? TMPaying = 0m;
                            decimal? TMPayingVAT = 0m;
                            decimal? OverPaying = 0m;
                            decimal? OverPayingVAT = 0m;


                            foreach (var recipt in recipts)
                            {
                                foreach (var item in recipt.MixinOrderLine)
                                {
                                    if (item.VotePaymentTypeId == 1)
                                    {
                                        LYArrearsPaying += item.Amount;
                                        LYArrearsPayingVAT += item.PaymentVatAmount;
                                    }
                                    else if (item.VotePaymentTypeId == 2)
                                    {
                                        TYArrearsPaying += item.Amount;
                                        TYArrearsPayingVAT += item.PaymentVatAmount;
                                    }
                                    else if (item.VotePaymentTypeId == 3)
                                    {
                                        TMPaying += item.Amount;
                                        TMPayingVAT += item.PaymentVatAmount;
                                    }
                                    else if (item.VotePaymentTypeId == 4)
                                    {
                                        OverPaying += item.Amount;
                                        OverPayingVAT += item.PaymentVatAmount;

                                    }

                                }
                            }

                            var rpt = new WaterMonthEndReport
                            {
                                //Id
                                WcPrimaryId = wc.Id!.Value,
                                Year = year,
                                Month = month,
                                CreatedBy = -1,
                                CreatedAt = DateTime.Now,

                                //LastYearArrears = wc.OpeningBalanceInformation.LastYearArrears,
                                //ThisYearArrears = 0,
                                //TMCharge = wc.OpeningBalanceInformation.MonthlyBalance > 0 ? wc.OpeningBalanceInformation.MonthlyBalance : 0,
                                //OverPaymentWithVat = wc.OpeningBalanceInformation.MonthlyBalance < 0 ? wc.OpeningBalanceInformation.MonthlyBalance : 0,
                                //LYArrearsPaying = 0,
                                //TYArrearsPaying = 0,
                                //TMPaying = 0,
                                //OverPaying = 0,

                            };

                            rpt.ReceivedOverPay = OverPaying;
                            rpt.ReceivedOverPayVAT = OverPayingVAT;

                            rpt.LastYearArrears = lastReport.LastYearArrears - LYArrearsPaying;
                            rpt.LYABalanceVAT = lastReport.LYABalanceVAT - LYArrearsPayingVAT;

                            rpt.ThisYearArrears = lastReport.ThisYearArrears - TYArrearsPaying;
                            rpt.TYABalanceVAT = lastReport.TYABalanceVAT - TYArrearsPayingVAT;

                            rpt.TMCharge = lastReport.TMCharge - TMPaying;
                            rpt.TMBalanceVAT = lastReport.TMBalanceVAT - TMPayingVAT;


                            if (lastReport.Year < year)
                            {
                                rpt.LastYearArrears += rpt.ThisYearArrears;
                                rpt.LYABalanceVAT += rpt.TYABalanceVAT;

                                rpt.ThisYearArrears = 0;
                                rpt.TYABalanceVAT = 0;

                                rpt.LastYearArrears += rpt.TMCharge;
                                rpt.LYABalanceVAT += rpt.TMBalanceVAT;

                                rpt.TMCharge = 0;
                                rpt.TMBalanceVAT = 0;
                            }

                            if (lastReport.Year == year && lastReport.Month < month)
                            {
                                rpt.ThisYearArrears += rpt.TMCharge;
                                rpt.TYABalanceVAT += rpt.TMBalanceVAT;
                            }

                            if (0 <= (balance.ThisMonthCharge - lastReport.OverPaying))
                            {

                                rpt.TMCharge = balance.ThisMonthCharge - lastReport.OverPaying;
                                rpt.TMBalanceVAT = balance.VATAmount - lastReport.OverPaymentWithVat;

                                rpt.RemainOverPay = 0;
                                rpt.RemainOverPayVat = 0;

                            }

                            if ((balance.ThisMonthCharge - lastReport.OverPaying) < 0)
                            {
                                rpt.RemainOverPay = lastReport.OverPaying - balance.ThisMonthCharge;
                                rpt.RemainOverPayVat = lastReport.OverPaymentWithVat - balance.VATAmount;

                                OverPaying += rpt.RemainOverPay;
                                OverPayingVAT += rpt.RemainOverPayVat;

                                rpt.TMCharge = 0;
                                rpt.TMBalanceVAT = 0;
                            }



                            /*last year arrears*/
                            if (rpt.LastYearArrears > 0 && OverPaying > 0 && rpt.LastYearArrears >= OverPaying)
                            {
                                rpt.LastYearArrears -= OverPaying;
                                rpt.LYABalanceVAT -= OverPayingVAT;
                                OverPaying = 0;
                                OverPayingVAT = 0;
                            }

                            if (rpt.LastYearArrears > 0 && OverPaying > rpt.LastYearArrears)
                            {
                                OverPaying = OverPaying - rpt.LastYearArrears;
                                OverPayingVAT = OverPayingVAT - rpt.LYABalanceVAT;
                                rpt.LastYearArrears = 0;
                                rpt.LYABalanceVAT = 0;

                            }

                            /*this year arrears*/
                            if (rpt.ThisYearArrears > 0 && rpt.ThisYearArrears >= OverPaying)
                            {
                                rpt.ThisYearArrears -= OverPaying;
                                rpt.TYABalanceVAT -= OverPayingVAT;
                                OverPaying = 0;
                                OverPayingVAT = 0;
                            }

                            if (rpt.ThisYearArrears > 0 && OverPaying > rpt.ThisYearArrears)
                            {
                                OverPaying = OverPaying - rpt.ThisYearArrears;
                                OverPayingVAT = OverPayingVAT - rpt.TYABalanceVAT;

                                rpt.ThisYearArrears = 0;
                                rpt.TYABalanceVAT = 0;

                            }


                            if (rpt.TMCharge > 0 && rpt.TMCharge >= OverPaying)
                            {
                                rpt.TMCharge -= OverPaying;
                                rpt.TMBalanceVAT -= OverPayingVAT;
                                OverPaying = 0;
                                OverPayingVAT = 0;
                            }

                            if (rpt.TMCharge > 0 && OverPaying > rpt.TMCharge)
                            {
                                OverPaying = OverPaying - rpt.TMCharge;
                                OverPayingVAT = OverPayingVAT - rpt.TMBalanceVAT;
                                rpt.TMCharge = 0;
                                rpt.TMBalanceVAT = 0;
                            }

                            //rpt.OverPaying = OverPaying;
                            //rpt.OverPaymentWithVat = OverPayingVAT;

                            rpt.LYArrearsPaying = LYArrearsPaying;
                            rpt.TYArrearsPaying = TYArrearsPaying;
                            rpt.TMPaying = TMPaying;
                            rpt.OverPaying = OverPaying;
                            rpt.OverPaymentWithVat = OverPayingVAT;

                            rpt.MonthlyBill = balance.ThisMonthCharge;
                            rpt.MonthlyBillWithVat = balance.VATAmount;


                            Console.WriteLine("Creating report for " + rpt);

                            await _wb_unitOfWork.WaterMonthEndReport.AddAsync(rpt);
                        }
                        else
                        {
                            Console.WriteLine("No balance found for " + wc.Id);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Report already created for " + wc.Id);

                    }
                    //}
                    if ((index + 1) % 35 == 0)
                    {
                        Console.WriteLine($"Index {index + 1} is a multiple of 35, adding 3 seconds delay.");
                        await Task.Delay(3000); // 3-second delay
                    }

                }




                await _wb_unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }




        public async Task<bool> validate(int officeId)
        {
            try
            {
                var year = 2024;
                var month = 3;
                var wcs = await _wb_unitOfWork.WaterMonthEndReport.Validate(111,null ,year,month);


                foreach (var wc in wcs)
                {
                    var jan = wc.WaterMonthEndReports.Where(w => w.Year == year && w.Month == 2).FirstOrDefault();

                    if(jan != null)
                    {
                        var feb = wc.WaterMonthEndReports.Where(w => w.Year == year && w.Month == 3).FirstOrDefault();

                        if (feb != null)
                        {
                            var openArreaes = jan.LastYearArrears + jan.ThisYearArrears + jan.TMCharge - jan.OverPaying;

                            var bill = feb.MonthlyBill;

                            var totalPay= feb.LYArrearsPaying + feb.TYArrearsPaying + feb.TMPaying + feb.ReceivedOverPay;

                            var closing = (openArreaes + bill - totalPay)  + feb.OverPaying;

                            if (closing != feb.LastYearArrears + feb.ThisYearArrears + feb.TMCharge)
                            { 
                                if(closing == feb.OverPaying)
                                {
                                    Console.WriteLine("over paymatch " + wc.Id);
                                }
                            
                                Console.WriteLine("LastYearArrears not matching for " + wc.Id);
                            }
                            else
                            {

                            }


                        }
                        else
                        {
                            Console.WriteLine("No report found for february for " + wc.Id);
                        }

                    }
                    else
                    {
                        Console.WriteLine("No report found for january for " + wc.Id);
                        var feb = wc.WaterMonthEndReports.Where(w => w.Year == year && w.Month == 3).FirstOrDefault();

                        if(feb != null  )
                        {
                            Console.WriteLine("Report found for february for " + wc.Id);
                        }
                        else
                        {
                            Console.WriteLine("No report found for february for " + wc.Id);
                        }



                    }


                }




                return true;


            }
            catch (Exception ex)
            {
                return false;
            }
        }   

    }
}
