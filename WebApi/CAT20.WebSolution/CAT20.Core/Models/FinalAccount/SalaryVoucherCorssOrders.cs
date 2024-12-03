using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.FinalAccount
{
    public class SalaryVoucherCorssOrders
    {
        [Key]
        public int? Id { get; set; }
        [Required]
        public int? VoucherId { get; set; }

        public int? CrossOrderId { get; set; }

        public int? CrossAmount { get; set; }

        public virtual Voucher? Voucher { get; set; }
    }
}
