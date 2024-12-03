using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CAT20.Core.Models.HRM.PersonalFile
{
    public partial class SalaryInfo
    {
        public int? Id { get; set; }
        public int? EmployeeID { get; set; }
        public int? WAndOPRate { get; set; }
        public int? PSPFRate { get; set; }
        public int? EmployeePSPFRate { get; set; }
        public int? LocalAuthoritiyPSPFRate { get; set; }       
        public int? OTRate { get; set; }
        public int? DaysPayRate { get; set; }
        public int? ETFRate { get; set; }


        // mandatory fields
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int StatusId { get; set; }

        // Navigation property
        public virtual Employee? Employee { get; set; }
    }
}
