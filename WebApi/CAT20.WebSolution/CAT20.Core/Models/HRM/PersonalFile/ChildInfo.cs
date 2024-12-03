using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.HRM.PersonalFile
{
    public partial class ChildInfo
    {
        public int? Id { get; set; }
        public int? EmployeeID { get; set; }
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }


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
