using CAT20.Core;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Models.Enums;
using CAT20.Core.Services.AssessmentTax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.AssessmentTax
{
    public class AssessmentQuarterReportService : IAssessmentQuarterReportService
    {
        private readonly IAssessmentTaxUnitOfWork _unitOfWork;
        public AssessmentQuarterReportService(IAssessmentTaxUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        public async Task<(bool,List<int>,string)> CreateInitialReport(List<int> includeIds)
        {

            List<int> completed = new List<int>();

            try
            {
                var ProceedSbha = await _unitOfWork.AssessmentQuarterReports.GetAllSbahaFinishedQEND(includeIds,AssessmentProcessType.QuarterOneEnd);

               

                foreach (var p in ProceedSbha)
                {

                    var asmts = await _unitOfWork.AssessmentQuarterReports.GetForInitialReport(p.ShabaId);

                    foreach (var asmt in asmts)
                    {

                        

                        var sysAdj = asmt.Transactions!.Where(asmt => asmt.Type == AssessmentTransactionsType.SystemAdjustment).LastOrDefault();
                        var jnlAdj = asmt.Transactions!.Where(asmt => asmt.Type == AssessmentTransactionsType.JournalAdjustment).LastOrDefault();
                        var init = asmt.Transactions!.Where(asmt => asmt.Type == AssessmentTransactionsType.Init).FirstOrDefault();


                        if (sysAdj != null)
                        {
                            var oldReport = new AssessmentQuarterReport
                            {
                                DateTime = sysAdj.DateTime,
                                Year = 2023,
                                QuarterNo = 4,
                                QAmount = 0,
                                M1Paid = 0,
                                M2Paid = 0,
                                M3Paid = sysAdj.RunningOverPay,
                                QWarrant = 0,
                                QDiscount = 0,

                                LYArrears = sysAdj.LYArrears,
                                LYWarrant = sysAdj.LYWarrant,
                                TYArrears = sysAdj.TYArrears,
                                TYWarrant = sysAdj.TYWarrant,
                                AssessmentId = asmt.Id,
                                CreatedBy = -1,
                                CreatedAt = DateTime.Now,
                                UseTransactionsType = AssessmentTransactionsType.SystemAdjustment,

                            };
                            oldReport.RunningBalance =
                                                +oldReport.LYArrears
                                                + oldReport.LYWarrant
                                                + oldReport.TYArrears
                                                + oldReport.TYWarrant
                                                + oldReport.QAmount
                                                + oldReport.QWarrant

                                                - oldReport.QDiscount
                                                - oldReport.M1Paid
                                                - oldReport.M2Paid
                                                - oldReport.M3Paid;



                            if (!await _unitOfWork.AssessmentQuarterReports.HasReportExist(asmt.Id!.Value, oldReport.Year, oldReport.QuarterNo))
                            {
                                await _unitOfWork.AssessmentQuarterReports.AddAsync(oldReport);
                            }
                            //throw new Exception("Test Exception");
                            //await _unitOfWork.CommitAsync();
                        }
                        else if (jnlAdj != null)
                        {
                            var oldReport = new AssessmentQuarterReport
                            {
                                DateTime = jnlAdj.DateTime,
                                Year = 2023,
                                QuarterNo = 4,
                                QAmount = 0,
                                M1Paid = 0,
                                M2Paid = 0,
                                M3Paid = jnlAdj.RunningOverPay,
                                QWarrant = 0,
                                QDiscount = 0,

                                LYArrears = jnlAdj.LYArrears,
                                LYWarrant = jnlAdj.LYWarrant,
                                TYArrears = jnlAdj.TYArrears,
                                TYWarrant = jnlAdj.TYWarrant,
                                AssessmentId = asmt.Id,
                                CreatedBy = -1,
                                CreatedAt = DateTime.Now,
                                UseTransactionsType = AssessmentTransactionsType.JournalAdjustment,

                            };

                            oldReport.RunningBalance =
                                    +oldReport.LYArrears
                                    + oldReport.LYWarrant
                                    + oldReport.TYArrears
                                    + oldReport.TYWarrant
                                    + oldReport.QAmount
                                    + oldReport.QWarrant
                                    - oldReport.QDiscount
                                     - oldReport.M1Paid
                                     - oldReport.M2Paid
                                     - oldReport.M3Paid;


                            if (!await _unitOfWork.AssessmentQuarterReports.HasReportExist(asmt.Id!.Value, oldReport.Year, oldReport.QuarterNo))
                            {
                                await _unitOfWork.AssessmentQuarterReports.AddAsync(oldReport);
                            }

                        }
                        else if (init != null)
                        {
                            if (asmt.AssessmentBalance!.Q1!.StartDate != asmt.AssessmentBalance.Q1.EndDate)
                            {

                                var oldReport = new AssessmentQuarterReport
                                {

                                    DateTime = init.DateTime,
                                    Year = 2023,
                                    QuarterNo = 4,
                                    QAmount = 0,
                                    M1Paid = 0,
                                    M2Paid = 0,
                                    M3Paid = init.RunningOverPay,
                                    QWarrant = 0,
                                    QDiscount = 0,

                                    LYArrears = init.LYArrears,
                                    LYWarrant = init.LYWarrant,
                                    TYArrears = init.TYArrears,
                                    TYWarrant = init.TYWarrant,
                                    AssessmentId = asmt.Id,
                                    CreatedBy = -1,
                                    CreatedAt = DateTime.Now,
                                    UseTransactionsType = AssessmentTransactionsType.Init,

                                };
                                oldReport.RunningBalance =
                                        +oldReport.LYArrears
                                        + oldReport.LYWarrant
                                        + oldReport.TYArrears
                                        + oldReport.TYWarrant
                                        + oldReport.QAmount
                                        + oldReport.QWarrant
                                        - oldReport.QDiscount
                                         - oldReport.M1Paid
                                         - oldReport.M2Paid
                                         - oldReport.M3Paid;


                                if (!await _unitOfWork.AssessmentQuarterReports.HasReportExist(asmt.Id!.Value, oldReport.Year, oldReport.QuarterNo))
                                {

                                    await _unitOfWork.AssessmentQuarterReports.AddAsync(oldReport);
                                }
                            }
                            else
                            {
                                var oldReport = new AssessmentQuarterReport
                                {

                                    DateTime = init.DateTime,
                                    Year = asmt.AssessmentBalance.Year,
                                    QuarterNo = (int)asmt.AssessmentBalance!.CurrentQuarter! - 1,
                                    QAmount = 0,
                                    M1Paid = 0,
                                    M2Paid = 0,
                                    M3Paid = init.RunningOverPay,
                                    QWarrant = 0,
                                    QDiscount = 0,

                                    LYArrears = init.LYArrears,
                                    LYWarrant = init.LYWarrant,
                                    TYArrears = init.TYArrears,
                                    TYWarrant = init.TYWarrant,
                                    AssessmentId = asmt.Id,
                                    CreatedBy = -1,
                                    CreatedAt = DateTime.Now,
                                    UseTransactionsType = AssessmentTransactionsType.Init,

                                };
                                oldReport.RunningBalance =
                                        +oldReport.LYArrears
                                        + oldReport.LYWarrant
                                        + oldReport.TYArrears
                                        + oldReport.TYWarrant
                                        + oldReport.QAmount
                                        + oldReport.QWarrant
                                        - oldReport.QDiscount
                                         - oldReport.M1Paid
                                         - oldReport.M2Paid
                                         - oldReport.M3Paid;

                                if (!await _unitOfWork.AssessmentQuarterReports.HasReportExist(asmt.Id!.Value, oldReport.Year, oldReport.QuarterNo))
                                {
                                    await _unitOfWork.AssessmentQuarterReports.AddAsync(oldReport);
                                }

                                

                            }

                        }




                    }
                    await _unitOfWork.CommitAsync();
                    completed.Add(p.ShabaId);
                }



                return (true,completed,"init succeed");


            }
            catch (Exception ex)
            {
                return (false, completed,ex.InnerException+ex.Message);
            }
        }


        public async Task<(bool, List<int>, string)> CreateReportQ1(List<int> includeIds)
        {
            List<int> completed = new List<int>();
            try
            {
                var ProceedSbha = await _unitOfWork.AssessmentQuarterReports.GetAllSbahaFinishedQEND(includeIds, AssessmentProcessType.QuarterOneEnd);


                foreach (var p in ProceedSbha)
                {


                    var asmts = await _unitOfWork.AssessmentQuarterReports.GetForQ1ReportForSabha(p.ShabaId);


                    foreach (var asmt in asmts)
                    {

                        if (asmt.AssessmentBalance!.Q1 != null && asmt.AssessmentQuarterReport!.Count()>0 && asmt.AssessmentBalance.Q1.StartDate != asmt.AssessmentBalance.Q1.EndDate)
                        {


                            decimal M1 = 0;
                            decimal M2 = 0;
                            decimal M3 = 0;


                            var orders = await _unitOfWork.MixinOrders.GetForAssessmentReport(asmt.Id!.Value);

                            foreach (var order in orders)
                            {
                                if (order.CreatedAt.Month == 1)
                                {
                                    M1 += order.TotalAmount;
                                }
                                else if (order.CreatedAt.Month == 2)
                                {
                                    M2 += order.TotalAmount;
                                }
                                else if (order.CreatedAt.Month == 3)
                                {
                                    M3 += order.TotalAmount;
                                }
                            }


                            var forceJnl = asmt.Transactions!.Where(asmt => asmt.Type == AssessmentTransactionsType.JournalAdjustmentForce).FirstOrDefault();
                            var sysAdjQ1 = asmt.Transactions!.Where(asmt => asmt.Type == AssessmentTransactionsType.SystemAdjustmentQ1).FirstOrDefault();
                            var wrntQ1 = asmt.Transactions!.Where(asmt => asmt.Type == AssessmentTransactionsType.WarrantingQ1).FirstOrDefault();
                            var Qend = asmt.Transactions!.Where(asmt => asmt.Type == AssessmentTransactionsType.Q1End).FirstOrDefault();



                            if (sysAdjQ1 != null)
                            {


                                var newReport = new AssessmentQuarterReport
                                {

                                    //Id = asmtBal.Id,
                                    DateTime = DateTime.Now,
                                    Year = asmt.AssessmentBalance.Year,
                                    QuarterNo = 1,
                                    QAmount = asmt.AssessmentBalance.Q1!.Amount,
                                    M1Paid = M1,
                                    M2Paid = M2,
                                    M3Paid = M3,
                                    QWarrant = asmt.AssessmentBalance.Q1!.Warrant,
                                    QDiscount = asmt.AssessmentBalance.Q1!.Discount,

                                    LYArrears = sysAdjQ1.LYArrears,
                                    LYWarrant = sysAdjQ1.LYWarrant,
                                    TYArrears = sysAdjQ1.TYArrears,
                                    TYWarrant = sysAdjQ1.TYWarrant,
                                    AssessmentId = asmt.Id,
                                    CreatedBy=-1,
                                    CreatedAt=DateTime.Now,
                                    UseTransactionsType = AssessmentTransactionsType.SystemAdjustmentQ1,


                                };

                                newReport.RunningBalance = asmt.AssessmentQuarterReport!.FirstOrDefault()!.RunningBalance + newReport.QAmount + newReport.QWarrant- (newReport.M1Paid + newReport.M2Paid + newReport.M3Paid + newReport.QDiscount);

                                if (!await _unitOfWork.AssessmentQuarterReports.HasReportExist(asmt.Id!.Value, newReport.Year, newReport.QuarterNo))
                                {
                                    await _unitOfWork.AssessmentQuarterReports.AddAsync(newReport);

                                }

                            }
                            else if (forceJnl != null)
                            {


                                var newReport = new AssessmentQuarterReport
                                {

                                    //Id = asmtBal.Id,
                                    DateTime = DateTime.Now,
                                    Year = asmt.AssessmentBalance.Year,
                                    QuarterNo = 1,
                                    QAmount = asmt.AssessmentBalance.Q1!.Amount,
                                    M1Paid = M1,
                                    M2Paid = M2,
                                    M3Paid = M3,
                                    QWarrant = asmt.AssessmentBalance.Q1!.Warrant,
                                    QDiscount = asmt.AssessmentBalance.Q1!.Discount,

                                    LYArrears = forceJnl.LYArrears,
                                    LYWarrant = forceJnl.LYWarrant,
                                    TYArrears = forceJnl.TYArrears,
                                    TYWarrant = forceJnl.TYWarrant,
                                    Adjustment = (decimal)asmt.AssessmentBalance.Q1!.QReportAdjustment!,
                                    AssessmentId = asmt.Id,
                                    CreatedBy = -1,
                                    CreatedAt = DateTime.Now,
                                    UseTransactionsType = AssessmentTransactionsType.SystemAdjustmentQ3,


                                };

                                newReport.RunningBalance = asmt.AssessmentQuarterReport!.FirstOrDefault()!.RunningBalance + newReport.QAmount + newReport.QWarrant + (decimal)asmt.AssessmentBalance.Q1!.QReportAdjustment! - (newReport.M1Paid + newReport.M2Paid + newReport.M3Paid + newReport.QDiscount);
                                if (!await _unitOfWork.AssessmentQuarterReports.HasReportExist(asmt.Id!.Value, newReport.Year, newReport.QuarterNo))
                                {

                                    await _unitOfWork.AssessmentQuarterReports.AddAsync(newReport);

                                }
                            }

                            else if (wrntQ1 != null)
                            {


                                var newReport = new AssessmentQuarterReport
                                {

                                    //Id = asmtBal.Id,
                                    DateTime = DateTime.Now,
                                    Year = asmt.AssessmentBalance.Year,
                                    QuarterNo = 1,
                                    QAmount = asmt.AssessmentBalance.Q1!.Amount,
                                    M1Paid = M1,
                                    M2Paid = M2,
                                    M3Paid = M3,
                                    QWarrant = asmt.AssessmentBalance.Q1!.Warrant,
                                    QDiscount = asmt.AssessmentBalance.Q1!.Discount,

                                    LYArrears = wrntQ1.LYArrears,
                                    LYWarrant = wrntQ1.LYWarrant,
                                    TYArrears = wrntQ1.TYArrears,
                                    TYWarrant = wrntQ1.TYWarrant,
                                    AssessmentId = asmt.Id,
                                    CreatedBy=-1,
                                    CreatedAt=DateTime.Now,
                                    UseTransactionsType = AssessmentTransactionsType.WarrantingQ1,


                                };

                                newReport.RunningBalance = asmt.AssessmentQuarterReport!.FirstOrDefault()!.RunningBalance + newReport.QAmount + newReport.QWarrant - (newReport.M1Paid + newReport.M2Paid + newReport.M3Paid + newReport.QDiscount);
                                if (!await _unitOfWork.AssessmentQuarterReports.HasReportExist(asmt.Id!.Value, newReport.Year, newReport.QuarterNo))
                                {
                                    await _unitOfWork.AssessmentQuarterReports.AddAsync(newReport);
                                }

                            }
                            else if (Qend != null)
                            {

                                var newReport = new AssessmentQuarterReport
                                {

                                    //Id = asmtBal.Id,
                                    DateTime = DateTime.Now,
                                    Year = asmt.AssessmentBalance.Year,
                                    QuarterNo = 1,
                                    QAmount = asmt.AssessmentBalance.Q1!.Amount,
                                    M1Paid = M1,
                                    M2Paid = M2,
                                    M3Paid = M3,
                                    QWarrant = asmt.AssessmentBalance.Q1!.Warrant,
                                    QDiscount = asmt.AssessmentBalance.Q1!.Discount,

                                    LYArrears = Qend.LYArrears,
                                    LYWarrant = Qend.LYWarrant,
                                    TYArrears = Qend.TYArrears,
                                    TYWarrant = Qend.TYWarrant,
                                    AssessmentId = asmt.Id,
                                    CreatedBy=-1,
                                    CreatedAt=DateTime.Now,
                                    UseTransactionsType = AssessmentTransactionsType.Q1End,


                                };

                                newReport.RunningBalance = asmt.AssessmentQuarterReport!.FirstOrDefault()!.RunningBalance + newReport.QAmount + newReport.QWarrant - (newReport.M1Paid + newReport.M2Paid + newReport.M3Paid + newReport.QDiscount);

                                if (!await _unitOfWork.AssessmentQuarterReports.HasReportExist(asmt.Id!.Value, newReport.Year, newReport.QuarterNo))
                                {
                                    await _unitOfWork.AssessmentQuarterReports.AddAsync(newReport);
                                }


                            }
                            else
                            {
                                if (asmt.AssessmentBalance.Q1 != null && asmt.AssessmentBalance.Q1.IsCompleted == true)
                                {

                                    var newReport = new AssessmentQuarterReport
                                    {

                                        //Id = asmtBal.Id,
                                        DateTime = DateTime.Now,
                                        Year = asmt.AssessmentBalance.Year,
                                        QuarterNo = 1,
                                        QAmount = asmt.AssessmentBalance.Q1!.Amount,
                                        M1Paid = M1,
                                        M2Paid = M2,
                                        M3Paid = M3,
                                        QWarrant = asmt.AssessmentBalance.Q1!.Warrant,
                                        QDiscount = asmt.AssessmentBalance.Q1!.Discount,

                                        LYArrears = asmt.AssessmentBalance.LYArrears,
                                        LYWarrant = asmt.AssessmentBalance.LYWarrant,
                                        TYArrears = asmt.AssessmentBalance.TYArrears,
                                        TYWarrant = asmt.AssessmentBalance.TYWarrant,
                                        AssessmentId = asmt.Id,
                                        CreatedBy=-1,
                                        CreatedAt=DateTime.Now,
                                        UseTransactionsType = AssessmentTransactionsType.RunningBalance,


                                    };

                                    newReport.RunningBalance = asmt.AssessmentQuarterReport!.FirstOrDefault()!.RunningBalance + newReport.QAmount + newReport.QWarrant - (newReport.M1Paid + newReport.M2Paid + newReport.M3Paid + newReport.QDiscount);

                                    if (!await _unitOfWork.AssessmentQuarterReports.HasReportExist(asmt.Id!.Value, newReport.Year, newReport.QuarterNo))
                                    {
                                        await _unitOfWork.AssessmentQuarterReports.AddAsync(newReport);
                                    }
                                }

                            }


                        }
                        else
                        {
                            Console.WriteLine("Unable to create Report");
                        }
                    }

                    await _unitOfWork.CommitAsync();
                    completed.Add(p.ShabaId);
                }


                return (true, completed,"Succeed");
            }
            catch (Exception ex)
            {

                return (false, completed,ex.InnerException+ex.Message);
            }



        }


        public async Task<(bool, List<int>, string)> CreateReportQ2(List<int> includeIds)
        {
            List<int> completed = new List<int>();
            try
            {
                var ProceedSbha = await _unitOfWork.AssessmentQuarterReports.GetAllSbahaFinishedQEND(includeIds, AssessmentProcessType.QuarterTwoEnd);


                foreach (var p in ProceedSbha)
                {


                    var asmts = await _unitOfWork.AssessmentQuarterReports.GetForQ2ReportForSabha(p.ShabaId);


                    foreach (var asmt in asmts)
                    {

                        if (asmt.AssessmentBalance!.Q2 != null && asmt.AssessmentQuarterReport!.Count() > 0 && asmt.AssessmentBalance.Q2.StartDate != asmt.AssessmentBalance.Q2.EndDate)
                        {


                            decimal M1 = 0;
                            decimal M2 = 0;
                            decimal M3 = 0;


                            var orders = await _unitOfWork.MixinOrders.GetForAssessmentReport(asmt.Id!.Value);

                            foreach (var order in orders)
                            {
                                if (order.CreatedAt.Month == 4)
                                {
                                    M1 += order.TotalAmount;
                                }
                                else if (order.CreatedAt.Month == 5)
                                {
                                    M2 += order.TotalAmount;
                                }
                                else if (order.CreatedAt.Month == 6)
                                {
                                    M3 += order.TotalAmount;
                                }
                            }


                            var forceJnl = asmt.Transactions!.Where(asmt => asmt.Type == AssessmentTransactionsType.JournalAdjustmentForce).FirstOrDefault();
                            var sysAdjQ1 = asmt.Transactions!.Where(asmt => asmt.Type == AssessmentTransactionsType.SystemAdjustmentQ2).FirstOrDefault();
                            var wrntQ1 = asmt.Transactions!.Where(asmt => asmt.Type == AssessmentTransactionsType.WarrantingQ2).FirstOrDefault();
                            var Qend = asmt.Transactions!.Where(asmt => asmt.Type == AssessmentTransactionsType.Q2End).FirstOrDefault();



                            if (sysAdjQ1 != null)
                            {


                                var newReport = new AssessmentQuarterReport
                                {

                                    //Id = asmtBal.Id,
                                    DateTime = DateTime.Now,
                                    Year = asmt.AssessmentBalance.Year,
                                    QuarterNo = 2,
                                    QAmount = asmt.AssessmentBalance.Q2!.Amount,
                                    M1Paid = M1,
                                    M2Paid = M2,
                                    M3Paid = M3,
                                    QWarrant = asmt.AssessmentBalance.Q2!.Warrant,
                                    QDiscount = asmt.AssessmentBalance.Q2!.Discount,

                                    LYArrears = sysAdjQ1.LYArrears,
                                    LYWarrant = sysAdjQ1.LYWarrant,
                                    TYArrears = sysAdjQ1.TYArrears,
                                    TYWarrant = sysAdjQ1.TYWarrant,
                                    AssessmentId = asmt.Id,
                                    CreatedBy = -1,
                                    CreatedAt = DateTime.Now,
                                    UseTransactionsType = AssessmentTransactionsType.SystemAdjustmentQ1,


                                };

                                newReport.RunningBalance = asmt.AssessmentQuarterReport!.FirstOrDefault()!.RunningBalance + newReport.QAmount + newReport.QWarrant - (newReport.M1Paid + newReport.M2Paid + newReport.M3Paid + newReport.QDiscount);
                                if (!await _unitOfWork.AssessmentQuarterReports.HasReportExist(asmt.Id!.Value, newReport.Year, newReport.QuarterNo))
                                {

                                    await _unitOfWork.AssessmentQuarterReports.AddAsync(newReport);

                                }
                            }

                            else if (forceJnl != null)
                            {


                                var newReport = new AssessmentQuarterReport
                                {

                                    //Id = asmtBal.Id,
                                    DateTime = DateTime.Now,
                                    Year = asmt.AssessmentBalance.Year,
                                    QuarterNo = 2,
                                    QAmount = asmt.AssessmentBalance.Q2!.Amount,
                                    M1Paid = M1,
                                    M2Paid = M2,
                                    M3Paid = M3,
                                    QWarrant = asmt.AssessmentBalance.Q2!.Warrant,
                                    QDiscount = asmt.AssessmentBalance.Q2!.Discount,

                                    LYArrears = forceJnl.LYArrears,
                                    LYWarrant = forceJnl.LYWarrant,
                                    TYArrears = forceJnl.TYArrears,
                                    TYWarrant = forceJnl.TYWarrant,
                                    Adjustment = (decimal)asmt.AssessmentBalance.Q2!.QReportAdjustment!,
                                    AssessmentId = asmt.Id,
                                    CreatedBy = -1,
                                    CreatedAt = DateTime.Now,
                                    UseTransactionsType = AssessmentTransactionsType.SystemAdjustmentQ3,


                                };

                                newReport.RunningBalance = asmt.AssessmentQuarterReport!.FirstOrDefault()!.RunningBalance + newReport.QAmount + newReport.QWarrant + (decimal)asmt.AssessmentBalance.Q2!.QReportAdjustment! - (newReport.M1Paid + newReport.M2Paid + newReport.M3Paid + newReport.QDiscount);
                                if (!await _unitOfWork.AssessmentQuarterReports.HasReportExist(asmt.Id!.Value, newReport.Year, newReport.QuarterNo))
                                {

                                    await _unitOfWork.AssessmentQuarterReports.AddAsync(newReport);

                                }
                            }

                            else if (wrntQ1 != null)
                            {


                                var newReport = new AssessmentQuarterReport
                                {

                                    //Id = asmtBal.Id,
                                    DateTime = DateTime.Now,
                                    Year = asmt.AssessmentBalance.Year,
                                    QuarterNo = 2,
                                    QAmount = asmt.AssessmentBalance.Q2!.Amount,
                                    M1Paid = M1,
                                    M2Paid = M2,
                                    M3Paid = M3,
                                    QWarrant = asmt.AssessmentBalance.Q2!.Warrant,
                                    QDiscount = asmt.AssessmentBalance.Q2!.Discount,

                                    LYArrears = wrntQ1.LYArrears,
                                    LYWarrant = wrntQ1.LYWarrant,
                                    TYArrears = wrntQ1.TYArrears,
                                    TYWarrant = wrntQ1.TYWarrant,
                                    AssessmentId = asmt.Id,
                                    CreatedBy = -1,
                                    CreatedAt = DateTime.Now,
                                    UseTransactionsType = AssessmentTransactionsType.WarrantingQ1,


                                };

                                newReport.RunningBalance = asmt.AssessmentQuarterReport!.FirstOrDefault()!.RunningBalance + newReport.QAmount + newReport.QWarrant - (newReport.M1Paid + newReport.M2Paid + newReport.M3Paid + newReport.QDiscount);

                                if (!await _unitOfWork.AssessmentQuarterReports.HasReportExist(asmt.Id!.Value, newReport.Year, newReport.QuarterNo))
                                {
                                    await _unitOfWork.AssessmentQuarterReports.AddAsync(newReport);

                                }
                            }
                            else if (Qend != null)
                            {

                                var newReport = new AssessmentQuarterReport
                                {

                                    //Id = asmtBal.Id,
                                    DateTime = DateTime.Now,
                                    Year = asmt.AssessmentBalance.Year,
                                    QuarterNo = 2,
                                    QAmount = asmt.AssessmentBalance.Q2!.Amount,
                                    M1Paid = M1,
                                    M2Paid = M2,
                                    M3Paid = M3,
                                    QWarrant = asmt.AssessmentBalance.Q2!.Warrant,
                                    QDiscount = asmt.AssessmentBalance.Q2!.Discount,

                                    LYArrears = Qend.LYArrears,
                                    LYWarrant = Qend.LYWarrant,
                                    TYArrears = Qend.TYArrears,
                                    TYWarrant = Qend.TYWarrant,
                                    AssessmentId = asmt.Id,
                                    CreatedBy = -1,
                                    CreatedAt = DateTime.Now,
                                    UseTransactionsType = AssessmentTransactionsType.Q2End,


                                };

                                newReport.RunningBalance = asmt.AssessmentQuarterReport!.FirstOrDefault()!.RunningBalance + newReport.QAmount + newReport.QWarrant - (newReport.M1Paid + newReport.M2Paid + newReport.M3Paid + newReport.QDiscount);

                                if (!await _unitOfWork.AssessmentQuarterReports.HasReportExist(asmt.Id!.Value, newReport.Year, newReport.QuarterNo))
                                {
                                    await _unitOfWork.AssessmentQuarterReports.AddAsync(newReport);


                                }
                            }
                            else
                            {
                                if (asmt.AssessmentBalance.Q2 != null && asmt.AssessmentBalance.Q2.IsCompleted == true)
                                {

                                    var newReport = new AssessmentQuarterReport
                                    {

                                        //Id = asmtBal.Id,
                                        DateTime = DateTime.Now,
                                        Year = asmt.AssessmentBalance.Year,
                                        QuarterNo = 2,
                                        QAmount = asmt.AssessmentBalance.Q2!.Amount,
                                        M1Paid = M1,
                                        M2Paid = M2,
                                        M3Paid = M3,
                                        QWarrant = asmt.AssessmentBalance.Q2!.Warrant,
                                        QDiscount = asmt.AssessmentBalance.Q2!.Discount,

                                        LYArrears = asmt.AssessmentBalance.LYArrears,
                                        LYWarrant = asmt.AssessmentBalance.LYWarrant,
                                        TYArrears = asmt.AssessmentBalance.TYArrears,
                                        TYWarrant = asmt.AssessmentBalance.TYWarrant,
                                        AssessmentId = asmt.Id,
                                        CreatedBy = -1,
                                        CreatedAt = DateTime.Now,
                                        UseTransactionsType = AssessmentTransactionsType.RunningBalance,


                                    };

                                    newReport.RunningBalance = asmt.AssessmentQuarterReport!.FirstOrDefault()!.RunningBalance + newReport.QAmount + newReport.QWarrant - (newReport.M1Paid + newReport.M2Paid + newReport.M3Paid + newReport.QDiscount);

                                    if (!await _unitOfWork.AssessmentQuarterReports.HasReportExist(asmt.Id!.Value, newReport.Year, newReport.QuarterNo))
                                    {
                                        await _unitOfWork.AssessmentQuarterReports.AddAsync(newReport);
                                    }
                                }

                            }


                        }
                        else
                        {
                            Console.WriteLine("Unable to create Report");
                        }
                    }

                    await _unitOfWork.CommitAsync();
                    completed.Add(p.ShabaId);
                }


                return (true, completed, "Succeed");
            }
            catch (Exception ex)
            {

                return (false, completed, ex.InnerException + ex.Message);
            }



        }


        public async Task<(bool, List<int>, string)> CreateReportQ3(List<int> includeIds)
        {
            List<int> completed = new List<int>();
            try
            {
                var ProceedSbha = await _unitOfWork.AssessmentQuarterReports.GetAllSbahaFinishedQEND(includeIds, AssessmentProcessType.QuarterThreeEnd);


                foreach (var p in ProceedSbha)
                {


                    var asmts = await _unitOfWork.AssessmentQuarterReports.GetForQ3ReportForSabha(p.ShabaId);


                    foreach (var asmt in asmts)
                    {

                        if (asmt.AssessmentBalance!.Q3 != null && asmt.AssessmentQuarterReport!.Count() > 0 && asmt.AssessmentBalance.Q3.StartDate != asmt.AssessmentBalance.Q3.EndDate)
                        {


                            decimal M1 = 0;
                            decimal M2 = 0;
                            decimal M3 = 0;


                            var orders = await _unitOfWork.MixinOrders.GetForAssessmentReport(asmt.Id!.Value);

                            foreach (var order in orders)
                            {
                                if (order.CreatedAt.Month == 7)
                                {
                                    M1 += order.TotalAmount;
                                }
                                else if (order.CreatedAt.Month == 8)
                                {
                                    M2 += order.TotalAmount;
                                }
                                else if (order.CreatedAt.Month == 9)
                                {
                                    M3 += order.TotalAmount;
                                }
                            }

                            var forceJnl = asmt.Transactions!.Where(asmt => asmt.Type == AssessmentTransactionsType.JournalAdjustmentForce).FirstOrDefault();
                            var sysAdjQ3 = asmt.Transactions!.Where(asmt => asmt.Type == AssessmentTransactionsType.SystemAdjustmentQ2).FirstOrDefault();
                            var wrntQ3 = asmt.Transactions!.Where(asmt => asmt.Type == AssessmentTransactionsType.WarrantingQ2).FirstOrDefault();
                            var Qend = asmt.Transactions!.Where(asmt => asmt.Type == AssessmentTransactionsType.Q3End).FirstOrDefault();



                            if (sysAdjQ3 != null)
                            {


                                var newReport = new AssessmentQuarterReport
                                {

                                    //Id = asmtBal.Id,
                                    DateTime = DateTime.Now,
                                    Year = asmt.AssessmentBalance.Year,
                                    QuarterNo = 3,
                                    QAmount = asmt.AssessmentBalance.Q3!.Amount,
                                    M1Paid = M1,
                                    M2Paid = M2,
                                    M3Paid = M3,
                                    QWarrant = asmt.AssessmentBalance.Q3!.Warrant,
                                    QDiscount = asmt.AssessmentBalance.Q3!.Discount,

                                    LYArrears = sysAdjQ3.LYArrears,
                                    LYWarrant = sysAdjQ3.LYWarrant,
                                    TYArrears = sysAdjQ3.TYArrears,
                                    TYWarrant = sysAdjQ3.TYWarrant,
                                    AssessmentId = asmt.Id,
                                    CreatedBy = -1,
                                    CreatedAt = DateTime.Now,
                                    UseTransactionsType = AssessmentTransactionsType.SystemAdjustmentQ3,


                                };

                                newReport.RunningBalance = asmt.AssessmentQuarterReport!.FirstOrDefault()!.RunningBalance + newReport.QAmount + newReport.QWarrant - (newReport.M1Paid + newReport.M2Paid + newReport.M3Paid + newReport.QDiscount);
                                if (!await _unitOfWork.AssessmentQuarterReports.HasReportExist(asmt.Id!.Value, newReport.Year, newReport.QuarterNo))
                                {

                                    await _unitOfWork.AssessmentQuarterReports.AddAsync(newReport);

                                }
                            }
                            else if (forceJnl != null)
                            {


                                var newReport = new AssessmentQuarterReport
                                {

                                    //Id = asmtBal.Id,
                                    DateTime = DateTime.Now,
                                    Year = asmt.AssessmentBalance.Year,
                                    QuarterNo = 3,
                                    QAmount = asmt.AssessmentBalance.Q3!.Amount,
                                    M1Paid = M1,
                                    M2Paid = M2,
                                    M3Paid = M3,
                                    QWarrant = asmt.AssessmentBalance.Q3!.Warrant,
                                    QDiscount = asmt.AssessmentBalance.Q3!.Discount,

                                    LYArrears = forceJnl.LYArrears,
                                    LYWarrant = forceJnl.LYWarrant,
                                    TYArrears = forceJnl.TYArrears,
                                    TYWarrant = forceJnl.TYWarrant,
                                    Adjustment = (decimal)asmt.AssessmentBalance.Q3!.QReportAdjustment!,
                                    AssessmentId = asmt.Id,
                                    CreatedBy = -1,
                                    CreatedAt = DateTime.Now,
                                    UseTransactionsType = AssessmentTransactionsType.SystemAdjustmentQ3,


                                };

                                newReport.RunningBalance = asmt.AssessmentQuarterReport!.FirstOrDefault()!.RunningBalance + newReport.QAmount + newReport.QWarrant + (decimal)asmt.AssessmentBalance.Q3!.QReportAdjustment! - (newReport.M1Paid + newReport.M2Paid + newReport.M3Paid + newReport.QDiscount);
                                if (!await _unitOfWork.AssessmentQuarterReports.HasReportExist(asmt.Id!.Value, newReport.Year, newReport.QuarterNo))
                                {

                                    await _unitOfWork.AssessmentQuarterReports.AddAsync(newReport);

                                }
                            }

                            else if (wrntQ3 != null)
                            {


                                var newReport = new AssessmentQuarterReport
                                {

                                    //Id = asmtBal.Id,
                                    DateTime = DateTime.Now,
                                    Year = asmt.AssessmentBalance.Year,
                                    QuarterNo = 3,
                                    QAmount = asmt.AssessmentBalance.Q3!.Amount,
                                    M1Paid = M1,
                                    M2Paid = M2,
                                    M3Paid = M3,
                                    QWarrant = asmt.AssessmentBalance.Q3!.Warrant,
                                    QDiscount = asmt.AssessmentBalance.Q3!.Discount,

                                    LYArrears = wrntQ3.LYArrears,
                                    LYWarrant = wrntQ3.LYWarrant,
                                    TYArrears = wrntQ3.TYArrears,
                                    TYWarrant = wrntQ3.TYWarrant,
                                    AssessmentId = asmt.Id,
                                    CreatedBy = -1,
                                    CreatedAt = DateTime.Now,
                                    UseTransactionsType = AssessmentTransactionsType.WarrantingQ1,


                                };

                                newReport.RunningBalance = asmt.AssessmentQuarterReport!.FirstOrDefault()!.RunningBalance + newReport.QAmount + newReport.QWarrant - (newReport.M1Paid + newReport.M2Paid + newReport.M3Paid + newReport.QDiscount);

                                if (!await _unitOfWork.AssessmentQuarterReports.HasReportExist(asmt.Id!.Value, newReport.Year, newReport.QuarterNo))
                                {
                                    await _unitOfWork.AssessmentQuarterReports.AddAsync(newReport);

                                }
                            }
                            else if (Qend != null)
                            {

                                var newReport = new AssessmentQuarterReport
                                {

                                    //Id = asmtBal.Id,
                                    DateTime = DateTime.Now,
                                    Year = asmt.AssessmentBalance.Year,
                                    QuarterNo = 3,
                                    QAmount = asmt.AssessmentBalance.Q3!.Amount,
                                    M1Paid = M1,
                                    M2Paid = M2,
                                    M3Paid = M3,
                                    QWarrant = asmt.AssessmentBalance.Q3!.Warrant,
                                    QDiscount = asmt.AssessmentBalance.Q3!.Discount,

                                    LYArrears = Qend.LYArrears,
                                    LYWarrant = Qend.LYWarrant,
                                    TYArrears = Qend.TYArrears,
                                    TYWarrant = Qend.TYWarrant,
                                    AssessmentId = asmt.Id,
                                    CreatedBy = -1,
                                    CreatedAt = DateTime.Now,
                                    UseTransactionsType = AssessmentTransactionsType.Q3End,


                                };

                                newReport.RunningBalance = asmt.AssessmentQuarterReport!.FirstOrDefault()!.RunningBalance + newReport.QAmount + newReport.QWarrant - (newReport.M1Paid + newReport.M2Paid + newReport.M3Paid + newReport.QDiscount);

                                if (!await _unitOfWork.AssessmentQuarterReports.HasReportExist(asmt.Id!.Value, newReport.Year, newReport.QuarterNo))
                                {
                                    await _unitOfWork.AssessmentQuarterReports.AddAsync(newReport);


                                }
                            }
                            else
                            {
                                if (asmt.AssessmentBalance.Q3 != null && asmt.AssessmentBalance.Q3.IsCompleted == true)
                                {

                                    var newReport = new AssessmentQuarterReport
                                    {

                                        //Id = asmtBal.Id,
                                        DateTime = DateTime.Now,
                                        Year = asmt.AssessmentBalance.Year,
                                        QuarterNo = 3,
                                        QAmount = asmt.AssessmentBalance.Q3!.Amount,
                                        M1Paid = M1,
                                        M2Paid = M2,
                                        M3Paid = M3,
                                        QWarrant = asmt.AssessmentBalance.Q3!.Warrant,
                                        QDiscount = asmt.AssessmentBalance.Q3!.Discount,

                                        LYArrears = asmt.AssessmentBalance.LYArrears,
                                        LYWarrant = asmt.AssessmentBalance.LYWarrant,
                                        TYArrears = asmt.AssessmentBalance.TYArrears,
                                        TYWarrant = asmt.AssessmentBalance.TYWarrant,
                                        AssessmentId = asmt.Id,
                                        CreatedBy = -1,
                                        CreatedAt = DateTime.Now,
                                        UseTransactionsType = AssessmentTransactionsType.RunningBalance,


                                    };

                                    newReport.RunningBalance = asmt.AssessmentQuarterReport!.FirstOrDefault()!.RunningBalance + newReport.QAmount + newReport.QWarrant - (newReport.M1Paid + newReport.M2Paid + newReport.M3Paid + newReport.QDiscount);

                                    if (!await _unitOfWork.AssessmentQuarterReports.HasReportExist(asmt.Id!.Value, newReport.Year, newReport.QuarterNo))
                                    {
                                        await _unitOfWork.AssessmentQuarterReports.AddAsync(newReport);
                                    }
                                }

                            }


                        }
                        else
                        {
                            Console.WriteLine("Unable to create Report");
                        }
                    }

                    await _unitOfWork.CommitAsync();
                    completed.Add(p.ShabaId);
                }


                return (true, completed, "Succeed");
            }
            catch (Exception ex)
            {

                return (false, completed, ex.InnerException + ex.Message);
            }



        }

        public async Task<(bool, List<int>, string)> CreateReportQ4(List<int> includeIds)
        {
            List<int> completed = new List<int>();
            try
            {
                var ProceedSbha = await _unitOfWork.AssessmentQuarterReports.GetAllSbahaFinishedQEND(includeIds, AssessmentProcessType.QuarterFourEnd);


                foreach (var p in ProceedSbha)
                {


                    var asmts = await _unitOfWork.AssessmentQuarterReports.GetForQ4ReportForSabha(p.ShabaId);


                    foreach (var asmt in asmts)
                    {

                        if (asmt.AssessmentBalance!.Q4 != null && asmt.AssessmentQuarterReport!.Count() > 0 && asmt.AssessmentBalance.Q4.StartDate != asmt.AssessmentBalance.Q4.EndDate)
                        {


                            decimal M1 = 0;
                            decimal M2 = 0;
                            decimal M3 = 0;


                            var orders = await _unitOfWork.MixinOrders.GetForAssessmentReport(asmt.Id!.Value);

                            foreach (var order in orders)
                            {
                                if (order.CreatedAt.Month == 10)
                                {
                                    M1 += order.TotalAmount;
                                }
                                else if (order.CreatedAt.Month == 11)
                                {
                                    M2 += order.TotalAmount;
                                }
                                else if (order.CreatedAt.Month == 12)
                                {
                                    M3 += order.TotalAmount;
                                }
                            }

                            var forceJnl = asmt.Transactions!.Where(asmt => asmt.Type == AssessmentTransactionsType.JournalAdjustmentForce).FirstOrDefault();
                            var sysAdjQ4 = asmt.Transactions!.Where(asmt => asmt.Type == AssessmentTransactionsType.SystemAdjustmentQ4).FirstOrDefault();
                            var wrntQ4 = asmt.Transactions!.Where(asmt => asmt.Type == AssessmentTransactionsType.WarrantingQ4).FirstOrDefault();
                            var Qend = asmt.Transactions!.Where(asmt => asmt.Type == AssessmentTransactionsType.Q4End).FirstOrDefault();



                            if (sysAdjQ4 != null)
                            {


                                var newReport = new AssessmentQuarterReport
                                {

                                    //Id = asmtBal.Id,
                                    DateTime = DateTime.Now,
                                    Year = asmt.AssessmentBalance.Year,
                                    QuarterNo = 4,
                                    QAmount = asmt.AssessmentBalance.Q4!.Amount,
                                    M1Paid = M1,
                                    M2Paid = M2,
                                    M3Paid = M3,
                                    QWarrant = asmt.AssessmentBalance.Q4!.Warrant,
                                    QDiscount = asmt.AssessmentBalance.Q4!.Discount,

                                    LYArrears = sysAdjQ4.LYArrears,
                                    LYWarrant = sysAdjQ4.LYWarrant,
                                    TYArrears = sysAdjQ4.TYArrears,
                                    TYWarrant = sysAdjQ4.TYWarrant,
                                    AssessmentId = asmt.Id,
                                    CreatedBy = -1,
                                    CreatedAt = DateTime.Now,
                                    UseTransactionsType = AssessmentTransactionsType.SystemAdjustmentQ3,


                                };

                                newReport.RunningBalance = asmt.AssessmentQuarterReport!.FirstOrDefault()!.RunningBalance + newReport.QAmount + newReport.QWarrant - (newReport.M1Paid + newReport.M2Paid + newReport.M3Paid + newReport.QDiscount);
                                if (!await _unitOfWork.AssessmentQuarterReports.HasReportExist(asmt.Id!.Value, newReport.Year, newReport.QuarterNo))
                                {

                                    await _unitOfWork.AssessmentQuarterReports.AddAsync(newReport);

                                }
                            }
                            else if (forceJnl != null)
                            {


                                var newReport = new AssessmentQuarterReport
                                {

                                    //Id = asmtBal.Id,
                                    DateTime = DateTime.Now,
                                    Year = asmt.AssessmentBalance.Year,
                                    QuarterNo = 4,
                                    QAmount = asmt.AssessmentBalance.Q4!.Amount,
                                    M1Paid = M1,
                                    M2Paid = M2,
                                    M3Paid = M3,
                                    QWarrant = asmt.AssessmentBalance.Q4!.Warrant,
                                    QDiscount = asmt.AssessmentBalance.Q4!.Discount,

                                    LYArrears = forceJnl.LYArrears,
                                    LYWarrant = forceJnl.LYWarrant,
                                    TYArrears = forceJnl.TYArrears,
                                    TYWarrant = forceJnl.TYWarrant,
                                    Adjustment = (decimal)asmt.AssessmentBalance.Q4!.QReportAdjustment!,
                                    AssessmentId = asmt.Id,
                                    CreatedBy = -1,
                                    CreatedAt = DateTime.Now,
                                    UseTransactionsType = AssessmentTransactionsType.SystemAdjustmentQ3,


                                };

                                newReport.RunningBalance = asmt.AssessmentQuarterReport!.FirstOrDefault()!.RunningBalance + newReport.QAmount + newReport.QWarrant + (decimal)asmt.AssessmentBalance.Q4!.QReportAdjustment! - (newReport.M1Paid + newReport.M2Paid + newReport.M3Paid + newReport.QDiscount);
                                if (!await _unitOfWork.AssessmentQuarterReports.HasReportExist(asmt.Id!.Value, newReport.Year, newReport.QuarterNo))
                                {

                                    await _unitOfWork.AssessmentQuarterReports.AddAsync(newReport);

                                }
                            }

                            else if (wrntQ4 != null)
                            {


                                var newReport = new AssessmentQuarterReport
                                {

                                    //Id = asmtBal.Id,
                                    DateTime = DateTime.Now,
                                    Year = asmt.AssessmentBalance.Year,
                                    QuarterNo = 4,
                                    QAmount = asmt.AssessmentBalance.Q4!.Amount,
                                    M1Paid = M1,
                                    M2Paid = M2,
                                    M3Paid = M3,
                                    QWarrant = asmt.AssessmentBalance.Q4!.Warrant,
                                    QDiscount = asmt.AssessmentBalance.Q4!.Discount,

                                    LYArrears = wrntQ4.LYArrears,
                                    LYWarrant = wrntQ4.LYWarrant,
                                    TYArrears = wrntQ4.TYArrears,
                                    TYWarrant = wrntQ4.TYWarrant,
                                    AssessmentId = asmt.Id,
                                    CreatedBy = -1,
                                    CreatedAt = DateTime.Now,
                                    UseTransactionsType = AssessmentTransactionsType.WarrantingQ1,


                                };

                                newReport.RunningBalance = asmt.AssessmentQuarterReport!.FirstOrDefault()!.RunningBalance + newReport.QAmount + newReport.QWarrant - (newReport.M1Paid + newReport.M2Paid + newReport.M3Paid + newReport.QDiscount);

                                if (!await _unitOfWork.AssessmentQuarterReports.HasReportExist(asmt.Id!.Value, newReport.Year, newReport.QuarterNo))
                                {
                                    await _unitOfWork.AssessmentQuarterReports.AddAsync(newReport);

                                }
                            }
                            else if (Qend != null)
                            {

                                var newReport = new AssessmentQuarterReport
                                {

                                    //Id = asmtBal.Id,
                                    DateTime = DateTime.Now,
                                    Year = asmt.AssessmentBalance.Year,
                                    QuarterNo = 4,
                                    QAmount = asmt.AssessmentBalance.Q4!.Amount,
                                    M1Paid = M1,
                                    M2Paid = M2,
                                    M3Paid = M3,
                                    QWarrant = asmt.AssessmentBalance.Q4!.Warrant,
                                    QDiscount = asmt.AssessmentBalance.Q4!.Discount,

                                    LYArrears = Qend.LYArrears,
                                    LYWarrant = Qend.LYWarrant,
                                    TYArrears = Qend.TYArrears,
                                    TYWarrant = Qend.TYWarrant,
                                    AssessmentId = asmt.Id,
                                    CreatedBy = -1,
                                    CreatedAt = DateTime.Now,
                                    UseTransactionsType = AssessmentTransactionsType.Q3End,


                                };

                                newReport.RunningBalance = asmt.AssessmentQuarterReport!.FirstOrDefault()!.RunningBalance + newReport.QAmount + newReport.QWarrant - (newReport.M1Paid + newReport.M2Paid + newReport.M3Paid + newReport.QDiscount);

                                if (!await _unitOfWork.AssessmentQuarterReports.HasReportExist(asmt.Id!.Value, newReport.Year, newReport.QuarterNo))
                                {
                                    await _unitOfWork.AssessmentQuarterReports.AddAsync(newReport);


                                }
                            }
                            else
                            {
                                if (asmt.AssessmentBalance.Q4 != null && asmt.AssessmentBalance.Q4.IsCompleted == true)
                                {

                                    var newReport = new AssessmentQuarterReport
                                    {

                                        //Id = asmtBal.Id,
                                        DateTime = DateTime.Now,
                                        Year = asmt.AssessmentBalance.Year,
                                        QuarterNo = 4,
                                        QAmount = asmt.AssessmentBalance.Q4!.Amount,
                                        M1Paid = M1,
                                        M2Paid = M2,
                                        M3Paid = M3,
                                        QWarrant = asmt.AssessmentBalance.Q4!.Warrant,
                                        QDiscount = asmt.AssessmentBalance.Q4!.Discount,

                                        LYArrears = asmt.AssessmentBalance.LYArrears,
                                        LYWarrant = asmt.AssessmentBalance.LYWarrant,
                                        TYArrears = asmt.AssessmentBalance.TYArrears,
                                        TYWarrant = asmt.AssessmentBalance.TYWarrant,
                                        AssessmentId = asmt.Id,
                                        CreatedBy = -1,
                                        CreatedAt = DateTime.Now,
                                        UseTransactionsType = AssessmentTransactionsType.RunningBalance,


                                    };

                                    newReport.RunningBalance = asmt.AssessmentQuarterReport!.FirstOrDefault()!.RunningBalance + newReport.QAmount + newReport.QWarrant - (newReport.M1Paid + newReport.M2Paid + newReport.M3Paid + newReport.QDiscount);

                                    if (!await _unitOfWork.AssessmentQuarterReports.HasReportExist(asmt.Id!.Value, newReport.Year, newReport.QuarterNo))
                                    {
                                        await _unitOfWork.AssessmentQuarterReports.AddAsync(newReport);
                                    }
                                }

                            }


                        }
                        else
                        {
                            Console.WriteLine("Unable to create Report");
                        }
                    }

                    await _unitOfWork.CommitAsync();
                    completed.Add(p.ShabaId);
                }


                return (true, completed, "Succeed");
            }
            catch (Exception ex)
            {

                return (false, completed, ex.InnerException + ex.Message);
            }



        }
        public async Task<bool> UpdateAnnulAmount()
        {
            try
            {
                var reports = await _unitOfWork.AssessmentQuarterReports.GetForAnnualAmount();
                foreach(var report in reports)
                {
                   
                    report.AnnualAmount = report.Assessment!.AssessmentBalance!.AnnualAmount;

                }
                await _unitOfWork.CommitAsync();
                return true;

            }catch (Exception ex)
            {
                throw new Exception(ex.Message);

                return false;
            }
        }
    }
}
