using CAT20.Core.Models.Enums.HRM;
using CAT20.Core.Models.HRM.LoanManagement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.HRM
{
    public class SaveAdvanceBSettlementResource
    {
        //public int Id { get; set; }
        //public int AdvanceBId { get; set; }

        //public AdvanceBSettelemntType? Type { get; set; }
        public string? SettlementCode { get; set; }
        //public int PayMonth { get; set; }
        public decimal? Amount { get; set; }
        public decimal? InterestAmount { get; set; }
        public decimal? Balance { get; set; }

        //// Mandatory fields
        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        //public int? CreatedBy { get; set; }
        //public int? UpdatedBy { get; set; }
        //public int StatusId { get; set; }

        // Navigation property
        //public virtual AdvanceB? AdvanceB { get; set; }

        //public DateTime? SystemActionAt { get; set; }
    }
}
