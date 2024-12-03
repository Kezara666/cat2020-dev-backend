using CAT20.Core;
using CAT20.Core.CustomExceptions;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.Control;
using CAT20.Core.Services.Control;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.Control
{
    public class AgentsService : IAgentsService
    {
        private readonly IControlUnitOfWork _unitOfWork;
        public AgentsService(IControlUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<(bool, string)> Craete(Agents newAgent, HTokenClaim token)
        {
            try
            {
                newAgent.CreatedAt= DateTime.Now;
                newAgent.UpdatedBy = token.userId;
                newAgent.SabhaId = token.sabhaId;
                

                await _unitOfWork.Agents.AddAsync(newAgent);
                await _unitOfWork.CommitAsync();
                return (true, "Agent created successfully");

            }catch(Exception ex)
            {
                return (false, ex is FinalAccountException or GeneralException ? ex.Message : null);
            }
        }

        public async Task<(bool, string)> Delete(int agentId, HTokenClaim token)
        {
            try
            {
                var agent = await _unitOfWork.Agents.GetByIdAsync(agentId);
                if(agent != null && agent.SabhaId == token.sabhaId)
                {
                    agent.Status = 0;
                    await _unitOfWork.CommitAsync();
                    return (true, "Agent Deleted successfully");
                }
                else
                {
                    throw new GeneralException("Agent not found");
                }
                                   
              

            }catch(Exception ex)
            {
                return (false, ex is FinalAccountException or GeneralException ? ex.Message : null);
            }
        }

        public async Task<Agents> GetAgentById(int agentId)
        {
            return await _unitOfWork.Agents.GetByIdAsync(agentId);
        }

        public async Task<Agents> GetAgentByIdWithBankInfo(int agentId)
        {
            return await _unitOfWork.Agents.GetAgentByIdWithBankInfo(agentId);
        }

        public Task<IEnumerable<Agents>> GetAgentsForSabha(int sabhaId)
        {
           return _unitOfWork.Agents.GetAgentsForSabha(sabhaId);
        }

        public async Task<(bool, string)> Update(Agents agentToUpdate, HTokenClaim token)
        {
            try
            {
                var agent = await _unitOfWork.Agents.GetByIdAsync(agentToUpdate.Id);
                if (agent != null && agent.SabhaId == token.sabhaId)
                {
                    agent.Status = 0;
                    await _unitOfWork.CommitAsync();
                    return (true, "Agent Update Successfully");
                }
                else
                {
                    throw new GeneralException("Agent not found");
                }



            }
            catch (Exception ex)
            {
                return (false, ex is FinalAccountException or GeneralException ? ex.Message : null);
            }
        }
    }
}
