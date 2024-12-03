using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.AuditTrails;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.Interfaces;
using KellermanSoftware.CompareNetObjects;
using CAT20.Core.Models.Common;
using CAT20.Common.Enums;
using CAT20.Common.Envelop;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CAT20.Data.Resources
{
    public static class ReferentialIntegrityDictionary
    {
        public const string DEFAULT = "REF00001";
        public const string COMPANIES = "REF00002";
    }
}

