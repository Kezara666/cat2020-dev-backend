using AutoMapper;
using CAT20.Core.Services.Control;
using CAT20.Core;
using CAT20.Core.Services.FinalAccount;
using CAT20.Core.Services.Vote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.DTO.Final.Save;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.CustomExceptions;

namespace CAT20.Services.FinalAccount
{
    public class FinalAccountAdjustmentService : IFinalAccountAdjustmentService
    {
        private readonly IVoteUnitOfWork _unitOfWork;
        private readonly IVoteBalanceService _voteBalanceService;
        private readonly ICustomVoteBalanceService _customVoteBalanceService;
        private readonly IPartnerService _partnerService;
        private readonly IMapper _mapper;

        public FinalAccountAdjustmentService(IVoteUnitOfWork unitOfWork, IMapper mapper, IVoteBalanceService voteBalanceService, ICustomVoteBalanceService customVoteBalanceService, IPartnerService partnerService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _voteBalanceService = voteBalanceService;
            _customVoteBalanceService = customVoteBalanceService;
            _partnerService = partnerService;
        }

        public async Task<(bool, string?)> CreateUpdateStockExpenditureAdjustment(SaveStockExpenditureAdjustment adjustment, HTokenClaim token)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                    if (session != null)
                    {





                        /*update AssesetBalnce*/

                        await _voteBalanceService.UpdateVoteBalance(new HVoteBalanceTransaction
                        {
                            VoteDetailId = adjustment.StockLedgerId!.Value,
                            Amount = (decimal)adjustment.Amount,
                            Year = session.StartAt.Year,
                            Month = session.StartAt.Month,
                            Code = $"STR EXP AJUST",
                            SubCode = null,
                            TransactionMethod = FAMainTransactionMethod.Forward,
                            TransactionType = VoteBalanceTransactionTypes.Credit,
                            Session = session
                        }, token);


                        /*update Expenditure balance*/


                        await _voteBalanceService.UpdateVoteBalance(new HVoteBalanceTransaction
                        {
                            VoteDetailId = adjustment.ExpenditureLedgerId.Value,
                            Amount = (decimal)adjustment.Amount,
                            Year = session.StartAt.Year,
                            Month = session.StartAt.Month,
                            Code = $"STR EXP AJUST",
                            SubCode = null,
                            TransactionMethod = FAMainTransactionMethod.Forward,
                            TransactionType = VoteBalanceTransactionTypes.Debit,
                            Session = session
                        }, token);


                        await _unitOfWork.CommitAsync();
                        transaction.Commit();

                        return (true, $"Successfully Saved Entry");

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
