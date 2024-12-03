using CAT20.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CAT20.Core.Models.Vote
{
    public class CustomVoteBalanceActionLog
    {
        public int? Id { get; set; }
        public int? CustomVoteBalanceId { get; set; }
        public FinalAccountActionStates? ActionState { get; set; }
        public int ActionBy { get; set; }
        public string? Comment { get; set; }
        public DateTime? ActionDateTime { get; set; }
        [JsonIgnore]
        public virtual CustomVoteBalance? CustomVoteBalance { get; set; }

        //compulsory fields
        public DateTime? SystemActionAt { get; set; }
    }
}
