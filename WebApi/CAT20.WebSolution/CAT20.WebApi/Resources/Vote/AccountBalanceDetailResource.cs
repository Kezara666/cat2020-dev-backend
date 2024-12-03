using System;
using System.Collections.Generic;

namespace CAT20.WebApi.Resources.Vote
{
    public partial class AccountBalanceDetailResource
    {
        public int ID { get; set; }
        public int AccountDetailID { get; set; }
        public int? Year { get; set; }
        public decimal BalanceAmount { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int Status { get; set; }
        public int SabhaID { get; set; }

        //public virtual AccountDetailResource accountDetail { get; set; }
    }
}
