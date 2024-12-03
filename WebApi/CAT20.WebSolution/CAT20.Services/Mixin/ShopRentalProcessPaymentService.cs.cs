using CAT20.Core;
using CAT20.Core.Models.Mixin;
using CAT20.Core.Models.ShopRental;
using CAT20.Core.Services.Mixin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.Mixin
{
    public class ShopRentalProcessPaymentService : IShopRentalProcessPaymentService
    {
        private readonly IMixinUnitOfWork _unitOfWork;

        public ShopRentalProcessPaymentService(IMixinUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //----------------- [Start - shop rental process payment] -------------
        public async Task<bool> ProcessPayment(int mixinOrderId, int cashierId, int shopId)
        {


            //using (var dbtransaction = _unitOfWork.BeginTransaction())
            //{


                try
                {
                    //#0 get shop information
                    var mixinOrderLinesForShopRental = await _unitOfWork.MixinOrderLines.GetAllForOrderId(mixinOrderId);

                    decimal PayingAmount_lastYear_fine = 0;
                    decimal PayingAmount_lastYear_arreas = 0;
                    decimal PayingAmount_thisYear_fine = 0;
                    decimal PayingAmount_thisYear_arreas = 0;
                    decimal PayingAmount_rental = 0;
                    decimal PayingAmount_serviceChargeArreas = 0;
                    decimal PayingAmount_serviceCharge = 0;
                    decimal next_overpayment = 0;

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

                    foreach (var mixinOrderLineForShopRental in mixinOrderLinesForShopRental)
                    {
                        if (mixinOrderLineForShopRental.VotePaymentTypeId == 1)
                        {
                            PayingAmount_lastYear_fine = (decimal)mixinOrderLineForShopRental.Amount;
                        }
                        else if (mixinOrderLineForShopRental.VotePaymentTypeId == 2)
                        {
                            PayingAmount_lastYear_arreas = (decimal)mixinOrderLineForShopRental.Amount;
                        }
                        else if (mixinOrderLineForShopRental.VotePaymentTypeId == 3)
                        {
                            PayingAmount_thisYear_fine = (decimal)mixinOrderLineForShopRental.Amount;
                        }
                        else if (mixinOrderLineForShopRental.VotePaymentTypeId == 4)
                        {
                            PayingAmount_thisYear_arreas = (decimal)mixinOrderLineForShopRental.Amount;
                        }
                        else if (mixinOrderLineForShopRental.VotePaymentTypeId == 5)
                        {
                            PayingAmount_rental = (decimal)mixinOrderLineForShopRental.Amount;
                        }
                        else if (mixinOrderLineForShopRental.VotePaymentTypeId == 6)
                        {
                            PayingAmount_serviceChargeArreas = (decimal)mixinOrderLineForShopRental.Amount;
                        }
                        else if (mixinOrderLineForShopRental.VotePaymentTypeId == 7)
                        {
                            PayingAmount_serviceCharge = (decimal)mixinOrderLineForShopRental.Amount;
                        }
                        else
                        {
                            //mixinOrderLineForShopRental.VotePaymentTypeId == 8
                            next_overpayment = (decimal)mixinOrderLineForShopRental.Amount;
                        }
                    }

                    if (!await ShopRentalBalanceUpdate(shopId, PayingAmount_lastYear_fine, PayingAmount_lastYear_arreas, PayingAmount_thisYear_fine, PayingAmount_thisYear_arreas, PayingAmount_rental, PayingAmount_serviceChargeArreas, PayingAmount_serviceCharge, next_overpayment, cashierId, mixinOrderId))
                    {
                        throw new Exception("Error occured while updating shop rental balances table");

                    }
                    else
                    {
                        //If successful then disable all locks
                        var shBalannces = await _unitOfWork.ShopRentalBalances.GetAllBalanceByShopId(shopId);

                        foreach (var sh in shBalannces)
                        {
                            sh.HasTransaction = false; //disable lock
                                                       //sh.IsCompleted = true; //set all to is processd
                        }
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                  // dbtransaction.Rollback();
                }

            //}
            
        }

        private async Task<bool> ShopRentalBalanceUpdate(int shopId, decimal PayingAmount_lastYear_fine, decimal PayingAmount_lastYear_arreas, decimal PayingAmount_thisYear_fine, decimal PayingAmount_thisYear_arreas, decimal PayingAmount_rental, decimal PayingAmount_serviceChargeArreas, decimal PayingAmount_serviceCharge, decimal next_overpayment, int cashierId, int mixinOrderId)
        {
            try
            {
                var lastBalanceRecord = await _unitOfWork.ShopRentalBalances.GetLastBalanceInfo(shopId);

                if (lastBalanceRecord != null)
                {
                    if(lastBalanceRecord.Shop.OpeningBalance.ApproveStatus == 1)
                    {
                        var shopAllBalanceNotCompletedInfo = await _unitOfWork.ShopRentalBalances.GetAllNotCompletedBalanceInformationOfShop(shopId);

                        if (shopAllBalanceNotCompletedInfo != null && shopAllBalanceNotCompletedInfo.Count() != 0)
                        {
                            if (!await UpdateCurrentMonthReportRecordFields(shopId, PayingAmount_lastYear_fine, PayingAmount_lastYear_arreas, PayingAmount_thisYear_fine, PayingAmount_thisYear_arreas, PayingAmount_rental, PayingAmount_serviceChargeArreas, PayingAmount_serviceCharge, next_overpayment, cashierId, mixinOrderId))
                            {
                                throw new Exception("unable to update current month report fields");
                            }                           //ly
                            decimal lypaidFineToBeUpdated = 0;
                            decimal lypaidArreasToBeUpdated = 0;
                            decimal lypaidServiceChargeArreasToBeUpdated = 0;

                            //ty
                            decimal typaidFineToBeUpdated = 0;
                            decimal typaidArreasToBeUpdated = 0;
                            decimal typaidServiceChargeArreasToBeUpdated = 0;

                            //previous balance settlement + this month payment
                            var lyears = shopAllBalanceNotCompletedInfo.Where(y => y.Year < lastBalanceRecord.Year).Select(y => y.Year).Distinct().ToList();

                            if (lyears.Count() != 0)
                            {
                                //LY case
                                foreach (var year in lyears)
                                {
                                    for (int month = 1; month <= 12; month++)
                                    {
                                        decimal reduced_LYFine = 0;
                                        decimal reduced_LYArreas = 0;
                                        decimal reduced_LYServiceCharge = 0;

                                        var shRowLastYearMonthlyBalance = await _unitOfWork.ShopRentalBalances.GetShopRentalNotCompletedBalanceByYearMonth(year, month, shopId);

                                        if (shRowLastYearMonthlyBalance != null)
                                        {
                                            //fine--------
                                            if ((shRowLastYearMonthlyBalance.FineAmount - shRowLastYearMonthlyBalance.PaidFineAmount) != 0)
                                            {
                                                if (PayingAmount_lastYear_fine < (shRowLastYearMonthlyBalance.FineAmount - shRowLastYearMonthlyBalance.PaidFineAmount))
                                                {
                                                    lypaidFineToBeUpdated = (decimal)shRowLastYearMonthlyBalance.PaidFineAmount + PayingAmount_lastYear_fine;

                                                    reduced_LYFine = PayingAmount_lastYear_fine;
                                                }
                                                else if (PayingAmount_lastYear_fine == (shRowLastYearMonthlyBalance.FineAmount - shRowLastYearMonthlyBalance.PaidFineAmount))
                                                {
                                                    lypaidFineToBeUpdated = (decimal)shRowLastYearMonthlyBalance.PaidFineAmount + PayingAmount_lastYear_fine;

                                                    reduced_LYFine = PayingAmount_lastYear_fine;
                                                }
                                                else
                                                {
                                                    lypaidFineToBeUpdated = (decimal)shRowLastYearMonthlyBalance.FineAmount;

                                                    reduced_LYFine = (decimal)((shRowLastYearMonthlyBalance.FineAmount - shRowLastYearMonthlyBalance.PaidFineAmount));
                                                }
                                            }
                                            else
                                            {
                                                lypaidFineToBeUpdated = (decimal)shRowLastYearMonthlyBalance.PaidFineAmount;
                                            }
                                            //------------

                                            //arreas
                                            if ((shRowLastYearMonthlyBalance.ArrearsAmount - shRowLastYearMonthlyBalance.PaidArrearsAmount) != 0)
                                            {
                                                if (PayingAmount_lastYear_arreas < (shRowLastYearMonthlyBalance.ArrearsAmount - shRowLastYearMonthlyBalance.PaidArrearsAmount))
                                                {
                                                    lypaidArreasToBeUpdated = (decimal)shRowLastYearMonthlyBalance.PaidArrearsAmount + PayingAmount_lastYear_arreas;

                                                    reduced_LYArreas = PayingAmount_lastYear_arreas;
                                                }
                                                else if (PayingAmount_lastYear_arreas == (shRowLastYearMonthlyBalance.ArrearsAmount - shRowLastYearMonthlyBalance.PaidArrearsAmount))
                                                {
                                                    lypaidArreasToBeUpdated = (decimal)shRowLastYearMonthlyBalance.PaidArrearsAmount + PayingAmount_lastYear_arreas;

                                                    reduced_LYArreas = PayingAmount_lastYear_arreas;
                                                }
                                                else
                                                {
                                                    lypaidArreasToBeUpdated = (decimal)shRowLastYearMonthlyBalance.ArrearsAmount;

                                                    reduced_LYArreas = (decimal)(shRowLastYearMonthlyBalance.ArrearsAmount - shRowLastYearMonthlyBalance.PaidArrearsAmount);
                                                }
                                            }
                                            else
                                            {
                                                lypaidArreasToBeUpdated = (decimal)shRowLastYearMonthlyBalance.PaidArrearsAmount;
                                            }
                                            //------------


                                            //serviceChargeArreas
                                            if ((shRowLastYearMonthlyBalance.ServiceChargeArreasAmount - shRowLastYearMonthlyBalance.PaidServiceChargeArreasAmount) != 0)
                                            {
                                                if (PayingAmount_serviceChargeArreas < (shRowLastYearMonthlyBalance.ServiceChargeArreasAmount - shRowLastYearMonthlyBalance.PaidServiceChargeArreasAmount))
                                                {
                                                    lypaidServiceChargeArreasToBeUpdated = (decimal)shRowLastYearMonthlyBalance.PaidServiceChargeArreasAmount + PayingAmount_serviceChargeArreas;

                                                    reduced_LYServiceCharge = PayingAmount_serviceChargeArreas;
                                                }
                                                else if (PayingAmount_serviceChargeArreas < (shRowLastYearMonthlyBalance.ServiceChargeArreasAmount - shRowLastYearMonthlyBalance.PaidServiceChargeArreasAmount))
                                                {
                                                    lypaidServiceChargeArreasToBeUpdated = (decimal)shRowLastYearMonthlyBalance.PaidServiceChargeArreasAmount + PayingAmount_serviceChargeArreas;

                                                    reduced_LYServiceCharge = PayingAmount_serviceChargeArreas;
                                                }
                                                else
                                                {
                                                    lypaidServiceChargeArreasToBeUpdated = (decimal)shRowLastYearMonthlyBalance.ServiceChargeArreasAmount;

                                                    reduced_LYServiceCharge = (decimal)(shRowLastYearMonthlyBalance.ServiceChargeArreasAmount - shRowLastYearMonthlyBalance.PaidServiceChargeArreasAmount);
                                                }
                                            }
                                            else
                                            {
                                                lypaidServiceChargeArreasToBeUpdated = (decimal)shRowLastYearMonthlyBalance.PaidServiceChargeArreasAmount;
                                            }
                                            //------------

                                            //Is completed
                                            if (
                                                PayingAmount_lastYear_fine >= (shRowLastYearMonthlyBalance.FineAmount - shRowLastYearMonthlyBalance.PaidFineAmount) &&
                                                PayingAmount_lastYear_arreas >= (shRowLastYearMonthlyBalance.ArrearsAmount - shRowLastYearMonthlyBalance.PaidArrearsAmount) &&
                                                (PayingAmount_serviceChargeArreas >= (shRowLastYearMonthlyBalance.ServiceChargeArreasAmount - shRowLastYearMonthlyBalance.PaidServiceChargeArreasAmount) ||
                                                (shRowLastYearMonthlyBalance.ServiceChargeArreasAmount >= shRowLastYearMonthlyBalance.PaidServiceChargeArreasAmount))
                                            )
                                            {
                                                shRowLastYearMonthlyBalance.IsCompleted = true;
                                                shRowLastYearMonthlyBalance.SettledMixinOrderId = mixinOrderId;
                                            }

                                            //-------updating table row
                                            shRowLastYearMonthlyBalance.UpdatedBy = cashierId;
                                            shRowLastYearMonthlyBalance.UpdatedAt = DateTime.Now;
                                            shRowLastYearMonthlyBalance.BillProcessDate = DateOnly.FromDateTime(DateTime.Now);
                                            shRowLastYearMonthlyBalance.PaidFineAmount = lypaidFineToBeUpdated;
                                            shRowLastYearMonthlyBalance.PaidArrearsAmount = lypaidArreasToBeUpdated;
                                            shRowLastYearMonthlyBalance.PaidServiceChargeArreasAmount = lypaidServiceChargeArreasToBeUpdated;


                                            //---
                                            if (shRowLastYearMonthlyBalance.IsProcessed == false)
                                            {
                                                shRowLastYearMonthlyBalance.IsProcessed = true;
                                                shRowLastYearMonthlyBalance.Shop.OpeningBalance.IsProcessed = true;

                                            }
                                            shRowLastYearMonthlyBalance.NoOfPayments = shRowLastYearMonthlyBalance.NoOfPayments + 1;
                                            //---

                                            //---
                                            //shRowLastYearMonthlyBalance.HasTransaction = false;
                                            //---

                                            await _unitOfWork.CommitAsync();

                                            //next iteration
                                            if (PayingAmount_lastYear_fine != 0)
                                            {
                                                PayingAmount_lastYear_fine = PayingAmount_lastYear_fine - reduced_LYFine;
                                            }

                                            if (PayingAmount_lastYear_arreas != 0)
                                            {
                                                PayingAmount_lastYear_arreas = PayingAmount_lastYear_arreas - reduced_LYArreas;
                                            }

                                            if (PayingAmount_serviceChargeArreas != 0)
                                            {
                                                PayingAmount_serviceChargeArreas = PayingAmount_serviceChargeArreas - reduced_LYServiceCharge;
                                            }
                                        }
                                    }
                                }
                            }


                            //TY Case
                            for (int month = 1; month < lastBalanceRecord.Month; month++)
                            {
                                decimal reduced_TYFine = 0;
                                decimal reduced_TYArreas = 0;
                                decimal reduced_TYServiceCharge = 0;

                                var shRowThisYearMonthlyBalance = await _unitOfWork.ShopRentalBalances.GetShopRentalNotCompletedBalanceByYearMonth(lastBalanceRecord.Year, month, shopId);

                                if (shRowThisYearMonthlyBalance != null)
                                {
                                    //fine--------
                                    if ((shRowThisYearMonthlyBalance.FineAmount - shRowThisYearMonthlyBalance.PaidFineAmount) != 0)
                                    {
                                       
                                        if (PayingAmount_thisYear_fine < (shRowThisYearMonthlyBalance.FineAmount - shRowThisYearMonthlyBalance.PaidFineAmount))
                                        {
                                            typaidFineToBeUpdated = (decimal)shRowThisYearMonthlyBalance.PaidFineAmount + PayingAmount_thisYear_fine;

                                            reduced_TYFine = PayingAmount_thisYear_fine;
                                        }
                                        else if (PayingAmount_thisYear_fine == (shRowThisYearMonthlyBalance.FineAmount - shRowThisYearMonthlyBalance.PaidFineAmount))
                                        {
                                            typaidFineToBeUpdated = (decimal)shRowThisYearMonthlyBalance.PaidFineAmount + PayingAmount_thisYear_fine;

                                            reduced_TYFine = PayingAmount_thisYear_fine;
                                        }
                                        else
                                        {
                                            typaidFineToBeUpdated = (decimal)shRowThisYearMonthlyBalance.FineAmount;

                                            reduced_TYFine = (decimal)(shRowThisYearMonthlyBalance.FineAmount - shRowThisYearMonthlyBalance.PaidFineAmount);
                                        }
                                    }
                                    else
                                    {
                                        typaidFineToBeUpdated = (decimal)shRowThisYearMonthlyBalance.PaidFineAmount;
                                    }
                                    //------------

                                    //arreas
                                    if ((shRowThisYearMonthlyBalance.ArrearsAmount - shRowThisYearMonthlyBalance.PaidArrearsAmount) != 0)
                                    {
                                        if (PayingAmount_thisYear_arreas < (shRowThisYearMonthlyBalance.ArrearsAmount - shRowThisYearMonthlyBalance.PaidArrearsAmount))
                                        {
                                            typaidArreasToBeUpdated = (decimal)shRowThisYearMonthlyBalance.PaidArrearsAmount + PayingAmount_thisYear_arreas;

                                            reduced_TYArreas = PayingAmount_thisYear_arreas;
                                        }
                                        else if (PayingAmount_thisYear_arreas == (shRowThisYearMonthlyBalance.ArrearsAmount - shRowThisYearMonthlyBalance.PaidArrearsAmount))
                                        {
                                            typaidArreasToBeUpdated = (decimal)shRowThisYearMonthlyBalance.PaidArrearsAmount + PayingAmount_thisYear_arreas;

                                            reduced_TYArreas = PayingAmount_thisYear_arreas;
                                        }
                                        else
                                        {
                                            typaidArreasToBeUpdated = (decimal)shRowThisYearMonthlyBalance.ArrearsAmount;

                                            reduced_TYArreas = (decimal)(shRowThisYearMonthlyBalance.ArrearsAmount - shRowThisYearMonthlyBalance.PaidArrearsAmount);
                                        }
                                    }
                                    else
                                    {
                                        typaidArreasToBeUpdated = (decimal)shRowThisYearMonthlyBalance.PaidArrearsAmount;
                                    }
                                    //------------

                                    //serviceChargeArreas
                                    if ((shRowThisYearMonthlyBalance.ServiceChargeArreasAmount - shRowThisYearMonthlyBalance.PaidServiceChargeArreasAmount) != 0)
                                    {
                                        
                                        if (PayingAmount_serviceChargeArreas < (shRowThisYearMonthlyBalance.ServiceChargeArreasAmount - shRowThisYearMonthlyBalance.PaidServiceChargeArreasAmount))
                                        {
                                            typaidServiceChargeArreasToBeUpdated = (decimal)shRowThisYearMonthlyBalance.PaidServiceChargeArreasAmount + PayingAmount_serviceChargeArreas;

                                            reduced_TYServiceCharge = PayingAmount_serviceChargeArreas;
                                        }
                                        else if (PayingAmount_serviceChargeArreas == (shRowThisYearMonthlyBalance.ServiceChargeArreasAmount - shRowThisYearMonthlyBalance.PaidServiceChargeArreasAmount))
                                        {
                                            typaidServiceChargeArreasToBeUpdated = (decimal)shRowThisYearMonthlyBalance.PaidServiceChargeArreasAmount + PayingAmount_serviceChargeArreas;

                                            reduced_TYServiceCharge = PayingAmount_serviceChargeArreas;
                                        }
                                        else
                                        {
                                            typaidServiceChargeArreasToBeUpdated = (decimal)shRowThisYearMonthlyBalance.ServiceChargeArreasAmount;

                                            reduced_TYServiceCharge = (decimal)(shRowThisYearMonthlyBalance.ServiceChargeArreasAmount - shRowThisYearMonthlyBalance.PaidServiceChargeArreasAmount);
                                        }
                                    }
                                    else
                                    {
                                        typaidServiceChargeArreasToBeUpdated = (decimal)shRowThisYearMonthlyBalance.PaidServiceChargeArreasAmount;
                                    }
                                    //------------


                                    //Is completed
                                    if (
                                        PayingAmount_thisYear_fine >= (shRowThisYearMonthlyBalance.FineAmount - shRowThisYearMonthlyBalance.PaidFineAmount) &&
                                        PayingAmount_thisYear_arreas >= (shRowThisYearMonthlyBalance.ArrearsAmount - shRowThisYearMonthlyBalance.PaidArrearsAmount) &&
                                        PayingAmount_serviceChargeArreas >= (shRowThisYearMonthlyBalance.ServiceChargeArreasAmount - shRowThisYearMonthlyBalance.PaidServiceChargeArreasAmount) //&&
                                                                                                                                                                                                 //PayingAmount_serviceCharge == (shRowThisYearMonthlyBalance.CurrentServiceChargeAmount - shRowThisYearMonthlyBalance.PaidCurrentServiceChargeAmount)
                                    )
                                    {
                                        shRowThisYearMonthlyBalance.IsCompleted = true;
                                        shRowThisYearMonthlyBalance.SettledMixinOrderId = mixinOrderId;
                                    }

                                    //-------updating table row
                                    shRowThisYearMonthlyBalance.UpdatedBy = cashierId;
                                    shRowThisYearMonthlyBalance.UpdatedAt = DateTime.Now;
                                    shRowThisYearMonthlyBalance.BillProcessDate = DateOnly.FromDateTime(DateTime.Now);
                                    shRowThisYearMonthlyBalance.PaidFineAmount = typaidFineToBeUpdated;
                                    shRowThisYearMonthlyBalance.PaidArrearsAmount = typaidArreasToBeUpdated;
                                    shRowThisYearMonthlyBalance.PaidServiceChargeArreasAmount = typaidServiceChargeArreasToBeUpdated;

                                    //---
                                    if (shRowThisYearMonthlyBalance.IsProcessed == false)
                                    {
                                        shRowThisYearMonthlyBalance.IsProcessed = true;
                                        shRowThisYearMonthlyBalance.Shop.OpeningBalance.IsProcessed = true;

                                    }
                                    shRowThisYearMonthlyBalance.NoOfPayments = shRowThisYearMonthlyBalance.NoOfPayments + 1;
                                    //---

                                    //---
                                    //shRowThisYearMonthlyBalance.HasTransaction = false;
                                    //---

                                    await _unitOfWork.CommitAsync();

                                    //next iteration
                                    if (PayingAmount_thisYear_fine != 0)
                                    {
                                        PayingAmount_thisYear_fine = PayingAmount_thisYear_fine - reduced_TYFine;
                                    }

                                    if (PayingAmount_thisYear_arreas != 0)
                                    {
                                        PayingAmount_thisYear_arreas = PayingAmount_thisYear_arreas - reduced_TYArreas;
                                    }

                                    if (PayingAmount_serviceChargeArreas != 0)
                                    {
                                        PayingAmount_serviceChargeArreas = PayingAmount_serviceChargeArreas - reduced_TYServiceCharge;
                                    }
                                }
                            }
                        }
                        else
                        {
                            //this month payment only
                            if (!await UpdateCurrentMonthReportRecordFields(shopId, PayingAmount_lastYear_fine, PayingAmount_lastYear_arreas, PayingAmount_thisYear_fine, PayingAmount_thisYear_arreas, PayingAmount_rental, PayingAmount_serviceChargeArreas, PayingAmount_serviceCharge, next_overpayment, cashierId, mixinOrderId))
                            {
                                throw new Exception("unable to update current month report fields");
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("Opening balance should be approved to place an order");
                    }
                }
                else
                {
                    throw new Exception("Last balance record cannot be null");
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //------


        //------
        private async Task<bool> UpdateCurrentMonthReportRecordFields(int shopId, decimal PayingAmount_lastYear_fine, decimal PayingAmount_lastYear_arreas, decimal PayingAmount_thisYear_fine, decimal PayingAmount_thisYear_arreas, decimal PayingAmount_rental, decimal PayingAmount_serviceChargeArreas, decimal PayingAmount_serviceCharge, decimal next_overpayment, int cashierId, int mixinOrderId)
        {
            try
            {
                var lastBalanceRecord = await _unitOfWork.ShopRentalBalances.GetLastBalanceInfo(shopId);

                if (lastBalanceRecord != null)
                {

                    if (lastBalanceRecord.IsCompleted == false)
                    {
                        //Is completed
                        if (
                            //(lastBalanceRecord.LYFine == lastBalanceRecord.PaidLYFine + PayingAmount_lastYear_fine) &&
                            //(lastBalanceRecord.LYArreas == lastBalanceRecord.PaidLYArreas + PayingAmount_lastYear_arreas) &&
                            //(lastBalanceRecord.TYFine == lastBalanceRecord.PaidTYFine + PayingAmount_thisYear_fine) &&
                            //(lastBalanceRecord.TYArreas == lastBalanceRecord.PaidTYArreas + PayingAmount_thisYear_arreas) &&
                            (lastBalanceRecord.TYLYServiceChargeArreas == lastBalanceRecord.PaidTYLYServiceChargeArreas + PayingAmount_serviceChargeArreas) &&
                            (lastBalanceRecord.Shop.Rental == lastBalanceRecord.PaidCurrentRentalAmount + PayingAmount_rental) &&
                            (lastBalanceRecord.Shop.ServiceCharge == lastBalanceRecord.PaidCurrentServiceChargeAmount + PayingAmount_serviceCharge)
                        )
                        {
                            lastBalanceRecord.IsCompleted = true;
                            lastBalanceRecord.SettledMixinOrderId = mixinOrderId;
                        }

                        if (lastBalanceRecord.IsProcessed == false)
                        {
                            lastBalanceRecord.IsProcessed = true;
                            lastBalanceRecord.Shop.OpeningBalance.IsProcessed = true;
                        }

                        //---- [Start - report field] ---
                        lastBalanceRecord.PaidLYFine += PayingAmount_lastYear_fine;
                        lastBalanceRecord.PaidLYArreas += PayingAmount_lastYear_arreas;

                        //------This month fine process case
                        decimal remaining_after_prev_months_fine = 0;
                        if (lastBalanceRecord.TYFine < (PayingAmount_thisYear_fine + lastBalanceRecord.PaidTYFine))
                        {
                            remaining_after_prev_months_fine = (decimal)(PayingAmount_thisYear_fine + lastBalanceRecord.PaidTYFine) - (decimal)lastBalanceRecord.TYFine;

                            lastBalanceRecord.PaidTYFine     = lastBalanceRecord.TYFine;

                            var processConfigAssignmentInfo = await _unitOfWork.ProcessConfigurationSettingAssign.GetByShopId(shopId);
                            if (processConfigAssignmentInfo.ShopRentalProcessConfigaration.RentalPaymentDateTypeId != 1)
                            {
                                lastBalanceRecord.PaidFineAmount += remaining_after_prev_months_fine;
                            }

                            lastBalanceRecord.PaidCurrentMonthNewFine += remaining_after_prev_months_fine; //Report field
                        }
                        else
                        {
                            lastBalanceRecord.PaidTYFine += PayingAmount_thisYear_fine;
                        }
                        //-------

                        lastBalanceRecord.PaidTYArreas += PayingAmount_thisYear_arreas;
                        lastBalanceRecord.PaidCurrentRentalAmount += PayingAmount_rental;
                        lastBalanceRecord.PaidTYLYServiceChargeArreas += PayingAmount_serviceChargeArreas;
                        lastBalanceRecord.PaidCurrentServiceChargeAmount += PayingAmount_serviceCharge;
                        //---- [Start - report field] ---
                        lastBalanceRecord.OverPaymentAmount += next_overpayment;

                        lastBalanceRecord.NoOfPayments += 1;

                        lastBalanceRecord.UpdatedBy = cashierId;
                        lastBalanceRecord.UpdatedAt = DateTime.Now;
                        lastBalanceRecord.BillProcessDate = DateOnly.FromDateTime(DateTime.Now);

                        //lastBalanceRecord.HasTransaction = false;
                    }
                    else
                    {
                        //add more over payments

                        lastBalanceRecord.OverPaymentAmount += next_overpayment;

                        lastBalanceRecord.NoOfPayments += 1;
                        lastBalanceRecord.UpdatedBy = cashierId;
                        lastBalanceRecord.UpdatedAt = DateTime.Now;
                        lastBalanceRecord.BillProcessDate = DateOnly.FromDateTime(DateTime.Now);

                        //lastBalanceRecord.HasTransaction = false;

                        if (lastBalanceRecord.IsProcessed == false)
                        {
                            lastBalanceRecord.IsProcessed = true;
                            lastBalanceRecord.Shop.OpeningBalance.IsProcessed = true;

                        }
                    }

                    await _unitOfWork.CommitAsync();
                }
                else
                {
                    throw new Exception("Last balance record cannot be null");
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //----------------- [End - shop rental process payment] ---------------
    }
}
