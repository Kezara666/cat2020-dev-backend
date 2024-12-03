using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Final
{
    public class VoucherCrossOrderResource
    {
        public int Id { get; set; }
        public int? SubVoucherItemId { get; set; }
        public decimal Amount { get; set; }
        public int? CrossOrderId { get; set; }
        public int OrderType { get; set; }

        //public virtual SubVoucherItemResource SubVoucherItem { get; set; }

        //public virtual CrossOrderResource CrossOrder { get; set; }
    }
}
