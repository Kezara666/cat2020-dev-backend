using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.FinalAccount
{
    public class IndustrialCreditorsDebtorsTypes
    {
        public int? Id { get; set; }
        public string? Description { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? SystemActionAt { get; set; }

        public virtual ICollection<IndustrialCreditors>? IndustrialCreditors { get; set; }
        public virtual ICollection<IndustrialDebtors>? IndustrialDebtors { get; set; }
    }
}
