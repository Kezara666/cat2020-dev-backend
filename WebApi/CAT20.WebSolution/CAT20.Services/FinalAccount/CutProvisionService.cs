using AutoMapper;
using CAT20.Core;
using CAT20.Core.CustomExceptions;
using CAT20.Core.DTO.Final;
using CAT20.Core.DTO.OtherModule;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.User;
using CAT20.Core.Models.Vote;
using CAT20.Core.Services.Control;
using CAT20.Core.Services.FinalAccount;
using CAT20.Core.Services.Mixin;
using CAT20.Core.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.FinalAccount
{
    public class CutProvisionService : ICutProvisionService
    {
        private readonly IVoteUnitOfWork _unitOfWork;
        private readonly IUserDetailService _userDetailService;
        private readonly IMapper _mapper;
        private readonly IPartnerService _partnerService;
        private readonly IMixinOrderService _mixinOrderService;

        public CutProvisionService(IVoteUnitOfWork unitOfWork, IUserDetailService userDetailServiceService, IMapper mapper, IPartnerService partnerService, IMixinOrderService mixinOrderService)
        {
            _unitOfWork = unitOfWork;
            _userDetailService = userDetailServiceService;
            _mapper = mapper;
            _partnerService = partnerService;
            _mixinOrderService = mixinOrderService;
        }



        public async Task<bool> Create(CutProvision newCutProvision, HTokenClaim token)
        {
            try
            {
                var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                if (session != null)
                {

                    var docSeqNums = await _unitOfWork.FinalAccountSequenceNumber.GetNextSequenceNumberForYearSabhaModuleType(session.CreatedAt.Year, token.sabhaId, FinalAccountModuleType.CutProvince);

                    if (docSeqNums != null)
                    {
                        newCutProvision.CPNo = $"{token.sabhaId}/{docSeqNums.Year}/{docSeqNums.Prefix}/{++docSeqNums.LastIndex}";


                        //newJournalAdjustment.JournalNo = "ABC";
                        newCutProvision.SabahId = token.sabhaId;
                        newCutProvision.OfficeId = token.officeId;
                        newCutProvision.RequestDate = session.StartAt;
                        newCutProvision.RequestBy = token.userId;
                        newCutProvision.SystemRequestDate = DateTime.Now;
                        newCutProvision.ActionState = VoteTransferActions.Init;


                        var voteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(newCutProvision.VoteDetailId, token.sabhaId, session.StartAt.Year);

                        var classification = await _unitOfWork.VoteDetails.GetVoteClassification(newCutProvision.VoteDetailId);

                        if (voteBalance != null && classification != null && voteBalance.Id == newCutProvision.VoteBalanceId)
                        {


                            if (classification == 1)
                            {



                            }

                            if (classification == 2)
                            {

                                if (voteBalance.TransferFlag == VoteTransferFlag.To)
                                {
                                    throw new FinalAccountException("Vote Balance Not Allowed For Transfer");
                                }
                                else
                                {

                                    newCutProvision.RequestSnapshotAllocation = voteBalance.AllocationAmount;
                                    newCutProvision.RequestSnapshotBalance = (voteBalance.AllocationAmount - voteBalance.Debit + voteBalance.Credit);
                                }
                               

                              

                            }

                            if (classification == 3)
                            {


                            }

                            if (classification == 4)
                            {


                            }
                                                     

                        }
                        else
                        {
                            throw new FinalAccountException("Unable to Find Vote Balance Or Classification");
                        }

                        await _unitOfWork.CutProvision.AddAsync(newCutProvision);
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

        public async Task<(int totalCount, IEnumerable<CutProvisionResource> list)> GetAllCutProvisionSabha(int sabhaId, int pageNo, int pageSize, string? filterKeyWord)
        {
            var cutProvisions = await _unitOfWork.CutProvision.GetAllCutProvisionSabha(sabhaId, pageNo, pageSize, filterKeyWord);

            var CutProvisionResources = _mapper.Map<IEnumerable<CutProvision>, IEnumerable<CutProvisionResource>>(cutProvisions.list);

            foreach (var c in CutProvisionResources)
            {
                if (c.RequestBy.HasValue)
                {
                    c.UserRequestBy = _mapper.Map<UserDetail, FinalUserActionByResources>(await _userDetailService.GetUserDetailById(c.RequestBy!.Value));
                }

                if (c.ActionBy.HasValue)
                {
                    c.UserActionBy = _mapper.Map<UserDetail, FinalUserActionByResources>(await _userDetailService.GetUserDetailById(c.ActionBy!.Value));
                }
                c.VoteDetail = _mapper.Map<VoteDetail, VoteDetailLimitedresource>(await _unitOfWork.VoteDetails.GetByIdAsync(c.VoteDetailId));
            }

            return (cutProvisions.totalCount, CutProvisionResources);
        }

        public async Task<CutProvisionResource> GetCuProvisionById(int Id, HTokenClaim token)
        {
           var cp= await _unitOfWork.CutProvision.GetByIdAsync(Id);

           
            
            var cutProvisionResources = _mapper.Map<CutProvision, CutProvisionResource>(cp);
            if (cp != null)
            {
            
                if (cutProvisionResources.RequestBy.HasValue)
                {
                    cutProvisionResources.UserRequestBy = _mapper.Map<UserDetail, FinalUserActionByResources>(await _userDetailService.GetUserDetailById(cutProvisionResources.RequestBy!.Value));
                }

                if (cutProvisionResources.ActionBy.HasValue)
                {
                    cutProvisionResources.UserActionBy = _mapper.Map<UserDetail, FinalUserActionByResources>(await _userDetailService.GetUserDetailById(cutProvisionResources.ActionBy!.Value));
                }
                cutProvisionResources.VoteDetail = _mapper.Map<VoteDetail, VoteDetailLimitedresource>(await _unitOfWork.VoteDetails.GetByIdAsync(cutProvisionResources.VoteDetailId));
                
            }
            return cutProvisionResources;
        }


            public async Task<(int totalCount, IEnumerable<CutProvisionResource> list)> GetCutProvisionForApproval(int sabhaId, int pageNo, int pageSize, string? filterKeyWord)
        {
            var cutProvisions = await _unitOfWork.CutProvision.GetCutProvisionForApproval(sabhaId, pageNo, pageSize, filterKeyWord);

            var cutProvisionResources = _mapper.Map<IEnumerable<CutProvision>, IEnumerable<CutProvisionResource>>(cutProvisions.list);

            foreach (var c in cutProvisionResources)
            {
                if (c.RequestBy.HasValue)
                {
                    c.UserRequestBy = _mapper.Map<UserDetail, FinalUserActionByResources>(await _userDetailService.GetUserDetailById(c.RequestBy!.Value));
                }

                if (c.ActionBy.HasValue)
                {
                    c.UserActionBy = _mapper.Map<UserDetail, FinalUserActionByResources>(await _userDetailService.GetUserDetailById(c.ActionBy!.Value));
                }
                c.VoteDetail = _mapper.Map<VoteDetail, VoteDetailLimitedresource>(await _unitOfWork.VoteDetails.GetByIdAsync(c.VoteDetailId));
            }

            return (cutProvisions.totalCount, cutProvisionResources);
        }

        public async Task<(bool,string?)> MakeApproveReject(MakeVoteTransferApproveRejectResource request, HTokenClaim token)
        {
            if (request.Entity == 3)
            {
                using (var transaction = _unitOfWork.BeginTransaction())
                {
                    try
                    {
                        var cutProvision = await _unitOfWork.CutProvision.GetByIdAsync(request.Id);

                        var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                        if (session != null)
                        {


                            if (cutProvision != null)
                            {
                                cutProvision.ActionDate = session.StartAt;
                                cutProvision.ActionBy = token.userId;
                                cutProvision.ActionNote = request.ActionNote;
                                cutProvision.SystemActionDate = DateTime.Now;

                                if (request.State == 1)
                                {
                                    cutProvision.ActionState = VoteTransferActions.Approve; 
                                    var voteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(cutProvision.VoteDetailId, token.sabhaId, session.StartAt.Year);

                                    var classification = await _unitOfWork.VoteDetails.GetVoteClassification(cutProvision.VoteDetailId);

                                    if (voteBalance != null && classification != null && voteBalance.Id == cutProvision.VoteBalanceId)
                                    {


                                        if (classification == 1)
                                        {



                                        }

                                        if (classification == 2)
                                        {

                                            if (voteBalance.TransferFlag == VoteTransferFlag.To)
                                            {
                                                throw new FinalAccountException("Vote Balance Not Allowed For Transfer");
                                            }
                                            else
                                            {

                                                cutProvision.ActionSnapshotAllocation = voteBalance.AllocationAmount;
                                                cutProvision.ActionSnapshotBalance = (voteBalance.AllocationAmount - voteBalance.Debit + voteBalance.Credit);


                                                if (voteBalance.TakeHoldRate > 0)
                                                {
                                                    var holdAmount = ((voteBalance.AllocationAmount-cutProvision.Amount) * voteBalance.TakeHoldRate) / 100;
                                                    voteBalance.TakeHoldAmount = Math.Round((decimal)holdAmount, 2, MidpointRounding.AwayFromZero);

                                                }


                                                if ((voteBalance.AllocationAmount - voteBalance.Debit + voteBalance.Credit - voteBalance.TakeHoldAmount- voteBalance.TotalCommitted -voteBalance.TotalPending-voteBalance.TotalHold) >= cutProvision.Amount)
                                                {
                                                    voteBalance.AllocationAmount -= cutProvision.Amount;
                                                    voteBalance.AllocationExchangeAmount = cutProvision.Amount;
                                                    voteBalance.TransferFlag = VoteTransferFlag.From;


                                                }
                                                else
                                                {
                                                    var vote = await _unitOfWork.VoteDetails.GetByIdAsync(cutProvision.VoteDetailId);

                                                    throw new FinalAccountException($"Insufficient Balance Vote {vote.Code}");
                                                }

                                                voteBalance.UpdatedBy = token.userId;
                                                voteBalance.UpdatedAt = session.StartAt;
                                                voteBalance.SystemActionAt = DateTime.Now;
                                                voteBalance.TransactionType = VoteBalanceTransactionTypes.CutProvision;
                                                voteBalance.Code = "CUTPROVISION";
                                                voteBalance.SubCode= cutProvision.CPNo;
                                                voteBalance.RunningBalance = (decimal)voteBalance.AllocationAmount! - voteBalance.CreditDebitRunningBalance;
                                            }

                                        }

                                        if (classification == 3)
                                        {


                                        }

                                        if (classification == 4)
                                        {


                                        }

                                        //await _unitOfWork.VoteLedgerBook.AddAsync(vtb);
                                        var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalance);
                                        await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);

                                    }
                                    else
                                    {
                                        throw new FinalAccountException("Unable to Find Vote Balance Or Classification");
                                    }



                                    await _unitOfWork.CommitAsync();
                                }
                                else if (request.State == 0)
                                {
                                    cutProvision.ActionState = VoteTransferActions.Reject;
                                    await _unitOfWork.CommitAsync();
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
                                throw new FinalAccountException("Unable To Find Cut Province");
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
            else
            {
                return (false,"Not CutProvince");
            }
        }

        public async Task<bool> WithdrawCutProvision(int cutProvisionId, HTokenClaim token)
        {
            try
            {
                var cutProvision = await _unitOfWork.CutProvision.GetByIdAsync(cutProvisionId);

                var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                if (session != null)
                {


                    if (cutProvision != null && cutProvision.ActionState == VoteTransferActions.Init)
                    {

                        cutProvision.ActionState = VoteTransferActions.withdraw;
                        cutProvision.ActionDate = session.StartAt;
                        cutProvision.SystemActionDate = DateTime.Now;

                        await _unitOfWork.CommitAsync();

                    }
                    else
                    {
                        throw new Exception("Unable To Find Cut Provision");
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
