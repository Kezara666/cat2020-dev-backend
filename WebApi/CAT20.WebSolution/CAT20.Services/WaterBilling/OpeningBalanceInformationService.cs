using AutoMapper;
using CAT20.Core;
using CAT20.Core.CustomExceptions;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.Vote;
using CAT20.Core.Models.WaterBilling;
using CAT20.Core.Services.Vote;
using CAT20.Core.Services.WaterBilling;
using Irony.Parsing;
using AppCategory = CAT20.Core.Models.Enums.AppCategory;

namespace CAT20.Services.WaterBilling
{
    public class OpeningBalanceInformationService : IOpeningBalanceInformationService
    {
        private readonly IMapper _mapper;
        private readonly IWaterBillingUnitOfWork _unitOfWork;
        private readonly IVoteBalanceService _voteBalanceService;

        public OpeningBalanceInformationService(IMapper mapper, IWaterBillingUnitOfWork wb_unitOfWork, IVoteBalanceService voteBalanceService)
        {
            _mapper = mapper;
            _unitOfWork = wb_unitOfWork;
            _voteBalanceService = voteBalanceService;
        }

        public async Task<OpeningBalanceInformation> Create(OpeningBalanceInformation obj, HTokenClaim token)
        {

            try
            {

                var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);

                if (session != null)
                {

                    var wc = await _unitOfWork.WaterConnections.GetInfoById(obj.WaterConnectionId);
                    var billingAccount = await _unitOfWork.Partners.GetByIdAsync(wc.BillingId!.Value);
                    var connectioninfo = await _unitOfWork.MeterConnectInfos.GetById(wc.ConnectionId!);

                    var today = DateOnly.FromDateTime(DateTime.Now);

                    if (token.IsFinalAccountsEnabled == 1)
                    {
                        today = DateOnly.FromDateTime(session.StartAt);
                    }

                    if (obj.LastYearArrears > 0)
                    {
                        var lastYearBal = new WaterConnectionBalance
                        {
                            //Id = null,
                            WcPrimaryId = obj.WaterConnectionId,
                            BarCode = $"0000-0000-0000",
                            ConnectionNo = connectioninfo.ConnectionNo,
                            InvoiceNo = " ",
                            Year = (obj.Year - 1),
                            Month = 12,
                            FromDate = new DateOnly((obj.Year - 1), 1, 1),
                            ToDate = new DateOnly((obj.Year - 1), 12, 31),


                            MeterNo = connectioninfo.MeterNo,
                            MeterCondition = wc.ActiveNature!.CType == 1 ? 1 : 0,
                            PreviousMeterReading = 0,
                            ThisMonthMeterReading = 0,

                            WaterCharge = 0,
                            FixedCharge = 0,

                            VATRate = 0,
                            VATAmount = 0,
                            ThisMonthCharge = obj.LastYearArrears,



                            TotalDue = 0,

                            OnTimePaid = 0,
                            LatePaid = 0,
                            Payments = 0,

                            IsCompleted = false,
                            IsFilled = true,
                            IsProcessed = false,


                            LastBillYearMonth = "no data in new system",
                            PrintBalanceBF = 0,
                            PrintLastBalance = 0,
                            PrintBillingDetails = "no data in new system",
                            PrintLastMonthPayments = 0,
                            NoOfPayments = 0,
                            NoOfCancels = 0,


                            //CreatedAt
                            CreatedBy = obj.CreatedBy,

                        };

                        var currentBalWithArrears = new WaterConnectionBalance
                        {
                            //Id = null,
                            WcPrimaryId = obj.WaterConnectionId,
                            BarCode = $"0000-0000-0000",
                            ConnectionNo = connectioninfo.ConnectionNo,
                            InvoiceNo = "",
                            Year = token.IsFinalAccountsEnabled == 1 ? session.StartAt.Year : obj.Year,
                            Month = token.IsFinalAccountsEnabled == 1 ? session.StartAt.Month : obj.Month,
                            FromDate = new DateOnly(obj.Year, 1, 1),
                            ToDate = today,


                            MeterNo = connectioninfo.MeterNo,
                            MeterCondition = 1,
                            PreviousMeterReading = 0,
                            ThisMonthMeterReading = obj.LastMeterReading,

                            WaterCharge = 0,
                            FixedCharge = 0,

                            VATRate = 0,
                            VATAmount = 0,
                            ThisMonthCharge = obj.MonthlyBalance - obj.LastYearArrears,


                            TotalDue = 0,

                            OnTimePaid = 0,
                            LatePaid = 0,
                            Payments = 0,

                            IsCompleted = false,
                            IsFilled = true,
                            IsProcessed = false,


                            LastBillYearMonth = "no data in new system",
                            PrintBalanceBF = 0,
                            PrintLastBalance = 0,
                            PrintBillingDetails = "no data in new system",
                            PrintLastMonthPayments = 0,
                            NoOfPayments = 0,
                            NoOfCancels = 0,


                            CreatedBy = obj.CreatedBy,

                        };

                        await _unitOfWork.Balances.AddAsync(lastYearBal);
                        await _unitOfWork.Balances.AddAsync(currentBalWithArrears);

                        //await _unitOfWork.CommitAsync();
                        obj.BalanceIdForLastYearArrears = lastYearBal.Id;
                        obj.BalanceIdForCurrentBalance = currentBalWithArrears.Id;


                        if (token.IsFinalAccountsEnabled == 1)
                        {

                            await UpdateLedgerAccounts(obj.WaterConnectionId, obj, session, token);


                        }
                        await _unitOfWork.CommitAsync();
                        obj.BalanceIdForLastYearArrears = lastYearBal.Id;
                        obj.BalanceIdForCurrentBalance = currentBalWithArrears.Id;
                        await _unitOfWork.OpeningBalanceInformations.AddAsync(obj);
                        await _unitOfWork.CommitAsync();

                    }
                    else
                    {
                        var currentBal = new WaterConnectionBalance
                        {
                            //Id = null,
                            WcPrimaryId = obj.WaterConnectionId,
                            BarCode = $"0000-0000-0000",
                            ConnectionNo = connectioninfo.ConnectionNo,
                            InvoiceNo = "",
                            Year = token.IsFinalAccountsEnabled == 1 ? session.StartAt.Year : obj.Year,
                            Month = token.IsFinalAccountsEnabled == 1 ? session.StartAt.Month : obj.Month,
                            FromDate = new DateOnly(obj.Year, 1, 1),
                            ToDate = today,


                            MeterNo = connectioninfo.MeterNo,
                            MeterCondition = 1,
                            PreviousMeterReading = 0,
                            ThisMonthMeterReading = obj.LastMeterReading,

                            WaterCharge = 0,
                            FixedCharge = 0,

                            VATRate = 0,
                            VATAmount = 0,
                            ThisMonthCharge = obj.MonthlyBalance > 0 ? obj.MonthlyBalance : 0,


                            TotalDue = 0,

                            OnTimePaid = 0,
                            LatePaid = 0,
                            Payments = 0,

                            IsCompleted = obj.MonthlyBalance < 0 ? true : false,
                            IsFilled = true,
                            IsProcessed = false,

                            OverPay = obj.MonthlyBalance < 0 ? -obj.MonthlyBalance : 0,

                            LastBillYearMonth = "no data in new system",
                            PrintBalanceBF = 0,
                            PrintLastBalance = obj.LastYearArrears,
                            PrintBillingDetails = "no data in new system",
                            PrintLastMonthPayments = 0,
                            NoOfPayments = 0,
                            NoOfCancels = 0,


                            CreatedBy = obj.CreatedBy,

                        };

                        if (obj.MonthlyBalance < 0)
                        {
                            wc.RunningOverPay = Math.Abs((decimal) obj.MonthlyBalance);
                        }
                        else
                        {
                            wc.RunningOverPay = 0m;
                        }

                        await _unitOfWork.Balances.AddAsync(currentBal);
                        //await _unitOfWork.CommitAsync();


                        if (token.IsFinalAccountsEnabled == 1)
                        {

                            await UpdateLedgerAccounts(obj.WaterConnectionId, obj, session, token);


                        }
                        await _unitOfWork.CommitAsync();
                        obj.BalanceIdForCurrentBalance = currentBal.Id;
                        await _unitOfWork.OpeningBalanceInformations.AddAsync(obj);
                        await _unitOfWork.CommitAsync();
                    }




                    //    if (token.IsFinalAccountsEnabled == 1)
                    //    {

                    //        await UpdateLedgerAccounts(obj.WaterConnectionId, session, token);


                    //    }
                    //await _unitOfWork.CommitAsync();
                    //await _unitOfWork.OpeningBalanceInformations.AddAsync(obj);
                    //await _unitOfWork.CommitAsync();

                    return obj;
                }
                else
                {
                    throw new FinalAccountException("Active Session Not Found");
                }





            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message.ToString());
                return new OpeningBalanceInformation();
            }

        }

        public async Task<IEnumerable<OpeningBalanceInformation>> CreateMultiple(List<OpeningBalanceInformation> objs)
        {
            try
            {
                await _unitOfWork.OpeningBalanceInformations.AddRangeAsync(objs);
                await _unitOfWork.CommitAsync();


            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message.ToString());
                return new List<OpeningBalanceInformation>();
            }

            return objs;
        }

        public async Task<OpeningBalanceInformation> GetById(int id)
        {
            var opnbl = await _unitOfWork.OpeningBalanceInformations.GetByIdAsync(id);


            if (opnbl != null)
            {
                var bal = await _unitOfWork.Balances.GetByIdAsync(opnbl.BalanceIdForCurrentBalance!.Value);

                if (bal.IsProcessed == true)
                {
                    opnbl.IsProcessed = true;

                }
            }

            return opnbl;

        }

        public Task<IEnumerable<OpeningBalanceInformation>> GetOpeningBalancesForConnectionIds(List<int?> wcKeyIds)
        {
            return _unitOfWork.OpeningBalanceInformations.GetOpeningBalancesForConnectionIds(wcKeyIds);
        }

        //--------------- [Start - Define new api to filter openBalance data w.r.t. isProcessed] --------
        public Task<OpeningBalanceInformation> GetNotProcessedOpenBalancesByWaterConnectionId(int waterConnectionId)
        {
            return _unitOfWork.OpeningBalanceInformations.GetNotProcessedOpenBalancesByWaterConnectionId(waterConnectionId);
        }

        public Task<IEnumerable<OpeningBalanceInformation>> GetAllNotProcessedOpenBalancesForConnectionIds(List<int?> wcKeyIds)
        {
            return _unitOfWork.OpeningBalanceInformations.GetAllNotProcessedOpenBalancesForConnectionIds(wcKeyIds);
        }

        public Task<IEnumerable<OpeningBalanceInformation>> GetAllProcessedOpenBalancesForConnectionIds(List<int?> wcKeyIds)
        {
            return _unitOfWork.OpeningBalanceInformations.GetAllProcessedOpenBalancesForConnectionIds(wcKeyIds);
        }

        public async Task<(bool, string?)> UpdateNotProcessedOpenBalance(OpeningBalanceInformation obj, HTokenClaim token)
        {

            try
            {

                var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);

                if (session != null)
                {
                    var objToBeUpdated = await _unitOfWork.OpeningBalanceInformations.GetByIdAsync(obj.Id);
                    var wc = await _unitOfWork.WaterConnections.GetInfoById(objToBeUpdated.WaterConnectionId);

                    if (objToBeUpdated != null && wc != null)
                    {



                        var bal = await _unitOfWork.Balances.GetByIdAsync(objToBeUpdated.BalanceIdForCurrentBalance!.Value);

                        var today = DateOnly.FromDateTime(DateTime.Now);
                        if (bal.IsProcessed != true)
                        {
                           

                            if (token.IsFinalAccountsEnabled == 1)
                            {

                                await UpdateLedgerAccountsRollBack(objToBeUpdated.WaterConnectionId, session, token);
                            }
                            if (obj.MonthlyBalance > 0)
                            {


                                wc.RunningOverPay = 0m;



                                if (objToBeUpdated.BalanceIdForLastYearArrears.HasValue && objToBeUpdated.BalanceIdForLastYearArrears != 0)
                                {

                                    var lyearBal = await _unitOfWork.Balances.GetByIdAsync(objToBeUpdated.BalanceIdForLastYearArrears);

                                    lyearBal.Year = (obj.Year - 1);
                                    lyearBal.Month = obj.Month;
                                    lyearBal.FromDate = new DateOnly((obj.Year - 1), 1, 1);
                                    lyearBal.ThisMonthCharge = obj.LastYearArrears;


                                    bal.Year = obj.Year;
                                    bal.Month = obj.Month;
                                    bal.FromDate = new DateOnly(obj.Year, 1, 1);
                                    bal.ThisMonthMeterReading = obj.LastMeterReading;
                                    bal.ThisMonthCharge = obj.MonthlyBalance - obj.LastYearArrears;
                                }
                                else if (obj.LastYearArrears > 0 && !objToBeUpdated.BalanceIdForLastYearArrears.HasValue)
                                {
                                    var meterNo = bal.MeterNo;
                                    var mci_conn_no = bal.ConnectionNo;
                                    _unitOfWork.Balances.Remove(bal);
                                    await _unitOfWork.CommitAsync();

                                    //var wc = await _wb_unitOfWork.WaterConnections.GetByIdAsync(obj.WaterConnectionId);
                                    ////var billingAccount = await _wb_unitOfWork.Partners.GetByIdAsync(wc.BillingId!.Value);
                                    //var connectioninfo = await _wb_unitOfWork.MeterConnectInfos.GetById(wc.ConnectionId!);

                                    var lastYearBal = new WaterConnectionBalance
                                    {
                                        //Id = null,
                                        WcPrimaryId = objToBeUpdated.WaterConnectionId,
                                        BarCode = $"0000-0000-0000",
                                        ConnectionNo = mci_conn_no,
                                        InvoiceNo = " ",
                                        Year = (obj.Year - 1),
                                        Month = 12,
                                        FromDate = new DateOnly((obj.Year - 1), 1, 1),
                                        ToDate = new DateOnly((obj.Year - 1), 12, 31),


                                        MeterNo = meterNo,
                                        MeterCondition = 1,
                                        PreviousMeterReading = 0,
                                        ThisMonthMeterReading = 0,

                                        WaterCharge = 0,
                                        FixedCharge = 0,

                                        VATRate = 0,
                                        VATAmount = 0,
                                        ThisMonthCharge = obj.LastYearArrears,


                                        TotalDue = 0,

                                        OnTimePaid = 0,
                                        LatePaid = 0,
                                        Payments = 0,

                                        IsCompleted = false,
                                        IsFilled = true,
                                        IsProcessed = false,


                                        LastBillYearMonth = "no data in new system",
                                        PrintBalanceBF = 0,
                                        PrintLastBalance = 0,
                                        PrintBillingDetails = "no data in new system",
                                        PrintLastMonthPayments = 0,
                                        NoOfPayments = 0,
                                        NoOfCancels = 0,


                                        //CreatedAt
                                        CreatedBy = obj.CreatedBy,

                                    };

                                    var currentBalWithArrears = new WaterConnectionBalance
                                    {
                                        //Id = null,
                                        WcPrimaryId = objToBeUpdated.WaterConnectionId,
                                        BarCode = $"0000-0000-0000",
                                        ConnectionNo = mci_conn_no,
                                        InvoiceNo = "",
                                        Year = obj.Year,
                                        Month = obj.Month,
                                        FromDate = new DateOnly(obj.Year, 1, 1),
                                        ToDate = today,


                                        MeterNo = meterNo,
                                        MeterCondition = 1,
                                        PreviousMeterReading = 0,
                                        ThisMonthMeterReading = obj.LastMeterReading,

                                        WaterCharge = 0,
                                        FixedCharge = 0,

                                        VATRate = 0,
                                        VATAmount = 0,
                                        ThisMonthCharge = obj.MonthlyBalance - obj.LastYearArrears,


                                        TotalDue = 0,

                                        OnTimePaid = 0,
                                        LatePaid = 0,
                                        Payments = 0,

                                        IsCompleted = false,
                                        IsFilled = true,
                                        IsProcessed = false,


                                        LastBillYearMonth = "no data in new system",
                                        PrintBalanceBF = 0,
                                        PrintLastBalance = obj.LastYearArrears,
                                        PrintBillingDetails = "no data in new system",
                                        PrintLastMonthPayments = 0,
                                        NoOfPayments = 0,
                                        NoOfCancels = 0,


                                        //CreatedAt
                                        CreatedBy = obj.CreatedBy,

                                    };



                                    await _unitOfWork.Balances.AddAsync(lastYearBal);
                                    await _unitOfWork.Balances.AddAsync(currentBalWithArrears);

                                    await _unitOfWork.CommitAsync();

                                    objToBeUpdated.BalanceIdForLastYearArrears = lastYearBal.Id;
                                    objToBeUpdated.BalanceIdForCurrentBalance = currentBalWithArrears.Id;




                                    //bal.Year = obj.Year;
                                    //bal.Month = obj.Month;
                                    //bal.FromDate = new DateOnly(obj.Year, 1, 1);
                                    //bal.ThisMonthMeterReading = obj.LastMeterReading;
                                    //bal.ThisMonthCharges = obj.MonthlyBalance - obj.LastYearArrears;

                                }
                                else
                                {

                                    bal.Year = obj.Year;
                                    bal.Month = obj.Month;
                                    bal.FromDate = new DateOnly(obj.Year, 1, 1);
                                    bal.ThisMonthMeterReading = obj.LastMeterReading;
                                    bal.ThisMonthCharge = obj.MonthlyBalance;
                                }

                            }
                            else
                            {
                                if (objToBeUpdated.BalanceIdForLastYearArrears.HasValue)
                                {
                                    var lyearBal = await _unitOfWork.Balances.GetByIdAsync(objToBeUpdated.BalanceIdForLastYearArrears);
                                    if (lyearBal != null)
                                    {
                                        objToBeUpdated.BalanceIdForLastYearArrears = null;
                                        _unitOfWork.Balances.Remove(lyearBal);
                                    }
                                }



                                bal.Year = obj.Year;
                                bal.Month = obj.Month;
                                bal.FromDate = new DateOnly(obj.Year, 1, 1);
                                bal.ThisMonthMeterReading = obj.LastMeterReading;
                                bal.ThisMonthCharge = obj.MonthlyBalance > 0 ? obj.MonthlyBalance : 0;

                                bal.IsCompleted = obj.MonthlyBalance < 0 ? true : false;
                                bal.OverPay = obj.MonthlyBalance < 0 ? Math.Abs((decimal)obj.MonthlyBalance!) : 0;
                                wc.RunningOverPay = Math.Abs((decimal)obj.MonthlyBalance!);

                            }

                            objToBeUpdated.Year = obj.Year;
                            objToBeUpdated.Month = obj.Month;
                            objToBeUpdated.LastYearArrears = obj.LastYearArrears;
                            objToBeUpdated.MonthlyBalance = obj.MonthlyBalance;
                            objToBeUpdated.LastMeterReading = obj.LastMeterReading;

                            objToBeUpdated.UpdatedBy = obj.UpdatedBy;
                            objToBeUpdated.UpdatedAt = DateTime.Now;

                            if (token.IsFinalAccountsEnabled == 1)
                            {
                                await UpdateLedgerAccounts(objToBeUpdated.WaterConnectionId, objToBeUpdated, session, token);
                            }

                            await _unitOfWork.CommitAsync();
                            return (true, "Update Sucessfull");

                        }
                        else
                        {
                            throw new FinalAccountException("Alread Prcceed Opening Balcnce For Next Month");
                        }
                    }
                    else
                    {
                        throw new GeneralException("No Open Balance Found");
                    }
                }
                else
                {
                    throw new FinalAccountException("No Active Session");
                }
            }
            catch (Exception ex)
            {
                return (false, ex is FinalAccountException or GeneralException ? ex.Message : null);

            }

        }

        public async Task<IEnumerable<WaterConnection>> GetAllWaterConnectionsNotProcessedOpenBalanceForOfficeId(int officeId)
        {
            return await _unitOfWork.OpeningBalanceInformations.GetAllWaterConnectionsNotProcessedOpenBalanceForOfficeId(officeId);
        }

        public async Task<WaterConnection> GetWaterConnectionsNotProcessedOpenBalance(int wcId)
        {
            return await _unitOfWork.OpeningBalanceInformations.GetWaterConnectionsNotProcessedOpenBalance(wcId);
        }

        private async Task<bool> UpdateLedgerAccounts(int wcId, OpeningBalanceInformation openBalanceInfo, Session session, HTokenClaim token)
        {
            try
            {
                var accumulatedFund = await _unitOfWork.SpecialLedgerAccounts.GetAccumulatedFundLedgerAccount(token.sabhaId);

                var accumulatedFundBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(accumulatedFund.VoteId!.Value, token.sabhaId, session.StartAt.Year);

                if (accumulatedFundBalance == null)
                {
                    accumulatedFundBalance = await _voteBalanceService.CreateNewVoteBalance(accumulatedFund.VoteId!.Value, session, token);


                }

                var item = await _unitOfWork.WaterConnections.GetYearEndProcessForFinalAccount(wcId);

                if (item != null)
                {
                    var votes = await _unitOfWork.VoteAssigns.GetAllForWaterProject(item.SubRoad!.WaterProjectId);
                    item.OpeningBalanceInformation = openBalanceInfo;

                    if (votes != null)
                    {

                        decimal? LYArrears = 0m;
                        decimal? TYArrears = 0m;
                        decimal? OverPay = 0m;

                        if (item.OpeningBalanceInformation!.MonthlyBalance < 0)
                        {
                            OverPay = Math.Abs((decimal)item.OpeningBalanceInformation!.MonthlyBalance);
                        }
                        else
                        {
                            LYArrears = item.OpeningBalanceInformation!.LastYearArrears;
                            TYArrears = item.OpeningBalanceInformation!.MonthlyBalance - item.OpeningBalanceInformation!.LastYearArrears;
                        }


                        var voteBalanceLYArrears = await _unitOfWork.VoteBalances.GetActiveVoteBalance(votes.Where(v => v.PaymentCategoryId == 1).Select(v => v.VoteDetailsId).FirstOrDefault(), token.sabhaId, session.StartAt.Year);


                        if (voteBalanceLYArrears == null)
                        {
                            voteBalanceLYArrears = await _voteBalanceService.CreateNewVoteBalance(votes.Where(v => v.PaymentCategoryId == 1).Select(v => v.VoteDetailsId).FirstOrDefault(), session, token);


                        }

                        if (voteBalanceLYArrears != null)
                        {
                            voteBalanceLYArrears.Debit += (decimal)LYArrears!;
                            voteBalanceLYArrears.UpdatedBy = token.userId;
                            voteBalanceLYArrears.UpdatedAt = session.StartAt;
                            voteBalanceLYArrears.SystemActionAt = DateTime.Now;
                            voteBalanceLYArrears.ExchangedAmount = (decimal)LYArrears!;

                            voteBalanceLYArrears.TransactionType = VoteBalanceTransactionTypes.BFDebit;
                            voteBalanceLYArrears.CreditDebitRunningBalance = voteBalanceLYArrears.Debit - voteBalanceLYArrears.Credit;

                            /*vote balance log */
                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalanceLYArrears);
                            voteBalanceLYArrears.ExchangedAmount = 0;


                            vtbLog.Year = session.StartAt.Year;
                            vtbLog.Code = "WTR LYA O/B";
                            vtbLog.SubCode = item.MeterConnectInfo!.ConnectionNo;
                            vtbLog.OfficeId = item.OfficeId;
                            vtbLog.AppCategory = AppCategory.Water_Bill;
                            vtbLog.ModulePrimaryKey = item.Id;

                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                            CreateJournalTransfer(vtbLog, CashBookTransactionType.DEBIT);

                            /**********/

                        }
                        else
                        {
                            throw new Exception("Vote Balance Not Found For Last year Arrears");
                        }


                        /******************/

                        if (accumulatedFund != null)
                        {

                            accumulatedFundBalance.Credit += (decimal)LYArrears!;
                            accumulatedFundBalance.UpdatedBy = token.userId;
                            accumulatedFundBalance.UpdatedAt = session.StartAt;
                            accumulatedFundBalance.SystemActionAt = DateTime.Now;
                            accumulatedFundBalance.ExchangedAmount = (decimal)LYArrears!;


                            accumulatedFundBalance.TransactionType = VoteBalanceTransactionTypes.BFCredit;
                            //voteBalanceLYWarrant.CreditDebitRunningBalance = voteBalanceLYWarrant
                            accumulatedFundBalance.CreditDebitRunningBalance = accumulatedFundBalance.Credit - accumulatedFundBalance.Debit;


                            /*vote balance log */
                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(accumulatedFundBalance);
                            accumulatedFundBalance.ExchangedAmount = 0;

                            vtbLog.Year = session.StartAt.Year;
                            vtbLog.Code = "WTR LYA O/B";
                            vtbLog.SubCode = item.MeterConnectInfo!.ConnectionNo;
                            vtbLog.OfficeId = item.OfficeId;
                            vtbLog.AppCategory = AppCategory.Water_Bill;
                            vtbLog.ModulePrimaryKey = item.Id;
                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);




                            /**********/

                        }
                        else
                        {
                            throw new Exception("Vote Balance Not Found For Accumulated Fund");
                        }

                        /******************/

                        var voteBalanceTYArrears = await _unitOfWork.VoteBalances.GetActiveVoteBalance(votes.Where(v => v.PaymentCategoryId == 2).Select(v => v.VoteDetailsId).FirstOrDefault(), token.sabhaId, session.StartAt.Year);


                        if (voteBalanceTYArrears == null)
                        {
                            voteBalanceTYArrears = await _voteBalanceService.CreateNewVoteBalance(votes.Where(v => v.PaymentCategoryId == 2).Select(v => v.VoteDetailsId).FirstOrDefault(), session, token);


                        }

                        if (voteBalanceTYArrears != null)
                        {

                            voteBalanceTYArrears.Debit += (decimal)TYArrears!;
                            voteBalanceTYArrears.UpdatedBy = token.userId;
                            voteBalanceTYArrears.UpdatedAt = session.StartAt;
                            voteBalanceTYArrears.SystemActionAt = DateTime.Now;

                            voteBalanceTYArrears.TransactionType = VoteBalanceTransactionTypes.BFDebit;
                            voteBalanceTYArrears.CreditDebitRunningBalance = voteBalanceTYArrears.Debit - voteBalanceTYArrears.Credit;
                            voteBalanceTYArrears.ExchangedAmount = (decimal)TYArrears!;

                            /*vote balance log */
                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalanceTYArrears);
                            voteBalanceTYArrears.ExchangedAmount = 0m;

                            vtbLog.Year = session.StartAt.Year;
                            vtbLog.Code = "WTR TYA O/B";
                            vtbLog.SubCode = item.MeterConnectInfo!.ConnectionNo;
                            vtbLog.OfficeId = item.OfficeId;
                            vtbLog.AppCategory = AppCategory.Water_Bill;
                            vtbLog.ModulePrimaryKey = item.Id;
                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                            CreateJournalTransfer(vtbLog, CashBookTransactionType.DEBIT);

                            /**********/
                        }
                        else
                        {
                            throw new Exception("Vote Balance Not Found For This Year Arrears");
                        }


                        /******************/

                        if (accumulatedFund != null)
                        {

                            accumulatedFundBalance.Credit += (decimal)TYArrears!;
                            accumulatedFundBalance.UpdatedBy = token.userId;
                            accumulatedFundBalance.UpdatedAt = session.StartAt;
                            accumulatedFundBalance.SystemActionAt = DateTime.Now;
                            accumulatedFundBalance.ExchangedAmount = (decimal)TYArrears!;


                            accumulatedFundBalance.TransactionType = VoteBalanceTransactionTypes.BFCredit;
                            //voteBalanceLYWarrant.CreditDebitRunningBalance = voteBalanceLYWarrant
                            accumulatedFundBalance.CreditDebitRunningBalance = accumulatedFundBalance.Credit - accumulatedFundBalance.Debit;


                            /*vote balance log */
                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(accumulatedFundBalance);
                            accumulatedFundBalance.ExchangedAmount = 0;

                            vtbLog.Year = session.StartAt.Year;
                            vtbLog.Code = "WTR TYA O/B";
                            vtbLog.Month = item.OpeningBalanceInformation.Month;
                            vtbLog.SubCode = item.MeterConnectInfo!.ConnectionNo;
                            vtbLog.OfficeId = item.OfficeId;
                            vtbLog.AppCategory = AppCategory.Water_Bill;
                            vtbLog.ModulePrimaryKey = item.Id;
                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);


                            /**********/

                        }
                        else
                        {
                            throw new Exception("Vote Balance Not Found For Accumulated Fund");
                        }

                        /******************/

                        var voteBalanceOverPay = await _unitOfWork.VoteBalances.GetActiveVoteBalance(votes.Where(v => v.PaymentCategoryId == 4).Select(v => v.VoteDetailsId).FirstOrDefault(), token.sabhaId, session.StartAt.Year);


                        if (voteBalanceOverPay == null)
                        {
                            voteBalanceOverPay = await _voteBalanceService.CreateNewVoteBalance(votes.Where(v => v.PaymentCategoryId == 4).Select(v => v.VoteDetailsId).FirstOrDefault(), session, token);


                        }

                        if (voteBalanceOverPay != null)
                        {
                            voteBalanceOverPay.Credit += (decimal)OverPay!;
                            voteBalanceOverPay.UpdatedBy = token.userId;
                            voteBalanceOverPay.UpdatedAt = session.StartAt;
                            voteBalanceOverPay.SystemActionAt = DateTime.Now;

                            voteBalanceOverPay.TransactionType = VoteBalanceTransactionTypes.BFCredit;
                            voteBalanceOverPay.CreditDebitRunningBalance = voteBalanceOverPay.Credit - voteBalanceOverPay.Debit;
                            voteBalanceOverPay.ExchangedAmount = (decimal)OverPay!;


                            /*vote balance log */
                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalanceOverPay);
                            voteBalanceOverPay.ExchangedAmount = 0m;


                            vtbLog.Year = session.StartAt.Year;
                            vtbLog.Code = "WTR OverPay O/B";
                            vtbLog.Month = item.OpeningBalanceInformation.Month;
                            vtbLog.SubCode = item.MeterConnectInfo!.ConnectionNo;
                            vtbLog.OfficeId = item.OfficeId;
                            vtbLog.AppCategory = AppCategory.Water_Bill;
                            vtbLog.ModulePrimaryKey = item.Id;

                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                            CreateJournalTransfer(vtbLog, CashBookTransactionType.CREDIT);

                            /**********/

                        }
                        else
                        {
                            throw new Exception("Vote Balance Not Found For Over Payment");
                        }

                        /******************/

                        if (accumulatedFund != null)
                        {

                            accumulatedFundBalance.Debit += (decimal)OverPay!;
                            accumulatedFundBalance.UpdatedBy = token.userId;
                            accumulatedFundBalance.UpdatedAt = session.StartAt;
                            accumulatedFundBalance.SystemActionAt = DateTime.Now;
                            accumulatedFundBalance.ExchangedAmount = (decimal)OverPay!;


                            accumulatedFundBalance.TransactionType = VoteBalanceTransactionTypes.BFDebit;
                            //voteBalanceLYWarrant.CreditDebitRunningBalance = voteBalanceLYWarrant
                            accumulatedFundBalance.CreditDebitRunningBalance = accumulatedFundBalance.Credit - accumulatedFundBalance.Debit;


                            /*vote balance log */
                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(accumulatedFundBalance);
                            accumulatedFundBalance.ExchangedAmount = 0;

                            vtbLog.Year = session.StartAt.Year;
                            vtbLog.Code = "WTR OverPay O/B";
                            vtbLog.SubCode = item.MeterConnectInfo!.ConnectionNo;
                            vtbLog.OfficeId = item.OfficeId;
                            vtbLog.AppCategory = AppCategory.Water_Bill;
                            vtbLog.ModulePrimaryKey = item.Id;
                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);




                            /**********/

                        }
                        else
                        {
                            throw new Exception("Vote Balance Not Found For Accumulated Fund");
                        }

                        /******************/


                    }
                    else
                    {
                        throw new Exception("Vote Assignment Not Found");
                    }

                }
                else
                {
                    throw new Exception("Water Connection Not Found");
                }
                return true;

            }
            catch (Exception ex)
            {
                throw;
            }

        }


        private async Task<bool> UpdateLedgerAccountsRollBack(int wcId, Session session, HTokenClaim token)
        {
            try
            {
                var accumulatedFund = await _unitOfWork.SpecialLedgerAccounts.GetAccumulatedFundLedgerAccount(token.sabhaId);

                var accumulatedFundBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(accumulatedFund.VoteId!.Value, token.sabhaId, session.StartAt.Year);

                if (accumulatedFundBalance == null)
                {
                    accumulatedFundBalance = await _voteBalanceService.CreateNewVoteBalance(accumulatedFund.VoteId!.Value, session, token);


                }

                var item = await _unitOfWork.WaterConnections.GetYearEndProcessForFinalAccount(wcId);

                if (item != null)
                {
                    var votes = await _unitOfWork.VoteAssigns.GetAllForWaterProject(item.SubRoad!.WaterProjectId);

                    //item.OpeningBalanceInformation = openBalanceInfo;

                    if (votes != null)
                    {

                        decimal? LYArrears = 0m;
                        decimal? TYArrears = 0m;
                        decimal? OverPay = 0m;

                        if (item.OpeningBalanceInformation!.MonthlyBalance < 0)
                        {
                            OverPay = Math.Abs((decimal)item.OpeningBalanceInformation!.MonthlyBalance);
                        }
                        else
                        {
                            LYArrears = item.OpeningBalanceInformation!.LastYearArrears;
                            TYArrears = item.OpeningBalanceInformation!.MonthlyBalance - item.OpeningBalanceInformation!.LastYearArrears;
                        }


                        var voteBalanceLYArrears = await _unitOfWork.VoteBalances.GetActiveVoteBalance(votes.Where(v => v.PaymentCategoryId == 1).Select(v => v.VoteDetailsId).FirstOrDefault(), token.sabhaId, session.StartAt.Year);


                        if (voteBalanceLYArrears == null)
                        {
                            voteBalanceLYArrears = await _voteBalanceService.CreateNewVoteBalance(votes.Where(v => v.PaymentCategoryId == 1).Select(v => v.VoteDetailsId).FirstOrDefault(), session, token);


                        }

                        if (voteBalanceLYArrears != null)
                        {
                            voteBalanceLYArrears.Credit += (decimal)LYArrears!;
                            voteBalanceLYArrears.UpdatedBy = token.userId;
                            voteBalanceLYArrears.UpdatedAt = session.StartAt;
                            voteBalanceLYArrears.SystemActionAt = DateTime.Now;
                            voteBalanceLYArrears.ExchangedAmount = (decimal)LYArrears!;

                            voteBalanceLYArrears.TransactionType = VoteBalanceTransactionTypes.BFCredit;
                            voteBalanceLYArrears.CreditDebitRunningBalance = voteBalanceLYArrears.Debit - voteBalanceLYArrears.Credit;

                            /*vote balance log */
                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalanceLYArrears);
                            voteBalanceLYArrears.ExchangedAmount = 0;


                            vtbLog.Year = session.StartAt.Year;
                            vtbLog.Code = "WTR LYA O/B RB";
                            vtbLog.SubCode = item.MeterConnectInfo!.ConnectionNo;
                            vtbLog.OfficeId = item.OfficeId;
                            vtbLog.AppCategory = AppCategory.Water_Bill;
                            vtbLog.ModulePrimaryKey = item.Id;

                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                            CreateJournalTransfer(vtbLog, CashBookTransactionType.DEBIT);

                            /**********/

                        }
                        else
                        {
                            throw new Exception("Vote Balance Not Found For Last year Arrears");
                        }


                        /******************/

                        if (accumulatedFund != null)
                        {

                            accumulatedFundBalance.Debit += (decimal)LYArrears!;
                            accumulatedFundBalance.UpdatedBy = token.userId;
                            accumulatedFundBalance.UpdatedAt = session.StartAt;
                            accumulatedFundBalance.SystemActionAt = DateTime.Now;
                            accumulatedFundBalance.ExchangedAmount = (decimal)LYArrears!;


                            accumulatedFundBalance.TransactionType = VoteBalanceTransactionTypes.BFDebit;
                            //voteBalanceLYWarrant.CreditDebitRunningBalance = voteBalanceLYWarrant
                            accumulatedFundBalance.CreditDebitRunningBalance = accumulatedFundBalance.Credit - accumulatedFundBalance.Debit;


                            /*vote balance log */
                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(accumulatedFundBalance);
                            accumulatedFundBalance.ExchangedAmount = 0;

                            vtbLog.Year = session.StartAt.Year;
                            vtbLog.Code = "WTR LYA O/B RB";
                            vtbLog.SubCode = item.MeterConnectInfo!.ConnectionNo;
                            vtbLog.OfficeId = item.OfficeId;
                            vtbLog.AppCategory = AppCategory.Water_Bill;
                            vtbLog.ModulePrimaryKey = item.Id;
                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);




                            /**********/

                        }
                        else
                        {
                            throw new Exception("Vote Balance Not Found For Accumulated Fund");
                        }

                        /******************/

                        var voteBalanceTYArrears = await _unitOfWork.VoteBalances.GetActiveVoteBalance(votes.Where(v => v.PaymentCategoryId == 2).Select(v => v.VoteDetailsId).FirstOrDefault(), token.sabhaId, session.StartAt.Year);


                        if (voteBalanceTYArrears == null)
                        {
                            voteBalanceTYArrears = await _voteBalanceService.CreateNewVoteBalance(votes.Where(v => v.PaymentCategoryId == 2).Select(v => v.VoteDetailsId).FirstOrDefault(), session, token);


                        }

                        if (voteBalanceTYArrears != null)
                        {

                            voteBalanceTYArrears.Credit += (decimal)TYArrears!;
                            voteBalanceTYArrears.UpdatedBy = token.userId;
                            voteBalanceTYArrears.UpdatedAt = session.StartAt;
                            voteBalanceTYArrears.SystemActionAt = DateTime.Now;

                            voteBalanceTYArrears.TransactionType = VoteBalanceTransactionTypes.BFCredit;
                            voteBalanceTYArrears.CreditDebitRunningBalance = voteBalanceTYArrears.Debit - voteBalanceTYArrears.Credit;
                            voteBalanceTYArrears.ExchangedAmount = (decimal)TYArrears!;

                            /*vote balance log */
                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalanceTYArrears);
                            voteBalanceTYArrears.ExchangedAmount = 0m;

                            vtbLog.Year = session.StartAt.Year;
                            vtbLog.Code = "WTR TYA O/B RB";
                            vtbLog.SubCode = item.MeterConnectInfo!.ConnectionNo;
                            vtbLog.OfficeId = item.OfficeId;
                            vtbLog.AppCategory = AppCategory.Water_Bill;
                            vtbLog.ModulePrimaryKey = item.Id;
                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                            CreateJournalTransfer(vtbLog, CashBookTransactionType.DEBIT);

                            /**********/
                        }
                        else
                        {
                            throw new Exception("Vote Balance Not Found For This Year Arrears");
                        }


                        /******************/

                        if (accumulatedFund != null)
                        {

                            accumulatedFundBalance.Debit += (decimal)TYArrears!;
                            accumulatedFundBalance.UpdatedBy = token.userId;
                            accumulatedFundBalance.UpdatedAt = session.StartAt;
                            accumulatedFundBalance.SystemActionAt = DateTime.Now;
                            accumulatedFundBalance.ExchangedAmount = (decimal)TYArrears!;


                            accumulatedFundBalance.TransactionType = VoteBalanceTransactionTypes.BFDebit;
                            //voteBalanceLYWarrant.CreditDebitRunningBalance = voteBalanceLYWarrant
                            accumulatedFundBalance.CreditDebitRunningBalance = accumulatedFundBalance.Credit - accumulatedFundBalance.Debit;


                            /*vote balance log */
                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(accumulatedFundBalance);
                            accumulatedFundBalance.ExchangedAmount = 0;

                            vtbLog.Year = session.StartAt.Year;
                            vtbLog.Code = "WTR TYA O/B RB";
                            vtbLog.Month = item.OpeningBalanceInformation.Month;
                            vtbLog.SubCode = item.MeterConnectInfo!.ConnectionNo;
                            vtbLog.OfficeId = item.OfficeId;
                            vtbLog.AppCategory = AppCategory.Water_Bill;
                            vtbLog.ModulePrimaryKey = item.Id;
                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);


                            /**********/

                        }
                        else
                        {
                            throw new Exception("Vote Balance Not Found For Accumulated Fund");
                        }

                        /******************/

                        var voteBalanceOverPay = await _unitOfWork.VoteBalances.GetActiveVoteBalance(votes.Where(v => v.PaymentCategoryId == 4).Select(v => v.VoteDetailsId).FirstOrDefault(), token.sabhaId, session.StartAt.Year);


                        if (voteBalanceOverPay == null)
                        {
                            voteBalanceOverPay = await _voteBalanceService.CreateNewVoteBalance(votes.Where(v => v.PaymentCategoryId == 4).Select(v => v.VoteDetailsId).FirstOrDefault(), session, token);


                        }

                        if (voteBalanceOverPay != null)
                        {
                            voteBalanceOverPay.Debit += (decimal)OverPay!;
                            voteBalanceOverPay.UpdatedBy = token.userId;
                            voteBalanceOverPay.UpdatedAt = session.StartAt;
                            voteBalanceOverPay.SystemActionAt = DateTime.Now;

                            voteBalanceOverPay.TransactionType = VoteBalanceTransactionTypes.BFDebit;
                            voteBalanceOverPay.CreditDebitRunningBalance = voteBalanceOverPay.Credit - voteBalanceOverPay.Debit;
                            voteBalanceOverPay.ExchangedAmount = (decimal)OverPay!;


                            /*vote balance log */
                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalanceOverPay);
                            voteBalanceOverPay.ExchangedAmount = 0m;


                            vtbLog.Year = session.StartAt.Year;
                            vtbLog.Code = "WTR OverPay O/B RB";
                            vtbLog.Month = item.OpeningBalanceInformation.Month;
                            vtbLog.SubCode = item.MeterConnectInfo!.ConnectionNo;
                            vtbLog.OfficeId = item.OfficeId;
                            vtbLog.AppCategory = AppCategory.Water_Bill;
                            vtbLog.ModulePrimaryKey = item.Id;

                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                            CreateJournalTransfer(vtbLog, CashBookTransactionType.CREDIT);

                            /**********/

                        }
                        else
                        {
                            throw new Exception("Vote Balance Not Found For Over Payment");
                        }

                        /******************/

                        if (accumulatedFund != null)
                        {

                            accumulatedFundBalance.Credit += (decimal)OverPay!;
                            accumulatedFundBalance.UpdatedBy = token.userId;
                            accumulatedFundBalance.UpdatedAt = session.StartAt;
                            accumulatedFundBalance.SystemActionAt = DateTime.Now;
                            accumulatedFundBalance.ExchangedAmount = (decimal)OverPay!;


                            accumulatedFundBalance.TransactionType = VoteBalanceTransactionTypes.BFCredit;
                            //voteBalanceLYWarrant.CreditDebitRunningBalance = voteBalanceLYWarrant
                            accumulatedFundBalance.CreditDebitRunningBalance = accumulatedFundBalance.Credit - accumulatedFundBalance.Debit;


                            /*vote balance log */
                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(accumulatedFundBalance);
                            accumulatedFundBalance.ExchangedAmount = 0;

                            vtbLog.Year = session.StartAt.Year;
                            vtbLog.Code = "WTR OverPay O/B RB";
                            vtbLog.SubCode = item.MeterConnectInfo!.ConnectionNo;
                            vtbLog.OfficeId = item.OfficeId;
                            vtbLog.AppCategory = AppCategory.Water_Bill;
                            vtbLog.ModulePrimaryKey = item.Id;
                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);




                            /**********/

                        }
                        else
                        {
                            throw new Exception("Vote Balance Not Found For Accumulated Fund");
                        }

                        /******************/


                    }
                    else
                    {
                        throw new Exception("Vote Assignment Not Found");
                    }

                }
                else
                {
                    throw new Exception("Water Connection Not Found");
                }
                return true;

            }
            catch (Exception ex)
            {
                throw;
            }

        }

        private void CreateJournalTransfer(VoteBalanceLog vbl, CashBookTransactionType transactionType)
        {
            var journalTransfer = new InternalJournalTransfers
            {
                VoteBalanceId = vbl.VoteBalanceId,
                VoteDetailId = vbl.VoteDetailId,
                SabhaId = vbl.SabhaId,
                Year = vbl.Year,
                Month = vbl.Month,
                Code = vbl.Code,
                SubCode = vbl.SubCode,
                Description = vbl.Description,
                Status = vbl.Status,
                TransactionType = vbl.TransactionType,
                ModulePrimaryKey = vbl.ModulePrimaryKey,
                AppCategory = vbl.AppCategory,
                CreateBy = vbl.UpdatedBy,
                CreateAt = vbl.UpdatedAt,
                SystemActionAt = vbl.SystemActionAt
            };

            if (transactionType == CashBookTransactionType.CREDIT)
            {
                journalTransfer.Credit = vbl.ExchangedAmount;
            }
            else
            {
                journalTransfer.Debit = vbl.ExchangedAmount;
            }

            _unitOfWork.InternalJournalTransfers.AddAsync(journalTransfer);

        }
    }
}
