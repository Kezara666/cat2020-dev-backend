using CAT20.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAT20.WebApi.Resources.User
{
    public class GroupUserResource
    {
        //public virtual Group Group { get; set; }
        public int GroupID { get; set; }
        //public virtual UserDetail User { get; set; }
        public int UserID { get; set; }

        //public virtual UserDetail UserCreated { get; set; }
        public int? UserCreatedID { get; set; }
        //public virtual UserDetail UserModified { get; set; }
        public int? UserModifiedID { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

    }
}
