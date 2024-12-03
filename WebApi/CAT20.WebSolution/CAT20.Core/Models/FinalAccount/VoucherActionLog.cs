using System.Text.Json.Serialization;

namespace CAT20.Core.Models.FinalAccount
{

    public class VoucherActionLog
    {
        public int? Id { get; set; }
        public int? VoucherId { get; set; }
        public Enums.FinalAccountActionStates? ActionState { get; set; }
        public int ActionBy { get; set; }
        public string? Comment { get; set; }
        public DateTime? ActionDateTime { get; set; }
        [JsonIgnore]
        public virtual Voucher? Voucher { get; set; }

        //compulsory fields
        public DateTime? SystemActionAt { get; set; }
    }
}