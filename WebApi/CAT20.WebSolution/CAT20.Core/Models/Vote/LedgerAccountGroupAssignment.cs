using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Core.Models.Vote
{
    public partial class LedgerAccountGroupAssignment
    { 
        public LedgerAccountGroupAssignment()
        {
        }

        [Key]
        public int ID { get; set; }

        [Required]
        public string SubtitleCode { get; set; }

        public string? SubtitleDescription { get; set; }

        [Required]
        public Enums.LedgerAccountGroupCategory LedgerAccountGroupCategory { get; set; }  // Enum for LedgerAccountGroupCategory

        public string? LedgerAccountGroupCategoryDescription { get; set; }

        [Required]
        public int AccountSystemVersion { get; set; } // 1 for old system, 2 for new system

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}