using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.FinalAccount
{
    public class DepreciationRates
    {
        [Key]
        public int? Id { get; set; }
        [Required]
        public string? SubTitleCode { get; set; }
        [Required]
        [Precision(18, 2)]
        public decimal? Rate { get; set; }
        [Required]
        public int? SabhaId { get; set; }
        public int? Status { get; set; }

        // mandatory fields

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
