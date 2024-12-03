using CAT20.Core.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.FinalAccount
{
    public class AccountTransferRefunding
    {
        public int? Id { get; set; }
        public int AccountTransferId { get; set; }

        public string? RefundNote { get; set; } 

        public int? VoucherId { get; set; }

        [Precision(18, 2)]
        public decimal Amount { get; set; }
        public int? CrossOrderId { get; set; }




        // mandatory fields
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual AccountTransfer? AccountTransfer { get; set; }

        [Required]
        public DateTime? SystemCreateAt { get; set; }

        public DateTime? SystemUpdateAt { get; set; }

        [ConcurrencyCheck]
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
