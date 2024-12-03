using CAT20.Core;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Models.Enums;
using CAT20.Core.Services.AssessmentTax;
using Irony.Parsing;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace CAT20.Services.AssessmentTax
{
    public class AssessmentWarrantService : IAssessmentWarrantService
    {
        private readonly ILogger<AssessmentWarrantService> _logger;
        private readonly IAssessmentTaxUnitOfWork _unitOfWork;


        public AssessmentWarrantService(ILogger<AssessmentWarrantService> logger, IAssessmentTaxUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Q1Warranting(HAssessmentWarrant assessmentWarrant, HTokenClaim token)
        {
            try
            {
                var asmts = await _unitOfWork.Assessments.GetQ1Warranting(assessmentWarrant.SabhaId);

                if (assessmentWarrant.AssessmentIdList!.Count > 0)
                {
                    //var x = asmts.Select(a => a.Id.HasValue && assessmentWarrant.AssessmentIdList.Contains(a.Id.Value));
                    asmts = asmts.Where(a => a.Id.HasValue && !assessmentWarrant.AssessmentIdList.Contains(a.Id.Value));
                }


                foreach (var asmt in asmts)
                {
                    if (asmt.AssessmentPropertyType != null && asmt.AssessmentPropertyType.WarrantRate != null && asmt.AssessmentBalance != null && asmt.AssessmentBalance.Q1 != null)
                    {
                        //decimal precision handling

                        var warrantAmount = (asmt.AssessmentBalance.Q1.Amount * (asmt.AssessmentPropertyType.WarrantRate / 100));

                        asmt.AssessmentBalance.Q1.Warrant = Math.Round((decimal)warrantAmount, 2, MidpointRounding.AwayFromZero);

                        asmt.AssessmentBalance.Q1.WarrantMethod = 1;

                        asmt.AssessmentBalance.TYWarrant += asmt.AssessmentBalance.Q1.Warrant;

                        asmt.AssessmentBalance.Q1.WarrantBy = token.userId;
                        asmt.AssessmentBalance.Q1.WarrantRate = asmt.AssessmentPropertyType.WarrantRate / 100;

                        createTransaction(asmt.AssessmentBalance, AssessmentTransactionsType.WarrantingQ1);

                    }
                }

                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException, "An error occurred during Warranting.{EventType}", "Assessment Warrant Q1");
                return false;
            }
        }


        public async Task<bool> Q2Warranting(HAssessmentWarrant assessmentWarrant, HTokenClaim token)
        {
            try
            {
                var asmts = await _unitOfWork.Assessments.GetQ2Warranting(assessmentWarrant.SabhaId);

                if (assessmentWarrant.AssessmentIdList!.Count > 0)
                {
                    asmts = asmts.Where(a => !assessmentWarrant.AssessmentIdList.Contains(a.Id!.Value));
                }

                foreach (var asmt in asmts)
                {
                    if (asmt.AssessmentPropertyType!.WarrantRate != null && asmt.AssessmentBalance != null && asmt.AssessmentBalance.Q2 != null)
                    {
                        //decimal precision handling

                        var warrantAmount = (asmt.AssessmentBalance.Q2.Amount * (asmt.AssessmentPropertyType.WarrantRate / 100));

                        asmt.AssessmentBalance.Q2.Warrant = Math.Round((decimal)warrantAmount, 2, MidpointRounding.AwayFromZero);
                        asmt.AssessmentBalance.Q2.WarrantMethod = 1;


                        asmt.AssessmentBalance.TYWarrant += asmt.AssessmentBalance.Q2.Warrant;

                        asmt.AssessmentBalance.Q2.WarrantBy = token.userId;
                        asmt.AssessmentBalance.Q2.WarrantRate = asmt.AssessmentPropertyType.WarrantRate / 100;

                        createTransaction(asmt.AssessmentBalance, AssessmentTransactionsType.WarrantingQ2);
                    }
                }

                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException, "An error occurred during Warranting.{EventType}", "Assessment Warrant Q2");
                return false;
            }
        }


        public async Task<bool> Q3Warranting(HAssessmentWarrant assessmentWarrant, HTokenClaim token)
        {
            try
            {
                var asmts = await _unitOfWork.Assessments.GetQ3Warranting(assessmentWarrant.SabhaId);

                if (assessmentWarrant.AssessmentIdList!.Count > 0)
                {
                    asmts = asmts.Where(a => !assessmentWarrant.AssessmentIdList.Contains(a.Id!.Value));
                }


                foreach (var asmt in asmts)
                {
                    if (asmt.AssessmentPropertyType!.WarrantRate != null && asmt.AssessmentBalance != null && asmt.AssessmentBalance.Q3 != null)
                    {
                        //decimal precision handling


                        var warrantAmount = (asmt.AssessmentBalance.Q3.Amount * (asmt.AssessmentPropertyType.WarrantRate / 100));

                        asmt.AssessmentBalance.Q3.Warrant = Math.Round((decimal)warrantAmount, 2, MidpointRounding.AwayFromZero);

                        asmt.AssessmentBalance.Q3.WarrantMethod = 1;

                        asmt.AssessmentBalance.TYWarrant += asmt.AssessmentBalance.Q3.Warrant;

                        asmt.AssessmentBalance.Q3.WarrantBy = token.userId;
                        asmt.AssessmentBalance.Q3.WarrantRate = asmt.AssessmentPropertyType.WarrantRate / 100;

                        createTransaction(asmt.AssessmentBalance, AssessmentTransactionsType.WarrantingQ3);
                    }
                }

                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException, "An error occurred during Warranting.{EventType}", "Assessment Warrant Q3");
                return false;
            }
        }



        public async Task<bool> Q4Warranting(HAssessmentWarrant assessmentWarrant, HTokenClaim token)
        {
            try
            {
                var asmts = await _unitOfWork.Assessments.GetQ4Warranting(assessmentWarrant.SabhaId);


                if (assessmentWarrant.AssessmentIdList!.Count > 0)
                {
                    asmts = asmts.Where(a => assessmentWarrant.AssessmentIdList.Contains(a.Id!.Value));
                }


                foreach (var asmt in asmts)
                {
                    if (asmt.AssessmentPropertyType!.WarrantRate != null && asmt.AssessmentBalance != null && asmt.AssessmentBalanceHistories != null && asmt.AssessmentBalanceHistories!.First().QH4 != null)
                    {
                        //decimal precision handling

                        var warrantAmount = (asmt.AssessmentBalanceHistories!.First().QH4!.Amount * (asmt.AssessmentPropertyType.WarrantRate / 100));

                        asmt.AssessmentBalanceHistories!.First().QH4!.Warrant = Math.Round((decimal)warrantAmount, 2, MidpointRounding.AwayFromZero);
                        asmt.AssessmentBalanceHistories!.First().QH4!.WarrantMethod = 1;

                        asmt.AssessmentBalance.LYWarrant += asmt.AssessmentBalanceHistories!.First().QH4!.Warrant;

                        asmt.AssessmentBalanceHistories!.First().QH4!.WarrantBy = token.userId;
                        asmt.AssessmentBalanceHistories!.First().QH4!.WarrantRate = asmt.AssessmentPropertyType.WarrantRate / 100;

                        createTransaction(asmt.AssessmentBalance, AssessmentTransactionsType.WarrantingQ4);
                    }
                }

                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException, "An error occurred during Warranting.{EventType}", "Assessment Warrant QH4");
                return false;
            }
        }







        /*****
        * 
        * Following use for method Two  For Remaining Balance 
        *  
        * 
        */









        public async Task<bool> Q1WarrantingMethod2(HAssessmentWarrant assessmentWarrant, HTokenClaim token)
        {
            try
            {
                var asmts = await _unitOfWork.Assessments.GetQ1Warranting(assessmentWarrant.SabhaId);

                if (assessmentWarrant.AssessmentIdList!.Count > 0)
                {
                    asmts = asmts.Where(a => !assessmentWarrant.AssessmentIdList.Contains(a.Id!.Value));
                }


                foreach (var asmt in asmts)
                {
                    if (asmt.AssessmentPropertyType != null && asmt.AssessmentPropertyType.WarrantRate != null && asmt.AssessmentBalance != null && asmt.AssessmentBalance.Q1 != null)
                    {
                        //decimal precision handling

                        var warrantAmount = ((asmt.AssessmentBalance.Q1.Amount - asmt.AssessmentBalance.Q1.ByExcessDeduction - asmt.AssessmentBalance.Q1.Paid - asmt.AssessmentBalance.Q1.Discount) * (asmt.AssessmentPropertyType.WarrantRate / 100));

                        asmt.AssessmentBalance.Q1.Warrant = Math.Round((decimal)warrantAmount, 2, MidpointRounding.AwayFromZero);

                        asmt.AssessmentBalance.Q1.WarrantMethod = 2;

                        asmt.AssessmentBalance.TYWarrant += asmt.AssessmentBalance.Q1.Warrant;

                        asmt.AssessmentBalance.Q1.WarrantBy = token.userId;
                        asmt.AssessmentBalance.Q1.WarrantRate = asmt.AssessmentPropertyType.WarrantRate / 100;

                        createTransaction(asmt.AssessmentBalance, AssessmentTransactionsType.WarrantingQ1);
                    }
                }

                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException, "An error occurred during Warranting.{EventType}", "Assessment Warrant Q1 M2");
                return false;
            }
        }

        public async Task<bool> Q2WarrantingMethod2(HAssessmentWarrant assessmentWarrant, HTokenClaim token)
        {
            try
            {
                var asmts = await _unitOfWork.Assessments.GetQ2Warranting(assessmentWarrant.SabhaId);

                if (assessmentWarrant.AssessmentIdList!.Count > 0)
                {
                    asmts = asmts.Where(a => !assessmentWarrant.AssessmentIdList.Contains(a.Id!.Value));
                }

                foreach (var asmt in asmts)
                {
                    if (asmt.AssessmentPropertyType!.WarrantRate != null && asmt.AssessmentBalance != null && asmt.AssessmentBalance.Q2 != null)
                    {
                        //decimal precision handling

                        var warrantAmount = ((asmt.AssessmentBalance.Q2.Amount - asmt.AssessmentBalance.Q2.ByExcessDeduction - asmt.AssessmentBalance.Q2.Paid - asmt.AssessmentBalance.Q2.Discount) * (asmt.AssessmentPropertyType.WarrantRate / 100));

                        asmt.AssessmentBalance.Q2.Warrant = Math.Round((decimal)warrantAmount, 2, MidpointRounding.AwayFromZero);
                        asmt.AssessmentBalance.Q2.WarrantMethod = 2;


                        asmt.AssessmentBalance.TYWarrant += asmt.AssessmentBalance.Q2.Warrant;

                        asmt.AssessmentBalance.Q2.WarrantBy = token.userId;
                        asmt.AssessmentBalance.Q2.WarrantRate = asmt.AssessmentPropertyType.WarrantRate / 100;

                        createTransaction(asmt.AssessmentBalance, AssessmentTransactionsType.WarrantingQ2);
                    }
                }

                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException, "An error occurred during Warranting.{EventType}", "Assessment Warrant Q2 M2");
                return false;
            }
        }


        public async Task<bool> Q3WarrantingMethod2(HAssessmentWarrant assessmentWarrant, HTokenClaim token)
        {
            try
            {
                var asmts = await _unitOfWork.Assessments.GetQ3Warranting(assessmentWarrant.SabhaId);

                if (assessmentWarrant.AssessmentIdList!.Count > 0)
                {
                    asmts = asmts.Where(a => !assessmentWarrant.AssessmentIdList.Contains(a.Id!.Value));
                }


                foreach (var asmt in asmts)
                {
                    if (asmt.AssessmentPropertyType!.WarrantRate != null && asmt.AssessmentBalance != null && asmt.AssessmentBalance.Q3 != null)
                    {
                        //decimal precision handling


                        var warrantAmount = ((asmt.AssessmentBalance.Q3.Amount - asmt.AssessmentBalance.Q3.ByExcessDeduction - asmt.AssessmentBalance.Q3.Paid - asmt.AssessmentBalance.Q3.Discount) * (asmt.AssessmentPropertyType.WarrantRate / 100));

                        asmt.AssessmentBalance.Q3.Warrant = Math.Round((decimal)warrantAmount, 2, MidpointRounding.AwayFromZero);

                        asmt.AssessmentBalance.Q3.WarrantMethod = 2;

                        asmt.AssessmentBalance.TYWarrant += asmt.AssessmentBalance.Q3.Warrant;

                        asmt.AssessmentBalance.Q3.WarrantBy = token.userId;
                        asmt.AssessmentBalance.Q3.WarrantRate = asmt.AssessmentPropertyType.WarrantRate / 100;

                        createTransaction(asmt.AssessmentBalance, AssessmentTransactionsType.WarrantingQ3);
                    }
                }

                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException, "An error occurred during Warranting.{EventType}", "Assessment Warrant Q3 M2");
                return false;
            }
        }

        public async Task<bool> Q4WarrantingMethod2(HAssessmentWarrant assessmentWarrant, HTokenClaim token)
        {
            try
            {
                var asmts = await _unitOfWork.Assessments.GetQ4Warranting(assessmentWarrant.SabhaId);


                if (assessmentWarrant.AssessmentIdList!.Count > 0)
                {
                    asmts = asmts.Where(a => !assessmentWarrant.AssessmentIdList.Contains(a.Id!.Value));
                }


                foreach (var asmt in asmts)
                {
                    if (asmt.AssessmentPropertyType!.WarrantRate != null && asmt.AssessmentBalance != null && asmt.AssessmentBalanceHistories != null && asmt.AssessmentBalanceHistories!.First().QH4 != null)
                    {
                        //decimal precision handling

                        var warrantAmount = ((asmt.AssessmentBalanceHistories!.First().QH4!.Amount - asmt.AssessmentBalanceHistories!.First().QH4!.ByExcessDeduction - asmt.AssessmentBalanceHistories!.First().QH4!.Paid - asmt.AssessmentBalanceHistories!.First().QH4!.Discount) * (asmt.AssessmentPropertyType.WarrantRate / 100));

                        asmt.AssessmentBalanceHistories!.First().QH4!.Warrant = Math.Round((decimal)warrantAmount, 2, MidpointRounding.AwayFromZero);
                        asmt.AssessmentBalanceHistories!.First().QH4!.WarrantMethod = 2;



                        asmt.AssessmentBalance.LYWarrant += asmt.AssessmentBalanceHistories!.First().QH4!.Warrant;


                        asmt.AssessmentBalanceHistories!.First().QH4!.WarrantBy = token.userId;
                        asmt.AssessmentBalanceHistories!.First().QH4!.WarrantRate = asmt.AssessmentPropertyType.WarrantRate / 100;

                        createTransaction(asmt.AssessmentBalance, AssessmentTransactionsType.WarrantingQ4);
                    }
                }

                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException, "An error occurred during Warranting.{EventType}", "Assessment Warrant QH4 M2");
                return false;
            }
        }

        private async void createTransaction(AssessmentBalance assessmentBalance, AssessmentTransactionsType trp)
        {
            var q1 = (assessmentBalance!.Q1 != null && !assessmentBalance!.Q1!.IsCompleted && !assessmentBalance.Q1.IsOver) ? assessmentBalance.Q1!.Amount - assessmentBalance.Q1.ByExcessDeduction - assessmentBalance.Q1.Paid - assessmentBalance.Q1.Discount : 0;
            var q2 = (assessmentBalance!.Q2 != null && !assessmentBalance!.Q2!.IsCompleted && !assessmentBalance.Q2.IsOver) ? assessmentBalance.Q2!.Amount - assessmentBalance.Q2.ByExcessDeduction - assessmentBalance.Q2.Paid - assessmentBalance.Q2.Discount : 0;
            var q3 = (assessmentBalance!.Q3 != null && !assessmentBalance!.Q3!.IsCompleted && !assessmentBalance.Q3.IsOver) ? assessmentBalance.Q3!.Amount - assessmentBalance.Q3.ByExcessDeduction - assessmentBalance.Q3.Paid - assessmentBalance.Q3.Discount : 0;
            var q4 = (assessmentBalance!.Q4 != null && !assessmentBalance!.Q4!.IsCompleted && !assessmentBalance.Q4.IsOver) ? assessmentBalance.Q4!.Amount - assessmentBalance.Q4.ByExcessDeduction - assessmentBalance.Q4.Paid - assessmentBalance.Q4.Discount : 0;
            var transaction = new AssessmentTransaction
            {
                AssessmentId = assessmentBalance.AssessmentId,
                //DateTime = DateTime.Now,
                Type = trp,
                LYArrears = assessmentBalance.LYArrears,
                LYWarrant = assessmentBalance.LYWarrant,
                TYArrears = assessmentBalance.TYArrears,
                TYWarrant = assessmentBalance.TYWarrant,
                RunningOverPay = assessmentBalance.OverPayment,
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
            };

            await _unitOfWork.AssessmentTransactions.AddAsync(transaction);
        }
    }
}
