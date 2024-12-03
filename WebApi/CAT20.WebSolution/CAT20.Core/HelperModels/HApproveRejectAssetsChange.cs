using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.HelperModels
{
    public class HApproveRejectAssetsChange
    {
        public int? Id { get; set; }
        public int? AssessmentId { get; set; }
        public int? DraftApproveReject { get; set; }
        public int? ActionBy { get; set; }
    }
}
