using CAT20.Core.Models.Enums.HRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CAT20.Core.Models.HRM.PersonalFile
{
    public partial class Address
    {
        public int? Id { get; set; }
        public int? EmployeeID { get; set; }
        public AddressType AddressType { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string CityTown { get; set; }
        public int GnDivision { get; set; }
        public int? PostalCode { get; set; }
        public string? Telephone { get; set; }
        public string? Fax { get; set; }


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
