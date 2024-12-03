using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.ShopRental
{
    public class ProcessConfigurationSettingAssign
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        public int ShopId { get; set; } //FK

        [Required]
        public int ShopRentalProcessConfigarationId { get; set; } //FK



        //Mapping 1(Shop): 1 (ProcessConfigurationSettingAssign)
        public virtual Shop? Shop { get; set; }

        //Mapping 1(ShopRentalProcessConfigaration): Many (ProcessConfigurationSettingAssign)
        public virtual ShopRentalProcessConfigaration? ShopRentalProcessConfigaration { get; set; }


        // mandatory fields
        public int? Status { get; set; }
        public int? SabhaId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
