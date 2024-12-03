using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CAT20.Core.Models.HRM.PersonalFile
{
    public partial class NetSalaryAgentResource
    {
        public int? Id { get; set; }
        public int? EmployeeID { get; set; }
        public int BankName { get; set; }
        public int BankCode { get; set; }
        public int BranchCode { get; set; }
        public string AccountNo { get; set; }

    }
}
