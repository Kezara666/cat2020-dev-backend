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
using CAT20.Services.Control;
using CAT20.Services.Vote;

namespace CAT20.Services.FinalAccount
{
    public class LABankLoanService : ILABankLoanService

    {
        private readonly IVoteUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IVoteBalanceService _voteBalanceService;
        private readonly ICustomVoteBalanceService _customVoteBalanceService;
        private readonly IBankBranchService _bankBranchService;

        public LABankLoanService(IVoteUnitOfWork unitOfWork, IMapper mapper, IVoteBalanceService voteBalanceService,ICustomVoteBalanceService customVoteBalanceService, IBankBranchService bankBranchService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _voteBalanceService = voteBalanceService;
            _customVoteBalanceService = customVoteBalanceService;
            _bankBranchService = bankBranchService;
        }



        public async Task<(int totalCount, IEnumerable<LALoanResource> list)> GetAllLABankLoanForSabha(int sabhaId, int ledgerAccountId, int pageNo, int pageSize, string? filterKeyword, HTokenClaim token)
        {
            var LALoan = await _unitOfWork.LABankLoans.GetAllLABankLoanForSabha(sabhaId, ledgerAccountId, pageNo, pageSize, filterKeyword);

            var LALoanResource = _mapper.Map<IEnumerable<LALoan>, IEnumerable<LALoanResource>>(LALoan.list);

            foreach (var item in LALoanResource)
            {

                item.LoanTypeVoteDetail = _mapper.Map<VoteDetailLimitedresource>(await _unitOfWork.VoteDetails.GetByIdAsync(item.LoanTypeVote!.Value));

                item.BankBranch = _mapper.Map<FinalBankBranchResource>(await _bankBranchService.GetBankBranchWithBankById(item.BankBranchId!.Value));
            }



            return (LALoan.totalCount, LALoanResource);

        }



        public async Task<(bool, string?)> CreateUpdateLABankLoan(SaveLALoanResource newLABankLoanResource, HTokenClaim token)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var newLABankLoan = _mapper.Map<SaveLALoanResource, LALoan>(newLABankLoanResource);

                    //var customeVoteEntries = _mapper.Map<IEnumerable<SaveCustomVoteEntry>, IEnumerable<CustomVoteEntry>>(newLABankLoanResource.customVoteEntries!.OrderBy(x => x.CustomVoteDetailId));

                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                    if (session != null)
                    {
                        if (newLABankLoan.Id != null) //update existing object
                        {
                            var laLoan = await _unitOfWork.LABankLoans.GetByIdAsync(newLABankLoan.Id);
                            if (laLoan != null)
                            {
                                if (await _voteBalanceService.UpdateVoteBalanceForOpenBalances(laLoan.LoanTypeVote!.Value, (decimal)laLoan.Balance!, null, null, $"B/F LA LOAN ", laLoan.LoanPurpose, FAMainTransactionMethod.Backward, session, token))
                                {

                                    laLoan.Status = 0;
                                    laLoan.UpdatedAt = session.StartAt;
                                    laLoan.UpdatedBy = token.userId;
                                    laLoan.SystemActionAt = DateTime.Now;

                                    newLABankLoan.Id = null;
                                    newLABankLoan.CreatedAt = session.StartAt;
                                    newLABankLoan.CreatedBy = token.userId;
                                    newLABankLoan.SabhaId = token.sabhaId;
                                    newLABankLoan.OfficeId = token.officeId;
                                    newLABankLoan.SystemActionAt = DateTime.Now;
                                    await _unitOfWork.LABankLoans.AddAsync(newLABankLoan);

                                    await _unitOfWork.CommitAsync();

                                    var itemsToSave = new List<CustomVoteEntry>();

                                 

                                    // Add all items to the database in one go
                                    await _unitOfWork.CustomVoteEntries.AddRangeAsync(itemsToSave);
                                    await _unitOfWork.CommitAsync();
                                    await _voteBalanceService.UpdateVoteBalanceForOpenBalances(newLABankLoan.LoanTypeVote!.Value, (decimal)newLABankLoan.Balance!, null, null, $"B/F LA LOAN ", newLABankLoan.LoanPurpose, FAMainTransactionMethod.Forward, session, token);

                                
                                        await _customVoteBalanceService.UpdateCustomVoteBalanceForOpenBalances(newLABankLoan.CustomVoteId!.Value, FAMainTransactionMethod.Forward, VoteBalanceTransactionTypes.Credit, newLABankLoan.Balance, session, token);
                                    
                                }

                                else
                                {
                                    throw new Exception("Unable To Update Entry");
                                }

                            }
                            else
                            {
                                throw new Exception("Unable To Find Entry");
                            }
                        }
                        else //create a new object
                        {
                            newLABankLoan.Id = null;
                            newLABankLoan.CreatedAt = session.StartAt;
                            newLABankLoan.CreatedBy = token.userId;
                            newLABankLoan.SabhaId = token.sabhaId;
                            newLABankLoan.OfficeId = token.officeId;
                            newLABankLoan.SystemActionAt = DateTime.Now;
                            await _unitOfWork.LABankLoans.AddAsync(newLABankLoan);

                            await _voteBalanceService.UpdateVoteBalanceForOpenBalances(newLABankLoan.LoanTypeVote!.Value, (decimal)newLABankLoan.Balance!, null, null, $"B/F LA LOAN ", newLABankLoan.LoanPurpose, FAMainTransactionMethod.Forward, session, token);
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

        public async Task<(bool, string?)> DeleteLABankLoan(int laBankLoanId, HTokenClaim token)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                    if (session != null)
                    {

                        var laLoan = await _unitOfWork.LABankLoans.GetByIdAsync(laBankLoanId);
                        if (laLoan != null)
                        {
                            if (await _voteBalanceService.UpdateVoteBalanceForOpenBalances(laLoan.LoanTypeVote!.Value, (decimal)laLoan.Balance!, null, null, $"B/F LA LOAN ", laLoan.LoanPurpose, FAMainTransactionMethod.Backward, session, token))
                            {
                                laLoan.Status = 0;
                                laLoan.UpdatedAt = session.StartAt;
                                laLoan.UpdatedBy = token.userId;
                                laLoan.SystemActionAt = DateTime.Now;
                                await _unitOfWork.CommitAsync();
                                transaction.Commit();
                            }

                            else
                            {
                                throw new Exception("Unable To Update Entry");
                            }

                        }
                        else
                        {
                            throw new Exception("Unable To Find Entry");
                        }



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
