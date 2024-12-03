using CAT20.Core.Models.FinalAccount;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Final
{
    public class FR66ToItemResource
    {
        public int Id { get; set; }

        public int FR66Id { get; set; }
        public int ToVoteBalanceId { get; set; }

        public int ToVoteDetailId { get; set; }

        public decimal ToAmount { get; set; }

        //public FR66Transfer? FR66Transfer { get; set; }

        public VoteDetailLimitedresource? ToVoteDetail { get; set; }
    }
}
