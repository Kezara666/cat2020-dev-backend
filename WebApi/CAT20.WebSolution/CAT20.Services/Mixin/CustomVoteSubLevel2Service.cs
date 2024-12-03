using CAT20.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Mixin;
using CAT20.Core.Services.Mixin;
using CAT20.Core.Models.Common;
using Microsoft.AspNetCore.Identity;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using CAT20.Core.HelperModels;

namespace CAT20.Services.Mixin
{
    public class CustomVoteSubLevel2Service : ICustomVoteSubLevel2Service
    {
        private readonly IMixinUnitOfWork _unitOfWork;
        public CustomVoteSubLevel2Service(IMixinUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        // Custom Vote Sub Level 1
        public async Task<(bool, string?)> SaveCustomVoteSubLevel2sAsync(List<CustomVoteSubLevel2> customVoteSubLevel2s, HTokenClaim _token)
        {
            try
            {
                foreach (var CustomVoteSubLevel2 in customVoteSubLevel2s)
                {
                    CustomVoteSubLevel2.CreatedBy = _token.userId;
                }

                _unitOfWork.CustomVoteSubLevel2s.AddRangeAsync(customVoteSubLevel2s);
                var result = await _unitOfWork.CommitAsync();
                return (true, "Successfully Saved !");
            }
            catch (Exception ex)
            {
                return (false, "error occoured " + ex.Message);
            }
        }

        public async Task<(bool, string?)> DeleteCustomVoteSubLevel2Async(int id, HTokenClaim _token)
        {
            try
            {
                var subLevel2 = await _unitOfWork.CustomVoteSubLevel2s.GetCustomVoteSubLevel2ByIdAsync(id);
                if (subLevel2 != null)
                {
                    _unitOfWork.CustomVoteSubLevel2s.Remove(subLevel2);
                    await _unitOfWork.CommitAsync();
                    return (true, "Successfully Deleted !");
                }
                return (false, "Not Found !");
            }
            catch (Exception ex)
            {
                    return (false, "error occoured " + ex.Message);
            }
        }

        public async Task<CustomVoteSubLevel2> GetCustomVoteSubLevel2ByIdAsync(int customVoteId)
        {
            return await _unitOfWork.CustomVoteSubLevel2s.GetCustomVoteSubLevel2ByIdAsync(customVoteId);
        }

        public async Task<IEnumerable<CustomVoteSubLevel2>> GetCustomVoteSubLevel2sBySubLevel1Async(int customVoteId)
        {
            return await _unitOfWork.CustomVoteSubLevel2s.GetCustomVoteSubLevel2sBySubLevel1Async(customVoteId);
        }

    }
}