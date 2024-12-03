using CAT20.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Assessment.Save
{
    public class SaveAssessmentAmalgamationResource
    {
        public  virtual ICollection<SaveAmalgamationAssessmentResource>? Amalgamations { get; set; }

        public ICollection<SaveAmalgamationSubDivisionActionsResource>? AmalgamationSubDivisionActions { get; set; }
        public virtual SaveAmalgamationAssessmentResource? AmalgamationAssessment { get; set; }
        public AssessmentStatus? RequestedAction { get; set; }
    }
}
