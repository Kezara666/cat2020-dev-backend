using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Assessment.Save
{
    public class SaveAmalgamationSubDivisionActionsResource
    {
        //public int? Id { get; set; }
        public int? AmalgamationSubDivisionId { get; set; }
        public int ActionState { get; set; }
        //public int ActionBy { get; set; }
        public string? Comment { get; set; }
        //public DateTime? ActionDateTime { get; set; }

        //compulsory fields
        //public DateTime? SystemActionAt { get; set; }
    }
}
