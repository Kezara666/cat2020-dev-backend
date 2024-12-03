using CAT20.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO
{
    public class PayeeResources
    {
        public VoucherPayeeCategory PayeeCategory { get; set; }
        public int PayeeId { get; set; }
        public string? PayeeName { get; set; }
        public string? FullName { get; set; }
    }
}
