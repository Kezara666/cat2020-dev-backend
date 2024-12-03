using AutoMapper;
using CAT20.Core.CustomExceptions;
using CAT20.Core.DTO.Final.Save;
using CAT20.Core.DTO.Final;
using CAT20.Core.DTO;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.Vote;
using CAT20.Core.Services.Control;
using CAT20.Core;
using CAT20.Core.Services.FinalAccount;
using CAT20.Core.Services.Vote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.FinalAccount
{
    public class SingleOpenBalanceService : ISingleOpenBalanceService
    {
        private readonly IVoteUnitOfWork _unitOfWork;
        private readonly IVoteBalanceService _voteBalanceService;
        private readonly ICustomVoteBalanceService _customVoteBalanceService;
        private readonly IPartnerService _partnerService;
        private readonly IMapper _mapper;

        public SingleOpenBalanceService(IVoteUnitOfWork unitOfWork, IMapper mapper, IVoteBalanceService voteBalanceService, ICustomVoteBalanceService customVoteBalanceService, IPartnerService partnerService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _voteBalanceService = voteBalanceService;
            _customVoteBalanceService = customVoteBalanceService;
            _partnerService = partnerService;
        }
        public async Task<(bool, string?)> CreateUpdate(SaveSingleOpenBalanceResource newBalanceResource, HTokenClaim token)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);

                    var newBalance = _mapper.Map<SaveSingleOpenBalanceResource, SingleOpenBalance>(newBalanceResource);



                    if (session != null)
                    {


                        if (newBalance.Id != null) //update existing object
                        {
                            var creditor = await _unitOfWork.CreditorsBilling.GetByIdAsync(newBalance.Id.Value);
                            if (creditor != null)
                            {
                                if (await _voteBalanceService.UpdateVoteBalanceForOpenBalances(creditor.LedgerAccountId!.Value, creditor.Amount, creditor.Year, creditor.Month, creditor.Description, "", FAMainTransactionMethod.Backward, session, token))
                                {

                                    creditor.Status = 0;
                                    creditor.UpdatedAt = session.StartAt;
                                    creditor.CreatedBy = token.userId;
                                    creditor.SystemActionAt = DateTime.Now;


                                    newBalance.Id = null;
                                    newBalance.CreatedAt = session.StartAt;
                                    newBalance.CreatedBy = token.userId;
                                    newBalance.SabhaId = token.sabhaId;
                                    newBalance.OfficeId = token.officeId;
                                    newBalance.SystemActionAt = DateTime.Now;

                                    await _unitOfWork.SingleOpenBalances.AddAsync(newBalance);


                                    await _unitOfWork.CommitAsync();



                                    await _voteBalanceService.UpdateVoteBalanceForOpenBalances(newBalance.LedgerAccountId!.Value, newBalance.Amount, null, null, newBalance.Description, "", FAMainTransactionMethod.Forward, session, token);


                                    await _customVoteBalanceService.UpdateCustomVoteBalanceForOpenBalances(newBalance.CustomVoteId!.Value, FAMainTransactionMethod.Forward, VoteBalanceTransactionTypes.Credit, newBalance.Amount, session, token);


                                }

                                else
                                {
                                    throw new GeneralException("Unable To Update Vote Balance");
                                }



                            }
                            else
                            {
                                throw new GeneralException("Unable To Find Entry");
                            }
                        }
                        else
                        {
                            newBalance.Id = null;
                            newBalance.CreatedAt = session.StartAt;
                            newBalance.CreatedBy = token.userId;
                            newBalance.SabhaId = token.sabhaId;
                            newBalance.OfficeId = token.officeId;
                            newBalance.SystemActionAt = DateTime.Now;
                            await _unitOfWork.SingleOpenBalances.AddAsync(newBalance);
                            await _voteBalanceService.UpdateVoteBalanceForOpenBalances(newBalance.LedgerAccountId!.Value, newBalance.Amount, null, null, newBalance.Description, "", FAMainTransactionMethod.Forward, session, token);
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



        public async Task<(int totalCount, IEnumerable<SingleOpenBalanceResource> list)> GetAllSingleOpenBalancesForSabha(int sabhaId, Nullable<int> ledgerAccountId, int pageNo, int pageSize, string? filterKeyword, HTokenClaim token)
        {
            var result = await _unitOfWork.SingleOpenBalances.GetAllSingleOpenBalancesForSabha(sabhaId, ledgerAccountId, pageNo, pageSize, filterKeyword);

            var creditorsBillingRescouce = _mapper.Map<IEnumerable<SingleOpenBalance>, IEnumerable<SingleOpenBalanceResource>>(result.list);

            foreach (var item in creditorsBillingRescouce)
            {

                item.LedgerVoteDetail = _mapper.Map<VoteDetail, VoteDetailLimitedresource>(await _unitOfWork.VoteDetails.GetByIdAsync(item.LedgerAccountId!.Value));

            }

            return (result.totalCount, creditorsBillingRescouce);

        }

        public async Task<(bool, string?)> DeleteSingleOpenBalances(int Id, HTokenClaim token)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                    if (session != null)
                    {


                        var singleOpenBalance = await _unitOfWork.SingleOpenBalances.GetByIdAsync(Id);
                        if (singleOpenBalance != null)
                        {
                            if (await _voteBalanceService.UpdateVoteBalanceForOpenBalances(singleOpenBalance.LedgerAccountId!.Value, singleOpenBalance.Amount, null, null, singleOpenBalance.Description, "", FAMainTransactionMethod.Backward, session, token))
                            {

                                singleOpenBalance.Status = 0;
                                singleOpenBalance.UpdatedAt = session.StartAt;
                                singleOpenBalance.CreatedBy = token.userId;
                                singleOpenBalance.SystemActionAt = DateTime.Now;



                            }

                            else
                            {
                                throw new GeneralException("Unable To Update Vote Balance");
                            }



                        }
                        else
                        {
                            throw new GeneralException("Unable To Find Entry");
                        }



                        await _unitOfWork.CommitAsync();
                        transaction.Commit();

                        return (true, "Successfully Delete Entry");
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
