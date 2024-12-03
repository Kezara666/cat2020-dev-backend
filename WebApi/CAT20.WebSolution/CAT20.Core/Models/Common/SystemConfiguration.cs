using CAT20.Core.Models.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.Common
{
    public class SystemConfiguration : EntityBase
    {
        private string _companyName;
        public string CompanyName
        {
            get { return _companyName; }
            set
            {
                _companyName = value;
                AuditReference = value;
            }
        }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Email { get; set; }
        public string EmailServer { get; set; }
        public int? EmailServerPort { get; set; }
        public string EmailSenderGeneral { get; set; }
        public string EmailSenderGeneralPassword { get; set; }
        public string EmailSenderGeneralDisplayName { get; set; }
        public decimal? POLimitForMDApproval { get; set; }
        //public TaxGroup DefaultTaxGroup { get; set; }
        public int? DefaultTaxGroupID { get; set; }
        //public Currency BaseCurrency { get; set; }
        public int? BaseCurrencyID { get; set; }
    }
}
