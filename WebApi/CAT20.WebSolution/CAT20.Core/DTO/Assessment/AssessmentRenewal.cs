using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Assessment
{
    public class AssessmentRenewal
    {
        public int? Id { get; set; }
        public int? NewAllocation { get; set; }
        public int? NewPropertyTypeId { get; set; }
        public int? NewDescriptionId { get; set; }
    }
}
