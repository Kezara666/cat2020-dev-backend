// Ignore Spelling: Sabha

using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Models.Control;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CAT20.WebApi.Resources.AssessmentTax
{
    public partial class WardResource
    {

        public int? Id { get; set; }
        public string? WardName { get; set; }
        public string? WardNo { get; set; }
        public string? WardCode { get; set; }
        public int OfficeId { get; set; }
        public int SabhaId { get; set; }
        public virtual ICollection<StreetResource>? Streets { get; set; }
        public int? Status { get; set; }

        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        //public virtual ICollection<AssessmentResource>? assessments { get; set; }
    }
}
