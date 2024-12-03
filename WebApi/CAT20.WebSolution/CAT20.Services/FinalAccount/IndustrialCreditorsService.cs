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
    public class IndustrialCreditorsService : IIndustrialCreditorsService
    {
        private readonly IVoteUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IVoteBalanceService _voteBalanceService;
        private readonly ICustomVoteBalanceService _customVoteBalanceService;
        private readonly IPartnerService _partnerService;

        public IndustrialCreditorsService(IVoteUnitOfWork unitOfWork, IMapper mapper, IVoteBalanceService voteBalanceService,ICustomVoteBalanceService customVoteBalanceService, IPartnerService partnerService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _voteBalanceService = voteBalanceService;
            _customVoteBalanceService = customVoteBalanceService;
            _partnerService = partnerService;
        }
        public async Task<(int totalCount, IEnumerable<IndustrialCreditorsResource> list)> GetAllIndustrialCreditorsForSabha(int sabhaId, int ledgerAccountId, int pageNo, int pageSize, string? filterKeyword, HTokenClaim token)
        {
            var InduastrialCreditors = await _unitOfWork.IndustrialCreditors.GetAllIndustrialCreditorsForSabha(sabhaId, ledgerAccountId, pageNo, pageSize, filterKeyword);

            var InduastrialCreditorsResource = _mapper.Map<IEnumerable<IndustrialCreditors>, IEnumerable<IndustrialCreditorsResource>>(InduastrialCreditors.list);

            foreach (var item in InduastrialCreditorsResource)
            {
                item.CreditorDebtorInfo = _mapper.Map<Partner, CreditorDebtorResource>(await _partnerService.GetById(item.CreditorId));

                item.CategoryVoteDetail = _mapper.Map<VoteDetailLimitedresource>(await _unitOfWork.VoteDetails.GetByIdAsync(item.CategoryVote!.Value));
                item.FundSource = _mapper.Map<VoteDetailLimitedresource>(await _unitOfWork.VoteDetails.GetByIdAsync(item.FundSourceId!.Value));
            }

            return (InduastrialCreditors.totalCount, InduastrialCreditorsResource);

        }

        public async Task<(bool, string?)> CreateUpdateIndustrialCreditors(SaveIndustrialCreditorsResource newIndustrialCreditorResource, HTokenClaim token)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);

                    var newIndustrialCreditor = _mapper.Map<SaveIndustrialCreditorsResource, IndustrialCreditors>(newIndustrialCreditorResource);
                    //var customeVoteEntries = _mapper.Map<IEnumerable<SaveCustomVoteEntry>, IEnumerable<CustomVoteEntry>>(newIndustrialCreditorResource.customVoteEntries!.OrderBy(x => x.CustomVoteDetailId));



                    if (session != null)
                    {


                        if (newIndustrialCreditor.Id != null) //update existing object
                        {
                            var industrialCreditor = await _unitOfWork.IndustrialCreditors.GetByIdAsync(newIndustrialCreditor.Id);
                            if (industrialCreditor != null)
                            {
                                if (await _voteBalanceService.UpdateVoteBalanceForOpenBalances(industrialCreditor.FundSourceId!.Value, industrialCreditor.Amount, null, null, "IDC B/F", industrialCreditor.ProjectName, FAMainTransactionMethod.Backward, session, token))
                                {

                                    industrialCreditor.Status = 0;
                                    industrialCreditor.SystemActionAt = DateTime.Now;
                                    industrialCreditor.UpdatedBy = token.userId;
                                    industrialCreditor.UpdatedAt = session.StartAt;

                                    newIndustrialCreditor.Id = null;
                                    newIndustrialCreditor.CreatedAt = session.StartAt;
                                    newIndustrialCreditor.CreatedBy = token.userId;
                                    newIndustrialCreditor.SabhaId = token.sabhaId;
                                    newIndustrialCreditor.OfficeId = token.officeId;
                                    newIndustrialCreditor.SystemActionAt = DateTime.Now;

                                    await _unitOfWork.IndustrialCreditors.AddAsync(newIndustrialCreditor);

                                    await _voteBalanceService.UpdateVoteBalanceForOpenBalances(newIndustrialCreditor.FundSourceId!.Value, newIndustrialCreditor.Amount, null, null, "IDC B/F", newIndustrialCreditor.ProjectName, FAMainTransactionMethod.Forward, session, token);
                                }
                                else
                                {
                                    throw new GeneralException("Unable To Update Entry");
                                }
                            }
                            else
                            {
                                throw new GeneralException("Unable To Find Entry");
                            }

                        }
                        else //create a new object
                        {
                            newIndustrialCreditor.Id = null;
                            newIndustrialCreditor.CreatedAt = session.StartAt;
                            newIndustrialCreditor.CreatedBy = token.userId;
                            newIndustrialCreditor.SabhaId = token.sabhaId;
                            newIndustrialCreditor.OfficeId = token.officeId;
                            newIndustrialCreditor.SystemActionAt = DateTime.Now;
                            await _unitOfWork.IndustrialCreditors.AddAsync(newIndustrialCreditor);

                            await _unitOfWork.CommitAsync();

                            var itemsToSave = new List<CustomVoteEntry>();

                           

                            // Add all items to the database in one go
                            await _unitOfWork.CustomVoteEntries.AddRangeAsync(itemsToSave);
                            await _unitOfWork.CommitAsync();

                            await _voteBalanceService.UpdateVoteBalanceForOpenBalances(newIndustrialCreditor.FundSourceId!.Value, newIndustrialCreditor.Amount, null, null, "IDC B/F", newIndustrialCreditor.ProjectName, FAMainTransactionMethod.Forward, session, token);


                            await _customVoteBalanceService.UpdateCustomVoteBalanceForOpenBalances(newIndustrialCreditor.CustomVoteId!.Value, FAMainTransactionMethod.Forward, VoteBalanceTransactionTypes.Credit, newIndustrialCreditor.Amount, session, token);
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

        public async Task<(bool, string?)> DeleteIndustrialCreditors(int industrialCreditorsId, HTokenClaim token)
        {

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                    if (session != null)
                    {


                        var industrialCreditor = await _unitOfWork.IndustrialCreditors.GetByIdAsync(industrialCreditorsId);
                        if (industrialCreditor != null)
                        {
                            if (await _voteBalanceService.UpdateVoteBalanceForOpenBalances(industrialCreditor.FundSourceId!.Value, industrialCreditor.Amount, null, null, "IDC B/F", industrialCreditor.ProjectName, FAMainTransactionMethod.Backward, session, token))
                            {

                                industrialCreditor.Status = 0;
                                industrialCreditor.SystemActionAt = DateTime.Now;
                                industrialCreditor.UpdatedBy = token.userId;
                                industrialCreditor.UpdatedAt = session.StartAt;


                                await _unitOfWork.CommitAsync();
                                transaction.Commit();

                                return (true, "Successfully Delete Entry");

                            }
                            else
                            {
                                throw new GeneralException("Unable To Update Entry");
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
