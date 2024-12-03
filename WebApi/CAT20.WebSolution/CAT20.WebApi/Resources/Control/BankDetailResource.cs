using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CAT20.Core.Models.Control;
using Microsoft.EntityFrameworkCore;

namespace CAT20.WebApi.Resources.Control
{
    public partial class BankDetailResource
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public int BankCode { get; set; }
        public int? Status { get; set; }

        public virtual ICollection<BankBranch>? bankBranch { get; set; }
    }
}
