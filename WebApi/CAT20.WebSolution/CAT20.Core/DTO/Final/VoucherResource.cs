using CAT20.Core.DTO.OtherModule;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.FinalAccount.logs;
using CAT20.Core.Models.Mixin;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CAT20.Core.DTO.Final
{

    public class VoucherResource
    {

        public int? Id { get; set; }
        public int? CommitmentId { get; set; }
        public int? SubImprestId { get; set; }
        public int? SettlementId { get; set; }
        public int? AccountTransferId { get; set; }
        public int? RefundId { get; set; }
        public string CommentOrDescription { get; set; }
        public int SabhaId { get; set; }
        [Precision(18, 2)]
        public decimal VATTotal { get; set; }
        [Precision(18, 2)]
        public decimal NBTTotal { get; set; }
        [Precision(18, 2)]
        public decimal? StampTotal { get; set; }

        [Precision(18, 2)]
        public decimal VoucherAmount { get; set; }

        public int? BankId { get; set; }
        [Precision(18, 2)]
        public decimal? CrossAmount { get; set; }

        [Precision(18, 2)]
        public decimal TotalChequeAmount { get; set; }


        public FinalAccountActionStates? ActionState { get; set; }
        public PaymentStatus? PaymentStatus { get; set; }
        public VoucherCategory? VoucherCategory { get; set; }

        public string VoucherSequenceNumber { get; set; }

        public VoucherPayeeCategory? PayeeCategory { get; set; }

        public virtual ICollection<DepositsForVoucherResource>? DepositsForVoucher { get; set; }
        public virtual ICollection<SubVoucherItemResource>? SubVoucherItems { get; set; }

        //public virtual ICollection<VoucherActionLog>? ActionLog { get; set; }
        //public virtual ICollection<VoucherLog>? VoucherLog { get; set; }

        public ICollection<VoucherLineResources>? VoucherLine { get; set; }

        public ICollection<VoucherDocumentResource>? VoucherDocuments { get; set; }
        public ICollection<VoucherInvoiceResource>? VoucherInvoices { get; set; }

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


        //liking models
        
        public AccountTransferResource? AccountTransfer { get; set; }
        public string? VoucherCategoryString { get; set; }

        //public virtual VendorResource? VendorAccount { get; set; }
        //public virtual MixinOrder? CrossOrder { get; set; }

        //public List<string>? CrossOrderVoteCodes { get; set; } = new List<string>();
        public FinalUserActionByResources? UserActionBy { get; set; }
    }
}