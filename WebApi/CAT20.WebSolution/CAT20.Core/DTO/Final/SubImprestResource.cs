using CAT20.Core.DTO.OtherModule;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.HRM.PersonalFile;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Final
{
    public class SubImprestResource
    {
        //public int? Id { get; set; }

        //public int? SubImprestVoteId { get; set; }
        //public DateTime Date { get; set; }
        //public int? MixOrderId { get; set; }
        //public String Description { get; set; }

        //public int EmployeeId { get; set; }
        //public decimal Amount { get; set; }
        //public decimal SettleByBills { get; set; }
        //public decimal SettleByCash { get; set; }

        //public int? SabhaId { get; set; }
        //public int? OfficeId { get; set; }

        //public virtual ICollection<SubImprestSettlement>? SubImprestSettlements { get; set; }

        //// mandatory fields
        //public int? Status { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        //public int? CreatedBy { get; set; }
        //public int? UpdatedBy { get; set; }


        [Key]
        public int? Id { get; set; }

        public int SubImprestVoteId { get; set; }
        public DateTime Date { get; set; }
        public int? MixOrderId { get; set; }
        public int? SettlementCrossId { get; set; }
        public int? VoucherId { get; set; }
        public String? ReceiptNo { get; set; }
        public String Description { get; set; }

        [Required]
        public int? EmployeeId { get; set; }
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

        public int? SettlementVoucherNo { get; set; }

        public virtual ICollection<SubImprestSettlementResource>? SubImprestSettlements { get; set; }
        public virtual ICollection<SettlementCrossOrderResource>? SettlementCrossOrders { get; set; }

        // mandatory fields
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public bool IsOpenBalance { get; set; }


        //linking model
        public virtual VoucherResource? Voucher { get; set; }
        public virtual FinalUserActionByResources? Officer { get; set; }
        public virtual FinalEmployeeResource? Employee { get; set; }
    }
}
