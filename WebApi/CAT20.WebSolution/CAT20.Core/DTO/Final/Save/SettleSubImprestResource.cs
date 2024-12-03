using CAT20.Core.Models.FinalAccount;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Core.DTO.Final.Save
{
    public class SettleSubImprestResource
    {
        public int? Id { get; set; }

        //public int SubImprestVoteId { get; set; }
        //public DateTime Date { get; set; }
        //public int? MixOrderId { get; set; }
        //public int? SettlementCrossId { get; set; }
        //public String? VoucherNo { get; set; }
        //public String? ReceiptNo { get; set; }
        //public String Description { get; set; }

        //public int EmployeeId { get; set; }
        //public decimal Amount { get; set; }
        public decimal? SettleByBills { get; set; }
        public decimal? SettleByCash { get; set; }

        public int ExceedSettlementVoteId { get; set; }
        public decimal? ExceedAmount { get; set; }

        //public int? SabhaId { get; set; }
        //public int? OfficeId { get; set; }

        public virtual ICollection<SaveSubImprestSettlementResource>? SubImprestSettlements { get; set; }
    }
}
