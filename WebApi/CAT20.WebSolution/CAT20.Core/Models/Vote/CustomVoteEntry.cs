using CAT20.Core.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.Vote
{
    public class CustomVoteEntry
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int? EntityPrimaryId { get; set; }
        [Required]
        public VoteEntityType? EntityType { get; set; }
        public int? CustomVoteDetailIdParentId { get; set; }

        [Required]
        public int? CustomVoteDetailId { get; set; }

        [Precision(20, 2)]
        public decimal Amount { get; set; }

        public int Depth { get; set; }


        public int? ParentId { get; set; }
        public bool? IsSubLevel { get; set; }

        // mandatory fields
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        [Required]
        public DateTime? SystemCreateAt { get; set; }

        public DateTime? SystemUpdateAt { get; set; }


        [ConcurrencyCheck]
        [Timestamp]
        public byte[] RowVersion { get; set; }

    }
}
