using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.FinalAccount
{
    public class VoteJournalAdjustment
    {

        [Key]
        public int? Id { get; set; }
        public  string JournalNo { get; set; }
        public  int SabahId { get; set; }
        public  int OfficeId { get; set; }

        public VoteJournalAdjustmentActions ActionState { get; set; }

        [Precision(18, 2)]
        public decimal FromAmount { get; set; }
        [Precision(18, 2)]
        public decimal ToAmount { get; set; }


        [Required]
        public DateTime? RequestDate { get; set; }

        [Required]
        public int? RequestBy { get; set; }

        public string? RequestNote { get; set; }


        public DateTime? ActionDate { get; set; }

        public int? ActionBy { get; set; }

        public string? ActionNote { get; set; }

        [Required]
        public DateTime? SystemRequestDate { get; set; }

        public DateTime? SystemActionDate { get; set; }


        public ICollection<VoteJournalItemFrom>? VoteJournalItemsFrom { get; set; }
        public ICollection<VoteJournalItemTo>? VoteJournalItemsTo { get; set; }


        // mandatory fields
        public int? RowStatus { get; set; }

    }
}
