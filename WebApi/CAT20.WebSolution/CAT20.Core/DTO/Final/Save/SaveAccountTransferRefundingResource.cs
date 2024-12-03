using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Final.Save
{
    public class SaveAccountTransferRefundingResource
    {
        public int? Id { get; set; }
        public int AccountTransferId { get; set; }

        public int? voucherId { get; set; }

        public string? RefundNote { get; set; }

        public decimal Amount { get; set; }
        public int? CrossOrderId { get; set; }
    }
}
