using CAT20.Core;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Services.AssessmentTax;

namespace CAT20.Services.AssessmentTax
{

    public class AssessmentBalanceService : IAssessmentBalanceService
    {
        private readonly IAssessmentTaxUnitOfWork _unitOfWork;
        public AssessmentBalanceService(IAssessmentTaxUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AssessmentBalance> GetById(int id)
        {
            return await _unitOfWork.AssessmentBalances.GetById(id);
        }

        public async Task<IEnumerable<AssessmentBalance>> GetAll()
        {
            return await _unitOfWork.AssessmentBalances.GetAll();
        }

        public async Task<IEnumerable<AssessmentBalance>> GetAllForOffice(int officeid)
        {
            return await _unitOfWork.AssessmentBalances.GetAllForOffice(officeid);
        }

        public async Task<IEnumerable<AssessmentBalance>> GetAllForSabha(int sabhhaid)
        {
            return await _unitOfWork.AssessmentBalances.GetAllForSabha(sabhhaid);
        }

        public async Task<IEnumerable<AssessmentBalance>> GetAllForAssessmentId(int assessementid)
        {
            return await _unitOfWork.AssessmentBalances.GetAllForAssessmentId(assessementid);
        }

        public async Task<IEnumerable<AssessmentBalance>> GetAllForAssessmentIdAndYear(int assessementid, int year)
        {
            return await _unitOfWork.AssessmentBalances.GetAllForAssessmentIdAndYear(assessementid, year);
        }

        public async Task<IEnumerable<AssessmentBalance>> GetAllForAssessmentIdAndYearAndQuarter(int assessementid, int year, int quarter)
        {
            return await _unitOfWork.AssessmentBalances.GetAllForAssessmentIdAndYearAndQuarter(assessementid, year, quarter);
        }

        public async Task<AssessmentBalance> Create(AssessmentBalance newQuarterOpeningBalance)
        {
            try
            {
                await _unitOfWork.AssessmentBalances
                .AddAsync(newQuarterOpeningBalance);

                #region Audit Log
                //var note = new StringBuilder();
                //if (newQuarterOpeningBalance.ID == 0)
                //    note.Append("Created on ");
                //else
                //    note.Append("Edited on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append("System");


                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = newQuarterOpeningBalance.ID,
                //    TransactionName = "QuarterOpeningBalance",
                //    User = 1,
                //    Note = note.ToString()
                //});

                #endregion

                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            return newQuarterOpeningBalance;
        }

        public async Task<IEnumerable<AssessmentBalance>> BulkCreate(IEnumerable<AssessmentBalance> newObjsList)
        {
            try
            {
                await _unitOfWork.AssessmentBalances
                .AddRangeAsync(newObjsList);

                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            return newObjsList;
        }

        public async Task Update(AssessmentBalance QuarterOpeningBalanceToBeUpdated, AssessmentBalance QuarterOpeningBalance)
        {
            try
            {
                #region Audit Log

                //var note = new StringBuilder();
                //note.Append("Edited on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append(" System.");

                //if (QuarterOpeningBalanceToBeUpdated.NameEnglish != QuarterOpeningBalance.NameEnglish)
                //    note.Append(" English Name :" + QuarterOpeningBalanceToBeUpdated.NameEnglish + " Change to " + QuarterOpeningBalance.NameEnglish);
                //if (QuarterOpeningBalanceToBeUpdated.NameSinhala != QuarterOpeningBalance.NameSinhala)
                //    note.Append(" Sinhala Name :" + QuarterOpeningBalanceToBeUpdated.NameSinhala + " Change to " + QuarterOpeningBalance.NameSinhala);
                //if (QuarterOpeningBalanceToBeUpdated.NameTamil != QuarterOpeningBalance.NameTamil)
                //    note.Append(" Tamil Name :" + QuarterOpeningBalanceToBeUpdated.NameTamil + " Change to " + QuarterOpeningBalance.NameTamil);
                //if (QuarterOpeningBalanceToBeUpdated.Code != QuarterOpeningBalance.Code)
                //    note.Append(" Code :" + QuarterOpeningBalanceToBeUpdated.Code + " Change to " + QuarterOpeningBalance.Code);

                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = QuarterOpeningBalanceToBeUpdated.ID,
                //    TransactionName = "QuarterOpeningBalance",
                //    User = 1,
                //    Note = note.ToString()
                //});

                #endregion

                //QuarterOpeningBalanceToBeUpdated.OBYear =QuarterOpeningBalance.OBYear;
                //QuarterOpeningBalanceToBeUpdated.QuarterNumber =QuarterOpeningBalance.QuarterNumber;
                //QuarterOpeningBalanceToBeUpdated.AssessmentId =QuarterOpeningBalance.AssessmentId;
                //QuarterOpeningBalanceToBeUpdated.ProcessDate =QuarterOpeningBalance.ProcessDate;
                //QuarterOpeningBalanceToBeUpdated.LYArrears =QuarterOpeningBalance.LYArrears;
                //QuarterOpeningBalanceToBeUpdated.LYWarrant =QuarterOpeningBalance.LYWarrant;
                //QuarterOpeningBalanceToBeUpdated.LYCArrears =QuarterOpeningBalance.LYCArrears;
                //QuarterOpeningBalanceToBeUpdated.LYCWarrant =QuarterOpeningBalance.LYCWarrant;
                //QuarterOpeningBalanceToBeUpdated.LQArrears =QuarterOpeningBalance.LQArrears;
                //QuarterOpeningBalanceToBeUpdated.LQWarrant =QuarterOpeningBalance.LQWarrant;
                //QuarterOpeningBalanceToBeUpdated.LQCArrears =QuarterOpeningBalance.LQCArrears;
                //QuarterOpeningBalanceToBeUpdated.LQCWarrant =QuarterOpeningBalance.LQCWarrant;
                //QuarterOpeningBalanceToBeUpdated.HaveToQPay =QuarterOpeningBalance.HaveToQPay;
                //QuarterOpeningBalanceToBeUpdated.QPay =QuarterOpeningBalance.QPay;
                //QuarterOpeningBalanceToBeUpdated.QDiscount =QuarterOpeningBalance.QDiscount;
                //QuarterOpeningBalanceToBeUpdated.QTotal =QuarterOpeningBalance.QTotal;
                //QuarterOpeningBalanceToBeUpdated.FullTotal =QuarterOpeningBalance.FullTotal;
                //QuarterOpeningBalanceToBeUpdated.Status =QuarterOpeningBalance.Status;
                //QuarterOpeningBalanceToBeUpdated.ProcessUpdateWarrant =QuarterOpeningBalance.ProcessUpdateWarrant;
                //QuarterOpeningBalanceToBeUpdated.ProcessUpdateArrears =QuarterOpeningBalance.ProcessUpdateArrears;
                //QuarterOpeningBalanceToBeUpdated.ProcessUpdateComment =QuarterOpeningBalance.ProcessUpdateComment;
                //QuarterOpeningBalanceToBeUpdated.OldArrears =QuarterOpeningBalance.OldArrears;
                //QuarterOpeningBalanceToBeUpdated.OldWarrant =QuarterOpeningBalance.OldWarrant;
                //QuarterOpeningBalanceToBeUpdated.UpdatedAt = DateTime.Now ;
                //QuarterOpeningBalanceToBeUpdated.UpdatedBy =QuarterOpeningBalance.UpdatedBy;

                await _unitOfWork.CommitAsync();


            }
            catch (Exception)
            {
            }
        }

        public async Task Delete(AssessmentBalance obj)
        {
            _unitOfWork.AssessmentBalances.Remove(obj);
            await _unitOfWork.CommitAsync();
        }

        public (HAssessmentBalance, HAssessmentBalance, HAssessmentBalance, HAssessmentBalance, HAssessmentBalance, HAssessmentBalance, decimal) CalculatePaymentBalance(AssessmentBalance balance, AssessmentRates rates, decimal inputAmount, int month, bool isPayment)
        {
            if (balance != null && balance.Q1 != null && balance.Q2 != null && balance.Q3 != null && balance.Q4 != null && rates != null)
            {






                var outstandingPayable = new HAssessmentBalance();
                var payable = new HAssessmentBalance();
                var discount = new HAssessmentBalance();
                //var DeductionBalace = new HAssessmentBalance();

                var calcDct = new HAssessmentBalance();
                //var payingBalance = new HAssessmentBalance();
                //var nextBalance = new HAssessmentBalance();







                outstandingPayable.LYWarrant = balance.LYWarrant;
                outstandingPayable.LYArrears = balance.LYArrears;
                outstandingPayable.TYWarrant = balance.TYWarrant;
                outstandingPayable.TYArrears = balance.TYArrears;



                payable.LYWarrant = balance.LYWarrant;
                payable.LYArrears = balance.LYArrears;
                payable.TYWarrant = balance.TYWarrant;
                payable.TYArrears = balance.TYArrears;


                if (!balance.Q1.IsOver && !balance.Q1.IsCompleted)
                {
                    payable.Q1 = balance.Q1.Amount - balance.Q1.ByExcessDeduction - balance.Q1.Paid - balance.Q1.Discount;

                    if (balance.CurrentQuarter == 1)
                    {
                        outstandingPayable.Q1 += payable.Q1;
                    }




                }
                if (!balance.Q2.IsOver && !balance.Q2.IsCompleted)
                {
                    payable.Q2 = balance.Q2.Amount - balance.Q2.ByExcessDeduction - balance.Q2.Paid - balance.Q2.Discount;

                    if (balance.CurrentQuarter == 2)
                    {
                        outstandingPayable.Q2 += payable.Q2;
                    }


                }

                if (!balance.Q3.IsOver && !balance.Q3.IsCompleted)
                {
                    payable.Q3 = balance.Q3.Amount - balance.Q3.ByExcessDeduction - balance.Q3.Paid - balance.Q3.Discount;

                    if (balance.CurrentQuarter == 3)
                    {
                        outstandingPayable.Q3 += payable.Q3;
                    }



                }

                if (!balance.Q4.IsOver && !balance.Q4.IsCompleted)
                {
                    payable.Q4 = balance.Q4.Amount - balance.Q4.ByExcessDeduction - balance.Q4.Paid - balance.Q4.Discount;

                    if (balance.CurrentQuarter == 4)
                    {
                        outstandingPayable.Q4 += payable.Q4;
                    }



                }

                payable.Total = payable.LYWarrant + payable.LYArrears + payable.TYWarrant + payable.TYArrears + payable.Q1 + payable.Q2 + payable.Q3 + payable.Q4;





                (decimal appliedRate, decimal appliedDct, decimal correctiondct) = getPaymentDiscount(balance, payable, rates, inputAmount, ref calcDct, month);



                (var deductionBalace, var remainBalace) = DeductionCalculator(balance, payable, balance.ExcessPayment, month, ref calcDct, ref discount);


                (var payingBalance, var nextBalance) = DeductionCalculator(balance, remainBalace, inputAmount, month, ref calcDct, ref discount);







                /*





                if (payable.LYWarrant <= remainder)
                {
                    payingBalance.LYWarrant = payable.LYWarrant;
                    nextBalance.LYWarrant = 0;
                    remainder -= payable.LYWarrant;

                    payingBalance.Total += payingBalance.LYWarrant;
                    nextBalance.Total += nextBalance.LYWarrant;

                }
                else
                {
                    payingBalance.LYWarrant = remainder;
                    nextBalance.LYWarrant = payable.LYWarrant - remainder;
                    remainder = 0;

                    payingBalance.Total += payingBalance.LYWarrant;
                    nextBalance.Total += nextBalance.LYWarrant;

                }

                if (payable.LYArrears <= remainder)
                {
                    payingBalance.LYArrears = payable.LYArrears;
                    nextBalance.LYArrears = 0;
                    remainder -= payable.LYArrears;



                    payingBalance.Total += payingBalance.LYArrears;
                    nextBalance.Total += nextBalance.LYArrears;
                }
                else
                {
                    payingBalance.LYArrears = remainder;
                    nextBalance.LYArrears = payable.LYArrears - remainder;
                    remainder = 0;

                    payingBalance.Total += payingBalance.LYArrears;
                    nextBalance.Total += nextBalance.LYArrears;
                }

                //this year


                if (payable.TYWarrant <= remainder)
                {
                    payingBalance.TYWarrant = payable.TYWarrant;
                    nextBalance.TYWarrant = 0;
                    remainder -= payable.TYWarrant;

                    payingBalance.Total += payingBalance.TYWarrant;
                    nextBalance.Total += nextBalance.TYWarrant;
                }
                else
                {
                    payingBalance.TYWarrant = remainder;
                    nextBalance.TYWarrant = payable.TYWarrant - remainder;
                    remainder = 0;

                    payingBalance.Total += payingBalance.TYWarrant;
                    nextBalance.Total += nextBalance.TYWarrant;
                }

                if (payable.TYArrears <= remainder)
                {
                    payingBalance.TYArrears = payable.TYArrears;
                    nextBalance.TYArrears = 0;
                    remainder -= payable.TYArrears;

                    payingBalance.Total += payingBalance.TYArrears;
                    nextBalance.Total += nextBalance.TYArrears;
                }
                else
                {
                    payingBalance.TYArrears = remainder;
                    nextBalance.TYArrears = payable.TYArrears - remainder;
                    remainder = 0;

                    payingBalance.Total += payingBalance.TYArrears;
                    nextBalance.Total += nextBalance.TYArrears;
                }

                //quarters


                //Q1
                if (balance.Q1?.Id != null && !balance.Q1.IsOver && !balance.Q1.IsCompleted)
                {
                    totalPayable += balance.Q1.Amount;


                    if (month == 1)
                    {

                        discount.Q1 = appliedDec;

                        if (checkBoxes.Item1)
                        {
                            //outstandingPayable.Q1 -= ousDec;
                            perBalance.Q1 -= perDec;
                            perBalance.DiscountTotal += perDec;
                        }
                    }

                    if (payable.Q1 - discount.Q1 <= remainder)
                    {
                        nextBalance.Q1 = 0;

                        payingBalance.Q1 = payable.Q1 - discount.Q1;
                        //remainder = remainder - balance.Q1?.Amount;
                        remainder = remainder - payingBalance.Q1;


                        payingBalance.Total += payingBalance.Q1;
                        nextBalance.Total += nextBalance.Q1;
                    }
                    else
                    {
                        payingBalance.Q1 = remainder;
                        //nextBalance.Q1 = balance?.Q1?.Amount - remainder;
                        nextBalance.Q1 = payable.Q1 - remainder;
                        remainder = 0;

                        discount.Q1 = 0;
                        payingBalance.Total += payingBalance.Q1;
                        nextBalance.Total += nextBalance.Q1;
                    }
                    //else
                    //{
                    //    discount.Q1 = 0;
                    //    nextBalance.Q1 = -1;
                    //}
                    discount.Total += discount.Q1;
                }

                //Q2

                if (balance?.Q2?.Id != null && !balance.Q2.IsOver && !balance.Q2.IsCompleted)
                {
                    totalPayable += balance.Q2.Amount;

                    if (month <= 4)
                    {



                        discount.Q2 = appliedDec;

                        if (checkBoxes.Item2)
                        {

                            perBalance.Q2 -= perDec;
                            perBalance.DiscountTotal += perDec;
                        }
                    }

                    if (payable.Q2 - discount.Q2 <= remainder)
                    {
                        nextBalance.Q2 = 0;


                        payingBalance.Q2 = payable.Q2 - discount.Q2;
                        //remainder = remainder - balance.Q2?.Amount;
                        remainder = remainder - payingBalance.Q2;


                        payingBalance.Total += payingBalance.Q2;
                        nextBalance.Total += nextBalance.Q2;
                    }
                    else
                    {
                        payingBalance.Q2 = remainder;
                        nextBalance.Q2 = payable.Q2 - remainder;
                        remainder = 0;

                        discount.Q2 = 0;
                        payingBalance.Total += payingBalance.Q2;
                        nextBalance.Total += nextBalance.Q2;
                    }
                    //else
                    //{
                    //    discount.Q2 = 0;
                    //    nextBalance.Q2 = -1;
                    //}
                    discount.Total += discount.Q2;
                }


                //Q3

                if (balance.Q3.Id != null && !balance.Q3.IsOver && !balance.Q3.IsCompleted)
                {
                    totalPayable += balance.Q3.Amount;

                    if (month <= 7)
                    {

                        discount.Q3 = appliedDec;

                        if (checkBoxes.Item3)
                        {
                            perBalance.Q3 -= perDec;
                            perBalance.DiscountTotal += perDec;
                        }
                    }

                    if (payable.Q3 - discount.Q3 <= remainder)
                    {
                        nextBalance.Q3 = 0;


                        payingBalance.Q3 = payable.Q3 - discount.Q3;
                        //remainder = remainder - balance.Q3?.Amount;
                        remainder = remainder - payingBalance.Q3;


                        payingBalance.Total += payingBalance.Q3;
                        nextBalance.Total += nextBalance.Q3;
                    }
                    else
                    {
                        payingBalance.Q3 = remainder;
                        nextBalance.Q3 = payable.Q3 - remainder;
                        remainder = 0;

                        discount.Q3 = 0;
                        payingBalance.Total += payingBalance.Q3;
                        nextBalance.Total += nextBalance.Q3;
                    }
                    //else
                    //{
                    //    discount.Q3 = 0;
                    //    nextBalance.Q3 = -1;
                    //}

                    discount.Total += discount.Q3;
                }

                //Q4

                if (balance?.Q4?.Id != null && !balance.Q4.IsOver && !balance.Q4.IsCompleted)
                {
                    totalPayable += balance.Q4.Amount;

                    if (month <= 10)
                    {
                        discount.Q4 = appliedDec;

                        if (checkBoxes.Item4)
                        {

                            perBalance.Q4 -= perDec;
                            perBalance.DiscountTotal += perDec;
                        }
                    }

                    if (payable.Q4 - discount.Q4 <= remainder)
                    {
                        nextBalance.Q4 = 0;

                        payingBalance.Q4 = payable.Q4 - discount.Q4;
                        //remainder = remainder - balance.Q4?.Amount;
                        remainder = remainder - payingBalance.Q4;


                        payingBalance.Total += payingBalance.Q4;
                        nextBalance.Total += nextBalance.Q4;
                    }
                    else
                    {
                        payingBalance.Q4 = remainder;
                        nextBalance.Q4 = payable.Q4 - remainder;
                        remainder = 0;

                        discount.Q4 = 0;
                        payingBalance.Total += payingBalance.Q4;
                        nextBalance.Total += nextBalance.Q4;
                    }
                    //else
                    //{
                    //    discount.Q4 = 0;
                    //    nextBalance.Q4 = -1;
                    //}

                    discount.Total += discount.Q4;
                }

                payingBalance.OverPayment = remainder;
                //payingBalance.Total += payingBalance.OverPayment;

                //var isPayament = true;
                if (month == 1 && isPayment && (nextBalance.Q1 != 0 || nextBalance.Q2 != 0 || nextBalance.Q3 != 0 || nextBalance.Q4 != 0))
                {
                    payingBalance.OverPayment += payingBalance.Q1 + payingBalance.Q2 + payingBalance.Q3 + payingBalance.Q4;

                    payingBalance.Q1 = 0;
                    payingBalance.Q2 = 0;
                    payingBalance.Q3 = 0;
                    payingBalance.Q4 = 0;

                    nextBalance.Q1 = 0;
                    nextBalance.Q2 = 0;
                    nextBalance.Q3 = 0;
                    nextBalance.Q4 = 0;

                    discount.Q1 = 0;
                    discount.Q2 = 0;
                    discount.Q3 = 0;
                    discount.Q4 = 0;

                    discount.Total = 0;

                }



                perBalance.DiscountRate = perRate;
                perBalance.ExcessPayment = balance.OverPayment;

                perBalance.Total = perBalance.LYWarrant + perBalance.LYArrears + perBalance.TYWarrant + perBalance.TYArrears + perBalance.Q1 + perBalance.Q2 + perBalance.Q3 + perBalance.Q4 - perBalance.ExcessPayment;


                outstandingPayable.Total = outstandingPayable.LYWarrant + outstandingPayable.LYArrears + outstandingPayable.TYWarrant + outstandingPayable.TYArrears + outstandingPayable.Q1 + outstandingPayable.Q2 + outstandingPayable.Q3 + outstandingPayable.Q4;



                */

                //return (outstandingPayable, payable, payingBalance, nextBalance, discount, appliedRate);
                return (outstandingPayable, payable, deductionBalace, payingBalance, nextBalance, discount, appliedRate);


            }
            else
            {
                throw new ArgumentNullException(nameof(balance), "Balance cannot be null.");
            }

        }

        private (HAssessmentBalance, HAssessmentBalance) DeductionCalculator(AssessmentBalance balance, HAssessmentBalance payable, decimal? remainder, int month, ref HAssessmentBalance calcDct, ref HAssessmentBalance discount)
        {
            var nextBalance = new HAssessmentBalance();
            var payingBalance = new HAssessmentBalance();

            if (payable.LYWarrant <= remainder)
            {
                payingBalance.LYWarrant = payable.LYWarrant;
                nextBalance.LYWarrant = 0;
                remainder -= payable.LYWarrant;

                payingBalance.Total += payingBalance.LYWarrant;
                nextBalance.Total += nextBalance.LYWarrant;

            }
            else
            {
                payingBalance.LYWarrant = remainder;
                nextBalance.LYWarrant = payable.LYWarrant - remainder;
                remainder = 0;

                payingBalance.Total += payingBalance.LYWarrant;
                nextBalance.Total += nextBalance.LYWarrant;

            }

            if (payable.LYArrears <= remainder)
            {
                payingBalance.LYArrears = payable.LYArrears;
                nextBalance.LYArrears = 0;
                remainder -= payable.LYArrears;



                payingBalance.Total += payingBalance.LYArrears;
                nextBalance.Total += nextBalance.LYArrears;
            }
            else
            {
                payingBalance.LYArrears = remainder;
                nextBalance.LYArrears = payable.LYArrears - remainder;
                remainder = 0;

                payingBalance.Total += payingBalance.LYArrears;
                nextBalance.Total += nextBalance.LYArrears;
            }

            //this year


            if (payable.TYWarrant <= remainder)
            {
                payingBalance.TYWarrant = payable.TYWarrant;
                nextBalance.TYWarrant = 0;
                remainder -= payable.TYWarrant;

                payingBalance.Total += payingBalance.TYWarrant;
                nextBalance.Total += nextBalance.TYWarrant;
            }
            else
            {
                payingBalance.TYWarrant = remainder;
                nextBalance.TYWarrant = payable.TYWarrant - remainder;
                remainder = 0;

                payingBalance.Total += payingBalance.TYWarrant;
                nextBalance.Total += nextBalance.TYWarrant;
            }

            if (payable.TYArrears <= remainder)
            {
                payingBalance.TYArrears = payable.TYArrears;
                nextBalance.TYArrears = 0;
                remainder -= payable.TYArrears;

                payingBalance.Total += payingBalance.TYArrears;
                nextBalance.Total += nextBalance.TYArrears;
            }
            else
            {
                payingBalance.TYArrears = remainder;
                nextBalance.TYArrears = payable.TYArrears - remainder;
                remainder = 0;

                payingBalance.Total += payingBalance.TYArrears;
                nextBalance.Total += nextBalance.TYArrears;
            }

            //quarters


            //Q1
            if (balance.Q1?.Id != null && !balance.Q1.IsOver && !balance.Q1.IsCompleted && payable.Q1 != 0)
            {


                if (month == 1)
                {

                    discount.Q1 = calcDct.Q1;


                }

                if (payable.Q1 - discount.Q1 <= remainder)
                {
                    nextBalance.Q1 = 0;

                    payingBalance.Q1 = payable.Q1 - discount.Q1;
                    //remainder = remainder - balance.Q1?.Amount;
                    remainder = remainder - payingBalance.Q1;


                    payingBalance.Total += payingBalance.Q1;
                    nextBalance.Total += nextBalance.Q1;
                }
                else
                {
                    payingBalance.Q1 = remainder;
                    //nextBalance.Q1 = balance?.Q1?.Amount - remainder;
                    nextBalance.Q1 = payable.Q1 - remainder;
                    remainder = 0;

                    discount.Q1 = 0;
                    payingBalance.Total += payingBalance.Q1;
                    nextBalance.Total += nextBalance.Q1;
                }
                //else
                //{
                //    discount.Q1 = 0;
                //    nextBalance.Q1 = -1;
                //}
                discount.Total += discount.Q1;
            }

            //Q2

            if (balance?.Q2?.Id != null && !balance.Q2.IsOver && !balance.Q2.IsCompleted && payable.Q2 != 0)
            {

                if (month <= 4)
                {



                    discount.Q2 = calcDct.Q2;


                }

                if (payable.Q2 - discount.Q2 <= remainder)
                {
                    nextBalance.Q2 = 0;


                    payingBalance.Q2 = payable.Q2 - discount.Q2;
                    //remainder = remainder - balance.Q2?.Amount;
                    remainder = remainder - payingBalance.Q2;


                    payingBalance.Total += payingBalance.Q2;
                    nextBalance.Total += nextBalance.Q2;
                }
                else
                {
                    payingBalance.Q2 = remainder;
                    nextBalance.Q2 = payable.Q2 - remainder;
                    remainder = 0;

                    discount.Q2 = 0;
                    payingBalance.Total += payingBalance.Q2;
                    nextBalance.Total += nextBalance.Q2;
                }
                //else
                //{
                //    discount.Q2 = 0;
                //    nextBalance.Q2 = -1;
                //}
                discount.Total += discount.Q2;
            }


            //Q3

            if (balance.Q3.Id != null && !balance.Q3.IsOver && !balance.Q3.IsCompleted && payable.Q3 != 0)
            {

                if (month <= 7)
                {

                    discount.Q3 = calcDct.Q3;


                }

                if (payable.Q3 - discount.Q3 <= remainder)
                {
                    nextBalance.Q3 = 0;


                    payingBalance.Q3 = payable.Q3 - discount.Q3;
                    //remainder = remainder - balance.Q3?.Amount;
                    remainder = remainder - payingBalance.Q3;


                    payingBalance.Total += payingBalance.Q3;
                    nextBalance.Total += nextBalance.Q3;
                }
                else
                {
                    payingBalance.Q3 = remainder;
                    nextBalance.Q3 = payable.Q3 - remainder;
                    remainder = 0;

                    discount.Q3 = 0;
                    payingBalance.Total += payingBalance.Q3;
                    nextBalance.Total += nextBalance.Q3;
                }
                //else
                //{
                //    discount.Q3 = 0;
                //    nextBalance.Q3 = -1;
                //}

                discount.Total += discount.Q3;
            }

            //Q4

            if (balance?.Q4?.Id != null && !balance.Q4.IsOver && !balance.Q4.IsCompleted && payable.Q4 != 0)
            {

                if (month <= 10)
                {
                    discount.Q4 = calcDct.Q4;


                }

                if (payable.Q4 - discount.Q4 <= remainder)
                {
                    nextBalance.Q4 = 0;

                    payingBalance.Q4 = payable.Q4 - discount.Q4;
                    //remainder = remainder - balance.Q4?.Amount;
                    remainder = remainder - payingBalance.Q4;


                    payingBalance.Total += payingBalance.Q4;
                    nextBalance.Total += nextBalance.Q4;
                }
                else
                {
                    payingBalance.Q4 = remainder;
                    nextBalance.Q4 = payable.Q4 - remainder;
                    remainder = 0;

                    discount.Q4 = 0;
                    payingBalance.Total += payingBalance.Q4;
                    nextBalance.Total += nextBalance.Q4;
                }
                //else
                //{
                //    discount.Q4 = 0;
                //    nextBalance.Q4 = -1;
                //}

                discount.Total += discount.Q4;
            }


            payingBalance.OverPayment = remainder + calcDct.OverPayment;




            return (payingBalance, nextBalance);
        }

        private (decimal, decimal, decimal) getPaymentDiscount(AssessmentBalance balance, HAssessmentBalance payable, AssessmentRates rates, decimal inputAmount, ref HAssessmentBalance calcDct, int month)
        {

            decimal appliedRate = 0;
            decimal appliedDct = 0;
            decimal correctiondct = 0;


            decimal remainder = (decimal)balance.ExcessPayment + inputAmount;


            var perannualdct = balance.AnnualAmount * rates.AnnualDiscount;
            var roundedAnualdct = Math.Round((decimal)perannualdct, 2, MidpointRounding.AwayFromZero);

            var quarterPaid = balance.Q1.ByExcessDeduction + balance.Q2.ByExcessDeduction + balance.Q3.ByExcessDeduction + balance.Q4.ByExcessDeduction + balance.Q1.Paid + balance.Q2.Paid + balance.Q3.Paid + balance.Q4.Paid;

            var availableDiscount = roundedAnualdct - balance.Discount;



            //var totalPayableAmount = balance.LYWarrant + balance.LYArrears + balance.TYWarrant + balance.TYArrears + balance.AnnualAmount - quarterPaid - balance.Discount - availableDiscount;
            var totalPayableAmount = payable.Total - availableDiscount;


            if (month == 1 && rates != null && totalPayableAmount <= remainder)
            {
                appliedRate = rates.AnnualDiscount ?? 0m;
                var annualdct = balance.AnnualAmount * rates.AnnualDiscount;

                var remaindct = annualdct - balance.Discount;

                var avialbleQuarters = 4;
                if (balance.Q1.IsCompleted)
                {
                    avialbleQuarters -= 1;
                }
                if (balance.Q2.IsCompleted)
                {
                    avialbleQuarters -= 1;
                }
                if (balance.Q3.IsCompleted)
                {
                    avialbleQuarters -= 1;
                }
                if (balance.Q4.IsCompleted)
                {
                    avialbleQuarters -= 1;
                }



                (decimal, decimal) InnerFunction(decimal? availabledct, int quarters)
                {
                    decimal? dctq = 0;
                    if (1 <= avialbleQuarters)
                        dctq = availabledct / quarters;

                    appliedDct = Math.Round((decimal)dctq, 2, MidpointRounding.AwayFromZero);

                    correctiondct = Math.Round((decimal)remaindct, 2, MidpointRounding.AwayFromZero) - appliedDct * avialbleQuarters;

                    return (appliedDct, correctiondct);
                }



                (appliedDct, correctiondct) = InnerFunction(remaindct, avialbleQuarters);

                if (!balance.Q1.IsCompleted)
                {
                    if (payable.Q1 < appliedDct)
                    {
                        calcDct.Q1 = payable.Q1;
                        remaindct -= calcDct.Q1;
                        avialbleQuarters -= 1;
                        (appliedDct, correctiondct) = InnerFunction(remaindct, avialbleQuarters);
                    }
                    else
                    {
                        calcDct.Q1 = appliedDct;
                    }

                }

                if (!balance.Q2.IsCompleted)
                {
                    if (payable.Q2 < appliedDct)
                    {
                        calcDct.Q2 = payable.Q2;
                        remaindct -= calcDct.Q2;
                        avialbleQuarters -= 1;
                        (appliedDct, correctiondct) = InnerFunction(remaindct, avialbleQuarters);
                    }
                    else
                    {
                        calcDct.Q2 = appliedDct;
                    }
                }
                if (!balance.Q3.IsCompleted)
                {
                    if (payable.Q3 < appliedDct)
                    {
                        calcDct.Q3 = payable.Q3;
                        remaindct -= calcDct.Q3;
                        avialbleQuarters -= 1;
                        (appliedDct, correctiondct) = InnerFunction(remaindct, avialbleQuarters);
                    }
                    else
                    {
                        calcDct.Q3 = appliedDct;
                    }
                }
                if (!balance.Q4.IsCompleted)
                {

                    if (payable.Q4 < (appliedDct + correctiondct))
                    {
                        calcDct.Q4 = payable.Q4;
                        calcDct.OverPayment = (appliedDct + correctiondct) - payable.Q1;
                    }
                    else
                    {
                        calcDct.Q4 = appliedDct + correctiondct;
                    }


                }


            }
            else
            {

                appliedRate = rates.QuarterDiscount ?? 0m;
                var annualdct = balance.AnnualAmount * rates.QuarterDiscount;

                //var remaindct = annualdct - balance.Discount;
                var remaindct = annualdct;

                var avialbleQuarters = 4;


                (decimal, decimal) InnerFunction(decimal? availabledct, int quarters)
                {
                    decimal? dctq = 0;
                    if (1 <= avialbleQuarters)
                        dctq = availabledct / quarters;

                    appliedDct = Math.Round((decimal)dctq, 2, MidpointRounding.AwayFromZero);

                    correctiondct = Math.Round((decimal)remaindct, 2, MidpointRounding.AwayFromZero) - appliedDct * avialbleQuarters;

                    return (appliedDct, correctiondct);
                }



                (appliedDct, correctiondct) = InnerFunction(remaindct, avialbleQuarters);

                if (!balance.Q1.IsCompleted && !balance.Q1.IsOver)
                {
                    if (payable.Q1 < appliedDct)
                    {
                        calcDct.Q1 = payable.Q1;
                        remaindct -= calcDct.Q1;
                        avialbleQuarters -= 1;
                        (appliedDct, correctiondct) = InnerFunction(remaindct, avialbleQuarters);
                    }
                    else
                    {
                        calcDct.Q1 = appliedDct;
                    }

                }
                else
                {
                    calcDct.Q1 = 0;
                }

                if (!balance.Q2.IsCompleted && !balance.Q2.IsOver)
                {
                    if (payable.Q2 < appliedDct)
                    {
                        calcDct.Q2 = payable.Q2;
                        remaindct -= calcDct.Q2;
                        avialbleQuarters -= 1;
                        (appliedDct, correctiondct) = InnerFunction(remaindct, avialbleQuarters);
                    }
                    else
                    {
                        calcDct.Q2 = appliedDct;
                    }
                }
                else
                {
                    calcDct.Q2 = 0;
                }



                if (!balance.Q3.IsCompleted && !balance.Q3.IsOver)
                {
                    if (payable.Q3 < appliedDct)
                    {
                        calcDct.Q3 = payable.Q3;
                        remaindct -= calcDct.Q3;
                        avialbleQuarters -= 1;
                        (appliedDct, correctiondct) = InnerFunction(remaindct, avialbleQuarters);
                    }
                    else
                    {
                        calcDct.Q3 = appliedDct;
                    }
                }
                else
                {
                    calcDct.Q3 = 0;
                }

                if (!balance.Q4.IsCompleted && !balance.Q4.IsOver)
                {

                    if (payable.Q4 < (appliedDct + correctiondct))
                    {
                        calcDct.Q4 = payable.Q4;
                        calcDct.OverPayment = (appliedDct + correctiondct) - payable.Q1;
                    }
                    else
                    {
                        calcDct.Q4 = appliedDct + correctiondct;
                    }

                }
                else
                {
                    calcDct.Q4 = 0;
                }


            }


            return (appliedRate, appliedDct, correctiondct);
        }



        public HPerBalance PerCalculator(AssessmentBalance balance, AssessmentRates rates, (bool, bool, bool, bool) checkBoxes, int month)
        {

            if (balance != null && balance.Q1 != null && balance.Q2 != null && balance.Q3 != null && balance.Q4 != null && rates != null && month != 0)
            {

                var perBalance = new HPerBalance();
                var perDct = new HPerBalance();

                //var payable = new HAssessmentBalance();


                perBalance.LYWarrant = balance.LYWarrant;
                perBalance.LYArrears = balance.LYArrears;
                perBalance.TYWarrant = balance.TYWarrant;
                perBalance.TYArrears = balance.TYArrears;





                //payable.LYWarrant = balance.LYWarrant;
                //payable.LYArrears = balance.LYArrears;
                //payable.TYWarrant = balance.TYWarrant;
                //payable.TYArrears = balance.TYArrears;


                if (!balance.Q1.IsOver && !balance.Q1.IsCompleted && checkBoxes.Item1)
                {

                    perBalance.Q1 += (balance.Q1.Amount - balance.Q1.ByExcessDeduction - balance.Q1.Paid - balance.Q1.Discount);

                }



                if (!balance.Q2.IsOver && !balance.Q2.IsCompleted && checkBoxes.Item2)
                {
                    perBalance.Q2 += (balance.Q2.Amount - balance.Q2.ByExcessDeduction - balance.Q2.Paid - balance.Q2.Discount);
                }



                if (!balance.Q3.IsOver && !balance.Q3.IsCompleted && checkBoxes.Item3)
                {
                    perBalance.Q3 += (balance.Q3.Amount - balance.Q3.ByExcessDeduction - balance.Q3.Paid - balance.Q3.Discount);
                }



                if (!balance.Q4.IsOver && !balance.Q4.IsCompleted && checkBoxes.Item4)
                {

                    perBalance.Q4 += (balance.Q4.Amount - balance.Q4.ByExcessDeduction - balance.Q4.Paid - balance.Q4.Discount);

                }

                //payable.Total = payable.LYWarrant + payable.LYArrears + payable.TYWarrant + payable.TYArrears + payable.Q1 + payable.Q2 + payable.Q3 + payable.Q4;





                (decimal perRate, decimal x, decimal correctionRemainder) = getDiscount(balance, perBalance, rates, month, checkBoxes, ref perDct);

                //quarters

                // Q1
                if (balance.Q1?.Id != null && !balance.Q1.IsOver && !balance.Q1.IsCompleted && month == 1 && checkBoxes.Item1)
                {
                    perBalance.Q1 -= perDct.Q1;
                    perBalance.DiscountTotal += perDct.Q1;
                }

                // Q2
                if (balance.Q2?.Id != null && !balance.Q2.IsOver && !balance.Q2.IsCompleted && month <= 4 && checkBoxes.Item2)
                {
                    perBalance.Q2 -= perDct.Q2;
                    perBalance.DiscountTotal += perDct.Q2;
                }

                // Q3
                if (balance.Q3?.Id != null && !balance.Q3.IsOver && !balance.Q3.IsCompleted && month <= 7 && checkBoxes.Item3)
                {
                    perBalance.Q3 -= perDct.Q3;
                    perBalance.DiscountTotal += perDct.Q3;
                }

                // Q4
                if (balance.Q4?.Id != null && !balance.Q4.IsOver && !balance.Q4.IsCompleted && month <= 10 && checkBoxes.Item4)
                {
                    perBalance.Q4 -= perDct.Q4;
                    perBalance.DiscountTotal += perDct.Q4;
                }





                perBalance.DiscountRate = perRate;
                perBalance.ExcessPayment = balance.ExcessPayment;

                perBalance.Total = perBalance.LYWarrant + perBalance.LYArrears + perBalance.TYWarrant + perBalance.TYArrears + perBalance.Q1 + perBalance.Q2 + perBalance.Q3 + perBalance.Q4 - perBalance.ExcessPayment;




                return perBalance;


            }
            else
            {
                throw new ArgumentNullException(nameof(balance), "Balance cannot be null.");
            }
        }

        private (decimal, decimal, decimal) getDiscount(AssessmentBalance balance, HPerBalance perBalance, AssessmentRates rates, int month, (bool, bool, bool, bool) checkBoxes, ref HPerBalance perDct)
        {
            decimal ousRate = 0;
            decimal ousDec = 0;
            decimal correctiondct = 0;


            if (month == 1 && checkBoxes.Item1 && checkBoxes.Item2 && checkBoxes.Item3 && checkBoxes.Item4)
            {
                ousRate = rates.AnnualDiscount ?? 0m;
                var annualdct = balance.AnnualAmount * rates.AnnualDiscount;

                var remaindct = annualdct - balance.Discount;

                var avialbleQuarters = 4;
                if (balance.Q1.IsCompleted)
                {
                    avialbleQuarters -= 1;
                }
                if (balance.Q2.IsCompleted)
                {
                    avialbleQuarters -= 1;
                }
                if (balance.Q3.IsCompleted)
                {
                    avialbleQuarters -= 1;
                }
                if (balance.Q4.IsCompleted)
                {
                    avialbleQuarters -= 1;
                }



                (decimal, decimal) InnerFunction(decimal? availabledct, int quarters)
                {
                    decimal? dctq = 0;
                    if (1 <= avialbleQuarters)
                        dctq = availabledct / quarters;

                    ousDec = Math.Round((decimal)dctq, 2, MidpointRounding.AwayFromZero);

                    correctiondct = Math.Round((decimal)remaindct, 2, MidpointRounding.AwayFromZero) - ousDec * avialbleQuarters;

                    return (ousDec, correctiondct);
                }



                (ousDec, correctiondct) = InnerFunction(remaindct, avialbleQuarters);

                if (!balance.Q1.IsCompleted)
                {
                    if (perBalance.Q1 < ousDec)
                    {
                        perDct.Q1 = perBalance.Q1;
                        remaindct -= perDct.Q1;
                        avialbleQuarters -= 1;
                        (ousDec, correctiondct) = InnerFunction(remaindct, avialbleQuarters);
                    }
                    else
                    {
                        perDct.Q1 = ousDec;
                    }

                }

                if (!balance.Q2.IsCompleted)
                {
                    if (perBalance.Q2 < ousDec)
                    {
                        perDct.Q2 = perBalance.Q2;
                        remaindct -= perDct.Q2;
                        avialbleQuarters -= 1;
                        (ousDec, correctiondct) = InnerFunction(remaindct, avialbleQuarters);
                    }
                    else
                    {
                        perDct.Q2 = ousDec;
                    }
                }
                if (!balance.Q3.IsCompleted)
                {
                    if (perBalance.Q3 < ousDec)
                    {
                        perDct.Q3 = perBalance.Q3;
                        remaindct -= perDct.Q3;
                        avialbleQuarters -= 1;
                        (ousDec, correctiondct) = InnerFunction(remaindct, avialbleQuarters);
                    }
                    else
                    {
                        perDct.Q3 = ousDec;
                    }
                }
                if (!balance.Q4.IsCompleted)
                {

                    if (perBalance.Q4 < (ousDec + correctiondct))
                    {
                        perDct.Q4 = perBalance.Q4;
                        //calcDct.OverPayment = (appliedDct + correctiondct) - payable.Q1;
                    }
                    else
                    {
                        perDct.Q4 = ousDec + correctiondct;
                    }

                }

            }
            else
            {
                ousRate = rates.QuarterDiscount ?? 0m;
                var annualdct = balance.AnnualAmount * rates.QuarterDiscount;
                var remaindct = annualdct;

                var avialbleQuarters = 4;


                (decimal, decimal) InnerFunction(decimal? availabledct, int quarters)
                {
                    decimal? dctq = 0;
                    if (1 <= avialbleQuarters)
                        dctq = availabledct / quarters;

                    ousDec = Math.Round((decimal)dctq, 2, MidpointRounding.AwayFromZero);

                    correctiondct = Math.Round((decimal)remaindct, 2, MidpointRounding.AwayFromZero) - ousDec * avialbleQuarters;

                    return (ousDec, correctiondct);
                }



                (ousDec, correctiondct) = InnerFunction(remaindct, avialbleQuarters);


                if (!balance.Q1!.IsCompleted && !balance.Q1.IsOver)
                {
                    if (perBalance.Q1 < ousDec)
                    {
                        perDct.Q1 = perBalance.Q1;
                        remaindct -= perDct.Q1;
                        avialbleQuarters -= 1;
                        (ousDec, correctiondct) = InnerFunction(remaindct, avialbleQuarters);
                    }
                    else
                    {
                        perDct.Q1 = ousDec;
                    }

                }
                else
                {
                    perDct.Q1 = 0;
                }

                if (!balance.Q2!.IsCompleted && !balance.Q2.IsOver)
                {
                    if (perBalance.Q2 < ousDec)
                    {
                        perDct.Q2 = perBalance.Q2;
                        remaindct -= perDct.Q2;
                        avialbleQuarters -= 1;
                        (ousDec, correctiondct) = InnerFunction(remaindct, avialbleQuarters);
                    }
                    else
                    {
                        perDct.Q2 = ousDec;
                    }
                }
                else
                {
                    perDct.Q2 = 0;
                }


                if (!balance.Q3!.IsCompleted && !balance.Q3.IsOver)
                {
                    if (perBalance.Q3 < ousDec)
                    {
                        perDct.Q3 = perBalance.Q3;
                        remaindct -= perDct.Q3;
                        avialbleQuarters -= 1;
                        (ousDec, correctiondct) = InnerFunction(remaindct, avialbleQuarters);
                    }
                    else
                    {
                        perDct.Q3 = ousDec;
                    }
                }
                else
                {
                    perDct.Q3 = 0;
                }
                if (!balance.Q4!.IsCompleted && !balance.Q4.IsOver)
                {

                    if (perBalance.Q4 < (ousDec + correctiondct))
                    {
                        perDct.Q4 = perBalance.Q4;
                        //calcDct.OverPayment = (appliedDct + correctiondct) - payable.Q1;
                    }
                    else
                    {
                        perDct.Q4 = ousDec + correctiondct;
                    }

                }
                else
                {
                    perDct.Q4 = 0;
                }
            }


            return (ousRate, ousDec, correctiondct);
        }
    }
}