using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CAT20.Core.Models.Control;
using Microsoft.EntityFrameworkCore;

namespace CAT20.WebApi.Resources.Control
{
    public partial class BankBranchResource
    {
        public int ID { get; set; }
        public int BankCode { get; set; }
        public string BranchName { get; set; }
        public string BranchCode { get; set; }
        public string BranchAddress { get; set; }
        public string TelNo1 { get; set; }
        public string TelNo2 { get; set; }
        public string TelNo3 { get; set; }
        public string TelNo4 { get; set; }
        public string FaxNo { get; set; }
        public string District { get; set; }
        public int? Status { get; set; }

        public BankDetailResource? Bank { get; set; }
    }
}
