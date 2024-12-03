using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CAT20.Core.Models.HRM.PersonalFile
{
    public partial class SupportingDocumentResource
    {
        public int? Id { get; set; }
        public int? EmployeeID { get; set; }
        public int SupportingDocTypeID { get; set; }
        public string DocumentPath { get; set; }

    }
}
