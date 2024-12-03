﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CAT20.Core.Models.Vote
{
    public partial class BalancesheetSubtitle
    {
        [Key]
        public int ID { get; set; }
        public string NameSinhala { get; set; }
        public string NameEnglish { get; set; }
        public string NameTamil { get; set; }
        public string Code { get; set; }
        public int? BalsheetTitleID { get; set; }
        public int? Status { get; set; }
        public int? SabhaID { get; set; }
        public int? BankAccountID { get; set; }



        // Navigation properties
        public virtual BalancesheetTitle balancesheetTitle { get; set; }
        public virtual AccountDetail accountDetail { get; set; }
        public ICollection<BalsheetSubledgerAccount>? SubLedgerAccounts { get; set; }
    }
}