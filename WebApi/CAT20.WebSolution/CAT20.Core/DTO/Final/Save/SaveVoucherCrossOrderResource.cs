

using CAT20.Core.Models.FinalAccount;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Core.DTO.Final.Save
{
    public class SaveVoucherCrossOrderResource
    {
        public int Id { get; set; }
        public int? SubVoucherItemId { get; set; }
        public decimal Amount { get; set; }
        public int? CrossOrderId { get; set; }
        public int? OrderType { get; set; }

        //public virtual SubVoucherItem? SubVoucherItem { get; set; }

        public virtual SaveCrossOrderResource? CrossOrder { get; set; }

    }
}
