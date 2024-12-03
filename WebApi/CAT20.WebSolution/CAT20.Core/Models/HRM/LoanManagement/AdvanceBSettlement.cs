using CAT20.Core.Models.Enums.HRM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CAT20.Core.Models.HRM.LoanManagement
{
    public partial class AdvanceBSettlement
    {
        public int Id { get; set; }
        public int AdvanceBId { get; set; }

        public AdvanceBSettelemntType? Type { get; set; }
        public string? SettlementCode { get; set; }
        public int PayMonth { get; set; }
        [Precision(18, 2)]
        public decimal? Amount { get; set; }
        [Precision(18, 2)]
        public decimal? InterestAmount { get; set; }
        [Precision(18, 2)]
        public decimal? Balance { get; set; }

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
