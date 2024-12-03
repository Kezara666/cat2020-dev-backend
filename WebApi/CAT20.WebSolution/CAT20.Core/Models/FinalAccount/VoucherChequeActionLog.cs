using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.FinalAccount
{
    public class VoucherChequeActionLog
    {

        public int? Id { get; set; }
        public int? VoucherChequeId { get; set; }
        public Enums.FinalAccountActionStates? ActionState { get; set; }
        public string? Comment { get; set; }
        public int ActionBy { get; set; }
        public DateTime? ActionDateTime { get; set; }
        public DateTime? SystemActionAt { get; set; }
        [JsonIgnore]
        public virtual VoucherCheque? VoucherCheque { get; set; }
    }
}
