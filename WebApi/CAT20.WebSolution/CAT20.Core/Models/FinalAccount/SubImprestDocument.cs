using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.FinalAccount
{
    public class SubImprestDocument
    {
        public int Id { get; set; }
        public int SubImprestSettlementId { get; set; }

        public virtual SubImprestSettlement? SubImprestSettlement { get; set; }
        public string? Description { get; set; }

        public string URI { get; set; }
    }
}
