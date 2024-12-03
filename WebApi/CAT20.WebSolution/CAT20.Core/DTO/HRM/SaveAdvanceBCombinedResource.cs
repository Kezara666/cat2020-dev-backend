using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.HRM
{
    public class SaveAdvanceBCombinedResource
    {
        public SaveAdvanceBResource AdvanceB { get; set; }
        public SaveAdvanceBSettlementResource? AdvanceBSettlement { get; set; }
    }
}
