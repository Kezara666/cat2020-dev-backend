using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CAT20.Core.Models.Vote
{
    public partial class BalancesheetTitle
    {
        public BalancesheetTitle()
        {
            balancesheetSubtitle = new HashSet<BalancesheetSubtitle>();
        }
        [Key]
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

        public virtual ICollection<BalancesheetSubtitle>? balancesheetSubtitle { get; set; }
    }
}