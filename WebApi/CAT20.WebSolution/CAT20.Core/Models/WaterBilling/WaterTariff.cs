using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CAT20.Core.Models.WaterBilling
{
    public class WaterTariff
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int? WaterProjectId { get; set; }
        [Required]

        [JsonIgnore]
        public virtual WaterProjectNature? WaterProjectNature { get; set; }
        public int? NatureId { get; set; }
        [Required]
        public int? RangeStart { get; set; }
        [Required]
        public int? RangeEnd { get; set; }
        [Required]
        [Precision(18, 2)]
        public decimal? UnitPrice { get; set; }
        [Precision(18, 2)]
        public decimal? FixedCharge { get; set; }

        // mandatory fields
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }


    }
}
