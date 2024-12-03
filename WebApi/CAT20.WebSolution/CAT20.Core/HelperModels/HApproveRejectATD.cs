using CAT20.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.HelperModels
{
    public class HApproveRejectATD
    {
        public int? Id { get; set; }
        public int? AssessmentId { get; set; }
        public ATDRequestStatus ATDRequestStatus { get; set; }
        public int? ActionBy { get; set; }
    }
}
