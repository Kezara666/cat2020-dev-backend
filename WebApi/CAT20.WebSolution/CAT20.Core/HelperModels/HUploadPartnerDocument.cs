using CAT20.Core.Models.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.HelperModels
{
    public class HUploadPartnerDocument
    {
        public int? Id { get; set; }
        public int PartnerId { get; set; }

        public IFormFile? File { get; set; }

        public PartnerDocumentType partnerDocumentType { get; set; }
        public string? Description { get; set; }

    }
}
