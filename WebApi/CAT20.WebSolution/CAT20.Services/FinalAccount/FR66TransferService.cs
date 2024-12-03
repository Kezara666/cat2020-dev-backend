using AutoMapper;
using CAT20.Core.DTO.Final;
using CAT20.Core.DTO.OtherModule;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.User;
using CAT20.Core.Models.Vote;
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
using CAT20.Core.CustomExceptions;

namespace CAT20.Services.FinalAccount
{
    public class FR66TransferService: IFR66TransferService
    {
        private readonly IVoteUnitOfWork _unitOfWork;
        private readonly IUserDetailService _userDetailService;
        private readonly IMapper _mapper;
        private readonly IPartnerService _partnerService;
        private readonly IMixinOrderService _mixinOrderService;

        public FR66TransferService(IVoteUnitOfWork unitOfWork, IUserDetailService userDetailServiceService, IMapper mapper, IPartnerService partnerService, IMixinOrderService mixinOrderService)
        {
            _unitOfWork = unitOfWork;
            _userDetailService = userDetailServiceService;
            _mapper = mapper;
            _partnerService = partnerService;
            _mixinOrderService = mixinOrderService;
        }

        public async Task<bool> Create(FR66Transfer newTransfer, HTokenClaim token)
        {
            try
            {
                var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                if (session != null)
                {

                    var docSeqNums = await _unitOfWork.FinalAccountSequenceNumber.GetNextSequenceNumberForYearSabhaModuleType(session.CreatedAt.Year, token.sabhaId, FinalAccountModuleType.FR66);

                    if (docSeqNums != null)
                    {
                        newTransfer.FR66No = $"{token.sabhaId}/{docSeqNums.Year}/{docSeqNums.Prefix}/{++docSeqNums.LastIndex}";


                        //newJournalAdjustment.JournalNo = "ABC";
                        newTransfer.SabahId = token.sabhaId;
                        newTransfer.OfficeId = token.officeId;
                        newTransfer.RequestDate = session.StartAt;
                        newTransfer.RequestBy = token.userId;
                        newTransfer.SystemRequestDate = DateTime.Now;
                        newTransfer.ActionState = VoteTransferActions.Init;

                        await _unitOfWork.FR66Transfer.AddAsync(newTransfer);

                        foreach (var item in newTransfer.FR66FromItems!)
                        {
                            var voteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(item.FromVoteDetailId, token.sabhaId, session.StartAt.Year);
                            var clasification = await _unitOfWork.VoteDetails.GetVoteClassification(item.FromVoteDetailId);

                            if (voteBalance != null && clasification != null)
                            {
                                if (clasification == 1)
                                {




                                }

                                if (clasification == 2)
                                {
                                    if (voteBalance.TransferFlag == VoteTransferFlag.To)
                                    {
                                        throw new FinalAccountException("Vote Balance Not Allowed For Transfer");
                                    }
                                    else
                                    {

                                        item.RequestSnapshotAllocation = voteBalance.AllocationAmount;
                                        item.RequestSnapshotBalance = (voteBalance.AllocationAmount - voteBalance.Debit + voteBalance.Credit);
                                    }
                                   

                                }

                                if (clasification == 3)
                                {




                                }

                                if (clasification == 4)
                                {


                                }

                            }
                            else
                            {
                                throw new Exception("Unable to Find Vote Balance");
                            }
                        }


                        foreach (var item in newTransfer.FR66ToItems!)
                        {
                            var voteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(item.ToVoteDetailId, token.sabhaId, session.StartAt.Year);

                            var clasification = await _unitOfWork.VoteDetails.GetVoteClassification(item.ToVoteDetailId);

                            if (voteBalance != null && clasification != null)
                            {


                                if (clasification == 1)
                                {



                                }

                                if (clasification == 2)
                                {


                                    if (voteBalance.TransferFlag == VoteTransferFlag.From)
                                    {
                                        throw new FinalAccountException("Vote Balance Not Allowed For Transfer");
                                    }
                                    else
                                    {

                                        item.RequestSnapshotAllocation = voteBalance.AllocationAmount;
                                        item.RequestSnapshotBalance = (voteBalance.AllocationAmount - voteBalance.Debit + voteBalance.Credit);

                                    }
                                  


                                }

                                if (clasification == 3)
                                {


                                }

                                if (clasification == 4)
                                {


                                }


                            }
                            else
                            {
                                throw new FinalAccountException("Unable to Find Vote Balance");
                            }

                        }

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


            }
            catch (Exception ex)
            {
                return false;

            }
        }


        public async Task<FR66TransferResource> GetFR66ById(int id, HTokenClaim token)
        {
            var transfer = await _unitOfWork.FR66Transfer.GetFR66TransferById(id, token);

            var vjnl = _mapper.Map<FR66Transfer, FR66TransferResource>(transfer);


            if (vjnl.RequestBy.HasValue)
            {
                vjnl.UserRequestBy = _mapper.Map<UserDetail, FinalUserActionByResources>(await _userDetailService.GetUserDetailById(vjnl.RequestBy!.Value));
            }

            if (vjnl.ActionBy.HasValue)
            {
                vjnl.UserActionBy = _mapper.Map<UserDetail, FinalUserActionByResources>(await _userDetailService.GetUserDetailById(vjnl.ActionBy!.Value));
            }


            foreach (var fromItem in vjnl.FR66FromItems)
            {

                fromItem.FromVoteDetail = _mapper.Map<VoteDetail, VoteDetailLimitedresource>(await _unitOfWork.VoteDetails.GetByIdAsync(fromItem.FromVoteDetailId));

            }

            foreach (var toItem in vjnl.FR66ToItems)
            {

                toItem.ToVoteDetail = _mapper.Map<VoteDetail, VoteDetailLimitedresource>(await _unitOfWork.VoteDetails.GetByIdAsync(toItem.ToVoteDetailId));

            }

            return vjnl;

        }

        public async Task<(int totalCount, IEnumerable<FR66TransferResource> list)> GetAllFR66TransferForSabha(int sabhaId, int pageNo, int pageSize, string? filterKeyWord)
        {
            var fr66Transfers = await _unitOfWork.FR66Transfer.GetAllFR66TransferForSabha(sabhaId, pageNo, pageSize, filterKeyWord);

            var fr66TransferResources = _mapper.Map<IEnumerable<FR66Transfer>, IEnumerable<FR66TransferResource>>(fr66Transfers.list);

            foreach (var vjnl in fr66TransferResources)
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

            return (fr66Transfers.totalCount, fr66TransferResources);
        }

        public async Task<(int totalCount, IEnumerable<FR66TransferResource> list)> GetFR66TransferFroApproval(int sabhaId, int pageNo, int pageSize, string? filterKeyWord)
        {
            var fr66Transfers = await _unitOfWork.FR66Transfer.GetFR66TransferFroApproval(sabhaId, pageNo, pageSize, filterKeyWord);

            var fr66TransferResources = _mapper.Map<IEnumerable<FR66Transfer>, IEnumerable<FR66TransferResource>>(fr66Transfers.list);

            foreach (var vjnl in fr66TransferResources)
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


            return (fr66Transfers.totalCount, fr66TransferResources);

        }

        public async Task<(bool,string?)> MakeApproveReject(MakeVoteTransferApproveRejectResource request, HTokenClaim token)
        {

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var transfer = await _unitOfWork.FR66Transfer.GetFR66TransferById(request.Id, token);

                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                    if (session != null)
                    {


                        if (transfer != null)
                        {
                            transfer.ActionDate = session.StartAt;
                            transfer.ActionBy = token.userId;
                            transfer.ActionNote = request.ActionNote;
                            transfer.SystemActionDate = DateTime.Now;

                            if (request.State == 1)
                            {
                                transfer.ActionState = VoteTransferActions.Approve;
                                foreach (var item in transfer.FR66FromItems!)
                                {
                                    var voteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(item.FromVoteDetailId, token.sabhaId, session.StartAt.Year);
                                    var clasification = await _unitOfWork.VoteDetails.GetVoteClassification(item.FromVoteDetailId);

                                    if (voteBalance != null && clasification != null)
                                    {
                                        if (clasification == 1)
                                        {


                                          

                                        }

                                        if (clasification == 2)
                                        {
                                            if (voteBalance.TransferFlag == VoteTransferFlag.To)
                                            {
                                                throw new FinalAccountException("Vote Balance Not Allowed For Transfer");
                                            }
                                            else
                                            {

                                                item.ActionSnapshotAllocation = voteBalance.AllocationAmount;
                                                item.ActionSnapshotBalance = (voteBalance.AllocationAmount - voteBalance.Debit + voteBalance.Credit);

                                                if (voteBalance.AllocationAmount - item.FromAmount > voteBalance.Debit - voteBalance.Credit-voteBalance.TotalPending - voteBalance.TotalHold -voteBalance.TotalCommitted)
                                                {
                                                    voteBalance.AllocationAmount -= item.FromAmount;
                                                    voteBalance.TransferFlag = VoteTransferFlag.From;


                                                }
                                                else
                                                {
                                                    var vote = await _unitOfWork.VoteDetails.GetByIdAsync(item.FromVoteDetailId);

                                                    throw new FinalAccountException($"Insufficient Balance Vote {vote.Code}");
                                                }

                                                //voteBalance.Debit += item.FromAmount;
                                                voteBalance.AllocationExchangeAmount = item.FromAmount;
                                                voteBalance.UpdatedBy = token.userId;
                                                voteBalance.UpdatedAt = session.StartAt;
                                                voteBalance.SystemActionAt = DateTime.Now;
                                                voteBalance.OfficeId = token.officeId;
                                                voteBalance.Code = "FR66";
                                                voteBalance.SubCode = transfer.FR66No;

                                                voteBalance.TransactionType = VoteBalanceTransactionTypes.FRCredit;
                                                voteBalance.RunningBalance = (decimal)voteBalance.AllocationAmount! - voteBalance.CreditDebitRunningBalance;
                                            }

                                        }

                                        if (clasification == 3)
                                        {

                                           


                                        }

                                        if (clasification == 4)
                                        {

                                           
                                        }

                                        //var vtb = new VoteLedgerBook
                                        //{
                                        //    SabhaId = token.sabhaId,
                                        //    OfiiceId = token.officeId,
                                        //    SessionId = session.Id,
                                        //    Description = transfer.FR66No,
                                        //    Date = session.StartAt,
                                        //    VoteBalanceId = (int)voteBalance.Id!,
                                        //    VoteDetailId = item.FromVoteDetailId,
                                        //    TransactionType = CashBookTransactionType.DEBIT,
                                        //    VoteBalanceTransactionTypes = voteBalance.TransactionType,
                                        //    Credit = item.FromAmount,
                                        //    Debit = 0,
                                        //    RunningTotal = voteBalance.CreditDebitRunningBalance,
                                        //    RowStatus = 1,
                                        //    CreatedAt = session.StartAt,
                                        //    UpdatedAt = session.StartAt,
                                        //    CreatedBy = token.userId,
                                        //    UpdatedBy = token.userId,
                                        //    SystemActionDate = DateTime.Now
                                        //};

                                        ////await _unitOfWork.VoteLedgerBook.AddAsync(vtb);
                                        var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalance);
                                        await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                                    }
                                    else
                                    {
                                        throw new Exception("Unable to Find Vote Balance");
                                    }
                                }

                                await _unitOfWork.CommitAsync();

                                foreach (var item in transfer.FR66ToItems!)
                                {
                                    var voteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(item.ToVoteDetailId, token.sabhaId, session.StartAt.Year);

                                    var clasification = await _unitOfWork.VoteDetails.GetVoteClassification(item.ToVoteDetailId);

                                    if (voteBalance != null && clasification != null)
                                    {


                                        if (clasification == 1)
                                        {

                                          

                                        }

                                        if (clasification == 2)
                                        {
                                            if (voteBalance.TransferFlag == VoteTransferFlag.From)
                                            {
                                                throw new FinalAccountException("Vote Balance Not Allowed For Transfer");
                                            }
                                            else
                                            {

                                                item.ActionSnapshotAllocation = voteBalance.AllocationAmount;
                                                item.ActionSnapshotBalance = (voteBalance.AllocationAmount - voteBalance.Debit + voteBalance.Credit);

                                                voteBalance.AllocationAmount += item.ToAmount;
                                                voteBalance.AllocationExchangeAmount = item.ToAmount;
                                                voteBalance.UpdatedBy = token.userId;
                                                voteBalance.UpdatedAt = session.StartAt;
                                                voteBalance.SystemActionAt = DateTime.Now;
                                                voteBalance.OfficeId = token.officeId;
                                                voteBalance.TransactionType = VoteBalanceTransactionTypes.FRDebit;
                                                voteBalance.TransferFlag = VoteTransferFlag.To;
                                                voteBalance.Code = "FR66";
                                                voteBalance.SubCode = transfer.FR66No;
                                                voteBalance.RunningBalance = (decimal)voteBalance.AllocationAmount! - voteBalance.CreditDebitRunningBalance;

                                            }


                                        }

                                        if (clasification == 3)
                                        {


                                        }

                                        if (clasification == 4)
                                        {


                                        }

                                        //var vtb = new VoteLedgerBook
                                        //{
                                        //    SabhaId = token.sabhaId,
                                        //    OfiiceId = token.officeId,
                                        //    SessionId = session.Id,
                                        //    Description = transfer.FR66No,
                                        //    Date = session.StartAt,
                                        //    VoteBalanceId = (int)voteBalance.Id!,
                                        //    VoteDetailId = item.ToVoteDetailId,
                                        //    TransactionType = CashBookTransactionType.DEBIT,
                                        //    VoteBalanceTransactionTypes = voteBalance.TransactionType,
                                        //    Credit = 0,
                                        //    Debit = item.ToAmount,
                                        //    RunningTotal = voteBalance.CreditDebitRunningBalance,
                                        //    RowStatus = 1,
                                        //    CreatedAt = session.StartAt,
                                        //    UpdatedAt = session.StartAt,
                                        //    CreatedBy = token.userId,
                                        //    UpdatedBy = token.userId,
                                        //    SystemActionDate = DateTime.Now
                                        //};

                                        ////await _unitOfWork.VoteLedgerBook.AddAsync(vtb);
                                        var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalance);
                                        await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);

                                    }
                                    else
                                    {
                                        throw new FinalAccountException("Unable to Find Vote Balance");
                                    }

                                }

                                await _unitOfWork.CommitAsync();
                            }
                            else if (request.State == 0)
                            {
                                transfer.ActionState = VoteTransferActions.Reject;
                                await _unitOfWork.CommitAsync();
                                //transaction.Commit();
                            }
                            else
                            {
                                throw new FinalAccountException("Unable to Perform Action");
                            }

                            transaction.Commit();
                            return (true,"Saved Successfully");

                        }
                        else
                        {
                            throw new FinalAccountException("Unable To Find Transfer");
                        }
                    }
                    else
                    {
                        throw new FinalAccountException("No Active Session Found");
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

        public async Task<bool> WithdrawFR66Transfer(int journalId, HTokenClaim token)
        {
            try
            {
                var transfer = await _unitOfWork.FR66Transfer.GetFR66TransferById(journalId, token);

                var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                if (session != null)
                {


                    if (transfer != null && transfer.ActionState == VoteTransferActions.Init)
                    {

                        transfer.ActionState = VoteTransferActions.withdraw;
                        transfer.ActionDate = session.StartAt;
                        transfer.SystemActionDate = DateTime.Now;

                        await _unitOfWork.CommitAsync();

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
    }
}
