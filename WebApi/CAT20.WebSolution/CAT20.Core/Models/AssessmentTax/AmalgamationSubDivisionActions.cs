using CAT20.Core.Models.FinalAccount;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.AssessmentTax
{
    public class AmalgamationSubDivisionActions
    {
        public int? Id { get; set; }
        public int? AmalgamationSubDivisionId { get; set; }
        public int ActionState { get; set; }
        public int ActionBy { get; set; }
        public string? Comment { get; set; }
        public DateTime? ActionDateTime { get; set; }
        [JsonIgnore]
        public virtual AmalgamationSubDivision? AmalgamationSubDivision { get; set; }

        //compulsory fields
        public DateTime? SystemActionAt { get; set; }
    }
}
