using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.FinalAccount
{
    public class VoucherItemsForCheque
    {
        public int? Id { get; set; }
        [Required]
        public int VoucherChequeId { get; set; }
        [Required]
        public int SubVoucherItemId { get; set; }

        public virtual VoucherCheque? VoucherCheque { get; set; }
    }
}
