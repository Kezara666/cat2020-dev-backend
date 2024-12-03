using CAT20.Core.DTO.Final;
using CAT20.WebApi.Resources.Control;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace CAT20.WebApi.Resources.Vote
{
    public partial class AccountDetailResource
    {
        //public AccountDetailResource()
        //{
        //    accountBalDetail = new HashSet<AccountBalanceDetailResource>();
        //}

        public int ID { get; set; }
        public string AccountNo { get; set; }
        public string NameSinhala { get; set; }
        public string NameEnglish { get; set; }
        public string NameTamil { get; set; }
        public int? BankID { get; set; }
        public int? VoteId { get; set; }
        public int? Status { get; set; }
        public int? OfficeID { get; set; }
        public string? BankCode { get; set; }
        public string? BranchCode { get; set; }


        public virtual ICollection<AccountBalanceDetailResource>? accountBalDetail { get; set; }

        public decimal RunningBalance { get; set; }
        public decimal ExpenseHold { get; set; }

        /*mapping properties*/


        public decimal AccountBalance { get; set; } = 0;

        /*linking model*/
        public virtual BankDetailResource? BankDetail { get; set; }

        public virtual VoteDetailLimitedresource? VoteDetail { get; set; }
    }
}