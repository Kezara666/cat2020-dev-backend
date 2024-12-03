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
    public class CustomVoteSubLevel1Service : ICustomVoteSubLevel1Service
    {
        private readonly IMixinUnitOfWork _unitOfWork;
        public CustomVoteSubLevel1Service(IMixinUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        // Custom Vote Sub Level 1
        public async Task<(bool, string?)> SaveCustomVoteSubLevel1sAsync(List<CustomVoteSubLevel1> customVoteSubLevel1s, HTokenClaim _token)
        {
            try
            {
                foreach (var CustomVoteSubLevel2 in customVoteSubLevel1s)
                {
                    CustomVoteSubLevel2.CreatedBy = _token.userId;
                }

                _unitOfWork.CustomVoteSubLevel1s.AddRangeAsync(customVoteSubLevel1s);
                var result = await _unitOfWork.CommitAsync();
                return (true, "Successfully Saved !");
            }
            catch (Exception ex)
            {
                return (false, "error occoured " + ex.Message);
            }
        }

        public async Task<(bool, string?)> DeleteCustomVoteSubLevel1Async(int id)
        {
            try
            {
                var subLevel1 = await _unitOfWork.CustomVoteSubLevel1s.GetByCustomVoteSubLevel1IdAsysc(id);
                if (subLevel1 != null)
                {
                    _unitOfWork.CustomVoteSubLevel1s.Remove(subLevel1);
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

        public async Task<CustomVoteSubLevel1> GetByCustomVoteSubLevel1IdAsync(int customVoteId)
        {
            return await _unitOfWork.CustomVoteSubLevel1s.GetByCustomVoteSubLevel1IdAsysc(customVoteId);
        }

        public async Task<IEnumerable<CustomVoteSubLevel1>> GetCustomVoteSubLevel1sByCustomVoteAsync(int customVoteId)
        {
            return await _unitOfWork.CustomVoteSubLevel1s.GetCustomVoteSubLevel1sByCustomVoteAsync(customVoteId);
        }

    }
}