using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.ShopRental
{
    public class RentalPaymentDateType
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }
        public int? Status { get; set; }

        public virtual ICollection<ShopRentalProcessConfigaration> ShopRentalProcessConfigarations { get; set; }
    }
}
