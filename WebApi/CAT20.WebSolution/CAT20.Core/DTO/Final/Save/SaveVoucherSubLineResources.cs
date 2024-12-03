using Microsoft.EntityFrameworkCore;

namespace CAT20.Core.DTO.Final.Save
{
    public class SaveVoucherSubLineResources
    {
        public string? Description { get; set; }

        [Precision(18, 2)]
        public decimal NetAmount { get; set; }
        [Precision(18, 2)]
        public decimal VAT { get; set; }
        [Precision(18, 2)]
        public decimal NBT { get; set; }

        [Precision(18, 2)]
        public decimal TotalAmount { get; set; }
    }
}
