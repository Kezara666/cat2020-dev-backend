using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Models.Control;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CAT20.WebApi.Resources.AssessmentTax
{
    public partial class AssessmentTempPartnerResource
    {
        
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? NicNumber { get; set; }
        public string? MobileNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Street1 { get; set; }
        public string? Street2 { get; set; }
        //public string? City { get; set; }
        //public string? Zip { get; set; }
        public string? Email { get; set; }
        public int AssessmentId { get; set; }

        public virtual AssessmentResource? Assessment { get; set; }



        // mandatory fields
        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

    }
}
