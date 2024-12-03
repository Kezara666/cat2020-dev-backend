using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CAT20.Core.Models.Enums;

namespace CAT20.Core.Models.AssessmentTax
{
    public class AssessmentATDOwnerslog
    {
        [Key]
        public int Id { get; set; }

        public int AssessmentATDId { get; set; }  

        public int? PartnerId { get; set; }  // Partner ID for the owner/sub-owner
        public string? OwnerNIC { get; set; }  // Partner ID for the owner/sub-owner

        [Required]
        public string OwnerName { get; set; }  // Owner/SubOwner name

        public string? AddressLine1 { get; set; } 

        public string? AddressLine2 { get; set; }  

        [Required]
        public AssessmentOwnerType OwnerType { get; set; }  // Enum to determine Owner or SubOwner

        [Required]
        public AssessmentOwnerStatus OwnerStatus { get; set; }  // Enum to determine Previous or New owner status

        public virtual AssessmentATD? AssessmentATD { get; set; }
    }
}
