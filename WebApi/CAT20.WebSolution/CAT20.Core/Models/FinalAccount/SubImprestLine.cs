using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.FinalAccount
{
    public class SubImprestLine
    {
        [Key]
        public int? Id { get; set; }

        public int LedgerAccountId { get; set; }
        public DateTime DepositDate { get; set; }
        public int? MixOrderId { get; set; }
        public int? MixOrderLineId { get; set; }
        public String ReceiptNo { get; set; }
        public String Description { get; set; }

        public int PartnerId { get; set; }
        [Precision(18, 2)]
        public decimal Amount { get; set; }
        [Precision(18, 2)]
        public decimal SettleByCash { get; set; }
        [Precision(18, 2)]
        public decimal SettleByBills { get; set; }

        public int? SabhaId { get; set; }
        public int? OfficeId { get; set; }


        // mandatory fields
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        [Required]
        public DateTime? SystemCreateAt { get; set; }

        public DateTime? SystemUpdateAt { get; set; }

        [ConcurrencyCheck]
        [Timestamp]
        public byte[] RowVersion { get; set; }

    }
}
