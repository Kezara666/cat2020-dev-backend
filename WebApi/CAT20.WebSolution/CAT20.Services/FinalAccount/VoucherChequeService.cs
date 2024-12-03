using AutoMapper;
using CAT20.Core;
using CAT20.Core.DTO.Final;
using CAT20.Core.DTO.OtherModule;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.Mixin;
using CAT20.Core.Models.Vote;
using CAT20.Core.Services.FinalAccount;
using CAT20.WebApi.Resources.Final;
using Irony.Parsing;
using System.Collections.Generic;

namespace CAT20.Services.FinalAccount;

public class VoucherChequeService : IVoucherChequeService
{
    private readonly IVoteUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public VoucherChequeService(IVoteUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<(int totalCount, IEnumerable<VoucherChequeResource> list)> getChequeForSabha(int sabhaId, bool stage,
        int pageNo,
        int pageSize, string? filterKeyWord)
    {

        var vcs = await _unitOfWork.VoucherCheque.getChequeForSabha(sabhaId, stage, pageNo, pageSize, filterKeyWord);

        var vcsResource = _mapper.Map<IEnumerable<VoucherCheque>, IEnumerable<VoucherChequeResource>>(vcs.list);

        foreach (var vc in vcsResource)
        {
            try { 
                var x = await _unitOfWork.AccountDetails.GetByIdAsync(vc.BankId);
                vc.AccountDetail = _mapper.Map<AccountDetail, AccountDetailOnlyBankId>(x);

            }
            catch (Exception e)
            {
                // ignored
            }

            try
            {
                List<int> itemIds = vc.VoucherItemsForCheque
                     .Select(i => (int)i.SubVoucherItemId)
                     .ToList();
                vc.Vouchers= _mapper.Map<IEnumerable<Voucher>, IEnumerable<VoucherResource>>(await _unitOfWork.Voucher.GetVoucherBySubVouchers(itemIds));

            }
            catch(Exception e)
            {
                // ignored
            }
            
        }


        return (vcs.totalCount, vcsResource);

    }

    public async Task<VoucherCheque> payVoucher(int id, bool status, MakeVoucherApproveRejectResource approval)
    {
        //var voucherChequeRepository = await _unitOfWork.VoucherCheque.GetByIdAsync(id);
        //voucherChequeRepository.IsPrinted = status;
        //await _unitOfWork.VoucherLog.AddAsync(approval.VoucherLog);
        //await _unitOfWork.VoucherActionLog.AddAsync(approval.ApprovedLog);
        //await _unitOfWork.CommitAsync();
        return await _unitOfWork.VoucherCheque.GetByIdAsync(id);
    }

    public async Task<bool> PrintCheque(int id, HTokenClaim token)
    {
        try
        {
            var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);

            if (session != null) {

                var vc = await _unitOfWork.VoucherCheque.GetByIdAsync(id);

                //var vids = vc.getVoucherIds();

                //string code = "";
                //foreach (var vid in vids)
                //{
                //    var voucher = await _unitOfWork.Voucher.GetByIdAsync(vid);
                //    code = string.Join(",", voucher.VoucherSequenceNumber);

                //}

                //if(await CreateCashBookEntry(vc, code, token, session))
                //{
                //    vc.IsPrinted = true;
                //    await _unitOfWork.CommitAsync();
                //}

                vc.IsPrinted = true;
                await _unitOfWork.CommitAsync();

                return true;
            }
            else
            {
                throw new Exception("No Active Session Found");
            }


        }catch(Exception e)
        {
            return false;
        }
    }

    private async Task<bool> CreateCashBookEntry(VoucherCheque voucherCheque,string code,HTokenClaim token, Session session)
    {


        try
        {
            var account = await _unitOfWork.AccountDetails.GetByIdAsync(voucherCheque.BankId);
            //var session = await _unitOfWork.Sessions.GetByIdAsync(mx.SessionId);

            account.RunningBalance -= voucherCheque.Amount;
            account.ExpenseHold -= voucherCheque.Amount;

           
            if (account.RunningBalance < 0)
            {
                throw new Exception("Insufficient Balance");
            }
            
            var cashbook = new CashBook
            {
                //Id
                SabhaId = token.sabhaId,
                OfiiceId = token.officeId,
                SessionId = session.Id,
                BankAccountId = voucherCheque.BankId,
                Date = session.CreatedAt,

                TransactionType = CashBookTransactionType.CREDIT,
                ExpenseCategory = CashBookExpenseCategory.VoucherCheque,
                IncomeExpenseMethod = CashBookIncomeExpenseMethod.Cheque,
                Code = code,
                ExpenseItemId = voucherCheque.Id,   
                ChequeAmount = voucherCheque.Amount,
                RunningTotal = account.RunningBalance,

                Description ="",
                CreatedAt = DateTime.Now,
                CreatedBy = token.userId,


            };




            await _unitOfWork.CashBook.AddAsync(cashbook);

            return true;

        }
        catch (Exception ex)
        {
            return false;
        }


    }


}