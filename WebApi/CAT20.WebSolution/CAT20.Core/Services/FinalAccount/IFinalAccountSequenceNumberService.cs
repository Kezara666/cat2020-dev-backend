using CAT20.Core.Models.FinalAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.FinalAccount
{
    public interface IFinalAccountSequenceNumberService
    {
        Task<bool> CheckIsExistingAndIfNotCreateSequenceNoForYear(int year, int sabhaId);
    }
}
