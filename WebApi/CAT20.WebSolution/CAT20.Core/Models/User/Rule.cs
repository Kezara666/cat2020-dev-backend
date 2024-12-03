using CAT20.Core.Models.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.User
{
    public class Rule
    {
        public int? ID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Module { get; set; }

        public UserDetail UserCreated { get; set; }
        public int? UserCreatedID { get; set; }
        public UserDetail UserModified { get; set; }
        public int? UserModifiedID { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
