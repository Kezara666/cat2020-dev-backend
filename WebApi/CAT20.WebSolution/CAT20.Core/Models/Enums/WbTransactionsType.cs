using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.Enums
{
    public enum  WbTransactionsType
    {
        Init,
        Payment,
        ReversePayemet,
        MonthlyBill,
        AddMeterReading,
        JournalAdjustment,
        SystemAdjustment,
        ProcessBill,
    }
}
