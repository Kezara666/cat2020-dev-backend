using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount.logs;
using CAT20.Core.Models.Mixin;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.FinalAccount
{
    public class DepositVoucherItem
    {
        public int? Id { get; set; }
        public int? VoucherId { get; set; }
        public string? CommentOrDescription { get; set; }
        [Precision(18, 2)]
        public decimal VATTotal { get; set; }
        [Precision(18, 2)]
        public decimal NBTTotal { get; set; }
        [Precision(18, 2)]
        public decimal TotalChequeAmount { get; set; }
        [Precision(18, 2)]
        public decimal Amount { get; set; }
        [Precision(18, 2)]
        public decimal? Stamp { get; set; }

        public int PartnerId { get; set; }

        public int? CrossOrderId { get; set; }
        [Precision(18, 2)]
        public decimal? CrossAmount { get; set; }

        public virtual Voucher? Voucher{ get; set; }


        [ConcurrencyCheck]
        [Timestamp]
        public byte[] RowVersion { get; set; }

     }
}
