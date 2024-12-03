using CAT20.Core.Models.FinalAccount;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Final
{
    public class SubImprestSettlementResource
    {
        public int? Id { get; set; }
        public int? SubImprestId { get; set; }

        public int VoteDetailId { get; set; }
        public DateTime Date { get; set; }
        public String Description { get; set; }

        public decimal Amount { get; set; }


        // mandatory fields
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        //public virtual SubImprest? SubImprest { get; set; }
        public virtual ICollection<SubImprestDocument>? Documents { get; set; }

        //[Required]
        //public DateTime? SystemCreateAt { get; set; }

        //public DateTime? SystemUpdateAt { get; set; }

        //[ConcurrencyCheck]
        //[Timestamp]
        //public byte[] RowVersion { get; set; }
    }
}
