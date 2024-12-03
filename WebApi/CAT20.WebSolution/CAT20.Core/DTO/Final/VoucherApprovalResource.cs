using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.FinalAccount.logs;

namespace CAT20.Core.DTO.Final
{

    public class VoucherApprovalResource
    {
        public int? Id { get; set; }
        public int CommitmentId { get; set; }
        public int SabhaID { get; set; }
        public string partnerName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public decimal VATTotal { get; set; }
        public decimal NBTTotal { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Amount { get; set; }
        public decimal? Stamp { get; set; }
        public Core.Models.Enums.FinalAccountActionStates? Status { get; set; }

        //public virtual CrossSettlement? CrossSettlement { get; set; }
        public virtual List<VoucherActionLog>? ApprovedLog { get; set; }
        public virtual List<VoucherLog>? VoucherLog { get; set; }
        public virtual List<VoucherLine>? voucherLine { get; set; }
    }
}