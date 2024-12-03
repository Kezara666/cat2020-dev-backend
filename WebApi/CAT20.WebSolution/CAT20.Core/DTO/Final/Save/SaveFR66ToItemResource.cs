using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Final.Save
{
    public class SaveFR66ToItemResource
    {
        public int? Id { get; set; }

        public int? FR66Id { get; set; }
        public int ToVoteBalanceId { get; set; }

        public int ToVoteDetailId { get; set; }

        public decimal ToAmount { get; set; }
    }
}
