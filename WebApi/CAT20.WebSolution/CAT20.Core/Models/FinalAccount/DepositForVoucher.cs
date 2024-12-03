using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.FinalAccount
{
    public class DepositForVoucher
    {
        public int? Id { get; set; }
        public int? VoucherId { get; set; }

        public int? DepositId { get; set; }
        
        [Precision(18, 2)]
        public decimal Amount { get; set; }

        public virtual Voucher? Voucher { get; set; }

    }
}
