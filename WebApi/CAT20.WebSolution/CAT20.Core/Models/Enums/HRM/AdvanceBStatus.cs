using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.Enums.HRM
{
    public enum AdvanceBStatus
    {
        Deleted=0,
        Init=1,
        ExistingCadre = 2,
        Settled=3,
        Rejected=4,
        /*withing provice*/
        TranferIn = 5,
        TransferOut = 6,
         /*out of provice*/
        TransferInOuter = 7,
        TransferOutOuter = 8,
        Deceased = 9,
        Pension=10,
        SupensionOfWork=11,
        Quitting=12,


    }
}
