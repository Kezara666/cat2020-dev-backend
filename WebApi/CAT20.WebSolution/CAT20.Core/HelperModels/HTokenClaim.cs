using CAT20.Core.Models.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.HelperModels
{
    public class HTokenClaim
    {
        public int userId { get; set; } = -1;
        public int officeId { get; set; } = -1;

        public int sabhaId { get; set; } = -1;
        public int AccountSystemVersionId { get; set; } = -1;
        public int ChartOfAccountVersionId { get; set; } = -1;

        public string sabhaName { get; set; } = "";

        public int IsFinalAccountsEnabled { get; set; } = -1;
        public int officeTypeID { get; set; } = -1;
        public string officeCode { get; set; } = "";
        public string? username { get; set; } = "";
        public int languageid { get; set; } = -1;
    }
}
