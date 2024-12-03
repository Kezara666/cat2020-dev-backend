using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Final.Save
{
    public class SaveSingleOpenBalanceResource
    {
        public int? Id { get; set; }
        public int? LedgerAccountId { get; set; }

        public int? CustomVoteId { get; set; }

        public string? Description { get; set; }

        public decimal Amount { get; set; }
    }
}
