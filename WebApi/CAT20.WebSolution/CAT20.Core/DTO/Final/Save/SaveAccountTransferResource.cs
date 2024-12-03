using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Final.Save
{
    public class SaveAccountTransferResource
    {
        public int? Id { get; set; }


        public decimal Amount { get; set; }
        public int FromAccountId { get; set; }

        public int FromVoteDetailId { get; set; }



        public int ToAccountId { get; set; }

        public int ToVoteDetailId { get; set; }

        public bool IsRefund { get; set; }



        public string? RequestNote { get; set; }




    }
}
