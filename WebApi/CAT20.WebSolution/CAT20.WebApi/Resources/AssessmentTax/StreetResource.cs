using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Models.Control;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CAT20.WebApi.Resources.AssessmentTax
{
    public partial class StreetResource
    {
        public int? Id { get; set; }
        public string? StreetName { get; set; }
        public string? StreetNo { get; set; }
        public string? StreetCode { get; set; }
        public int WardId { get; set; }
        public virtual WardResource? Ward { get; set; }
        public int? Status { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
