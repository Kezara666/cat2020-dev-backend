using CAT20.Core.DTO.OtherModule;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Final
{
    public class SubVoucherItemResource
    {

        public int? Id { get; set; }
        public int? VoucherId { get; set; }
        public int? SubVoucherNo { get; set; }
        public string? CommentOrDescription { get; set; }

        public int PayeeId { get; set; }
        public decimal VATTotal { get; set; }
        public decimal NBTTotal { get; set; }

        public decimal VoucherItemAmount { get; set; }
        public decimal? Stamp { get; set; }

        public decimal? CrossAmount { get; set; }

        public decimal ChequeAmount { get; set; }

        public virtual ICollection<VoucherCrossOrderResource>? VoucherCrossOrders { get; set; }


        /*mapping properties*/
        public VoucherPayeeCategory? PayeeCategory { get; set; }

        //liking models

        public virtual PayeeResources? Payee { get; set; }


    }
}
