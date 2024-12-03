using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Final.Save
{
    public class SaveStockExpenditureAdjustment
    {
        public int? StockLedgerId { get; set; }
        public int? StockCustomVoteId { get; set; }
        public int? ExpenditureLedgerId { get; set; }
        public int? ExpenditureCustomVoteId { get; set; }
        public decimal? Amount { get; set; }
    }
}
