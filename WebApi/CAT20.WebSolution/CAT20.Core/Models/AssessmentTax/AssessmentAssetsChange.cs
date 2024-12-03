using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.AssessmentTax
{
    public class AssessmentAssetsChange
    {

        [Key]
        public int? Id { get; set; }

        public int? AssessmentId { get; set; }

        [JsonIgnore]
        public virtual Assessment? Assessment { get; set; }


        public string? OldNumber { get; set; }


        public string? NewNumber { get; set; }



        public string? OldName { get; set; }


        public string? NewName { get; set; }



        public string? OldAddressLine1 { get; set; }


        public string? NewAddressLine1 { get; set; }



        public string? OldAddressLine2 { get; set; }


        public string? NewAddressLine2 { get; set; }

        

        public string? ChangingProperties { get; set; }

        [Required]
        public int? DraftApproveReject { get; set; }



        [Required]
        public DateTime? RequestDate { get; set; }

        [Required]
        public int? RequestBy { get; set; }

        public string? RequestNote { get; set; }


        public DateTime? ActionDate { get; set; }

        public int? ActionBy { get; set; }

        public string? ActionNote { get; set; }

    }
}
