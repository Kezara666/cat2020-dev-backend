using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.Enums
{
    public enum FinalAccountModuleType
    {
        [Description("COM")]
        Commitment,
        [Description("VOU")]
        Voucher,
        [Description("JNL")]
        Journal,
        [Description("IMP")]
        Imprest,
        [Description("FR66")]
        FR66,
        [Description("CPRV")]
        CutProvince,
        [Description("SUPP")]
        Supplementary,
    }
}
