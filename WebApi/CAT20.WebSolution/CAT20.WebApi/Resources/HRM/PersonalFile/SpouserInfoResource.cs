using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CAT20.Core.Models.HRM.PersonalFile
{
    public partial class SpouserInfoResource
    {
        public int? Id { get; set; }
        public int? EmployeeID { get; set; }
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? JobTitle { get; set; }
        public string? WorkPlace { get; set; }
        public int? GnDivision { get; set; }

    }
}
