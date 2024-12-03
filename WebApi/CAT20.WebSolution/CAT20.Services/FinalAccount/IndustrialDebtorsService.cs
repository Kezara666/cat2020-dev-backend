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

namespace CAT20.Services.FinalAccount
{
    public class IndustrialDebtorsService : IIndustrialDebtorsService
    {
        private readonly IVoteUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IVoteBalanceService _voteBalanceService;
        private readonly ICustomVoteBalanceService _customVoteBalanceService;
        private readonly IPartnerService _partnerService;

        public IndustrialDebtorsService(IVoteUnitOfWork unitOfWork, IMapper mapper, IVoteBalanceService voteBalanceService, ICustomVoteBalanceService customVoteBalanceService,IPartnerService partnerService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _voteBalanceService = voteBalanceService;
            _customVoteBalanceService = customVoteBalanceService;
            _partnerService = partnerService;
        }

        public async Task<(int totalCount, IEnumerable<IndustrialDebtorsResource> list)> GetAllIndustrialDebtorsForSabha(int sabhaId, int ledgerAccountId, int pageNo, int pageSize, string? filterKeyword, HTokenClaim token)
        {
            var InduastrialDebtors = await _unitOfWork.IndustrialDebtors.GetAllIndustrialDebtorsForSabha(sabhaId, ledgerAccountId, pageNo, pageSize, filterKeyword);

            var InduastrialDebtorsResource = _mapper.Map<IEnumerable<IndustrialDebtors>, IEnumerable<IndustrialDebtorsResource>>(InduastrialDebtors.list);

            foreach (var item in InduastrialDebtorsResource)
            {
                item.CreditorDebtorInfo = _mapper.Map<Partner, CreditorDebtorResource>(await _partnerService.GetById(item.DebtorId));

                item.CategoryVoteDetail = _mapper.Map<VoteDetailLimitedresource>(await _unitOfWork.VoteDetails.GetByIdAsync(item.CategoryVote!.Value));
                item.FundSource = _mapper.Map<VoteDetailLimitedresource>(await _unitOfWork.VoteDetails.GetByIdAsync(item.FundSourceId!.Value));

            }


            return (InduastrialDebtorsResource.Count(), InduastrialDebtorsResource);

        }

        public async Task<(bool, string?)> CreateUpdateIndustrialDebtors(SaveIndustrialDebtorsResource newIndustrialDebtorResource, HTokenClaim token)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                    var newIndustrialDebtor = _mapper.Map<SaveIndustrialDebtorsResource, IndustrialDebtors>(newIndustrialDebtorResource);
                    //var customeVoteEntries = _mapper.Map<IEnumerable<SaveCustomVoteEntry>, IEnumerable<CustomVoteEntry>>(newIndustrialDebtorResource.customVoteEntries!.OrderBy(x => x.CustomVoteDetailId));

                    if (session != null)
                    {
                        if (newIndustrialDebtor.Id != null) //update existing object
                        {
                            var industrialDebtor = await _unitOfWork.IndustrialDebtors.GetByIdAsync(newIndustrialDebtor.Id);
                            if (industrialDebtor != null)
                            {
                                if (await _voteBalanceService.UpdateVoteBalanceForOpenBalances(industrialDebtor.FundSourceId!.Value, industrialDebtor.Amount, null, null, "IDB B/F", industrialDebtor.ProjectName, FAMainTransactionMethod.Backward, session, token))
                                {

                                    industrialDebtor.Status = 0;
                                    industrialDebtor.SystemActionAt = DateTime.Now;
                                    industrialDebtor.UpdatedBy = token.userId;
                                    industrialDebtor.UpdatedAt = session.StartAt;

                                    newIndustrialDebtor.Id = null;
                                    newIndustrialDebtor.CreatedAt = session.StartAt;
                                    newIndustrialDebtor.CreatedBy = token.userId;
                                    newIndustrialDebtor.SabhaId = token.sabhaId;
                                    newIndustrialDebtor.OfficeId = token.officeId;
                                    newIndustrialDebtor.SystemActionAt = DateTime.Now;  
                                    await _unitOfWork.IndustrialDebtors.AddAsync(newIndustrialDebtor);

                                    await _voteBalanceService.UpdateVoteBalanceForOpenBalances(newIndustrialDebtor.FundSourceId!.Value, newIndustrialDebtor.Amount, null, null, "IDB B/F", newIndustrialDebtor.ProjectName, FAMainTransactionMethod.Forward, session, token);
                                }




                            }
                            else
                            {
                                throw new Exception("Unable To Find");
                            }
                        }
                        else //create a new object
                        {
                            newIndustrialDebtor.Id = null;
                            newIndustrialDebtor.CreatedAt = session.StartAt;
                            newIndustrialDebtor.CreatedBy = token.userId;
                            newIndustrialDebtor.SabhaId = token.sabhaId;
                            newIndustrialDebtor.OfficeId = token.officeId;
                            newIndustrialDebtor.SystemActionAt = DateTime.Now;
                            await _unitOfWork.IndustrialDebtors.AddAsync(newIndustrialDebtor);

                            await _unitOfWork.CommitAsync();

                            var itemsToSave = new List<CustomVoteEntry>();



                            await _voteBalanceService.UpdateVoteBalanceForOpenBalances(newIndustrialDebtor.FundSourceId!.Value, newIndustrialDebtor.Amount, null, null, "IDB B/F", newIndustrialDebtor.ProjectName, FAMainTransactionMethod.Forward, session, token);

                            await _customVoteBalanceService.UpdateCustomVoteBalanceForOpenBalances(newIndustrialDebtor.CustomVoteId!.Value, FAMainTransactionMethod.Forward, VoteBalanceTransactionTypes.Credit, newIndustrialDebtor.Amount, session, token);
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

        public async Task<(bool, string?)> DeleteIndustrialDebtor(int industrialDebtorId, HTokenClaim token)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                    if (session != null)
                    {

                        var industrialDebtor = await _unitOfWork.IndustrialDebtors.GetByIdAsync(industrialDebtorId);
                        if (industrialDebtor != null)
                        {
                            if (await _voteBalanceService.UpdateVoteBalanceForOpenBalances(industrialDebtor.FundSourceId!.Value, industrialDebtor.Amount, null, null, "IDB B/F", industrialDebtor.ProjectName, FAMainTransactionMethod.Backward, session, token))
                            {
                                industrialDebtor.Status = 0;
                                industrialDebtor.SystemActionAt = DateTime.Now;
                                industrialDebtor.UpdatedBy = token.userId;
                                industrialDebtor.UpdatedAt = session.StartAt;

                                await _unitOfWork.CommitAsync();
                                transaction.Commit();
                                return (true, "Successfully Deleted Entry");
                            }
                            else
                            {
                                throw new FinalAccountException("Unable To Delete Entry");
                            }

                        }
                        else
                        {
                            throw new GeneralException("Unable To Find");
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
