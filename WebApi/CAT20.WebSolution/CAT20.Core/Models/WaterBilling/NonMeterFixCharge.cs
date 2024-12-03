using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.WaterBilling
{
    public class NonMeterFixCharge
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public int? WaterProjectId { get; set; }

        [JsonIgnore]
        public virtual WaterProjectNature? WaterProjectNature { get; set; }

        [Required]
        public int? NatureId { get; set; }
        [Required]
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
