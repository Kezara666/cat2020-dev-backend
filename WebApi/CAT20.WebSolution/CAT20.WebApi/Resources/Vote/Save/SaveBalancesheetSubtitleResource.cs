using System;
using System.Collections.Generic;

namespace CAT20.WebApi.Resources.Vote.Save
{
    public partial class SaveBalancesheetSubtitleResource
    {
        public int ID { get; set; }
        public string NameSinhala { get; set; }
        public string NameEnglish { get; set; }
        public string NameTamil { get; set; }
        public string Code { get; set; }
        public int? BalsheetTitleID { get; set; }
        public int? Status { get; set; }
        public int? SabhaID { get; set; }
        public int? BankAccountID { get; set; }

        public int? ClassificationID { get; set; }

        //public virtual SaveBalancesheetTitleResource balancesheetTitle { get; set; }
    }
}