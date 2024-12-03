using CAT20.Core.Models.FinalAccount;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Final.Save
{
    public class SaveSalaryVoucherCorssOrders
    {
        public int? Id { get; set; }
        //public int? VoucherId { get; set; }

        //public int? CrossOrderId { get; set; }

        public int? CrossAmount { get; set; }

        public virtual SaveCrossOrderResource? CrossOrder { get; set; }
    }
}
