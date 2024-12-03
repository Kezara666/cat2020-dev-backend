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

namespace CAT20.Services.FinalAccount
{
    public class SupplementaryService : ISupplementaryService
    {
        private readonly IVoteUnitOfWork _unitOfWork;
        private readonly IUserDetailService _userDetailService;
        private readonly IMapper _mapper;
        private readonly IPartnerService _partnerService;
        private readonly IMixinOrderService _mixinOrderService;

        public SupplementaryService(IVoteUnitOfWork unitOfWork, IUserDetailService userDetailServiceService, IMapper mapper, IPartnerService partnerService, IMixinOrderService mixinOrderService)
        {
            _unitOfWork = unitOfWork;
            _userDetailService = userDetailServiceService;
            _mapper = mapper;
            _partnerService = partnerService;
            _mixinOrderService = mixinOrderService;
        }

        public async Task<bool> Create(Supplementary newSupplementary, HTokenClaim token)
        {
            try
            {
                var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                if (session != null)
                {

                    var docSeqNums = await _unitOfWork.FinalAccountSequenceNumber.GetNextSequenceNumberForYearSabhaModuleType(session.CreatedAt.Year, token.sabhaId, FinalAccountModuleType.Supplementary);

                    if (docSeqNums != null)
                    {
                        newSupplementary.SPLNo = $"{token.sabhaId}/{docSeqNums.Year}/{docSeqNums.Prefix}/{++docSeqNums.LastIndex}";


                        //newJournalAdjustment.JournalNo = "ABC";
                        newSupplementary.SabahId = token.sabhaId;
                        newSupplementary.OfficeId = token.officeId;
                        newSupplementary.RequestDate = session.StartAt;
                        newSupplementary.RequestBy = token.userId;
                        newSupplementary.SystemRequestDate = DateTime.Now;
                        newSupplementary.ActionState = VoteTransferActions.Init;


                        var voteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(newSupplementary.VoteDetailId, token.sabhaId, session.StartAt.Year);

                        var classification = await _unitOfWork.VoteDetails.GetVoteClassification(newSupplementary.VoteDetailId);

                        if (voteBalance != null && classification != null && voteBalance.Id == newSupplementary.VoteBalanceId)
                        {


                            if (classification == 1)
                            {



                            }

                            if (classification == 2)
                            {
                                if (voteBalance.TransferFlag == VoteTransferFlag.From)
                                {
                                    throw new FieldAccessException("Vote Balance Not Allowed For Transfer");
                                }
                                else
                                {

                                    newSupplementary.RequestSnapshotAllocation = voteBalance.AllocationAmount;
                                    newSupplementary.RequestSnapshotBalance = (voteBalance.AllocationAmount - voteBalance.Debit + voteBalance.Credit);

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
                            throw new Exception("Unable to Find Vote Balance Or Classification");
                        }

                        await _unitOfWork.Supplementary.AddAsync(newSupplementary);
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

            public async Task<(int totalCount, IEnumerable<SupplementaryResource> list)> GetAllSupplementaryForSabha(int sabhaId, int pageNo, int pageSize, string? filterKeyWord)
        {
            var supplementary = await _unitOfWork.Supplementary.GetAllSupplementaryForSabha(sabhaId, pageNo, pageSize, filterKeyWord);

            var supplementaryResources = _mapper.Map<IEnumerable<Supplementary>, IEnumerable<SupplementaryResource>>(supplementary.list);

            foreach (var s in supplementaryResources)
            {
                if (s.RequestBy.HasValue)
                {
                    s.UserRequestBy = _mapper.Map<UserDetail, FinalUserActionByResources>(await _userDetailService.GetUserDetailById(s.RequestBy!.Value));
                }

                if (s.ActionBy.HasValue)
                {
                    s.UserActionBy = _mapper.Map<UserDetail, FinalUserActionByResources>(await _userDetailService.GetUserDetailById(s.ActionBy!.Value));
                }

              
                    s.VoteDetail = _mapper.Map<VoteDetail, VoteDetailLimitedresource>(await _unitOfWork.VoteDetails.GetByIdAsync(s.VoteDetailId));
            }

            return (supplementary.totalCount, supplementaryResources);
        }

        public async Task<SupplementaryResource> GetSupplementaryId(int Id, HTokenClaim token)
        {
            var s = await _unitOfWork.Supplementary.GetByIdAsync(Id);



            var cutProvisionResources = _mapper.Map<Supplementary, SupplementaryResource>(s);
            if (s != null)
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

        public async Task<(int totalCount, IEnumerable<SupplementaryResource> list)> GetSupplementaryForApproval(int sabhaId, int pageNo, int pageSize, string? filterKeyWord)
        {
            var supplementary = await _unitOfWork.Supplementary.GetSupplementaryForApproval(sabhaId, pageNo, pageSize, filterKeyWord);

            var supplementaryResources = _mapper.Map<IEnumerable<Supplementary>, IEnumerable<SupplementaryResource>>(supplementary.list);

            foreach (var s in supplementaryResources)
            {
                if (s.RequestBy.HasValue)
                {
                    s.UserRequestBy = _mapper.Map<UserDetail, FinalUserActionByResources>(await _userDetailService.GetUserDetailById(s.RequestBy!.Value));
                }

                if (s.ActionBy.HasValue)
                {
                    s.UserActionBy = _mapper.Map<UserDetail, FinalUserActionByResources>(await _userDetailService.GetUserDetailById(s.ActionBy!.Value));
                }


                s.VoteDetail = _mapper.Map<VoteDetail, VoteDetailLimitedresource>(await _unitOfWork.VoteDetails.GetByIdAsync(s.VoteDetailId));
            }

            return (supplementary.totalCount, supplementaryResources);
        }

        public async Task<(bool,string?)> MakeApproveReject(MakeVoteTransferApproveRejectResource request, HTokenClaim token)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var supplementary = await _unitOfWork.Supplementary.GetByIdAsync(request.Id);

                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                    if (session != null)
                    {


                        if (supplementary != null)
                        {
                            supplementary.ActionDate = session.StartAt;
                            supplementary.ActionBy = token.userId;
                            supplementary.ActionNote = request.ActionNote;
                            supplementary.SystemActionDate = DateTime.Now;

                            if (request.State == 1)
                            {
                                supplementary.ActionState  = VoteTransferActions.Approve;

                                var voteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(supplementary.VoteDetailId, token.sabhaId, session.StartAt.Year);

                                    var classification = await _unitOfWork.VoteDetails.GetVoteClassification(supplementary.VoteDetailId);

                                    if (voteBalance != null && classification != null && voteBalance.Id == supplementary.VoteBalanceId)
                                    {


                                        if (classification == 1)
                                        {



                                        }

                                    if (classification == 2)
                                    {
                                        if (voteBalance.TransferFlag == VoteTransferFlag.From)
                                        {
                                            throw new FieldAccessException("Vote Balance Not Allowed For Transfer");
                                        }
                                        else
                                        {



                                        supplementary.ActionSnapshotAllocation = voteBalance.AllocationAmount;
                                        supplementary.ActionSnapshotBalance = (voteBalance.AllocationAmount - voteBalance.Debit + voteBalance.Credit);

                                        voteBalance.AllocationAmount += supplementary.Amount;
                                        voteBalance.UpdatedBy = token.userId;
                                        voteBalance.UpdatedAt = session.StartAt;
                                        voteBalance.OfficeId = token.officeId;
                                        voteBalance.SystemActionAt = DateTime.Now;
                                        voteBalance.TransactionType = VoteBalanceTransactionTypes.Supplementary;
                                        voteBalance.AllocationExchangeAmount= supplementary.Amount;
                                        voteBalance.RunningBalance = ((decimal)voteBalance.AllocationAmount! - voteBalance.Debit + voteBalance.Credit);
                                        voteBalance.TransferFlag = VoteTransferFlag.To;
                                        voteBalance.Code = "SUPPLEMENTARY";
                                        voteBalance.SubCode= supplementary.SPLNo;


                                            if (voteBalance.TakeHoldRate > 0)
                                            {
                                                var holdAmount = (voteBalance.AllocationAmount * voteBalance.TakeHoldRate) / 100;
                                                voteBalance.TakeHoldAmount =  Math.Round((decimal)holdAmount, 2, MidpointRounding.AwayFromZero);

                                            }


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
                                        throw new Exception("Unable to Find Vote Balance Or Classification");
                                    }

                                

                                await _unitOfWork.CommitAsync();
                            }
                            else if (request.State == 0)
                            {
                                supplementary.ActionState = VoteTransferActions.Reject;
                                await _unitOfWork.CommitAsync();
                                //transaction.Commit();
                            }
                            else
                            {
                                throw new Exception("Unable to Perform Action");
                            }

                            transaction.Commit();
                            return (true,"Saved Successfully");

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

        public async Task<bool> WithdrawSupplementary(int supplementaryId, HTokenClaim token)
        {
            try
            {
                var cutProvision = await _unitOfWork.Supplementary.GetByIdAsync(supplementaryId);

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
