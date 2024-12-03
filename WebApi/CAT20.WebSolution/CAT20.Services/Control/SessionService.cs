using CAT20.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Control;
using CAT20.Core.Services.Control;
using CAT20.Core.Models.Common;
using Microsoft.AspNetCore.Identity;
using System.Globalization;
using CAT20.Core.Models.Enums;
using CAT20.Core.Services.AssessmentTax;
using CAT20.Core.Services.FinalAccount;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.ShopRental;
using CAT20.Core.Services.ShopRental;
using CAT20.Core.CustomExceptions;
using CAT20.Services.ShopRental;
using Microsoft.Extensions.Hosting;

namespace CAT20.Services.Control
{
    public class SessionService : ISessionService
    {
        private readonly IControlUnitOfWork _unitOfWork;
        private readonly ICashBookDailyBalanceService _cashBookDailyBalanceService;
        private readonly IVoteLedgerBookDailyBalanceService _voteLedgerBookDailyBalanceService;
        private readonly IAssessmentEndSessionService _assessmentEndSession;
        private readonly IDocumentSequenceNumberService _documentSequenceNumberService;
        private readonly IFinalAccountSequenceNumberService _finalAccountSequenceNumberService;
        private readonly IShopService _shopService;
        private readonly IShopRentalProcessService _shopRentalProcessService;


        public SessionService(IControlUnitOfWork unitOfWork, ICashBookDailyBalanceService cashBookDailyBalanceService, IAssessmentEndSessionService assessmentEndSession, IDocumentSequenceNumberService documentSequenceNumberService,   IFinalAccountSequenceNumberService finalAccountSequenceNumberService, IShopService shopService, IVoteLedgerBookDailyBalanceService voteLedgerBookDailyBalanceService, IShopRentalProcessService shopRentalProcessService)
        {
            _unitOfWork = unitOfWork;
            _cashBookDailyBalanceService = cashBookDailyBalanceService;
            _assessmentEndSession = assessmentEndSession;
            _documentSequenceNumberService = documentSequenceNumberService;

            _shopService = shopService;
            _finalAccountSequenceNumberService = finalAccountSequenceNumberService;
            _voteLedgerBookDailyBalanceService = voteLedgerBookDailyBalanceService;
            _shopRentalProcessService = shopRentalProcessService;   
        }
        public async Task<(bool,string?, Session)> Create(Session newSession, HTokenClaim token)
        {
            try
            {
                if (token.sabhaId != 0) {
                    if(token.IsFinalAccountsEnabled == 1)
                    {
                            /*limit session creation for sub offices */
                        var ofices = await _unitOfWork.Offices.GetAllWithOfficeBySabhaIdAsync(token.sabhaId);
                        foreach (var office in ofices)
                        {

                               if(await _unitOfWork.Sessions.HasActiveSession(office.ID!.Value)){

                                    throw new FinalAccountException($"Active Session Found in {office.NameEnglish} Office!..");
                                }

                                var newSessionByOffice = new Session
                                {
                                    Active = 1,
                                    CreatedAt = newSession.CreatedAt,
                                    CreatedBy = newSession.CreatedBy,
                                    OfficeId = office.ID!.Value,
                                    Module = newSession.Module,
                                    StartAt = newSession.StartAt,
                                    UpdatedAt = newSession.UpdatedAt,
                                    UpdatedBy = newSession.UpdatedBy,
                                    Rescue = newSession.Rescue,
                                    RescueStartedAt = newSession.RescueStartedAt,
                                    StopAt = newSession.StopAt,
                                    Name = newSession.Name,
                                };

                                await _unitOfWork.Sessions.AddAsync(newSessionByOffice);


                                

                            

                            if (await HasAssessmentProcessCompleted(newSessionByOffice, token))
                            {

                            }
                            else
                            {
                                throw new GeneralException("Assessment Process Doesn't Completed");

                            }

                            var todayAgreementEndShopslist = await _unitOfWork.Shop.GetAllAgreementEndedShopsForOfficeAndDate(newSessionByOffice.OfficeId, new DateOnly(newSessionByOffice.StartAt.Year, newSessionByOffice.StartAt.Month, newSessionByOffice.StartAt.Day));

                            if (todayAgreementEndShopslist != null)
                            {
                                foreach (var shop in todayAgreementEndShopslist)
                                {
                                    shop.Status = ShopStatus.Hold;

                                    //await _unitOfWork.CommitAsync();

                                }
                            }

                            /*receipt number generation */
                            if (!await _documentSequenceNumberService.CheckIsExistingAndIfNotCreateSequenceNoForYear(newSession.CreatedAt.Year, newSession.OfficeId, "MIX"))
                            {
                                throw new GeneralException("Unable To Start New Document Sequence Number ");
                            }
                        }

                      


                        if (!await _finalAccountSequenceNumberService.CheckIsExistingAndIfNotCreateSequenceNoForYear(newSession.CreatedAt.Year, token.sabhaId))
                        {
                            throw new FinalAccountException("Unable To Start New Final Account Sequence Numbers ");
                        }
                    }
                    else
                    {
                        await _unitOfWork.Sessions.AddAsync(newSession);

                        /*receipt number generation */

                        if (!await _documentSequenceNumberService.CheckIsExistingAndIfNotCreateSequenceNoForYear(newSession.CreatedAt.Year, newSession.OfficeId, "MIX"))
                        {
                            throw new GeneralException("Unable To Start New Document Sequence Number ");
                        }

                        var todayAgreementEndShopslist = await _unitOfWork.Shop.GetAllAgreementEndedShopsForOfficeAndDate(newSession.OfficeId, new DateOnly(newSession.StartAt.Year, newSession.StartAt.Month, newSession.StartAt.Day));

                        if (todayAgreementEndShopslist != null)
                        {
                            foreach (var shop in todayAgreementEndShopslist)
                            {
                                shop.Status = ShopStatus.Hold;

                                //await _unitOfWork.CommitAsync();

                            }
                        }
                      
                        if (await HasAssessmentProcessCompleted(newSession, token))
                        {
                            //await _unitOfWork.CommitAsync();
                        }
                        else
                        {
                            throw new GeneralException("Assessment Process Doesn't Completed");

                        }

                        //await _unitOfWork.CommitAsync();
                    }


                    //Daily Fine Process
                    var result = await _shopRentalProcessService.DailyFineProcess(newSession, token);

                    //By Daily Rate
                    //By Daily Fixed Amount

                    await _unitOfWork.CommitAsync();
                    return (true, "Session Start Successful", newSession);

                }
                else
                {
                    throw new Exception("Unable Sabha Id Couldn't Find ");
                }

               
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(FinalAccountException) || ex.GetType() == typeof(GeneralException))
                {
                    return (false, ex.Message,new Session());
                }
                else
                {
                    return (false, null,new Session());

                }
            }

            
        }
        public async Task<(bool,string)> EndSession(Session session, Session toupdate)
        {
            //using (var transaction = await _unitOfWork.BeginTransactionAsync())
            //{

                try
                {
                    #region Audit Log

                    //var note = new StringBuilder();
                    //note.Append("Deleted on ");
                    //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                    //note.Append(" by ");
                    //note.Append("System");

                    //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                    //{
                    //    dateTime = DateTime.Now,
                    //    TransactionID = session.ID,
                    //    TransactionName = "Session",
                    //    User = 1,
                    //    Note = note.ToString()
                    //});

                    #endregion


                    session.Active = 0;
                    session.StopAt = toupdate.StopAt;
                    session.UpdatedAt = DateTime.Now;
                    session.Rescue = 0;
                    session.UpdatedBy = toupdate.UpdatedBy;
                    await _assessmentEndSession.EndSessionDisableTransaction(session.OfficeId!);

                    var office = await _unitOfWork.Offices.GetByIdAsync(session.OfficeId);
                    var sabha = await _unitOfWork.Sabhas.GetByIdAsync(office.SabhaID);
                    if (office != null)
                    {

                        if (await _unitOfWork.Assessments.HasAssessmentForOffice(office.ID!.Value))
                        {
                            if (session.StartAt.Month == 1 && session.StartAt.Day == 31)
                            {
                                if (await _unitOfWork.AssessmentProcesses.IsCompetedProcess(office.SabhaID!.Value, session.StartAt.Year, AssessmentProcessType.January31))
                                {
                                    //await _unitOfWork.CommitAsync();
                                }
                                else
                                {
                                    throw new GeneralException("January End Discount Process Doesn't Completed");
                                }

                            }
                            else if (session.StartAt.Month == 3 && session.StartAt.Day == 31)
                            {

                                if (await _unitOfWork.AssessmentProcesses.IsCompetedProcess(office.SabhaID!.Value, session.StartAt.Year, AssessmentProcessType.QuarterOneEnd))
                                {
                                    //await _unitOfWork.CommitAsync();
                                }
                                else
                                {
                                    throw new GeneralException("Quarter One End Process Doesn't Completed");
                                }
                            }
                            else if (session.StartAt.Month == 6 && session.StartAt.Day == 30)
                            {
                                if (await _unitOfWork.AssessmentProcesses.IsCompetedProcess(office.SabhaID!.Value, session.StartAt.Year, AssessmentProcessType.QuarterTwoEnd))
                                {
                                    //await _unitOfWork.CommitAsync();
                                }
                                else
                                {
                                    throw new GeneralException("Quarter Two  Process Doesn't Completed");
                                }
                            }
                            else if (session.StartAt.Month == 9 && session.StartAt.Day == 30)
                            {
                                if (await _unitOfWork.AssessmentProcesses.IsCompetedProcess(office.SabhaID!.Value, session.StartAt.Year, AssessmentProcessType.QuarterThreeEnd))
                                {
                                    //await _unitOfWork.CommitAsync();
                                }
                                else
                                {
                                    throw new GeneralException("Quarter Three  Process Doesn't Completed");
                                }
                            }
                            else if (session.StartAt.Month == 12 && session.StartAt.Day == 31)
                            {
                                if (await _unitOfWork.AssessmentProcesses.IsCompetedProcess(office.SabhaID!.Value, session.StartAt.Year, AssessmentProcessType.QuarterFourEnd) && await _unitOfWork.AssessmentProcesses.IsCompetedProcess(office.SabhaID!.Value, session.StartAt.Year, AssessmentProcessType.YearEnd))
                                {
                                    //await _unitOfWork.CommitAsync();
                                }
                                else
                                {
                                    throw new GeneralException("Quarter Four End And Year End  Process Doesn't Completed");
                                }
                            }
                            //else
                            //{
                            //    await _unitOfWork.CommitAsync();
                            //}

                        }
                        

                        if (sabha?.IsFinalAccountsEnabled == 1)
                        {
                            var createdCashBookSummary = await _cashBookDailyBalanceService.GetTotalsAndCreateDailyCashBookDailyBalances(session.OfficeId, session.Id, session.StartAt, toupdate.UpdatedBy.Value);
                            var createdVoteLedgerSummary = await _voteLedgerBookDailyBalanceService.GetTotalsAndCreateDailyVoteLedgerBookDailyBalances(session.OfficeId, session.Id, session.StartAt, toupdate.UpdatedBy.Value);

                            if (!createdCashBookSummary)
                            {
                                throw new FinalAccountException("Unable to create Cashbook daily summary records");
                            }

                            if (!createdVoteLedgerSummary)
                            {
                                throw new FinalAccountException("Unable to create VoteLedgerBook daily summary records");
                            }
                        }

                        await _unitOfWork.CommitAsync();
                       return (true, "Session End Successful");
                        //transaction.Commit();
                    }
                    else
                    {
                        throw new GeneralException("Unable To Find Office");
                    }
                }
                catch (Exception ex)
                {
                    //transaction.Rollback();
                   return (false, ex is FinalAccountException or GeneralException ? ex.Message : null);
                }
                //_unitOfWork.Sessions.Remove(session);
            //}
        }
        public async Task<IEnumerable<Session>> GetAllActiveSessionsByOffice(int officeId)
        {
            return await _unitOfWork.Sessions.GetAllActiveSessionsByOfficeAsync(officeId);
        }
        public async Task<IEnumerable<Session>> GetAllSessionsByOffice(int officeId)
        {
            return await _unitOfWork.Sessions.GetAllSessionsByOfficeAsync(officeId);
        }

        public async Task<IEnumerable<Session>> GetAllSessionsByOfficeAndModule(int officeId, string module)
        {
            return await _unitOfWork.Sessions.GetAllSessionsByOfficeAndModuleAsync(officeId, module);
        }

        public async Task<IEnumerable<Session>> GetLast10SessionsForOffice(int officeId)
        {
            return await _unitOfWork.Sessions.GetLast10SessionsForOffice(officeId);
        }

        public async Task<Session> GetActiveSessionByOfficeAndModule(int officeId, string module)
        {
            return await _unitOfWork.Sessions.GetActiveSessionByOfficeAndModuleAsync(officeId, module);
        }

        public async Task<Session> GetActiveSessionByOffice(int officeId)
        {
            return await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(officeId);
        }

        public async Task<Session> GetById(int id)
        {
            return await _unitOfWork.Sessions.GetByIdAsync(id);
        }

        public async Task<Session> GetByOfficeAndModule(int officeid, string Module)
        {
            return await _unitOfWork.Sessions.GetByOfficeAndModule(officeid, Module);
        }
        public async Task<Session> GetAnyByOfficeAndModule(int? officeid, string Module)
        {
            return await _unitOfWork.Sessions.GetAnyByOfficeAndModule(officeid, Module);
        }

        public async Task<Session> GetActiveOrLastActiveSessionForOnlinePaymentCurrentMonth(int? officeid, string Module)
        {
            return await _unitOfWork.Sessions.GetActiveOrLastActiveSessionForOnlinePaymentCurrentMonth(officeid, Module);
        }


        public async Task<Session> GetByOfficeModuleAndDate(int officeid, string Module, DateTime date)
        {
            return await _unitOfWork.Sessions.GetByOfficeModuleAndDate(officeid, Module, date);
        }

        public async Task<Session> GetByOfficeAndDate(int officeid, DateTime date)
        {
            return await _unitOfWork.Sessions.GetByOfficeAndDateAsync(officeid, date);
        }
        public async Task<DateTime?> IsRescueSessionThenDate(int sessionId)
        {
            return await _unitOfWork.Sessions.IsRescueSessionThenDate(sessionId);
        }

        public async Task AllowReceiptsForExpiredSession(Session session, Session toupdate)
        {
            try
            {
                #region Audit Log

                //var note = new StringBuilder();
                //note.Append("Deleted on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append("System");

                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = session.ID,
                //    TransactionName = "Session",
                //    User = 1,
                //    Note = note.ToString()
                //});

                #endregion

                session.Rescue = toupdate.Rescue;
                session.RescueStartedAt = toupdate.RescueStartedAt;

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
            }
            //_unitOfWork.Sessions.Remove(session);
        }

        
        private async Task<bool> HasAssessmentProcessCompleted(Session session, HTokenClaim token)
        {
            try
            {




                if (await _unitOfWork.Assessments.HasAssessmentForSabha(token.sabhaId) && await _unitOfWork.Assessments.HasAssessmentForOffice(token.officeId))
                {
                    if (session.StartAt.Month > 1)
                    {
                        if (!await _unitOfWork.AssessmentProcesses.IsCompetedProcess(token.sabhaId, session.StartAt.Year, AssessmentProcessType.January31))
                        {

                            return false;
                            throw new GeneralException("January End Discount Process Doesn't Completed");
                        }

                    }

                    if (session.StartAt.Month > 3)
                    {

                        if (!await _unitOfWork.AssessmentProcesses.IsCompetedProcess(token.sabhaId, session.StartAt.Year, AssessmentProcessType.QuarterOneEnd))
                        {
                            return false;
                            throw new GeneralException("Quarter One End Process Doesn't Completed");
                        }
                    }
                    
                    if (session.StartAt.Month > 6 )
                    {
                        if (!await _unitOfWork.AssessmentProcesses.IsCompetedProcess(token.sabhaId, session.StartAt.Year, AssessmentProcessType.QuarterTwoEnd))
                        {
                            return false;
                            throw new GeneralException("Quarter Two End Process Doesn't Completed");
                        }
                    }
                    
                    if (session.StartAt.Month > 9)
                    {
                        if (!await _unitOfWork.AssessmentProcesses.IsCompetedProcess(token.sabhaId, session.StartAt.Year, AssessmentProcessType.QuarterThreeEnd))
                        {

                            return false;
                            throw new GeneralException("Quarter Three End Process Doesn't Completed");
                        }
                    }
                    
                    if (session.StartAt.Month == 1 && session.StartAt.Year !=2024)
                    {
                        if (await _unitOfWork.AssessmentProcesses.IsCompetedProcess(token.sabhaId, session.StartAt.Year-1, AssessmentProcessType.QuarterFourEnd) && await _unitOfWork.AssessmentProcesses.IsCompetedProcess(token.sabhaId, session.StartAt.Year-1, AssessmentProcessType.YearEnd))
                        {
                            return false;
                            throw new GeneralException("Quarter Four End And Year End  Process Doesn't Completed");
                        }
                    }

                        return true;

                }
                else
                {
                    return true;
                }

            }
            catch (Exception)
            {
                throw;
            }   
        }



        public async Task<Session> GetLastEndedSessionByOfficeAsync(int officeId, string Module)
        {
            return await _unitOfWork.Sessions.GetLastEndedSessionByOfficeAsync(officeId, Module);
        }

        public async Task<IEnumerable<Session>> GetLast2SessionsForOffice(int officeId)
        {
            return await _unitOfWork.Sessions.GetLast2SessionsForOffice(officeId);
        }


        public async Task<IEnumerable<Session>> GetAllSessionsForOfficeByYearMonth(int officeId, int year, int month)
        {
            return await _unitOfWork.Sessions.GetAllSessionsForOfficeByYearMonth(officeId, year, month);
        }


        ////--------------- [Start - modification for shop rental process ]-----
        //private async Task<bool> HasShopRentalProcessCompleted(Session session, HTokenClaim token)
        //{
        //    try
        //    {
        //        if (await _unitOfWork.ShopRentalBalance.HasShoprentalBalanceForSabha(token.sabhaId) && await _unitOfWork.ShopRentalBalance.HasShoprentalBalanceForOffice(token.officeId))
        //        {
        //            var processConfigForSabha = await _unitOfWork.ShopRentalProcessConfigaration.GetAllForSabha(token.sabhaId);

        //            if (processConfigForSabha != null)
        //            {
        //                if (session.StartAt.Day >= 1)
        //                {
        //                    //monthend process

        //                    //opening balances - Jan, Feb - Monthend, current session - Feb

        //                    //Get last day of prev. session month
        //                    DateTime lastDayOfLastSessionMonth = GetLastDayOfLastMonth(session.StartAt);

        //                    if (!await _unitOfWork.ShopRentalProcess.IsCompetedProcess(token.sabhaId, new DateOnly(lastDayOfLastSessionMonth.Year, lastDayOfLastSessionMonth.Month, lastDayOfLastSessionMonth.Day), ShopRentalProcessType.MonthendProcess))
        //                    {
        //                        return false;

        //                        throw new Exception("Monthend Process Doesn't Completed");
        //                    }
        //                }

        //                if (processConfigForSabha.FirstOrDefault()!.FineRateType.Id == 1)
        //                {
        //                    //daily fine process

        //                    // Get the previous day
        //                    DateTime previousSessionDay = GetPreviousDay(session.StartAt);

        //                    if (!await _unitOfWork.ShopRentalProcess.IsCompetedProcess(token.sabhaId, new DateOnly(previousSessionDay.Year, previousSessionDay.Month, previousSessionDay.Day), ShopRentalProcessType.FineProcess))
        //                    {
        //                        return false;

        //                        throw new Exception("Daily Fine Process Doesn't Completed");
        //                    }
        //                }
        //                else
        //                {
        //                    if (session.StartAt.Day > processConfigForSabha.FirstOrDefault()!.FineDate)
        //                    {
        //                        //monthly fine process
        //                        if (!await _unitOfWork.ShopRentalProcess.IsCompetedProcess(token.sabhaId, new DateOnly(session.StartAt.Year, session.StartAt.Month, processConfigForSabha.FirstOrDefault()!.FineDate), ShopRentalProcessType.FineProcess))
        //                        {
        //                            return false;

        //                            throw new Exception("Monthly Fine Process Doesn't Completed");
        //                        }
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                throw new Exception("Process Config setting for Sabha cannot be null");
        //            }

        //            return true;
        //        }
        //        else
        //        {
        //            return true;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
        ////-------


        ////-------
        //private static DateTime GetLastDayOfLastMonth(DateTime currentDate)
        //{
        //    DateTime firstDayOfCurrentSessionMonth = new DateTime(currentDate.Year, currentDate.Month, 1);

        //    DateTime lastDayOfLastSessionMonth = firstDayOfCurrentSessionMonth.AddDays(-1);

        //    return lastDayOfLastSessionMonth;
        //}
        ////-------


        ////-------
        //private static DateTime GetPreviousDay(DateTime currentDate)
        //{
        //    DateTime previousSessionDay = currentDate.AddDays(-1);

        //    return previousSessionDay;
        //}
        ////--------------- [End - modification for shop rental process ]-------
    }
}