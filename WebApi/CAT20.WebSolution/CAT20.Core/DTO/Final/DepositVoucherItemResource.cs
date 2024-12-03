using CAT20.Core.DTO.OtherModule;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.Mixin;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Final
{
    public class DepositVoucherItemResource
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

        //public virtual Voucher? Voucher { get; set; }
    


        [ConcurrencyCheck]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        //liking models

        public virtual VendorResource? Partner { get; set; }
        //public virtual MixinOrder? CrossOrder { get; set; }
    }
}
