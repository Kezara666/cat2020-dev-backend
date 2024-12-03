using CAT20.Core.DTO.Final;
using CAT20.Core.Models.Vote;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Vote
{
    public class SpecialLedgerAccountsResource
    {
        public int Id { get; set; }
        public string? VoteCode { get; set; }
        public int? VoteId { get; set; }
        public virtual SpecialLedgerAccountTypes? Type { get; set; }
        public int? TypeId { get; set; }
        public int? SabhaId { get; set; }

        //// mandatory fields
        //public int? Status { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        //public int? CreatedBy { get; set; }
        //public int? UpdatedBy { get; set; }

        //public DateTime? SystemActionAt { get; set; }

        public virtual VoteDetailLimitedresource? SpecialLegerAccount { get; set; }
    }
}
