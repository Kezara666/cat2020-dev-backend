using CAT20.Core.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.ShopRental
{
    public class ShopDeposit
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        public int ShopId { get; set; }

        [Required]
        public int DepositId { get; set; }

        public DateTime DepositDate { get; set; }
        public int? MixOrderId { get; set; }
        public int? MixOrderLineId { get; set; }
        public String ReceiptNo { get; set; }

        [Precision(18, 2)]
        public decimal DepositAmount { get; set; }

        public int SessionId { get; set; }

        public ShopDepositTypes? Type { get; set; }

        public bool IsFullyRefund { get; set; }

        // mandatory fields
        public int? Status { get; set; }
        public int SabhaId { get; set; }
        public int OfficeId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }


        [ConcurrencyCheck]
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
