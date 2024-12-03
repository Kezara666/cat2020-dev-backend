using CAT20.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO
{
    public class CreditorDebtorResource
    {
        public VoucherPayeeCategory PayeeCategory { get; set; }
        public int CreditorDebtorId { get; set; }
        public string? CreditorDebtorName { get; set; }
        public string? Description { get; set; }
    }
}
