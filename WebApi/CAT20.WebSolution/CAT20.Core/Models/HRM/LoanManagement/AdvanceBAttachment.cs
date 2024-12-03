using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CAT20.Core.Models.HRM.LoanManagement
{
    public partial class AdvanceBAttachment
    {
        public int Id { get; set; }
        public int LoanId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }


        // Mandatory fields
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int StatusId { get; set; }


        // Navigation property
        [JsonIgnore]
        public virtual AdvanceB? AdvanceB { get; set; }

        public DateTime? SystemActionAt { get; set; }
    }
}
