using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.Enums
{
    public enum EntityType
    {
        //ControlDB Entities : IDs 1000-1099
        
        Province = 1001,
        District = 1002,
        Sabha = 1003,
        Office = 1004,


        //Mixin Order Entities : IDs 10-19


        //Trade_Tax Entities : IDs 20-29


        //Shop Rental Entities : IDs 30-39
        PropertyNature=30,
        RentalPlace = 31,


        //Water Bill Entities : IDs 40-49
        WaterProject = 40,


        //Assessment Tax Entities : IDs 50-59
        Assessment = 50,

        // Add other entity types



        Unknown = 9999,

    }
}
