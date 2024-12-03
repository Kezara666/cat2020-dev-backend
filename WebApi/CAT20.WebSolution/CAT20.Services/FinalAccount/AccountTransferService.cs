using AutoMapper;
using AutoMapper.Configuration.Annotations;
using CAT20.Core;
using CAT20.Core.CustomExceptions;
using CAT20.Core.DTO.Final;
using CAT20.Core.DTO.OtherModule;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.User;
using CAT20.Core.Models.Vote;
using CAT20.Core.Services.Control;
using CAT20.Core.Services.FinalAccount;
using CAT20.Core.Services.Mixin;
using CAT20.Core.Services.User;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.FinalAccount
{
    public class AccountTransferService : IAccountTransferService
    {
        private readonly IVoteUnitOfWork _unitOfWork;
        private readonly IUserDetailService _userDetailService;
        private readonly IVoucherService _voucherService;
        private readonly IMapper _mapper;
        private readonly IPartnerService _partnerService;
        private readonly IMixinOrderService _mixinOrderService;

        public AccountTransferService(IVoteUnitOfWork unitOfWork, IUserDetailService userDetailServiceService, IVoucherService voucherService , IMapper mapper, IPartnerService partnerService, IMixinOrderService mixinOrderService)
        {
            _unitOfWork = unitOfWork;
            _userDetailService = userDetailServiceService;
            _voucherService = voucherService;
            _mapper = mapper;
            _partnerService = partnerService;
            _mixinOrderService = mixinOrderService;
        }

        public async Task<(bool,string)> Create(AccountTransfer newTransfer, HTokenClaim token)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {

                try
                {

                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                    if (session != null)
                    {
                        var fromAccount = await _unitOfWork.AccountDetails.GetByIdAsync(newTransfer.FromAccountId);
                        var toAccount = await _unitOfWork.AccountDetails.GetByIdAsync(newTransfer.ToAccountId);

                        var fromVoteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(fromAccount.VoteId!.Value,token.sabhaId,session.StartAt.Year);
                        var toVoteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(toAccount.VoteId!.Value, token.sabhaId, session.StartAt.Year);

                        if (fromAccount != null && toAccount != null && fromVoteBalance != null && toVoteBalance!=null)
                        {


                            newTransfer.CreatedAt = session.StartAt;
                            newTransfer.SabhaId = token.sabhaId;
                            newTransfer.OfficeId = token.officeId;
                            newTransfer.CreatedBy = token.userId;
                            newTransfer.SystemCreateAt = DateTime.Now;

                            newTransfer.FromVoteDetailId = fromAccount.VoteId!.Value;
                            newTransfer.ToVoteDetailId = toAccount.VoteId!.Value;

                            newTransfer.FromVoteBalanceId = fromVoteBalance.Id!.Value;
                            newTransfer.ToVoteBalanceId = toVoteBalance.Id!.Value;

                            await _unitOfWork.AccountTransfer.AddAsync(newTransfer);

                            await _unitOfWork.CommitAsync();


                            var response = await _voucherService.CreateAccountTransferVoucher(newTransfer, session, token);

                            if (response.Item1)
                            {
                                newTransfer.VoucherId = response.Item3.Id!.Value;
                                await _unitOfWork.CommitAsync();
                                transaction.Commit();
                                return (true, "Transfer Voucher Saved Successfully");

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
                        throw new FinalAccountException("No Active Session Found");
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

        public async Task<AccountTransferResource> GetAccountTransferById(int Id, HTokenClaim token)
        {
            var transfer = await _unitOfWork.AccountTransfer.GetAccountTransferById(Id);

            var transferResource = _mapper.Map<AccountTransfer, AccountTransferResource>(transfer);

           
                if (transferResource.VoucherId.HasValue)
                {
                   transferResource.Voucher = _mapper.Map<Voucher, VoucherResource>(await _unitOfWork.Voucher.GetByIdAsync(transferResource.VoucherId!.Value));
                }

            transferResource.RefundedAmount = 0;   
            foreach (var c in transferResource.AccountTransferRefunding!)
            {
                if (c.VoucherId.HasValue)
                {
                    c.Voucher = _mapper.Map<Voucher, VoucherResource>(await _unitOfWork.Voucher.GetByIdAsync(c.VoucherId!.Value));
                    transferResource.RefundedAmount += c.Amount;
                }
            }


            if (transferResource.FromVoteDetailId.HasValue)
                {

                    transferResource.FromVoteDetail = _mapper.Map<VoteDetail, VoteDetailLimitedresource>(await _unitOfWork.VoteDetails.GetByIdAsync(transferResource.FromVoteDetailId));
                }

                if (transferResource.ToVoteDetailId.HasValue)
                {

                    transferResource.ToVoteDetail = _mapper.Map<VoteDetail, VoteDetailLimitedresource>(await _unitOfWork.VoteDetails.GetByIdAsync(transferResource.ToVoteDetailId));
                }


                if(transferResource.FromAccountId.HasValue)
                 {
                    transferResource.FromAccount = _mapper.Map<AccountDetail, AccountDetailOnlyBankId>(await _unitOfWork.AccountDetails.GetByIdAsync(transferResource.FromAccountId!.Value));
                }

            if (transferResource.ToAccountId.HasValue)
            {
                transferResource.ToAccount = _mapper.Map<AccountDetail, AccountDetailOnlyBankId>(await _unitOfWork.AccountDetails.GetByIdAsync(transferResource.ToAccountId!.Value));
            }

            return transferResource;
        }

        public async Task<(int totalCount, IEnumerable<AccountTransferResource> list)> GetAllAccountTransferForSabha(int sabhaId, bool? type, int pageNo, int pageSize, string? filterKeyWord)
        {
            var transfer = await _unitOfWork.AccountTransfer.GetAllAccountTransferForSabha(sabhaId,type, pageNo, pageSize, filterKeyWord);

            var transferResources = _mapper.Map<IEnumerable<AccountTransfer>, IEnumerable<AccountTransferResource>>(transfer.list);

            foreach (var c in transferResources)
            {
                if (c.VoucherId.HasValue)
                {
                    c.Voucher = _mapper.Map<Voucher, VoucherResource>(await _unitOfWork.Voucher.GetByIdAsync(c.VoucherId!.Value));
                }

                if (c.FromVoteDetailId.HasValue)
                {

                    c.FromVoteDetail = _mapper.Map<VoteDetail, VoteDetailLimitedresource>(await _unitOfWork.VoteDetails.GetByIdAsync(c.FromVoteDetailId));
                }

                if (c.ToVoteDetailId.HasValue)
                {

                    c.ToVoteDetail = _mapper.Map<VoteDetail, VoteDetailLimitedresource>(await _unitOfWork.VoteDetails.GetByIdAsync(c.ToVoteDetailId));
                }

                if (type == true)
                {
                    c.IsRefundable = (c.Voucher!.ActionState == FinalAccountActionStates.HasCheque || (bool)c.IsFullyRefunded!);

                }
            }

            return (transfer.totalCount, transferResources);
        }

        public Task<bool> WithdrawAccountTransfer(int journalId, HTokenClaim token)
        {
            throw new NotImplementedException();
        }
    }
}
