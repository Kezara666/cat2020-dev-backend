using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Final
{
    public class VoucherItemsForChequeResource
    {
        public int? Id { get; set; }
        public int? VoucherChequeId { get; set; }
        public int? SubVoucherItemId { get; set; }
    }
}
