using CAT20.Core.Models.OnlienePayment;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Control.Save
{
    public class SavePartnerResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [RegularExpression(@"^(?:[0-9]{9}[vVxX]|[0-9]{12})$", ErrorMessage = "Invalid NIC Number format.")]
        public string? NicNumber { get; set; }
        public string? PassportNo { get; set; }
        [RegularExpression(@"^(?:\+94|0)(7[0-9]{8}|[1-9][0-9]{8})$", ErrorMessage = "Invalid Mobile or Landline Number format.")]
        public string? MobileNumber { get; set; }
        [RegularExpression(@"^(?:\+94|0)(7[0-9]{8}|[1-9][0-9]{8})$", ErrorMessage = "Invalid Mobile or Landline Number format.")]
        public string? PhoneNumber { get; set; }
        public string Street1 { get; set; }
        public string? Street2 { get; set; }
        public string? City { get; set; }
        public string? Zip { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string? Email { get; set; }
        public sbyte? Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public sbyte? IsEditable { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public int GnDivisionId { get; set; }

        ////---
        //public string? GnDivisionName { get; set; }
        ////---

        //public virtual GnDivisionsResource? GnDivision { get; set; }
        // public virtual GnDivisions? GnDivision { get; set; }


        //public int? SabhaId { get; set; }
        //public sbyte? IsTempory { get; set; }
        ////public int? RIUserId { get; set; }
        //public int? IsBusinessOwner { get; set; }
        //public int? IsPropertyOwner { get; set; }

        //public int? IsBusiness { get; set; }
        //public string? BusinessRegNo { get; set; }

        //public virtual List<PartnerMobile>? PartnerMobiles { get; set; }

        //public virtual List<PermittedThirdPartyAssessments>? PermittedThirdPartyAssessments { get; set; }
    }
}
