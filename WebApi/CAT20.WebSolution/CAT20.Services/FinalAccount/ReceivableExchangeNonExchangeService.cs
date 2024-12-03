using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CAT20.Core;
using CAT20.Core.CustomExceptions;
using CAT20.Core.DTO;
using CAT20.Core.DTO.Final;
using CAT20.Core.DTO.Final.Save;
using CAT20.Core.DTO.Vote.Save;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.Vote;
using CAT20.Core.Services.Control;
using CAT20.Core.Services.FinalAccount;
using CAT20.Core.Services.Vote;
using CAT20.Services.Vote;

namespace CAT20.Services.FinalAccount
{
    public class ReceivableExchangeNonExchangeService : IReceivableExchangeNonExchangeService
    {

        private readonly IVoteUnitOfWork _unitOfWork;
        private readonly IVoteBalanceService _voteBalanceService;
        private readonly ICustomVoteBalanceService _customVoteBalanceService;
        private readonly IPartnerService _partnerService;
        private readonly IMapper _mapper;

        public ReceivableExchangeNonExchangeService(IVoteUnitOfWork unitOfWork, IMapper mapper, IVoteBalanceService voteBalanceService,ICustomVoteBalanceService customVoteBalanceService, IPartnerService partnerService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _voteBalanceService = voteBalanceService;
            _customVoteBalanceService = customVoteBalanceService;
            _partnerService = partnerService;
        }
        public async Task<(int totalCount, IEnumerable<ReceivableExchangeNonExchgangeResource> list)> GetAllReceivableExchangeNonExchangeForSabha(int sabhaId, int ledgerAccountId, int pageNo, int pageSize, string? filterKeyword, HTokenClaim token)
        {
            var result = await _unitOfWork.ReceivableExchangeNonExchanges.GetAllReceivableExchangrNonExchangeeForSabha(sabhaId, ledgerAccountId, pageNo, pageSize, filterKeyword);

            var receivableExchangeNonExchangeRescouce = _mapper.Map<IEnumerable<ReceivableExchangeNonExchange>, IEnumerable<ReceivableExchangeNonExchgangeResource>>(result.list);

            foreach (var item in receivableExchangeNonExchangeRescouce)
            {
                item.CreditorDebtorInfo = _mapper.Map<Partner, CreditorDebtorResource>(await _partnerService.GetById(item.ReceivableFromId));

                item.LedgerVoteDetail = _mapper.Map<VoteDetail, VoteDetailLimitedresource>(await _unitOfWork.VoteDetails.GetByIdAsync(item.LedgerAccountId!.Value));
            }

            return (result.totalCount, receivableExchangeNonExchangeRescouce);

        }


        public async Task<(bool, string?)> CreateUpdateReceivableExchangeNonExchange(SaveReceivableExchangeNonExchangeResource newReceivableExchangeResource, HTokenClaim token)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var newReceivableExchange = _mapper.Map<SaveReceivableExchangeNonExchangeResource, ReceivableExchangeNonExchange>(newReceivableExchangeResource);
                    //var customeVoteEntries = _mapper.Map<IEnumerable<SaveCustomVoteEntry>, IEnumerable<CustomVoteEntry>>(newReceivableExchangeResource.customVoteEntries!.OrderBy(x => x.CustomVoteDetailId));

                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                    if (session != null)
                    {

                        if (newReceivableExchange.Id != null)
                        {
                            var receivableExchange = await _unitOfWork.ReceivableExchangeNonExchanges.GetByIdAsync(newReceivableExchange.Id);
                            if (receivableExchange != null)
                            {
                                if (await _voteBalanceService.UpdateVoteBalanceForOpenBalances(receivableExchange.LedgerAccountId!.Value, receivableExchange.Amount, null, null, "REC EXE NON EXE", "", FAMainTransactionMethod.Backward, session, token))
                                {
                                    receivableExchange.Status = 0;
                                    receivableExchange.UpdatedBy = token.userId;
                                    receivableExchange.UpdatedAt = session.StartAt;
                                    receivableExchange.SystemActionAt = DateTime.Now;

                                    newReceivableExchange.Id = null;
                                    newReceivableExchange.CreatedAt = session.StartAt;
                                    newReceivableExchange.CreatedBy = token.userId;
                                    newReceivableExchange.SabhaId = token.sabhaId;
                                    newReceivableExchange.OfficeId = token.officeId;
                                    newReceivableExchange.SystemActionAt = DateTime.Now;
                                    await _unitOfWork.ReceivableExchangeNonExchanges.AddAsync(newReceivableExchange);

                                    await _voteBalanceService.UpdateVoteBalanceForOpenBalances(newReceivableExchange.LedgerAccountId!.Value, newReceivableExchange.Amount, null, null, "REC EXE NON EXE", "", FAMainTransactionMethod.Forward, session, token);
                                }
                                else
                                {
                                    throw new Exception("Unable To Find");
                                }
                            }
                            else
                            {
                                throw new GeneralException("Unable To Update Entry");


                            }

                        }
                        else //create a new object
                        {
                            newReceivableExchange.Id = null;
                            newReceivableExchange.CreatedAt = session.StartAt;
                            newReceivableExchange.CreatedBy = token.userId;
                            newReceivableExchange.SabhaId = token.sabhaId;
                            newReceivableExchange.OfficeId = token.officeId;
                            newReceivableExchange.SystemActionAt = DateTime.Now;
                            await _unitOfWork.ReceivableExchangeNonExchanges.AddAsync(newReceivableExchange);

                            await _unitOfWork.CommitAsync();

                           

                            await _voteBalanceService.UpdateVoteBalanceForOpenBalances(newReceivableExchange.LedgerAccountId!.Value, newReceivableExchange.Amount, null, null, "REC EXE NON EXE", "", FAMainTransactionMethod.Forward, session, token);
                            await _customVoteBalanceService.UpdateCustomVoteBalanceForOpenBalances(newReceivableExchange.CustomVoteId!.Value, FAMainTransactionMethod.Forward, VoteBalanceTransactionTypes.Credit, newReceivableExchange.Amount, session, token);
                          
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

        public async Task<(bool, string?)> DeleteReceivableExchangeNonExchange(int receivableExchangeNonExchangeId, HTokenClaim token)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {

                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                    if (session != null)
                    {

                        var receivableExchange = await _unitOfWork.ReceivableExchangeNonExchanges.GetByIdAsync(receivableExchangeNonExchangeId);
                        if (receivableExchange != null)
                        {
                            if (await _voteBalanceService.UpdateVoteBalanceForOpenBalances(receivableExchange.LedgerAccountId!.Value, receivableExchange.Amount, null, null, "REC EXE NON EXE", "", FAMainTransactionMethod.Backward, session, token))
                            {

                                receivableExchange.Status = 0;
                                receivableExchange.UpdatedBy = token.userId;
                                receivableExchange.UpdatedAt = session.StartAt;
                                receivableExchange.SystemActionAt = DateTime.Now;


                                await _unitOfWork.CommitAsync();
                                transaction.Commit();

                                return (true, "Successfully Delete Entry");
                            }
                            else
                            {
                                throw new GeneralException("Unable Delete Entry");
                            }
                        }
                        else
                        {
                            throw new GeneralException("Unable To Delete Entry");


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
