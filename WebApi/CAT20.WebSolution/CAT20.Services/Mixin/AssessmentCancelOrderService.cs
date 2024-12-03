using CAT20.Core;
using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Models.AssessmentTax.Quarter;
using CAT20.Core.Models.Enums;
using CAT20.Core.Services.Mixin;
using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.Mixin
{
    public class AssessmentCancelOrderService: IAssessmentCancelOrderService
    {

        private readonly IMixinUnitOfWork _unitOfWork;
        public AssessmentCancelOrderService(IMixinUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> ReversePayment(int mixId, int assessmentId)
        {
            try
            {

                var mxs = await _unitOfWork.MixinOrders.GetForReversePayment(assessmentId);

                if (mxs != null && mxs.First() != null && mxs.First().Id == mixId)
                {

                    if (mxs != null && mxs.First().AssessmentId.HasValue && 1<= mxs.First().MixinOrderLine.Count() )
                    {
                        var trns = await _unitOfWork.AssessmentTransactions.GetLastTransaction(mxs.First().AssessmentId!.Value,2);

                        var assessmentBalance = await _unitOfWork.AssessmentBalances.GetByIdToProcessPayment(mxs.First().AssessmentId!.Value);
                        int? month = await _unitOfWork.Sessions.GetCurrentSessionMonthForProcess(mxs.First().SessionId);

                        if (trns.Count() == 2 && trns.First().Type == Core.Models.Enums.AssessmentTransactionsType.Payment && assessmentBalance != null && month.HasValue && assessmentBalance.Q1 != null && assessmentBalance.Q2 != null && assessmentBalance.Q3 != null && assessmentBalance.Q4 != null)
                        {
                            //check month

                            var q1 = (!assessmentBalance.Q1.IsCompleted && !assessmentBalance.Q1.IsOver) ? assessmentBalance.Q1.Amount - assessmentBalance.Q1.ByExcessDeduction - assessmentBalance.Q1.Paid - assessmentBalance.Q1.Discount : 0;
                            var q2 = (!assessmentBalance.Q2.IsCompleted && !assessmentBalance.Q2.IsOver) ? assessmentBalance.Q2.Amount - assessmentBalance.Q2.ByExcessDeduction - assessmentBalance.Q2.Paid - assessmentBalance.Q2.Discount : 0;
                            var q3 = (!assessmentBalance.Q3.IsCompleted && !assessmentBalance.Q3.IsOver) ? assessmentBalance.Q3.Amount - assessmentBalance.Q3.ByExcessDeduction - assessmentBalance.Q3.Paid - assessmentBalance.Q3.Discount : 0;
                            var q4 = (!assessmentBalance.Q4.IsCompleted && !assessmentBalance.Q4.IsOver) ? assessmentBalance.Q4.Amount - assessmentBalance.Q4.ByExcessDeduction - assessmentBalance.Q4.Paid - assessmentBalance.Q4.Discount : 0;

                            if (assessmentBalance.NumberOfPayments - assessmentBalance.NumberOfCancels==1  && month == 1)
                            {
                                assessmentBalance.ExcessPayment = trns.Last().RunningOverPay;
                                assessmentBalance.ByExcessDeduction = 0;

                                assessmentBalance.Paid = 0;
                                assessmentBalance.Discount = 0;
                                assessmentBalance.DiscountRate = 0;
                                assessmentBalance.OverPayment = 0;
                                assessmentBalance.IsCompleted = false;

                                assessmentBalance.LYArrears = trns.Last().LYArrears;
                                assessmentBalance.LYWarrant = trns.Last().LYWarrant;


                                assessmentBalance.Q1.Paid = 0;
                                assessmentBalance.Q1.ByExcessDeduction = 0;
                                assessmentBalance.Q1.Discount = 0;
                                assessmentBalance.Q1.IsCompleted = false;

                                assessmentBalance.Q2.Paid = 0;
                                assessmentBalance.Q2.ByExcessDeduction = 0;
                                assessmentBalance.Q2.Discount = 0;
                                assessmentBalance.Q2.IsCompleted = false;

                                assessmentBalance.Q3.Paid = 0;
                                assessmentBalance.Q3.ByExcessDeduction = 0;
                                assessmentBalance.Q3.Discount = 0;
                                assessmentBalance.Q3.IsCompleted = false;

                                assessmentBalance.Q4.Paid = 0;
                                assessmentBalance.Q4.ByExcessDeduction = 0;
                                assessmentBalance.Q4.Discount = 0;
                                assessmentBalance.Q4.IsCompleted = false;




                            }
                            else
                            {
                                

                                assessmentBalance.Paid -= mxs.First().TotalAmount;
                                assessmentBalance.Discount -= mxs.First().DiscountAmount;

                                assessmentBalance.DiscountRate = trns.Last().DiscountRate;

                                assessmentBalance.OverPayment = trns.Last().RunningOverPay;
                              
                                assessmentBalance.LYArrears = trns.Last().LYArrears;
                                assessmentBalance.LYWarrant = trns.Last().LYWarrant;

                                assessmentBalance.TYArrears = trns.Last().TYArrears;
                                assessmentBalance.TYWarrant = trns.Last().TYWarrant;


                                if (assessmentBalance.AnnualAmount + assessmentBalance.LYArrears + assessmentBalance.LYWarrant + assessmentBalance.TYWarrant + assessmentBalance.TYArrears - (assessmentBalance.ByExcessDeduction + assessmentBalance.Paid + assessmentBalance.Discount) > 0)
                                {
                                    assessmentBalance.IsCompleted = false;
                                }


                                if (!assessmentBalance.Q1.IsOver && q1 != trns.Last().Q1)
                                {
                                    assessmentBalance.Q1.Paid = assessmentBalance.Q1.Amount - (trns.Last().Q1 + assessmentBalance.Q1.ByExcessDeduction);
                                    //assessmentBalance.Q1.ByExcessDeduction = 0;
                                    assessmentBalance.Q1.Discount = 0;
                                    assessmentBalance.Q1.IsCompleted = false;
                                }

                                if (!assessmentBalance.Q2.IsOver && q2 != trns.Last().Q2)
                                {

                                    assessmentBalance.Q2.Paid = assessmentBalance.Q2.Amount - (trns.Last().Q2 + assessmentBalance.Q2.ByExcessDeduction);
                                    //assessmentBalance.Q2.ByExcessDeduction = 0;
                                    assessmentBalance.Q2.Discount = 0;
                                    assessmentBalance.Q2.IsCompleted = false;
                                }

                                if (!assessmentBalance.Q3.IsOver && q3 != trns.Last().Q3)
                                {
                                    assessmentBalance.Q3.Paid = assessmentBalance.Q3.Amount - (trns.Last().Q3 + assessmentBalance.Q3.ByExcessDeduction);
                                    //assessmentBalance.Q3.ByExcessDeduction = 0;
                                    assessmentBalance.Q3.Discount = 0;
                                    assessmentBalance.Q3.IsCompleted = false;

                                }

                                if (!assessmentBalance.Q4.IsOver && q4 != trns.Last().Q4)
                                {
                                    assessmentBalance.Q4.Paid = assessmentBalance.Q4.Amount - (trns.Last().Q4 + assessmentBalance.Q4.ByExcessDeduction);
                                    //assessmentBalance.Q4.ByExcessDeduction = 0;
                                    assessmentBalance.Q4.Discount = 0;
                                    assessmentBalance.Q4.IsCompleted = false;

                                }
                            }
                            assessmentBalance.NumberOfCancels += 1;
                            if (assessmentBalance.Q1.IsCompleted && assessmentBalance.Q2.IsCompleted && assessmentBalance.Q3.IsCompleted && assessmentBalance.Q4.IsCompleted)
                            {
                                assessmentBalance.IsCompleted = true;
                            }
                            else
                            {
                                assessmentBalance.IsCompleted = false;
                            }

                            assessmentBalance.HasTransaction = false;
                            createTransaction(assessmentBalance, AssessmentTransactionsType.ReversePayemet);
                            /*

                            var nq1 = (!assessmentBalance.Q1.IsCompleted && !assessmentBalance.Q1.IsOver) ? assessmentBalance.Q1.Amount - assessmentBalance.Q1.ByExcessDeduction - assessmentBalance.Q1.Paid - assessmentBalance.Q1.Discount : 0;
                            var nq2 = (!assessmentBalance.Q2.IsCompleted && !assessmentBalance.Q2.IsOver) ? assessmentBalance.Q2.Amount - assessmentBalance.Q2.ByExcessDeduction - assessmentBalance.Q2.Paid - assessmentBalance.Q2.Discount : 0;
                            var nq3 = (!assessmentBalance.Q3.IsCompleted && !assessmentBalance.Q3.IsOver) ? assessmentBalance.Q3.Amount - assessmentBalance.Q3.ByExcessDeduction - assessmentBalance.Q3.Paid - assessmentBalance.Q3.Discount : 0;
                            var nq4 = (!assessmentBalance.Q4.IsCompleted && !assessmentBalance.Q4.IsOver) ? assessmentBalance.Q4.Amount - assessmentBalance.Q4.ByExcessDeduction - assessmentBalance.Q4.Paid - assessmentBalance.Q4.Discount : 0;
                            var transaction = new AssessmentTransaction
                            {
                                AssessmentId = assessmentBalance.AssessmentId,
                                //DateTime = DateTime.Now,
                                Type = AssessmentTransactionsType.ReversePayemet,
                                LYArrears = assessmentBalance.LYArrears,
                                LYWarrant = assessmentBalance.LYWarrant,
                                TYArrears = assessmentBalance.TYArrears,
                                TYWarrant = assessmentBalance.TYWarrant,
                                RunningOverPay = assessmentBalance.OverPayment,
                                Q1 = nq1,
                                Q2 = nq2,
                                Q3 = nq3,
                                Q4 = nq4,

                                RunningDiscount = assessmentBalance.Discount,
                                RunningTotal =
                                assessmentBalance.LYArrears
                                + assessmentBalance.LYWarrant
                                + assessmentBalance.TYArrears
                                + assessmentBalance.TYWarrant
                                      + nq1
                                      + nq2
                                      + nq3
                                      + nq4
                                - assessmentBalance.OverPayment,
                            };

                            await _unitOfWork.AssessmentTransactions.AddAsync(transaction);
                            */


                        }

                    }

                   



                }
                else if(mxs != null && mxs.First() != null && mxs.First().State == OrderStatus.Cancel_Approved && mxs.Last() != null && mxs.Last().Id == mixId)
                {

                    var trns = await _unitOfWork.AssessmentTransactions.GetLastTransaction(mxs.First().AssessmentId!.Value, 4);

                    var assessmentBalance = await _unitOfWork.AssessmentBalances.GetByIdToProcessPayment(mxs.First().AssessmentId!.Value);
                    int? month = await _unitOfWork.Sessions.GetCurrentSessionMonthForProcess(mxs.First().SessionId);

                    if (trns.Count() == 4 && trns.First().Type == Core.Models.Enums.AssessmentTransactionsType.ReversePayemet && assessmentBalance != null && month.HasValue && assessmentBalance.Q1 != null && assessmentBalance.Q2 != null && assessmentBalance.Q3 != null && assessmentBalance.Q4 != null)
                    {
                        //check month

                        var q1 = (!assessmentBalance.Q1.IsCompleted && !assessmentBalance.Q1.IsOver) ? assessmentBalance.Q1.Amount - assessmentBalance.Q1.ByExcessDeduction - assessmentBalance.Q1.Paid - assessmentBalance.Q1.Discount : 0;
                        var q2 = (!assessmentBalance.Q2.IsCompleted && !assessmentBalance.Q2.IsOver) ? assessmentBalance.Q2.Amount - assessmentBalance.Q2.ByExcessDeduction - assessmentBalance.Q2.Paid - assessmentBalance.Q2.Discount : 0;
                        var q3 = (!assessmentBalance.Q3.IsCompleted && !assessmentBalance.Q3.IsOver) ? assessmentBalance.Q3.Amount - assessmentBalance.Q3.ByExcessDeduction - assessmentBalance.Q3.Paid - assessmentBalance.Q3.Discount : 0;
                        var q4 = (!assessmentBalance.Q4.IsCompleted && !assessmentBalance.Q4.IsOver) ? assessmentBalance.Q4.Amount - assessmentBalance.Q4.ByExcessDeduction - assessmentBalance.Q4.Paid - assessmentBalance.Q4.Discount : 0;

                        if (assessmentBalance.NumberOfPayments-assessmentBalance.NumberOfCancels == 1 && month == 1)
                        {
                            assessmentBalance.ExcessPayment = trns.Last().RunningOverPay;
                            assessmentBalance.ByExcessDeduction = 0;

                            assessmentBalance.Paid = 0;
                            assessmentBalance.Discount = 0;
                            assessmentBalance.DiscountRate = 0;
                            assessmentBalance.OverPayment = 0;
                            assessmentBalance.IsCompleted = false;

                            assessmentBalance.LYArrears = trns.Last().LYArrears;
                            assessmentBalance.LYWarrant = trns.Last().LYWarrant;


                            assessmentBalance.Q1.Paid = 0;
                            assessmentBalance.Q1.ByExcessDeduction = 0;
                            assessmentBalance.Q1.Discount = 0;
                            assessmentBalance.Q1.IsCompleted = false;

                            assessmentBalance.Q2.Paid = 0;
                            assessmentBalance.Q2.ByExcessDeduction = 0;
                            assessmentBalance.Q2.Discount = 0;
                            assessmentBalance.Q2.IsCompleted = false;

                            assessmentBalance.Q3.Paid = 0;
                            assessmentBalance.Q3.ByExcessDeduction = 0;
                            assessmentBalance.Q3.Discount = 0;
                            assessmentBalance.Q3.IsCompleted = false;

                            assessmentBalance.Q4.Paid = 0;
                            assessmentBalance.Q4.ByExcessDeduction = 0;
                            assessmentBalance.Q4.Discount = 0;
                            assessmentBalance.Q4.IsCompleted = false;




                        }
                        else
                        {

                            assessmentBalance.Paid -= mxs.Last().TotalAmount;
                            assessmentBalance.Discount -= mxs.Last().DiscountAmount;

                            assessmentBalance.DiscountRate = trns.Last().DiscountRate;

                            assessmentBalance.OverPayment = trns.Last().RunningOverPay;

                            assessmentBalance.LYArrears = trns.Last().LYArrears;
                            assessmentBalance.LYWarrant = trns.Last().LYWarrant;

                            assessmentBalance.TYArrears = trns.Last().TYArrears;
                            assessmentBalance.TYWarrant = trns.Last().TYWarrant;


                            if (assessmentBalance.AnnualAmount + assessmentBalance.LYArrears + assessmentBalance.LYWarrant + assessmentBalance.TYWarrant + assessmentBalance.TYArrears - (assessmentBalance.ByExcessDeduction + assessmentBalance.Paid + assessmentBalance.Discount) > 0)
                            {
                                assessmentBalance.IsCompleted = false;
                            }


                            if (!assessmentBalance.Q1.IsOver && q1 != trns.Last().Q1)
                            {
                                assessmentBalance.Q1.Paid = assessmentBalance.Q1.Amount - (trns.Last().Q1 + assessmentBalance.Q1.ByExcessDeduction);
                                //assessmentBalance.Q1.ByExcessDeduction = 0;
                                assessmentBalance.Q1.Discount = 0;
                                assessmentBalance.Q1.IsCompleted = false;
                            }

                            if (!assessmentBalance.Q2.IsOver && q2 != trns.Last().Q2)
                            {

                                assessmentBalance.Q2.Paid = assessmentBalance.Q2.Amount - (trns.Last().Q2 + assessmentBalance.Q2.ByExcessDeduction);
                                //assessmentBalance.Q2.ByExcessDeduction = 0;
                                assessmentBalance.Q2.Discount = 0;
                                assessmentBalance.Q2.IsCompleted = false;
                            }

                            if (!assessmentBalance.Q3.IsOver && q3 != trns.Last().Q3)
                            {
                                assessmentBalance.Q3.Paid = assessmentBalance.Q3.Amount - (trns.Last().Q3 + assessmentBalance.Q3.ByExcessDeduction);
                                //assessmentBalance.Q3.ByExcessDeduction = 0;
                                assessmentBalance.Q3.Discount = 0;
                                assessmentBalance.Q3.IsCompleted = false;

                            }

                            if (!assessmentBalance.Q4.IsOver && q4 != trns.Last().Q4)
                            {
                                assessmentBalance.Q4.Paid = assessmentBalance.Q4.Amount - (trns.Last().Q4 + assessmentBalance.Q4.ByExcessDeduction);
                                //assessmentBalance.Q4.ByExcessDeduction = 0;
                                assessmentBalance.Q4.Discount = 0;
                                assessmentBalance.Q4.IsCompleted = false;

                            }
                        }
                        assessmentBalance.NumberOfCancels += 1;
                        if (assessmentBalance.Q1.IsCompleted && assessmentBalance.Q2.IsCompleted && assessmentBalance.Q3.IsCompleted && assessmentBalance.Q4.IsCompleted)
                        {
                            assessmentBalance.IsCompleted = true;
                        }
                        else
                        {
                            assessmentBalance.IsCompleted = false;
                        }
                        assessmentBalance.HasTransaction = false;
                        createTransaction(assessmentBalance, AssessmentTransactionsType.ReversePayemet);

                    }

                        //throw new Exception("Unable to delete Assessment Order.");

                }
                else
                {

                    throw new Exception("Unable to delete Assessment Order.");

                }

                return true;

            }
            catch (Exception ex)
            {

                return false;

            }
            
            
            }
        private async void createTransaction(AssessmentBalance assessmentBalance, AssessmentTransactionsType trp)
        {
            //if (assessmentBalance == null || assessmentBalance.Q1 == null || assessmentBalance.Q2 == null || assessmentBalance.Q3 == null || assessmentBalance.Q4 == null) { }

            var q1 = (!assessmentBalance!.Q1!.IsCompleted && !assessmentBalance.Q1.IsOver) ? assessmentBalance.Q1!.Amount - assessmentBalance.Q1.ByExcessDeduction - assessmentBalance.Q1.Paid - assessmentBalance.Q1.Discount : 0;
            var q2 = (!assessmentBalance!.Q2!.IsCompleted && !assessmentBalance.Q2.IsOver) ? assessmentBalance.Q2!.Amount - assessmentBalance.Q2.ByExcessDeduction - assessmentBalance.Q2.Paid - assessmentBalance.Q2.Discount : 0;
            var q3 = (!assessmentBalance!.Q3!.IsCompleted && !assessmentBalance.Q3.IsOver) ? assessmentBalance.Q3!.Amount - assessmentBalance.Q3.ByExcessDeduction - assessmentBalance.Q3.Paid - assessmentBalance.Q3.Discount : 0;
            var q4 = (!assessmentBalance!.Q4!.IsCompleted && !assessmentBalance.Q4.IsOver) ? assessmentBalance.Q4!.Amount - assessmentBalance.Q4.ByExcessDeduction - assessmentBalance.Q4.Paid - assessmentBalance.Q4.Discount : 0;
            var transaction = new AssessmentTransaction
            {
                AssessmentId = assessmentBalance.AssessmentId,
                //DateTime = DateTime.Now,
                Type = trp,
                LYArrears = assessmentBalance.LYArrears,
                LYWarrant = assessmentBalance.LYWarrant,
                TYArrears = assessmentBalance.TYArrears,
                TYWarrant = assessmentBalance.TYWarrant,
                RunningOverPay = assessmentBalance.OverPayment + assessmentBalance.ExcessPayment,
                Q1 = q1,
                Q2 = q2,
                Q3 = q3,
                Q4 = q4,
                RunningDiscount = assessmentBalance.Discount,
                RunningTotal =
                assessmentBalance.LYArrears
                + assessmentBalance.LYWarrant
                + assessmentBalance.TYArrears
                + assessmentBalance.TYWarrant
                + q1
                + q2
                + q3
                + q4
                - assessmentBalance.OverPayment - assessmentBalance.ExcessPayment,
                DiscountRate=assessmentBalance.DiscountRate,
            };

            await _unitOfWork.AssessmentTransactions.AddAsync(transaction);
        }
    }
}
