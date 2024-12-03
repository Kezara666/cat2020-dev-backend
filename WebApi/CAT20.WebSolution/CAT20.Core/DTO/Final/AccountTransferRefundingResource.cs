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
    public class AccountTransferRefundingResource
    {
        public int? Id { get; set; }
        public int AccountTransferId { get; set; }

        public int? VoucherId { get; set; }
        public string? RefundNote { get; set; }

        public decimal Amount { get; set; }
        //public int? CrossOrderId { get; set; }




        //// mandatory fields
        //public int? Status { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        //public int? CreatedBy { get; set; }
        //public int? UpdatedBy { get; set; }

        //public virtual AccountTransfer? AccountTransfer { get; set; }

        /*linking model*/

        public virtual VoucherResource? Voucher { get; set; }


    }
}
