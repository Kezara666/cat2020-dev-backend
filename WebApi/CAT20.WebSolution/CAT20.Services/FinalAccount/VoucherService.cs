using AutoMapper;
using CAT20.Core;
using CAT20.Core.CustomExceptions;
using CAT20.Core.DTO;
using CAT20.Core.DTO.Final;
using CAT20.Core.DTO.Final.Save;
using CAT20.Core.DTO.OtherModule;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.FinalAccount.logs;
using CAT20.Core.Models.HRM.LoanManagement;
using CAT20.Core.Models.HRM.PersonalFile;
using CAT20.Core.Models.Mixin;
using CAT20.Core.Models.User;
using CAT20.Core.Models.Vote;
using CAT20.Core.Services.Control;
using CAT20.Core.Services.FinalAccount;
using CAT20.Core.Services.HRM.PersonalFile;
using CAT20.Core.Services.Mixin;
using CAT20.Core.Services.User;
using CAT20.Core.Services.Vote;
using CAT20.Services.User;
using CAT20.WebApi.Resources.Final;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Wordprocessing;
using Irony.Parsing;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using Org.BouncyCastle.Asn1.Ocsp;
using AppCategory = CAT20.Core.Models.Enums.AppCategory;


namespace CAT20.Services.FinalAccount;

public class VoucherService : IVoucherService
{
    private readonly IVoteUnitOfWork _unitOfWork;
    private readonly IVoteBalanceService _voteBalanceService;
    private readonly IUserDetailService _userDetailService;
    private readonly IEmployeeService _employeeService;
    private readonly IMapper _mapper;
    private readonly IPartnerService _partnerService;
    private readonly IMixinOrderService _mixinOrderService;
    private readonly IAgentsService _agentsService;

    public VoucherService(IVoteUnitOfWork unitOfWork, IVoteBalanceService voteBalanceService,
        IUserDetailService userDetailServiceService, IEmployeeService employeeService, IMapper mapper, IPartnerService partnerService,
        IMixinOrderService mixinOrderService,IAgentsService agentsService)
    {
        _unitOfWork = unitOfWork;
        _voteBalanceService = voteBalanceService;
        _userDetailService = userDetailServiceService;
        _employeeService = employeeService;
        _mapper = mapper;
        _partnerService = partnerService;
        _mixinOrderService = mixinOrderService;
        _agentsService = agentsService;
    }

    public async Task<(bool, string?,Voucher)> CreateVoucher(SaveVoucherResource voucherResource, HTokenClaim token)
    {
        using (var transaction = _unitOfWork.BeginTransaction())
        {
            try
            {
                var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                if (session != null)
                {
                    var commitmentById =
                        await _unitOfWork.Commitment.GetForCreateVoucherById(voucherResource.CommitmentId!.Value);

                    if (commitmentById != null && commitmentById.CommitmentLine != null)
                    {
                        var sessionDate = await _unitOfWork.Sessions.IsRescueSessionThenDate(session.Id);

                        foreach (var voucherItem in voucherResource.SubVoucherItems!)
                        {
                            foreach (var voucherCrossOrder in voucherItem.VoucherCrossOrders!)
                            {
                                if (sessionDate.HasValue)
                                {
                                    voucherCrossOrder.CrossOrder.CreatedAt = (DateTime)sessionDate;

                                    foreach (var item in voucherCrossOrder.CrossOrder!.MixinOrderLine)
                                    {
                                        item.CreatedAt = (DateTime)sessionDate;
                                    }
                                }
                                else
                                {
                                    voucherCrossOrder.CrossOrder.CreatedAt = DateTime.Now;

                                    foreach (var item in voucherCrossOrder.CrossOrder!.MixinOrderLine)
                                    {
                                        item.CreatedAt = DateTime.Now;
                                    }
                                }


                                voucherCrossOrder.CrossOrder.State = OrderStatus.Cross;

                                var mixinOrderToCreate =
                                    _mapper.Map<SaveCrossOrderResource, MixinOrder>(voucherCrossOrder.CrossOrder);

                                await _unitOfWork.MixinOrders.AddAsync(mixinOrderToCreate);
                                await _unitOfWork.CommitAsync();

                                voucherCrossOrder.CrossOrderId = mixinOrderToCreate.Id;
                                voucherCrossOrder.Amount = mixinOrderToCreate.TotalAmount;

                            }
                        }

                       

                        var newVoucher = _mapper.Map<SaveVoucherResource, Voucher>(voucherResource);

                       
                        commitmentById.HasVoucher = true;


                        foreach (var vLine in newVoucher.VoucherLine!)
                        {
                            var voteAllocation =  await _unitOfWork.VoteBalances.GetActiveVoteBalance(vLine.VoteId, token.sabhaId, session.StartAt.Year);
                            if (voteAllocation != null)
                            {
                                vLine.VoteBalanceId = voteAllocation.Id!.Value;
                            }
                            else
                            {
                                throw new Exception("Unable to find Vote Allocation");
                            }
                        }


                        foreach (var commitmentLine in commitmentById.CommitmentLine)
                        {
                            foreach (var commitmentLineVotes in commitmentLine.CommitmentLineVotes!)
                            {
                                var voteAllocation =  await _unitOfWork.VoteBalances.GetWithVoteAllocationByIdAsync(commitmentLineVotes.VoteAllocationId!.Value);

                                var voteFound = false;

                                foreach (var vLine in newVoucher.VoucherLine!)
                                {
                                    if (voteAllocation.Id == vLine.VoteBalanceId && vLine.CommitmentLineVoteId== commitmentLineVotes.Id)
                                    {
                                        if (newVoucher.PaymentStatus == PaymentStatus.FinalPayment)
                                        {
                                            voteAllocation.TotalHold -= commitmentLineVotes.RemainingAmount;
                                            voteAllocation.TotalCommitted += vLine.TotalAmount;
                                            commitmentById.PaymentStatus = PaymentStatus.FinalPayment;
                                            voteAllocation.TransactionType =    VoteBalanceTransactionTypes.CreateVoucher;


                                            commitmentLineVotes.RemainingAmount = 0;

                                            commitmentById.PaymentStatus  = PaymentStatus.FinalPayment;

                                            voteAllocation.UpdatedAt = session.StartAt;
                                            voteAllocation.UpdatedBy = token.userId;
                                            voteAllocation.SystemActionAt = DateTime.Now;
                                            voteAllocation.OfficeId = token.officeId;
                                            voteAllocation.SessionIdByOffice = session.Id;
                                            var vtbLog =  _mapper.Map<VoteBalance, VoteBalanceLog>(voteAllocation);
                                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                                        }
                                        else if (newVoucher.PaymentStatus == PaymentStatus.PartPayment)
                                        {
                                            voteAllocation.TotalHold -= vLine.TotalAmount;
                                            voteAllocation.TotalCommitted += vLine.TotalAmount;
                                            commitmentById.PaymentStatus = PaymentStatus.PartPayment;
                                            voteAllocation.TransactionType = VoteBalanceTransactionTypes.CreateVoucher;


                                            //commitmentLineVotes.RemainingAmount =  commitmentLineVotes.Amount - vLine.TotalAmount;
                                            commitmentLineVotes.RemainingAmount -=  vLine.TotalAmount;

                                            commitmentById.PaymentStatus = PaymentStatus.PartPayment;

                                            voteAllocation.UpdatedAt = session.StartAt;
                                            voteAllocation.UpdatedBy = token.userId;
                                            voteAllocation.SystemActionAt = DateTime.Now;
                                            voteAllocation.OfficeId = token.officeId;
                                            voteAllocation.SessionIdByOffice = session.Id;
                                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteAllocation);
                                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                                        }

                                        vLine.CommentOrDescription = commitmentLineVotes.Comment!;
                                        voteFound = true;
                                    }
                                    else
                                    {

                                    }
                                   
                                }

                                if (!voteFound&& newVoucher.PaymentStatus== PaymentStatus.FinalPayment)
                                {
                                    voteAllocation.TotalHold -= commitmentLineVotes.RemainingAmount;
                                }
                            }
                        }


                        if (await CreateCommitmentLog(commitmentById, token.userId, "create voucher"))
                        {

                        }
                        else
                        {
                            throw new Exception("Unable to Create Commitment Log");
                        }

                       
                        {
                           
                                //newVoucher.PartnerId = commitmentById.PayeeId;
                                newVoucher.ActionState = FinalAccountActionStates.Init;
                                newVoucher.SessionId = session.Id;
                                newVoucher.PayeeCategory = commitmentById.PayeeCategory;
                                newVoucher.BankId = commitmentById.BankId;

                                newVoucher.Year = session.StartAt.Year;
                                newVoucher.Month = session.StartAt.Month;
                                newVoucher.CommentOrDescription = commitmentById.Description;

                                newVoucher.VoucherCategory = VoucherCategory.VoteLedger;
                                newVoucher.CreatedAt = session.StartAt;
                                newVoucher.UpdatedAt = session.StartAt;
                                newVoucher.CreatedBy = token.userId;
                                newVoucher.UpdatedBy = token.userId;
                                newVoucher.SystemCreateAt = DateTime.Now;

                                newVoucher.Month = session.StartAt.Month;
                                newVoucher.Year = session.StartAt.Year;
                                newVoucher.SabhaId = token.sabhaId;
                                newVoucher.SystemCreateAt = DateTime.Now;

                                newVoucher.VoucherSequenceNumber = DateTime.Now.ToString("HHmmssfff") + new Random().Next(10);

                            foreach ((SubVoucherItem item, int index) in newVoucher.SubVoucherItems!.Select((item, index) => (item, index)))
                            {
                                item.SubVoucherNo = index + 1;
                            }




                            await _unitOfWork.Voucher.AddAsync(newVoucher);
                                await _unitOfWork.CommitAsync();


                                if (await CreateVoucherLog(newVoucher, token.userId, "create"))
                                {
                                    transaction.Commit();
                                    return (true, "Successfully Voucher Saved",newVoucher);
                                }
                                else
                                {
                                    throw new Exception("Unable to Create Log");
                                }

                          
                        }
                    }
                    else
                    {
                        throw new FinalAccountException("Unable to find Commitment");
                    }
                }
                else

                {
                    throw new FinalAccountException("Unable To Find Active Session");
                }
            }
            catch (Exception e)
            {
                transaction.Rollback();

                if (e.GetType() == typeof(FinalAccountException))
                {
                    return (false, e.Message,new Voucher());
                }
                else
                {
                    return (false, null,new Voucher());
                }
            }
        }
    }

    public async Task<(bool, string)> CreateDepositVoucher(SaveDepositVoucherResource depositVoucher, HTokenClaim token)
    {
        using (var transaction = _unitOfWork.BeginTransaction())
        {
            try
            {
                var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                if (session != null)
                {
                    var sessionDate = await _unitOfWork.Sessions.IsRescueSessionThenDate(session.Id);

                    List<VoucherLine> voucherLines = new List<VoucherLine>();

                    foreach (var voucherItem in depositVoucher.SubVoucherItems!)
                    {
                        foreach ( var voucherCrossOrder in  voucherItem.VoucherCrossOrders!)
                        {
                            if (sessionDate.HasValue)
                            {
                                voucherCrossOrder.CrossOrder.CreatedAt = (DateTime)sessionDate;

                                foreach (var item in voucherCrossOrder.CrossOrder!.MixinOrderLine)
                                {
                                    item.CreatedAt = (DateTime)sessionDate;
                                }
                            }
                            else
                            {
                                voucherCrossOrder.CrossOrder.CreatedAt = DateTime.Now;

                                foreach (var item in voucherCrossOrder.CrossOrder!.MixinOrderLine)
                                {
                                    item.CreatedAt = DateTime.Now;
                                }
                            }


                            voucherCrossOrder.CrossOrder.State = OrderStatus.Cross;

                            var mixinOrderToCreate =
                                _mapper.Map<SaveCrossOrderResource, MixinOrder>(voucherCrossOrder.CrossOrder);

                            await _unitOfWork.MixinOrders.AddAsync(mixinOrderToCreate);
                            await _unitOfWork.CommitAsync();

                            voucherCrossOrder.CrossOrderId = mixinOrderToCreate.Id;
                            voucherCrossOrder.Amount = mixinOrderToCreate.TotalAmount;
                            
                        }
                    }

                    foreach (var deposit in depositVoucher.DepositsForVoucher!)
                    {
                        var depositById = await _unitOfWork.Deposits.GetByIdAsync(deposit.Id);
                        if (depositById != null)
                        {
                            depositById.HoldAmount += deposit.PayingAmount;
                            depositById.IsEditable = false;
                            depositById.UpdatedBy = token.userId;
                            depositById.UpdatedAt = session.StartAt;
                            depositById.SystemUpdateAt = DateTime.Now;

                            var item = voucherLines.FirstOrDefault(x => x.VoteId == depositById.LedgerAccountId);
                            var voteBalance =
                                await _unitOfWork.VoteBalances.GetActiveVoteBalance(depositById.LedgerAccountId,
                                    token.sabhaId, session.StartAt.Year);

                            if (voteBalance != null)
                            {
                                if (item != null)
                                {
                                    item.NetAmount += deposit.PayingAmount;
                                    item.TotalAmount += deposit.PayingAmount;
                                }
                                else
                                {
                                    var vote = await _unitOfWork.VoteDetails.GetByIdAsync(depositById.LedgerAccountId);
                                    voucherLines.Add(new VoucherLine
                                    {
                                        VoteId = depositById.LedgerAccountId,
                                        NetAmount = deposit.PayingAmount,
                                        TotalAmount = deposit.PayingAmount,
                                        //PaymentStatus = PaymentStatus.FinalPayment,
                                        VoteCode = vote.Code,
                                        VoteBalanceId = (int)voteBalance.Id!,
                                        CommentOrDescription = ""
                                    });
                                }
                            }
                            else
                            {
                                throw new FinalAccountException("Unable to find Vote Balance");
                            }
                        }
                    }

                    var newDepositVoucher = _mapper.Map<SaveDepositVoucherResource, Voucher>(depositVoucher!);

                    newDepositVoucher.VoucherLine = voucherLines;
                    

                    foreach (var voucherLine in newDepositVoucher.VoucherLine)
                    {
                        var voteBalance = await _unitOfWork.VoteBalances.GetByIdAsync(voucherLine.VoteBalanceId);
                        //voteBalance.TotalHold += voucherLine.TotalAmount;
                        voteBalance.TransactionType = VoteBalanceTransactionTypes.CreateVoucher;
                        voteBalance.OfficeId = token.officeId;
                        voteBalance.SessionIdByOffice = session.Id;
                        voteBalance.UpdatedAt = session.StartAt;
                        voteBalance.UpdatedBy = token.userId;
                        voteBalance.SystemActionAt = DateTime.Now;
                        var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalance);

                        await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                    }


                    foreach (var (item, index) in newDepositVoucher.SubVoucherItems!.Select((item, index) =>
                                 (item, index)))
                    {
                        item.SubVoucherNo = index + 1;
                    }

                    var docSeqNums =
                        await _unitOfWork.FinalAccountSequenceNumber.GetNextSequenceNumberForYearSabhaModuleType(
                            session.CreatedAt.Year, token.sabhaId, FinalAccountModuleType.Voucher);
                    {
                       
                            //newDepositVoucher.PartnerId = 0;
                            newDepositVoucher.PayeeCategory = VoucherPayeeCategory.Partner;
                            newDepositVoucher.ActionState = FinalAccountActionStates.Init;
                            newDepositVoucher.VoucherCategory = VoucherCategory.Deposit;
                            newDepositVoucher.SessionId = session.Id;

                            newDepositVoucher.Year = session.StartAt.Year;
                            newDepositVoucher.Month = session.StartAt.Month;
                            //newDepositVoucher.CommentOrDescription = "deposit voucher";

                            newDepositVoucher.CreatedAt = session.StartAt;
                            newDepositVoucher.UpdatedAt = session.StartAt;
                            newDepositVoucher.CreatedBy = token.userId;
                            newDepositVoucher.UpdatedBy = token.userId;
                            newDepositVoucher.SystemCreateAt = DateTime.Now;

                            newDepositVoucher.SabhaId = token.sabhaId;
                            newDepositVoucher.VoucherSequenceNumber = DateTime.Now.ToString("HHmmssfff") + new Random().Next(10); ;
                    }

                    await _unitOfWork.Voucher.AddAsync(newDepositVoucher);
                    await _unitOfWork.CommitAsync();


                    if (await CreateVoucherLog(newDepositVoucher, token.userId, "create"))
                    {
                        transaction.Commit();
                        return (true, "Deposit Voucher Saved Successfully");
                    }
                    else
                    {
                        throw new Exception("Unable to Create Log");
                    }

                    ;
                }
                else
                {
                    throw new FinalAccountException("Unable to find Active Session");
                }
            }
            catch (Exception e)
            {
                transaction.Rollback();

                if (e.GetType() == typeof(FinalAccountException))
                {
                    return (false, e.Message);
                }
                else
                {
                    return (false, null);
                }
            }
        }
    }

    public async Task<(bool, string, Voucher)> CreateSubImprestVoucher(SubImprest subImprest, bool IsNew, Session session,
        HTokenClaim token)
    {
        try
        {
            

            {
                var voteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(subImprest.SubImprestVoteId,
                    token.sabhaId, session.StartAt.Year);
                var vote = await _unitOfWork.VoteDetails.GetByIdAsync(subImprest.SubImprestVoteId);

                if (voteBalance == null)
                {
                    voteBalance =
                        await _voteBalanceService.CreateNewVoteBalance(subImprest.SubImprestVoteId, session, token);
                    if (voteBalance == null)
                    {
                        throw new Exception("Unable to Create Vote Balance");
                    }
                }

                if (voteBalance != null)
                {
                    //voteBalance.Credit += subImprest.Amount;
                    voteBalance.TransactionType = VoteBalanceTransactionTypes.CreateVoucher;
                    voteBalance.OfficeId = token.officeId;
                    voteBalance.SessionIdByOffice = session.Id;
                    voteBalance.UpdatedAt = session.StartAt;
                    voteBalance.UpdatedBy = token.userId;
                    voteBalance.SystemActionAt = DateTime.Now;
                    var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalance);

                    await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);


                    var voteAssignment = await _unitOfWork.VoteAssignments.GetByVoteId(subImprest.SubImprestVoteId, token);

                    if(voteAssignment == null)
                    {
                        throw new FinalAccountException("Unable To Find Bank Account");
                    }

                    var newSubImprestVoucher = new Voucher
                    {
                        PayeeCategory = VoucherPayeeCategory.Employee,
                        ActionState = FinalAccountActionStates.Init,
                        VoucherCategory = VoucherCategory.SubImprest,
                        SessionId = session.Id,
                        SubImprestId = subImprest.Id,
                        BankId = voteAssignment.BankAccountId,

                        Year = session.StartAt.Year,
                        Month = session.StartAt.Month,
                        CommentOrDescription = subImprest.Description,
                        CreatedAt = session.StartAt,
                        UpdatedAt = session.StartAt,
                        CreatedBy = token.userId,
                        UpdatedBy = token.userId,
                        SabhaId = token.sabhaId,
                        VoucherAmount = subImprest.Amount,
                        TotalChequeAmount = subImprest.Amount,
                        SystemCreateAt = DateTime.Now,
                        VoucherSequenceNumber = DateTime.Now.ToString("HHmmssfff") + new Random().Next(10),
                    };

                    var voucherLine = new VoucherLine
                    {
                        VoteId = subImprest.SubImprestVoteId,
                        NetAmount = subImprest.Amount,
                        TotalAmount = subImprest.Amount,
                        //PaymentStatus = PaymentStatus.FinalPayment,
                        VoteCode = vote.Code,
                        VoteBalanceId = voteBalance.Id!.Value,
                        CommentOrDescription = subImprest.Description,
                    };

                    var voucherItem = new SubVoucherItem
                    {
                        PayeeId = subImprest.EmployeeId,
                        VoucherItemAmount = subImprest.Amount,
                        ChequeAmount = subImprest.Amount,
                        CommentOrDescription = subImprest.Description,
                        SubVoucherNo = 1,
                    };

                    newSubImprestVoucher.SubVoucherItems = new List<SubVoucherItem> { voucherItem };


                    newSubImprestVoucher.VoucherLine = new List<VoucherLine> { voucherLine };

                    if (!IsNew)
                    {
                        newSubImprestVoucher.ActionState = FinalAccountActionStates.HasCheque;
                        newSubImprestVoucher.VoucherSequenceNumber = subImprest.VoucherNo!;
                    }

                    await _unitOfWork.Voucher.AddAsync(newSubImprestVoucher);
                    await _unitOfWork.CommitAsync();


                    if (await CreateVoucherLog(newSubImprestVoucher, token.userId, "create subimprest voucher"))
                    {
                        await _unitOfWork.CommitAsync();
                    }


                    return (true, "Sub Imprest Voucher Saved Successfully", newSubImprestVoucher);
                }
                else
                {
                    throw new FinalAccountException("Unable to find Vote Balance");
                }
            }
           
        }
        catch (Exception e)
        {
            //return (false, e.Message, new Voucher());
            throw;
        }
    }

    public async Task<(bool, string, Voucher)> CreateSettlementVoucher(SubImprest subImprest, Session session,
        HTokenClaim token)
    {
        try
        {
            

            {



                decimal settlemetChequeAmount = (decimal)subImprest.Amount < (decimal)subImprest.SettleByBills! ? Math.Abs((decimal)subImprest.Amount - (decimal)subImprest.SettleByBills ) :0;



                    var newSettlementVoucher = new Voucher
                    {
                        PayeeCategory = VoucherPayeeCategory.Employee,
                        ActionState = FinalAccountActionStates.Init,
                        VoucherCategory = VoucherCategory.Settlement,
                        SessionId = session.Id,
                        SubImprestId = subImprest.Id,

                        Year = session.StartAt.Year,
                        Month = session.StartAt.Month,
                        CommentOrDescription = subImprest.Description,
                        CreatedAt = session.StartAt,
                        UpdatedAt = session.StartAt,
                        CreatedBy = token.userId,
                        UpdatedBy = token.userId,
                        SabhaId = token.sabhaId,
                        SystemCreateAt = DateTime.Now,
                        VoucherAmount = (decimal)subImprest.SettleByBills!,
                        TotalChequeAmount = settlemetChequeAmount,
                        VoucherSequenceNumber = DateTime.Now.ToString("HHmmssfff") + new Random().Next(10),
                    };

                    var bankId = await _unitOfWork.VoteAssignments.GetAccountIdByVoteId(subImprest.SubImprestSettlements!.FirstOrDefault()!.VoteDetailId,token);

                    if(bankId== 0)
                      {
                    throw new FinalAccountException("Unable to find Bank Account");
                    }

                    newSettlementVoucher.BankId = bankId;   

                    newSettlementVoucher.VoucherLine = new List<VoucherLine>();

                    foreach (var settlemnt in subImprest.SubImprestSettlements!)
                    {
                        var voteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(settlemnt.VoteDetailId,token.sabhaId, session.StartAt.Year);
                        var vote = await _unitOfWork.VoteDetails.GetByIdAsync(subImprest.SubImprestVoteId);

                        if (voteBalance == null)
                        {
                            throw new FinalAccountException("Unable to find Vote Balance");
                        }

                        voteBalance.TotalCommitted += settlemnt.Amount;
                        voteBalance.TransactionType = VoteBalanceTransactionTypes.CreateVoucher;
                         voteBalance.OfficeId = token.officeId;
                             var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalance);
                        await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);

                        var voucherLine = new VoucherLine
                        {
                            VoteId = settlemnt.VoteDetailId,
                            NetAmount = settlemnt.Amount,
                            TotalAmount = settlemnt.Amount,
                            //PaymentStatus = PaymentStatus.FinalPayment,
                            VoteCode = vote.Code,
                            VoteBalanceId = voteBalance.Id!.Value,
                            CommentOrDescription = settlemnt.Description,
                        };

                    newSettlementVoucher.VoucherLine!.Add(voucherLine);



                    }



                    var voucherItem = new SubVoucherItem
                    {
                        PayeeId = subImprest.EmployeeId,
                        VoucherItemAmount = (decimal)subImprest.SettleByBills,
                        ChequeAmount = settlemetChequeAmount,
                        CommentOrDescription = subImprest.Description,
                        SubVoucherNo = 1,
                    };

                    newSettlementVoucher.SubVoucherItems = new List<SubVoucherItem> { voucherItem };


                    await _unitOfWork.Voucher.AddAsync(newSettlementVoucher);
                    await _unitOfWork.CommitAsync();


                    if (await CreateVoucherLog(newSettlementVoucher, token.userId, "create subimprest voucher"))
                    {
                        await _unitOfWork.CommitAsync();
                    }


                    return (true, "SubImprest Settlement Voucher Saved Successfully", newSettlementVoucher);
               
            }
        }
        catch (Exception e)
        {
            throw;
        }
    }

    public async Task<(bool, string, Voucher)> CreateAccountTransferVoucher(AccountTransfer accountTransfer,
    Session session, HTokenClaim token)
    {
        try
        {
           

            {
                var voteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(accountTransfer.FromVoteDetailId,
                    token.sabhaId, session.StartAt.Year);
                var vote = await _unitOfWork.VoteDetails.GetByIdAsync(accountTransfer.FromVoteDetailId);

                //if (voteBalance == null)
                //{

                //    voteBalance = await _voteBalanceService.CreateNewVoteBalance(accountTransfer.FromVoteBalanceId, session, token);
                //    if (voteBalance == null)
                //    {
                //        throw new Exception("Unable to Create Vote Balance");
                //    }

                //}

                if (voteBalance != null)
                {
                    var newAccountTransferVoucher = new Voucher
                    {
                        //PartnerId = 0,
                        PayeeCategory = VoucherPayeeCategory.InternalOfficer,
                        ActionState = FinalAccountActionStates.Init,
                        VoucherCategory = VoucherCategory.AccountTransfer,
                        SessionId = session.Id,
                        AccountTransferId = accountTransfer.Id,
                        BankId = accountTransfer.FromAccountId,

                        Year = session.StartAt.Year,
                        Month = session.StartAt.Month,
                        CommentOrDescription = accountTransfer.RequestNote!,
                        CreatedAt = session.StartAt,
                        UpdatedAt = session.StartAt,
                        CreatedBy = token.userId,
                        UpdatedBy = token.userId,
                        SabhaId = token.sabhaId,
                        SystemCreateAt = DateTime.Now,
                        VoucherAmount = accountTransfer.Amount,
                        TotalChequeAmount = accountTransfer.Amount,
                        VoucherSequenceNumber = DateTime.Now.ToString("HHmmssfff") + new Random().Next(10),
                    };

                    var voucherLine = new VoucherLine
                    {
                        VoteId = accountTransfer.FromVoteDetailId,
                        NetAmount = accountTransfer.Amount,
                        TotalAmount = accountTransfer.Amount,
                        //PaymentStatus = PaymentStatus.FinalPayment,
                        VoteCode = vote.Code,
                        VoteBalanceId = voteBalance.Id!.Value,
                        CommentOrDescription = accountTransfer.RequestNote,
                    };

                    var voucherItem = new SubVoucherItem
                    {
                        PayeeId = 0,
                        VoucherItemAmount = accountTransfer.Amount,
                        ChequeAmount = accountTransfer.Amount,
                        CommentOrDescription = accountTransfer.RequestNote,
                        SubVoucherNo = 1,
                        VoucherId = newAccountTransferVoucher.Id,
                    };

                    newAccountTransferVoucher.SubVoucherItems = new List<SubVoucherItem> { voucherItem };


                    newAccountTransferVoucher.VoucherLine = new List<VoucherLine> { voucherLine };

                    await _unitOfWork.Voucher.AddAsync(newAccountTransferVoucher);
                    await _unitOfWork.CommitAsync();


                    if (await CreateVoucherLog(newAccountTransferVoucher, token.userId, "create account transfer voucher"))
                    {
                        await _unitOfWork.CommitAsync();
                    }


                    return (true, "account transfer Voucher Saved Successfully", newAccountTransferVoucher);
                }
                else
                {
                    throw new Exception("Unable to find Vote Balance");
                }
            }
           
        }
        catch (Exception e)
        {
            return (false, e.Message, new Voucher());
        }
    }

    public async Task<(bool, string, Voucher)> CreateAccountRefundingVoucher(AccountTransfer accountTransfer,
        AccountTransferRefunding refunding, Session session, HTokenClaim token)
    {
        try
        {
            

            {
                var voteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(accountTransfer.ToVoteDetailId,token.sabhaId, session.StartAt.Year);
                var vote = await _unitOfWork.VoteDetails.GetByIdAsync(accountTransfer.ToVoteDetailId);

                if(vote == null)
                {
                    throw new FinalAccountException("Unable to find Vote");
                }

                //if (voteBalance == null)
                //{

                //    voteBalance = await _voteBalanceService.CreateNewVoteBalance(accountTransfer.FromVoteBalanceId, session, token);
                //    if (voteBalance == null)
                //    {
                //        throw new Exception("Unable to Create Vote Balance");
                //    }

                //}

                if (voteBalance != null)
                {
                    //voteBalance.Credit += refunding.Amount;
                    //voteBalance.TransactionType = VoteBalanceTransactionTypes.CreateVoucher;
                    //voteBalance.OfficeId = token.officeId;
                    //var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalance);
                    //vtbLog.Id = null;
                    //vtbLog.VoteBalanceId = voteBalance.Id!;

                    //await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);

                    var newAccountRefundTransferVoucher = new Voucher
                    {
                        //PartnerId = 0,


                        PayeeCategory = VoucherPayeeCategory.InternalOfficer,
                        AccountTransferId = accountTransfer.Id,
                        BankId = accountTransfer.ToAccountId,

                        ActionState = FinalAccountActionStates.Init,
                        VoucherCategory = VoucherCategory.Refund,
                        SessionId = session.Id,
                        RefundId = refunding.Id,

                        Year = session.StartAt.Year,
                        Month = session.StartAt.Month,
                        CommentOrDescription = refunding.RefundNote!,
                        CreatedAt = session.StartAt,
                        UpdatedAt = session.StartAt,
                        CreatedBy = token.userId,
                        UpdatedBy = token.userId,
                        SabhaId = token.sabhaId,
                        SystemCreateAt = DateTime.Now,
                        VoucherAmount = refunding.Amount,
                        TotalChequeAmount = refunding.Amount,
                        VoucherSequenceNumber = DateTime.Now.ToString("HHmmssfff") + new Random().Next(10),
                    };

                    var voucherLine = new VoucherLine
                    {
                        VoteId = accountTransfer.FromVoteDetailId,
                        NetAmount = refunding.Amount,
                        TotalAmount = refunding.Amount,
                        //PaymentStatus = PaymentStatus.FinalPayment,
                        VoteCode = vote.Code,
                        VoteBalanceId = voteBalance.Id!.Value,
                        CommentOrDescription = refunding.RefundNote,
                    };

                    var voucherItem = new SubVoucherItem
                    {
                        PayeeId = 0,
                        VoucherItemAmount = refunding.Amount,
                        ChequeAmount = refunding.Amount,
                        CommentOrDescription = refunding.RefundNote,
                        SubVoucherNo = 1,
                        VoucherId = newAccountRefundTransferVoucher.Id,
                    };

                    newAccountRefundTransferVoucher.SubVoucherItems = new List<SubVoucherItem> { voucherItem };

                    newAccountRefundTransferVoucher.VoucherLine = new List<VoucherLine> { voucherLine };

                    await _unitOfWork.Voucher.AddAsync(newAccountRefundTransferVoucher);
                    await _unitOfWork.CommitAsync();


                    if (await CreateVoucherLog(newAccountRefundTransferVoucher, token.userId, "create account transfer voucher"))
                    {
                        await _unitOfWork.CommitAsync();
                    }


                    return (true, "account transfer Voucher Saved Successfully", newAccountRefundTransferVoucher);
                }
                else
                {
                    throw new Exception("Unable to find Vote Balance");
                }
            }
           
        }
        catch (Exception e)
        {
            return (false, e.Message, new Voucher());
        }
    }

    public async Task<(bool, string?, Voucher)> CreateOrderRePaymentVoucher(SaveRepaymentVoucher voucherResource, HTokenClaim token) {

        using (var transaction = _unitOfWork.BeginTransaction())
        {
            try
            {
                var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                if (session != null)
                {
                    var mxOrder =
                        await _unitOfWork.MixinOrders.GetByIdAsync(voucherResource.RePaymentOrderId);

                    if (mxOrder != null)
                    {
                        //var sessionDate = await _unitOfWork.Sessions.IsRescueSessionThenDate(session.Id);

                        //foreach (var voucherItem in voucherResource.SubVoucherItems!)
                        //{
                        //    foreach (var voucherCrossOrder in voucherItem.VoucherCrossOrders!)
                        //    {
                        //        if (sessionDate.HasValue)
                        //        {
                        //            voucherCrossOrder.CrossOrder.CreatedAt = (DateTime)sessionDate;

                        //            foreach (var item in voucherCrossOrder.CrossOrder!.MixinOrderLine)
                        //            {
                        //                item.CreatedAt = (DateTime)sessionDate;
                        //            }
                        //        }
                        //        else
                        //        {
                        //            voucherCrossOrder.CrossOrder.CreatedAt = DateTime.Now;

                        //            foreach (var item in voucherCrossOrder.CrossOrder!.MixinOrderLine)
                        //            {
                        //                item.CreatedAt = DateTime.Now;
                        //            }
                        //        }


                        //        voucherCrossOrder.CrossOrder.State = OrderStatus.Cross;

                        //        var mixinOrderToCreate =
                        //            _mapper.Map<SaveCrossOrderResource, MixinOrder>(voucherCrossOrder.CrossOrder);

                        //        await _unitOfWork.MixinOrders.AddAsync(mixinOrderToCreate);
                        //        await _unitOfWork.CommitAsync();

                        //        voucherCrossOrder.CrossOrderId = mixinOrderToCreate.Id;
                        //        voucherCrossOrder.Amount = mixinOrderToCreate.TotalAmount;

                        //    }
                        //}



                        var newVoucher = _mapper.Map<SaveRepaymentVoucher, Voucher>(voucherResource);


                        foreach (var vLine in newVoucher.VoucherLine!)
                        {
                            var voteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(vLine.VoteId, token.sabhaId, session.StartAt.Year);
                            if (voteBalance != null)
                            {
                                vLine.VoteBalanceId = voteBalance.Id!.Value;
                            }
                            else
                            {
                                throw new Exception("Unable to find Vote Balance");
                            }
                        }

                       
                        {
                                newVoucher.ActionState = FinalAccountActionStates.Init;
                                newVoucher.SessionId = session.Id;

                                newVoucher.Year = session.StartAt.Year;
                                newVoucher.Month = session.StartAt.Month;
                                newVoucher.VoucherCategory = VoucherCategory.RePayment;
                            
                                newVoucher.CreatedAt = session.StartAt;
                                newVoucher.UpdatedAt = session.StartAt;
                                newVoucher.CreatedBy = token.userId;
                                newVoucher.UpdatedBy = token.userId;
                            
                                newVoucher.SystemCreateAt = DateTime.Now;

                                newVoucher.Month = session.StartAt.Month;
                                newVoucher.Year = session.StartAt.Year;
                                newVoucher.SabhaId = token.sabhaId;
                                newVoucher.SystemCreateAt = DateTime.Now;

                                newVoucher.VoucherSequenceNumber = DateTime.Now.ToString("HHmmssfff") + new Random().Next(10);


                            foreach (var (item, index) in newVoucher.SubVoucherItems!.Select((item, index) =>
                                             (item, index)))
                                {
                                    item.SubVoucherNo = index + 1;
                                }


                                await _unitOfWork.Voucher.AddAsync(newVoucher);
                                await _unitOfWork.CommitAsync();

                                if (await CreateVoucherLog(newVoucher, token.userId, "create"))
                                {
                                    transaction.Commit();
                                    return (true, "Successfully Repayment Voucher Saved", newVoucher);
                                }
                                else
                                {
                                    throw new Exception("Unable to Create Log");
                          }
                        }
                    }
                    else
                    {
                        throw new FinalAccountException("Unable to find Order For Provide Receipt");
                    }
                }
                else

                {
                    throw new FinalAccountException("Unable To Find Active Session");
                }
            }
            catch (Exception e)
            {
                transaction.Rollback();

                if (e.GetType() == typeof(FinalAccountException))
                {
                    return (false, e.Message, new Voucher());
                }
                else
                {
                    return (false, null, new Voucher());
                }
            }
        }

    }

    public async Task<(bool, string?, Voucher)> AdvancedBVoucher(AdvanceB advanceB,Session session, HTokenClaim token)
    {

        try
        {
            if (session != null)
            {


                var vote = await _unitOfWork.VoteDetails.GetByIdAsync(advanceB.LedgerAccId);

                if (vote == null)
                {
                    throw new  FinalAccountException("Unable To Found Vote");
                }

                var voteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(advanceB.LedgerAccId, token.sabhaId, session.StartAt.Year);

                if (voteBalance == null)
                {
                    voteBalance = await _voteBalanceService.CreateNewVoteBalance(advanceB.LedgerAccId, session, token);


                }

                if (voteBalance == null)
                {
                    throw new FinalAccountException("Unable To Found  Vote Balance");
                }


                var bankAccountId = await _unitOfWork.VoteAssignments.GetAccountIdByVoteIdByOffice(vote.ID!.Value, token);

                if (bankAccountId == 0)
                {
                    throw new FinalAccountException("Unable To Found Bank Account");
                }



                var newAdvancedBVoucher = new Voucher
                {
                    //PartnerId = 0,


                    PayeeCategory = VoucherPayeeCategory.Employee,
                    AdvancedBId = advanceB.Id,
                    BankId = bankAccountId,

                    ActionState = FinalAccountActionStates.Init,
                    VoucherCategory = VoucherCategory.AdvancedB,
                    SessionId = session.Id,

                    Year = session.StartAt.Year,
                    Month = session.StartAt.Month,
                    CommentOrDescription = advanceB.Description!,
                    CreatedAt = session.StartAt,
                    UpdatedAt = session.StartAt,
                    CreatedBy = token.userId,
                    UpdatedBy = token.userId,
                    SabhaId = token.sabhaId,
                    SystemCreateAt = DateTime.Now,
                    VoucherAmount = advanceB.Amount,
                    CrossAmount = 0,
                    TotalChequeAmount = advanceB.Amount,
                    VoucherSequenceNumber = DateTime.Now.ToString("HHmmssfff") + new Random().Next(10),
                };

                var voucherLine = new VoucherLine
                {
                    VoteId = advanceB.LedgerAccId,
                    NetAmount = advanceB.Amount,
                    TotalAmount = advanceB.Amount,
                    //PaymentStatus = PaymentStatus.FinalPayment,
                    VoteCode = vote.Code,
                    VoteBalanceId = voteBalance.Id!.Value,
                    CommentOrDescription = advanceB.Description,
                };

                var voucherItem = new SubVoucherItem
                {
                    PayeeId = advanceB.EmployeeId,
                    VoucherItemAmount = advanceB.Amount,
                    ChequeAmount = advanceB.Amount,
                    CommentOrDescription = advanceB.Description,
                    SubVoucherNo = 1,
                    VoucherId = newAdvancedBVoucher.Id,
                };


                newAdvancedBVoucher.VoucherLine = new List<VoucherLine> { voucherLine };
                newAdvancedBVoucher.SubVoucherItems = new List<SubVoucherItem> { voucherItem };

                
                {

                    foreach (var (item, index) in newAdvancedBVoucher.SubVoucherItems!.Select((item, index) =>
                                     (item, index)))
                    {
                        item.SubVoucherNo = index + 1;
                    }


                    await _unitOfWork.Voucher.AddAsync(newAdvancedBVoucher);
                    await _unitOfWork.CommitAsync();

                    if (await CreateVoucherLog(newAdvancedBVoucher, token.userId, "create"))
                    {
                        return (true, "Successfully Repayment Voucher Saved", newAdvancedBVoucher);
                    }
                    else
                    {
                        throw new Exception("Unable to Create Log");
                    }
                }

            }
            else

            {
                throw new FinalAccountException("Unable To Find Active Session");
            }
        }
        catch (Exception e)
        {

            throw; 


        }
    }


    public async Task<(bool, string?, Voucher)> CreateSalaryVoucher(SaveSalaryVoucher voucherResource, HTokenClaim token)
    {

        using (var transaction = _unitOfWork.BeginTransaction())
        {
            try
            {
                var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                if (session != null)
                {



                    var sessionDate = await _unitOfWork.Sessions.IsRescueSessionThenDate(session.Id);

                    foreach (var voucherItem in voucherResource.SubVoucherItems!)
                    {
                        foreach (var voucherCrossOrder in voucherItem.VoucherCrossOrders!)
                        {
                            if (sessionDate.HasValue)
                            {
                                voucherCrossOrder.CrossOrder.CreatedAt = (DateTime)sessionDate;

                                foreach (var item in voucherCrossOrder.CrossOrder!.MixinOrderLine)
                                {
                                    item.CreatedAt = (DateTime)sessionDate;
                                }
                            }
                            else
                            {
                                voucherCrossOrder.CrossOrder.CreatedAt = DateTime.Now;

                                foreach (var item in voucherCrossOrder.CrossOrder!.MixinOrderLine)
                                {
                                    item.CreatedAt = DateTime.Now;
                                }
                            }


                            voucherCrossOrder.CrossOrder.State = OrderStatus.Cross;

                            var mixinOrderToCreate =
                                _mapper.Map<SaveCrossOrderResource, MixinOrder>(voucherCrossOrder.CrossOrder);

                            await _unitOfWork.MixinOrders.AddAsync(mixinOrderToCreate);
                            await _unitOfWork.CommitAsync();

                            voucherCrossOrder.CrossOrderId = mixinOrderToCreate.Id;
                            voucherCrossOrder.Amount = mixinOrderToCreate.TotalAmount;

                        }
                    }



                    var newVoucher = _mapper.Map<SaveSalaryVoucher, Voucher>(voucherResource);


                        foreach (var vLine in newVoucher.VoucherLine!)
                        {
                            var voteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(vLine.VoteId, token.sabhaId, session.StartAt.Year);
                            if (voteBalance != null)
                            {
                                voteBalance.TotalCommitted += vLine.TotalAmount;
                                vLine.VoteBalanceId = voteBalance.Id!.Value;


                                voteBalance.UpdatedAt = session.StartAt;
                                voteBalance.UpdatedBy =  token.userId;
                                voteBalance.SystemActionAt = DateTime.Now;
                                voteBalance.OfficeId = token.officeId;
                                voteBalance.SessionIdByOffice = session.Id;
                                var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalance);
                            }
                            else
                            {
                                throw new Exception("Unable to find Vote Balance");
                            }
                        }

                    var bankAccountId = await _unitOfWork.VoteAssignments.GetAccountIdByVoteIdByOffice(newVoucher.VoucherLine.FirstOrDefault()!.VoteId, token);

                    if (bankAccountId == 0)
                    {
                        throw new FinalAccountException("Unable To Found Bank Account");
                    }


                    {
                            newVoucher.ActionState = FinalAccountActionStates.Init;
                            newVoucher.SessionId = session.Id;
                            newVoucher.PayeeCategory = VoucherPayeeCategory.Agent;
                            newVoucher.VoucherCategory = VoucherCategory.Salary;
                            newVoucher.BankId = bankAccountId;    

                            newVoucher.CreatedAt = session.StartAt;
                            newVoucher.UpdatedAt = session.StartAt;
                            newVoucher.CreatedBy = token.userId;
                            newVoucher.UpdatedBy = token.userId;

                            newVoucher.SystemCreateAt = DateTime.Now;

                            newVoucher.Month = session.StartAt.Month;
                            newVoucher.Year = session.StartAt.Year;
                            newVoucher.SabhaId = token.sabhaId;
                            newVoucher.SystemCreateAt = DateTime.Now;

                            newVoucher.VoucherSequenceNumber = DateTime.Now.ToString("HHmmssfff") + new Random().Next(10);


                            foreach (var (item, index) in newVoucher.SubVoucherItems!.Select((item, index) =>
                                             (item, index)))
                            {
                                item.SubVoucherNo = index + 1;
                            }


                            await _unitOfWork.Voucher.AddAsync(newVoucher);
                            await _unitOfWork.CommitAsync();

                            if (await CreateVoucherLog(newVoucher, token.userId, "create"))
                            {
                                transaction.Commit();
                                return (true, "Successfully Repayment Voucher Saved", newVoucher);
                            }
                            else
                            {
                                throw new Exception("Unable to Create Log");
                            }
                        }
                    
                }
                else

                {
                    throw new FinalAccountException("Unable To Find Active Session");
                }
            }
            catch (Exception e)
            {
                transaction.Rollback();

                if (e.GetType() == typeof(FinalAccountException))
                {
                    return (false, e.Message, new Voucher());
                }
                else
                {
                    return (false, null, new Voucher());
                }
            }
        }

    }
    public async Task<(int totalCount, IEnumerable<VoucherResource> list)> getVoucherForApproval(int sabhaId,
        List<int?> excludedIds, int? category, int stage, int pageNo,
        int pageSize, string? filterKeyWord)
    {
        try
        {
            var vouchers = await _unitOfWork.Voucher.getVoucherForApproval(sabhaId, excludedIds, category, stage,
                pageNo, pageSize, filterKeyWord);


            var voucherResource = _mapper.Map<IEnumerable<Voucher>, IEnumerable<VoucherResource>>(vouchers.list);
            
            foreach (var voucher in voucherResource)
            {
                
                voucher.UserActionBy =
                    _mapper.Map<UserDetail, FinalUserActionByResources>(
                        await _userDetailService.GetUserDetailById(voucher.CreatedBy!.Value));
            }



            return (vouchers.totalCount, voucherResource);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


    public async Task<(int totalCount, IEnumerable<VoucherResource> list)> searchVoucherByKeywordForSurcharge(int sabhaId,
     int pageNo,
    int pageSize, string? filterKeyWord)
    {
        try
        {
            var vouchers = await _unitOfWork.Voucher.searchVoucherByKeywordForSurcharge(sabhaId,  pageNo, pageSize, filterKeyWord);


            var voucherResource = _mapper.Map<IEnumerable<Voucher>, IEnumerable<VoucherResource>>(vouchers.list);

            foreach (var voucher in voucherResource)
            {

                voucher.UserActionBy =
                    _mapper.Map<UserDetail, FinalUserActionByResources>(
                        await _userDetailService.GetUserDetailById(voucher.CreatedBy!.Value));
            }



            return (vouchers.totalCount, voucherResource);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<(int totalCount, IEnumerable<VoucherResource> list)> getVoucherProgressRejected(int sabhaId,
        List<int?> stages, int pageNo,
        int pageSize, string? filterKeyWord)
    {
        try
        {
            var voucherForApproval =
                await _unitOfWork.Voucher.getVoucherProgressRejected(sabhaId, stages, pageNo, pageSize, filterKeyWord);


            var voucherResource =
                _mapper.Map<IEnumerable<Voucher>, IEnumerable<VoucherResource>>(voucherForApproval.list);


            //foreach (var voucher in voucherResource)
            //{
            //    voucher.VendorAccount =
            //        _mapper.Map<Partner, VendorResource>(await _partnerService.GetById(voucher.PartnerId));
            //    voucher.UserActionBy =
            //        _mapper.Map<UserDetail, FinalUserActionByResources>(
            //            await _userDetailService.GetUserDetailById(voucher.CreatedBy!.Value));
            //}

            foreach (var voucher in voucherResource)
            {

                voucher.UserActionBy =
                    _mapper.Map<UserDetail, FinalUserActionByResources>(
                        await _userDetailService.GetUserDetailById(voucher.CreatedBy!.Value));
            }

            return (voucherForApproval.totalCount, voucherResource);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<VoucherResource> GetVoucherById(int id)
    {
        var voucher = await _unitOfWork.Voucher.getVoucherById(id);

        var voucherResource = _mapper.Map<Voucher, VoucherResource>(voucher);

        if (voucherResource.VoucherCategory == VoucherCategory.VoteLedger || voucherResource.VoucherCategory == VoucherCategory.RePayment)
        {

            if (voucherResource.PayeeCategory is VoucherPayeeCategory.Partner or VoucherPayeeCategory.Business)
            {
                foreach (var item in voucherResource.SubVoucherItems!)
                {
                    item.Payee = _mapper.Map<Partner, PayeeResources>(await _partnerService.GetById(item.PayeeId));
                }

            }

            if (voucherResource.PayeeCategory is VoucherPayeeCategory.Employee)
            {
                foreach (var item in voucherResource.SubVoucherItems!)
                {
                    item.Payee = _mapper.Map<Employee, PayeeResources>(await _employeeService.GetEmployeeById(item.PayeeId));
                }

            }
        }

        if (voucherResource.VoucherCategory == VoucherCategory.SubImprest || voucherResource.VoucherCategory == VoucherCategory.Settlement)
        {


            if (voucherResource.PayeeCategory is VoucherPayeeCategory.Employee)
            {
                foreach (var item in voucherResource.SubVoucherItems!)
                {
                    item.Payee = _mapper.Map<Employee, PayeeResources>(await _employeeService.GetEmployeeById(item.PayeeId));
                }

            }
        }

        if (voucherResource.VoucherCategory == VoucherCategory.AdvancedB)
        {


            if (voucherResource.PayeeCategory is VoucherPayeeCategory.Employee)
            {
                foreach (var item in voucherResource.SubVoucherItems!)
                {
                    item.Payee = _mapper.Map<Employee, PayeeResources>(await _employeeService.GetEmployeeById(item.PayeeId));
                }

            }
        }

        if (voucherResource.VoucherCategory == VoucherCategory.Salary)
        {
            foreach (var item in voucherResource.SubVoucherItems!)
            {
                item.Payee = _mapper.Map<Agents, PayeeResources>(await _agentsService.GetAgentByIdWithBankInfo(item.PayeeId));
            }
        }


        if (voucherResource.BankId != null)
        {
            //voucherResource.CrossOrder =
            //    await _unitOfWork.MixinOrders.GetCrossOrderById(voucherResource.BankId.Value);
            //if (voucherResource.CrossOrder != null)
            //{
            //    foreach (var item in voucherResource.CrossOrder!.MixinOrderLine)
            //    {
            //        var voteAssignDetails =
            //            await _unitOfWork.VoteAssignmentDetails.GetForCrossOrder(item.MixinVoteAssignmentDetailId);

            //        if (voteAssignDetails != null)
            //        {
            //            var vote = await _unitOfWork.VoteDetails.GetByIdAsync(voteAssignDetails.voteAssignment!.VoteId);
            //            if (vote != null)
            //            {
            //                voucherResource.CrossOrderVoteCodes!.Add(vote.Code);
            //            }
            //        }
            //    }

            //    //voucherResource.CrossOrder.CreatedBy = await _userDetailService.GetUserDetailById(voucherResource.CrossOrder.CreatedBy!.Value);
            //}
        }

        if (voucherResource.VoucherCategory == VoucherCategory.Deposit)
        {
            foreach (var item in voucherResource.SubVoucherItems!)
            {

                item.Payee = _mapper.Map<Partner, PayeeResources>(await _partnerService.GetById(item.PayeeId));
            }

            foreach (var depositForVoucher in voucherResource.DepositsForVoucher!)
            {
                depositForVoucher.Deposit =
                    _mapper.Map<Deposit, DepositResource>(
                        await _unitOfWork.Deposits.GetByIdAsync(depositForVoucher.DepositId));
                depositForVoucher.Deposit.Partner =
                    _mapper.Map<Partner, VendorResource>(
                        await _partnerService.GetById(depositForVoucher.Deposit!.PartnerId));
                depositForVoucher.Deposit.VoteDetail =
                    _mapper.Map<VoteDetail, VoteDetailLimitedresource>(
                        await _unitOfWork.VoteDetails.GetByIdAsync(depositForVoucher.Deposit.LedgerAccountId));
            }
        }

        if (voucherResource.VoucherCategory == VoucherCategory.AccountTransfer)
        { 
            voucherResource.AccountTransfer = _mapper.Map<AccountTransfer, AccountTransferResource>(await _unitOfWork.AccountTransfer.GetAccountTransferById(voucherResource.AccountTransferId!.Value));


        }

        foreach (var item in voucherResource.SubVoucherItems!) { 
        
            item.PayeeCategory = voucherResource.PayeeCategory;
        }


            return voucherResource;
    }

    public async Task<bool> MakeApproval(MakeVoucherApproveRejectResource request, HTokenClaim token)
    {
        using (var transaction = _unitOfWork.BeginTransaction())
        {
            try
            {
                var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);

                if (session != null)
                {
                    if (Enum.IsDefined(typeof(FinalAccountActionStates), request.State))
                    {
                        var voucherById = await _unitOfWork.Voucher.getVoucherById(request.VoucherId);

                        if (voucherById != null)
                        {
                            voucherById.ActionState = (FinalAccountActionStates)request.State;

                            if (voucherById.ActionState is FinalAccountActionStates.Decertification
                                or FinalAccountActionStates.Discouragement or FinalAccountActionStates.CCRejected or FinalAccountActionStates.Disapproval)
                            {
                                if (voucherById.VoucherCategory == VoucherCategory.VoteLedger)
                                {

                                    foreach (var voucherLine in voucherById.VoucherLine!)
                                    {
                                        var voteBalance =
                                            await _unitOfWork.VoteBalances.GetByIdAsync(voucherLine.VoteBalanceId);

                                        if (voteBalance != null)
                                        {
                                            voteBalance.TotalCommitted -= voucherLine.TotalAmount;

                                            if (voucherById.ActionState is FinalAccountActionStates.Discouragement)
                                            {
                                                voteBalance.TransactionType =
                                                    VoteBalanceTransactionTypes.SecretaryAction;
                                            }
                                            else if (voucherById.ActionState is FinalAccountActionStates.CCRejected)
                                            {
                                                voteBalance.TransactionType = VoteBalanceTransactionTypes.CCAction;
                                            }
                                            else if (voucherById.ActionState is FinalAccountActionStates
                                                                                                        .Decertification)
                                            {
                                                voteBalance.TransactionType = VoteBalanceTransactionTypes.ChairmanAction;
                                            }
                                            else if (voucherById.ActionState is FinalAccountActionStates
                                                                                                       .Disapproval)
                                            {
                                                voteBalance.TransactionType = VoteBalanceTransactionTypes.Disapproval;
                                            }
                                            else
                                            {
                                                throw new Exception("Invalid State");
                                            }
                                            voteBalance.OfficeId = token.officeId;

                                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalance);
                                            voteBalance.OfficeId = token.officeId;
                                            voteBalance.UpdatedBy = token.userId;
                                            voteBalance.UpdatedAt = session.StartAt;
                                            voteBalance.SystemActionAt = DateTime.Now;
                                            voteBalance.SessionIdByOffice = session.Id;

                                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                                        }
                                        else
                                        {
                                            throw new Exception("Unable to find Vote Balance");
                                        }
                                    }


                                }

                                if (voucherById.VoucherCategory == VoucherCategory.Deposit)
                                {
                                    foreach (var deposit in voucherById.DepositsForVoucher!)
                                    {
                                        var depositById = await _unitOfWork.Deposits.GetByIdAsync(deposit.Id);
                                        if (depositById != null)
                                        {
                                            depositById.HoldAmount -= deposit.Amount;
                                        }
                                        else
                                        {
                                            throw new Exception("Unable to find Deposit");
                                        }
                                    }

                                    foreach (var voucherLine in voucherById.VoucherLine!)
                                    {
                                        var voteBalance =
                                            await _unitOfWork.VoteBalances.GetByIdAsync(voucherLine.VoteBalanceId);

                                        if (voteBalance != null)
                                        {


                                            if (voucherById.ActionState is FinalAccountActionStates.Discouragement)
                                            {
                                                voteBalance.TransactionType =
                                                    VoteBalanceTransactionTypes.SecretaryAction;
                                            }
                                            else if (voucherById.ActionState is FinalAccountActionStates.CCRejected)
                                            {
                                                voteBalance.TransactionType = VoteBalanceTransactionTypes.CCAction;
                                            }
                                            else if (voucherById.ActionState is FinalAccountActionStates
                                                         .Decertification)
                                            {
                                                voteBalance.TransactionType = VoteBalanceTransactionTypes.ChairmanAction;
                                            }
                                            else
                                            {
                                                throw new Exception("Invalid State");
                                            }
                                            voteBalance.OfficeId = token.officeId;
                                            voteBalance.UpdatedBy = token.userId;
                                            voteBalance.UpdatedAt = session.StartAt;
                                            voteBalance.SystemActionAt = DateTime.Now;
                                            voteBalance.SessionIdByOffice = session.Id;


                                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalance);
                                         

                                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                                        }
                                        else
                                        {
                                            throw new Exception("Unable to find Vote Balance");
                                        }
                                    }
                                }
                            }


                            voucherById.UpdatedAt = session.StartAt;
                            voucherById.UpdatedBy = token.userId;

                            await _unitOfWork.CommitAsync();

                            if (await CreateVoucherLog(voucherById, token.userId, request.ActionNote))
                            {
                                transaction.Commit();
                                return true;
                            }
                            else
                            {
                                throw new Exception("Unable to Create Log");
                            }

                            ;
                        }
                        else
                        {
                            throw new Exception("Voucher Not Found");
                        }
                    }
                    else
                    {
                        throw new Exception("Invalid State");
                    }
                }
                else
                {
                    throw new Exception("Unable to find Active Session");
                }
            }
            catch (Exception e)
            {
                transaction.Rollback();
                return false;
            }
        }
    }


    public async Task<(bool,string)> PostSettlement(Voucher Voucher, HTokenClaim token)
    {
        using (var transaction = _unitOfWork.BeginTransaction())
        {
            try
            {
                var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);

                if (session != null)
                {

                    var voucherById = await _unitOfWork.Voucher.getVoucherById(Voucher.Id!.Value);


                    if (voucherById != null)
                    {
                        var docSeqNum = await _unitOfWork.FinalAccountSequenceNumber.GetNextSequenceNumberForYearSabhaModuleType(session.CreatedAt.Year, token.sabhaId, FinalAccountModuleType.Voucher);
                        if (docSeqNum != null)
                        {
                            voucherById.VoucherSequenceNumber = $"{voucherById.SabhaId}/{docSeqNum.Year}/{docSeqNum.Prefix}/{++docSeqNum.LastIndex}";
                        }
                        else
                        {
                            throw new FinalAccountException("Unable to find Sequence Number");
                        }


                        if (0 < voucherById.StampTotal)
                        {
                            var stampVote = await _unitOfWork.SpecialLedgerAccounts.GetStampLedgerAccount(token.sabhaId);

                            if (stampVote == null)
                            {
                                throw new FinalAccountException("Unable to find Stamp Vote");
                            }
                            var stampVoteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(stampVote.VoteId!.Value, token.sabhaId, session.StartAt.Year);
                            if (stampVoteBalance != null)
                            {
                                stampVoteBalance.Debit += (decimal)voucherById.StampTotal;
                                stampVoteBalance.ExchangedAmount = (decimal)voucherById.StampTotal;
                                stampVoteBalance.CreditDebitRunningBalance = stampVoteBalance.Debit - stampVoteBalance.Credit;
                                stampVoteBalance.UpdatedAt = session.StartAt;
                                stampVoteBalance.UpdatedBy = token.userId;
                                stampVoteBalance.SystemActionAt = DateTime.Now;
                                stampVoteBalance.OfficeId = token.officeId;
                                stampVoteBalance.SessionIdByOffice = session.Id;

                                var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(stampVoteBalance);

                                await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                            }
                            else
                            {
                                throw new FinalAccountException("Unable to find Stamp Vote Balance");
                            }

                        }


                        foreach (var deposit in voucherById.DepositsForVoucher!)
                        {
                            var depositById = await _unitOfWork.Deposits.GetByIdAsync(deposit.DepositId);
                            if (depositById != null)
                            {
                                depositById.HoldAmount -= deposit.Amount;
                                depositById.ReleasedAmount += deposit.Amount;
                                depositById.IsEditable = false;
                                depositById.UpdatedBy = token.userId;
                                depositById.UpdatedAt = session.StartAt;
                                depositById.SystemUpdateAt = DateTime.Now;
                            }
                            else
                            {
                                throw new FinalAccountException("Unable to find Deposit");
                            }
                        }

                        await _unitOfWork.CommitAsync();


                        foreach (var voucherLine in voucherById.VoucherLine!)
                        {
                            var voteBalance =
                                await _unitOfWork.VoteBalances.GetActiveVoteBalance(voucherLine.VoteId,
                                    token.sabhaId, session.StartAt.Year);

                            if (voteBalance != null)
                            {
                                voteBalance.TransactionType = VoteBalanceTransactionTypes.CreateCheque;
                                voteBalance.Code = "VOUCHER";

                                if (voteBalance.ClassificationId == 1)
                                {
                                    voteBalance.Credit += voucherLine.TotalAmount;
                                    voteBalance.ExchangedAmount = voucherLine.TotalAmount;

                                    voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;
                                    //voteBalance.RunningBalance = (decimal)voteBalance.AllocationAmount! - voteBalance.CreditDebitRunningBalance;

                                    voteBalance.SubCode = voucherById.VoucherSequenceNumber;

                                    voteBalance.UpdatedAt = session.StartAt;
                                    voteBalance.UpdatedBy = token.userId;
                                    voteBalance.SystemActionAt = DateTime.Now;



                                    voteBalance.OfficeId = token.officeId;
                                    var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalance);
                                    vtbLog.AppCategory = AppCategory.Expenditure;
                                    vtbLog.ModulePrimaryKey = voucherById.Id;
                                    await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);

                                }

                                if (voteBalance.ClassificationId == 2)
                                {

                                    if (voteBalance.TotalCommitted >= voucherLine.TotalAmount)
                                    {

                                        voteBalance.TotalCommitted -= voucherLine.TotalAmount;
                                    }
                                    else
                                    {
                                        throw new FinalAccountException("Insufficient Vote Balance");

                                    }


                                    voteBalance.Debit += voucherLine.TotalAmount;
                                    voteBalance.ExchangedAmount = voucherLine.TotalAmount;

                                    voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;
                                    voteBalance.RunningBalance = (decimal)voteBalance.AllocationAmount! - voteBalance.CreditDebitRunningBalance;

                                    voteBalance.SubCode = voucherById.VoucherSequenceNumber;

                                    voteBalance.UpdatedAt = session.StartAt;
                                    voteBalance.UpdatedBy = token.userId;
                                    voteBalance.SystemActionAt = DateTime.Now;
                                    voteBalance.SessionIdByOffice = session.Id;


                                    voucherLine.RptExpenditure = voteBalance.Debit - voteBalance.Credit;
                                    voucherLine.RptBalance = (decimal)voteBalance.AllocationAmount! - voucherLine.RptExpenditure;
                                    voucherLine.RptBudgetAllocation = (decimal)voteBalance.AllocationAmount!;



                                    //var vtb = new VoteLedgerBook
                                    //{
                                    //    SabhaId = token.sabhaId,
                                    //    OfiiceId = token.officeId,
                                    //    SessionId = session.Id,
                                    //    Description = voucherById.VoucherSequenceNumber,
                                    //    Date = session.StartAt,
                                    //    VoteBalanceId = (int)voteBalance.Id!,
                                    //    VoteDetailId = (int)voteBalance.VoteDetailId!,
                                    //    TransactionType = CashBookTransactionType.CREDIT,
                                    //    VoteBalanceTransactionTypes = VoteBalanceTransactionTypes.CreateCheque,
                                    //    Credit = 0,
                                    //    Debit = voteBalance.ExchangedAmount,
                                    //    RunningTotal = voteBalance.CreditDebitRunningBalance,
                                    //    RowStatus = 1,
                                    //    CreatedAt = session.StartAt,
                                    //    UpdatedAt = session.StartAt,
                                    //    CreatedBy = token.userId,
                                    //    UpdatedBy = token.userId,
                                    //    SystemActionDate = DateTime.Now
                                    //};

                                    //await _unitOfWork.VoteLedgerBook.AddAsync(vtb);
                                    voteBalance.OfficeId = token.officeId;
                                    var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalance);
                                    vtbLog.AppCategory = AppCategory.Expenditure;
                                    vtbLog.ModulePrimaryKey = voucherById.Id;
                                    await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                                }

                                if (voteBalance.ClassificationId == 3)
                                {
                                    voteBalance.Debit += voucherLine.TotalAmount;
                                    voteBalance.ExchangedAmount = voucherLine.TotalAmount;

                                    voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;
                                    //voteBalance.RunningBalance = (decimal)voteBalance.AllocationAmount! - voteBalance.CreditDebitRunningBalance;

                                    voteBalance.SubCode = voucherById.VoucherSequenceNumber;

                                    voteBalance.UpdatedAt = session.StartAt;
                                    voteBalance.UpdatedBy = token.userId;
                                    voteBalance.SystemActionAt = DateTime.Now;
                                    voteBalance.OfficeId = token.officeId;
                                    var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalance);
                                    vtbLog.AppCategory = AppCategory.Expenditure;
                                    vtbLog.ModulePrimaryKey = voucherById.Id;
                                    await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);

                                }

                                if (voteBalance.ClassificationId == 4)
                                {
                                    voteBalance.Debit += voucherLine.TotalAmount;
                                    voteBalance.ExchangedAmount = voucherLine.TotalAmount;

                                    voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;
                                    //voteBalance.RunningBalance = (decimal)voteBalance.AllocationAmount! - voteBalance.CreditDebitRunningBalance;

                                    voteBalance.SubCode = voucherById.VoucherSequenceNumber;

                                    voteBalance.UpdatedAt = session.StartAt;
                                    voteBalance.UpdatedBy = token.userId;
                                    voteBalance.SystemActionAt = DateTime.Now;
                                    voteBalance.OfficeId = token.officeId;
                                    var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalance);
                                    vtbLog.AppCategory = AppCategory.Expenditure;
                                    vtbLog.ModulePrimaryKey = voucherById.Id;
                                    await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);

                                }

                            }
                            else
                            {
                                throw new Exception("Unable to find Vote Balance");
                            }
                        }


                        foreach (var voucherItem in voucherById.SubVoucherItems!)
                        {
                            foreach (var voucherCrossOrder in voucherItem.VoucherCrossOrders!)
                            {

                                var mixinOrder = await _unitOfWork.MixinOrders.GetCrossOrderById(voucherCrossOrder.CrossOrderId!.Value);
                                if (mixinOrder != null)
                                {
                                    string prefix = "MIX";

                                    var docSeqNums = await _unitOfWork.DocumentSequenceNumbers
                                        .GetNextSequenceNumberForYearOfficePrefixAsync(mixinOrder.CreatedAt.Year,
                                            mixinOrder.OfficeId.Value, "MIX");

                                    var office = await _unitOfWork.Offices.GetByIdAsync(mixinOrder.OfficeId);


                                    mixinOrder.Code =
                                        $"{office.Code}/{DateTime.Now.Year}/{"MIX"}/{++docSeqNums.LastIndex}";
                                    mixinOrder.PaymentMethodId = 3;
                                    mixinOrder.State = OrderStatus.Posted;
                                    mixinOrder.CashierId = token.userId;


                                    foreach (var item in mixinOrder!.MixinOrderLine)
                                    {


                                        var voteAssignmentDetail = await _unitOfWork.CustomVoteDetails.GetByIdAsync(item.MixinVoteAssignmentDetailId);

                                        if (voteAssignmentDetail == null)
                                        {
                                            throw new FinalAccountException("Unable to find Vote Assignment Detail");
                                        }

                                        var voteAssignment = await _unitOfWork.VoteAssignments.GetByIdAsync(voteAssignmentDetail.VoteAssignmentId);


                                        if (voteAssignment != null)
                                        {
                                            item.VoteDetailId = voteAssignment.VoteId;
                                        }
                                        else
                                        {
                                            throw new FinalAccountException("Unable to find Vote Assignment");
                                        }

                                    }

                                    if (await UpdateVoteBalance(mixinOrder, session, CashBookTransactionType.DEBIT, CashBookIncomeCategory.Mix, token))
                                    {
                                        //await _unitOfWork.CommitAsync();
                                    }
                                    else
                                    {
                                        throw new FinalAccountException("Vote Balance Entry Not updated.");
                                    }


                                    if (await CreateCashBookEntry(mixinOrder, session, CashBookTransactionType.DEBIT, CashBookIncomeCategory.Mix, token))
                                    {
                                        //await _unitOfWork.CommitAsync();
                                    }
                                    else
                                    {
                                        throw new Exception("Cashbook entry not created.");
                                    }

                                }
                                else
                                {
                                    throw new FinalAccountException("Unable to find CrossOrder");
                                }
                            }
                        }

                        await _unitOfWork.CommitAsync();


                        if (voucherById.VoucherCategory == VoucherCategory.AccountTransfer)
                        {

                           throw new FinalAccountException("Account Transfer Voucher Can't Be Post Settle");

                        }


                        if (voucherById.VoucherCategory == VoucherCategory.Refund)
                        {

                            throw new FinalAccountException("Account Transfer Refund Voucher Can't Be Post Settle");
                        }


                        if (voucherById.VoucherCategory == VoucherCategory.Settlement)
                        {
                            if (voucherById != null && voucherById.VoucherCategory == VoucherCategory.Settlement && voucherById.TotalChequeAmount == 0)
                            {

                                voucherById.ActionState = FinalAccountActionStates.HasCheque;

                                var subImprest = await _unitOfWork.SubImprests.GetSubImprestById(voucherById.SubImprestId!.Value);


                                if (subImprest != null)
                                {
                                    subImprest.ActionStates = FinalAccountActionStates.Settled;

                                    if (subImprest.SettleByCash > 0)
                                    {
                                        if ((subImprest.SettleByBills < subImprest.Amount) && (subImprest.SettleByCash + subImprest.SettleByBills > subImprest.Amount))
                                        {


                                            decimal? revenueAmount = subImprest.SettleByCash - (subImprest.Amount - subImprest.SettleByBills);

                                            var otherReciptLegerAccount = await _unitOfWork.SpecialLedgerAccounts.GetOtherReceiptsLedgerAccount(token.sabhaId);
                                            if (otherReciptLegerAccount == null)
                                            {

                                                throw new FinalAccountException("Unable to find Other Receipts Ledger Account");
                                            }

                                            var voteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(otherReciptLegerAccount.VoteId!.Value, token.sabhaId, session.StartAt.Year);

                                            if (voteBalance == null)
                                            {

                                                voteBalance = await _voteBalanceService.CreateNewVoteBalance(otherReciptLegerAccount.VoteId!.Value, session, token);
                                                if (voteBalance == null)
                                                {
                                                    throw new Exception("Unable to Create Other Receipts Vote Balance");
                                                }

                                            }

                                            if (voteBalance != null)
                                            {
                                                voteBalance.Credit += (decimal)revenueAmount;
                                                voteBalance.TransactionType = VoteBalanceTransactionTypes.Income;
                                                voteBalance.ExchangedAmount = (decimal)revenueAmount;
                                                voteBalance.OfficeId = token.officeId;
                                                voteBalance.SessionIdByOffice = session.Id;
                                                voteBalance.UpdatedAt = session.StartAt;
                                                voteBalance.UpdatedBy = token.userId;
                                                voteBalance.SystemActionAt = DateTime.Now;
                                                voteBalance.Code = "Other Receipts From Imprest JNL";

                                                voteBalance.CreditDebitRunningBalance = voteBalance.Credit - voteBalance.Debit;

                                                var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalance);

                                                vtbLog.AppCategory = AppCategory.SubImprest;
                                                vtbLog.ModulePrimaryKey = subImprest.Id!.Value;

                                                await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);

                                            }
                                            else
                                            {
                                                throw new FinalAccountException("Unable to find Other Receipts Vote Balance ");
                                            }

                                            var imprestVoteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(subImprest.SubImprestVoteId!, token.sabhaId, session.StartAt.Year);

                                            if (imprestVoteBalance != null)
                                            {
                                                imprestVoteBalance.Debit += (decimal)revenueAmount;
                                                imprestVoteBalance.TransactionType = VoteBalanceTransactionTypes.Income;
                                                imprestVoteBalance.ExchangedAmount = (decimal)revenueAmount;
                                                imprestVoteBalance.OfficeId = token.officeId;
                                                imprestVoteBalance.SessionIdByOffice = session.Id;
                                                imprestVoteBalance.UpdatedAt = session.StartAt;
                                                imprestVoteBalance.UpdatedBy = token.userId;
                                                imprestVoteBalance.SystemActionAt = DateTime.Now;
                                                imprestVoteBalance.Code = "Imprest Settlement For Other Receipts JNL";

                                                imprestVoteBalance.CreditDebitRunningBalance = imprestVoteBalance.Debit - imprestVoteBalance.Credit;
                                                var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(imprestVoteBalance);

                                                vtbLog.AppCategory = AppCategory.SubImprest;
                                                vtbLog.ModulePrimaryKey = subImprest.Id!.Value;

                                                await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);

                                            }
                                            else
                                            {
                                                throw new FinalAccountException("Unable to find  Imprest Vote Balance");
                                            }



                                        }
                                        else if ((subImprest.SettleByBills < subImprest.Amount) && (subImprest.SettleByCash + subImprest.SettleByBills == subImprest.Amount))
                                        {

                                        }
                                        else
                                        {
                                            throw new FinalAccountException("Invalid  Post Settlement ");
                                        }
                                    }

                                    {
                                        var imprestVoteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(subImprest.SubImprestVoteId!, token.sabhaId, session.StartAt.Year);

                                        if (imprestVoteBalance != null)
                                        {

                                            imprestVoteBalance.Credit += subImprest.SettleByBills!.Value;
                                            imprestVoteBalance.TransactionType = VoteBalanceTransactionTypes.CreateCheque;
                                            imprestVoteBalance.ExchangedAmount = (decimal)subImprest.SettleByBills!.Value;
                                            imprestVoteBalance.OfficeId = token.officeId;
                                            imprestVoteBalance.SessionIdByOffice = session.Id;
                                            imprestVoteBalance.UpdatedAt = session.StartAt;
                                            imprestVoteBalance.UpdatedBy = token.userId;
                                            imprestVoteBalance.SystemActionAt = DateTime.Now;
                                            imprestVoteBalance.Code = "Imprest Settlement";
                                            imprestVoteBalance.SubCode = voucherById.VoucherSequenceNumber;

                                            imprestVoteBalance.CreditDebitRunningBalance = imprestVoteBalance.Debit - imprestVoteBalance.Credit;
                                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(imprestVoteBalance);

                                            vtbLog.AppCategory = AppCategory.SubImprest;
                                            vtbLog.ModulePrimaryKey = subImprest.Id!.Value;

                                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);

                                        }
                                        else
                                        {
                                            throw new FinalAccountException("Unable to find Vote Balance For Other Receipts ");
                                        }

                                        //foreach (var s in subImprest.SubImprestSettlements!)
                                        //{
                                        //    var voteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(s.VoteDetailId, token.sabhaId, session.StartAt.Year);

                                        //    if (voteBalance != null)
                                        //    {
                                        //        voteBalance.Debit += (decimal)s.Amount;
                                        //        voteBalance.TransactionType = VoteBalanceTransactionTypes.Income;
                                        //        voteBalance.ExchangedAmount = (decimal)s.Amount;
                                        //        voteBalance.OfficeId = token.officeId;
                                        //        voteBalance.SessionIdByOffice = session.Id;
                                        //        voteBalance.UpdatedAt = session.StartAt;
                                        //        voteBalance.UpdatedBy = token.userId;
                                        //        voteBalance.SystemActionAt = DateTime.Now;
                                        //        voteBalance.Code = "Imprest Settlement";
                                        //        voteBalance.SubCode = voucherById.VoucherSequenceNumber;


                                        //        voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;
                                        //        voteBalance.RunningBalance = (decimal)voteBalance.AllocationAmount! - voteBalance.CreditDebitRunningBalance;


                                        //        var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalance);

                                        //        vtbLog.AppCategory = AppCategory.Expenditure;
                                        //        vtbLog.ModulePrimaryKey = voucherById.Id;

                                        //        await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);

                                        //    }
                                        //    else
                                        //    {
                                        //        throw new FinalAccountException("Unable to find Expenture Vote Balance ");
                                        //    }
                                        //}
                                    }

                                }
                                else
                                {
                                    throw new FinalAccountException("Unable to find SubImprest");
                                }


                                if (await CreateCashBookCrossEntry(voucherById, CashBookTransactionType.DEBIT, token, session))
                                {
                                    await _unitOfWork.CommitAsync();
                                }
                                else
                                {
                                    throw new FinalAccountException("Cashbook Cross Debit Not Created.");
                                }

                                if (await CreateCashBookCrossEntry(voucherById, CashBookTransactionType.CREDIT, token, session))
                                {
                                    await _unitOfWork.CommitAsync();
                                }
                                else
                                {
                                    throw new FinalAccountException("Cashbook Cross Credit Not Created.");
                                }


                            }
                            else
                            {
                                throw new FinalAccountException("Unable to find Settlement Voucher");
                            }

                        }


                        if (voucherById.VoucherCategory == VoucherCategory.AdvancedB)
                        {
                            throw new FinalAccountException("Advanced B Voucher Can't Be Post Settle");
                        }

                        voucherById.ActionState = FinalAccountActionStates.HasCheque;



                    }
                    else
                    {
                        throw new FinalAccountException($"Unable To Found  Voucher To Post Settle");
                    }






                    await _unitOfWork.CommitAsync();
                    transaction.Commit();
                    return (true,"Successfully Post  Settled");
                }
                else
                {
                    throw new FinalAccountException("Unable to find Active Session");
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return (false, ex is FinalAccountException or GeneralException ? ex.Message : null);
            }
        }
    }

    public async Task<(bool, string)> WithdrawVoucher(int voucherId, HTokenClaim token) {

        using (var transaction = _unitOfWork.BeginTransaction())
        {
            try
            {
                var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);

                if (session != null)
                {
                   
                        var voucherById = await _unitOfWork.Voucher.getVoucherById(voucherId);

                        if (voucherById != null && voucherById.ActionState == FinalAccountActionStates.Init)
                        {
                             voucherById.ActionState = FinalAccountActionStates.Deleted;

                            
                                if (voucherById.VoucherCategory == VoucherCategory.VoteLedger)
                                {

                                    foreach (var voucherLine in voucherById.VoucherLine!)
                                    {
                                        var voteBalance =
                                            await _unitOfWork.VoteBalances.GetByIdAsync(voucherLine.VoteBalanceId);

                                        if (voteBalance != null)
                                        {
                                            voteBalance.TotalCommitted -= voucherLine.TotalAmount;

                                             voteBalance.TransactionType = VoteBalanceTransactionTypes.WithdrawVoucher;
                                            voteBalance.OfficeId = token.officeId;

                                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalance);
                                            voteBalance.OfficeId = token.officeId;
                                            voteBalance.UpdatedBy = token.userId;
                                            voteBalance.UpdatedAt = session.StartAt;
                                            voteBalance.SystemActionAt = DateTime.Now;
                                            voteBalance.SessionIdByOffice = session.Id;

                                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                                        }
                                        else
                                        {
                                            throw new Exception("Unable to find Vote Balance");
                                        }
                                    }


                                }

                                if (voucherById.VoucherCategory == VoucherCategory.Deposit)
                                {
                                    foreach (var deposit in voucherById.DepositsForVoucher!)
                                    {
                                        var depositById = await _unitOfWork.Deposits.GetByIdAsync(deposit.Id);
                                        if (depositById != null)
                                        {
                                            depositById.HoldAmount -= deposit.Amount;
                                        }
                                        else
                                        {
                                            throw new Exception("Unable to find Deposit");
                                        }
                                    }

                                    foreach (var voucherLine in voucherById.VoucherLine!)
                                    {
                                        var voteBalance =
                                            await _unitOfWork.VoteBalances.GetByIdAsync(voucherLine.VoteBalanceId);

                                        if (voteBalance != null)
                                        {
                                            voteBalance.TransactionType =  VoteBalanceTransactionTypes.WithdrawVoucher;


                                    
                                            voteBalance.OfficeId = token.officeId;
                                            voteBalance.UpdatedBy = token.userId;
                                            voteBalance.UpdatedAt = session.StartAt;
                                            voteBalance.SystemActionAt = DateTime.Now;
                                            voteBalance.SessionIdByOffice = session.Id;


                                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalance);


                                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                                        }
                                        else
                                        {
                                            throw new Exception("Unable to find Vote Balance");
                                        }
                                    }
                                }
                            


                            voucherById.UpdatedAt = session.StartAt;
                            voucherById.UpdatedBy = token.userId;

                            await _unitOfWork.CommitAsync();

                            if (await CreateVoucherLog(voucherById, token.userId, "withdraw"))
                            {
                                transaction.Commit();
                                return (true,"Withdraval Sucessfully");
                            }
                            else
                            {
                                throw new Exception("Unable to Create Log");
                            }

                        }
                        else
                        {
                            throw new FinalAccountException("Voucher Not Found");
                        }
                   
                }
                else
                {
                    throw new Exception("Unable to find Active Session");
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                transaction.Rollback();
                if (ex.GetType() == typeof(FinalAccountException))
                {
                    return (false, ex.Message);
                }
                else
                {
                    return (false, null);
                }
            }
        }

    }
    public async Task<(bool, string)> CreateDepositVoucherCheques(List<VoucherCheque> VoucherCheques,
        List<Voucher> Vouchers, HTokenClaim token)
    {
        using (var transaction = _unitOfWork.BeginTransaction())
        {
            try
            {
                var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
               

                if (session != null)
                {
                    foreach (var vc in VoucherCheques)
                    {

                        if (await _unitOfWork.VoucherCheque.HasChequeeNumberExist(vc.ChequeNo,token))
                        {
                            throw new FinalAccountException($"Cheque Number {vc.ChequeNo} Already Exists");
                        }


                        var accBalance = await _unitOfWork.AccountDetails.GetByIdAsync(vc.BankId);
                        accBalance.ExpenseHold += vc.Amount;

                        if (accBalance.RunningBalance < accBalance.ExpenseHold)
                        {
                            throw new FinalAccountException("Insufficient Account Balance");
                        }
                        else
                        {
                            await _unitOfWork.CommitAsync();
                        }

                        vc.CreatedBy = token.userId;
                        vc.CreatedAt = session.StartAt;

                        vc.SabhaId = token.sabhaId;
                        vc.SystemCreateAt = DateTime.Now;
                        vc.ChequeCategory = VoucherCategory.Deposit;
                    }

                    foreach (var v in Vouchers)
                    {
                        var voucherById = await _unitOfWork.Voucher.getVoucherById(v.Id!.Value);
                       

                        if (voucherById != null)
                        {
                            var docSeqNum = await _unitOfWork.FinalAccountSequenceNumber.GetNextSequenceNumberForYearSabhaModuleType(session.CreatedAt.Year, token.sabhaId, FinalAccountModuleType.Voucher);
                            if (docSeqNum != null)
                            {
                                voucherById.VoucherSequenceNumber = $"{voucherById.SabhaId}/{docSeqNum.Year}/{docSeqNum.Prefix}/{++docSeqNum.LastIndex}";
                            }
                            else
                            {
                                throw new FinalAccountException("Unable to find Sequence Number");
                            }


                            if (0 < voucherById.StampTotal)
                            {
                                var stampVote =  await _unitOfWork.SpecialLedgerAccounts.GetStampLedgerAccount(token.sabhaId);

                                if(stampVote== null)
                                {
                                    throw new FinalAccountException("Unable to find Stamp Vote");
                                }
                                var stampVoteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(stampVote.VoteId!.Value, token.sabhaId, session.StartAt.Year);
                                if (stampVoteBalance != null)
                                {
                                    stampVoteBalance.Debit += (decimal)voucherById.StampTotal;
                                    stampVoteBalance.ExchangedAmount = (decimal)voucherById.StampTotal;
                                    stampVoteBalance.CreditDebitRunningBalance = stampVoteBalance.Debit - stampVoteBalance.Credit;
                                    stampVoteBalance.UpdatedAt = session.StartAt;
                                    stampVoteBalance.UpdatedBy = token.userId;
                                    stampVoteBalance.SystemActionAt = DateTime.Now;
                                    stampVoteBalance.OfficeId = token.officeId;
                                    stampVoteBalance.SessionIdByOffice = session.Id;

                                    var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(stampVoteBalance);
                                    
                                    await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                                }
                                else
                                {
                                    throw new FinalAccountException("Unable to find Stamp Vote Balance");
                                }

                            }


                            foreach (var deposit in voucherById.DepositsForVoucher!)
                                {
                                    var depositById = await _unitOfWork.Deposits.GetByIdAsync(deposit.DepositId);
                                    if (depositById != null)
                                    {
                                        depositById.HoldAmount -= deposit.Amount;
                                        depositById.ReleasedAmount += deposit.Amount;
                                        depositById.IsEditable = false;
                                        depositById.UpdatedBy = token.userId;
                                        depositById.UpdatedAt = session.StartAt;
                                        depositById.SystemUpdateAt = DateTime.Now;
                                    }
                                    else
                                    {
                                        throw new FinalAccountException("Unable to find Deposit");
                                    }
                                }

                            await _unitOfWork.CommitAsync();


                            foreach (var voucherLine in voucherById.VoucherLine!)
                            {
                                var voteBalance =
                                    await _unitOfWork.VoteBalances.GetActiveVoteBalance(voucherLine.VoteId,
                                        token.sabhaId, session.StartAt.Year);

                                if (voteBalance != null)
                                {
                                    voteBalance.TransactionType = VoteBalanceTransactionTypes.CreateCheque;
                                    voteBalance.Code = "VOUCHER";

                                    if(voteBalance.ClassificationId == 1)
                                    {
                                        voteBalance.Credit += voucherLine.TotalAmount;
                                        voteBalance.ExchangedAmount = voucherLine.TotalAmount;

                                        voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;
                                        //voteBalance.RunningBalance = (decimal)voteBalance.AllocationAmount! - voteBalance.CreditDebitRunningBalance;

                                        voteBalance.SubCode = voucherById.VoucherSequenceNumber;

                                        voteBalance.UpdatedAt = session.StartAt;
                                        voteBalance.UpdatedBy = token.userId;
                                        voteBalance.SystemActionAt = DateTime.Now;



                                        voteBalance.OfficeId = token.officeId;
                                        var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalance);
                                        vtbLog.AppCategory = AppCategory.Expenditure;
                                        vtbLog.ModulePrimaryKey = voucherById.Id;
                                        await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);

                                    }

                                    if (voteBalance.ClassificationId == 2)
                                    {

                                            if (voteBalance.TotalCommitted >= voucherLine.TotalAmount)
                                            {

                                                voteBalance.TotalCommitted -= voucherLine.TotalAmount;
                                            }
                                            else
                                            {
                                                throw new FinalAccountException("Insufficient Vote Balance");

                                            }
                                    

                                        voteBalance.Debit += voucherLine.TotalAmount;
                                        voteBalance.ExchangedAmount = voucherLine.TotalAmount;

                                        voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;
                                        voteBalance.RunningBalance = (decimal)voteBalance.AllocationAmount! - voteBalance.CreditDebitRunningBalance;

                                        voteBalance.SubCode = voucherById.VoucherSequenceNumber;

                                        voteBalance.UpdatedAt = session.StartAt;
                                        voteBalance.UpdatedBy = token.userId;
                                        voteBalance.SystemActionAt = DateTime.Now;
                                        voteBalance.SessionIdByOffice = session.Id;


                                        voucherLine.RptExpenditure = voteBalance.Debit- voteBalance.Credit;
                                        voucherLine.RptBalance = (decimal)voteBalance.AllocationAmount! - voucherLine.RptExpenditure;
                                        voucherLine.RptBudgetAllocation = (decimal)voteBalance.AllocationAmount!;



                                        //var vtb = new VoteLedgerBook
                                        //{
                                        //    SabhaId = token.sabhaId,
                                        //    OfiiceId = token.officeId,
                                        //    SessionId = session.Id,
                                        //    Description = voucherById.VoucherSequenceNumber,
                                        //    Date = session.StartAt,
                                        //    VoteBalanceId = (int)voteBalance.Id!,
                                        //    VoteDetailId = (int)voteBalance.VoteDetailId!,
                                        //    TransactionType = CashBookTransactionType.CREDIT,
                                        //    VoteBalanceTransactionTypes = VoteBalanceTransactionTypes.CreateCheque,
                                        //    Credit = 0,
                                        //    Debit = voteBalance.ExchangedAmount,
                                        //    RunningTotal = voteBalance.CreditDebitRunningBalance,
                                        //    RowStatus = 1,
                                        //    CreatedAt = session.StartAt,
                                        //    UpdatedAt = session.StartAt,
                                        //    CreatedBy = token.userId,
                                        //    UpdatedBy = token.userId,
                                        //    SystemActionDate = DateTime.Now
                                        //};

                                        //await _unitOfWork.VoteLedgerBook.AddAsync(vtb);
                                        voteBalance.OfficeId = token.officeId;
                                        var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalance);
                                        vtbLog.AppCategory = AppCategory.Expenditure;
                                        vtbLog.ModulePrimaryKey = voucherById.Id;
                                        await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                                    }

                                    if(voteBalance.ClassificationId == 3)
                                    {
                                        if (voucherById.VoucherCategory is not (VoucherCategory.AccountTransfer or VoucherCategory.Refund))
                                        {
                                            voteBalance.Debit += voucherLine.TotalAmount;
                                            voteBalance.ExchangedAmount = voucherLine.TotalAmount;

                                            voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;
                                            //voteBalance.RunningBalance = (decimal)voteBalance.AllocationAmount! - voteBalance.CreditDebitRunningBalance;
                                        }

                                        voteBalance.SubCode = voucherById.VoucherSequenceNumber;

                                        voteBalance.UpdatedAt = session.StartAt;
                                        voteBalance.UpdatedBy = token.userId;
                                        voteBalance.SystemActionAt = DateTime.Now;
                                        voteBalance.OfficeId = token.officeId;
                                        var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalance);
                                        vtbLog.AppCategory = AppCategory.Expenditure;
                                        vtbLog.ModulePrimaryKey = voucherById.Id;
                                        await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);

                                    }

                                    if (voteBalance.ClassificationId == 4)
                                    {
                                        voteBalance.Debit += voucherLine.TotalAmount;
                                        voteBalance.ExchangedAmount = voucherLine.TotalAmount;

                                        voteBalance.CreditDebitRunningBalance = voteBalance.Credit - voteBalance.Debit;
                                        //voteBalance.RunningBalance = (decimal)voteBalance.AllocationAmount! - voteBalance.CreditDebitRunningBalance;

                                        voteBalance.SubCode = voucherById.VoucherSequenceNumber;

                                        voteBalance.UpdatedAt = session.StartAt;
                                        voteBalance.UpdatedBy = token.userId;
                                        voteBalance.SystemActionAt = DateTime.Now;
                                        voteBalance.OfficeId = token.officeId;
                                        var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalance);
                                        vtbLog.AppCategory = AppCategory.Expenditure;
                                        vtbLog.ModulePrimaryKey = voucherById.Id;
                                        await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);

                                    }

                                }
                                else
                                {
                                    throw new Exception("Unable to find Vote Balance");
                                }
                            }


                            foreach (var voucherItem in voucherById.SubVoucherItems!)
                            {
                                foreach (var voucherCrossOrder in voucherItem.VoucherCrossOrders!)
                                {

                                    var mixinOrder = await _unitOfWork.MixinOrders.GetCrossOrderById(voucherCrossOrder.CrossOrderId!.Value);
                                    if (mixinOrder != null)
                                    {
                                        string prefix = "MIX";

                                        var docSeqNums = await _unitOfWork.DocumentSequenceNumbers
                                            .GetNextSequenceNumberForYearOfficePrefixAsync(mixinOrder.CreatedAt.Year,
                                                mixinOrder.OfficeId.Value, "MIX");

                                        var office = await _unitOfWork.Offices.GetByIdAsync(mixinOrder.OfficeId);


                                        mixinOrder.Code =
                                            $"{office.Code}/{DateTime.Now.Year}/{"MIX"}/{++docSeqNums.LastIndex}";
                                        mixinOrder.PaymentMethodId = 3;
                                        mixinOrder.State = OrderStatus.Posted;
                                        mixinOrder.CashierId = token.userId;


                                        foreach (var item in mixinOrder!.MixinOrderLine)
                                        {
                                           

                                            var voteAssignmentDetail = await _unitOfWork.CustomVoteDetails.GetByIdAsync(item.MixinVoteAssignmentDetailId);

                                            if (voteAssignmentDetail == null)
                                            {
                                                throw new FinalAccountException("Unable to find Vote Assignment Detail");
                                            }

                                            var voteAssignment = await _unitOfWork.VoteAssignments.GetByIdAsync(voteAssignmentDetail.VoteAssignmentId);


                                            if (voteAssignment != null)
                                            {
                                                item.VoteDetailId = voteAssignment.VoteId;
                                            }
                                            else{
                                                throw new FinalAccountException("Unable to find Vote Assignment");
                                            }

                                        }

                                        if (await UpdateVoteBalance(mixinOrder, session, CashBookTransactionType.DEBIT, CashBookIncomeCategory.Mix, token))
                                        {
                                            //await _unitOfWork.CommitAsync();
                                        }
                                        else
                                        {
                                            throw new FinalAccountException("Vote Balance Entry Not updated.");
                                        }


                                        if (await CreateCashBookEntry(mixinOrder, session, CashBookTransactionType.DEBIT, CashBookIncomeCategory.Mix, token))
                                        {
                                            //await _unitOfWork.CommitAsync();
                                        }
                                        else
                                        {
                                            throw new Exception("Cashbook entry not created.");
                                        }

                                    }
                                    else
                                    {
                                        throw new FinalAccountException("Unable to find CrossOrder");
                                    }
                                }
                            }

                            await _unitOfWork.CommitAsync();


                            if (voucherById.VoucherCategory == VoucherCategory.AccountTransfer)
                            {

                                var accountTransfer = await _unitOfWork.AccountTransfer.GetAccountTransferByVoucherId(voucherById.Id!.Value);

                                if(accountTransfer == null)
                                {
                                    throw new FinalAccountException("Unable to find Account Transfer");
                                }

                                var voteAssignment = await _unitOfWork.VoteAssignments.GetByVoteId(accountTransfer.ToVoteDetailId,token);

                                if (voteAssignment == null)
                                {
                                    throw new FinalAccountException("Unable to find Vote Assignment");
                                }

                                var voteAssignmentDetail = await _unitOfWork.CustomVoteDetails.GetByAssignmentId(voteAssignment.Id);

                                if(voteAssignmentDetail == null)
                                {
                                    throw new FinalAccountException("Unable to find Vote Assignment Detail");
                                }


                                var payee = new PayeeResources();
                                
                                if(VoucherCheques.FirstOrDefault()!.PayeeCategory == VoucherPayeeCategory.InternalOfficer)
                                {
                                    if(VoucherCheques.FirstOrDefault()!.PayeeId == -2)
                                    {
                                        payee = _mapper.Map<Employee, PayeeResources>(await _employeeService.GetEmployeeById(2));
                                    }

                                    if(VoucherCheques.FirstOrDefault()!.PayeeId == -3)
                                    {
                                        payee = _mapper.Map<Employee, PayeeResources>(await _employeeService.GetEmployeeById(3));
                                    }

                                    if(VoucherCheques.FirstOrDefault()!.PayeeId == -4)
                                    {
                                        payee = _mapper.Map<Employee, PayeeResources>(await _employeeService.GetEmployeeById(4));
                                    }


                                }
                                else
                                {
                                    throw new FinalAccountException("Invalid Payee Category");
                                }
                               

                                if(payee == null)
                                {
                                    throw new FinalAccountException("Unable to find Payee");
                                }


                                var newCrossOrder = new MixinOrder
                                {
                                    OfficeId = token.officeId,
                                    CreatedBy = token.userId,
                                    CashierId = token.userId,
                                    AccountDetailId= accountTransfer.ToAccountId,
                                    TotalAmount = accountTransfer.Amount,
                                    EmployeeId= payee.PayeeId,

                                    CustomerNicNumber = "0000000000000",
                                    CustomerMobileNumber = "0000000000",
                                    CustomerName = $"{payee.FullName} - {token.sabhaName}",
                                    PaymentMethodId = 1,
                                    SessionId = session.Id,
                                    MixinOrderLine = new List<MixinOrderLine>()

                                };

                                var orderLine = new MixinOrderLine
                                {
                                    CustomVoteName = voteAssignmentDetail.CustomVoteName,
                                    Amount = accountTransfer.Amount,
                                    Description = voucherById.CommentOrDescription,
                                    MixinVoteAssignmentDetailId = voteAssignmentDetail.Id,
                                    VoteDetailId = accountTransfer.ToVoteDetailId,

                                };

                                newCrossOrder.MixinOrderLine.Add(orderLine);


                                var sessionDate = await _unitOfWork.Sessions.IsRescueSessionThenDate(session.Id);

                                if (sessionDate.HasValue)
                                {
                                    newCrossOrder.CreatedAt = (DateTime)sessionDate;

                                    foreach (var item in newCrossOrder!.MixinOrderLine)
                                    {
                                        item.CreatedAt = (DateTime)sessionDate;
                                    }
                                }
                                else
                                {
                                    newCrossOrder.CreatedAt = DateTime.Now;

                                    foreach (var item in newCrossOrder!.MixinOrderLine)
                                    {
                                        item.CreatedAt = DateTime.Now;
                                    }
                                }


                                newCrossOrder.State = OrderStatus.Posted;
                                var office = await _unitOfWork.Offices.GetByIdAsync(newCrossOrder.OfficeId.Value);

                                var crossOrderdocSeqNums = await _unitOfWork.DocumentSequenceNumbers.GetNextSequenceNumberForYearOfficePrefixAsync(newCrossOrder.CreatedAt.Year, newCrossOrder.OfficeId.Value, "MIX");
                                newCrossOrder.Code = $"{office.Code}/{DateTime.Now.Year}/{"MIX"}/{++crossOrderdocSeqNums.LastIndex}";

                                await _unitOfWork.MixinOrders.AddAsync(newCrossOrder);
                                await _unitOfWork.CommitAsync();


                                if (await CreateCashBookEntry(newCrossOrder,session ,CashBookTransactionType.DEBIT, CashBookIncomeCategory.Mix, token))
                                {
                                    await _unitOfWork.CommitAsync();
                                }
                                else
                                {
                                    throw new Exception("Cashbook entry not created.");
                                }

                            }


                            if (voucherById.VoucherCategory == VoucherCategory.Refund)
                            {

                                //var accountTransfer = await _unitOfWork.AccountTransfer.GetAccountTransferByVoucherId(voucherById.Id!.Value);
                                var accountTransfer = await _unitOfWork.AccountTransfer.GetByIdAsync(voucherById.AccountTransferId);

                                var refunding = await _unitOfWork.AccountTransferRefunding.GetByIdAsync(voucherById.RefundId);

                                if(refunding!= null)
                                {
                                    accountTransfer.RefundedAmount += refunding.Amount;

                                    if(accountTransfer.Amount == accountTransfer.RefundedAmount)
                                    {
                                        accountTransfer.IsFullyRefunded = true;
                                    }
                                    refunding.UpdatedAt = session.StartAt;
                                    refunding.UpdatedBy = token.userId;
                                    refunding.SystemUpdateAt = DateTime.Now;
                                }
                                else
                                {
                                    throw new FinalAccountException("Unable to find Refund");
                                }


                                if (accountTransfer==null)
                                {
                                    throw new FinalAccountException("Unable to find Account Transfer");
                                }

                                var voteAssignment = await _unitOfWork.VoteAssignments.GetByVoteId(accountTransfer.FromVoteDetailId, token);


                                if(voteAssignment == null)
                                {
                                    throw new FinalAccountException("Unable to find Vote Assignment");
                                }


                                var voteAssignmentDetail = await _unitOfWork.CustomVoteDetails.GetByAssignmentId(voteAssignment.Id);

                                if (voteAssignmentDetail == null)
                                {
                                    throw new FinalAccountException("Unable to find Vote Assignment Detail");
                                }


                                var payee = new PayeeResources();

                                if (VoucherCheques.FirstOrDefault()!.PayeeCategory == VoucherPayeeCategory.InternalOfficer)
                                {
                                    if (VoucherCheques.FirstOrDefault()!.PayeeId == -2)
                                    {
                                        payee = _mapper.Map<Employee, PayeeResources>(await _employeeService.GetEmployeeById(2));
                                    }

                                    if (VoucherCheques.FirstOrDefault()!.PayeeId == -3)
                                    {
                                        payee = _mapper.Map<Employee, PayeeResources>(await _employeeService.GetEmployeeById(3));
                                    }

                                    if (VoucherCheques.FirstOrDefault()!.PayeeId == -4)
                                    {
                                        payee = _mapper.Map<Employee, PayeeResources>(await _employeeService.GetEmployeeById(4));
                                    }


                                }
                                else
                                {
                                    throw new FinalAccountException("Invalid Payee Category");
                                }


                                if (payee == null)
                                {
                                    throw new FinalAccountException("Unable to find Payee");
                                }


                                var newCrossOrder = new MixinOrder
                                {
                                    OfficeId = token.officeId,
                                    CreatedBy = token.userId,
                                    CashierId = token.userId,
                                    AccountDetailId = accountTransfer.FromAccountId,
                                    TotalAmount = refunding.Amount,
                                    EmployeeId = payee.PayeeId,

                                    CustomerNicNumber = "0000000000000",
                                    CustomerMobileNumber = "0000000000",
                                    CustomerName = $"{payee.FullName} - {token.sabhaName}",
                                    PaymentMethodId = 1,
                                    SessionId = session.Id,
                                    MixinOrderLine = new List<MixinOrderLine>()

                                };

                                var orderLine = new MixinOrderLine
                                {
                                    CustomVoteName = voteAssignmentDetail.CustomVoteName,
                                    Amount = refunding.Amount,
                                    Description = voucherById.CommentOrDescription,
                                    MixinVoteAssignmentDetailId = voteAssignmentDetail.Id,
                                    VoteDetailId = accountTransfer.ToVoteDetailId,

                                };

                                newCrossOrder.MixinOrderLine.Add(orderLine);


                                var sessionDate = await _unitOfWork.Sessions.IsRescueSessionThenDate(session.Id);

                                if (sessionDate.HasValue)
                                {
                                    newCrossOrder.CreatedAt = (DateTime)sessionDate;

                                    foreach (var item in newCrossOrder!.MixinOrderLine)
                                    {
                                        item.CreatedAt = (DateTime)sessionDate;
                                    }
                                }
                                else
                                {
                                    newCrossOrder.CreatedAt = DateTime.Now;

                                    foreach (var item in newCrossOrder!.MixinOrderLine)
                                    {
                                        item.CreatedAt = DateTime.Now;
                                    }
                                }


                                newCrossOrder.State = OrderStatus.Posted;
                                var office = await _unitOfWork.Offices.GetByIdAsync(newCrossOrder.OfficeId.Value);

                                var crossOrderdocSeqNums = await _unitOfWork.DocumentSequenceNumbers.GetNextSequenceNumberForYearOfficePrefixAsync(newCrossOrder.CreatedAt.Year, newCrossOrder.OfficeId.Value, "MIX");
                                newCrossOrder.Code = $"{office.Code}/{DateTime.Now.Year}/{"MIX"}/{++crossOrderdocSeqNums.LastIndex}";

                                await _unitOfWork.MixinOrders.AddAsync(newCrossOrder);
                                await _unitOfWork.CommitAsync();


                                if (await CreateCashBookEntry(newCrossOrder, session, CashBookTransactionType.DEBIT, CashBookIncomeCategory.Mix, token))
                                {
                                    await _unitOfWork.CommitAsync();
                                }
                                else
                                {
                                    throw new Exception("Cashbook entry not created.");
                                }

                            }


                            if (voucherById.VoucherCategory == VoucherCategory.Settlement)
                            {
                                var subImprest = await _unitOfWork.SubImprests.GetByIdAsync(voucherById.SubImprestId);

                                if (subImprest != null) { 

                                    if (subImprest.SettleByCash > 0)
                                    {
                                        if ((subImprest.SettleByCash + subImprest.SettleByBills > subImprest.Amount))
                                        {


                                            decimal? revenueAmount = subImprest.SettleByCash - (subImprest.Amount - subImprest.SettleByBills);

                                            if (subImprest.SettleByBills > subImprest.Amount)
                                            {
                                                revenueAmount = subImprest.SettleByCash;
                                            }

                                            var otherReciptLegerAccount = await _unitOfWork.SpecialLedgerAccounts.GetOtherReceiptsLedgerAccount(token.sabhaId);
                                            if (otherReciptLegerAccount == null)
                                            {

                                                throw new FinalAccountException("Unable to find Other Receipts Ledger Account");
                                            }

                                            var otherReciptsVoteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(otherReciptLegerAccount.VoteId!.Value, token.sabhaId, session.StartAt.Year);


                                            if (otherReciptsVoteBalance == null)
                                            {
                                                otherReciptsVoteBalance = await _voteBalanceService.CreateNewVoteBalance(otherReciptLegerAccount.VoteId!.Value, session,token);

                                                if (otherReciptsVoteBalance == null)
                                                {
                                                    throw new FinalAccountException("Unable to Create Other Receipts Ledger Account Entry");
                                                }


                                            }

                                            if (otherReciptsVoteBalance != null)
                                            {
                                                otherReciptsVoteBalance.Credit += (decimal)revenueAmount;
                                                otherReciptsVoteBalance.TransactionType = VoteBalanceTransactionTypes.Income;
                                                otherReciptsVoteBalance.ExchangedAmount = (decimal)revenueAmount;
                                                otherReciptsVoteBalance.OfficeId = token.officeId;
                                                otherReciptsVoteBalance.SessionIdByOffice = session.Id;
                                                otherReciptsVoteBalance.UpdatedAt = session.StartAt;
                                                otherReciptsVoteBalance.UpdatedBy = token.userId;
                                                otherReciptsVoteBalance.SystemActionAt = DateTime.Now;
                                                otherReciptsVoteBalance.Code = "Other Receipts From Imprest JNL";

                                                otherReciptsVoteBalance.CreditDebitRunningBalance = otherReciptsVoteBalance.Credit - otherReciptsVoteBalance.Debit;
                                                var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(otherReciptsVoteBalance);

                                                vtbLog.AppCategory = AppCategory.SubImprest;
                                                vtbLog.ModulePrimaryKey = subImprest.Id!.Value;

                                                await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);

                                            }
                                            else
                                            {
                                                throw new FinalAccountException("Unable to find Other Receipts Vote Balance ");
                                            }

                                            var imprestVoteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(subImprest.SubImprestVoteId!, token.sabhaId, session.StartAt.Year);

                                            if (imprestVoteBalance != null)
                                            {
                                                imprestVoteBalance.Debit += (decimal)revenueAmount;
                                                imprestVoteBalance.TransactionType = VoteBalanceTransactionTypes.Income;
                                                imprestVoteBalance.ExchangedAmount = (decimal)revenueAmount;
                                                imprestVoteBalance.OfficeId = token.officeId;
                                                imprestVoteBalance.SessionIdByOffice = session.Id;
                                                imprestVoteBalance.UpdatedAt = session.StartAt;
                                                imprestVoteBalance.UpdatedBy = token.userId;
                                                imprestVoteBalance.SystemActionAt = DateTime.Now;
                                                imprestVoteBalance.Code = "Imprest Settlement For Other Receipts JNL";

                                                imprestVoteBalance.CreditDebitRunningBalance = imprestVoteBalance.Debit - imprestVoteBalance.Credit;
                                                var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(imprestVoteBalance);

                                                vtbLog.AppCategory = AppCategory.SubImprest;
                                                vtbLog.ModulePrimaryKey = subImprest.Id!.Value;

                                                await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);

                                            }
                                            else
                                            {
                                                throw new FinalAccountException("Unable to find  Imprest Vote Balance");
                                            }

                                          

                                        }
                                        else
                                        {
                                            throw new FinalAccountException("Invalid   Settlement ");
                                        }

                                    }
                                    
                                     {
                                            var imprestVoteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(subImprest.SubImprestVoteId!, token.sabhaId, session.StartAt.Year);

                                            if (imprestVoteBalance != null)
                                            {

                                                imprestVoteBalance.Credit += subImprest.Amount!;
                                                imprestVoteBalance.TransactionType = VoteBalanceTransactionTypes.CreateCheque;
                                                imprestVoteBalance.ExchangedAmount = subImprest.Amount!;
                                                imprestVoteBalance.CreditDebitRunningBalance = imprestVoteBalance.Debit - imprestVoteBalance.Credit;
                                                imprestVoteBalance.OfficeId = token.officeId;
                                                imprestVoteBalance.SessionIdByOffice = session.Id;
                                                imprestVoteBalance.UpdatedAt = session.StartAt;
                                                imprestVoteBalance.UpdatedBy = token.userId;
                                                imprestVoteBalance.SystemActionAt = DateTime.Now;
                                                imprestVoteBalance.Code = "Imprest Settlement";
                                                imprestVoteBalance.SubCode = voucherById.VoucherSequenceNumber;

                                                var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(imprestVoteBalance);

                                                vtbLog.AppCategory = AppCategory.SubImprest;
                                                vtbLog.ModulePrimaryKey = subImprest.Id!.Value;

                                                await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);

                                            }
                                            else
                                            {
                                                throw new FinalAccountException("Unable to find Vote Balance For Other Receipts ");
                                            }
                                     }
                                    

                                }
                                else
                                {
                                    throw new FinalAccountException("Unable to find SubImprest");
                                }


                                if (await CreateCashBookCrossEntry(voucherById,  CashBookTransactionType.DEBIT,  token,session))
                                {
                                    await _unitOfWork.CommitAsync();
                                }
                                else
                                {
                                    throw new FinalAccountException("Cashbook Cross Debit Not Created.");
                                }

                                if (await CreateCashBookCrossEntry(voucherById, CashBookTransactionType.CREDIT, token, session))
                                {
                                    await _unitOfWork.CommitAsync();
                                }
                                else
                                {
                                    throw new FinalAccountException("Cashbook Cross Credit Not Created.");
                                }


                            }


                            if (voucherById.VoucherCategory == VoucherCategory.AdvancedB)
                            { 
                                var advanceB = await _unitOfWork.AdvanceBs.GetByIdAsync(voucherById.AdvancedBId!.Value);

                                if(advanceB != null)
                                {
                                   advanceB.VoucherNo = voucherById.VoucherSequenceNumber;
                                }
                                else
                                {
                                    throw new FinalAccountException("Unable to find Advance B");
                                }
                            }

                                voucherById.ActionState = FinalAccountActionStates.HasCheque;

                          



                            if (!await CreateVoucherLog(voucherById, token.userId, "create cheque"))
                            {
                                throw new Exception("Unable to Create Voucher Logs");
                            }
                        }
                        else
                        {
                            throw new FinalAccountException($"Unable To Found {v.VoucherSequenceNumber} Voucher");
                        }
                    }

                    await _unitOfWork.VoucherCheque.AddRangeAsync(VoucherCheques);
                    await _unitOfWork.CommitAsync();


                    foreach (var vc in VoucherCheques)
                    {
                        List<int> itemIds = vc.VoucherItemsForCheque.Select(i => (int)i.SubVoucherItemId).ToList();

                       


                       var vouchers = await _unitOfWork.Voucher.GetVoucherBySubVouchers(itemIds);

                        foreach (var v in vouchers)
                        {
                            vc.VoucherNoAsString = string.Join(",", v.VoucherSequenceNumber);
                        }

                        if (!await CreateCashBookEntry(vc, vc.VoucherNoAsString!, token, session))
                        {
                            throw new FinalAccountException("Unable to Create Cash Book Entry");
                        }

                        await _unitOfWork.CommitAsync();
                    }

                    transaction.Commit();
                    return (true, "Successful Saved Cheques");
                }
                else
                {
                    throw new Exception("Unable to find Active Session");
                }
            }
            catch (Exception e)
            {
                transaction.Rollback();

                if (e.GetType() == typeof(FinalAccountException))
                {
                    return (false, e.Message);
                }
                else
                {
                    return (false, null);
                }
            }
        }
    }

    private async Task<bool> CreateCommitmentLog(Commitment commitment, int actionBy, string actionComment)
    {
        try
        {
            CommitmentActionsLog commitmentActionsLog = new CommitmentActionsLog
            {
                CommitmentId = commitment.Id,
                ActionBy = actionBy,
                ActionState = commitment.ActionState,
                Comment = actionComment,
                ActionDateTime = commitment.UpdatedAt,
                SystemActionAt = DateTime.Now,

            };

            var commitmentLog = _mapper.Map<Commitment, CommitmentLog>(commitment);

            foreach (var cmmitLine in commitment.CommitmentLine!)
            {
                var lineLog = _mapper.Map<CommitmentLine, CommitmentLineLog>(cmmitLine);

                foreach (var voteLine in cmmitLine.CommitmentLineVotes!)
                {
                    var voteLineLog = _mapper.Map<CommitmentLineVotes, CommitmentLineVotesLog>(voteLine);
                    lineLog.CommitmentLineVotesLog!.Add(voteLineLog);

                }

                commitmentLog.CommitmentLineLog!.Add(lineLog);
            }

            await _unitOfWork.CommitmentActionLog.AddAsync(commitmentActionsLog);
            await _unitOfWork.CommitmentLog.AddAsync(commitmentLog);
            await _unitOfWork.CommitAsync();
            return true;

        }
        catch (Exception e)
        {
            return false;
        }
    }

    private async Task<bool> CreateVoucherLog(Voucher voucher, int actionBy, string comment)
    {
        try
        {
            VoucherActionLog voucherActionLog = new VoucherActionLog
            {
                VoucherId = voucher.Id,
                ActionBy = actionBy,
                ActionDateTime = voucher.UpdatedAt,
                SystemActionAt = DateTime.Now,
                Comment = comment,
                ActionState = voucher.ActionState,
            };

            var voucherLog = _mapper.Map<Voucher, VoucherLog>(voucher);

            voucherLog.Id = null;
            voucherLog.VoucherId = voucher.Id;
            voucherLog.SystemActionAt = DateTime.Now;

            foreach (var vline in voucher.VoucherLine)
            {

                var voucherLineLog = _mapper.Map<VoucherLine, VoucherLineLog>(vline);

                voucherLineLog.Id = null;
                voucherLineLog.VoucherLineId = vline.Id;

                voucherLog.voucherLineLog.Add(voucherLineLog);
            }

            await _unitOfWork.VoucherActionLog.AddAsync(voucherActionLog);
            await _unitOfWork.VoucherLog.AddAsync(voucherLog);
            await _unitOfWork.CommitAsync();

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<IEnumerable<VoucherResource>> getVoucherForPsReport(int sabhaId, int year, int month)
    {
        var vouchers = await _unitOfWork.Voucher.getVoucherForPsReport(sabhaId, year, month);
        var voucherResource = _mapper.Map<IEnumerable<Voucher>, IEnumerable<VoucherResource>>(vouchers);

        return voucherResource;
    }

    private async Task<bool> CreateCashBookEntry(VoucherCheque voucherCheque, string code, HTokenClaim token,
        Session session)
    {
        try
        {
            var account = await _unitOfWork.AccountDetails.GetByIdAsync(voucherCheque.BankId);
            //var session = await _unitOfWork.Sessions.GetByIdAsync(mx.SessionId);

            account.RunningBalance -= voucherCheque.Amount;
            account.ExpenseHold -= voucherCheque.Amount;


            if (account.RunningBalance < 0)
            {
                throw new Exception("Insufficient Balance");
            }

            var voteBalance =
                await _unitOfWork.VoteBalances.GetActiveVoteBalance(account.VoteId.Value, token.sabhaId,
                    session.StartAt.Year);

            if (voteBalance != null)
            {
                voteBalance.TransactionType = VoteBalanceTransactionTypes.CreateCheque;
                voteBalance.Credit += voucherCheque.Amount;
                voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;
                voteBalance.ExchangedAmount = voucherCheque.Amount;
               
                voteBalance.OfficeId = token.officeId;
                voteBalance.UpdatedAt = session.StartAt;
                voteBalance.UpdatedBy = token.userId;
                voteBalance.SystemActionAt = DateTime.Now;
                var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalance);
                await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
            }
            else
            {
                throw new Exception("Unable to find CashBook Vote Balance");
            }

            var cashbook = new CashBook
            {
                //Id
                SabhaId = token.sabhaId,
                OfiiceId = token.officeId,
                SessionId = session.Id,
                BankAccountId = voucherCheque.BankId,
                Date = session.CreatedAt,

                TransactionType = CashBookTransactionType.CREDIT,
                ExpenseCategory = CashBookExpenseCategory.VoucherCheque,
                IncomeExpenseMethod = CashBookIncomeExpenseMethod.Cheque,
                Code = code,
                ExpenseItemId = voucherCheque.Id,
                ChequeAmount = voucherCheque.Amount,
                RunningTotal = account.RunningBalance,

                Description = "",
                CreatedAt = DateTime.Now,
                CreatedBy = token.userId,
            };


            await _unitOfWork.CashBook.AddAsync(cashbook);

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    } 
    private async Task<bool> CreateCashBookCrossEntry(Voucher voucher, CashBookTransactionType transactionType, HTokenClaim token,Session session)
    {
        try
        {
            var account = await _unitOfWork.AccountDetails.GetByIdAsync(voucher.BankId);
            var voteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(account.VoteId!.Value, token.sabhaId, session.StartAt.Year);

            if (voteBalance != null)
            {
            }
            else
            {
                throw new Exception("Unable to find CashBook Vote Balance");
            }

            var cashbook = new CashBook
            {
                //Id
                SabhaId = token.sabhaId,
                OfiiceId = token.officeId,
                SessionId = session.Id,
                BankAccountId = account.ID,
                Date = session.CreatedAt,

                TransactionType = transactionType,
                ExpenseCategory = CashBookExpenseCategory.VoucherCheque,
                IncomeExpenseMethod = CashBookIncomeExpenseMethod.Cheque,
                Code = voucher.VoucherSequenceNumber,
                ExpenseItemId = voucher.Id,
                CrossAmount = voucher.VoucherAmount,
                RunningTotal = account.RunningBalance,

                Description = "",
                CreatedAt = DateTime.Now,
                CreatedBy = token.userId,
            };


            await _unitOfWork.CashBook.AddAsync(cashbook);

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

   private async Task<bool> CreateCashBookEntry(MixinOrder mx,Session session, CashBookTransactionType transactionType, CashBookIncomeCategory incomeCategory, HTokenClaim token)
        {

            try
            {
                var account = await _unitOfWork.AccountDetails.GetByIdAsync(mx.AccountDetailId!.Value);
            //var session = await _unitOfWork.Sessions.GetByIdAsync(mx.SessionId);
            if (mx.PaymentMethodId != 3)
            {
                account.RunningBalance += mx.TotalAmount;
            }

                var cashbook = new CashBook
                {
                    //Id
                    SabhaId = token.sabhaId,
                    OfiiceId = mx.OfficeId!.Value,
                    SessionId = mx.SessionId,
                    BankAccountId = mx.AccountDetailId.Value,
                    Date = mx.CreatedAt,

                    TransactionType = transactionType,
                    IncomeCategory = incomeCategory,
                    Code = mx.Code,
                    IncomeItemId = mx.Id,


                    CreatedAt = DateTime.Now,
                    CreatedBy = token.userId,
                    RunningTotal = account.RunningBalance,


                };

            if (mx.PaymentMethodId != 3)
            {

                var voteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(account.VoteId!.Value, token.sabhaId, session.StartAt.Year);

                if (voteBalance != null)
                {
                    voteBalance.TransactionType = VoteBalanceTransactionTypes.CreateCheque;
                    voteBalance.Debit += mx.TotalAmount;
                    voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;

                    voteBalance.ExchangedAmount = mx.TotalAmount;
                    voteBalance.OfficeId = token.officeId;
                    voteBalance.UpdatedAt = session.StartAt;
                    voteBalance.UpdatedBy = token.userId;
                    voteBalance.SystemActionAt = DateTime.Now;
                    voteBalance.SessionIdByOffice = session.Id;

                    var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalance);
                    await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                }
                else
                {
                    throw new Exception("Unable to find Vote Balance");
                }
            }
            //1 = cash
            //2 = cheque
            //3 cross
            //4 = direct
            if (mx.PaymentMethodId == 1)
                {
                    cashbook.CashAmount = mx.TotalAmount;
                    cashbook.IncomeExpenseMethod = CashBookIncomeExpenseMethod.Cash;
                }
                else if (mx.PaymentMethodId == 2)
                {
                    cashbook.ChequeAmount = mx.TotalAmount;
                    cashbook.IncomeExpenseMethod = CashBookIncomeExpenseMethod.Cheque;
                    cashbook.ChequeNo = mx.ChequeNumber;

                }
                else if (mx.PaymentMethodId == 3)
                {
                    cashbook.CrossAmount = mx.TotalAmount;
                    cashbook.IncomeExpenseMethod = CashBookIncomeExpenseMethod.Cross;
                }
                else if (mx.PaymentMethodId == 4)
                {
                    cashbook.DirectAmount = mx.TotalAmount;
                    cashbook.IncomeExpenseMethod = CashBookIncomeExpenseMethod.Direct;
                }


                await _unitOfWork.CashBook.AddAsync(cashbook);

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }


        
        }

    private async Task<bool> UpdateVoteBalance(MixinOrder mx, Session session, CashBookTransactionType transactionType, CashBookIncomeCategory incomeCategory, HTokenClaim token)

    {
        try
        {



            foreach (var item in mx.MixinOrderLine)
            {


                if (item.StampAmount > 0)
                {
                    var stampLedgerAccount = await _unitOfWork.SpecialLedgerAccounts.GetStampLedgerAccount(token.sabhaId);

                    if(stampLedgerAccount == null)
                    {
                        throw new FinalAccountException("Unable to find Stamp Ledger Account");
                    }   

                    var stampVoteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(stampLedgerAccount.VoteId!.Value, token.sabhaId, session.StartAt.Year);

                    if (stampVoteBalance == null)
                    {
                        stampVoteBalance = await _voteBalanceService.CreateNewVoteBalance(stampLedgerAccount.VoteId!.Value, session, token);

                        if (stampVoteBalance == null)
                        {
                            throw new FinalAccountException("Unable to Create Stamp Ledger Account Entry");
                        }


                    }


                    if (stampVoteBalance != null && stampVoteBalance.Id.HasValue)
                    {
                        stampVoteBalance.UpdatedAt = session.StartAt;
                        stampVoteBalance.UpdatedBy = token.userId;
                        stampVoteBalance.SystemActionAt = DateTime.Now;
                        stampVoteBalance.OfficeId = token.officeId;
                        stampVoteBalance.ExchangedAmount = (decimal)item.StampAmount!;
                        stampVoteBalance.SessionIdByOffice = session.Id;


                        if (transactionType == CashBookTransactionType.DEBIT)
                        {
                            stampVoteBalance.TransactionType = VoteBalanceTransactionTypes.Income;
                            stampVoteBalance.SubCode = mx.Code;


                            if (stampVoteBalance.ClassificationId == 1)
                            {
                                stampVoteBalance.Credit += (decimal)item.StampAmount!;
                                stampVoteBalance.CreditDebitRunningBalance = stampVoteBalance.Credit - stampVoteBalance.Debit;

                            }
                            else if (stampVoteBalance.ClassificationId == 2)
                            {
                                stampVoteBalance.Credit += (decimal)item.StampAmount!;
                                stampVoteBalance.CreditDebitRunningBalance = stampVoteBalance.Debit - stampVoteBalance.Credit;

                            }
                            else if (stampVoteBalance.ClassificationId == 3)
                            {
                                stampVoteBalance.Credit += (decimal)item.StampAmount!;
                                stampVoteBalance.CreditDebitRunningBalance = stampVoteBalance.Debit - stampVoteBalance.Credit;

                            }
                            else if (stampVoteBalance.ClassificationId == 4)
                            {
                                stampVoteBalance.Credit += (decimal)item.StampAmount!;
                                stampVoteBalance.CreditDebitRunningBalance = stampVoteBalance.Credit - stampVoteBalance.Debit;

                            }
                            else if (stampVoteBalance.ClassificationId == 5)
                            {
                                stampVoteBalance.Debit += (decimal)item.StampAmount!;
                                stampVoteBalance.CreditDebitRunningBalance = stampVoteBalance.Debit - stampVoteBalance.Credit;

                            }
                            else
                            {
                                throw new FinalAccountException("Ledger Account Classification Not Found.");
                            }

                        }



                        else if (transactionType == CashBookTransactionType.CREDIT)
                        {
                            stampVoteBalance.TransactionType = VoteBalanceTransactionTypes.ReverseIncome;

                            if (stampVoteBalance.ClassificationId == 1)
                            {
                                stampVoteBalance.Debit += (decimal)item.StampAmount!;
                                stampVoteBalance.CreditDebitRunningBalance = stampVoteBalance.Credit - stampVoteBalance.Debit;

                            }
                            else if (stampVoteBalance.ClassificationId == 2)
                            {
                                stampVoteBalance.Debit += (decimal)item.StampAmount!;
                                stampVoteBalance.CreditDebitRunningBalance = stampVoteBalance.Debit - stampVoteBalance.Credit;

                            }
                            else if (stampVoteBalance.ClassificationId == 3)
                            {
                                stampVoteBalance.Debit += (decimal)item.StampAmount!;
                                stampVoteBalance.CreditDebitRunningBalance = stampVoteBalance.Debit - stampVoteBalance.Credit;

                            }
                            else if (stampVoteBalance.ClassificationId == 4)
                            {
                                stampVoteBalance.Debit += (decimal)item.StampAmount!;
                                stampVoteBalance.CreditDebitRunningBalance = stampVoteBalance.Credit - stampVoteBalance.Debit;

                            }
                            else if (stampVoteBalance.ClassificationId == 5)
                            {
                                stampVoteBalance.Credit += (decimal)item.StampAmount!;
                                stampVoteBalance.CreditDebitRunningBalance = stampVoteBalance.Debit - stampVoteBalance.Credit;

                            }
                            else
                            {
                                throw new FinalAccountException("Ledger Account Classification Not Found.");
                            }
                        }




                    }
                    else
                    {
                        throw new FinalAccountException("Vote stamp ledger balance not found.");
                    }
                }


                var voteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(item.VoteDetailId!.Value, token.sabhaId, session.StartAt.Year);

                if (voteBalance == null)
                {
                    voteBalance = await _voteBalanceService.CreateNewVoteBalance(item.VoteDetailId!.Value, session, token);

                    if (voteBalance == null)
                    {
                        throw new FinalAccountException("Unable to Create Vote Balance Entry");
                    }


                }


                if (voteBalance != null && voteBalance.Id.HasValue)
                {
                    voteBalance.UpdatedAt = session.StartAt;
                    voteBalance.UpdatedBy = token.userId;


                    if (transactionType == CashBookTransactionType.DEBIT)
                    {
                        voteBalance.TransactionType = VoteBalanceTransactionTypes.Income;
                        voteBalance.SubCode = mx.Code;


                        if (voteBalance.ClassificationId == 1)
                        {
                            voteBalance.Credit += (decimal)item.Amount!;
                            voteBalance.CreditDebitRunningBalance = voteBalance.Credit - voteBalance.Debit;

                        }
                        else if (voteBalance.ClassificationId == 2)
                        {
                            voteBalance.Credit += (decimal)item.Amount!;
                            voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;

                        }
                        else if (voteBalance.ClassificationId == 3)
                        {
                            voteBalance.Credit += (decimal)item.Amount!;
                            voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;

                        }
                        else if (voteBalance.ClassificationId == 4)
                        {
                            voteBalance.Credit += (decimal)item.Amount!;
                            voteBalance.CreditDebitRunningBalance = voteBalance.Credit - voteBalance.Debit;

                        }
                        else if (voteBalance.ClassificationId == 5)
                        {
                            voteBalance.Debit += (decimal)item.Amount!;
                            voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;

                        }
                        else
                        {
                            throw new FinalAccountException("Ledger Account Classification Not Found.");
                        }

                    }



                    else if (transactionType == CashBookTransactionType.CREDIT)
                    {
                        voteBalance.TransactionType = VoteBalanceTransactionTypes.ReverseIncome;

                        if (voteBalance.ClassificationId == 1)
                        {
                            voteBalance.Debit += (decimal)item.Amount!;
                            voteBalance.CreditDebitRunningBalance = voteBalance.Credit - voteBalance.Debit;

                        }
                        else if (voteBalance.ClassificationId == 2)
                        {
                            voteBalance.Debit += (decimal)item.Amount!;
                            voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;

                        }
                        else if (voteBalance.ClassificationId == 3)
                        {
                            voteBalance.Debit += (decimal)item.Amount!;
                            voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;

                        }
                        else if (voteBalance.ClassificationId == 4)
                        {
                            voteBalance.Debit += (decimal)item.Amount!;
                            voteBalance.CreditDebitRunningBalance = voteBalance.Credit - voteBalance.Debit;

                        }
                        else if (voteBalance.ClassificationId == 5)
                        {
                            voteBalance.Credit += (decimal)item.Amount!;
                            voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;

                        }
                        else
                        {
                            throw new FinalAccountException("Ledger Account Classification Not Found.");
                        }
                    }


                    if (voteBalance.ClassificationId == 2)
                    {
                        voteBalance.RunningBalance = (decimal)voteBalance.AllocationAmount! + voteBalance.Debit - voteBalance.Credit;
                    }

                    voteBalance.ExchangedAmount = (decimal)item.Amount!;

                    voteBalance.OfficeId = token.officeId;
                    voteBalance.UpdatedBy = token.userId;
                    voteBalance.UpdatedAt = session.StartAt;
                    voteBalance.SystemActionAt = DateTime.Now;

                    var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalance);
                    await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);


                    if (transactionType == CashBookTransactionType.DEBIT && item.Id == 4)
                    {



                        var IncomeSubtitleId = await _unitOfWork.VoteDetails.IsDepositVote(voteBalance.VoteDetailId.Value);

                        if (IncomeSubtitleId.HasValue)
                        {

                            var newDeposit = new Deposit
                            {
                                DepositSubCategoryId = IncomeSubtitleId.Value,
                                InitialDepositAmount = (decimal)item.Amount!,
                                DepositDate = session.StartAt,
                                Description = item.Description,
                                CreatedAt = session.StartAt,
                                CreatedBy = token.userId,
                                SabhaId = token.sabhaId,
                                OfficeId = token.officeId,
                                LedgerAccountId = voteBalance.VoteDetailId.Value!,
                                MixOrderId = mx.Id,
                                MixOrderLineId = item.Id,
                                ReceiptNo = mx.Code,
                                IsEditable = false,
                                PartnerId = mx.PartnerId!.Value,
                                SystemCreateAt = DateTime.Now,

                            };

                            await _unitOfWork.Deposits.AddAsync(newDeposit);

                        }
                    }

                    //else if(transactionType == CashBookTransactionType.CREDIT)
                    //{
                    //    var IncomeSubtitleId = await _unitOfWork.VoteDetails.IsDepositVote(voteBalance.VoteDetailId.Value);

                    //    if (IncomeSubtitleId.HasValue)
                    //    {



                    //    }
                    //}


                }
                else
                {
                    throw new FinalAccountException("Vote balance not found.");
                }



            }

            if (transactionType == CashBookTransactionType.DEBIT)
            {
                var depostis = await _unitOfWork.Deposits.ClearDepots(mx.Id);

                foreach (var item in depostis)
                {
                    item.Status = 0;
                    item.UpdatedAt = session.StartAt;
                    item.UpdatedBy = token.userId;
                    item.SystemUpdateAt = DateTime.Now;

                }
            }



            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}