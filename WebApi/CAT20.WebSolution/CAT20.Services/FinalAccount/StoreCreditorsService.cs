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
using DocumentFormat.OpenXml.Spreadsheet;

namespace CAT20.Services.FinalAccount
{
    public class StoreCreditorsService : IStoreCreditorsService
    {
        private readonly IVoteUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IVoteBalanceService _voteBalanceService;
        private readonly ICustomVoteBalanceService _customVoteBalanceService;
        private readonly IPartnerService _partnerService;

        public StoreCreditorsService(IVoteUnitOfWork unitOfWork, IMapper mapper,IVoteBalanceService voteBalanceService,ICustomVoteBalanceService customVoteBalanceService,IPartnerService partnerService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _voteBalanceService = voteBalanceService;
            _customVoteBalanceService = customVoteBalanceService;
            _partnerService = partnerService;
        }





        public async Task<(int totalCount, IEnumerable<StoresCreditorResource> list)> GetAllStoreCreditorsServiceForSabha(int sabhaId, int ledgerAccountId, int pageNo, int pageSize, string? filterKeyword, HTokenClaim token)
        {
            var storeCreditor = await _unitOfWork.StoreCreditors.GetAllStoreCreditorForSabha(sabhaId, ledgerAccountId, pageNo, pageSize, filterKeyword);

            var storeCreditorResource = _mapper.Map<IEnumerable<StoresCreditor>, IEnumerable<StoresCreditorResource>>(storeCreditor.list);

            foreach (var item in storeCreditorResource)
            {
               item.CreditorDebtorInfo  =  _mapper.Map<Partner, CreditorDebtorResource>(await _partnerService.GetById(item.SupplierId));

                item.LedgerVoteDetail = _mapper.Map<VoteDetail, VoteDetailLimitedresource>(await _unitOfWork.VoteDetails.GetByIdAsync(item.LedgerAccountId!.Value));

            }

            return (storeCreditor.totalCount, storeCreditorResource);

        }



        public async Task<(bool, string?)> CreateUpdateStoreCreditor(SaveStoresCreditor storeCreditorResource, HTokenClaim token)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                var newStoresCreditor = _mapper.Map<SaveStoresCreditor, StoresCreditor>(storeCreditorResource);
                //var customeVoteEntries = _mapper.Map<IEnumerable<SaveCustomVoteEntry>, IEnumerable<CustomVoteEntry>>(storeCreditorResource.customVoteEntries!.OrderBy(x=>x.CustomVoteDetailId));

                try
                {

                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                    if (session != null)
                    {

                        if (newStoresCreditor.Id != null) //update existing object
                        {
                            var storeCreditor = await _unitOfWork.StoreCreditors.GetByIdAsync(newStoresCreditor.Id);
                            if (storeCreditor != null)
                            {
                                if (await _voteBalanceService.UpdateVoteBalanceForOpenBalances(storeCreditor.LedgerAccountId!.Value, storeCreditor.InvoiceAmount, null, null, "STC B/F", storeCreditor.OrderNo, FAMainTransactionMethod.Backward, session, token))
                                {

                                    storeCreditor.Status = 0;

                                    newStoresCreditor.Id = null;
                                    newStoresCreditor.CreatedAt = DateTime.Now;
                                    newStoresCreditor.CreatedBy = token.userId;
                                    newStoresCreditor.SabhaId = token.sabhaId;
                                    newStoresCreditor.OfficeId = token.officeId;
                                    await _unitOfWork.StoreCreditors.AddAsync(newStoresCreditor);

                                    await _voteBalanceService.UpdateVoteBalanceForOpenBalances(newStoresCreditor.LedgerAccountId!.Value, newStoresCreditor.InvoiceAmount, null, null, "STC B/F", newStoresCreditor.OrderNo, FAMainTransactionMethod.Forward, session, token);
                                }
                                
                            }
                            else
                            {
                                throw new GeneralException("Unable To Update Entry");


                            }
                       
                        }
                        else //create a new object
                        {
                            //customeVoteEntries.OrderBy(x => x.CustomVoteDetailId);


                            newStoresCreditor.Id = null;
                            newStoresCreditor.CreatedAt = DateTime.Now;
                            newStoresCreditor.CreatedBy = token.userId;
                            newStoresCreditor.SabhaId = token.sabhaId;
                            newStoresCreditor.OfficeId = token.officeId;
                            await _unitOfWork.StoreCreditors.AddAsync(newStoresCreditor);

                            await _unitOfWork.CommitAsync();

                           


                             await _voteBalanceService.UpdateVoteBalanceForOpenBalances(newStoresCreditor.LedgerAccountId!.Value, newStoresCreditor.InvoiceAmount, null, null, "STC", newStoresCreditor.OrderNo, FAMainTransactionMethod.Forward, session, token);
                                                     

                             await _customVoteBalanceService.UpdateCustomVoteBalanceForOpenBalances(newStoresCreditor.CustomVoteId!.Value, FAMainTransactionMethod.Forward, VoteBalanceTransactionTypes.Credit , newStoresCreditor.InvoiceAmount, session, token);
                            


                        }



                        await _unitOfWork.CommitAsync();
                        transaction.Commit();

                        return (true, "Successfully Saved");
                    }
                    else
                    {
                        throw new FinalAccountException("No Active Session Found");
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return (false, ex is FinalAccountException or GeneralException ? ex.Message : null);
                }
            }
        }

        public async Task<(bool, string?)> DeleteStoreCreditor(int storeCreditorId, HTokenClaim token)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {

                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                    if (session != null)
                    {


                        var storeCreditor = await _unitOfWork.StoreCreditors.GetByIdAsync(storeCreditorId);
                        if (storeCreditor != null)
                        {
                            if (await _voteBalanceService.UpdateVoteBalanceForOpenBalances(storeCreditor.LedgerAccountId!.Value, storeCreditor.InvoiceAmount, null, null, "STC B/F", storeCreditor.OrderNo, FAMainTransactionMethod.Backward, session, token))
                            {

                                storeCreditor.Status = 0;
                                storeCreditor.UpdatedAt = session.StartAt;
                                storeCreditor.UpdatedBy = token.userId;
                                storeCreditor.SystemActionAt = DateTime.Now;
                            }

                        }
                        else
                        {
                            throw new GeneralException("Unable To Update Entry");

                        }


                        await _unitOfWork.CommitAsync();
                        transaction.Commit();

                        return (true, "Successfully Delete Entry");
                    }
                    else
                    {
                        throw new FinalAccountException("No Active Session Found");
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
