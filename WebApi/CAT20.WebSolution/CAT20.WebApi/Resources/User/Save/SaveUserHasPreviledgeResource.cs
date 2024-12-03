using CAT20.Core.Models;
using CAT20.Core.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAT20.WebApi.Resources.User.Save
{
    public class SaveUserHasPreviledgeResource
    {
        public int ID { get; set; }
        public int? UserDetailID { get; set; }
        public int? PreviledgeID { get; set; }
        public int? Status { get; set; }
        public int? SabhaID { get; set; }

        //public virtual UserDetail userDetail { get; set; }
        //public virtual Previledge previledge { get; set; }
    }
}
