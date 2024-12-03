using CAT20.Core.Models.FinalAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.FinalAccount
{
    public interface ICommitmentLineService
    {
       
        Task<CommitmentLine> CreateCommitmentLine(CommitmentLine commitmentLineToCreate);
             
    }
}
