using System;
using System.Collections.Generic;

namespace CAT20.Core.DTO.Final
{
    public partial class AccountDetailOnlyBankId
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
 
    }
}