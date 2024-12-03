using CAT20.Core.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Vote.Save
{
    public class SaveCustomVoteEntry
    {
        public int? Id { get; set; }
        public int? EntityPrimaryId { get; set; }
        public VoteEntityType? EntityType { get; set; }
        public int CustomVoteDetailIdParentId { get; set; }

        public int CustomVoteDetailId { get; set; }

        public decimal Amount { get; set; }

        public int Depth { get; set; }


        public int? ParentId { get; set; }
        public bool? IsSubLevel { get; set; }

        //// mandatory fields
        //public int? Status { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        //public int? CreatedBy { get; set; }
        //public int? UpdatedBy { get; set; }

        //public DateTime? SystemCreateAt { get; set; }

        //public DateTime? SystemUpdateAt { get; set; }


        //[ConcurrencyCheck]
        //[Timestamp]
        //public byte[] RowVersion { get; set; }
    }
}
