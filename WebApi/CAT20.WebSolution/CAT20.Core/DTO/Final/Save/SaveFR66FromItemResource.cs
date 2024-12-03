using CAT20.Core.Models.FinalAccount;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Final.Save
{
    public class SaveFR66FromItemResource
    {
        public int? Id { get; set; }

        public int? FR66Id { get; set; }
        public int FromVoteBalanceId { get; set; }
        public int FromVoteDetailId { get; set; }

        public decimal FromAmount { get; set; }
       
    }
}
