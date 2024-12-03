using CAT20.Core.Models.FinalAccount;
using CAT20.WebApi.Resources.Final;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Final
{
    public class CommitmentActionsLogResources
    {
        public int? Id { get; set; }
        public int? CommitmentId { get; set; }
        public int ActionBy { get; set; }
        public DateTime? ActionDateTime { get; set; }
        public string? Comment { get; set; }
        public Models.Enums.FinalAccountActionStates? ActionState { get; set; }
        [JsonIgnore]
        public virtual CommitmentResource? Commitment { get; set; }
    }
}
