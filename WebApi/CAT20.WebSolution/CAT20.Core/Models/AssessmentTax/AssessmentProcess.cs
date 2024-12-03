// Ignore Spelling: Qtr

using CAT20.Core.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace CAT20.Core.Models.AssessmentTax
{
    public partial class AssessmentProcess
    {
        [Key]
        public int Id { get; set; }
        public int? ActionBy { get; set; }
        public int Year { get; set; }
        public int ShabaId { get; set; }
        public AssessmentProcessType ProcessType { get; set; }
        public DateTime? ProceedDate { get; set; }
        public string? BackUpKey { get; set; }

    }
}
