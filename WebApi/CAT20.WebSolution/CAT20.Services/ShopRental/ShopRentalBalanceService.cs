using CAT20.Core;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.ShopRental;
using CAT20.Core.Services.ShopRental;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Org.BouncyCastle.Asn1.X509;
using System.Data.Common;
using System.Linq;
using System.Transactions;

namespace CAT20.Services.ShopRental
{
    public class ShopRentalBalanceService : IShopRentalBalanceService
    {
        private readonly IShopRentalUnitOfWork _unitOfWork;
        private readonly IMixinUnitOfWork _mixinOrderUnitOfWork;

        private readonly IShopRentalOpeningBalanceService _shopRentalOpeningBalanceService;
        private readonly IShopRentalProcessConfigarationService _shopRentalProcessConfigarationService;
        private readonly IProcessConfigurationSettingAssignService _processConfigurationSettingAssignService;
        private readonly IShopService _shopService;

        public ShopRentalBalanceService(IShopRentalUnitOfWork unitOfWork, IMixinUnitOfWork mixinOrderUnitOfWork, IShopRentalOpeningBalanceService shopRentalOpeningBalanceService, IShopService shopService, IShopRentalProcessConfigarationService shopRentalProcessConfigarationService, IProcessConfigurationSettingAssignService processConfigurationSettingAssignService)
        {
            _unitOfWork = unitOfWork;
            _mixinOrderUnitOfWork = mixinOrderUnitOfWork;

            _shopRentalOpeningBalanceService = shopRentalOpeningBalanceService;
            _shopService = shopService;
            _shopRentalProcessConfigarationService = shopRentalProcessConfigarationService;
            _processConfigurationSettingAssignService = processConfigurationSettingAssignService;
        }
        //---


        //--------[Start : UpdatedCalculateShopRentalPaymentBalance]------------
        public (decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal) UpdatedCalculateShopRentalPaymentBalance(HShopRentalBalance shBalanceSummary, decimal inputPayAmount)
        {
            if (shBalanceSummary != null)
            {
                if (inputPayAmount >= 0)
                {
                    //#0
                    decimal inputAmount = inputPayAmount; //modified
                    //decimal inputAmount = inputPayAmount + (decimal)shBalanceSummary.OverPayment; /////??????????????????

                    //#1
                    decimal lastYear_fine = (decimal)shBalanceSummary.LastYearFine;
                    decimal payingAmount_lastYear_fine = 0;
                    decimal reamingBalance_lastYear_fine = (decimal)shBalanceSummary.LastYearFine;

                    decimal remaingAmount_afterlastYear_fine = 0;

                    //#2
                    decimal lastYear_arreas = (decimal)shBalanceSummary.LastYearArreas;
                    decimal payingAmount_lastYear_arreas = 0;
                    decimal reamingBalance_lastYear_arreas = (decimal)shBalanceSummary.LastYearArreas;

                    decimal remaingAmount_afterlastYear_arreas = 0;

                    //#3
                    decimal thisYear_fine = (decimal)shBalanceSummary.ThisYearFine;
                    decimal payingAmount_thisYear_fine = 0;
                    decimal reamingBalance_thisYear_fine = (decimal)shBalanceSummary.ThisYearFine;

                    decimal remaingAmount_afterthisYear_fine = 0;

                    //#4
                    decimal thisYear_arreas = (decimal)shBalanceSummary.ThisYearArreas;
                    decimal payingAmount_thisYear_arreas = 0;
                    decimal reamingBalance_thisYear_arreas = (decimal)shBalanceSummary.ThisYearArreas;

                    decimal remaingAmount_afterthisYear_arreas = 0;

                    //#5
                    decimal currentRental = (decimal)shBalanceSummary.CurrentRental;
                    decimal payingAmount_rental = 0;
                    decimal reamingBalance_rental = (decimal)shBalanceSummary.CurrentRental;

                    decimal remaingAmount_afterrental = 0;

                    //#6
                    decimal serviceChargeArreas = (decimal)shBalanceSummary.ServiceChargeArreas;
                    decimal payingAmount_serviceChargeArreas = 0;
                    decimal reamingBalance_serviceChargeArreas = (decimal)shBalanceSummary.ServiceChargeArreas;

                    decimal remaingAmount_afterserviceChargeArreas = 0;

                    //#7
                    decimal currentServiceCharge = (decimal)shBalanceSummary.CurrentServiceCharge;
                    decimal payingAmount_serviceCharge = 0;
                    decimal reamingBalance_serviceCharge = (decimal)shBalanceSummary.CurrentServiceCharge;

                    //#8
                    decimal next_overpayment = 0;

                    //-------------------logic here-----------------------------------
                    //#1 : Last Year Fine - 1st execute
                    if (inputAmount == lastYear_fine)
                    {
                        payingAmount_lastYear_fine = inputAmount;
                        reamingBalance_lastYear_fine = 0;
                    }
                    else if (inputAmount > lastYear_fine)
                    {
                        //previous : a - lastYear_fine
                        payingAmount_lastYear_fine = lastYear_fine;
                        reamingBalance_lastYear_fine = 0;

                        //#2 : last Year Arreas - 2nd execute
                        remaingAmount_afterlastYear_fine = inputAmount - lastYear_fine;

                        if (remaingAmount_afterlastYear_fine == lastYear_arreas)
                        {
                            payingAmount_lastYear_arreas = remaingAmount_afterlastYear_fine;
                            reamingBalance_lastYear_arreas = 0;
                        }
                        else if (remaingAmount_afterlastYear_fine > lastYear_arreas)
                        {
                            //previous : b - lastYear_arreas
                            payingAmount_lastYear_arreas = lastYear_arreas;
                            reamingBalance_lastYear_arreas = 0;

                            //#3 : This Year fine - 3rd execute
                            remaingAmount_afterlastYear_arreas = remaingAmount_afterlastYear_fine - lastYear_arreas;

                            if (remaingAmount_afterlastYear_arreas == thisYear_fine)
                            {
                                payingAmount_thisYear_fine = remaingAmount_afterlastYear_arreas;
                                reamingBalance_thisYear_fine = 0;
                            }
                            else if (remaingAmount_afterlastYear_arreas > thisYear_fine)
                            {
                                //previous : c - thisYear_fine
                                payingAmount_thisYear_fine = thisYear_fine;
                                reamingBalance_thisYear_fine = 0;


                                //#4 : This Year Arreas - 4th execute
                                remaingAmount_afterthisYear_fine = remaingAmount_afterlastYear_arreas - thisYear_fine;

                                if (remaingAmount_afterthisYear_fine == thisYear_arreas)
                                {
                                    payingAmount_thisYear_arreas = remaingAmount_afterthisYear_fine;
                                    reamingBalance_thisYear_arreas = 0;
                                }
                                else if (remaingAmount_afterthisYear_fine > thisYear_arreas)
                                {
                                    //previous : d - thisYear_arreas
                                    payingAmount_thisYear_arreas = thisYear_arreas;
                                    reamingBalance_thisYear_arreas = 0;

                                    //#5 : Rental - 5th execute
                                    remaingAmount_afterthisYear_arreas = remaingAmount_afterthisYear_fine - thisYear_arreas;

                                    if (remaingAmount_afterthisYear_arreas == currentRental)
                                    {
                                        payingAmount_rental = remaingAmount_afterthisYear_arreas;
                                        reamingBalance_rental = 0;
                                    }
                                    else if (remaingAmount_afterthisYear_arreas > currentRental)
                                    {
                                        //previous : e - currentRental
                                        payingAmount_rental = currentRental;
                                        reamingBalance_rental = 0;

                                        //#6 : Service Charge arreas - 6th execute
                                        remaingAmount_afterrental = remaingAmount_afterthisYear_arreas - currentRental;

                                        if (remaingAmount_afterrental == serviceChargeArreas)
                                        {
                                            payingAmount_serviceChargeArreas = remaingAmount_afterrental;
                                            reamingBalance_serviceChargeArreas = 0;
                                        }
                                        else if (remaingAmount_afterrental > serviceChargeArreas)
                                        {
                                            //previous : f - serviceChargeArreas
                                            payingAmount_serviceChargeArreas = serviceChargeArreas;
                                            reamingBalance_serviceChargeArreas = 0;

                                            remaingAmount_afterserviceChargeArreas = remaingAmount_afterrental - serviceChargeArreas;

                                            //#7 : Service Charge - 7th execute
                                            if (remaingAmount_afterserviceChargeArreas == currentServiceCharge)
                                            {
                                                payingAmount_serviceCharge = remaingAmount_afterserviceChargeArreas;
                                                reamingBalance_serviceCharge = 0;
                                            }
                                            else if (remaingAmount_afterserviceChargeArreas > currentServiceCharge)
                                            {
                                                //previous : g - serviceChargeArreas
                                                payingAmount_serviceCharge = currentServiceCharge;
                                                reamingBalance_serviceCharge = 0;

                                                //#8 : Overpayment - 8th execute
                                                //next_overpayment = remaingAmount_afterserviceChargeArreas - currentServiceCharge;

                                                next_overpayment = remaingAmount_afterserviceChargeArreas - currentServiceCharge + (decimal)shBalanceSummary.OverPayment; //modified
                                            }
                                            else
                                            {
                                                //Note: serviceChargeArreas < remaingAmount_afterserviceChargeArreas < currentServiceCharge
                                                payingAmount_serviceCharge = remaingAmount_afterserviceChargeArreas;
                                                reamingBalance_serviceCharge = currentServiceCharge - remaingAmount_afterserviceChargeArreas;
                                            }
                                        }
                                        else
                                        {
                                            //Note: currentRental < remaingAmount_afterrental < serviceChargeArreas
                                            payingAmount_serviceChargeArreas = remaingAmount_afterrental;
                                            reamingBalance_serviceChargeArreas = serviceChargeArreas - remaingAmount_afterrental;
                                        }
                                    }
                                    else
                                    {
                                        //Note: thisYear_arreas < remaingAmount_afterthisYear_arreas < currentRental
                                        payingAmount_rental = remaingAmount_afterthisYear_arreas;
                                        reamingBalance_rental = currentRental - remaingAmount_afterthisYear_arreas;
                                    }
                                }
                                else
                                {
                                    //Note: thisYear_fine < remaingAmount_afterthisYear_fine < thisYear_arreas
                                    payingAmount_thisYear_arreas = remaingAmount_afterthisYear_fine;
                                    reamingBalance_thisYear_arreas = thisYear_arreas - remaingAmount_afterthisYear_fine;
                                }
                            }
                            else
                            {
                                //Note: lastYear_arreas < remaingAmount_afterlastYear_arreas < thisYear_fine
                                payingAmount_thisYear_fine = remaingAmount_afterlastYear_arreas;
                                reamingBalance_thisYear_fine = thisYear_fine - remaingAmount_afterlastYear_arreas;
                            }

                        }
                        else
                        {
                            //Note: lastYear_fine < remaingAmount_afterlastYear_fine < lastYear_arreas
                            payingAmount_lastYear_arreas = remaingAmount_afterlastYear_fine;
                            reamingBalance_lastYear_arreas = lastYear_arreas - remaingAmount_afterlastYear_fine;
                        }
                    }
                    else
                    {
                        //Note: inputAmmount < lastYear_fine
                        payingAmount_lastYear_fine = inputAmount;
                        reamingBalance_lastYear_fine = lastYear_fine - inputAmount;
                    }


                    //-----------------------------------------------------------------


                    return (payingAmount_lastYear_fine, reamingBalance_lastYear_fine, payingAmount_lastYear_arreas, reamingBalance_lastYear_arreas, payingAmount_thisYear_fine, reamingBalance_thisYear_fine, payingAmount_thisYear_arreas, reamingBalance_thisYear_arreas, payingAmount_rental, reamingBalance_rental, payingAmount_serviceChargeArreas, reamingBalance_serviceChargeArreas, payingAmount_serviceCharge, reamingBalance_serviceCharge, next_overpayment);

                }
                else
                {
                    throw new ArgumentNullException(nameof(inputPayAmount), "Input Pay Amount cannot be null.");
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(shBalanceSummary), "Balance cannot be null.");
            }
        }
        //--------[End : UpdatedCalculateShopRentalPaymentBalance]------------


        //---
        public async Task<ShopRentalBalance> GetCurrentMonthBalanceInfo(int shopId, int year, int monthId)
        {
            return await _unitOfWork.ShopRentalBalance.GetCurrentMonthBalanceInfo(shopId, year, monthId);
        }
        //---

        public async Task<ShopRentalBalance> GetLastBalanceInfo(int propertyId, int shopId)
        {
            return await _unitOfWork.ShopRentalBalance.GetLastBalanceInfo(shopId);
        }
        //-----



        //---
        public async Task<IEnumerable<ShopRentalBalance>> GetAllNotCompletedBalanceByShopId(int shopId)
        {
            return await _unitOfWork.ShopRentalBalance.GetAllNotCompletedBalanceByShopId(shopId);
        }
        //-----

        //---
        public async Task<IEnumerable<ShopRentalBalance>> GetAllBalanceByShopId(int shopId)
        {
            return await _unitOfWork.ShopRentalBalance.GetAllBalanceByShopId(shopId);
        }
        //-----
        //*********

        //---
        public async Task<ShopRentalBalance> GetShopRentalBalanceByYearMonth(int year, int month, int shopId)
        {
            return await _unitOfWork.ShopRentalBalance.GetShopRentalNotCompletedBalanceByYearMonth(year, month, shopId);
        }
        //-----

        //---
        //need to change name
        public async Task<IEnumerable<ShopRentalBalance>> GetAllBalanceOfYear(int propertyId, int shopId, int inputYear)
        {
            return await _unitOfWork.ShopRentalBalance.GetAllBalancesOfYear(propertyId, shopId, inputYear);
        }
        //---

        //---
        public async Task<IEnumerable<ShopRentalBalance>> GetAllBalanceInYearUpTOMonth(int propertyId, int shopId, int inputYear, int inputMonth)
        {
            return await _unitOfWork.ShopRentalBalance.GetAllBalanceInYearUpTOMonth(propertyId, shopId, inputYear, inputMonth);
        }
        //---

        //---
        public async Task<ShopRentalBalance> GetMonthlyBalanceInfo(int propertyId, int shopId, int year, int monthId)
        {
            return await _unitOfWork.ShopRentalBalance.GetMonthlyBalanceInfo(propertyId, shopId, year, monthId);
        }
        //---

        //---
        public async Task<IEnumerable<ShopRentalBalance>> GetNotCompletedBalancesByShopIdsByYearMonth(List<int?> shopKeyIds, int year, int month)
        {
            return await _unitOfWork.ShopRentalBalance.GetNotCompletedBalancesByShopIdsByYearMonth(shopKeyIds, year, month);
        }
        //---

        //-----
        //public async Task<(decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal)> calculateTotalPayableAmountByYearMonth(int propertyId, int shopId, int rowYear, int rowMonth)
        //{
        //    decimal payable_LYFine = 0;
        //    decimal payable_LYArreas = 0;

        //    decimal payable_TYFine = 0;
        //    decimal payable_TYArreas = 0;

        //    decimal overpayment = 0;
        //    decimal payable_TYLYServiceChargeArreas = 0;

        //    decimal this_month_rental_settlement = 0;
        //    decimal this_month_service_chaarge_settlement = 0;

        //    decimal total_payable_amount = 0;

        //    var rowBalanceInfo = await _unitOfWork.ShopRentalBalance.GetNotCompletedBalanceInfoByYearMonth(propertyId, shopId, rowYear, rowMonth);

        //    if (rowBalanceInfo != null)
        //    {
        //        payable_LYFine = ((decimal)rowBalanceInfo.LYFine - (decimal)rowBalanceInfo.PaidLYFine);
        //        payable_LYArreas = ((decimal)rowBalanceInfo.LYArreas - (decimal)rowBalanceInfo.PaidLYArreas);

        //        payable_TYFine = ((decimal)rowBalanceInfo.TYFine - (decimal)rowBalanceInfo.PaidTYFine);
        //        payable_TYArreas = ((decimal)rowBalanceInfo.TYArreas - (decimal)rowBalanceInfo.PaidTYArreas);

        //        payable_TYLYServiceChargeArreas = ((decimal)rowBalanceInfo.TYLYServiceChargeArreas - (decimal)rowBalanceInfo.PaidTYLYServiceChargeArreas);

        //        overpayment = (decimal)rowBalanceInfo.OverPaymentAmount;

        //        var shopInfo = await _unitOfWork.Shops.GetById(shopId);
        //        if (shopInfo != null)
        //        {
        //            if (shopInfo.Status == ShopStatus.Active)
        //            {
        //                this_month_rental_settlement = ((decimal)rowBalanceInfo.CurrentRentalAmount - (decimal)rowBalanceInfo.PaidCurrentRentalAmount);
        //                this_month_service_chaarge_settlement = ((decimal)rowBalanceInfo.CurrentServiceChargeAmount - (decimal)rowBalanceInfo.PaidCurrentServiceChargeAmount);
        //            }
        //        }
        //        else
        //        {
        //            throw new ArgumentNullException("Shop information cannot be null");
        //        }

        //        total_payable_amount = payable_LYFine + payable_LYArreas + payable_TYFine + payable_TYArreas + payable_TYLYServiceChargeArreas + this_month_rental_settlement + this_month_service_chaarge_settlement - overpayment;
        //    }
        //    else
        //    {
        //        throw new ArgumentNullException("Last balance information cannot be null");
        //    }

        //    return (payable_LYFine, payable_LYArreas, payable_TYFine, payable_TYArreas, overpayment, payable_TYLYServiceChargeArreas, this_month_rental_settlement, this_month_service_chaarge_settlement, total_payable_amount);
        //}

        //******

        public async Task<decimal> calculateTotalArrerseAmount(int shopId)
        {

            decimal totalArrearsAmount = 0;
            decimal totalFineAmount = 0;
            decimal totalServiceChargeArrersAmount = 0;
            decimal cumulativeTotalArrearsAmount = 0;
            decimal totalRentalAmount = 0;
            decimal totalServiceChargeAmount = 0;

            var shopRentalNotCompletedBalanceInfo = await GetAllNotCompletedBalanceByShopId(shopId);
             if(shopRentalNotCompletedBalanceInfo != null){

                foreach(var balances in shopRentalNotCompletedBalanceInfo)
                {
                    totalArrearsAmount += ((decimal)balances.ArrearsAmount - (decimal)balances.PaidArrearsAmount);
                    totalFineAmount += ((decimal)balances.FineAmount - (decimal)balances.PaidFineAmount);
                    totalServiceChargeArrersAmount += ((decimal)balances.ServiceChargeArreasAmount - (decimal)balances.PaidServiceChargeArreasAmount);
                    totalRentalAmount += (decimal)balances.CurrentRentalAmount;
                    totalServiceChargeAmount += (decimal)balances.CurrentServiceChargeAmount; 

                }
            }

            cumulativeTotalArrearsAmount  = totalArrearsAmount+ totalFineAmount+totalServiceChargeArrersAmount + totalRentalAmount+ totalServiceChargeAmount;

            return cumulativeTotalArrearsAmount;

        }




        public async Task<(decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal)> calculateTotalPayableAmountByYearMonth(int propertyId, int shopId, int rowYear, int rowMonth)
        {
            decimal overpayment = 0;

            decimal this_month_rental_settlement = 0;
            decimal this_month_service_chaarge_settlement = 0;

            decimal total_payable_amount = 0;

            decimal totalLYFine = 0;
            decimal totalLYArreas = 0;
            decimal totalLYServiceChargeArreas = 0;

            decimal totalTYFine = 0;
            decimal totalTYArreas = 0;

            decimal totalTYServiceChargeArreas = 0;
            decimal totalServiceChargeArreas = 0;

            //LY
            var lastyearbal = await _unitOfWork.ShopRentalBalance.GetAllNotCompletedBalanceByYear(shopId, rowYear - 1);
            if (lastyearbal != null && lastyearbal.Count() != 0)
            {
                foreach (var bal in lastyearbal)
                {
                    totalLYFine += ((decimal)bal.FineAmount - (decimal)bal.PaidFineAmount);
                    totalLYArreas += ((decimal)bal.ArrearsAmount - (decimal)bal.PaidArrearsAmount);
                    totalLYServiceChargeArreas += ((decimal)bal.ServiceChargeArreasAmount - (decimal)bal.PaidServiceChargeArreasAmount);
                }
            }

            //TY - up to the previous month
            var thisyearbalUpToPrevMonth = await _unitOfWork.ShopRentalBalance.GetAllNotCompletedBalanceInformationUptoYearMonth(propertyId, shopId, rowYear, rowMonth);
            if (thisyearbalUpToPrevMonth != null && thisyearbalUpToPrevMonth.Count() != 0)
            {
                foreach (var bal in thisyearbalUpToPrevMonth)
                {
                    totalTYFine += ((decimal)bal.FineAmount - (decimal)bal.PaidFineAmount);
                    totalTYArreas += ((decimal)bal.ArrearsAmount - (decimal)bal.PaidArrearsAmount);
                    totalTYServiceChargeArreas += ((decimal)bal.ServiceChargeArreasAmount - (decimal)bal.PaidServiceChargeArreasAmount);
                }
            }

            //Current month
            var currentMonthBalanceInfo = await _unitOfWork.ShopRentalBalance.GetCurrentMonthBalanceInfo(shopId, rowYear, rowMonth);
            if (currentMonthBalanceInfo != null)
            {
                if(currentMonthBalanceInfo.IsCompleted == false)
                {
                    var shopInfo = await _unitOfWork.Shops.GetById(shopId);
                    if (shopInfo != null)
                    {
                        if (shopInfo.Status == ShopStatus.Active)
                        {
                            this_month_rental_settlement = ((decimal)shopInfo.Rental - (decimal)currentMonthBalanceInfo.PaidCurrentRentalAmount);
                            this_month_service_chaarge_settlement = ((decimal)shopInfo.ServiceCharge - (decimal)currentMonthBalanceInfo.PaidCurrentServiceChargeAmount);
                        }
                        else
                        {
                            this_month_rental_settlement =0;
                            this_month_service_chaarge_settlement = 0;
                        }
                    }
                    else
                    {
                        throw new ArgumentNullException("Shop information cannot be null");
                    }
                }
                else
                {
                    overpayment = (decimal)currentMonthBalanceInfo.OverPaymentAmount;
                }
            }

            totalServiceChargeArreas = totalLYServiceChargeArreas + totalTYServiceChargeArreas;

            total_payable_amount = totalLYFine + totalLYArreas + totalTYFine + totalTYArreas + totalServiceChargeArreas + this_month_rental_settlement + this_month_service_chaarge_settlement;

            return (totalLYFine, totalLYArreas, totalTYFine, totalTYArreas, overpayment, totalServiceChargeArreas, this_month_rental_settlement, this_month_service_chaarge_settlement, total_payable_amount);
        }
        //------------[End: calculateTotalTyLyAmountForYearMonth()-----------------------------


        //---
        public async Task<IEnumerable<ShopRentalBalance>> GetAllNotCompletedBalanceInformationOfShop(int shopId)
        {
            return await _unitOfWork.ShopRentalBalance.GetAllNotCompletedBalanceInformationOfShop(shopId);
        }
        //--------[End : basic data get APIs]------------



        //------------[Start: calculateTotalTyLyAmountForYearMonth()-----------------------------
        public async Task<(decimal, decimal, decimal, decimal, decimal)> calculateTotalTyLyArreasAmountForYearMonth(int shopId, int inputYear, int inputMonth)
        {
            decimal totalLYFine = 0;
            decimal totalLYArreas = 0;
            decimal totalLYServiceChargeArreas = 0;

            decimal totalTYFine = 0;
            decimal totalTYArreas = 0;

            decimal totalTYServiceChargeArreas = 0;
            decimal totalServiceChargeArreas = 0;

            //LY
            var lastyearbal = await _unitOfWork.ShopRentalBalance.GetAllNotCompletedBalanceByYear(shopId, inputYear - 1);

            if (lastyearbal != null && lastyearbal.Count() != 0)
            {
                foreach (var bal in lastyearbal)
                {
                    totalLYFine += ((decimal)bal.FineAmount - (decimal)bal.PaidFineAmount);
                    totalLYArreas += ((decimal)bal.ArrearsAmount - (decimal)bal.PaidArrearsAmount);
                    totalLYServiceChargeArreas += ((decimal)bal.ServiceChargeArreasAmount - (decimal)bal.PaidServiceChargeArreasAmount);
                }
            }

            //TY
            var thisyearbal = await _unitOfWork.ShopRentalBalance.GetAllNotCompletedBalanceByYear(shopId, inputYear);
            if (thisyearbal != null && thisyearbal.Count() != 0)
            {
                foreach (var bal in thisyearbal)
                {
                    totalTYFine += ((decimal)bal.FineAmount - (decimal)bal.PaidFineAmount);
                    totalTYArreas += ((decimal)bal.ArrearsAmount - (decimal)bal.PaidArrearsAmount);
                    totalTYServiceChargeArreas += ((decimal)bal.ServiceChargeArreasAmount - (decimal)bal.PaidServiceChargeArreasAmount);
                }
            }
            totalServiceChargeArreas = totalLYServiceChargeArreas + totalTYServiceChargeArreas;

            return (totalLYFine, totalLYArreas, totalTYFine, totalTYArreas, totalServiceChargeArreas);
        }
        //------------[End: calculateTotalTyLyAmountForYearMonth()-----------------------------


















        //*************************************************************************************
        //Modification : 23024/06/13
        //*************************************************************************************
        //--------[Start : CalculateShopRentalPaymentBalanceRowWise]------------
        public async Task<HShopRentalCalculator> CalculateShopRentalPaymentBalanceRowWise(int shopId, int currentYear, int currentMonth, decimal inputPayAmount)
        {
            if (inputPayAmount >= 0)
            {
                //****************************
                decimal paying_Total_LY_fine = 0;
                decimal paying_Total_LY_arreas = 0;
                decimal paying_Total_LY_serviceCharge_arreas = 0;

                decimal paying_Total_TY_fine = 0;
                decimal paying_Total_TY_arreas = 0;
                decimal paying_Total_TY_serviceCharge_arreas = 0;

                decimal paying_rental = 0;
                decimal paying_service_charge = 0;

                decimal next_overpayment = 0;

                decimal remainderAfterRow = 0;

                var rowDetails = new List<HShopRentalCalculatorRowDetails>();
                decimal payingTotal = 0;

                bool rowFineCompleted = false;
                bool rowArreasCompleted = false;
                bool rowServiceChargeArreasCompleted = false;
                bool rowRentalCompleted = false;
                bool rowServiceChargeCompleted = false;
                bool rowCompleted = false;
                //****************************

                //#1 - Get All not completed balances
                var shNotCompletedBalanceInfo = await _unitOfWork.ShopRentalBalance.GetAllNotCompletedBalanceInformationOfShop(shopId);

                if (shNotCompletedBalanceInfo != null && shNotCompletedBalanceInfo.Count() !=0)
                {
                    // Get the year of the last record
                    int ty = shNotCompletedBalanceInfo.LastOrDefault()?.Year ?? 0;
                    int ty_lastMonth = shNotCompletedBalanceInfo.LastOrDefault()?.Month ?? 0;

                    // Get all years less than ty
                    var lys = shNotCompletedBalanceInfo.Where(b => b.Year < ty).Select(b => b.Year).ToList();

                    foreach (var balrow in shNotCompletedBalanceInfo)
                    {

                        if (lys.Contains(balrow.Year))
                        {
                            //last year records
                            (decimal paying_LY_fine, decimal paying_LY_arreas, decimal paying_LY_serviceCharge_arreas, paying_rental, paying_service_charge, remainderAfterRow, decimal payingRowTotal, rowFineCompleted, rowArreasCompleted, rowServiceChargeArreasCompleted, rowRentalCompleted, rowServiceChargeCompleted, rowCompleted) = RowDeductor((inputPayAmount - payingTotal), (decimal)(balrow.FineAmount - balrow.PaidFineAmount), (decimal)(balrow.ArrearsAmount - balrow.PaidArrearsAmount), (decimal)(balrow.ServiceChargeArreasAmount - balrow.PaidServiceChargeArreasAmount), 0, 0, 0);

                            paying_Total_LY_fine                 += paying_LY_fine;
                            paying_Total_LY_arreas               += paying_LY_arreas;
                            paying_Total_LY_serviceCharge_arreas += paying_LY_serviceCharge_arreas;
                            payingTotal                          += payingRowTotal;

                        }
                        else
                        {
                            //this year records
                            if (ty_lastMonth != balrow.Month)
                            {
                                //previous months
                                (decimal paying_TY_fine, decimal paying_TY_arreas, decimal paying_TY_serviceCharge_arreas, paying_rental, paying_service_charge, remainderAfterRow, decimal payingRowTotal, rowFineCompleted, rowArreasCompleted, rowServiceChargeArreasCompleted, rowRentalCompleted, rowServiceChargeCompleted, rowCompleted) = RowDeductor((inputPayAmount - payingTotal), (decimal)(balrow.FineAmount - balrow.PaidFineAmount), (decimal)(balrow.ArrearsAmount - balrow.PaidArrearsAmount), (decimal)(balrow.ServiceChargeArreasAmount - balrow.PaidServiceChargeArreasAmount), 0, 0, 0);

                                paying_Total_TY_fine                 += paying_TY_fine;
                                paying_Total_TY_arreas               += paying_TY_arreas;
                                paying_Total_TY_serviceCharge_arreas += paying_TY_serviceCharge_arreas;
                                payingTotal                          += payingRowTotal;
                            }
                            else
                            {
                                //this month
                                (decimal paying_TY_fine, decimal paying_TY_arreas, decimal paying_TY_serviceCharge_arreas, paying_rental, paying_service_charge, next_overpayment, decimal payingRowTotal, rowFineCompleted, rowArreasCompleted, rowServiceChargeArreasCompleted, rowRentalCompleted, rowServiceChargeCompleted, rowCompleted) = RowDeductor((inputPayAmount - payingTotal), (decimal)(balrow.FineAmount - balrow.PaidFineAmount), (decimal)(balrow.ArrearsAmount - balrow.PaidArrearsAmount), (decimal)(balrow.ServiceChargeArreasAmount - balrow.PaidServiceChargeArreasAmount), (decimal)balrow.OverPaymentAmount, ((decimal)(balrow.Shop.Rental - balrow.PaidCurrentRentalAmount)), ((decimal)(balrow.Shop.ServiceCharge - balrow.PaidCurrentServiceChargeAmount)));

                                paying_Total_TY_fine        += paying_TY_fine;
                                payingTotal                 += payingRowTotal;
                            }
                        }
                        ///-------------------------------
                        //********************
                        rowDetails.Add(new HShopRentalCalculatorRowDetails
                        {
                            ShopRentalBalanceRow = balrow,

                            IsRowFineCovered = rowFineCompleted,
                            IsRowArreasCovered = rowArreasCompleted,
                            IsRowServiceChargeArreasCovered = rowServiceChargeArreasCompleted,
                            IsRowRentalCovered = rowRentalCompleted,
                            IsRowServiceChargeCovered = rowServiceChargeCompleted,
                            IsRowCovered = rowCompleted,

                        });
                        //********************
                        ///-------------------------------
                    }
                }
                else
                {
                    //nothing to settle overpay
                    var currentMonthBalanceInfo = await _unitOfWork.ShopRentalBalance.GetCurrentMonthBalanceInfo(shopId, currentYear, currentMonth);

                    if(currentMonthBalanceInfo != null)
                    {
                        (decimal paying_TY_fine, decimal paying_TY_arreas, decimal paying_TY_serviceCharge_arreas, paying_rental, paying_service_charge, next_overpayment, payingTotal, rowFineCompleted, rowArreasCompleted, rowServiceChargeArreasCompleted, rowRentalCompleted, rowServiceChargeCompleted, rowCompleted) = RowDeductor((inputPayAmount - payingTotal), (decimal)(currentMonthBalanceInfo.FineAmount - currentMonthBalanceInfo.PaidFineAmount), (decimal)(currentMonthBalanceInfo.ArrearsAmount - currentMonthBalanceInfo.PaidArrearsAmount), (decimal)(currentMonthBalanceInfo.ServiceChargeArreasAmount - currentMonthBalanceInfo.PaidServiceChargeArreasAmount), (decimal)currentMonthBalanceInfo.OverPaymentAmount, ((decimal)(currentMonthBalanceInfo.Shop.Rental - currentMonthBalanceInfo.PaidCurrentRentalAmount)), ((decimal)(currentMonthBalanceInfo.Shop.ServiceCharge - currentMonthBalanceInfo.PaidCurrentServiceChargeAmount)));

                        //********************
                        rowDetails.Add(new HShopRentalCalculatorRowDetails
                        {
                            ShopRentalBalanceRow = currentMonthBalanceInfo,

                            IsRowFineCovered = rowFineCompleted,
                            IsRowArreasCovered = rowArreasCompleted,
                            IsRowServiceChargeArreasCovered = rowServiceChargeArreasCompleted,
                            IsRowRentalCovered = rowRentalCompleted,
                            IsRowServiceChargeCovered = rowServiceChargeCompleted,
                            IsRowCovered = rowCompleted,
                        });
                        //********************
                    }
                    else
                    {
                        throw new ArgumentNullException("Current month balance cannot be null");
                    }
                }

                //**********************
                var summaryResult = new HShopRentalCalculatorSummary
                {
                    PayingAmount_lastYear_fine = paying_Total_LY_fine,
                    PayingAmount_lastYear_arreas = paying_Total_LY_arreas,
                    PayingAmount_thisYear_fine = paying_Total_TY_fine,
                    PayingAmount_thisYear_arreas = paying_Total_TY_arreas,
                    PayingAmount_serviceChargeArreas = paying_Total_LY_serviceCharge_arreas + paying_Total_TY_serviceCharge_arreas,
                    PayingAmount_rental = paying_rental,
                    PayingAmount_serviceCharge = paying_service_charge,
                    Next_overpayment = next_overpayment,
                };

                var result = new HShopRentalCalculator
                {
                    ShopRentalCalculatorRowDetails = rowDetails,

                    ShopRentalCalculatorSummary = summaryResult,
                };
                //**********************
                return (result);
            }
            else
            {
                throw new ArgumentNullException("Input pay amount should be greater than zero");
            }
        }
        //--------[End : CalculateShopRentalPaymentBalanceRowWise]------------


        //----
        private (decimal, decimal, decimal, decimal, decimal, decimal, decimal, bool, bool, bool, bool, bool, bool) RowDeductor(decimal inputAmount, decimal rowFine, decimal rowArreas, decimal rowServiceChargeArreas, decimal rowOverpayment, decimal rowRental, decimal rowServiceCharge)
        
        {
            //*************************
            //#1-Fine
            //#2-Arreas
            //#3-Service Charge Arreas
            //#4-Rental
            //#5-serviceCharge
            //*************************


            decimal paying_fine = 0;
            decimal remaining_after_Fine = 0;
            decimal paying_arreas = 0;
            decimal remaining_after_Arreas = 0;
            decimal paying_serviceCharge_arreas = 0;
            decimal remaining_after_serviceCharge_arreas = 0;
            decimal paying_rental = 0;
            decimal remaining_after_rental = 0;
            decimal paying_serviceCharge = 0;
            decimal remaining_after_serviceCharge = 0;
            decimal remaining_after_row = 0;

            decimal payingTotal = 0;

            bool rowFineCompleted   = false;
            bool rowArreasCompleted  = false;
            bool rowServiceChargeArreasCompleted = false;
            bool rowRentalCompleted = false;
            bool rowServiceChargeCompleted = false;
            bool rowCompleted = false;


            //#1 - fine
            if (inputAmount >= rowFine)
            {
                paying_fine      = rowFine;
                rowFineCompleted = true;

                remaining_after_Fine = inputAmount - rowFine;
                //#2 - arreas
                if (remaining_after_Fine >= rowArreas)
                {
                    paying_arreas       = rowArreas;
                    rowArreasCompleted  = true;

                    remaining_after_Arreas = remaining_after_Fine - rowArreas;

                    //#3 - service charge arreas
                    if(remaining_after_Arreas >= rowServiceChargeArreas)
                    {
                        paying_serviceCharge_arreas     = rowServiceChargeArreas;
                        rowServiceChargeArreasCompleted = true;

                        remaining_after_serviceCharge_arreas = remaining_after_Arreas - rowServiceChargeArreas;

                        //#4 - Rental
                        if (remaining_after_serviceCharge_arreas >= rowRental)
                        {
                            paying_rental       = rowRental;
                            rowRentalCompleted  = true;

                            remaining_after_rental = remaining_after_serviceCharge_arreas - rowRental;

                            //#5 -Service charge
                            if (remaining_after_rental >= rowServiceCharge)
                            {
                                paying_serviceCharge            = rowServiceCharge;
                                rowServiceChargeCompleted       = true;

                                remaining_after_serviceCharge   = remaining_after_rental - rowServiceCharge;
                                rowCompleted                    = true;

                                //#6 - overpayment
                                remaining_after_row             = remaining_after_serviceCharge + rowOverpayment; //This will be the inputAmount to next cycle.
                            }
                            else
                            {
                                paying_serviceCharge = remaining_after_rental;
                            }
                        }
                        else
                        {
                            paying_rental = remaining_after_serviceCharge_arreas;
                        }
                    }
                    else
                    {
                        paying_serviceCharge_arreas = remaining_after_Arreas;
                    }
                }
                else
                {
                    paying_arreas = remaining_after_Fine;
                }
            }
            else
            {
                paying_fine = inputAmount;
            }

            //-------
            payingTotal = paying_fine + paying_arreas + paying_serviceCharge_arreas + paying_rental + paying_serviceCharge;
            //-------

            return (paying_fine, paying_arreas, paying_serviceCharge_arreas, paying_rental, paying_serviceCharge, remaining_after_row, payingTotal, rowFineCompleted, rowArreasCompleted, rowServiceChargeArreasCompleted, rowRentalCompleted, rowServiceChargeCompleted, rowCompleted);
        }
        //-----       
        //*************************************************************************************
    }
}
