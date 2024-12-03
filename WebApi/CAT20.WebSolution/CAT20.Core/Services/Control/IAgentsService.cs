using CAT20.Core.HelperModels;
using CAT20.Core.Models.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Control
{
    public interface IAgentsService
    {
        Task<IEnumerable<Agents>> GetAgentsForSabha(int sabhaId);

        Task<Agents> GetAgentById(int agentId);

        Task<Agents> GetAgentByIdWithBankInfo(int agentId);


        Task<(bool, string)> Craete(Agents newAgent, HTokenClaim token);
        Task<(bool, string)> Update(Agents agent, HTokenClaim token);
        Task<(bool, string)> Delete(int agentId, HTokenClaim token);


    }
}
