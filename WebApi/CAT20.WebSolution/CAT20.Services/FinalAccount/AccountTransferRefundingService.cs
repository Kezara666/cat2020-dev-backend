using AutoMapper;
using CAT20.Core.Services.Control;
using CAT20.Core;
using CAT20.Core.Services.FinalAccount;
using CAT20.Core.Services.Mixin;
using CAT20.Core.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.HelperModels;
using CAT20.Core.CustomExceptions;

namespace CAT20.Services.FinalAccount
{
    public class AccountTransferRefundingService : IAccountTransferRefundingService
    {
        private readonly IVoteUnitOfWork _unitOfWork;
        private readonly IUserDetailService _userDetailService;
        private readonly IVoucherService _voucherService;
        private readonly IMapper _mapper;
        private readonly IPartnerService _partnerService;
        private readonly IMixinOrderService _mixinOrderService;

        public AccountTransferRefundingService(IVoteUnitOfWork unitOfWork, IUserDetailService userDetailServiceService, IVoucherService voucherService, IMapper mapper, IPartnerService partnerService, IMixinOrderService mixinOrderService)
        {
            _unitOfWork = unitOfWork;
            _userDetailService = userDetailServiceService;
            _voucherService = voucherService;
            _mapper = mapper;
            _partnerService = partnerService;
            _mixinOrderService = mixinOrderService;
        }

        public async Task<(bool, string)> Create(AccountTransferRefunding newRefunding, HTokenClaim token)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {

                try
                {

                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                    if (session != null)
                    {

                        var transfer = await _unitOfWork.AccountTransfer.GetByIdAsync(newRefunding.AccountTransferId);
                        if (transfer != null)
                        {

                            if(newRefunding.Amount <= 0)
                            {
                                throw new FinalAccountException("Invalid Refunding Amount");
                            }


                            if(transfer.RefundedAmount + newRefunding.Amount > transfer.Amount)
                            {
                                throw new FinalAccountException("Refunded Amount Exceeds Transfer Amount");
                            }


                            var fromAccount = await _unitOfWork.AccountDetails.GetByIdAsync(transfer.FromAccountId);
                            var toAccount = await _unitOfWork.AccountDetails.GetByIdAsync(transfer.ToAccountId);

                            var fromVoteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(fromAccount.VoteId!.Value, token.sabhaId, session.StartAt.Year);
                            var toVoteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(toAccount.VoteId!.Value, token.sabhaId, session.StartAt.Year);

                            if (fromAccount != null && toAccount != null && fromVoteBalance != null && toVoteBalance != null)
                            {


                                newRefunding.CreatedAt = session.StartAt;
                                newRefunding.CreatedBy = token.userId;
                                newRefunding.SystemCreateAt = DateTime.Now;


                                await _unitOfWork.AccountTransferRefunding.AddAsync(newRefunding);

                                await _unitOfWork.CommitAsync();


                                var response = await _voucherService.CreateAccountRefundingVoucher(transfer, newRefunding, session, token);

                                if (response.Item1)
                                {
                                    newRefunding.VoucherId = response.Item3.Id!.Value;
                                    await _unitOfWork.CommitAsync();
                                    transaction.Commit();
                                    return (true, "Refunding Voucher Saved Successfully");

                                }
                                else
                                {
                                    throw new Exception(response.Item2);
                                }

                            }
                            else
                            {
                                throw new Exception("Invalid Account Details");
                            }

                        }
                        else
                        {

                            throw new FinalAccountException(" Account Transfer Not Found");


                        }
                    }
                    else
                    {
                        throw new Exception("No Active Session Found");
                    }




                }

                catch (Exception ex)
                {
                    transaction.Rollback();
                    if (ex.GetType() == typeof(FinalAccountException))
                    {
                        return (false, ex.Message);
                    }
                    else
                    {
                        return (false, null);

                    }
                }
            }
        }

        public Task<bool> WithdrawAccountRefundingTransfer(int Id, HTokenClaim token)
        {
            throw new NotImplementedException();
        }
    }
}
