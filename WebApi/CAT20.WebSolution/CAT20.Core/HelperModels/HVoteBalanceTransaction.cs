using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.HelperModels
{
    public class HVoteBalanceTransaction
    {
        public int VoteDetailId { get; set; }
        public decimal Amount { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
        public string? Code { get; set; }
        public string? SubCode { get; set; }
        public FAMainTransactionMethod TransactionMethod { get; set; }
        public VoteBalanceTransactionTypes TransactionType { get; set; }
        public Session Session { get; set; }
        public string NotFoundExceptionMessage { get; set; }
    }
}
