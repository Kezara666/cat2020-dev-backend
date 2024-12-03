﻿using CAT20.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Final.Save
{
    public class SaveCrossOrderResource
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNicNumber { get; set; }
        public string CustomerMobileNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public string? ChequeNumber { get; set; }
        public DateTime? ChequeDate { get; set; }
        public string? ChequeBankName { get; set; }
        public OrderStatus State { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int SessionId { get; set; }
        public int PaymentMethodId { get; set; }
        public int? CashierId { get; set; }
        public int? PartnerId { get; set; }
        public int? OfficeId { get; set; }
        public int? AccountDetailId { get; set; }
        public int? BusinessId { get; set; }
        public int? AppCategoryId { get; set; }
        public int? BusinessTaxId { get; set; }
        public TradeLicenseStatus TradeLicenseStatus { get; set; }
        public int? TaxTypeId { get; set; }
        public int? PaymentDetailId { get; set; }


        public decimal? AssmtBalByExcessDeduction { get; set; }

        public List<SaveCrossOrderLineResource> MixinOrderLine { get; set; }
    }
}
