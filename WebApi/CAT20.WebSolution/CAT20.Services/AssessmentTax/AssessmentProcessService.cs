using CAT20.Core;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Models.AssessmentTax.AssessmentBalanceHistory.AssessmentBalanceHistory;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Services.AssessmentTax;
using ClosedXML.Excel;
using Microsoft.Extensions.Logging;

namespace CAT20.Services.AssessmentTax
{
    public class AssessmentProcessService : IAssessmentProcessService
    {
        private readonly ILogger<AssessmentProcessService> _logger;
        private readonly IAssessmentTaxUnitOfWork _unitOfWork;
        private readonly IAssessmentBalanceService _assessmentBalanceService;

        public AssessmentProcessService(ILogger<AssessmentProcessService> logger, IAssessmentTaxUnitOfWork unitOfWork, IAssessmentBalanceService assessmentBalanceService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _assessmentBalanceService = assessmentBalanceService;
        }

        private async Task<bool> FinalizeProcess(int sbhaId,DateTime processDate)
        {
            try
            {
                var oficces = await  _unitOfWork.Offices.GetAllWithOfficeBySabhaIdAsync(sbhaId);

                bool hasToCommit =false;
                foreach (var office in oficces)
                {
                    if (await _unitOfWork.Assessments.HasAssessmentForOffice(office.ID.Value))
                    {
                        var hasactiveSession = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(office.ID);
                        if (hasactiveSession == null)
                        {
                            var lastSession = await _unitOfWork.Sessions.HasLastSessionMatched(office.ID.Value);

                            if (lastSession != null)
                            {
                                if (!(lastSession.StartAt.Year == processDate.Year && lastSession.StartAt.Month == processDate.Month && lastSession.StartAt.Day == processDate.Day))
                                {

                                    var s = new Session
                                    {
                                        Name = "MIX" + "-" + processDate.ToString("yyyy-MM-dd-HH:mm"),
                                        CreatedAt = processDate,
                                        StartAt = processDate,
                                        CreatedBy = -1,
                                        OfficeId = office.ID.Value,
                                        Module = "MIX",
                                        Active = 0,
                                        Rescue = 0,
                                        RescueStartedAt = processDate,
                                        UpdatedAt = processDate,
                                        UpdatedBy = -1,
                                        StopAt = processDate,
                                    };

                                    s.SetActiveFalse();
                                    await _unitOfWork.Sessions.AddAsync(s);

                                    hasToCommit = true;
                                }
                            }
                            else
                            {
                                throw new Exception("No Session Found.");
                            }
                        }
                        else if((hasactiveSession.StartAt.Year == processDate.Year && hasactiveSession.StartAt.Month == processDate.Month && hasactiveSession.StartAt.Day == processDate.Day))
                        {

                        }
                        else
                        {
                            throw new Exception("Active Session Found.");
                        }
                    }
                }
                if (hasToCommit)
                {
                    await _unitOfWork.CommitAsync();

                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException, "An error occurred during data Finalize processing.{EventType}", "Assessment");
                return false;
            }
        }

        public async Task<(int totalCount, IEnumerable<AssessmentProcess> list)> getAllProcessForSabha(int sabhaId, int pageNo)
        {
            try
            {
                return await _unitOfWork.AssessmentProcesses.getAllProcessForSabha(sabhaId, pageNo);
            }
            catch (Exception ex)
            {
                List<AssessmentProcess> list1 = new List<AssessmentProcess>();
                return (0, list1);
            }
        }

        public async Task<bool> JanuaryEndProcess(AssessmentProcess assessmentProcess, object environment, string _backupFolder)
        {
            if (await FinalizeProcess(assessmentProcess.ShabaId, new DateTime(DateTime.Now.Year, 1, 31, 12, 0, 0)))
            {

                try
                {
                    (var state, var uniqueFileName) = await BackupProcess(assessmentProcess.ShabaId, "january_end", environment, _backupFolder);
                    if (!state)
                    {
                        return false;
                    }

                    var asmtBalces = await _unitOfWork.AssessmentBalances.GetJanuaryEndProcessForSabha(assessmentProcess.ShabaId);
                    var rates = await _unitOfWork.AssessmentRates.GetByIdAsync(1);
                    if (asmtBalces.Count() == 0)
                    {
                        var balforCompleteProcess = await _unitOfWork.AssessmentBalances.GetAllForSabhaToProcess(assessmentProcess.ShabaId);
                        assessmentProcess.Year = balforCompleteProcess.FirstOrDefault()!.Year;
                        assessmentProcess.BackUpKey = uniqueFileName;
                    }
                    else
                    {
                        assessmentProcess.Year = asmtBalces.FirstOrDefault()!.Year;
                        assessmentProcess.BackUpKey = uniqueFileName;
                    }
                    foreach (var assessmentBalance in asmtBalces)
                    {

                        if (assessmentBalance != null && assessmentBalance.Q1 != null && assessmentBalance.Q2 != null && assessmentBalance.Q3 != null && assessmentBalance.Q4 != null && rates != null)
                        {
                            /*****
                                 op => outstandingPayable
                                 payable => payable amount for year
                                 deduction => deduction amount from last year overpayment
                                 paying => paying quantities    
                                 nextbal => next time balance after paying
                                 discount => applied discount
                                 dctRate => discount Rates   
                                 */


                            (var op, var payable, var deduction, var paying, var nextbal, var discount, var dctRate) = _assessmentBalanceService.CalculatePaymentBalance(assessmentBalance, rates, 0, 1, false);

                            assessmentBalance.ByExcessDeduction += deduction.Total;
                            //assessmentBalance.Paid += mxOrder.TotalAmount;
                            assessmentBalance.ExcessPayment = 0;
                            assessmentBalance.DiscountRate = discount.Total > 0 ? dctRate : 0;
                            assessmentBalance.Discount += discount.Total;
                            assessmentBalance.OverPayment += paying.OverPayment += deduction.OverPayment != 0 ? deduction.OverPayment : 0;

                            assessmentBalance.LYWarrant = nextbal.LYWarrant;
                            assessmentBalance.LYArrears = nextbal.LYArrears;

                            assessmentBalance.TYWarrant = nextbal.TYWarrant;
                            assessmentBalance.TYArrears = nextbal.TYArrears;




                            if (!assessmentBalance.Q1.IsOver && !assessmentBalance.Q1.IsCompleted && deduction.Q1 != 0 && paying.Q1 == 0)

                            {
                                assessmentBalance.Q1.ByExcessDeduction += deduction.Q1;
                                assessmentBalance.Q1.Paid += paying.Q1;
                                assessmentBalance.Q1.Discount += discount.Q1;
                                assessmentBalance.Q1.IsCompleted = assessmentBalance.Q1.Amount - assessmentBalance.Q1.ByExcessDeduction - assessmentBalance.Q1.Paid - assessmentBalance.Q1.Discount == 0 ? true : false;
                            }

                            if (!assessmentBalance.Q2.IsOver && !assessmentBalance.Q2.IsCompleted && deduction.Q2 != 0 && paying.Q2 == 0)
                            {
                                assessmentBalance.Q2.ByExcessDeduction += deduction.Q2;
                                assessmentBalance.Q2.Paid += paying.Q2;
                                assessmentBalance.Q2.Discount += discount.Q2;
                                assessmentBalance.Q2.IsCompleted = assessmentBalance.Q2.Amount - assessmentBalance.Q2.ByExcessDeduction - assessmentBalance.Q2.Paid - assessmentBalance.Q2.Discount == 0 ? true : false;
                            }

                            if (!assessmentBalance.Q3.IsOver && !assessmentBalance.Q3.IsCompleted && deduction.Q3 != 0 && paying.Q3 == 0)
                            {
                                assessmentBalance.Q3.ByExcessDeduction += deduction.Q3;
                                assessmentBalance.Q3.Paid += paying.Q3;
                                assessmentBalance.Q3.Discount += discount.Q3;
                                assessmentBalance.Q3.IsCompleted = assessmentBalance.Q3.Amount - assessmentBalance.Q3.ByExcessDeduction - assessmentBalance.Q3.Paid - assessmentBalance.Q3.Discount == 0 ? true : false;
                            }

                            if (!assessmentBalance.Q4.IsOver && !assessmentBalance.Q4.IsCompleted && deduction.Q4 != 0 && paying.Q4 == 0)
                            {
                                assessmentBalance.Q4.ByExcessDeduction += deduction.Q4;
                                assessmentBalance.Q4.Paid += paying.Q4;
                                assessmentBalance.Q4.Discount += discount.Q4;
                                assessmentBalance.Q4.IsCompleted = assessmentBalance.Q4.Amount - assessmentBalance.Q4.ByExcessDeduction - assessmentBalance.Q4.Paid - assessmentBalance.Q4.Discount == 0 ? true : false;
                            }

                            if (assessmentBalance.Q1.IsCompleted && assessmentBalance.Q2.IsCompleted && assessmentBalance.Q3.IsCompleted && assessmentBalance.Q4.IsCompleted)
                            {
                                assessmentBalance.IsCompleted = true;
                            }

                            assessmentBalance.UpdatedBy = assessmentProcess.ActionBy;
                            assessmentBalance.UpdatedAt = DateTime.Now;
                            assessmentBalance.HasTransaction = false;

                            createTransaction(assessmentBalance, AssessmentTransactionsType.DiscountProcess);
                        }
                        else
                        {

                            throw new Exception("Unable To Run Process.");
                        }

                    }

                    assessmentProcess.ProcessType = AssessmentProcessType.January31;
                    await _unitOfWork.AssessmentProcesses.AddAsync(assessmentProcess);
                    await _unitOfWork.CommitAsync();

                    _logger.LogInformation("January End Process Successfully Completed. {EventType}", "Assessment");

                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred during data January End processing.{EventType}", "Assessment");

                    return false;

                }
            }
            else
            {
                return false;
            }

        }

        public async Task<bool> Q1EndProcess(AssessmentProcess assessmentProcess, object environment, string _backupFolder)
        {

            if (await FinalizeProcess(assessmentProcess.ShabaId, new DateTime(DateTime.Now.Year, 3, 31, 12, 0, 0)))
            {

                try
                {

                    (var state, var uniqueFileName) = await BackupProcess(assessmentProcess.ShabaId, "q1_end", environment, _backupFolder);
                    if (!state)
                    {
                        return false;
                    }

                    var asmtBalces = await _unitOfWork.AssessmentBalances.GetQ1EndProcessForSabha(assessmentProcess.ShabaId);
                    var today = DateOnly.FromDateTime(DateTime.Now);
                    var todayTime = DateTime.Now;

                    assessmentProcess.BackUpKey = uniqueFileName;
                    assessmentProcess.Year = asmtBalces.FirstOrDefault()!.Year;




                    foreach (var assessmentBalance in asmtBalces)
                    {

                        if (assessmentBalance != null && assessmentBalance.Q1 != null && assessmentBalance.Q2 != null && assessmentBalance.Q3 != null && assessmentBalance.Q4 != null)
                        {
                            //assessmentBalance.LYWarrant += assessmentBalance.Q1.Warrant;

                            assessmentBalance.Q1.EndDate = todayTime;
                            assessmentBalance.Q1.IsOver = true;
                            assessmentBalance.Q2.StartDate = todayTime;
                            assessmentBalance.CurrentQuarter = 2;

                            assessmentBalance.UpdatedAt = todayTime;
                            assessmentBalance.UpdatedBy = assessmentProcess.ActionBy;


                            if (!assessmentBalance.Q1.IsCompleted)
                            {
                                assessmentBalance.TYArrears += assessmentBalance.Q1.Amount - assessmentBalance.Q1.ByExcessDeduction - assessmentBalance.Q1.Paid - assessmentBalance.Q1.Discount;

                                createTransaction(assessmentBalance, AssessmentTransactionsType.Q1End);
                            }


                        }
                        else
                        {

                            throw new Exception("Unable To Run Process.");
                        }
                    }

                    assessmentProcess.ProcessType = AssessmentProcessType.QuarterOneEnd;
                    await _unitOfWork.AssessmentProcesses.AddAsync(assessmentProcess);

                    await _unitOfWork.CommitAsync();
                    _logger.LogInformation("Q1 End Process Successfully Completed. {EventType}", "Assessment");
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.InnerException, "An error occurred during data Q1 End processing.{EventType}", "Assessment");
                    return false;

                }
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Q2EndProcess(AssessmentProcess assessmentProcess, object environment, string _backupFolder)
        {
            if (await FinalizeProcess(assessmentProcess.ShabaId, new DateTime(DateTime.Now.Year, 6, 30, 12, 0, 0)))
            {

                try
                {

                    (var state, var uniqueFileName) = await BackupProcess(assessmentProcess.ShabaId, "q2_end", environment, _backupFolder);
                    if (!state)
                    {
                        return false;
                    }

                    var asmtBalces = await _unitOfWork.AssessmentBalances.GetQ2EndProcessForSabha(assessmentProcess.ShabaId);
                    var today = DateOnly.FromDateTime(DateTime.Now);
                    var todayTime = DateTime.Now;

                    assessmentProcess.BackUpKey = uniqueFileName;
                    assessmentProcess.Year = asmtBalces.FirstOrDefault()!.Year;

                    foreach (var assessmentBalance in asmtBalces)
                    {

                        if (assessmentBalance != null && assessmentBalance.Q2 != null && assessmentBalance.Q3 != null && assessmentBalance.Q4 != null)
                        {
                            //assessmentBalance.LYWarrant += assessmentBalance.Q1.Warrant;




                            assessmentBalance.Q2.EndDate = todayTime;
                            assessmentBalance.Q2.IsOver = true;
                            assessmentBalance.Q3.StartDate = todayTime;
                            assessmentBalance.CurrentQuarter = 3;

                            assessmentBalance.UpdatedAt = todayTime;
                            assessmentBalance.UpdatedBy = assessmentProcess.ActionBy;

                            if (!assessmentBalance.Q2.IsCompleted)
                            {
                                assessmentBalance.TYArrears += assessmentBalance.Q2.Amount - assessmentBalance.Q2.ByExcessDeduction - assessmentBalance.Q2.Paid - assessmentBalance.Q2.Discount;
                                createTransaction(assessmentBalance, AssessmentTransactionsType.Q2End);
                            }

                        }
                        else
                        {

                            throw new Exception("Unable To Run Process.");
                        }
                    }

                    assessmentProcess.ProcessType = AssessmentProcessType.QuarterTwoEnd;
                    await _unitOfWork.AssessmentProcesses.AddAsync(assessmentProcess);

                    await _unitOfWork.CommitAsync();
                    _logger.LogInformation("Q2 End Process Successfully Completed. {EventType}", "Assessment");
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.InnerException, "An error occurred during data Q2 End processing.{EventType}", "Assessment");
                    return false;

                }
            }
            else
            {
                return false;
            }

        }

        public async Task<bool> Q3EndProcess(AssessmentProcess assessmentProcess, object environment, string _backupFolder)
        {
            if (await FinalizeProcess(assessmentProcess.ShabaId, new DateTime(DateTime.Now.Year, 9, 30, 12, 0, 0)))
            {

                try
                {

                    (var state, var uniqueFileName) = await BackupProcess(assessmentProcess.ShabaId, "q3_end", environment, _backupFolder);
                    if (!state)
                    {
                        return false;
                    }

                    var asmtBalces = await _unitOfWork.AssessmentBalances.GetQ3EndProcessForSabha(assessmentProcess.ShabaId);
                    var today = DateOnly.FromDateTime(DateTime.Now);
                    var todayTime = DateTime.Now;

                    assessmentProcess.BackUpKey = uniqueFileName;
                    assessmentProcess.Year = asmtBalces.FirstOrDefault()!.Year;

                    foreach (var assessmentBalance in asmtBalces)
                    {

                        if (assessmentBalance != null && assessmentBalance.Q3 != null && assessmentBalance.Q4 != null)
                        {
                            //assessmentBalance.LYWarrant += assessmentBalance.Q1.Warrant;




                            assessmentBalance.Q3.EndDate = todayTime;
                            assessmentBalance.Q3.IsOver = true;
                            assessmentBalance.Q4.StartDate = todayTime;
                            assessmentBalance.CurrentQuarter = 4;

                            assessmentBalance.UpdatedAt = todayTime;
                            assessmentBalance.UpdatedBy = assessmentProcess.ActionBy;

                            if (!assessmentBalance.Q3.IsCompleted)
                            {
                                assessmentBalance.TYArrears += assessmentBalance.Q3.Amount - assessmentBalance.Q3.ByExcessDeduction - assessmentBalance.Q3.Paid - assessmentBalance.Q3.Discount;
                                createTransaction(assessmentBalance, AssessmentTransactionsType.Q3End);
                            }
                        }
                        else
                        {

                            throw new Exception("Unable To Run Process.");
                        }
                    }

                    assessmentProcess.ProcessType = AssessmentProcessType.QuarterThreeEnd;
                    await _unitOfWork.AssessmentProcesses.AddAsync(assessmentProcess);

                    await _unitOfWork.CommitAsync();
                    _logger.LogInformation("Q3 End Process Successfully Completed. {EventType}", "Assessment");
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.InnerException, "An error occurred during data Q3 End processing.{EventType}", "Assessment");
                    return false;

                }
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Q4EndProcess(AssessmentProcess assessmentProcess, object environment, string _backupFolder)
        {
            if (await FinalizeProcess(assessmentProcess.ShabaId, new DateTime(DateTime.Now.Year, 3, 31, 12, 0, 0)))
            {
                try
                {

                    (var state, var uniqueFileName) = await BackupProcess(assessmentProcess.ShabaId, "q4_end", environment, _backupFolder);
                    if (!state)
                    {
                        return false;
                    }

                    var asmtBalces = await _unitOfWork.AssessmentBalances.GetQ4EndProcessForSabha(assessmentProcess.ShabaId);
                    var today = DateOnly.FromDateTime(DateTime.Now);
                    var todayTime = DateTime.Now;

                    assessmentProcess.BackUpKey = uniqueFileName;
                    assessmentProcess.Year = asmtBalces.FirstOrDefault()!.Year;

                    foreach (var assessmentBalance in asmtBalces)
                    {

                        if (assessmentBalance != null && assessmentBalance.Q4 != null)
                        {
                            //assessmentBalance.LYWarrant += assessmentBalance.Q1.Warrant;




                            assessmentBalance.Q4.EndDate = todayTime;
                            assessmentBalance.Q4.IsOver = true;
                            //assessmentBalance.Q2.StartDate = todayTime;
                            assessmentBalance.CurrentQuarter = -1;
                            assessmentBalance.UpdatedAt = todayTime;
                            assessmentBalance.UpdatedBy = assessmentProcess.ActionBy;

                            if (!assessmentBalance.Q4.IsCompleted)
                            {
                                assessmentBalance.TYArrears += assessmentBalance.Q4.Amount - assessmentBalance.Q4.ByExcessDeduction - assessmentBalance.Q4.Paid - assessmentBalance.Q4.Discount;
                                createTransaction(assessmentBalance, AssessmentTransactionsType.Q4End);
                            }


                        }
                        else
                        {

                            throw new Exception("Unable To Run Process.");
                        }
                    }

                    assessmentProcess.ProcessType = AssessmentProcessType.QuarterFourEnd;
                    await _unitOfWork.AssessmentProcesses.AddAsync(assessmentProcess);
                    await _unitOfWork.CommitAsync();
                    _logger.LogInformation("Q4 End Process Successfully Completed. {EventType}", "Assessment");
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.InnerException, "An error occurred during data Q4 End processing.{EventType}", "Assessment");
                    return false;

                }
            }
             else
            {
                return false;
            }
        }

        public async Task<bool> YearEndProcess(AssessmentProcess assessmentProcess, object environment, string _backupFolder)

        {

            try
            {
                (var state, var uniqueFileName) = await BackupProcess(assessmentProcess.ShabaId, "y_end", environment, _backupFolder);
                if (!state)
                {
                    return false;
                }
                var asmts = await _unitOfWork.Assessments.GetYearEndProcessForSabha(assessmentProcess.ShabaId);

                var today = DateOnly.FromDateTime(DateTime.Now);
                var todayTime = DateTime.Now;

                List<AssessmentBalancesHistory> balance_histories = new List<AssessmentBalancesHistory>();

                assessmentProcess.BackUpKey = uniqueFileName;
                assessmentProcess.Year = asmts.FirstOrDefault()!.AssessmentBalance!.Year;

                assessmentProcess.Year = asmts.FirstOrDefault()!.AssessmentBalance!.Year;
                foreach (var asmt in asmts)
                {


                    if (asmt.Allocation != null && asmt.AssessmentPropertyType != null && asmt.AssessmentBalance != null && asmt.AssessmentBalance.Q1 != null && asmt.AssessmentBalance.Q2 != null && asmt.AssessmentBalance.Q3 != null && asmt.AssessmentBalance.Q4 != null)
                    {


                        var bal_histoty = new AssessmentBalancesHistory
                        {
                            Id = null,
                            AssessmentId = asmt.Id,
                            //Year
                            Year = asmt.AssessmentBalance.Year,
                            StartDate = asmt.AssessmentBalance.StartDate,
                            EndData = today,

                            ExcessPayment = asmt.AssessmentBalance.ExcessPayment,

                            TYWarrant = asmt.AssessmentBalance.LYWarrant,
                            LYArrears = asmt.AssessmentBalance.LYArrears,

                            LYWarrant = asmt.AssessmentBalance.LYWarrant,
                            TYArrears = asmt.AssessmentBalance.TYArrears,

                            ByExcessDeduction = asmt.AssessmentBalance.ByExcessDeduction,
                            Paid = asmt.AssessmentBalance.Paid,
                            OverPayment = asmt.AssessmentBalance.OverPayment,
                            DiscountRate = asmt.AssessmentBalance.DiscountRate,
                            Discount = asmt.AssessmentBalance.Discount,
                            IsCompleted = asmt.AssessmentBalance.IsCompleted,

                            QH1 = new QH1
                            {
                                Id = null,
                                Amount = asmt.AssessmentBalance.Q1.Amount,
                                ByExcessDeduction = asmt.AssessmentBalance.Q1.ByExcessDeduction,

                                Paid = asmt.AssessmentBalance.Q1.Paid,
                                Discount = asmt.AssessmentBalance.Q1.Discount,
                                Warrant = asmt.AssessmentBalance.Q1.Warrant,

                                WarrantMethod = asmt.AssessmentBalance.Q1.WarrantMethod,
                                StartDate = asmt.AssessmentBalance.Q1.StartDate,
                                EndDate = asmt.AssessmentBalance.Q1.EndDate,
                                IsCompleted = asmt.AssessmentBalance.Q1.IsCompleted,

                                //AssessmentBalanceHistory Id Automatically created

                            },



                            QH2 = new QH2
                            {
                                Id = null,
                                Amount = asmt.AssessmentBalance.Q2.Amount,
                                ByExcessDeduction = asmt.AssessmentBalance.Q2.ByExcessDeduction,

                                Paid = asmt.AssessmentBalance.Q2.Paid,
                                Discount = asmt.AssessmentBalance.Q2.Discount,
                                Warrant = asmt.AssessmentBalance.Q2.Warrant,

                                WarrantMethod = asmt.AssessmentBalance.Q2.WarrantMethod,
                                StartDate = asmt.AssessmentBalance.Q2.StartDate,
                                EndDate = asmt.AssessmentBalance.Q2.EndDate,
                                IsCompleted = asmt.AssessmentBalance.Q2.IsCompleted,

                            },



                            QH3 = new QH3
                            {
                                Id = null,
                                Amount = asmt.AssessmentBalance.Q3.Amount,
                                ByExcessDeduction = asmt.AssessmentBalance.Q3.ByExcessDeduction,

                                Paid = asmt.AssessmentBalance.Q3.Paid,
                                Discount = asmt.AssessmentBalance.Q3.Discount,
                                Warrant = asmt.AssessmentBalance.Q3.Warrant,

                                WarrantMethod = asmt.AssessmentBalance.Q3.WarrantMethod,
                                StartDate = asmt.AssessmentBalance.Q3.StartDate,
                                EndDate = asmt.AssessmentBalance.Q3.EndDate,
                                IsCompleted = asmt.AssessmentBalance.Q3.IsCompleted,

                            },


                            QH4 = new QH4
                            {
                                Id = null,
                                Amount = asmt.AssessmentBalance.Q4.Amount,
                                ByExcessDeduction = asmt.AssessmentBalance.Q4.ByExcessDeduction,

                                Paid = asmt.AssessmentBalance.Q4.Paid,
                                Discount = asmt.AssessmentBalance.Q4.Discount,
                                Warrant = asmt.AssessmentBalance.Q4.Warrant,

                                WarrantMethod = asmt.AssessmentBalance.Q4.WarrantMethod,
                                StartDate = asmt.AssessmentBalance.Q4.StartDate,
                                EndDate = asmt.AssessmentBalance.Q4.EndDate,
                                IsCompleted = asmt.AssessmentBalance.Q4.IsCompleted,

                            },

                            CreatedBy = assessmentProcess.ActionBy,
                            UpdatedBy = -1,


                        };

                        //await _unitOfWork.AssessmentsBalancesHistories.AddAsync(bal_histoty);
                        balance_histories.Add(bal_histoty);

                        asmt.AssessmentBalance.ExcessPayment = asmt.AssessmentBalance.OverPayment;
                        asmt.AssessmentBalance.OverPayment = 0;
                        asmt.AssessmentBalance.LYWarrant += asmt.AssessmentBalance.TYWarrant;
                        asmt.AssessmentBalance.LYArrears += asmt.AssessmentBalance.TYArrears;
                        asmt.AssessmentBalance.TYWarrant = 0;
                        asmt.AssessmentBalance.TYArrears = 0;

                        asmt.AssessmentBalance.Year += 1;
                        asmt.AssessmentBalance.StartDate = today;


                        asmt.AssessmentBalance.ByExcessDeduction = 0;
                        asmt.AssessmentBalance.Paid = 0;
                        asmt.AssessmentBalance.Discount = 0;
                        asmt.AssessmentBalance.DiscountRate = 0;
                        asmt.AssessmentBalance.HasTransaction = false;



                        //decimal precision handling
                        var annualAmmount = (asmt.Allocation.AllocationAmount * (asmt.AssessmentPropertyType.QuarterRate / 100));
                        var qAmount = annualAmmount / 4;

                        var roundedValue = Math.Round((decimal)qAmount, 2, MidpointRounding.AwayFromZero);

                        var remainder = annualAmmount - (roundedValue * 4);

                        asmt.AssessmentBalance.AnnualAmount = annualAmmount;
                        asmt.AssessmentBalance.CurrentQuarter = 1;




                        asmt.AssessmentBalance.Q1.Amount = roundedValue;
                        asmt.AssessmentBalance.Q2.Amount = roundedValue;
                        asmt.AssessmentBalance.Q3.Amount = roundedValue;
                        asmt.AssessmentBalance.Q4.Amount = roundedValue + remainder;

                        asmt.AssessmentBalance.Q1.ByExcessDeduction = 0;
                        asmt.AssessmentBalance.Q2.ByExcessDeduction = 0;
                        asmt.AssessmentBalance.Q3.ByExcessDeduction = 0;
                        asmt.AssessmentBalance.Q4.ByExcessDeduction = 0;

                        asmt.AssessmentBalance.Q1.Paid = 0;
                        asmt.AssessmentBalance.Q2.Paid = 0;
                        asmt.AssessmentBalance.Q3.Paid = 0;
                        asmt.AssessmentBalance.Q4.Paid = 0;

                        asmt.AssessmentBalance.Q1.Discount = 0;
                        asmt.AssessmentBalance.Q2.Discount = 0;
                        asmt.AssessmentBalance.Q3.Discount = 0;
                        asmt.AssessmentBalance.Q4.Discount = 0;

                        asmt.AssessmentBalance.Q1.Warrant = 0;
                        asmt.AssessmentBalance.Q2.Warrant = 0;
                        asmt.AssessmentBalance.Q3.Warrant = 0;
                        asmt.AssessmentBalance.Q4.Warrant = 0;


                        asmt.AssessmentBalance.Q1.StartDate = todayTime;
                        asmt.AssessmentBalance.Q2.StartDate = null;
                        asmt.AssessmentBalance.Q3.StartDate = null;
                        asmt.AssessmentBalance.Q4.StartDate = null;


                        asmt.AssessmentBalance.Q1.WarrantMethod = null;
                        asmt.AssessmentBalance.Q2.WarrantMethod = null;
                        asmt.AssessmentBalance.Q3.WarrantMethod = null;
                        asmt.AssessmentBalance.Q4.WarrantMethod = null;


                        asmt.AssessmentBalance.Q1.EndDate = null;
                        asmt.AssessmentBalance.Q2.EndDate = null;
                        asmt.AssessmentBalance.Q3.EndDate = null;
                        asmt.AssessmentBalance.Q4.EndDate = null;

                        asmt.AssessmentBalance.Q1.IsCompleted = false;
                        asmt.AssessmentBalance.Q2.IsCompleted = false;
                        asmt.AssessmentBalance.Q3.IsCompleted = false;
                        asmt.AssessmentBalance.Q4.IsCompleted = false;


                        asmt.AssessmentBalance.Q1.IsOver = false;
                        asmt.AssessmentBalance.Q2.IsOver = false;
                        asmt.AssessmentBalance.Q3.IsOver = false;
                        asmt.AssessmentBalance.Q4.IsOver = false;


                        createTransaction(asmt.AssessmentBalance, AssessmentTransactionsType.YearEnd);
                    }
                    else
                    {

                        throw new Exception("Unable To Run Process.");
                    }


                }

                int batchSize = 4000;
                for (int i = 0; i < balance_histories.Count(); i += batchSize)
                {
                    var batch = balance_histories.Skip(i).Take(batchSize).ToList();
                    await _unitOfWork.AssessmentsBalancesHistories.AddRangeAsync(batch);
                }

                //assessmentProcess.Year = asmts.FirstOrDefault().AssessmentBalance.Year;
                assessmentProcess.ProcessType = AssessmentProcessType.YearEnd;
                await _unitOfWork.AssessmentProcesses.AddAsync(assessmentProcess);

                await _unitOfWork.CommitAsync();
                _logger.LogInformation("Year End Process Successfully Completed. {EventType}", "Assessment");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException, "An error occurred during data Year End processing.{EventType}", "Assessment");
                return false;
            }
        }

        private async Task<(bool, string)> BackupProcess(int sabhaId, string processType, object environment, string _uploadsFolder)
        {
            try
            {
                var asmtBalces = await _unitOfWork.AssessmentBalances.GetForBackup(sabhaId);

                // Create a new Excel workbook and worksheet
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Sheet1");

                    var protection = worksheet.Protect("YourPassword");

                    worksheet.Cell(1, 1).Value = "assmt_bal_id"; // First row, first column
                    worksheet.Cell(1, 2).Value = "assmt_bal_assmt_id"; 
                    worksheet.Cell(1, 3).Value = "assmt_bal_excess_payment"; 
                    worksheet.Cell(1, 4).Value = "assmt_bal_ly_arrears";
                    worksheet.Cell(1, 5).Value = "assmt_bal_ly_warrant";

                    worksheet.Cell(1, 6).Value = "assmt_bal_ty_arrears";
                    worksheet.Cell(1, 7).Value = "assmt_bal_ty_warrant";
                    worksheet.Cell(1, 8).Value = "assmt_bal_by_excess_deduction";

                    worksheet.Cell(1, 9).Value = "assmt_bal_paid";
                    worksheet.Cell(1, 10).Value = "assmt_bal_over_payment";
                    worksheet.Cell(1, 11).Value = "assmt_bal_discount";
                    worksheet.Cell(1, 12).Value = "assmt_bal_discount_rate";

                    worksheet.Cell(1, 13).Value = "assmt_bal_current_quarter";
                    worksheet.Cell(1, 14).Value = "assmt_bal_is_completed";


                    worksheet.Cell(1, 15).Value = "assmt_q1_id";
                    worksheet.Cell(1, 16).Value = "assmt_q1_paid";
                    worksheet.Cell(1, 17).Value = "assmt_q1_discount";
                    worksheet.Cell(1, 18).Value = "assmt_q1_by_excess_deduction";
                    worksheet.Cell(1, 19).Value = "assmt_q1_start_date";
                    worksheet.Cell(1, 20).Value = "assmt_q1_end_date";
                    worksheet.Cell(1, 21).Value = "assmt_q1_is_completed";
                    worksheet.Cell(1, 22).Value = "assmt_q1_is_over";
                    worksheet.Cell(1, 23).Value = "assmt_q1_assmt_balance_id";

                    worksheet.Cell(1, 24).Value = "assmt_q2_id";
                    worksheet.Cell(1, 25).Value = "assmt_q2_paid";
                    worksheet.Cell(1, 26).Value = "assmt_q2_discount";
                    worksheet.Cell(1, 27).Value = "assmt_q2_by_excess_deduction";
                    worksheet.Cell(1, 28).Value = "assmt_q2_start_date";
                    worksheet.Cell(1, 29).Value = "assmt_q2_end_date";
                    worksheet.Cell(1, 30).Value = "assmt_q2_is_completed";
                    worksheet.Cell(1, 31).Value = "assmt_q2_is_over";
                    worksheet.Cell(1, 32).Value = "assmt_q2_assmt_balance_id";

                    worksheet.Cell(1, 33).Value = "assmt_q3_id";
                    worksheet.Cell(1, 34).Value = "assmt_q3_paid";
                    worksheet.Cell(1, 35).Value = "assmt_q3_discount";
                    worksheet.Cell(1, 36).Value = "assmt_q3_by_excess_deduction";
                    worksheet.Cell(1, 37).Value = "assmt_q3_start_date";
                    worksheet.Cell(1, 38).Value = "assmt_q3_end_date";
                    worksheet.Cell(1, 39).Value = "assmt_q3_is_completed";
                    worksheet.Cell(1, 40).Value = "assmt_q3_is_over";
                    worksheet.Cell(1, 41).Value = "assmt_q3_assmt_balance_id";

                    worksheet.Cell(1, 42).Value = "assmt_q4_id";
                    worksheet.Cell(1, 43).Value = "assmt_q4_paid";
                    worksheet.Cell(1, 44).Value = "assmt_q4_discount";
                    worksheet.Cell(1, 45).Value = "assmt_q4_by_excess_deduction";
                    worksheet.Cell(1, 46).Value = "assmt_q4_start_date";
                    worksheet.Cell(1, 47).Value = "assmt_q4_end_date";
                    worksheet.Cell(1, 48).Value = "assmt_q4_is_completed";
                    worksheet.Cell(1, 49).Value = "assmt_q4_is_over";
                    worksheet.Cell(1, 50).Value = "assmt_q4_assmt_balance_id";





                    int row = 2;
                    foreach (var item in asmtBalces)
                    {
                        worksheet.Cell(row, 1).Value = item.Id; // First row, first column
                        worksheet.Cell(row, 2).Value = item.AssessmentId;
                        worksheet.Cell(row, 3).Value = item.ExcessPayment;
                        worksheet.Cell(row, 4).Value = item.LYArrears;
                        worksheet.Cell(row, 5).Value = item.LYWarrant;

                        worksheet.Cell(row, 6).Value = item.TYArrears;
                        worksheet.Cell(row, 7).Value = item.TYWarrant;
                        worksheet.Cell(row, 8).Value = item.ByExcessDeduction;

                        worksheet.Cell(row, 9).Value = item.Paid;
                        worksheet.Cell(row, 10).Value = item.OverPayment;
                        worksheet.Cell(row, 11).Value = item.Discount;
                        worksheet.Cell(row, 12).Value = item.DiscountRate;

                        worksheet.Cell(row, 13).Value = item.CurrentQuarter;
                        worksheet.Cell(row, 14).Value = item.IsCompleted!.Value;


                        worksheet.Cell(row, 15).Value = item.Q1!.Id;
                        worksheet.Cell(row, 16).Value = item.Q1!.Paid;
                        worksheet.Cell(row, 17).Value = item.Q1!.Discount;
                        worksheet.Cell(row, 18).Value = item.Q1!.ByExcessDeduction;
                        worksheet.Cell(row, 19).Value = item.Q1!.StartDate;
                        worksheet.Cell(row, 20).Value = item.Q1!.EndDate;
                        worksheet.Cell(row, 21).Value = item.Q1!.IsCompleted;
                        worksheet.Cell(row, 22).Value = item.Q1!.IsOver;
                        worksheet.Cell(row, 23).Value = item.Q1!.BalanceId;

                        worksheet.Cell(row, 24).Value = item.Q2!.Id;
                        worksheet.Cell(row, 25).Value = item.Q2!.Paid;
                        worksheet.Cell(row, 26).Value = item.Q2!.Discount;
                        worksheet.Cell(row, 27).Value = item.Q2!.ByExcessDeduction;
                        worksheet.Cell(row, 28).Value = item.Q2!.StartDate;
                        worksheet.Cell(row, 29).Value = item.Q2!.EndDate;
                        worksheet.Cell(row, 30).Value = item.Q2!.IsCompleted;
                        worksheet.Cell(row, 31).Value = item.Q2!.IsOver;
                        worksheet.Cell(row, 32).Value = item.Q2!.BalanceId;

                        worksheet.Cell(row, 33).Value = item.Q3!.Id;
                        worksheet.Cell(row, 34).Value = item.Q3!.Paid;
                        worksheet.Cell(row, 35).Value = item.Q3!.Discount;
                        worksheet.Cell(row, 36).Value = item.Q3!.ByExcessDeduction;
                        worksheet.Cell(row, 37).Value = item.Q3!.StartDate;
                        worksheet.Cell(row, 38).Value = item.Q3!.EndDate;
                        worksheet.Cell(row, 39).Value = item.Q3!.IsCompleted;
                        worksheet.Cell(row, 40).Value = item.Q3!.IsOver;
                        worksheet.Cell(row, 41).Value = item.Q3!.BalanceId;

                        worksheet.Cell(row, 42).Value = item.Q4!.Id;
                        worksheet.Cell(row, 43).Value = item.Q4!.Paid;
                        worksheet.Cell(row, 44).Value = item.Q4!.Discount;
                        worksheet.Cell(row, 45).Value = item.Q4!.ByExcessDeduction;
                        worksheet.Cell(row, 46).Value = item.Q4!.StartDate;
                        worksheet.Cell(row, 47).Value = item.Q4!.EndDate;
                        worksheet.Cell(row, 48).Value = item.Q4!.IsCompleted;
                        worksheet.Cell(row, 49).Value = item.Q4!.IsOver;
                        worksheet.Cell(row, 50).Value = item.Q4!.BalanceId;


                        row++;
                    }

                    worksheet.Column(1).Style.Protection.SetLocked(true);


                    //sheet styles

                    worksheet.CellsUsed().Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                    worksheet.CellsUsed().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                    foreach (IXLColumn column in worksheet.ColumnsUsed())
                    {
                        column.AdjustToContents();
                    }

                    //worksheet.Column(1).CellsUsed().Style.Fill.BackgroundColor = XLColor.Gray;
                    worksheet.Column(15).CellsUsed().Style.Fill.BackgroundColor = XLColor.LightGreen;
                    worksheet.Column(24).CellsUsed().Style.Fill.BackgroundColor = XLColor.LightBlue;
                    worksheet.Column(33).CellsUsed().Style.Fill.BackgroundColor = XLColor.LightCoral;
                    worksheet.Column(42).CellsUsed().Style.Fill.BackgroundColor = XLColor.LightSlateGray;




                    // Generate a unique file name
                    var uniqueFileName = $"asmt_bal_bkp_{processType}_{sabhaId}_{DateTime.Now:yyyy_MM_dd_HH_mm}.xlsx";


                    if (!Directory.Exists(_uploadsFolder))
                    {
                        Directory.CreateDirectory(_uploadsFolder);
                    }

                    var filePath = Path.Combine(_uploadsFolder, "Assessment Balance", uniqueFileName);
                    workbook.SaveAs(filePath);


                    return (true, uniqueFileName);
                }
            }
            catch (Exception ex)
            {
                return (false, "");
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
                DiscountRate = assessmentBalance.DiscountRate,
            };

            await _unitOfWork.AssessmentTransactions.AddAsync(transaction);
        }


    }
}
