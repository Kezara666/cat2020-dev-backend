using System;
using System.Collections.Generic;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.User;
using CAT20.Core.Models.Vote;

namespace CAT20.Core.Models.Mixin
{
    public partial class Banking 
    {
        public Banking()
        {
        }
        public int? Id { get; set; }
        //public MixinOrder MixinOrder { get; set; }
        public int OrderId { get; set; }
        //public Office Office { get; set; }
        //public int OfficeId { get; set; }
        //public AccountDetail AccountDetail { get; set; }
        //public int AccountDetailId { get; set; }
        //public int PaymentMethodId { get; set; }
        public DateTime BankedDate { get; set; }
        public DateTime CreatedAt { get; set; }
        //public UserDetail CreatedUserDetail { get; set; }
        public int CreatedBy { get; set; }
        public int OfficeId { get; set; }
        //public String ChequeNumber { get; set; }
        //public Decimal TotalAmount { get; set; }
    }
}