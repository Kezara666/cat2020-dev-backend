using CAT20.Core.DTO.Final;
using CAT20.Core.DTO.OtherModule;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.FinalAccount.logs;
using CAT20.WebApi.Resources.OnlinePayment;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CAT20.WebApi.Resources.Final;

public class CommitmentResource
{
    public int? Id { get; set; }

    public int sabhaId { get; set; }

    public VoucherPayeeCategory PayeeCategory { get; set; }
    public int PayeeId { get; set; }

    public string PayeeName { get; set; }
    public decimal TotalAmount { get; set; }

    public string Description { get; set; }

    public string? CommitmentSequenceNumber { get; set; }


    public FinalAccountActionStates? ActionState { get; set; }
    public PaymentStatus? PaymentStatus { get; set; }

    public bool HasVoucher { get; set; }


    public virtual List<CommitmentActionsLogResources>? ActionLog { get; set; } 


    public virtual List<CommitmentLineResource>? CommitmentLine { get; set; }

    // mandatory fields
    public int? RowStatus { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }



    //liking models

    public virtual VendorResource? VendorAccount { get; set; }
    public FinalUserActionByResources? UserActionBy { get; set; }
}