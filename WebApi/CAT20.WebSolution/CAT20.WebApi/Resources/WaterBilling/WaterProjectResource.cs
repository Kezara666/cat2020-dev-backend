using CAT20.Core.Models.Control;
using CAT20.Core.Models.Mixin;
using CAT20.Core.Models.ShopRental;
using CAT20.WebApi.Resources.Control;
using CAT20.WebApi.Resources.WaterBilling;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.WaterBilling
{
    public class WaterProjectResource
    {
        
        public int? Id { get; set; }
    
        public string? Name { get; set; }
       
        public int OfficeId { get; set; }

        public  OfficeResource? Office { get; set; }
        public ICollection<WaterProjectGnDivisionResource>? WaterProjectGnDivisions { get; set; }
        public ICollection<GnDivisionsResource>? GnDivisions { get; set; }
        public ICollection<WaterProjectMainRoadResource>? MainRoads { get; set; }
        public ICollection<WaterProjectSubRoadResource>? SubRoads { get; set; }
        public ICollection<WaterProjectNatureResource>? Natures { get; set; }




        // mandatory fields

        public int? Status { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

    }
}
