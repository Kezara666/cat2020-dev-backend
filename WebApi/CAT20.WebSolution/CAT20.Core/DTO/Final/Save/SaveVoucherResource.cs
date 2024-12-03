using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount.logs;
using CAT20.Core.Models.FinalAccount;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using CAT20.Core.DTO.Final.Save;

namespace CAT20.Core.DTO.Final.Save
{
    public class SaveVoucherResource
    {
        public int? Id { get; set; }
        public int? CommitmentId { get; set; }
        public int? SubImprest { get; set; }
        public string? CommentOrDescription { get; set; }
        public int SabhaID { get; set; }
        public decimal VATTotal { get; set; }
        public decimal NBTTotal { get; set; }
        public decimal TotalChequeAmount { get; set; }
        public decimal VoucherAmount { get; set; }
        public decimal? StampTotal { get; set; }
        //public FinalAccountActionStates? ActionState { get; set; }
        //public VoucherCategory? VoucherCategory { get; set; }

        public string? VoucherSequenceNumber { get; set; }
        public PaymentStatus PaymentStatus { get; set; }

        public decimal? CrossAmount { get; set; }

        //public virtual List<VoucherActionLog>? ActionLog { get; set; }
        //public virtual List<VoucherLog>? VoucherLog { get; set; }
        public virtual List<SaveVoucherDocument>? VoucherDocuments { get; set; }
        public List<SaveVoucherLineResource>? VoucherLine { get; set; }

        public virtual ICollection<SaveSubVoucherItemResource>? SubVoucherItems { get; set; }

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


        //// mandatory fields
        //public int? RowStatus { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        //public int? CreatedBy { get; set; }

        //public int? UpdatedBy { get; set; }


        //[ConcurrencyCheck]
        //[Timestamp]
        //public byte[] RowVersion { get; set; }
    }
}
