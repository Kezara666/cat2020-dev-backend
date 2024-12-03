using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.Vote
{
    public class SpecialLedgerAccounts
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? VoteCode { get; set; }
        [Required]
        public int? VoteId { get; set; }
        public virtual SpecialLedgerAccountTypes? Type { get; set; }
        public int? TypeId { get; set; }
        [Required]
        public int? SabhaId { get; set; }

        // mandatory fields
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public DateTime? SystemActionAt { get; set; }

    }
}
