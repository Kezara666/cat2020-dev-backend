using CAT20.Core.DTO.OtherModule;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.Mixin;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Final
{
    public class DepositsForVoucherResource
    {
        public int? Id { get; set; }
        public int? VoucherId { get; set; }

        public int? DepositId { get; set; }

        [Precision(18, 2)]
        public decimal Amount { get; set; }

        //public virtual VoucherResource? Voucher { get; set; }

        //linking modle

        //liking models

        public virtual VendorResource? VendorAccount { get; set; }
        public virtual DepositResource? Deposit { get; set; }
    }
}
