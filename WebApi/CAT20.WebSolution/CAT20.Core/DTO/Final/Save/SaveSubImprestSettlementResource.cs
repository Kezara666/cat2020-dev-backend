using Microsoft.EntityFrameworkCore;

namespace CAT20.Core.DTO.Final.Save
{
    public class SaveSubImprestSettlementResource
    {
        public int? Id { get; set; }
        public int? SubImprestId { get; set; }

        public int VoteDetailId { get; set; }
        public DateTime Date { get; set; }
        public String Description { get; set; }

        public decimal Amount { get; set; }
    }
}
