using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAT20.Core.Models.AssessmentTax
{
    public class AssessmentDocument
    {
        [Key]
        public int? Id { get; set; }
        [Required]
        public string? DocType { get; set; }

        [Required]
        public string? Uri { get; set; }

        [Required]
        [JsonIgnore]
        [NotMapped]
        public IFormFile? File { get; set; }

        //[Required]
        public int? AssessmentId { get; set; }

        [JsonIgnore]
        public virtual Assessment? Assessment { get; set; }



        // mandatory fields
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
