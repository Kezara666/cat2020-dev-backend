using CAT20.Core.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.FinalAccount
{
    public class CashBook
    {

        public int Id { get; set; }
        public int SabhaId { get; set; }
        public int OfiiceId { get; set; }
        public int SessionId { get; set; }
        public int BankAccountId { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }

        public CashBookTransactionType TransactionType { get; set; }
        public CashBookIncomeExpenseMethod IncomeExpenseMethod { get; set; } 
     
        public CashBookIncomeCategory IncomeCategory { get; set; }
        public int? IncomeItemId { get; set; }
        public CashBookExpenseCategory ExpenseCategory { get; set; }
        public int? ExpenseItemId { get; set; }

        public string? ChequeNo { get; set; }
        public string? IncomeHeadsAsString { get; set; }
        public string? ExpenseHeadsAsString { get; set; }
        public string? Code { get; set; }
        public string? SubCode { get; set; }

        [Precision(20, 2)]
        public decimal CashAmount { get; set; }
        [Precision(20, 2)]
        public decimal ChequeAmount { get; set; }
        [Precision(20, 2)]
        public decimal DirectAmount { get; set; }
        [Precision(20, 2)]
        public decimal CrossAmount { get; set; }
        [Precision(20, 2)]
        public decimal RunningTotal { get; set; }

        // mandatory fields
        public int? RowStatus { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime SystemAt { get; set; }

        [ConcurrencyCheck]
        [Timestamp]
        public byte[] RowVersion { get; set; }

    }
}
