using CAT20.Core.Models;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.Mixin;
using CAT20.Core.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAT20.WebApi.Resources.Mixin.Save
{
    public class UpdateMixinOrderStateResource
    {
        public int Id { get; set; }
        public OrderStatus State { get; set; }
    }
}
