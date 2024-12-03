using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.Vote
{
    public class VoteBalanceActionLog
    {
        public int? Id { get; set; }
        public int? VoteBalanceId { get; set; }
        public FinalAccountActionStates? ActionState { get; set; }
        public int ActionBy { get; set; }
        public string? Comment { get; set; }
        public DateTime? ActionDateTime { get; set; }
        [JsonIgnore]
        public virtual VoteBalance? VoteBalance { get; set; }

        //compulsory fields
        public DateTime? SystemActionAt { get; set; }
    }
}
