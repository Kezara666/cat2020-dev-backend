using CAT20.Core;
using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Models.Enums;
using CAT20.Core.Services.Mixin;
using CAT20.Core.Services.WaterBilling;
using DocumentFormat.OpenXml.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.Mixin
{
    public class WaterBillCancelOrderService : IWaterBillCancelOrderService
    {

        private readonly IMixinUnitOfWork _unitOfWork;
        private readonly IWaterConnectionBalanceHistoryService _balanceHistoryService;

        public WaterBillCancelOrderService(IMixinUnitOfWork unitOfWork, IWaterConnectionBalanceHistoryService balanceHistoryService)
        {
            _unitOfWork = unitOfWork;
            _balanceHistoryService = balanceHistoryService;
        }

        public async Task<bool> ReversePayment(int mixId, int wcPrimaryId, int approvedBy)
        {
            try
            {
                var mxs = await _unitOfWork.MixinOrders.GetForReversePaymentWaterBill(wcPrimaryId);
                var mx = await _unitOfWork.MixinOrders.GetByIdAsync(mixId);
                var wc = await _unitOfWork.WaterConnections.GetByIdAsync(wcPrimaryId);

                if ((mxs != null && mx != null && mx.Equals(mxs.First()) && wc!=null) || (mxs != null && mxs.First() != null && mxs.First().State == OrderStatus.Cancel_Approved && mxs.Last() != null && mxs.Last().Id == mixId)) {

                    decimal? amount = mx.TotalAmount;

                    if (amount > 0 && amount < wc.RunningOverPay)
                    {
                        amount -= wc.RunningOverPay;
                        amount = 0;

                        wc.RunningOverPay -= mx.TotalAmount;

                        var bals = await _unitOfWork.Balances.ReversePayments(wcPrimaryId, amount);
                        bals.First().Payments -= mx.TotalAmount;
                        bals.First().OverPay -= wc.RunningOverPay;
                    }
                    else
                    {
                        amount -= wc.RunningOverPay;
                       

                        var bals = await _unitOfWork.Balances.ReversePayments(wcPrimaryId, amount);

                        foreach (var b in bals)
                        {
                            if (amount > 0)
                            {
                                var flagAmount = amount;

                                if (amount < b.LatePaid)
                                {
                                    b.LatePaid -= amount;
                                    amount = 0;
                                    b.IsCompleted = false;

                                }
                                else if (amount >= b.LatePaid)
                                {
                                    amount -= b.LatePaid;
                                    b.LatePaid = 0;
                                    b.IsCompleted = false;

                                }


                                if (amount != 0 && amount < b.OnTimePaid)
                                {
                                    b.OnTimePaid -= amount;
                                    amount = 0;
                                    b.IsCompleted = false;

                                }
                                else if (amount != 0 && amount >= b.OnTimePaid)
                                {
                                    amount -= b.OnTimePaid;
                                    b.OnTimePaid = 0;
                                    b.IsCompleted = false;

                                }

                                if (flagAmount != amount)
                                {
                                    b.NoOfCancels++;
                                    b.UpdatedAt = DateTime.Now;
                                   
                                }

                            }
                            else
                            {
                                break;
                            }

                            if (b.Payments > 0)
                            {
                                b.Payments -= mx.TotalAmount;

                            }
                            if(b.OverPay > 0)
                            {
                                b.OverPay -= wc.RunningOverPay;
                            }
                        }
                        //bals.First().Payments -= mx.TotalAmount;
                        //bals.First().OverPay -= wc.RunningOverPay;
                        wc.RunningOverPay = 0;

                        if (!await _balanceHistoryService.CreateBalanceHistory(bals.ToList(), WbTransactionsType.ReversePayemet, approvedBy))
                        {
                            throw new Exception("Balance History Creation Failed");
                        }
                    }

                   

                }
                else
                {

                    throw new Exception("Unable to delete Water Bill Order.");

                }

                return true;
            }catch (Exception ex)
            {

                return false;
            }
        }
    }
}
