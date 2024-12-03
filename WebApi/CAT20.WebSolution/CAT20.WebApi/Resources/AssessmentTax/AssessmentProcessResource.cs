using CAT20.WebApi.Resources.User;

namespace CAT20.WebApi.Resources.AssessmentTax
{
    public class AssessmentProcessResource
    {
        public int Id { get; set; }
        public int ActionBy { get; set; }
        public int ShabaId { get; set; }

        public int Year { get; set; }
        public string? ProcessType { get; set; }
        public DateTime? ProceedDate { get; set; }

        public UserActionByResources? UserActionBy { get; set; }

        //public DateTime? ProceedDate { get; set; }
    }
}
