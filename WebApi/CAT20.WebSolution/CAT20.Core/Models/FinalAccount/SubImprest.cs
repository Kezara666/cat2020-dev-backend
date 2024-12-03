using CAT20.Core.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.FinalAccount
{
    public class SubImprest
    {
        [Key]
        public int? Id { get; set; }

        public int SubImprestVoteId { get; set; }
        public DateTime Date { get; set; }
        public int? MixOrderId { get; set; }

        public virtual ICollection<SettlementCrossOrder>? SettlementCrossOrders { get; set; }
        //public int? SettlementCrossId { get; set; }
        public int? VoucherId { get; set; }
        public String? ReceiptNo { get; set; }
        public String Description { get; set; }

        public int EmployeeId { get; set; }
        [Precision(18, 2)]
        public decimal Amount { get; set; }
        [Precision(18, 2)]
        public decimal? SettleByBills { get; set; }
        [Precision(18, 2)]
        public decimal? SettleByCash { get; set; }

        public int? SabhaId { get; set; }
        public int? OfficeId { get; set; }

        public int ExceedSettlementVoteId { get; set; }
        [Precision(18, 2)]
        public decimal? ExceedAmount { get; set; }

        public int? SettlementVoucherId { get; set; }

        public FinalAccountActionStates ActionStates { get; set; }

        public virtual ICollection<SubImprestSettlement>? SubImprestSettlements { get; set; }

        public bool IsOpenBalance { get; set; }

        public bool IsIllegal { get; set; }

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


        /*not mapped properties*/
        [NotMapped]
        public string? VoucherNo { get; set; }

    }
}
