using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Assessment.Save
{
    public class SaveAssessmentTempSubPartnerResource
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        [RegularExpression(@"^(?:[0-9]{9}[vVxX]|[0-9]{12})$", ErrorMessage = "Invalid NIC Number format.")]
        public string? NicNumber { get; set; }
        [RegularExpression(@"^(?:\+94|0)(7[0-9]{8}|[1-9][0-9]{8})$", ErrorMessage = "Invalid Mobile or Landline Number format.")]
        public string? MobileNumber { get; set; }
        [RegularExpression(@"^(?:\+94|0)(7[0-9]{8}|[1-9][0-9]{8})$", ErrorMessage = "Invalid Mobile or Landline Number format.")]
        public string? PhoneNumber { get; set; }
        public string? Street1 { get; set; }
        public string? Street2 { get; set; }
        //public string? City { get; set; }
        //public string? Zip { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string? Email { get; set; }
        //public int AssessmentId { get; set; }

        //public virtual AssessmentResource? Assessment { get; set; }



        // mandatory fields
        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        //public int? CreatedBy { get; set; }
        //public int? UpdatedBy { get; set; }
    }
}
