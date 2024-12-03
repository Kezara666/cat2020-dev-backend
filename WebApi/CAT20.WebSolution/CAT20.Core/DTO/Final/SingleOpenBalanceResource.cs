using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Final
{
    public class SingleOpenBalanceResource
    {
        public int? Id { get; set; }
        public int? LedgerAccountId { get; set; }

        public int? CustomVoteId { get; set; }

        public string? Description { get; set; }

        public decimal Amount { get; set; }

        /*mandatory filed*/

        public int? OfficeId { get; set; }
        public int? SabhaId { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        //public DateTime? SystemActionAt { get; set; }



        public virtual VoteDetailLimitedresource? LedgerVoteDetail { get; set; }
    }
}
