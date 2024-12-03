using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.HRM.PersonalFile
{
    public partial class OtherRemittanceAgent
    {
        public int? Id { get; set; }
        public int? EmployeeID { get; set; }
        public int BankName { get; set; }
        public int BankCode { get; set; }
        public int BranchCode { get; set; }
        public string AccountNo { get; set; }
        public DateTime? AgreDate { get; set; }
        public decimal? AgreMinimumAmount { get; set; }


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
