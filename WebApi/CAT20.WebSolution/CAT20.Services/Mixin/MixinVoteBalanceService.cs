using AutoMapper;
using CAT20.Core;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.Vote;
using CAT20.Core.Services.Mixin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.Mixin
{
    public  class MixinVoteBalanceService : IMixinVoteBalanceService
    {
        private readonly IMapper _mapper;
        private readonly IMixinUnitOfWork _unitOfWork;

        public MixinVoteBalanceService(IMapper mapper, IMixinUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<VoteBalance> CreateNewVoteBalance(int VoteDetailId, Session session, HTokenClaim token)
        {



            try
            {

                var vt = await _unitOfWork.VoteDetails.GetByIdAsync(VoteDetailId);

                if (vt != null)
                {
                    /*
                    income =1
                    expense =2
                    liability =3
                    asset =4
                    */


                    var voteBalance = new VoteBalance
                    {
                        VoteDetailId = VoteDetailId,
                        SabhaId = token.sabhaId,
                        Year = session.StartAt.Year,
                        AllocationAmount = 0,
                        Credit = 0,
                        ClassificationId = vt.ClassificationID,
                        Debit = 0,
                        ExchangedAmount = 0,
                        Status = VoteBalanceStatus.Active,
                        TransactionType = VoteBalanceTransactionTypes.Inti,
                        OfficeId = token.officeId,
                        CreatedAt = session.StartAt,
                        UpdatedAt = session.StartAt,
                        CreatedBy = token.userId,
                        UpdatedBy = token.userId,
                        SystemActionAt = DateTime.Now
                    };
                    await _unitOfWork.VoteBalances.AddAsync(voteBalance);
                    await _unitOfWork.CommitAsync();


                    if (vt.ClassificationID == 1)
                    {

                    }
                    else if (vt.ClassificationID == 2)
                    {

                    }
                    else if (vt.ClassificationID == 3)
                    {
                        //var vtb = new VoteLedgerBook
                        //{
                        //    SabhaId = token.sabhaId,
                        //    OfiiceId = token.officeId,
                        //    SessionId = session.Id,
                        //    Description = $"B/F {session.StartAt.Year - 1} Balance",
                        //    Date = session.StartAt,
                        //    VoteBalanceId = (int)voteBalance.Id!,
                        //    VoteDetailId = (int)voteBalance.VoteDetailId,
                        //    TransactionType = CashBookTransactionType.DEBIT,
                        //    VoteBalanceTransactionTypes = VoteBalanceTransactionTypes.Inti,
                        //    Credit = 0,
                        //    Debit = 0,
                        //    RunningTotal = voteBalance.CreditDebitRunningBalance,
                        //    RowStatus = 1,
                        //    CreatedAt = session.StartAt,
                        //    UpdatedAt = session.StartAt,
                        //    CreatedBy = token.userId,
                        //    UpdatedBy = token.userId,
                        //    SystemActionDate = DateTime.Now
                        //};

                        //await _unitOfWork.VoteLedgerBook.AddAsync(vtb);
                    }
                    else if (vt.ClassificationID == 4)
                    {
                        //var vtb = new VoteLedgerBook
                        //{
                        //    //Id=null,
                        //    SabhaId = token.sabhaId,
                        //    OfiiceId = token.officeId,
                        //    SessionId = session.Id,
                        //    Description = $"B/F {session.StartAt.Year - 1} Balance",
                        //    Date = session.StartAt,
                        //    VoteBalanceId = (int)voteBalance.Id!,
                        //    VoteDetailId = (int)voteBalance.VoteDetailId,
                        //    TransactionType = CashBookTransactionType.DEBIT,
                        //    VoteBalanceTransactionTypes = VoteBalanceTransactionTypes.Inti,
                        //    Credit = 0,
                        //    Debit = 0,
                        //    RunningTotal = voteBalance.CreditDebitRunningBalance,
                        //    RowStatus = 1,
                        //    CreatedAt = session.StartAt,
                        //    UpdatedAt = session.StartAt,
                        //    CreatedBy = token.userId,
                        //    UpdatedBy = token.userId,
                        //    SystemActionDate = DateTime.Now
                        //};

                        //await _unitOfWork.VoteLedgerBook.AddAsync(vtb);
                    }
                    else
                    {
                        throw new Exception("Vote Detail Not Have Classification");
                    }


                    await _unitOfWork.CommitAsync();

                    var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalance);

                    var vbtActionLog = new VoteBalanceActionLog
                    {

                        VoteBalanceId = voteBalance.Id,
                        ActionState = FinalAccountActionStates.Init,
                        ActionBy = token.userId,
                        Comment = "create",
                        ActionDateTime = session.StartAt,
                        SystemActionAt = DateTime.Now,
                    };


                    await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                    await _unitOfWork.VoteBalanceActionLogs.AddAsync(vbtActionLog);
                    //await _unitOfWork.CommitAsync();
                    return voteBalance;


                }
                else
                {
                    throw new Exception("Vote Detail Not Found");
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
