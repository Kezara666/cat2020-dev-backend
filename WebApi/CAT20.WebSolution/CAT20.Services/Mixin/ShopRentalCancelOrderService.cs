using CAT20.Core;
using CAT20.Core.Models.Enums;
using CAT20.Core.Services.Mixin;
using CAT20.Core.Services.ShopRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.Mixin
{
    public class ShopRentalCancelOrderService : IShopRentalCancelOrderService
    {
        
        private readonly IMixinUnitOfWork _unitOfWork;
        private readonly IShopRentalBalanceService _shopRentalBalanceService; //Note : modified : 2024/04/03

        public ShopRentalCancelOrderService(IMixinUnitOfWork unitOfWork, IShopRentalBalanceService shopRentalBalanceService)
        {
            _unitOfWork = unitOfWork;
            _shopRentalBalanceService = shopRentalBalanceService;
        }
         
        public async Task<bool> ReverseShopRentalPayment(int mixId, int shopId, int approvedBy)
        {
            try
            {
                //#0 - Extracts the first 2 (Sorted by descending order) shoprental-mixinOrders : last two shop renral mixin payments of a shop
                var mxs = await _unitOfWork.MixinOrders.GetForReversePaymentShopRentalPayment(shopId);

                //#0 - get mixinOrder info by mixId
                var mx = await _unitOfWork.MixinOrders.GetByIdAsync(mixId);


                if (  (mxs != null && mx != null && mx.Equals(mxs.First()) )  ||  (mxs != null && mxs.First() != null && mxs.First().State == OrderStatus.Cancel_Pending && mxs.Last() != null && mxs.Last().Id == mixId))
                {
                    if(!await ReverseProcessShopRentalPayment(mixId, approvedBy, shopId)){

                        throw new Exception("Shop rental reverse update process failed");
                    }
                }
                else
                {
                    var lastCode = mxs.Last()?.Code?? "N/A";
                    throw new Exception($"Unable to delete shop rental order. Last mixin order ID: {lastCode}");
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //-----



        //--------[Start : ReverseProcessShopRentalPayment)]------------
        private async Task<bool> ReverseProcessShopRentalPayment(int mixinOrderId, int cashierId, int shopId)
        {
            try
            { 
                decimal RevPayingAmount_lastYear_fine = 0;
                decimal RevPayingAmount_lastYear_arreas = 0;
                decimal RevPayingAmount_thisYear_fine = 0;
                decimal RevPayingAmount_thisYear_arreas = 0;
                decimal RevPayingAmount_rental = 0;
                decimal RevPayingAmount_serviceChargeArreas = 0;
                decimal RevPayingAmount_serviceCharge = 0;
                decimal Revnext_overpayment = 0;

                //------ [Start - note] ------
                /*
                    LastYearFineVote        = 1,
                    LastYearArreasVote      = 2,
                    ThisYearFineVote        = 3,
                    ThisYearArreasVote      = 4,
                    ShopRenalVote           = 5,
                    ServiceChargeArreasVote = 6,
                    ServiceChargeVote       = 7,
                    OverpaymentVote         = 8,
                 */
                //------ [End - note] --------

                //#1 get all mixin order line for the mixinOrder 
                var mixinOrderLinesForShopRental = await _unitOfWork.MixinOrderLines.GetAllForOrderId(mixinOrderId);

                foreach (var mixinOrderLineForShopRental in mixinOrderLinesForShopRental)
                {
                    if (mixinOrderLineForShopRental.VotePaymentTypeId == 1)
                    {
                        RevPayingAmount_lastYear_fine = (decimal)mixinOrderLineForShopRental.Amount;
                    }
                    else if (mixinOrderLineForShopRental.VotePaymentTypeId == 2)
                    {
                        RevPayingAmount_lastYear_arreas = (decimal)mixinOrderLineForShopRental.Amount;
                    }
                    else if (mixinOrderLineForShopRental.VotePaymentTypeId == 3)
                    {
                        RevPayingAmount_thisYear_fine = (decimal)mixinOrderLineForShopRental.Amount;
                    }
                    else if (mixinOrderLineForShopRental.VotePaymentTypeId == 4)
                    {
                        RevPayingAmount_thisYear_arreas = (decimal)mixinOrderLineForShopRental.Amount;
                    }
                    else if (mixinOrderLineForShopRental.VotePaymentTypeId == 5)
                    {
                        RevPayingAmount_rental = (decimal)mixinOrderLineForShopRental.Amount;
                    }
                    else if (mixinOrderLineForShopRental.VotePaymentTypeId == 6)
                    {
                        RevPayingAmount_serviceChargeArreas = (decimal)mixinOrderLineForShopRental.Amount;
                    }
                    else if (mixinOrderLineForShopRental.VotePaymentTypeId == 7)
                    {
                        RevPayingAmount_serviceCharge = (decimal)mixinOrderLineForShopRental.Amount;
                    }
                    else
                    {
                        //mixinOrderLineForShopRental.VotePaymentTypeId == 8
                        Revnext_overpayment = (decimal)mixinOrderLineForShopRental.Amount;
                    }
                }

                //#2 ReverseUpdate shop rental balance table
                if(!await ReverseUpdate(shopId, RevPayingAmount_lastYear_fine, RevPayingAmount_lastYear_arreas, RevPayingAmount_thisYear_fine, RevPayingAmount_thisYear_arreas, RevPayingAmount_rental, RevPayingAmount_serviceChargeArreas, RevPayingAmount_serviceCharge, Revnext_overpayment, cashierId))
                {
                    throw new Exception("Reverse update Failed");
                }
                else
                {
                    //If successful then disable all locks
                    var shBalannces = await _unitOfWork.ShopRentalBalances.GetAllBalanceByShopId(shopId);

                    foreach (var sh in shBalannces)
                    {
                        sh.HasTransaction = false; //disable lock
                    }
                }
                //-------- 

                return true;
            }
            catch (Exception ex)
            {
               return false;
            }
        }
        //--------[End : ReverseProcessShopRentalPayment)]------------



        //**********************************************
        //----------------[Start : ReverseUpdate] ------
        private async Task<bool> ReverseUpdate(int shopId, decimal RevePaidAmount_lastYear_fine, decimal RevPaidAmount_lastYear_arreas, decimal RevPaidAmount_thisYear_fine, decimal RevPaidAmount_thisYear_arreas, decimal RevPaidAmount_rental, decimal RevPaidAmount_serviceChargeArreas, decimal RevPaidAmount_serviceCharge, decimal Revnext_overpayment, int cashierId)
        {
            try
            {
                //variables for update
                //ly
                decimal lypaidFineToBeUpdated = 0;
                decimal lypaidArreasToBeUpdated = 0;

                decimal lypaidServiceChargeArreasToBeUpdated = 0;




                //ty
                decimal typaidFineToBeUpdated = 0;
                decimal typaidArreasToBeUpdated = 0;

                decimal typaidServiceChargeArreasToBeUpdated = 0;
                decimal tyoverPaymentToBeUpdated = 0;


                //----- [Start - logic]--------
                //need to check this: reverse only possible before monthend and fine process

                var lastBalanceRecordInfo = await _unitOfWork.ShopRentalBalances.GetLastBalanceInfo(shopId);

                //-** This month record *****
                if (RevePaidAmount_lastYear_fine >= 0 && RevPaidAmount_lastYear_arreas >= 0 && RevPaidAmount_thisYear_fine >= 0 && RevPaidAmount_thisYear_arreas >= 0 && RevPaidAmount_serviceCharge >= 0 && RevPaidAmount_rental >= 0)
                {
                    if (lastBalanceRecordInfo != null && lastBalanceRecordInfo.IsProcessed == true && lastBalanceRecordInfo.HasTransaction == true)
                    
                    {
                        //Is completed
                        if (
                            //(lastBalanceRecordInfo.LYFine != lastBalanceRecordInfo.PaidLYFine - RevePaidAmount_lastYear_fine) ||
                            //(lastBalanceRecordInfo.LYArreas != lastBalanceRecordInfo.PaidLYArreas - RevPaidAmount_lastYear_arreas) ||
                            //(lastBalanceRecordInfo.TYFine != lastBalanceRecordInfo.PaidTYFine - RevPaidAmount_thisYear_fine) ||
                            //(lastBalanceRecordInfo.TYArreas != lastBalanceRecordInfo.PaidTYArreas - RevPaidAmount_thisYear_arreas) ||
                            //(lastBalanceRecordInfo.TYLYServiceChargeArreas != lastBalanceRecordInfo.PaidTYLYServiceChargeArreas - RevPaidAmount_serviceChargeArreas) ||
                            (lastBalanceRecordInfo.Shop.Rental != lastBalanceRecordInfo.PaidCurrentRentalAmount - RevPaidAmount_rental) ||
                            (lastBalanceRecordInfo.Shop.ServiceCharge != lastBalanceRecordInfo.PaidServiceChargeArreasAmount - RevPaidAmount_serviceCharge)
                        )
                        {
                            lastBalanceRecordInfo.IsCompleted = false;
                        }

                        //lastBalanceRecordInfo.HasTransaction = false;

                        //---------- [Start: get last record] ---------------------
                        int lastrecordYear = lastBalanceRecordInfo.Year;
                        int lastrecordMonth = lastBalanceRecordInfo.Month;
                        //---------- [End: get last record] -----------------------


                        //------[Start: fields for Report]-------
                        if (RevePaidAmount_lastYear_fine > 0)
                        {
                            lastBalanceRecordInfo.PaidLYFine -= RevePaidAmount_lastYear_fine;
                        }

                        if(RevPaidAmount_lastYear_arreas > 0)
                        {
                            lastBalanceRecordInfo.PaidLYArreas -= RevPaidAmount_lastYear_arreas;
                        }

                        if(RevPaidAmount_thisYear_fine > 0)
                        {
                            //------This month fine process case
                            if (lastBalanceRecordInfo.PaidCurrentMonthNewFine > 0)
                            {
                                lastBalanceRecordInfo.PaidTYFine -= (RevPaidAmount_thisYear_fine - (lastBalanceRecordInfo.PaidCurrentMonthNewFine));

                                decimal reduced_tyFine = 0;
                                var processConfigAssignmentInfo = await _unitOfWork.ProcessConfigurationSettingAssign.GetByShopId(shopId);

                                if (lastBalanceRecordInfo.PaidCurrentMonthNewFine <= RevPaidAmount_thisYear_fine)
                                {
                                    lastBalanceRecordInfo.PaidCurrentMonthNewFine -= (decimal)lastBalanceRecordInfo.PaidFineAmount; //Report field

                                    if (processConfigAssignmentInfo.ShopRentalProcessConfigaration.RentalPaymentDateTypeId != 1)
                                    {
                                        reduced_tyFine = (decimal)lastBalanceRecordInfo.PaidFineAmount;
                                        lastBalanceRecordInfo.PaidFineAmount -= (decimal)lastBalanceRecordInfo.PaidFineAmount;
                                    }
                                }
                                else
                                {
                                    lastBalanceRecordInfo.PaidCurrentMonthNewFine = 0; //Report field

                                    if (processConfigAssignmentInfo.ShopRentalProcessConfigaration.RentalPaymentDateTypeId != 1)
                                    {
                                        reduced_tyFine = (decimal)lastBalanceRecordInfo.PaidFineAmount;
                                        lastBalanceRecordInfo.PaidFineAmount = 0;
                                    }
                                }
                                RevPaidAmount_thisYear_fine -= reduced_tyFine;
                            }
                            //-------
                        }

                        if (RevPaidAmount_thisYear_arreas > 0)
                        {
                            lastBalanceRecordInfo.PaidTYArreas -= RevPaidAmount_thisYear_arreas;
                        }

                        if (RevPaidAmount_serviceCharge > 0)
                        {
                            lastBalanceRecordInfo.PaidCurrentServiceChargeAmount -= RevPaidAmount_serviceCharge;
                        }

                        if(RevPaidAmount_rental > 0)
                        {
                            lastBalanceRecordInfo.PaidCurrentRentalAmount -= RevPaidAmount_rental;
                        }
                        //------[End: fields for Report]---------

                        if (Revnext_overpayment > 0)
                        {
                            lastBalanceRecordInfo.OverPaymentAmount -= Revnext_overpayment;
                        }

                        if(lastBalanceRecordInfo.NoOfPayments > 0) {
                            lastBalanceRecordInfo.NoOfPayments -= 1;
                        }

                        lastBalanceRecordInfo.UpdatedBy = cashierId;
                        lastBalanceRecordInfo.UpdatedAt = DateTime.Now;

                        if (lastBalanceRecordInfo.PaidCurrentServiceChargeAmount == 0 &&
                            lastBalanceRecordInfo.PaidCurrentRentalAmount == 0)
                        {
                            lastBalanceRecordInfo.IsProcessed = false;
                        }

                        await _unitOfWork.CommitAsync();

                        //-** TY-LY case *****
                        var shopAllBalancInfo = await _unitOfWork.ShopRentalBalances.GetAllBalanceForShopProperty(shopId);

                        if (shopAllBalancInfo != null && shopAllBalancInfo.Count() != 0)
                        {
                            if (RevPaidAmount_thisYear_fine > 0 || RevPaidAmount_thisYear_arreas > 0 || RevPaidAmount_serviceChargeArreas > 0)
                            {
                                //TY case
                                for (int month = lastrecordMonth; month >= 1; month--)
                                {
                                    if (RevPaidAmount_thisYear_fine == 0 && RevPaidAmount_thisYear_arreas == 0 && RevPaidAmount_serviceChargeArreas == 0)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        decimal reduced_revTYFine = 0;
                                        decimal reduced_revTYArreas = 0;
                                        decimal reduced_revTYServiceCharge = 0;

                                        if (month != lastrecordMonth)
                                        {
                                            var shRowThisYearMonthlyBalance = await _unitOfWork.ShopRentalBalances.GetShopRentalBalanceByYearMonthForReversePayment(lastrecordYear, month, shopId);

                                            if (shRowThisYearMonthlyBalance != null)
                                            {
                                                //fine--------
                                                if (shRowThisYearMonthlyBalance.PaidFineAmount > 0)
                                                {
                                                    if (RevPaidAmount_thisYear_fine < shRowThisYearMonthlyBalance.PaidFineAmount)
                                                    {
                                                        typaidFineToBeUpdated = (decimal)shRowThisYearMonthlyBalance.PaidFineAmount - RevPaidAmount_thisYear_fine;

                                                        reduced_revTYFine = RevPaidAmount_thisYear_fine;
                                                    }
                                                    else
                                                    {
                                                        typaidFineToBeUpdated = 0;

                                                        reduced_revTYFine = (decimal)shRowThisYearMonthlyBalance.PaidFineAmount;
                                                    }
                                                }
                                                //------------

                                                //arreas
                                                if (shRowThisYearMonthlyBalance.PaidArrearsAmount > 0)
                                                {
                                                    if (RevPaidAmount_thisYear_arreas < shRowThisYearMonthlyBalance.PaidArrearsAmount)
                                                    {
                                                        typaidArreasToBeUpdated = (decimal)shRowThisYearMonthlyBalance.PaidArrearsAmount - RevPaidAmount_thisYear_arreas;

                                                        reduced_revTYArreas = RevPaidAmount_thisYear_arreas;
                                                    }
                                                    else
                                                    {
                                                        typaidArreasToBeUpdated = 0;

                                                        reduced_revTYArreas = (decimal)shRowThisYearMonthlyBalance.PaidArrearsAmount;
                                                    }
                                                }
                                                //------------

                                                //serviceChargeArreas
                                                if (shRowThisYearMonthlyBalance.PaidServiceChargeArreasAmount > 0)
                                                {
                                                    if (RevPaidAmount_serviceChargeArreas < shRowThisYearMonthlyBalance.PaidServiceChargeArreasAmount)
                                                    {
                                                        typaidServiceChargeArreasToBeUpdated = (decimal)shRowThisYearMonthlyBalance.PaidServiceChargeArreasAmount - RevPaidAmount_serviceChargeArreas;

                                                        reduced_revTYServiceCharge = RevPaidAmount_serviceChargeArreas;
                                                    }
                                                    else
                                                    {
                                                        typaidServiceChargeArreasToBeUpdated = 0;

                                                        reduced_revTYServiceCharge = (decimal)shRowThisYearMonthlyBalance.PaidServiceChargeArreasAmount;
                                                    }
                                                }
                                                //------------


                                                //Is completed
                                                if (
                                                    (shRowThisYearMonthlyBalance.FineAmount != shRowThisYearMonthlyBalance.PaidFineAmount - RevPaidAmount_thisYear_fine) ||
                                                    (shRowThisYearMonthlyBalance.ArrearsAmount != shRowThisYearMonthlyBalance.PaidArrearsAmount - RevPaidAmount_thisYear_arreas) ||
                                                    (shRowThisYearMonthlyBalance.ServiceChargeArreasAmount != shRowThisYearMonthlyBalance.PaidServiceChargeArreasAmount - RevPaidAmount_serviceChargeArreas)
                                                )
                                                {
                                                    shRowThisYearMonthlyBalance.IsCompleted = false;
                                                }

                                                //-------updating table row
                                                shRowThisYearMonthlyBalance.UpdatedBy = cashierId;
                                                shRowThisYearMonthlyBalance.PaidFineAmount = typaidFineToBeUpdated;
                                                shRowThisYearMonthlyBalance.PaidArrearsAmount = typaidArreasToBeUpdated;
                                                shRowThisYearMonthlyBalance.PaidServiceChargeArreasAmount = typaidServiceChargeArreasToBeUpdated;

                                                //---
                                                shRowThisYearMonthlyBalance.NoOfPayments = shRowThisYearMonthlyBalance.NoOfPayments - 1;
                                                //---

                                                //---
                                                //shRowThisYearMonthlyBalance.HasTransaction = false;
                                                //---

                                                await _unitOfWork.CommitAsync();

                                                //next iteration
                                                if (RevPaidAmount_thisYear_fine != 0)
                                                {
                                                    RevPaidAmount_thisYear_fine = RevPaidAmount_thisYear_fine - reduced_revTYFine;
                                                }

                                                if (RevPaidAmount_thisYear_arreas != 0)
                                                {
                                                    RevPaidAmount_thisYear_arreas = RevPaidAmount_thisYear_arreas - reduced_revTYArreas;
                                                }

                                                if (RevPaidAmount_serviceChargeArreas != 0)
                                                {
                                                    RevPaidAmount_serviceChargeArreas = RevPaidAmount_serviceChargeArreas - reduced_revTYServiceCharge;
                                                }
                                            }

                                        }
                                    }
                                }
                            }
                            


                            //LY case
                            var lyears = shopAllBalancInfo.Where(y => y.Year < lastrecordYear).Select(y => y.Year).Distinct().ToList();

                            if (lyears.Count() != 0)
                            {
                                foreach (var year in lyears)
                                {
                                    if (RevePaidAmount_lastYear_fine > 0 || RevPaidAmount_lastYear_arreas > 0 || RevPaidAmount_serviceChargeArreas > 0)
                                    {
                                        for (int month = 12; month >= 1; month--)
                                        {
                                            if (RevePaidAmount_lastYear_fine == 0 && RevPaidAmount_lastYear_arreas == 0 && RevPaidAmount_serviceChargeArreas == 0)
                                            {
                                                break;
                                            }
                                            else
                                            {
                                                decimal reduced_revLYFine = 0;
                                                decimal reduced_revLYArreas = 0;
                                                decimal reduced_revLYServiceCharge = 0;

                                                var shRowLastYearMonthlyBalance = await _unitOfWork.ShopRentalBalances.GetShopRentalBalanceByYearMonthForReversePayment(year, month, shopId);

                                                if (shRowLastYearMonthlyBalance != null)
                                                {
                                                    //fine--------
                                                    if (shRowLastYearMonthlyBalance.PaidFineAmount > 0)
                                                    {
                                                        if (RevePaidAmount_lastYear_fine < shRowLastYearMonthlyBalance.PaidFineAmount)
                                                        {
                                                            lypaidFineToBeUpdated = (decimal)shRowLastYearMonthlyBalance.PaidFineAmount - RevePaidAmount_lastYear_fine;

                                                            reduced_revLYFine = RevePaidAmount_lastYear_fine;
                                                        }
                                                        else
                                                        {
                                                            lypaidFineToBeUpdated = 0;

                                                            reduced_revLYFine = (decimal)shRowLastYearMonthlyBalance.PaidFineAmount;
                                                        }
                                                    }
                                                    //------------

                                                    //arreas
                                                    if (shRowLastYearMonthlyBalance.PaidArrearsAmount > 0)
                                                    {
                                                        if (RevPaidAmount_lastYear_arreas < shRowLastYearMonthlyBalance.PaidArrearsAmount)
                                                        {
                                                            lypaidArreasToBeUpdated = (decimal)shRowLastYearMonthlyBalance.PaidArrearsAmount - RevPaidAmount_lastYear_arreas;

                                                            reduced_revLYArreas = RevPaidAmount_lastYear_arreas;
                                                        }
                                                        else
                                                        {
                                                            lypaidArreasToBeUpdated = 0;

                                                            reduced_revLYArreas = (decimal)shRowLastYearMonthlyBalance.PaidArrearsAmount;
                                                        }
                                                    }
                                                    //------------

                                                    //serviceChargeArreas
                                                    if (shRowLastYearMonthlyBalance.PaidServiceChargeArreasAmount > 0)
                                                    {
                                                        if (RevPaidAmount_serviceChargeArreas < shRowLastYearMonthlyBalance.PaidServiceChargeArreasAmount)
                                                        {
                                                            lypaidServiceChargeArreasToBeUpdated = (decimal)shRowLastYearMonthlyBalance.PaidServiceChargeArreasAmount - RevPaidAmount_serviceChargeArreas;

                                                            reduced_revLYServiceCharge = RevPaidAmount_serviceChargeArreas;
                                                        }
                                                        else
                                                        {
                                                            lypaidServiceChargeArreasToBeUpdated = 0;

                                                            reduced_revLYServiceCharge = (decimal)shRowLastYearMonthlyBalance.PaidServiceChargeArreasAmount;

                                                        }
                                                    }
                                                    //------------


                                                    //Is completed
                                                    if (
                                                        (shRowLastYearMonthlyBalance.FineAmount != shRowLastYearMonthlyBalance.PaidFineAmount - RevePaidAmount_lastYear_fine) ||
                                                        (shRowLastYearMonthlyBalance.ArrearsAmount != shRowLastYearMonthlyBalance.PaidArrearsAmount - RevPaidAmount_lastYear_arreas) ||
                                                        (shRowLastYearMonthlyBalance.ServiceChargeArreasAmount != shRowLastYearMonthlyBalance.PaidServiceChargeArreasAmount - RevPaidAmount_serviceChargeArreas)
                                                    )
                                                    {
                                                        shRowLastYearMonthlyBalance.IsCompleted = false;
                                                    }

                                                    //-------updating table row
                                                    shRowLastYearMonthlyBalance.UpdatedBy = cashierId;
                                                    shRowLastYearMonthlyBalance.PaidFineAmount = lypaidFineToBeUpdated; ;
                                                    shRowLastYearMonthlyBalance.PaidArrearsAmount = lypaidArreasToBeUpdated; ;
                                                    shRowLastYearMonthlyBalance.PaidServiceChargeArreasAmount = lypaidServiceChargeArreasToBeUpdated; ;

                                                    //---
                                                    shRowLastYearMonthlyBalance.NoOfPayments = shRowLastYearMonthlyBalance.NoOfPayments - 1;
                                                    //---

                                                    //---
                                                    //shRowLastYearMonthlyBalance.HasTransaction = false;
                                                    //---

                                                    await _unitOfWork.CommitAsync();

                                                    //next iteration
                                                    if (RevPaidAmount_thisYear_fine != 0)
                                                    {
                                                        RevPaidAmount_thisYear_fine = RevPaidAmount_thisYear_fine - reduced_revLYFine;
                                                    }

                                                    if (RevPaidAmount_lastYear_arreas != 0)
                                                    {
                                                        RevPaidAmount_lastYear_arreas = RevPaidAmount_lastYear_arreas - reduced_revLYArreas;
                                                    }

                                                    if (RevPaidAmount_serviceChargeArreas != 0)
                                                    {
                                                        RevPaidAmount_serviceChargeArreas = RevPaidAmount_serviceChargeArreas - reduced_revLYServiceCharge;
                                                    }
                                                }
                                            }
                                        }

                                    }
                                }                                    
                            }
                        }
                        //-** TY-LY case *****
                    }
                    else
                    {
                        throw new ArgumentNullException("This is not a proceese bill or it has a draft bill");
                    }
                    //-** This month record *****
                }
                //----- [End - logic]--------
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //---
    }
}

