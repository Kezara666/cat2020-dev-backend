using System;
using System.Collections.Generic;

namespace CAT20.WebApi.Resources.Vote.Save
{
    public partial class SaveBalancesheetBalanceResource
    {
        public int ID { get; set; }
        public int VoteDetailID { get; set; }
        public int? Year { get; set; }
        public double Balance { get; set; }
        public string Comment { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int SabhaID { get; set; }
        public int? Status { get; set; }
    }
}