using System;
using System.Collections.Generic;
using CAT20.Core.Models.Vote;
using System.ComponentModel.DataAnnotations;
using CAT20.Core.Models.HRM.LoanManagement;
using CAT20.Core.Models.HRM.PersonalFile;

namespace CAT20.Core.Models.Vote
{
    public partial class IncomeSubtitle
    {
        [Key]
        public int ID { get; set; }
        public string NameSinhala { get; set; }
        public string NameEnglish { get; set; }
        public string NameTamil { get; set; }
        public string Code { get; set; }
        public int? IncomeTitleID { get; set; }
        public int? Status { get; set; }
        public int? SabhaID { get; set; }
        public int? ProgrammeID { get; set; }


        //public IncomeSubtitle()
        //{
        //    voteDetail = new HashSet<VoteDetail>();
        //}
        //public virtual ICollection<VoteDetail> voteDetail { get; set; }


        // Navigation properties
        public virtual IncomeTitle? incomeTitle { get; set; }
        public ICollection<IncomeExpenditureSubledgerAccount>? SubLedgerAccounts { get; set; }
    }
}