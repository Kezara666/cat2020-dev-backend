using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.AssessmentTax
{
    public class AssessmentRatesResource
    {
        public int? Id { get; set; }

        public decimal? AnnualDiscount { get; set; }
        public decimal? QuarterDiscount { get; set; }
        //public double? QuarterWarrant { get; set; }


    }
}
