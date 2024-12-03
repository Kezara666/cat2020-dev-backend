using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CAT20.Core.DTO.Final.Save
{
    public class SaveSubImprestResource
    {
        public int? Id { get; set; }
        public int? SubImprestVoteId { get; set; }
        public String Description { get; set; }
        public DateTime? Date { get; set; }
        public int EmployeeId { get; set; }
        public decimal Amount { get; set; }

        public decimal? SettleByBills { get; set; }
        public decimal? SettleByCash { get; set; }
        public string? VoucherNo { get; set; }
        public bool IsOpenBalance { get; set; }
        public bool? IsIllegal { get; set; }
    }
}
