using CAT20.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAT20.WebApi.Resources.User
{
    public class RuleResource
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string Module { get; set; }

        //public UserDetail UserCreated { get; set; }
        public int? UserCreatedID { get; set; }
        //public UserDetail UserModified { get; set; }
        public int? UserModifiedID { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
