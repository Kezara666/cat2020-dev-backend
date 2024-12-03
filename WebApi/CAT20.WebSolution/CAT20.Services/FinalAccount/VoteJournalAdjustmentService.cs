using AutoMapper;
using CAT20.Core.Services.Control;
using CAT20.Core;
using CAT20.Core.Services.FinalAccount;
using CAT20.Core.Services.Mixin;
using CAT20.Core.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Vote;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.Enums;
using CAT20.WebApi.Resources.FInalAccount;
using CAT20.Core.DTO.Final;
using CAT20.Core.DTO.OtherModule;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.User;
using CAT20.Core.Models.Interfaces;
using Org.BouncyCastle.Asn1.Ocsp;

namespace CAT20.Services.FinalAccount
{
    public class VoteJournalAdjustmentService : IVoteJournalAdjustmentService
    {

        private readonly IVoteUnitOfWork _unitOfWork;
        private readonly IUserDetailService _userDetailService;
        private readonly IMapper _mapper;
        private readonly IPartnerService _partnerService;
        private readonly IMixinOrderService _mixinOrderService;

        public VoteJournalAdjustmentService(IVoteUnitOfWork unitOfWork, IUserDetailService userDetailServiceService, IMapper mapper, IPartnerService partnerService, IMixinOrderService mixinOrderService)
        {
            _unitOfWork = unitOfWork;
            _userDetailService = userDetailServiceService;
            _mapper = mapper;
            _partnerService = partnerService;
            _mixinOrderService = mixinOrderService;
        }

        public async Task<bool> Create(VoteJournalAdjustment newJournalAdjustment, HTokenClaim token)
        {
            try
            {
                var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                if (session != null)
                {

                    var docSeqNums = await _unitOfWork.FinalAccountSequenceNumber.GetNextSequenceNumberForYearSabhaModuleType(session.CreatedAt.Year, token.sabhaId, FinalAccountModuleType.Journal);

                    if (docSeqNums != null)
                    {
                        newJournalAdjustment.JournalNo = $"{token.sabhaId}/{docSeqNums.Year}/{docSeqNums.Prefix}/{++docSeqNums.LastIndex}";


                        //newJournalAdjustment.JournalNo = "ABC";
                        newJournalAdjustment.SabahId = token.sabhaId;
                        newJournalAdjustment.OfficeId = token.officeId;
                        newJournalAdjustment.RequestDate = session.StartAt;
                        newJournalAdjustment.RequestBy = token.userId;
                        newJournalAdjustment.SystemRequestDate = DateTime.Now;
                        newJournalAdjustment.ActionState = VoteJournalAdjustmentActions.Init;

                        await _unitOfWork.VoteJournalAdjustments.AddAsync(newJournalAdjustment);
                        await _unitOfWork.CommitAsync();
                        return true;
                    }
                    else
                    {
                        throw new Exception("Unable to Create Journal Number");
                    }
                }
                else
                {
                    throw new Exception("No Active Session Found");
                }


            }catch (Exception ex)
            {
               return  false;

            }
        }


        public async Task<VoteBalance> GetForJournalAdjustment(int voteDetailId, HTokenClaim token)
        {
            var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
            if (session != null)
            {

                return await _unitOfWork.VoteJournalAdjustments.GetForJournalAdjustment(voteDetailId, session.StartAt.Year);
            }
            else
            {
                return null;
            }
        }

        public async Task<VoteJournalAdjustmentResource> GetJournalAdjustmentById(int id,HTokenClaim token)
        {
            var voteJournalAdjustment = await _unitOfWork.VoteJournalAdjustments.GetJournalAdjustmentById(id, token);

            var vjnl = _mapper.Map<VoteJournalAdjustment, VoteJournalAdjustmentResource>(voteJournalAdjustment);

           
                if (vjnl.RequestBy.HasValue)
                {
                    vjnl.UserRequestBy = _mapper.Map<UserDetail, FinalUserActionByResources>(await _userDetailService.GetUserDetailById(vjnl.RequestBy!.Value));
                }

                if (vjnl.ActionBy.HasValue)
                {
                    vjnl.UserActionBy = _mapper.Map<UserDetail, FinalUserActionByResources>(await _userDetailService.GetUserDetailById(vjnl.ActionBy!.Value));
                }


                foreach(var fromItem in vjnl.VoteJournalItemsFrom)
                {

                         fromItem.FromVoteDetail = _mapper.Map<VoteDetail, VoteDetailLimitedresource>(await _unitOfWork.VoteDetails.GetByIdAsync(fromItem.FromVoteDetailId));

                }

                 foreach (var toItem in vjnl.VoteJournalItemsTo)
                {

                toItem.ToVoteDetail = _mapper.Map<VoteDetail, VoteDetailLimitedresource>(await _unitOfWork.VoteDetails.GetByIdAsync(toItem.ToVoteDetailId));

                }

            return vjnl;
        
        }

        public async Task<(int totalCount, IEnumerable<VoteJournalAdjustmentResource> list)> GetAllJournalAdjustmentForSabha(int sabhaId, int pageNo, int pageSize, string? filterKeyWord)
        {
           var voteJournalAdjustments = await _unitOfWork.VoteJournalAdjustments.GetAllJournalAdjustmentForSabha(sabhaId, pageNo, pageSize, filterKeyWord);

            var voteJournalAdjustmentsresources = _mapper.Map<IEnumerable<VoteJournalAdjustment>, IEnumerable<VoteJournalAdjustmentResource>>(voteJournalAdjustments.list);

            foreach (var vjnl in voteJournalAdjustmentsresources)
            {
                if (vjnl.RequestBy.HasValue)
                {
                    vjnl.UserRequestBy = _mapper.Map<UserDetail, FinalUserActionByResources>(await _userDetailService.GetUserDetailById(vjnl.RequestBy!.Value));
                }

                if (vjnl.ActionBy.HasValue)
                {
                    vjnl.UserActionBy = _mapper.Map<UserDetail, FinalUserActionByResources>(await _userDetailService.GetUserDetailById(vjnl.ActionBy!.Value));
                }
            }

            return (voteJournalAdjustments.totalCount, voteJournalAdjustmentsresources);
        }

        public async Task<(int totalCount, IEnumerable<VoteJournalAdjustmentResource> list)> GetJournalAdjustmentFroApproval(int sabhaId, int pageNo, int pageSize, string? filterKeyWord)
        {
            var voteJournalAdjustments = await  _unitOfWork.VoteJournalAdjustments.GetJournalAdjustmentFroApproval(sabhaId,pageNo,pageSize,filterKeyWord);

            var voteJournalAdjustmentsresources = _mapper.Map<IEnumerable<VoteJournalAdjustment>, IEnumerable<VoteJournalAdjustmentResource>>(voteJournalAdjustments.list);

            foreach (var vjnl in voteJournalAdjustmentsresources)
            {
                if (vjnl.RequestBy.HasValue)
                {
                    vjnl.UserRequestBy = _mapper.Map<UserDetail, FinalUserActionByResources>(await _userDetailService.GetUserDetailById(vjnl.RequestBy!.Value));
                }

                if (vjnl.ActionBy.HasValue)
                {
                    vjnl.UserActionBy = _mapper.Map<UserDetail, FinalUserActionByResources>(await _userDetailService.GetUserDetailById(vjnl.ActionBy!.Value));
                }
            }


            return (voteJournalAdjustments.totalCount, voteJournalAdjustmentsresources);

        }

        public async Task<bool> MakeApproveReject(MakeVoteJournalApproveRejectResource request, HTokenClaim token)
        {

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {

                    /*
                     * 
                    classification = 1 => income
                    classification = 2 => expense
                    classification = 3 => asset
                    classification = 4 => liability
                    classification = 5 => equity
                     
                     
                     */


                    var jnl = await _unitOfWork.VoteJournalAdjustments.GetJournalAdjustmentById(request.JournalId, token);

                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                    if (session != null)
                    {


                        if (jnl != null)
                        {
                            jnl.ActionDate = session.StartAt;
                            jnl.ActionBy = token.userId;
                            jnl.ActionNote = request.ActionNote;
                            jnl.SystemActionDate = DateTime.Now;
                           

                            if (request.State == 1)
                            {
                                jnl.ActionState = VoteJournalAdjustmentActions.Approve;

                                foreach (var item in jnl.VoteJournalItemsFrom!)
                                {
                                    var voteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(item.FromVoteDetailId, token.sabhaId, session.StartAt.Year);
                                    var clasification = await _unitOfWork.VoteDetails.GetVoteClassification(item.FromVoteDetailId);

                                    var cbTransactionType =  CashBookTransactionType.DEBIT;
                                    decimal credit = 0m;
                                    decimal debit = 0m;

                                    if (voteBalance != null && clasification !=null)
                                    {
                                        if (clasification == 1)
                                        {


                                            voteBalance.UpdatedBy = token.userId;
                                            voteBalance.UpdatedAt = session.StartAt;
                                            voteBalance.SystemActionAt = DateTime.Now;

                                            debit = item.FromAmount;
                                            credit =0m;
                                            cbTransactionType = CashBookTransactionType.DEBIT;

                                            voteBalance.Debit += item.FromAmount;
                                            voteBalance.CreditDebitRunningBalance = voteBalance.Credit - voteBalance.Debit;
                                            voteBalance.ExchangedAmount = item.FromAmount;
                                            voteBalance.TransactionType = VoteBalanceTransactionTypes.JournalDebit;
                                            voteBalance.Code = "JNL";
                                            voteBalance.SubCode = jnl.JournalNo;


                                        }

                                        if (clasification == 2)
                                        {

                                            voteBalance.UpdatedBy = token.userId;
                                            voteBalance.OfficeId  = token.officeId;
                                            voteBalance.UpdatedAt = session.StartAt;
                                            voteBalance.SystemActionAt = DateTime.Now;

                                            credit = item.FromAmount;
                                            debit = 0m;
                                            cbTransactionType = CashBookTransactionType.CREDIT;

                                            voteBalance.Credit += item.FromAmount;
                                            voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;
                                            voteBalance.ExchangedAmount = item.FromAmount;
                                            voteBalance.RunningBalance = (decimal)voteBalance.AllocationAmount! + voteBalance.Credit - voteBalance.Debit;
                                            voteBalance.TransactionType = VoteBalanceTransactionTypes.JournalCredit;
                                            voteBalance.Code = "JNL";
                                            voteBalance.SubCode= jnl.JournalNo;

                                        }

                                        if (clasification == 3)
                                        {

                                            voteBalance.Credit += item.FromAmount;
                                            voteBalance.UpdatedBy = token.userId;
                                            voteBalance.UpdatedAt = session.StartAt;
                                            voteBalance.SystemActionAt = DateTime.Now;
                                            voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;
                                            voteBalance.ExchangedAmount = item.FromAmount;
                                            voteBalance.TransactionType = VoteBalanceTransactionTypes.JournalCredit;
                                            voteBalance.Code = "JNL";
                                            voteBalance.SubCode = jnl.JournalNo;


                                        }

                                        if (clasification == 4)
                                        {

                                            voteBalance.Credit += item.FromAmount;
                                            voteBalance.UpdatedBy = token.userId;
                                            voteBalance.UpdatedAt = session.StartAt;
                                            voteBalance.SystemActionAt = DateTime.Now;
                                            voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;
                                            voteBalance.ExchangedAmount = item.FromAmount;
                                            voteBalance.TransactionType = VoteBalanceTransactionTypes.JournalCredit;
                                            voteBalance.Code = "JNL";
                                            voteBalance.SubCode = jnl.JournalNo;

                                        }

                                        //var vtb = new VoteLedgerBook
                                        //{
                                        //    SabhaId = token.sabhaId,
                                        //    OfiiceId = token.officeId,
                                        //    SessionId = session.Id,
                                        //    Description = jnl.JournalNo,
                                        //    Date = session.StartAt,
                                        //    VoteBalanceId = (int)voteBalance.Id!,
                                        //    VoteDetailId = item.FromVoteDetailId,
                                        //    TransactionType = cbTransactionType,
                                        //    VoteBalanceTransactionTypes = voteBalance.TransactionType,
                                        //    Credit = credit,
                                        //    Debit = debit,
                                        //    RunningTotal = voteBalance.CreditDebitRunningBalance,
                                        //    RowStatus = 1,
                                        //    CreatedAt = session.StartAt,
                                        //    UpdatedAt = session.StartAt,
                                        //    CreatedBy = token.userId,
                                        //    UpdatedBy = token.userId,
                                        //    SystemActionDate = DateTime.Now
                                        //};

                                        //await _unitOfWork.VoteLedgerBook.AddAsync(vtb);
                                        var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalance);
                                        await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                                    }
                                    else
                                    {
                                        throw new Exception("Unable to Find Vote Balance");
                                    }
                                }

                                await _unitOfWork.CommitAsync();

                                foreach (var item in jnl.VoteJournalItemsTo!)
                                {
                                    var voteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(item.ToVoteDetailId, token.sabhaId, session.StartAt.Year);

                                    var clasification = await _unitOfWork.VoteDetails.GetVoteClassification(item.ToVoteDetailId);

                                    var cbTransactionType = CashBookTransactionType.DEBIT;
                                    decimal credit = 0m;
                                    decimal debit = 0m;

                                    if (voteBalance != null && clasification !=null)
                                    {


                                        if (clasification == 1)
                                        {

                                            voteBalance.UpdatedBy = token.userId;
                                            voteBalance.UpdatedAt = session.StartAt;
                                            voteBalance.SystemActionAt = DateTime.Now;

                                            credit = item.ToAmount;
                                            debit = 0m;
                                            cbTransactionType = CashBookTransactionType.CREDIT;

                                            voteBalance.Credit += item.ToAmount;
                                            voteBalance.CreditDebitRunningBalance = voteBalance.Credit - voteBalance.Debit;
                                            voteBalance.ExchangedAmount = item.ToAmount;
                                            voteBalance.TransactionType = VoteBalanceTransactionTypes.JournalCredit;
                                            voteBalance.Code = "JNL";
                                            voteBalance.SubCode = jnl.JournalNo;

                                        }

                                        if (clasification == 2)
                                        {

                                            voteBalance.UpdatedBy = token.userId;
                                            voteBalance.UpdatedAt = session.StartAt;
                                            voteBalance.SystemActionAt = DateTime.Now;

                                            debit = item.ToAmount;
                                            credit = 0m;
                                            cbTransactionType = CashBookTransactionType.DEBIT;

                                            voteBalance.Debit += item.ToAmount;
                                            voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;
                                            voteBalance.RunningBalance = (decimal)voteBalance.AllocationAmount! + voteBalance.Credit - voteBalance.Debit;
                                            voteBalance.ExchangedAmount = item.ToAmount;
                                            voteBalance.TransactionType = VoteBalanceTransactionTypes.JournalDebit;
                                            voteBalance.Code = "JNL";
                                            voteBalance.SubCode = jnl.JournalNo;


                                        }

                                        if (clasification == 3)
                                        {

                                            voteBalance.Debit += item.ToAmount;
                                            voteBalance.UpdatedBy = token.userId;
                                            voteBalance.UpdatedAt = session.StartAt;
                                            voteBalance.SystemActionAt = DateTime.Now;
                                            voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;
                                            voteBalance.ExchangedAmount = item.ToAmount;
                                            voteBalance.TransactionType = VoteBalanceTransactionTypes.JournalDebit;
                                            voteBalance.Code = "JNL";
                                            voteBalance.SubCode = jnl.JournalNo;

                                        }

                                        if (clasification == 4)
                                        {

                                            voteBalance.Debit += item.ToAmount;
                                            voteBalance.UpdatedBy = token.userId;
                                            voteBalance.UpdatedAt = session.StartAt;
                                            voteBalance.SystemActionAt = DateTime.Now;
                                            voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;
                                            voteBalance.ExchangedAmount = item.ToAmount;
                                            voteBalance.TransactionType = VoteBalanceTransactionTypes.JournalDebit;
                                            voteBalance.Code = "JNL";
                                            voteBalance.SubCode = jnl.JournalNo;

                                        }

                                        //var vtb = new VoteLedgerBook
                                        //{
                                        //    SabhaId = token.sabhaId,
                                        //    OfiiceId = token.officeId,
                                        //    SessionId = session.Id,
                                        //    Description = jnl.JournalNo,
                                        //    Date = session.StartAt,
                                        //    VoteBalanceId = (int)voteBalance.Id!,
                                        //    VoteDetailId = item.ToVoteDetailId,
                                        //    TransactionType = cbTransactionType,
                                        //    VoteBalanceTransactionTypes = voteBalance.TransactionType,
                                        //    Credit = credit,
                                        //    Debit = debit,
                                        //    RunningTotal = voteBalance.CreditDebitRunningBalance,
                                        //    RowStatus = 1,
                                        //    CreatedAt = session.StartAt,
                                        //    UpdatedAt = session.StartAt,
                                        //    CreatedBy = token.userId,
                                        //    UpdatedBy = token.userId,
                                        //    SystemActionDate = DateTime.Now
                                        //};

                                        //await _unitOfWork.VoteLedgerBook.AddAsync(vtb);
                                        var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalance);
                                        await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);

                                    }
                                    else
                                    {
                                        throw new Exception("Unable to Find Vote Balance");
                                    }

                                }

                                await _unitOfWork.CommitAsync();
                            }
                            else if (request.State == 0)
                            {
                                jnl.ActionState = VoteJournalAdjustmentActions.Reject;
                                await _unitOfWork.CommitAsync();
                                //transaction.Commit();
                            }
                            else
                            {
                                throw new Exception("Unable to Perform Action");
                            }

                            transaction.Commit();

                        }
                        else
                        {
                            throw new Exception("Unable To Find Journal");
                        }
                    }
                    else
                    {
                        throw new Exception("No Active Session Found");
                    }

                    return true;

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public async Task<bool> WithdrawJournalAdjustment(int journalId, HTokenClaim token)
        {
            try
            {
                var jnl = await _unitOfWork.VoteJournalAdjustments.GetJournalAdjustmentById(journalId, token);

                var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                if (session != null)
                {


                    if (jnl != null)
                    {
                     
                            jnl.ActionState = VoteJournalAdjustmentActions.withdraw;
                            jnl.ActionDate = session.StartAt;
                            jnl.SystemActionDate = DateTime.Now;

                       await  _unitOfWork.CommitAsync();

                    }
                    else
                    {
                        throw new Exception("Unable To Find Journal");
                    }
                }
                else
                {
                    throw new Exception("No Active Session Found");
                }

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<(VoteBalance, string)> GetActiveVoteBalance(int VoteDetailId, HTokenClaim token)
        {
            try
            {
                var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                if (session != null)
                {
                    return (await _unitOfWork.VoteBalances.GetActiveVoteBalance(VoteDetailId, token.sabhaId, session.StartAt.Year), "");
                }
                else
                {
                    throw new Exception("No active Session Found");
                }
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }
    }
}
