using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CAT20.Core.Models.HRM.PersonalFile
{
    public partial class EmployeeTypeData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }

        // Navigation property
        public virtual ICollection<Employee>? Employees { get; set; }
    }
}
