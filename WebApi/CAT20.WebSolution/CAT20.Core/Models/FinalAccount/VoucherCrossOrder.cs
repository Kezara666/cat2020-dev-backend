using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.FinalAccount
{
    public class VoucherCrossOrder
    {
        public int Id { get; set; }
        public int SubVoucherItemId { get; set; }
        [Precision(18, 2)]
        public decimal Amount { get; set; }
        public int? CrossOrderId { get; set; }
        public int OrderType { get; set; }

        public virtual SubVoucherItem? SubVoucherItem { get; set; }
    }
}
