using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount.logs;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CAT20.Core.Models.FinalAccount
{

    public class Voucher
    {
        public int? Id { get; set; }
        public int? CommitmentId { get; set; }
        public int? SubImprestId { get; set; }
        public int? SettlementId { get; set; }
        public int? AccountTransferId { get; set; }
        public int? RefundId { get; set; }
        public int? RePaymentOrderId { get; set; }
        public int?  AdvancedBId { get; set; }
        public string CommentOrDescription { get; set; }
        [Required]
        public int SabhaId { get; set; }
        [Precision(18, 2)]
        public decimal VATTotal { get; set; }
        [Precision(18, 2)]
        public decimal NBTTotal { get; set; }
        [Precision(18, 2)]
        public decimal? StampTotal { get; set; }

        [Precision(18, 2)]
        public decimal VoucherAmount { get; set; }

        [Required]
        public int? BankId { get; set; }
        [Precision(18, 2)]
        public decimal? CrossAmount { get; set; }

        [Precision(18, 2)]
        public decimal TotalChequeAmount { get; set; }

        [Required]
        public VoucherPayeeCategory? PayeeCategory { get; set; }

        [Required]
        public FinalAccountActionStates? ActionState { get; set; }
        public PaymentStatus? PaymentStatus { get; set; }
        public VoucherCategory? VoucherCategory { get; set; }

        public string VoucherSequenceNumber { get; set; }

        //public int PartnerId { get; set; }


        public virtual ICollection<DepositForVoucher>? DepositsForVoucher { get; set; }
        public virtual ICollection<EmployeeLoansForVoucher>? EmployeeLoansForVoucher { get; set; }
        public virtual ICollection<SubVoucherItem>? SubVoucherItems { get; set; }

        public virtual ICollection<VoucherActionLog>? ActionLog { get; set; }
        public virtual ICollection<VoucherLog>? VoucherLog { get; set; }
        public ICollection<VoucherLine>? VoucherLine { get; set; }
        public ICollection<VoucherDocument>? VoucherDocuments { get; set; }
        public ICollection<VoucherInvoice>? VoucherInvoices { get; set; }

        public int SessionId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }


        public DateTime? PreChairmanApprove { get; set; }

        public string? FileNo { get; set; }
        public string? PreCommitteeNo { get; set; }
        public DateTime? PreCommitteeApprove { get; set; }
        public string? PreCouncilNo { get; set; }
        public DateTime? PreCouncilApprove { get; set; }
        public DateTime? SubjectToApprove { get; set; }


        // mandatory fields
        public int? RowStatus { get; set; }
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