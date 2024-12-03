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
    public class PropertyTypesLogs
    {

        [Key]
        public int? Id { get; set; }
        [Required]
        public int? PropertyTypeId { get; set; }
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

        [JsonIgnore]
        public virtual AssessmentPropertyType? AssessmentPropertyType { get; set; }

        [Range(0, 3, ErrorMessage = "Value must be either 0, 1, 2, or 3")]
        public int? ChangeFiled { get; set; }

        public DateOnly? DateFrom { get; set; }
        public DateOnly? DateTo { get; set; }

        /*
         * 0 both rates
         * 1 quarter rate
         * 2 warrant rate
         * 3 other text
         */

        // mandatory fields

        public DateTime? CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
    }
}
