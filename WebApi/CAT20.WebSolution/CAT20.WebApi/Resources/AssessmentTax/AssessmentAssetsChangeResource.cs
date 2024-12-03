using CAT20.Core.Models.AssessmentTax;
using CAT20.WebApi.Resources.User;
using System.ComponentModel.DataAnnotations;

namespace CAT20.WebApi.Resources.AssessmentTax
{
    public class AssessmentAssetsChangeResource
    {
        [Key]
        public int? Id { get; set; }

        public int? AssessmentId { get; set; }

        //[JsonIgnore]
        //public virtual Assessment? Assessment { get; set; }


        public string? OldNumber { get; set; }


        public string? NewNumber { get; set; }



        public string? OldName { get; set; }


        public string? NewName { get; set; }



        public string? OldAddressLine1 { get; set; }


        public string? NewAddressLine1 { get; set; }



        public string? OldAddressLine2 { get; set; }


        public string? NewAddressLine2 { get; set; }



        public string? ChangingProperties { get; set; }

        public int? DraftApproveReject { get; set; }



        public DateTime? RequestDate { get; set; }

        public int? RequestBy { get; set; }
        public UserActionByResources? UserRequestBy { get; set; }

        public string? RequestNote { get; set; }


        public DateTime? ActionDate { get; set; }

        public int? ActionBy { get; set; }
        public UserActionByResources? UserActionBy { get; set; }

        public string? ActionNote { get; set; }
    }
}
