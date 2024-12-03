using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Repositories.FinalAccount;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.FinalAccount
{
    public class CashBookRepository : Repository<CashBook>, ICashBookRepository
    {
        public async Task<IEnumerable<CashBookDailyBalance>> GetDailyBalancesAsync(int officeId, int sessionId, DateTime date, int createdby)
        {
            return await voteAccDbContext.CashBook
            .Where(cb => cb.OfiiceId == officeId && cb.SessionId == sessionId && cb.Date.Date == date.Date.Date)
            .GroupBy(cb => new { cb.SabhaId, cb.OfiiceId, cb.SessionId, cb.Date.Date, cb.BankAccountId })
            .Select(group => new CashBookDailyBalance
            {
                SabhaId = group.Key.SabhaId,
                OfficeId = group.Key.OfiiceId,
                SessionId = group.Key.SessionId,
                Date = group.Key.Date.Date,
                BankAccountId = group.Key.BankAccountId,
                CreatedBy = createdby,
                CreatedAt = DateTime.Now,
                IncomeTotalCashAmount = group.Where(cb => cb.TransactionType == CashBookTransactionType.DEBIT).Sum(cb => cb.CashAmount),
                IncomeTotalChequeAmount = group.Where(cb => cb.TransactionType == CashBookTransactionType.DEBIT).Sum(cb => cb.ChequeAmount),
                IncomeTotalDirectAmount = group.Where(cb => cb.TransactionType == CashBookTransactionType.DEBIT).Sum(cb => cb.DirectAmount),
                IncomeTotalCrossAmount = group.Where(cb => cb.TransactionType == CashBookTransactionType.DEBIT).Sum(cb => cb.CrossAmount),
                IncomeTotalAmount = group.Where(cb => cb.TransactionType == CashBookTransactionType.DEBIT)
                                      .Sum(cb => cb.CashAmount + cb.ChequeAmount + cb.DirectAmount + cb.CrossAmount),
                ExpenseTotalCashAmount = group.Where(cb => cb.TransactionType == CashBookTransactionType.CREDIT).Sum(cb => cb.CashAmount),
                ExpenseTotalChequeAmount = group.Where(cb => cb.TransactionType == CashBookTransactionType.CREDIT).Sum(cb => cb.ChequeAmount),
                ExpenseTotalDirectAmount = group.Where(cb => cb.TransactionType == CashBookTransactionType.CREDIT).Sum(cb => cb.DirectAmount),
                ExpenseTotalCrossAmount = group.Where(cb => cb.TransactionType == CashBookTransactionType.CREDIT).Sum(cb => cb.CrossAmount),
                ExpenseTotalAmount = group.Where(cb => cb.TransactionType == CashBookTransactionType.CREDIT)
                                    .Sum(cb => cb.CashAmount + cb.ChequeAmount + cb.DirectAmount + cb.CrossAmount)
                })
                .ToListAsync();
        }

        public async Task<(int totalCount, IEnumerable<CashBook> list)> GetAllTransactionsForSabhaByAccount(int sabhaId, int accountId, int pageNo, int pageSize, string? filterKeyword)
        {
            if (filterKeyword != "undefined")
            {
                filterKeyword = "%" + filterKeyword + "%";
            }
            else if (filterKeyword == "undefined")
            {
                filterKeyword = null;
            }

            var keyword = filterKeyword ?? "";
           

            var result = voteAccDbContext.CashBook
                .Where(c=>c.BankAccountId == accountId)
                .Where(v => (v.RowStatus == 1 && v.SabhaId == sabhaId))
                .Where(v => EF.Functions.Like(v.Code!, "%" + filterKeyword + "%") || EF.Functions.Like(v.ChequeNo!, "%" + filterKeyword + "%") || EF.Functions.Like(v.Description!, "%" + filterKeyword + "%"))
                .OrderByDescending(v => v.Id);



            int totalCount = await result.CountAsync();


            //var pageSize = 10;
            int skipAmount = (pageNo - 1) * pageSize;

            var list = await result.Skip(skipAmount).Take(pageSize).ToListAsync();


            return (totalCount, list);
        }

        public CashBookRepository(DbContext context) : base(context)
        {
        }
        private VoteAccDbContext voteAccDbContext
        {
            get { return Context as VoteAccDbContext; }
        }
    }
}
