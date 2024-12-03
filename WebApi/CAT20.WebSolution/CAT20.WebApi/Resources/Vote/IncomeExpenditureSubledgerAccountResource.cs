using CAT20.Core.Models.Vote;

namespace CAT20.WebApi.Resources.Vote
{
    public class IncomeExpenditureSubledgerAccountResource
    {
        public int Id { get; set; }
        public string NameSinhala { get; set; }
        public string NameEnglish { get; set; }
        public string NameTamil { get; set; }
        public string Code { get; set; }
        public int? IncomeExpenditureLedgerAccountId { get; set; }
        //public int COAVersionId { get; set; }
        //public int StatusID { get; set; }

        // Navigation properties
        //[JsonIgnore]
        //public virtual IncomeSubtitle? IncomeSubtitles { get; set; }
    }
}
