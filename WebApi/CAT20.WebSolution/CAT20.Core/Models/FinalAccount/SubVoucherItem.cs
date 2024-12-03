using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CAT20.Core.Models.FinalAccount
{
    public class SubVoucherItem
    {
        public int? Id { get; set; }
        [Required]
        public int? VoucherId { get; set; }
        [Required]
        public int? SubVoucherNo { get; set; }
        public string? CommentOrDescription { get; set; }
        [Required]
        public int? PayeeId { get; set; }
        [Precision(18, 2)]
        public decimal VATTotal { get; set; }
        [Precision(18, 2)]
        public decimal NBTTotal { get; set; }

        [Precision(18, 2)]
        public decimal VoucherItemAmount { get; set; }
        [Precision(18, 2)]
        public decimal? Stamp { get; set; }

        //public int? CrossOrderId { get; set; }
        [Precision(18, 2)]
        public decimal? CrossAmount { get; set; }

        [Precision(18, 2)]
        public decimal ChequeAmount { get; set; }

        public virtual ICollection<VoucherCrossOrder>? VoucherCrossOrders { get; set; }

        public virtual Voucher? Voucher{ get; set; }


        [ConcurrencyCheck]
        [Timestamp]
        public byte[] RowVersion { get; set; }

     }
}
