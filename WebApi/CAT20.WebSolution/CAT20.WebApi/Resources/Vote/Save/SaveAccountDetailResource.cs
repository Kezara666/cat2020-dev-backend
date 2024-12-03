using System;
using System.Collections.Generic;

namespace CAT20.WebApi.Resources.Vote.Save
{
    public partial class SaveAccountDetailResource
    {
        //public SaveAccountDetailResource()
        //{
        //    accountBalDetail = new HashSet<SaveAccountBalanceDetailResource>();
        //}
        public int ID { get; set; }
        public string AccountNo { get; set; }
        public string NameSinhala { get; set; }
        public string NameEnglish { get; set; }
        public string NameTamil { get; set; }
        public int? BankID { get; set; }
        public int? Status { get; set; }
        public int? OfficeID { get; set; }
        public string? BankCode { get; set; }
        public string? BranchCode { get; set; }

        //public virtual ICollection<SaveAccountBalanceDetailResource> accountBalDetail { get; set; }
    }
}