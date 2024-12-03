using CAT20.Core.Models.FinalAccount;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Final
{
    public class FR66FromItemResource
    {
        public int Id { get; set; }

        public int FR66Id { get; set; }
        public int FromVoteBalanceId { get; set; }
        public int FromVoteDetailId { get; set; }

        public decimal FromAmount { get; set; }

        //public FR66Transfer? FR66Transfer { get; set; }

        public VoteDetailLimitedresource? FromVoteDetail { get; set; }
    }
}
