using CAT20.Core.Models.FinalAccount;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Final
{
    public class SettlementCrossOrderResource
    {
        public int? Id { get; set; }
        public int? SettlementCrossId { get; set; }
        public int? SubImprestId { get; set; }

        public decimal Amount { get; set; }

        //public virtual SubImprest SubImprest { get; set; }
    }
}
