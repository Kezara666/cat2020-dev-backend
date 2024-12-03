using System;
using System.Collections.Generic;

namespace CAT20.WebApi.Resources.Vote
{
    public partial class BalancesheetTitleResource
    {
        //public BalancesheetTitleResource()
        //{
        //    balancesheetSubtitle = new HashSet<BalancesheetSubtitleResource>();
        //}

        public int ID { get; set; }
        public string NameSinhala { get; set; }
        public string NameEnglish { get; set; }
        public string NameTamil { get; set; }
        public string Code { get; set; }
        public int? Balpath { get; set; }
        public int? Status { get; set; }
        public int? SabhaID { get; set; }
        public int? ClassificationID { get; set; }
        public int? MainLedgerAccountID { get; set; }

        public virtual ICollection<BalancesheetSubtitleResource>? balancesheetSubtitle { get; set; }
    }
}