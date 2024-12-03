using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.FinalAccount
{
    public class SabhaFundSource
    {
        [Key]
        public int? Id { get; set; }
        [Required]
        public int? AccountSystem { get; set; }
        [Required]
        public string? Description { get; set; }


        /*mandatory filed*/
        public int ? Status { get; set; }
        [Required]
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public DateTime? SystemActionAt { get; set; }

        public virtual ICollection<IndustrialCreditors>? IndustrialCreditors { get; set; }
        public virtual ICollection<IndustrialDebtors>? IndustrialDebtors { get; set; }

    }
}
