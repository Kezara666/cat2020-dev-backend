using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Assessment.Save
{
    public class SaveAssessmentSubDivisionResource
    {

        //public ICollection<Amalgamation>? Amalgamations { get; set; }
        public ICollection<SaveSubDivisionAssessmentResource>? SubDivisions { get; set; }
        public ICollection<SaveAmalgamationSubDivisionActionsResource>? AmalgamationSubDivisionActions { get; set; }
        public AssessmentStatus? RequestedAction { get; set; }

    }
}
