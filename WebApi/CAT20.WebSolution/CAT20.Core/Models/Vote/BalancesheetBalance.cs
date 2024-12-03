using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CAT20.Core.Models.Vote
{
    public partial class BalancesheetBalance
    {
        [Key]
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