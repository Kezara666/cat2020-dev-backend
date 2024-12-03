using CAT20.Core.Models;
using CAT20.Core.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAT20.WebApi.Resources.User
{
    public class GroupResource
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int? SabhaId { get; set; }

        //public virtual UserDetail UserCreated { get; set; }
        public int? UserCreatedID { get; set; }
        //public virtual UserDetail UserModified { get; set; }
        public int? UserModifiedID { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

    }
}
