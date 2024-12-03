using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CAT20.Core.Models.FinalAccount
{
    public class CommitmentActionsLog
    {
       
        public int? Id { get; set; }
        public int? CommitmentId { get; set; }
        public Enums.FinalAccountActionStates? ActionState { get; set; }
        public int ActionBy { get; set; }
        public string? Comment { get; set; }
        public DateTime? ActionDateTime { get; set; }
        [JsonIgnore]
        public virtual Commitment? Commitment { get; set; }
        public DateTime? SystemActionAt { get; set; }



    }
}
