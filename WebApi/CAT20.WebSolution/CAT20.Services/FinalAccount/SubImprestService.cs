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
using CAT20.Core.DTO.Final;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.DTO.OtherModule;
using CAT20.Core.Models.User;
using CAT20.Core.HelperModels;
using System.Diagnostics;
using CAT20.Core.Models.Enums;
using CAT20.Core.Services.HRM.PersonalFile;
using CAT20.Core.Models.HRM.PersonalFile;
using CAT20.Core.Models.Mixin;
using CAT20.Core.CustomExceptions;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Vote;
using CAT20.Core.Services.Vote;

namespace CAT20.Services.FinalAccount
{
    public class SubImprestService : ISubImprestService
    {
        private readonly IVoteUnitOfWork _unitOfWork;
        private readonly IEmployeeService _employeeService;
        private readonly IUserDetailService _userDetailService;
        private readonly IVoucherService _voucherService;
        private readonly IMapper _mapper;
        private readonly IPartnerService _partnerService;
        private readonly IMixinOrderService _mixinOrderService;
        private readonly IVoteBalanceService _voteBalanceService;

        public SubImprestService(IVoteUnitOfWork unitOfWork, IEmployeeService employeeService, IUserDetailService userDetailServiceService, IVoucherService voucherService, IMapper mapper, IPartnerService partnerService, IMixinOrderService mixinOrderService,IVoteBalanceService voteBalanceService)
        {
            _unitOfWork = unitOfWork;
            _employeeService = employeeService;
            _userDetailService = userDetailServiceService;
            _voucherService = voucherService;
            _mapper = mapper;
            _partnerService = partnerService;
            _mixinOrderService = mixinOrderService;
            _voteBalanceService = voteBalanceService;
        }

        public async Task<(bool,string?)> CreateUpdateSubImprest(SubImprest newSubImprest, HTokenClaim token)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {

                try
                {

                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                    if (session != null)
                    {


                        newSubImprest.Date = session.StartAt;
                        newSubImprest.CreatedAt = session.StartAt;
                        newSubImprest.SabhaId = token.sabhaId;
                        newSubImprest.OfficeId = token.officeId;
                        newSubImprest.CreatedBy = token.userId;
                        newSubImprest.SystemCreateAt = DateTime.Now;
                        await _unitOfWork.SubImprests.AddAsync(newSubImprest);

                        await _unitOfWork.CommitAsync();

                        if (!newSubImprest.IsOpenBalance)
                        {

                            var response = await _voucherService.CreateSubImprestVoucher(newSubImprest, true, session, token);

                            if (response.Item1)
                            {
                                newSubImprest.VoucherId = response.Item3.Id!.Value;
                                await _unitOfWork.CommitAsync();
                                transaction.Commit();

                                return (true,"Save Imprest Successfully With Voucher");
                            }
                            else
                            {
                                throw new FinalAccountException(response.Item2);
                            }
                        }
                        else
                        {
                            var response = await _voucherService.CreateSubImprestVoucher(newSubImprest, false, session, token);

                            if (response.Item1)
                            {
                                newSubImprest.VoucherId = response.Item3.Id!.Value;
                                await _unitOfWork.CommitAsync();
                                transaction.Commit();

                                return (true, "Save Imprest Successfully With Voucher");
                            }
                            else
                            {
                                throw new FinalAccountException(response.Item2);
                            }

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

        public async Task<(int totalCount, IEnumerable<SubImprestResource> list)> getAllSubImprestForSabha(int sabhaId, int? officerId, int? subImprestVoteId, int pageNo, int pageSize, string? filterKeyword,int? state)
        {
            var subImprests = await _unitOfWork.SubImprests.getAllSubImprestForSabha(sabhaId, officerId, subImprestVoteId, pageNo, pageSize, filterKeyword,state);

            var subImprestsRescouce = _mapper.Map<IEnumerable<SubImprest>, IEnumerable<SubImprestResource>>(subImprests.list);

            foreach (var subImprest in subImprestsRescouce)
            {
                subImprest.Employee = _mapper.Map<Employee, FinalEmployeeResource>(await _employeeService.GetEmployeeById(subImprest.EmployeeId!.Value));
            }

            foreach (var subImprest in subImprestsRescouce)
            {
                if (subImprest.VoucherId.HasValue)
                {
                    subImprest.Voucher = _mapper.Map<Voucher, VoucherResource>(await _unitOfWork.Voucher.getVoucherById(subImprest.VoucherId!.Value));
                }
            }

            return (subImprests.totalCount, subImprestsRescouce);
        }

        public async Task<(int totalCount, IEnumerable<SubImprestResource> list)> getAllSubImprestToSettleForSabha(int sabhaId, int? officerId, int? subImprestVoteId, int pageNo, int pageSize, string? filterKeyword, int? state,int? status)
        {
            var subImprests = await _unitOfWork.SubImprests.getAllSubImprestToSettleForSabha(sabhaId, officerId, subImprestVoteId, pageNo, pageSize, filterKeyword, state, status);

            var subImprestsRescouce = _mapper.Map<IEnumerable<SubImprest>, IEnumerable<SubImprestResource>>(subImprests.list);

            foreach (var subImprest in subImprestsRescouce)
            {
                subImprest.Employee = _mapper.Map<Employee, FinalEmployeeResource>(await _employeeService.GetEmployeeById(subImprest.EmployeeId!.Value));
            }

            foreach (var subImprest in subImprestsRescouce)
            {
                subImprest.Voucher = _mapper.Map<Voucher, VoucherResource>(await _unitOfWork.Voucher.getVoucherById(subImprest.VoucherId!.Value));
            }

            return (subImprests.totalCount, subImprestsRescouce);
        }

        public async Task<SubImprestResource> GetSubImprestById(int subImprestId)
        {
            var subImprestsRescouce = _mapper.Map<SubImprest, SubImprestResource>(await _unitOfWork.SubImprests.GetSubImprestById(subImprestId));


            if(subImprestsRescouce.EmployeeId.HasValue)
            {
                subImprestsRescouce.Employee = _mapper.Map<Employee, FinalEmployeeResource>(await _employeeService.GetEmployeeById(subImprestsRescouce.EmployeeId.Value));
            }

            return subImprestsRescouce;
        }

        public async Task<(bool, string)> SettlementSubImprest(SubImprest newSubImprest, HTokenClaim token)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {

                try
                {

                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                    if (session != null)
                    {
                        var imprest = await _unitOfWork.SubImprests.GetByIdAsync(newSubImprest.Id);

                        if (imprest != null)
                        {
                            foreach (var item in newSubImprest.SubImprestSettlements)
                            {
                                item.SystemCreateAt = DateTime.Now;
                                item.CreatedAt = session.StartAt;
                                imprest.SettleByBills += item.Amount;
                            }

                            imprest.SubImprestSettlements = newSubImprest.SubImprestSettlements;
                            imprest.Status = 3;
                            imprest.UpdatedAt = session.StartAt;
                            imprest.UpdatedBy = token.userId;
                            imprest.SystemUpdateAt = DateTime.Now;  
                                           
                            
                            //if( imprest.SettleByCash!=0m && imprest.SettleByBills + imprest.SettleByCash > imprest.Amount)
                            //{
                            //    throw new FinalAccountException("Settlement Amount Cannot Exceed Imprest Amount");
                            //}

                            
                            await _unitOfWork.CommitAsync();

                            var response = await _voucherService.CreateSettlementVoucher(imprest, session, token);

                            if (response.Item1)
                            {
                                imprest.SettlementVoucherId = response.Item3.Id!.Value;
                                await _unitOfWork.CommitAsync();
                                transaction.Commit();

                                return (true, "Settlement Submitted Successfully");
                            }
                            else
                            {
                                throw new Exception(response.Item2);
                            }


                        }
                        else
                        {
                            throw new Exception("No Sub Imprest Found");
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
                    return (false, ex.Message);
                }
            }
        }

        public async Task<(bool, string,MixinOrder)> SettlementSubImprestByCash(SubImprest newSubImprest, HTokenClaim token)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {

                try
                {

                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                    if (session != null)
                    {
                        var imprest = await _unitOfWork.SubImprests.GetByIdAsync(newSubImprest.Id);

                        if (imprest != null)
                        {
                            imprest.SubImprestSettlements = newSubImprest.SubImprestSettlements;
                            //imprest.Status = 2;



                            var voteAssignment = await _unitOfWork.VoteAssignments.GetByVoteId(imprest.SubImprestVoteId,token);

                            if(voteAssignment == null)
                            {
                                throw new FinalAccountException("No Vote Assignment Found");
                            }

                            var voteAssignmentDetail = await _unitOfWork.CustomVoteDetails.GetByAssignmentId(voteAssignment.Id);

                            if (voteAssignmentDetail == null)
                            {
                                throw new FinalAccountException("No Vote Assignment Detail Found");
                            }

                            //var employee = await _employeeService.GetEmployeeById(imprest.EmployeeId);
                            var employee = _mapper.Map<Employee, FinalEmployeeResource>(await _employeeService.GetEmployeeById(imprest.EmployeeId));

                            var newCrossOrder = new MixinOrder
                            {
                                OfficeId = token.officeId,
                                CreatedBy = token.userId,
                                CashierId = token.userId,
                                PaymentMethodId = 1,
                                AccountDetailId = voteAssignment.BankAccountId,
                                TotalAmount = (decimal)newSubImprest.SettleByCash!,
                                AppCategoryId = 6,

                                CustomerNicNumber = employee.NICNumber,
                                CustomerMobileNumber = employee.MobileNo!,
                                CustomerName = employee.EmployeeName!,
                                EmployeeId = employee.Id,
                                MixinOrderLine = new List<MixinOrderLine>()

                            };

                            var orderLine = new MixinOrderLine
                            {
                                CustomVoteName = voteAssignmentDetail.CustomVoteName,
                                Amount = (decimal)newSubImprest.SettleByCash!,
                                Description = imprest.Description,
                                MixinVoteAssignmentDetailId = voteAssignmentDetail.Id,
                                VoteDetailId = imprest.SubImprestVoteId,

                            };

                            newCrossOrder.MixinOrderLine.Add(orderLine);


                            var sessionDate = await _unitOfWork.Sessions.IsRescueSessionThenDate(session.Id);

                            if (sessionDate.HasValue)
                            {
                                newCrossOrder.CreatedAt = (DateTime)sessionDate;

                                foreach (var item in newCrossOrder!.MixinOrderLine)
                                {
                                    item.CreatedAt = (DateTime)sessionDate;
                                }
                            }
                            else
                            {
                                newCrossOrder.CreatedAt = DateTime.Now;

                                foreach (var item in newCrossOrder!.MixinOrderLine)
                                {
                                    item.CreatedAt = DateTime.Now;
                                }
                            }


                            newCrossOrder.State = OrderStatus.Paid;
                            var office = await _unitOfWork.Offices.GetByIdAsync(newCrossOrder.OfficeId.Value);

                            var crossOrderdocSeqNums = await _unitOfWork.DocumentSequenceNumbers.GetNextSequenceNumberForYearOfficePrefixAsync(newCrossOrder.CreatedAt.Year, newCrossOrder.OfficeId.Value, "MIX");
                            newCrossOrder.Code = $"{office.Code}/{DateTime.Now.Year}/{"MIX"}/{++crossOrderdocSeqNums.LastIndex}";

                            await _unitOfWork.MixinOrders.AddAsync(newCrossOrder);
                            await _unitOfWork.CommitAsync();


                            var newCrossSettlement = new SettlementCrossOrder
                            {
                                Amount = (decimal)newSubImprest.SettleByCash!,
                                SubImprestId = imprest.Id,
                                SettlementCrossId = newCrossOrder.Id,


                            };

                            imprest.SettleByCash += newSubImprest.SettleByCash;

                            imprest.SettlementCrossOrders = new List<SettlementCrossOrder> { newCrossSettlement };
                            //imprest.SettlementCrossOrders.Add(newCrossSettlement);

                            await _unitOfWork.CommitAsync();
                            if (await UpdateVoteBalance(newCrossOrder, session, CashBookTransactionType.DEBIT, CashBookIncomeCategory.CashSettlement, token))
                            {
                                await _unitOfWork.CommitAsync();
                            }
                            else
                            {
                                throw new Exception("Vote balance not updated.");
                            }

                            if (await CreateCashBookEntry(newCrossOrder, CashBookTransactionType.DEBIT, CashBookIncomeCategory.CashSettlement, session, token))
                            {
                                await _unitOfWork.CommitAsync();
                            }
                            else
                            {    
                                throw new FinalAccountException("Cashbook entry not created.");
                            }


                            transaction.Commit();
                            return (true, "Cash Settlement Successful", newCrossOrder);


                        }
                        else
                        {
                            throw new FinalAccountException("No Sub Imprest Found");
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
                        return (false, ex.Message,new MixinOrder());
                    }
                    else
                    {
                        return (false, null,new MixinOrder());
                    }
                }
            }
        }

        //private async Task<bool> CreateCashBookEntry(MixinOrder mx, Session session, CashBookTransactionType transactionType, CashBookIncomeCategory incomeCategory, HTokenClaim token)
        //{

        //    try
        //    {
        //        var account = await _unitOfWork.AccountDetails.GetByIdAsync(mx.AccountDetailId!.Value);

        //        if(!account.VoteId.HasValue)
        //        {
        //           throw new FinalAccountException($"Unable to find vote For Bank Account {account.AccountNo}");
        //        }
        //        //var session = await _unitOfWork.Sessions.GetByIdAsync(mx.SessionId);

        //        account.RunningBalance += mx.TotalAmount;

        //        var cashbook = new CashBook
        //        {
        //            //Id
        //            SabhaId = token.sabhaId,
        //            OfiiceId = mx.OfficeId!.Value,
        //            SessionId = mx.SessionId,
        //            BankAccountId = mx.AccountDetailId.Value,
        //            Date = mx.CreatedAt,

        //            TransactionType = transactionType,
        //            IncomeCategory = incomeCategory,
        //            Code = mx.Code,
        //            IncomeItemId = mx.Id,


        //            CreatedAt = DateTime.Now,
        //            CreatedBy = token.userId,
        //            RunningTotal = account.RunningBalance,


        //        };


        //        var voteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(account.VoteId!.Value, token.sabhaId, session.StartAt.Year);

        //        if (voteBalance != null)
        //        {
        //            voteBalance.TransactionType = VoteBalanceTransactionTypes.CreateCheque;
        //            voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;
        //            var vtb = new VoteLedgerBook
        //            {
        //                SabhaId = token.sabhaId,
        //                OfiiceId = token.officeId,
        //                SessionId = session.Id,
        //                Description = mx.Code,
        //                Date = session.StartAt,
        //                VoteBalanceId = (int)voteBalance.Id!,
        //                VoteDetailId = (int)voteBalance.VoteDetailId!,
        //                TransactionType = CashBookTransactionType.CREDIT,
        //                VoteBalanceTransactionTypes = VoteBalanceTransactionTypes.CreateCheque,
        //                Credit = 0,
        //                Debit = voteBalance.ExchangedAmount,
        //                RunningTotal = voteBalance.CreditDebitRunningBalance,
        //                RowStatus = 1,
        //                CreatedAt = session.StartAt,
        //                UpdatedAt = session.StartAt,
        //                CreatedBy = token.userId,
        //                UpdatedBy = token.userId,
        //                SystemActionDate = DateTime.Now
        //            };

        //            await _unitOfWork.VoteLedgerBook.AddAsync(vtb);
        //            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalance);
        //            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
        //        }
        //        else
        //        {
        //            throw new Exception("Unable to find Vote Balance");
        //        }

        //        //1 = cash
        //        //2 = cheque
        //        //3 cross
        //        //4 = direct
        //        if (mx.PaymentMethodId == 1)
        //        {
        //            cashbook.CashAmount = mx.TotalAmount;
        //            cashbook.IncomeExpenseMethod = CashBookIncomeExpenseMethod.Cash;
        //        }
        //        else if (mx.PaymentMethodId == 2)
        //        {
        //            cashbook.ChequeAmount = mx.TotalAmount;
        //            cashbook.IncomeExpenseMethod = CashBookIncomeExpenseMethod.Cheque;
        //            cashbook.ChequeNo = mx.ChequeNumber;

        //        }
        //        else if (mx.PaymentMethodId == 3)
        //        {
        //            cashbook.CrossAmount = mx.TotalAmount;
        //            cashbook.IncomeExpenseMethod = CashBookIncomeExpenseMethod.Cross;
        //        }
        //        else if (mx.PaymentMethodId == 4)
        //        {
        //            cashbook.DirectAmount = mx.TotalAmount;
        //            cashbook.IncomeExpenseMethod = CashBookIncomeExpenseMethod.Direct;
        //        }


        //        await _unitOfWork.CashBook.AddAsync(cashbook);

        //        return true;

        //    }
        //    catch (Exception ex)
        //    {
        //        if(ex.GetType() == typeof(FinalAccountException))
        //        {
        //            throw new FinalAccountException(ex.Message);
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }



        //}


        private async Task<bool> UpdateVoteBalance(MixinOrder mx, Session session, CashBookTransactionType transactionType, CashBookIncomeCategory incomeCategory, HTokenClaim token)

        {
            try
            {



                foreach (var item in mx.MixinOrderLine)
                {


                    if (item.StampAmount > 0)
                    {
                        var stampLedgerAccount = await _unitOfWork.SpecialLedgerAccounts.GetStampLedgerAccount(token.sabhaId);

                        var stampVoteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(stampLedgerAccount.VoteId!.Value, token.sabhaId, session.StartAt.Year);

                        if (stampVoteBalance == null)
                        {
                            stampVoteBalance = await _voteBalanceService.CreateNewVoteBalance(stampLedgerAccount.VoteId!.Value, session, token);

                            if (stampVoteBalance == null)
                            {
                                throw new FinalAccountException("Unable to Create Stamp Ledger Account Entry");
                            }


                        }


                        if (stampVoteBalance != null && stampVoteBalance.Id.HasValue)
                        {
                            stampVoteBalance.UpdatedAt = session.StartAt;
                            stampVoteBalance.UpdatedBy = token.userId;
                            stampVoteBalance.SystemActionAt = DateTime.Now;
                            stampVoteBalance.OfficeId = token.officeId;
                            stampVoteBalance.ExchangedAmount = (decimal)item.StampAmount!;
                            stampVoteBalance.SessionIdByOffice = session.Id;


                            if (transactionType == CashBookTransactionType.DEBIT)
                            {
                                stampVoteBalance.TransactionType = VoteBalanceTransactionTypes.Income;
                                stampVoteBalance.SubCode = mx.Code;


                                if (stampVoteBalance.ClassificationId == 1)
                                {
                                    stampVoteBalance.Debit += (decimal)item.Amount!;
                                    stampVoteBalance.CreditDebitRunningBalance = stampVoteBalance.Debit - stampVoteBalance.Credit;

                                }
                                else if (stampVoteBalance.ClassificationId == 2)
                                {
                                    stampVoteBalance.Credit += (decimal)item.Amount!;
                                    stampVoteBalance.CreditDebitRunningBalance = stampVoteBalance.Debit - stampVoteBalance.Credit;

                                }
                                else if (stampVoteBalance.ClassificationId == 3)
                                {
                                    stampVoteBalance.Credit += (decimal)item.Amount!;
                                    stampVoteBalance.CreditDebitRunningBalance = stampVoteBalance.Debit - stampVoteBalance.Credit;

                                }
                                else if (stampVoteBalance.ClassificationId == 4)
                                {
                                    stampVoteBalance.Credit += (decimal)item.Amount!;
                                    stampVoteBalance.CreditDebitRunningBalance = stampVoteBalance.Credit - stampVoteBalance.Debit;

                                }
                                else if (stampVoteBalance.ClassificationId == 5)
                                {
                                    stampVoteBalance.Debit += (decimal)item.Amount!;
                                    stampVoteBalance.CreditDebitRunningBalance = stampVoteBalance.Debit - stampVoteBalance.Credit;

                                }
                                else
                                {
                                    throw new FinalAccountException("Ledger Account Classification Not Found.");
                                }

                            }



                            else if (transactionType == CashBookTransactionType.CREDIT)
                            {
                                stampVoteBalance.TransactionType = VoteBalanceTransactionTypes.ReverseIncome;

                                if (stampVoteBalance.ClassificationId == 1)
                                {
                                    stampVoteBalance.Credit += (decimal)item.Amount!;
                                    stampVoteBalance.CreditDebitRunningBalance = stampVoteBalance.Debit - stampVoteBalance.Credit;

                                }
                                else if (stampVoteBalance.ClassificationId == 2)
                                {
                                    stampVoteBalance.Debit += (decimal)item.Amount!;
                                    stampVoteBalance.CreditDebitRunningBalance = stampVoteBalance.Debit - stampVoteBalance.Credit;

                                }
                                else if (stampVoteBalance.ClassificationId == 3)
                                {
                                    stampVoteBalance.Debit += (decimal)item.Amount!;
                                    stampVoteBalance.CreditDebitRunningBalance = stampVoteBalance.Debit - stampVoteBalance.Credit;

                                }
                                else if (stampVoteBalance.ClassificationId == 4)
                                {
                                    stampVoteBalance.Debit += (decimal)item.Amount!;
                                    stampVoteBalance.CreditDebitRunningBalance = stampVoteBalance.Credit - stampVoteBalance.Debit;

                                }
                                else if (stampVoteBalance.ClassificationId == 5)
                                {
                                    stampVoteBalance.Credit += (decimal)item.Amount!;
                                    stampVoteBalance.CreditDebitRunningBalance = stampVoteBalance.Debit - stampVoteBalance.Credit;

                                }
                                else
                                {
                                    throw new FinalAccountException("Ledger Account Classification Not Found.");
                                }
                            }




                        }
                        else
                        {
                            throw new FinalAccountException("Vote stamp ledger balance not found.");
                        }
                    }


                    var voteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(item.VoteDetailId!.Value, token.sabhaId, session.StartAt.Year);

                    if (voteBalance == null)
                    {
                        voteBalance = await _voteBalanceService.CreateNewVoteBalance(item.VoteDetailId!.Value, session, token);

                        if (voteBalance == null)
                        {
                            throw new FinalAccountException("Unable to Create Vote Balance Entry");
                        }


                    }


                    if (voteBalance != null && voteBalance.Id.HasValue)
                    {
                        voteBalance.UpdatedAt = session.StartAt;
                        voteBalance.UpdatedBy = token.userId;


                        if (transactionType == CashBookTransactionType.DEBIT)
                        {
                            voteBalance.TransactionType = VoteBalanceTransactionTypes.Income;
                            voteBalance.SubCode = mx.Code;


                            if (voteBalance.ClassificationId == 1)
                            {
                                voteBalance.Credit += (decimal)item.Amount!;
                                voteBalance.CreditDebitRunningBalance = voteBalance.Credit - voteBalance.Debit;

                            }
                            else if (voteBalance.ClassificationId == 2)
                            {
                                voteBalance.Credit += (decimal)item.Amount!;
                                voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;

                            }
                            else if (voteBalance.ClassificationId == 3)
                            {
                                voteBalance.Credit += (decimal)item.Amount!;
                                voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;

                            }
                            else if (voteBalance.ClassificationId == 4)
                            {
                                voteBalance.Credit += (decimal)item.Amount!;
                                voteBalance.CreditDebitRunningBalance = voteBalance.Credit - voteBalance.Debit;

                            }
                            else if (voteBalance.ClassificationId == 5)
                            {
                                voteBalance.Debit += (decimal)item.Amount!;
                                voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;

                            }
                            else
                            {
                                throw new FinalAccountException("Ledger Account Classification Not Found.");
                            }

                        }



                        else if (transactionType == CashBookTransactionType.CREDIT)
                        {
                            voteBalance.TransactionType = VoteBalanceTransactionTypes.ReverseIncome;

                            if (voteBalance.ClassificationId == 1)
                            {
                                voteBalance.Debit += (decimal)item.Amount!;
                                voteBalance.CreditDebitRunningBalance = voteBalance.Credit - voteBalance.Debit;

                            }
                            else if (voteBalance.ClassificationId == 2)
                            {
                                voteBalance.Debit += (decimal)item.Amount!;
                                voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;

                            }
                            else if (voteBalance.ClassificationId == 3)
                            {
                                voteBalance.Debit += (decimal)item.Amount!;
                                voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;

                            }
                            else if (voteBalance.ClassificationId == 4)
                            {
                                voteBalance.Debit += (decimal)item.Amount!;
                                voteBalance.CreditDebitRunningBalance = voteBalance.Credit - voteBalance.Debit;

                            }
                            else if (voteBalance.ClassificationId == 5)
                            {
                                voteBalance.Credit += (decimal)item.Amount!;
                                voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;

                            }
                            else
                            {
                                throw new FinalAccountException("Ledger Account Classification Not Found.");
                            }
                        }


                        if (voteBalance.ClassificationId == 2)
                        {
                            voteBalance.RunningBalance = (decimal)voteBalance.AllocationAmount! + voteBalance.Debit - voteBalance.Credit;
                        }

                        voteBalance.ExchangedAmount = (decimal)item.Amount!;

                        voteBalance.OfficeId = token.officeId;
                        voteBalance.UpdatedBy = token.userId;
                        voteBalance.UpdatedAt = session.StartAt;
                        voteBalance.SystemActionAt = DateTime.Now;

                        var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalance);
                        await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);


                        if (transactionType == CashBookTransactionType.DEBIT && item.ClassificationId == 4)
                        {



                            var IncomeSubtitleId = await _unitOfWork.VoteDetails.IsDepositVote(voteBalance.VoteDetailId.Value);

                            if (IncomeSubtitleId.HasValue)
                            {

                                var newDeposit = new Deposit
                                {
                                    DepositSubCategoryId = IncomeSubtitleId.Value,
                                    InitialDepositAmount = (decimal)item.Amount!,
                                    DepositDate = session.StartAt,
                                    Description = item.Description,
                                    CreatedAt = session.StartAt,
                                    CreatedBy = token.userId,
                                    SabhaId = token.sabhaId,
                                    OfficeId = token.officeId,
                                    LedgerAccountId = voteBalance.VoteDetailId.Value!,
                                    MixOrderId = mx.Id,
                                    MixOrderLineId = item.Id,
                                    ReceiptNo = mx.Code,
                                    IsEditable = false,
                                    PartnerId = mx.PartnerId!.Value,
                                    SystemCreateAt = DateTime.Now,

                                };

                                await _unitOfWork.Deposits.AddAsync(newDeposit);

                            }
                        }

                        //else if(transactionType == CashBookTransactionType.CREDIT)
                        //{
                        //    var IncomeSubtitleId = await _unitOfWork.VoteDetails.IsDepositVote(voteBalance.VoteDetailId.Value);

                        //    if (IncomeSubtitleId.HasValue)
                        //    {



                        //    }
                        //}


                    }
                    else
                    {
                        throw new FinalAccountException("Vote balance not found.");
                    }



                }

                if (transactionType == CashBookTransactionType.DEBIT)
                {
                    var depostis = await _unitOfWork.Deposits.ClearDepots(mx.Id);

                    foreach (var item in depostis)
                    {
                        item.Status = 0;
                        item.UpdatedAt = session.StartAt;
                        item.UpdatedBy = token.userId;
                        item.SystemUpdateAt = DateTime.Now;

                    }
                }



                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        private async Task<bool> CreateCashBookEntry(MixinOrder mx, CashBookTransactionType transactionType, CashBookIncomeCategory incomeCategory, Session session, HTokenClaim token)
        {

            try
            {
                var account = await _unitOfWork.AccountDetails.GetByIdAsync(mx.AccountDetailId!.Value);
                //var session = await _unitOfWork.Sessions.GetByIdAsync(mx.SessionId);

                if (account == null)
                {
                    throw new FinalAccountException("Account not found.");
                }

                if (transactionType == CashBookTransactionType.DEBIT)
                {
                    account.RunningBalance += mx.TotalAmount;
                }
                else if (transactionType == CashBookTransactionType.CREDIT)
                {

                    account.RunningBalance -= mx.TotalAmount;
                }
                var cashbook = new CashBook
                {
                    //Id
                    SabhaId = token.sabhaId,
                    OfiiceId = mx.OfficeId!.Value,
                    SessionId = mx.SessionId,
                    BankAccountId = mx.AccountDetailId.Value,
                    Date = mx.CreatedAt,

                    TransactionType = transactionType,
                    IncomeCategory = incomeCategory,
                    Code = mx.Code,
                    IncomeItemId = mx.Id,


                    CreatedAt = DateTime.Now,
                    CreatedBy = token.userId,
                    RunningTotal = account.RunningBalance,


                };

                var voteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(account.VoteId!.Value, token.sabhaId, session.StartAt.Year);

                if (voteBalance != null)
                {

                    if (transactionType == CashBookTransactionType.DEBIT)
                    {
                        voteBalance.TransactionType = VoteBalanceTransactionTypes.Income;
                        voteBalance.Debit += mx.TotalAmount;
                        voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;
                    }
                    else if (transactionType == CashBookTransactionType.CREDIT)
                    {
                        voteBalance.TransactionType = VoteBalanceTransactionTypes.ReverseIncome;
                        voteBalance.Credit += mx.TotalAmount;
                        voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;
                    }


                    voteBalance.ExchangedAmount = mx.TotalAmount;

                    voteBalance.OfficeId = token.officeId;
                    var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalance);
                    await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                }
                else
                {
                    throw new Exception("Unable to Find Vote Balance");
                }



                //1 = cash
                //2 = cheque
                //3 cross
                //4 = direct
                if (mx.PaymentMethodId == 1)
                {
                    cashbook.CashAmount = mx.TotalAmount;
                    cashbook.IncomeExpenseMethod = CashBookIncomeExpenseMethod.Cash;
                }
                else if (mx.PaymentMethodId == 2)
                {
                    cashbook.ChequeAmount = mx.TotalAmount;
                    cashbook.IncomeExpenseMethod = CashBookIncomeExpenseMethod.Cheque;
                    cashbook.ChequeNo = mx.ChequeNumber;

                }
                else if (mx.PaymentMethodId == 3)
                {
                    cashbook.CrossAmount = mx.TotalAmount;
                    cashbook.IncomeExpenseMethod = CashBookIncomeExpenseMethod.Cross;
                }
                else if (mx.PaymentMethodId == 4)
                {
                    cashbook.DirectAmount = mx.TotalAmount;
                    cashbook.IncomeExpenseMethod = CashBookIncomeExpenseMethod.Direct;
                }


                await _unitOfWork.CashBook.AddAsync(cashbook);

                return true;

            }
            catch (Exception ex)
            {
                throw;
            }


        }

    }
}

