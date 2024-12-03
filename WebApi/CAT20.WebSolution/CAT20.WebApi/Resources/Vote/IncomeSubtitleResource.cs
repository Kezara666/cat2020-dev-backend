using System;
using System.Collections.Generic;
using CAT20.Core.Models.Vote;

namespace CAT20.WebApi.Resources.Vote
{
    public partial class IncomeSubtitleResource
    {
        public int ID { get; set; }
        public string NameSinhala { get; set; }
        public string NameEnglish { get; set; }
        public string NameTamil { get; set; }
        public string Code { get; set; }
        public int? IncomeTitleID { get; set; }
        public int? Status { get; set; }
        public int? SabhaID { get; set; }
        public int? ProgrammeID { get; set; }

        public int? ClassificationID { get; set; }
        public virtual IncomeTitleResource? incomeTitle { get; set; }

        public ICollection<IncomeExpenditureSubledgerAccountResource>? SubLedgerAccounts { get; set; }
    }
}