using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Final.Save
{
    public class SaveSubVoucherItemResource
    {
        public int? Id { get; set; }
        public int? VoucherId { get; set; }
        public string? CommentOrDescription { get; set; }
        [Precision(18, 2)]
        public decimal? VATTotal { get; set; }
        [Precision(18, 2)]
        public decimal? NBTTotal { get; set; }
        [Precision(18, 2)]
        public decimal? ChequeAmount { get; set; }
        [Precision(18, 2)]
        public decimal VoucherItemAmount { get; set; }
        [Precision(18, 2)]
        public decimal? Stamp { get; set; }

        public int PayeeId { get; set; }

        //public int? CrossOrderId { get; set; }
        [Precision(18, 2)]
        public decimal? CrossAmount { get; set; }

        public virtual List<SaveVoucherCrossOrderResource>? VoucherCrossOrders { get; set; }

        //public virtual SaveCrossOrderResource? CrossOrder { get; set; }
    }
}
