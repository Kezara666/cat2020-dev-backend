using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.AssessmentTax
{
    public class AssessmentRates
    {
        [Key]
        public int? Id { get; set; }
        [Precision(18, 2)]
        public decimal? AnnualDiscount { get; set; }
        [Precision(18, 2)]
        public decimal? QuarterDiscount { get; set; }


    }
}
