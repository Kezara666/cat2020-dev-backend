using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CAT20.Core;
using CAT20.Core.CustomExceptions;
using CAT20.Core.DTO.Final;
using CAT20.Core.DTO.Final.Save;
using CAT20.Core.DTO.OtherModule;
using CAT20.Core.DTO.Vote.Save;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.Vote;
using CAT20.Core.Services.Control;
using CAT20.Core.Services.FinalAccount;
using CAT20.Core.Services.Vote;
using CAT20.Services.Vote;

namespace CAT20.Services.FinalAccount
{
    public class FixedDepositService : IFixedDepositService
    {
        private readonly IVoteUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IVoteBalanceService _voteBalanceService;
        private readonly ICustomVoteBalanceService _customVoteBalanceService;
        private readonly IBankBranchService _bankBranchService;

        public FixedDepositService(IVoteUnitOfWork unitOfWork, IMapper mapper, IVoteBalanceService voteBalanceService,ICustomVoteBalanceService customVoteBalanceService, IBankBranchService bankBranchService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _voteBalanceService = voteBalanceService;
            _customVoteBalanceService = customVoteBalanceService;
            _bankBranchService = bankBranchService;
        }



        public async Task<(int totalCount, IEnumerable<FixedDepositResource> list)> GetAllFixedDepositForSabha(int sabhaId, int ledgerAccountId, int pageNo, int pageSize, string? filterKeyword, HTokenClaim token)
        {
            var fixedDeposit = await _unitOfWork.FixedDeposits.GetAllFixedDepositForSabha(sabhaId, ledgerAccountId, pageNo, pageSize, filterKeyword);

            var fixedDepositsResource = _mapper.Map<IEnumerable<FixedDeposit>, IEnumerable<FixedDepositResource>>(fixedDeposit.list);

            foreach (var item in fixedDepositsResource)
            {
                item.DepositTypeVoteDetail = _mapper.Map<VoteDetailLimitedresource>(await _unitOfWork.VoteDetails.GetByIdAsync(item.DepositTypeVote!.Value));

                item.BankBranch = _mapper.Map<FinalBankBranchResource>(await _bankBranchService.GetBankBranchWithBankById(item.BankBranchId!.Value));
            }


            return (fixedDeposit.totalCount, fixedDepositsResource);

        }



        public async Task<(bool, string?)> CreateUpdateFixedDeposit(SaveFixedDepositResource newFixedDepositResource, HTokenClaim token)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var newFixedDeposit = _mapper.Map<SaveFixedDepositResource, FixedDeposit>(newFixedDepositResource);
                    //var customeVoteEntries = _mapper.Map<IEnumerable<SaveCustomVoteEntry>, IEnumerable<CustomVoteEntry>>(newFixedDepositResource.customVoteEntries!.OrderBy(x => x.CustomVoteDetailId));


                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                    if (session != null)
                    {


                        if (newFixedDeposit.Id != null) //update existing object
                        {
                            var fixedDeposit = await _unitOfWork.FixedDeposits.GetByIdAsync(newFixedDeposit.Id);
                            if (fixedDeposit != null)
                            {
                                if (await _voteBalanceService.UpdateVoteBalanceForOpenBalances(fixedDeposit.DepositTypeVote!.Value, (decimal)fixedDeposit.FDAmount!, fixedDeposit.DepositDate.Year, fixedDeposit.DepositDate.Month, $"B/F FD REF {fixedDeposit.Reference} ", "", FAMainTransactionMethod.Backward, session, token))
                                {

                                    fixedDeposit.Status = 0;
                                    fixedDeposit.SystemActionAt = DateTime.Now;
                                    fixedDeposit.UpdatedAt = session.StartAt;
                                    fixedDeposit.UpdatedBy = token.userId;


                                    newFixedDeposit.Id = null;
                                    newFixedDeposit.CreatedAt = session.StartAt;
                                    newFixedDeposit.CreatedBy = token.userId;
                                    newFixedDeposit.SabhaId = token.sabhaId;
                                    newFixedDeposit.OfficeId = token.officeId;
                                    newFixedDeposit.SystemActionAt = DateTime.Now;
                                    await _unitOfWork.FixedDeposits.AddAsync(newFixedDeposit);

                                    await _voteBalanceService.UpdateVoteBalanceForOpenBalances(newFixedDeposit.DepositTypeVote!.Value, (decimal)newFixedDeposit.FDAmount!, newFixedDeposit.DepositDate.Year, newFixedDeposit.DepositDate.Month, $"FD REF {newFixedDeposit.Reference} ", "", FAMainTransactionMethod.Forward, session, token);
                                }
                                else
                                {
                                    throw new GeneralException("No Active Session Found");
                                }
                            }
                            else
                            {

                                throw new GeneralException("Unable To Find Entry");
                            }
                        }
                        else
                        {
                            newFixedDeposit.Id = null;
                            newFixedDeposit.CreatedAt = session.StartAt;
                            newFixedDeposit.CreatedBy = token.userId;
                            newFixedDeposit.SabhaId = token.sabhaId;
                            newFixedDeposit.OfficeId = token.officeId;
                            newFixedDeposit.SystemActionAt = DateTime.Now;
                            await _unitOfWork.FixedDeposits.AddAsync(newFixedDeposit);

                            await _unitOfWork.CommitAsync();


                            await _voteBalanceService.UpdateVoteBalanceForOpenBalances(newFixedDeposit.DepositTypeVote!.Value, (decimal)newFixedDeposit.FDAmount!, newFixedDeposit.DepositDate.Year, newFixedDeposit.DepositDate.Month, $"FD REF {newFixedDeposit.Reference} ", "", FAMainTransactionMethod.Forward, session, token);
                            await _customVoteBalanceService.UpdateCustomVoteBalanceForOpenBalances(newFixedDeposit.CustomVoteId!.Value, FAMainTransactionMethod.Forward, VoteBalanceTransactionTypes.Credit, (decimal)newFixedDeposit.FDAmount!, session, token);


                        }


                        await _unitOfWork.CommitAsync();
                        transaction.Commit();

                        return (true, "Successfully Saved");
                    }
                    else
                    {
                        throw new GeneralException("No Active Session Found");
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return (false, ex is FinalAccountException or GeneralException ? ex.Message : null);
                }
            }
        }

        public async Task<(bool, string?)> DeleteFixedDeposit(int fixedDepositId, HTokenClaim token)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {

                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                    if (session != null)
                    {

                        var fixedDeposit = await _unitOfWork.FixedDeposits.GetByIdAsync(fixedDepositId);
                        if (fixedDeposit != null)
                        {
                            if (await _voteBalanceService.UpdateVoteBalanceForOpenBalances(fixedDeposit.DepositTypeVote!.Value, (decimal)fixedDeposit.FDAmount!, fixedDeposit.DepositDate.Year, fixedDeposit.DepositDate.Month, $"B/F FD REF {fixedDeposit.Reference} ", "", FAMainTransactionMethod.Backward, session, token))
                            {

                                fixedDeposit.Status = 0;
                                fixedDeposit.SystemActionAt = DateTime.Now;
                                fixedDeposit.UpdatedAt = session.StartAt;
                                fixedDeposit.UpdatedBy = token.userId;



                                await _unitOfWork.CommitAsync();
                                transaction.Commit();

                                return (true, "Successfully Delete Entry");

                            }
                            else
                            {
                                throw new GeneralException("No Active Session Found");
                            }
                        }
                        else
                        {

                            throw new GeneralException("Unable To Find Entry");
                        }


                    }
                    else
                    {
                        throw new GeneralException("No Active Session Found");
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return (false, ex is FinalAccountException or GeneralException ? ex.Message : null);
                }
            }
        }
    }
}
