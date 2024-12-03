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
using CAT20.Core.DTO.OtherModule;
using CAT20.Core.DTO.Vote.Save;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.Vote;
using CAT20.Core.Services.Control;
using CAT20.Core.Services.FinalAccount;
using CAT20.Core.Services.Vote;
using CAT20.Services.Control;
using CAT20.Services.Vote;
using DocumentFormat.OpenXml.Bibliography;

namespace CAT20.Services.FinalAccount
{
    public class CreditorsBillingService : ICreditorsBillingService
    {
        private readonly IVoteUnitOfWork _unitOfWork;
        private readonly IVoteBalanceService _voteBalanceService;
        private readonly ICustomVoteBalanceService _customVoteBalanceService;
        private readonly IPartnerService _partnerService;
        private readonly IMapper _mapper;

        public CreditorsBillingService(IVoteUnitOfWork unitOfWork, IMapper mapper, IVoteBalanceService voteBalanceService,ICustomVoteBalanceService customVoteBalanceService, IPartnerService partnerService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _voteBalanceService = voteBalanceService;
            _customVoteBalanceService = customVoteBalanceService;
            _partnerService = partnerService;
        }
        public async Task<(bool, string?)> CreateUpdateCreditor(SaveCreditorBillingResource newCreditorResource, HTokenClaim token)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);

                    var newCreditor = _mapper.Map<SaveCreditorBillingResource, CreditorBilling>(newCreditorResource);

                    //var customeVoteEntries = _mapper.Map<IEnumerable<SaveCustomVoteEntry>, IEnumerable<CustomVoteEntry>>(newCreditorResource.customVoteEntries!.OrderBy(x => x.CustomVoteDetailId));


                    if (session != null)
                    {


                        if (newCreditor.Id != null) //update existing object
                        {
                            var creditor = await _unitOfWork.CreditorsBilling.GetByIdAsync(newCreditor.Id.Value);
                            if (creditor != null)
                            {
                                if (await _voteBalanceService.UpdateVoteBalanceForOpenBalances(creditor.LedgerAccountId!.Value, creditor.Amount, creditor.Year, creditor.Month, creditor.Description, "", FAMainTransactionMethod.Backward, session, token))
                                {

                                    creditor.Status = 0;
                                    creditor.UpdatedAt = session.StartAt;
                                    creditor.CreatedBy = token.userId;
                                    creditor.SystemActionAt = DateTime.Now;


                                    newCreditor.Id = null;
                                    newCreditor.CreatedAt = session.StartAt;
                                    newCreditor.CreatedBy = token.userId;
                                    newCreditor.SabhaId = token.sabhaId;
                                    newCreditor.OfficeId = token.officeId;
                                    newCreditor.SystemActionAt = DateTime.Now;

                                    await _unitOfWork.CreditorsBilling.AddAsync(newCreditor);


                                    await _unitOfWork.CommitAsync();

                                 

                                    await _voteBalanceService.UpdateVoteBalanceForOpenBalances(newCreditor.LedgerAccountId!.Value, newCreditor.Amount, newCreditor.Year, newCreditor.Month, newCreditor.Description, "", FAMainTransactionMethod.Forward, session, token);

                                 
                                    await _customVoteBalanceService.UpdateCustomVoteBalanceForOpenBalances(newCreditor.CustomVoteId!.Value, FAMainTransactionMethod.Forward, VoteBalanceTransactionTypes.Credit, newCreditor.Amount, session, token);
                                    

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
                            newCreditor.Id = null;
                            newCreditor.CreatedAt = session.StartAt;
                            newCreditor.CreatedBy = token.userId;
                            newCreditor.SabhaId = token.sabhaId;
                            newCreditor.OfficeId = token.officeId;
                            newCreditor.SystemActionAt = DateTime.Now;
                            await _unitOfWork.CreditorsBilling.AddAsync(newCreditor);
                            await _voteBalanceService.UpdateVoteBalanceForOpenBalances(newCreditor.LedgerAccountId!.Value, newCreditor.Amount, newCreditor.Year, newCreditor.Month, newCreditor.Description, "", FAMainTransactionMethod.Forward, session, token);
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

        public async Task<CreditorBilling> GetCreditorById(int Id)
        {
            return await _unitOfWork.CreditorsBilling.GetByIdAsync(Id);
        }


        public async Task<(int totalCount, IEnumerable<CreditorBillingResource> list)> GetAllCreditorsForSabha(int sabhaId, Nullable<int> ledgerAccountId, int pageNo, int pageSize, string? filterKeyword, HTokenClaim token)
        {
            var result = await _unitOfWork.CreditorsBilling.GetAllCreditorsForSabha(sabhaId, ledgerAccountId, pageNo, pageSize, filterKeyword);

            var creditorsBillingRescouce = _mapper.Map<IEnumerable<CreditorBilling>, IEnumerable<CreditorBillingResource>>(result.list);

            foreach (var item in creditorsBillingRescouce)
            {
                item.CreditorDebtorInfo = _mapper.Map<Partner, CreditorDebtorResource>(await _partnerService.GetById(item.CreditorId));

                item.LedgerVoteDetail = _mapper.Map<VoteDetail, VoteDetailLimitedresource>(await _unitOfWork.VoteDetails.GetByIdAsync(item.LedgerAccountId!.Value));

            }

              return (result.totalCount, creditorsBillingRescouce);

        }

        public async Task<(bool, string?)> DeleteCreditorBilling(int Id, HTokenClaim token)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                    if (session != null)
                    {


                            var creditor = await _unitOfWork.CreditorsBilling.GetByIdAsync(Id);
                            if (creditor != null)
                            {
                                if (await _voteBalanceService.UpdateVoteBalanceForOpenBalances(creditor.LedgerAccountId!.Value, creditor.Amount, creditor.Year, creditor.Month, creditor.Description, "", FAMainTransactionMethod.Backward, session, token))
                                {

                                    creditor.Status = 0;
                                    creditor.UpdatedAt = session.StartAt;
                                    creditor.CreatedBy = token.userId;
                                    creditor.SystemActionAt = DateTime.Now;



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
