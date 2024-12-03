using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CAT20.WebApi.Resources.AssessmentTax
{
    public partial class AssessmentPropertyTypeResource
    {

        public int? Id { get; set; }
        public string? PropertyTypeName { get; set; }
        public int? PropertyTypeNo { get; set; }
        public decimal? QuarterRate { get; set; }
        public int? Status { get; set; }
        public double? WarrantRate { get; set; }
        public int? SabhaId { get; set; }

        //[JsonIgnore]
        public virtual ICollection<AssessmentResource>? Assessments { get; set; }


        public decimal? NextYearQuarterRate { get; set; }
        public decimal? NextYearWarrantRate { get; set; }

        // mandatory fields

        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }


    }
}
