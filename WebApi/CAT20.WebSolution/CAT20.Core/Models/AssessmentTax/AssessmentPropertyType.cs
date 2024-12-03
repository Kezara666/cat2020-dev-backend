
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;


namespace CAT20.Core.Models.AssessmentTax
{
    public partial class AssessmentPropertyType
    {

        [Key]
        public int? Id { get; set; }
        [Required]
        public string? PropertyTypeName { get; set; }
        [Required]
        public int? PropertyTypeNo { get; set; }
        [Required]
        [Precision(5, 2)]
        public decimal? QuarterRate { get; set; }
        [Required]
        [Precision(5, 2)]
        public decimal? WarrantRate { get; set; }
        [Required]
        public int? SabhaId { get; set; }
        public int? Status { get; set; }


        [Required]
        [Precision(5, 2)]
        public decimal? NextYearQuarterRate { get; set; }
        [Required]
        [Precision(5, 2)]
        public decimal? NextYearWarrantRate { get; set; }

        [JsonIgnore]
        public virtual ICollection<Assessment>? Assessments { get; set; }
        public virtual ICollection<PropertyTypesLogs>? PropertyTypesLogs { get; set; }





        // mandatory fields

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }


    }
}
