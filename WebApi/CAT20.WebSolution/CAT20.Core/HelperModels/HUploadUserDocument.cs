using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.HelperModels
{
    public class HUploadUserDocument
    {
        public int? Id { get; set; }

        public IFormFile? File { get; set; }
    }
}
