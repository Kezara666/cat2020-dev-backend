using CAT20.Core.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CAT20.Core.Models.AssessmentTax
{
    public class AssessmentATD
    {
        [Key]
        public int? Id { get; set; }

        public int? AssessmentId { get; set; }

        [JsonIgnore]
        public virtual Assessment? Assessment { get; set; }

        [Required]
        public ATDRequestStatus ATDRequestStatus { get; set; }

        [Required]
        public DateTime? RequestDate { get; set; }

        [Required]
        public int? RequestBy { get; set; }

        public string? RequestNote { get; set; }

        public DateTime? ActionDate { get; set; }

        public int? ActionBy { get; set; }

        public string? ActionNote { get; set; }

        public int? OfficeId { get; set; }
        public int? SabhaId { get; set; }

        public int? Status { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public virtual ICollection<AssessmentATDOwnerslog> AssessmentATDOwnerslogs { get; set; }
    }
}
