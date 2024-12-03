using CAT20.Core;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Services.FinalAccount;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.Mixin;
using CAT20.Core.Services.Control;
using CAT20.Core.Services.User;
using CAT20.WebApi.Resources.Final;
using CAT20.Core.Models.Control;
using Microsoft.AspNetCore.Mvc;
using CAT20.Core.HelperModels;
using CAT20.Core.DTO.OtherModule;
using DocumentFormat.OpenXml.Spreadsheet;
using CAT20.Core.Models.FinalAccount.logs;
using System.ComponentModel.DataAnnotations;
using static PdfSharp.Capabilities;
using CAT20.Core.Models.User;
using CAT20.Services.User;
using Irony.Parsing;
using CAT20.Core.Models.Vote;
using CAT20.Core.CustomExceptions;
using CAT20.Core.DTO.Final.Save;

namespace CAT20.Services.FinalAccount
{
    public class CommitmentService : ICommitmentService
    {
        private readonly IVoteUnitOfWork _unitOfWork;
        private readonly IUserDetailService _userDetailService;
        private readonly IMapper _mapper;
        private readonly IPartnerService _partnerService;


        public CommitmentService(IVoteUnitOfWork unitOfWork, IMapper mapper, IUserDetailService userDetailService,
            IPartnerService partnerService)
        {
            _unitOfWork = unitOfWork;
            _userDetailService = userDetailService;
            _mapper = mapper;
            _partnerService = partnerService;
        }

        public async Task<(bool, string)> CreateCommitment(Commitment newCommitment, HTokenClaim token)
        {


            //using (var transaction = _unitOfWork.BeginTransaction())
            //{
            //    try
            //    {
            //        // First commit
            //        await _unitOfWork.CommitAsync();

            //        // Second commit
            //        await CreateLog(newCommitment);

            //        // If both commits succeed, commit the transaction
            //        transaction.Commit();
            //    }
            //    catch (Exception ex)
            //    {
            //        // If any commit fails, rollback the transaction

            //        // Handle the exception or rethrow it
            //        throw;
            //    }
            //}


            using (var transaction = _unitOfWork.BeginTransaction())
            {


                try
                {

                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                    if (session != null)
                    {
                        var docSeqNums = await _unitOfWork.FinalAccountSequenceNumber.GetNextSequenceNumberForYearSabhaModuleType(session.CreatedAt.Year, token.sabhaId, FinalAccountModuleType.Commitment);

                        if (docSeqNums == null)
                        {
                            throw new FinalAccountException("Unable To Get doc Number");
                        }


                        newCommitment.ActionState = FinalAccountActionStates.Init;
                        newCommitment.CreatedAt = session.StartAt;
                        newCommitment.UpdatedAt = session.StartAt;
                        newCommitment.CreatedBy = token.userId;
                        newCommitment.UpdatedBy = token.userId;

                        newCommitment.SabhaId = token.sabhaId;
                        newCommitment.Year = session.CreatedAt.Year;
                        newCommitment.Month = session.CreatedAt.Month;

                        newCommitment.SystemCreateAt = DateTime.Now;
                        newCommitment.SystemUpdateAt = DateTime.Now;
                        newCommitment.SessionId = session.Id;
                        newCommitment.CommitmentSequenceNumber = $"{newCommitment.SabhaId}/{docSeqNums.Year}/{docSeqNums.Prefix}/{++docSeqNums.LastIndex}";

                        await _unitOfWork.Commitment
                            .AddAsync(newCommitment);

                        var flag = false;

                        foreach (var commitmentLine in newCommitment.CommitmentLine!)
                        {
                            foreach (var commitmentLineVotes in commitmentLine.CommitmentLineVotes)
                            {
                                var withVoteAllocationByIdAsync = await _unitOfWork.VoteBalances.GetWithVoteAllocationByIdAsync(commitmentLineVotes.VoteAllocationId!.Value!);


                                if (withVoteAllocationByIdAsync != null)
                                {

                                    if ((withVoteAllocationByIdAsync.AllocationAmount!.Value + withVoteAllocationByIdAsync.Credit - (withVoteAllocationByIdAsync.Debit + withVoteAllocationByIdAsync.TotalHold + withVoteAllocationByIdAsync.TotalPending+withVoteAllocationByIdAsync.TakeHoldAmount + commitmentLineVotes.Amount)) >= 0)
                                    {
                                        withVoteAllocationByIdAsync.TotalPending += commitmentLineVotes.Amount;
                                        withVoteAllocationByIdAsync.TransactionType = VoteBalanceTransactionTypes.CreateCommitment;
                                        var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(withVoteAllocationByIdAsync);
                                        await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);


                                    }
                                    else
                                    {
                                        throw new FinalAccountException("Insufficient Vote Balance");
                                    }
                                }
                                else
                                {
                                    throw new FinalAccountException("Unable To Get Vote Allocation");
                                }


                            }
                        }




                        if (flag == true)
                        {
                            throw new Exception("Unable To Update Allocation");
                        }

                        await _unitOfWork.CommitAsync();

                        if (await CreateLog(newCommitment, token.userId, newCommitment.Description!))
                        {
                            transaction.Commit();
                            return (true, "Commitment Created Successfully");

                        }
                        else
                        {
                            throw new Exception("Unable to Create Log");
                        }

                    }
                    else
                    {
                        throw new FinalAccountException("There Is no Active Session");
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
        public async Task<bool> UpdateCommitment(Commitment newCommitment, int pId, HTokenClaim token)
        {

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                    if (session != null)
                    {

                        var commitmentById = await _unitOfWork.Commitment.GetCommitmentById(pId);
                        commitmentById.ActionState = FinalAccountActionStates.Edited;

                        foreach (var commitmentLine in commitmentById.CommitmentLine!)
                        {
                            foreach (var commitmentLineVotes in commitmentLine.CommitmentLineVotes)
                            {
                                var withVoteAllocationByIdAsync =
                                    await _unitOfWork.VoteBalances
                                        .GetWithVoteAllocationByIdAsync(commitmentLineVotes.VoteAllocationId.Value);
                                withVoteAllocationByIdAsync.TotalPending -= commitmentLineVotes.Amount;

                            }
                        }
                        var flag = false;

                        foreach (var (_commitmentLine, lineIndex) in newCommitment.CommitmentLine!.Select((value, index) => (value, index)))
                        {
                            if (newCommitment.CommitmentLine!.Count >= commitmentById.CommitmentLine.Count)
                            {
                                if (lineIndex < commitmentById.CommitmentLine.Count())
                                {
                                    foreach (var (_commitmentLineVotes, index) in _commitmentLine.CommitmentLineVotes!.Select((value, index) => (value, index)))
                                    {


                                        var withVoteAllocationByIdAsync = await _unitOfWork.VoteBalances.GetWithVoteAllocationByIdAsync(_commitmentLineVotes.VoteAllocationId!.Value!);

                                        if (withVoteAllocationByIdAsync != null)
                                        {

                                            if ((withVoteAllocationByIdAsync.AllocationAmount!.Value - withVoteAllocationByIdAsync.Credit - withVoteAllocationByIdAsync.TotalHold - withVoteAllocationByIdAsync.TotalPending) > 0)
                                            {
                                                withVoteAllocationByIdAsync.TotalPending += _commitmentLineVotes.Amount;
                                            }
                                            else
                                            {
                                                throw (new Exception("Unable Allocate Amount to commitment"));
                                            }
                                        }
                                        else
                                        {
                                            throw new Exception("Unable To Get Vote Allocation");
                                        }

                                        if (_commitmentLine.CommitmentLineVotes!.Count >= commitmentById.CommitmentLine[lineIndex].CommitmentLineVotes!.Count())
                                        {
                                            if (index < commitmentById.CommitmentLine[lineIndex].CommitmentLineVotes!.Count())
                                            {
                                                commitmentById.CommitmentLine[lineIndex].CommitmentLineVotes[index].VoteId = _commitmentLineVotes.VoteId;
                                                commitmentById.CommitmentLine[lineIndex].CommitmentLineVotes[index].VoteCode = _commitmentLineVotes.VoteCode;
                                                commitmentById.CommitmentLine[lineIndex].CommitmentLineVotes[index].VoteAllocationId = _commitmentLineVotes.VoteAllocationId;
                                                commitmentById.CommitmentLine[lineIndex].CommitmentLineVotes[index].Amount = _commitmentLineVotes.Amount;
                                                commitmentById.CommitmentLine[lineIndex].CommitmentLineVotes[index].RemainingAmount = _commitmentLineVotes.RemainingAmount;
                                                commitmentById.CommitmentLine[lineIndex].CommitmentLineVotes[index].PaymentStatus = _commitmentLineVotes.PaymentStatus;

                                            }
                                            else
                                            {
                                                CommitmentLineVotes clv = new CommitmentLineVotes
                                                {
                                                    CommitmentLineId = (int)commitmentById.CommitmentLine[lineIndex]!.Id!,
                                                    VoteId = _commitmentLineVotes.VoteId,
                                                    VoteCode = _commitmentLineVotes.VoteCode,
                                                    VoteAllocationId = _commitmentLineVotes.VoteAllocationId,
                                                    Amount = _commitmentLineVotes.Amount,
                                                    RemainingAmount = _commitmentLineVotes.RemainingAmount,
                                                    PaymentStatus = _commitmentLineVotes.PaymentStatus,
                                                };

                                                await _unitOfWork.CommitmentLineVote.AddAsync(clv);
                                            }
                                        }
                                        else
                                        {

                                            if (index < commitmentById.CommitmentLine[lineIndex].CommitmentLineVotes!.Count())
                                            {
                                                commitmentById.CommitmentLine[lineIndex].CommitmentLineVotes[index].VoteId = _commitmentLineVotes.VoteId;
                                                commitmentById.CommitmentLine[lineIndex].CommitmentLineVotes[index].VoteCode = _commitmentLineVotes.VoteCode;
                                                commitmentById.CommitmentLine[lineIndex].CommitmentLineVotes[index].VoteAllocationId = _commitmentLineVotes.VoteAllocationId;
                                                commitmentById.CommitmentLine[lineIndex].CommitmentLineVotes[index].Amount = _commitmentLineVotes.Amount;
                                                commitmentById.CommitmentLine[lineIndex].CommitmentLineVotes[index].RemainingAmount = _commitmentLineVotes.RemainingAmount;
                                                commitmentById.CommitmentLine[lineIndex].CommitmentLineVotes[index].PaymentStatus = _commitmentLineVotes.PaymentStatus;

                                            }
                                            else
                                            {
                                                commitmentById.CommitmentLine[lineIndex].CommitmentLineVotes[index].RowStatus = 0;
                                            }

                                        }

                                    }
                                }
                                else
                                {

                                    commitmentById.CommitmentLine.Add(_commitmentLine);

                                }




                            }
                            else
                            {
                                if (lineIndex < commitmentById.CommitmentLine.Count())
                                {
                                    foreach (var (_commitmentLineVotes, index) in _commitmentLine.CommitmentLineVotes!.Select((value, index) => (value, index)))
                                    {


                                        var withVoteAllocationByIdAsync = await _unitOfWork.VoteBalances.GetWithVoteAllocationByIdAsync(_commitmentLineVotes.VoteAllocationId!.Value!);

                                        if (withVoteAllocationByIdAsync != null)
                                        {

                                            if ((withVoteAllocationByIdAsync.AllocationAmount!.Value - withVoteAllocationByIdAsync.Credit - withVoteAllocationByIdAsync.TotalHold - withVoteAllocationByIdAsync.TotalPending) > 0)
                                            {
                                                withVoteAllocationByIdAsync.TotalPending += _commitmentLineVotes.Amount;
                                            }
                                            else
                                            {
                                                throw (new Exception("Unable Allocate Amount to commitment"));
                                            }
                                        }
                                        else
                                        {
                                            throw new Exception("Unable To Get Vote Allocation");
                                        }




                                        if (_commitmentLine.CommitmentLineVotes!.Count >= commitmentById.CommitmentLine[lineIndex].CommitmentLineVotes!.Count())
                                        {
                                            if (index < commitmentById.CommitmentLine[lineIndex].CommitmentLineVotes!.Count())
                                            {
                                                commitmentById.CommitmentLine[lineIndex].CommitmentLineVotes[index].VoteId = _commitmentLineVotes.VoteId;
                                                commitmentById.CommitmentLine[lineIndex].CommitmentLineVotes[index].VoteCode = _commitmentLineVotes.VoteCode;
                                                commitmentById.CommitmentLine[lineIndex].CommitmentLineVotes[index].VoteAllocationId = _commitmentLineVotes.VoteAllocationId;
                                                commitmentById.CommitmentLine[lineIndex].CommitmentLineVotes[index].Amount = _commitmentLineVotes.Amount;
                                                commitmentById.CommitmentLine[lineIndex].CommitmentLineVotes[index].RemainingAmount = _commitmentLineVotes.RemainingAmount;
                                                commitmentById.CommitmentLine[lineIndex].CommitmentLineVotes[index].PaymentStatus = _commitmentLineVotes.PaymentStatus;

                                            }
                                            else
                                            {
                                                CommitmentLineVotes clv = new CommitmentLineVotes
                                                {
                                                    CommitmentLineId = (int)commitmentById.CommitmentLine[lineIndex]!.Id!,
                                                    VoteId = _commitmentLineVotes.VoteId,
                                                    VoteCode = _commitmentLineVotes.VoteCode,
                                                    VoteAllocationId = _commitmentLineVotes.VoteAllocationId,
                                                    Amount = _commitmentLineVotes.Amount,
                                                    RemainingAmount = _commitmentLineVotes.RemainingAmount,
                                                    PaymentStatus = _commitmentLineVotes.PaymentStatus,
                                                };

                                                await _unitOfWork.CommitmentLineVote.AddAsync(clv);
                                            }
                                        }
                                        else
                                        {

                                            if (index < commitmentById.CommitmentLine[lineIndex].CommitmentLineVotes!.Count())
                                            {
                                                commitmentById.CommitmentLine[lineIndex].CommitmentLineVotes[index].VoteId = _commitmentLineVotes.VoteId;
                                                commitmentById.CommitmentLine[lineIndex].CommitmentLineVotes[index].VoteCode = _commitmentLineVotes.VoteCode;
                                                commitmentById.CommitmentLine[lineIndex].CommitmentLineVotes[index].VoteAllocationId = _commitmentLineVotes.VoteAllocationId;
                                                commitmentById.CommitmentLine[lineIndex].CommitmentLineVotes[index].Amount = _commitmentLineVotes.Amount;
                                                commitmentById.CommitmentLine[lineIndex].CommitmentLineVotes[index].RemainingAmount = _commitmentLineVotes.RemainingAmount;
                                                commitmentById.CommitmentLine[lineIndex].CommitmentLineVotes[index].PaymentStatus = _commitmentLineVotes.PaymentStatus;

                                            }
                                            else
                                            {
                                                commitmentById.CommitmentLine[lineIndex].CommitmentLineVotes[index].RowStatus = 0;
                                            }

                                        }

                                    }
                                }
                                else
                                {

                                    commitmentById.CommitmentLine[lineIndex].RowStatus = 0;

                                    foreach (var lineVote in commitmentById.CommitmentLine[lineIndex].CommitmentLineVotes!)
                                    {
                                        lineVote.RowStatus = 0;
                                    }


                                }
                            }


                        }

                        commitmentById.TotalAmount = newCommitment.TotalAmount;
                        commitmentById.Description = newCommitment.Description;
                        commitmentById.ActionState = FinalAccountActionStates.Init;
                        commitmentById.UpdatedBy = token.userId;
                        commitmentById.UpdatedAt = session.StartAt;


                        if (flag == true)
                        {
                            throw new Exception("Unable To Update Allocation");
                        }

                        await _unitOfWork.CommitAsync();


                        //if (await CreateLog(newCommitment, token.userId, null))
                        //{
                        transaction.Commit();
                        return true;
                        //}
                        //else
                        //{
                        //    throw new Exception("Unable to Create Log");
                        //}
                    }
                    else
                    {
                        throw new Exception("There Is no Active Session");
                    }

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return false;
                }


            }



        }


        public async Task<(int totalCount, IEnumerable<CommitmentResource> list)> getCommitmentForApproval(int sabhaId, int stage, int pageNo,
            int pageSize, string? filterKeyword)
        {
            try
            {

                var commitments = await _unitOfWork.Commitment.getCommitmentForApproval(sabhaId, stage, pageNo, pageSize, filterKeyword);
                var commitmentsResources = _mapper.Map<IEnumerable<Commitment>, IEnumerable<CommitmentResource>>(commitments.list);

                if (commitmentsResources != null)
                {
                    foreach (var commitment in commitmentsResources)
                    {
                        commitment.UserActionBy = _mapper.Map<UserDetail, FinalUserActionByResources>(await _userDetailService.GetUserDetailById(commitment.CreatedBy!.Value));
                    }
                }

                return (commitments.totalCount, commitmentsResources!);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public async Task<(int totalCount, IEnumerable<CommitmentResource> list)> getCommitmentForVoucher(int sabhaId, int stage, int pageNo,
            int pageSize, string? filterKeyword)
        {
            try
            {



                var commitments = await _unitOfWork.Commitment.getCommitmentForVoucher(sabhaId, stage, pageNo, pageSize, filterKeyword);
                var commitmentsResources = _mapper.Map<IEnumerable<Commitment>, IEnumerable<CommitmentResource>>(commitments.list);

                if (commitmentsResources != null)
                {
                    foreach (var commitment in commitmentsResources)
                    {
                        commitment.UserActionBy = _mapper.Map<UserDetail, FinalUserActionByResources>(await _userDetailService.GetUserDetailById(commitment.CreatedBy!.Value));
                    }
                }

                return (commitments.totalCount, commitmentsResources!);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<(int totalCount, IEnumerable<CommitmentResource> list)> getCreatedCommitment(int sabhaId, int stage, int userId, int pageNo,
            int pageSize, string? filterKeyword)
        {
            try
            {

                var commitments = await _unitOfWork.Commitment.getCreatedCommitment(sabhaId, stage, userId, pageNo, pageSize, filterKeyword);
                var commitmentsResouces = _mapper.Map<IEnumerable<Commitment>, IEnumerable<CommitmentResource>>(commitments.list);

                if (commitmentsResouces != null)
                {
                    foreach (var commitment in commitmentsResouces)
                    {
                        commitment.UserActionBy = _mapper.Map<UserDetail, FinalUserActionByResources>(await _userDetailService.GetUserDetailById(commitment.CreatedBy!.Value));
                    }
                }


                return (commitments.totalCount, commitmentsResouces!);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        public async Task<CommitmentResource> GetCommitmentById(string id)
        {
            try
            {
                var commitmentById = await _unitOfWork.Commitment.GetCommitmentById(id);
                var byId = await _partnerService.GetById(commitmentById.PayeeId);
                var commitmentResource = _mapper.Map<Commitment, CommitmentResource>(commitmentById);
                commitmentResource.VendorAccount = _mapper.Map<Partner, VendorResource>(await _partnerService.GetById(commitmentById.PayeeId));

                foreach (var commitmentLine in commitmentResource.CommitmentLine)
                {
                    foreach (var commitmentLineCommitmentLineVote in commitmentLine.CommitmentLineVotes)
                    {
                        var withVoteAllocationByIdAsync = await _unitOfWork.VoteBalances.GetWithVoteAllocationByVoteDetailIdAsync(commitmentLineCommitmentLineVote.VoteId);
                        commitmentLineCommitmentLineVote.VoteAllocation = withVoteAllocationByIdAsync;

                    }
                }


                return commitmentResource;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<CommitmentResource> GetCommitmentById(int id)
        {
            try
            {
                var commitmentById = await _unitOfWork.Commitment.GetCommitmentById(id);
                if (commitmentById != null)
                {
                    var commitmentResource = _mapper.Map<Commitment, CommitmentResource>(commitmentById);
                    var byId =

                    commitmentResource.VendorAccount = _mapper.Map<Partner, VendorResource>(await _partnerService.GetById(commitmentById.PayeeId));


                    foreach (var commitmentLine in commitmentResource.CommitmentLine!)
                    {
                        foreach (var commitmentLineCommitmentLineVote in commitmentLine.CommitmentLineVotes!)
                        {
                            var withVoteAllocationByIdAsync = await _unitOfWork.VoteBalances.GetWithVoteAllocationByVoteDetailIdAsync(commitmentLineCommitmentLineVote.VoteId);
                            commitmentLineCommitmentLineVote.VoteAllocation = withVoteAllocationByIdAsync;

                        }
                    }

                    return commitmentResource;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }




        public async Task<bool> MakeCommitmentApprovalReject(MakeApprovalRejectResource request, HTokenClaim token)
        {

            using (var transaction = _unitOfWork.BeginTransaction())
            {

                try
                {

                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);

                    if (session != null)
                    {


                        FinalAccountActionStates state = request.State == 1 ? FinalAccountActionStates.CCApproved : FinalAccountActionStates.CCRejected;

                        var commitmentById = await _unitOfWork.Commitment.GetCommitmentById(request.CommitmentId);

                        if (commitmentById != null)
                        {
                            commitmentById.UpdatedBy = token.userId;
                            commitmentById.UpdatedAt = session.StartAt;

                            if (state == FinalAccountActionStates.CCApproved)
                            {

                                commitmentById.ActionState = FinalAccountActionStates.CCApproved;
                                foreach (var commitmentLine in commitmentById.CommitmentLine)
                                {
                                    foreach (var commitmentLineVotes in commitmentLine.CommitmentLineVotes)
                                    {

                                        var withVoteAllocationByIdAsync = await _unitOfWork.VoteBalances.GetWithVoteAllocationByIdAsync(commitmentLineVotes.VoteAllocationId!.Value!);

                                        if (withVoteAllocationByIdAsync != null)
                                        {

                                            if ((withVoteAllocationByIdAsync.AllocationAmount!.Value + withVoteAllocationByIdAsync.Credit - withVoteAllocationByIdAsync.Debit - withVoteAllocationByIdAsync.TotalHold - withVoteAllocationByIdAsync.TotalPending -withVoteAllocationByIdAsync.TakeHoldAmount) >= 0)
                                            {
                                                withVoteAllocationByIdAsync.TotalPending -= commitmentLineVotes.Amount;
                                                withVoteAllocationByIdAsync.TotalHold += commitmentLineVotes.Amount;

                                                withVoteAllocationByIdAsync.TransactionType = VoteBalanceTransactionTypes.CCAction;
                                                withVoteAllocationByIdAsync.UpdatedBy = token.userId;
                                                withVoteAllocationByIdAsync.UpdatedAt = session.StartAt;
                                                withVoteAllocationByIdAsync.SystemActionAt = DateTime.Now;
                                                withVoteAllocationByIdAsync.OfficeId = token.officeId;

                                                var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(withVoteAllocationByIdAsync);

                                                vtbLog.Id = null;
                                                vtbLog.VoteBalanceId = withVoteAllocationByIdAsync.Id;

                                                await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);


                                            }
                                            else
                                            {
                                                throw (new Exception("Unable Allocate Amount to commitment"));
                                            }
                                        }
                                        else
                                        {
                                            throw new Exception("Unable To Get Vote Allocation");
                                        }

                                    }
                                }


                            }
                            else if (state == FinalAccountActionStates.CCRejected || state == FinalAccountActionStates.Deleted)
                            {
                                commitmentById.ActionState = state;

                                foreach (var commitmentLine in commitmentById.CommitmentLine)
                                {
                                    foreach (var commitmentLineVotes in commitmentLine.CommitmentLineVotes)
                                    {

                                        var withVoteAllocationByIdAsync = await _unitOfWork.VoteBalances.GetWithVoteAllocationByIdAsync(commitmentLineVotes.VoteAllocationId!.Value!);

                                        if (withVoteAllocationByIdAsync != null)
                                        {

                                            if ((withVoteAllocationByIdAsync.AllocationAmount!.Value - withVoteAllocationByIdAsync.Credit - withVoteAllocationByIdAsync.TotalHold - withVoteAllocationByIdAsync.TotalPending) >= 0)
                                            {
                                                withVoteAllocationByIdAsync.TotalPending -= commitmentLineVotes.Amount;
                                                withVoteAllocationByIdAsync.TransactionType = VoteBalanceTransactionTypes.CCAction;
                                                withVoteAllocationByIdAsync.UpdatedBy = token.userId;
                                                withVoteAllocationByIdAsync.UpdatedAt = session.StartAt;
                                                withVoteAllocationByIdAsync.SystemActionAt = DateTime.Now;
                                                withVoteAllocationByIdAsync.OfficeId = token.officeId;
                                                var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(withVoteAllocationByIdAsync);
                                             

                                                await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                                            }
                                            else
                                            {
                                                throw (new Exception("Unable Allocate Amount to commitment"));
                                            }
                                        }
                                        else
                                        {
                                            throw new Exception("Unable To Get Vote Allocation");
                                        }

                                    }
                                }
                            }

                            await _unitOfWork.CommitAsync();

                            if (await CreateLog(commitmentById, token.userId, request.ActionNote))
                            {
                                transaction.Commit();
                                return true;
                            }
                            else
                            {
                                throw new Exception("Unable to Create Log");
                            };
                        }
                        else
                        {
                            throw new Exception("Unable To Found Commitment");
                        }
                    }
                    else
                    {
                        throw new Exception("There Is no Active Session");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }
        }

        private async Task<bool> CreateLog(Commitment commitment, int actionBy, string actionComment)
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

        public async Task<(bool, string?)> WithdrawCommitment(int commitmentId, HTokenClaim token)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {


                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                    if (session != null)
                    {
                        var commitment = await _unitOfWork.Commitment.GetCommitmentById(commitmentId);

                        if (commitment != null && commitment.ActionState == FinalAccountActionStates.Init)
                        {
                            foreach (var commitmentLine in commitment.CommitmentLine!)
                            {
                                foreach (var commitmentLineVotes in commitmentLine.CommitmentLineVotes!)
                                {
                                    //var withVoteAllocationByIdAsync = await _unitOfWork.VoteBalances.GetWithVoteAllocationByIdAsync(commitmentLineVotes.VoteAllocationId!.Value!);

                                    var voteAllocation  =  await _unitOfWork.VoteBalances.GetActiveVoteBalance(commitmentLineVotes.VoteId,token.sabhaId, session.StartAt.Year);


                                    if (voteAllocation != null && voteAllocation.Id == commitmentLineVotes.VoteAllocationId)
                                    {

                                        if ((voteAllocation.AllocationAmount!.Value + voteAllocation.Credit - (voteAllocation.Debit + voteAllocation.TotalHold + voteAllocation.TotalPending + commitmentLineVotes.Amount)) >= 0)
                                        {
                                            voteAllocation.TotalPending -= commitmentLineVotes.Amount;
                                            voteAllocation.TransactionType = VoteBalanceTransactionTypes.WithdrawCommitment;

                                            voteAllocation.UpdatedAt = session.StartAt;
                                            voteAllocation.UpdatedBy = token.userId;
                                            voteAllocation.SystemActionAt = DateTime.Now;

                                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteAllocation);
                                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);



                                        }
                                        else
                                        {
                                            throw new FinalAccountException("Insufficient Vote Balance");
                                        }
                                    }
                                    else
                                    {
                                        throw new FinalAccountException("Unable To Get Vote Allocation");
                                    }


                                }
                            }

                            commitment.ActionState = FinalAccountActionStates.Deleted;
                            await _unitOfWork.CommitAsync();
                            transaction.Commit();
                            return (true, "Commitment Withdrawal Successful");
                        }
                        else
                        {
                            throw new FinalAccountException("Unable To Found Commitment To Withdraw");

                        }

                    }
                    else
                    {
                        throw new FinalAccountException("There Is no Active Session");
                    }
                }
                catch (Exception ex)
                {
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
        
        public async Task<(bool, string?)> ReleaseCommitmentHold(SaveCommitmentHoldReleaseResource holdReleaseResource , HTokenClaim token)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {


                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                    if (session != null)
                    {
                        var commitment = await _unitOfWork.Commitment.GetCommitmentById(holdReleaseResource.CommitmentId);

                        if (commitment != null && commitment.ActionState == FinalAccountActionStates.CCApproved)
                        {
                            foreach (var commitmentLine in commitment.CommitmentLine!)
                            {
                                foreach (var commitmentLineVotes in commitmentLine.CommitmentLineVotes!)
                                {
                                    //var withVoteAllocationByIdAsync = await _unitOfWork.VoteBalances.GetWithVoteAllocationByIdAsync(commitmentLineVotes.VoteAllocationId!.Value!);

                                    var voteAllocation = await _unitOfWork.VoteBalances.GetActiveVoteBalance(commitmentLineVotes.VoteId, token.sabhaId, session.StartAt.Year);

                                    if (voteAllocation != null)
                                    {

                                        if ((voteAllocation.AllocationAmount!.Value + voteAllocation.Credit - (voteAllocation.Debit + voteAllocation.TotalHold + voteAllocation.TotalPending )) >= 0)
                                        {
                                            voteAllocation.TotalHold -= commitmentLineVotes.RemainingAmount;
                                            voteAllocation.TransactionType = VoteBalanceTransactionTypes.ReleaseCommitmentHold;


                                            voteAllocation.UpdatedAt = session.StartAt;
                                            voteAllocation.UpdatedBy = token.userId;
                                            voteAllocation.SystemActionAt = DateTime.Now;
                                            voteAllocation.OfficeId = token.officeId;
                                            voteAllocation.SessionIdByOffice = session.Id;


                                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteAllocation);
                                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);



                                        }
                                        else
                                        {
                                            throw new FinalAccountException("Insufficient Vote Balance");
                                        }
                                    }
                                    else
                                    {
                                        throw new FinalAccountException("Unable To Get Vote Allocation");
                                    }


                                }
                            }

                            commitment.ActionState = FinalAccountActionStates.Settled;
                            await _unitOfWork.CommitAsync();
                            transaction.Commit();
                            return (true, "Commitment Withdrawal Successful");
                        }
                        else
                        {
                            throw new FinalAccountException("Unable To Found Commitment To Release Hold");

                        }

                    }
                    else
                    {
                        throw new FinalAccountException("There Is no Active Session");
                    }
                }
                catch (Exception ex)
                {
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
    }



}