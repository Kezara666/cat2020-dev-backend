using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount.logs;
using CAT20.Core.Models.FinalAccount;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Final.Save
{
    public class SaveSalaryVoucher
    {
        public int? Id { get; set; }
        //public int? CommitmentId { get; set; }
        //public int? SubImprestId { get; set; }
        //public int? SettlementId { get; set; }
        //public int? AccountTransferId { get; set; }
        //public int? RefundId { get; set; }
        //public int? RePaymentOrderId { get; set; }
        public string CommentOrDescription { get; set; } = "";
        public int SabhaId { get; set; }
        public decimal VATTotal { get; set; }
        public decimal NBTTotal { get; set; }
        public decimal? StampTotal { get; set; }

        public decimal VoucherAmount { get; set; }

        //public int? BankId { get; set; }
        public decimal? CrossAmount { get; set; }

        public decimal TotalChequeAmount { get; set; }

        public VoucherPayeeCategory? PayeeCategory { get; set; }

        public FinalAccountActionStates? ActionState { get; set; }
        public PaymentStatus? PaymentStatus { get; set; }
        public VoucherCategory? VoucherCategory { get; set; }

        //public string VoucherSequenceNumber { get; set; }

        //public int PartnerId { get; set; }


        //public virtual ICollection<DepositForVoucher>? DepositsForVoucher { get; set; }
        public virtual ICollection<SaveEmployeeLoansForVoucher>? EmployeeLoansForVoucher { get; set; }
        public virtual ICollection<SaveSubVoucherItemResource>? SubVoucherItems { get; set; }

        //public virtual ICollection<VoucherActionLog>? ActionLog { get; set; }
        //public virtual ICollection<VoucherLog>? VoucherLog { get; set; }
        public ICollection<SaveVoucherLineResource>? VoucherLine { get; set; }
        //public ICollection<VoucherDocument>? VoucherDocuments { get; set; }
        //public ICollection<VoucherInvoice>? VoucherInvoices { get; set; }

        public int SessionId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }


        //public DateTime? PreChairmanApprove { get; set; }

        //public string? FileNo { get; set; }
        //public string? PreCommitteeNo { get; set; }
        //public DateTime? PreCommitteeApprove { get; set; }
        //public string? PreCouncilNo { get; set; }
        //public DateTime? PreCouncilApprove { get; set; }
        //public DateTime? SubjectToApprove { get; set; }


        //// mandatory fields
        //public int? RowStatus { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        //public int? CreatedBy { get; set; }

        //public int? UpdatedBy { get; set; }


        //public DateTime? SystemCreateAt { get; set; }

        //public DateTime? SystemUpdateAt { get; set; }

        //public byte[] RowVersion { get; set; }
    }
}
