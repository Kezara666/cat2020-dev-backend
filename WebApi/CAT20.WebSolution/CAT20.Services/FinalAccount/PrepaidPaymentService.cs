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
using CAT20.Core.Models.HRM.PersonalFile;
using CAT20.Core.Models.Vote;
using CAT20.Core.Services.Control;
using CAT20.Core.Services.FinalAccount;
using CAT20.Core.Services.HRM.PersonalFile;
using CAT20.Core.Services.Vote;
using CAT20.Services.Vote;

namespace CAT20.Services.FinalAccount
{
    public class PrepaidPaymentService : IPrepaidPaymentService
    {
        private readonly IVoteUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IVoteBalanceService _voteBalanceService;
        private readonly ICustomVoteBalanceService _customVoteBalanceService;
        private readonly IPartnerService _partnerService;
        private readonly IEmployeeService _employeeService;

        public PrepaidPaymentService(IVoteUnitOfWork unitOfWork, IMapper mapper, IVoteBalanceService voteBalanceService, ICustomVoteBalanceService customVoteBalanceService,IPartnerService partnerService,IEmployeeService employeeService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _voteBalanceService = voteBalanceService;
            _customVoteBalanceService = customVoteBalanceService;
            _partnerService = partnerService;
            _employeeService = employeeService;
        }

        public async Task<(int totalCount, IEnumerable<PrePaidPaymentsResource> list)> GetAllPrepaidPaymentsForSabha(int sabhaId, int ledgerAccountId, int pageNo, int pageSize, string? filterKeyword, HTokenClaim token)
        {
            var prepaidPayments = await _unitOfWork.PrepaidPayments.GetAllPrepaidPaymentsForSabha(sabhaId, ledgerAccountId, pageNo, pageSize, filterKeyword);

            var prepaidPaymentsResource = _mapper.Map<IEnumerable<PrepaidPayment>, IEnumerable<PrePaidPaymentsResource>>(prepaidPayments.list);

            foreach (var item in prepaidPaymentsResource)
            {
                if (item.PrePaidToCategory == VoucherPayeeCategory.Partner || item.PrePaidToCategory == VoucherPayeeCategory.Business) { 

                     item.CreditorDebtorInfo = _mapper.Map<Partner, CreditorDebtorResource>(await _partnerService.GetById(item.PrePaidPaidToId));
                 }

                if (item.PrePaidToCategory == VoucherPayeeCategory.Employee)
                {
                    item.CreditorDebtorInfo = _mapper.Map<Employee, CreditorDebtorResource>(await _employeeService.GetEmployeeById(item.PrePaidPaidToId!.Value));
                }

                item.CategoryVoteDetail = _mapper.Map<VoteDetailLimitedresource>(await _unitOfWork.VoteDetails.GetByIdAsync(item.CategoryVote!.Value));
            }

            return (prepaidPayments.totalCount, prepaidPaymentsResource);

        }



        public async Task<(bool, string?)> CreateUpdatePrepaidPayment(SavePrepaidPaymentResource newPrepaidPaymentResource, HTokenClaim token)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var newPrepaidPayment = _mapper.Map<SavePrepaidPaymentResource, PrepaidPayment>(newPrepaidPaymentResource);
                    //var customeVoteEntries = _mapper.Map<IEnumerable<SaveCustomVoteEntry>, IEnumerable<CustomVoteEntry>>(newPrepaidPaymentResource.customVoteEntries!.OrderBy(x => x.CustomVoteDetailId));

                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                    if (session != null)
                    {

                        if (newPrepaidPayment.Id != null) //update existing object
                        {
                            var prepaidPayment = await _unitOfWork.PrepaidPayments.GetByIdAsync(newPrepaidPayment.Id);
                            if (prepaidPayment != null)
                            {
                                if (await _voteBalanceService.UpdateVoteBalanceForOpenBalances(prepaidPayment.CategoryVote!.Value, prepaidPayment.Amount, null, null, "PRE PAID RB", prepaidPayment.Description, FAMainTransactionMethod.Backward, session, token))
                                {

                                    prepaidPayment.Status = 0;
                                    prepaidPayment.SystemActionAt = DateTime.Now;
                                    prepaidPayment.UpdatedAt = session.StartAt;
                                    prepaidPayment.UpdatedBy = token.userId;

                                    newPrepaidPayment.Id = null;
                                    newPrepaidPayment.CreatedAt = session.StartAt;
                                    newPrepaidPayment.CreatedBy = token.userId;
                                    newPrepaidPayment.SabhaId = token.sabhaId;
                                    newPrepaidPayment.OfficeId = token.officeId;
                                    newPrepaidPayment.SystemActionAt = DateTime.Now;
                                    await _unitOfWork.PrepaidPayments.AddAsync(newPrepaidPayment);

                                    await _voteBalanceService.UpdateVoteBalanceForOpenBalances(newPrepaidPayment.CategoryVote!.Value, newPrepaidPayment.Amount, null, null, "PRE PAID", newPrepaidPayment.Description, FAMainTransactionMethod.Forward, session, token);
                                }

                            }
                            else
                            {
                                throw new GeneralException("Unable To Update Entry");


                            }

                        }
                        else
                        {
                            newPrepaidPayment.Id = null;
                            newPrepaidPayment.CreatedAt = session.StartAt;
                            newPrepaidPayment.CreatedBy = token.userId;
                            newPrepaidPayment.SabhaId = token.sabhaId;
                            newPrepaidPayment.OfficeId = token.officeId;
                            newPrepaidPayment.SystemActionAt = DateTime.Now;
                            await _unitOfWork.PrepaidPayments.AddAsync(newPrepaidPayment);



                            await _unitOfWork.CommitAsync();

                            

                            await _voteBalanceService.UpdateVoteBalanceForOpenBalances(newPrepaidPayment.CategoryVote!.Value, newPrepaidPayment.Amount, null, null, "PRE PAID", newPrepaidPayment.Description, FAMainTransactionMethod.Forward, session, token);


                                await _customVoteBalanceService.UpdateCustomVoteBalanceForOpenBalances(newPrepaidPayment.CustomVoteId!.Value, FAMainTransactionMethod.Forward, VoteBalanceTransactionTypes.Credit, newPrepaidPayment.Amount, session, token);
                           
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

        public async Task<(bool, string?)> DeletePrepaidPayment(int prepaidPaymentId, HTokenClaim token)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {

                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                    if (session != null)
                    {


                        var prepaidPayment = await _unitOfWork.PrepaidPayments.GetByIdAsync(prepaidPaymentId);
                        if (prepaidPayment != null)
                        {
                            if (await _voteBalanceService.UpdateVoteBalanceForOpenBalances(prepaidPayment.CategoryVote!.Value, prepaidPayment.Amount, null, null, "PRE PAID RB", prepaidPayment.Description, FAMainTransactionMethod.Backward, session, token))
                            {

                                prepaidPayment.Status = 0;
                                prepaidPayment.SystemActionAt = DateTime.Now;
                                prepaidPayment.UpdatedAt = session.StartAt;
                                prepaidPayment.UpdatedBy = token.userId;

                                await _unitOfWork.CommitAsync();
                                transaction.Commit();

                                return (true, "Successfully Delete Entry");

                            }
                            {
                                throw new FinalAccountException("Unable To Delete Entry");
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
