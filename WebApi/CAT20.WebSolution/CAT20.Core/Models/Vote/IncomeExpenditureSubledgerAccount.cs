using CAT20.Core.Models.HRM.LoanManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CAT20.Core.Models.Vote
{
    public partial class IncomeExpenditureSubledgerAccount
    {
        public int Id { get; set; }
        public string NameSinhala { get; set; }
        public string NameEnglish { get; set; }
        public string NameTamil { get; set; }
        public string Code { get; set; }
        public int? IncomeExpenditureLedgerAccountId { get; set; }
        public int COAVersionId { get; set; }
        public int StatusID { get; set; }

        // Navigation properties
        [JsonIgnore]
        public virtual IncomeSubtitle? IncomeSubtitles { get; set; }
    }
}
