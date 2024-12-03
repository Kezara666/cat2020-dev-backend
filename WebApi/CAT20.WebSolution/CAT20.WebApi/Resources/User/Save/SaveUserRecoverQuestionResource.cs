using CAT20.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAT20.WebApi.Resources.User.Save
{
    public class SaveUserRecoverQuestionResource
    {
        public int ID { get; set; }
        public string Question { get; set; }
    }
}
