using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.AssessmentTax
{
    public class SubDivision
    {
        [Key]
        public int? Id { get; set; }
        public int? AmalgamationSubDivisionId { get; set; }

        public int? KFormId { get; set; }

        [JsonIgnore]
        public virtual AmalgamationSubDivision? AmalgamationSubDivision { get; set; }
    }
}
