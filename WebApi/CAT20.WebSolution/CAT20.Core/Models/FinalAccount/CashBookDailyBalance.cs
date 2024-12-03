using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.FinalAccount
{
    public class CashBookDailyBalance
    {

        public int Id { get; set; }
        public int SabhaId { get; set; }
        public int OfficeId { get; set; }
        public int SessionId { get; set; }
        public DateTime Date { get; set; }
        public int BankAccountId { get; set; }

        [Precision(20, 2)]
        public decimal IncomeTotalCashAmount { get; set; }
        [Precision(20, 2)]
        public decimal IncomeTotalChequeAmount { get; set; }
        [Precision(20, 2)]
        public decimal IncomeTotalDirectAmount { get; set; }
        [Precision(20, 2)]
        public decimal IncomeTotalCrossAmount { get; set; }
        [Precision(20, 2)]
        public decimal IncomeTotalAmount { get; set; }


        [Precision(20, 2)]
        public decimal ExpenseTotalCashAmount { get; set; }
        [Precision(20, 2)]
        public decimal ExpenseTotalChequeAmount { get; set; }
        [Precision(20, 2)]
        public decimal ExpenseTotalDirectAmount { get; set; }
        [Precision(20, 2)]
        public decimal ExpenseTotalCrossAmount { get; set; }
        [Precision(20, 2)]
        public decimal ExpenseTotalAmount { get; set; }

        // mandatory fields
        public int? RowStatus { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        [ConcurrencyCheck]
        [Timestamp]
        public byte[] RowVersion { get; set; }  
    }
}
