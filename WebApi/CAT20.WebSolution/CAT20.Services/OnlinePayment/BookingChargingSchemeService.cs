using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.OnlienePayment;
using CAT20.Core.Repositories.OnlinePayment;
using CAT20.Core.Services.OnlinePayment;

namespace CAT20.Services.OnlinePayment
{
    public class BookingChargingSchemeService : IChargingSchemeService
    {
        private readonly IOnlinePaymentUnitOfWork _unitOfWork;

        public BookingChargingSchemeService(IOnlinePaymentUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<ChargingScheme>> GetAllChargingSchemeBySubPropertyId(int SubPropertyId)
        {
            return await _unitOfWork.ChargingScheme.GetAllChargingSchemeForSubProprtyIdAsync(SubPropertyId);
        }
        public async Task<ChargingScheme> GetChargingSchemeById(int id)
        {
            return await _unitOfWork.ChargingScheme.GetChargingSchemeByIdAsync(id);
        }
        public async Task<(bool, string?)> SaveChargingScheme(ChargingScheme newChargingScheme, HTokenClaim token)
        {
            try
            {
                if (newChargingScheme.ID != null)
                {
                    var ChargingScheme = await _unitOfWork.ChargingScheme.GetByIdAsync(newChargingScheme.ID);
                    if (ChargingScheme != null)
                    {
                        ChargingScheme.SubPropertyId = newChargingScheme.SubPropertyId;
                        ChargingScheme.ChargingType = newChargingScheme.ChargingType;
                        ChargingScheme.Amount = newChargingScheme.Amount;
                        ChargingScheme.Status = 1;
                        newChargingScheme.UpdatedBy = token.userId;

                        await _unitOfWork.CommitAsync();
                        return (true, "Charging Scheme Updated Successfully");
                    }
                    else
                    {
                        return (false, "No Charging Scheme Found");
                    }

                }
                else
                {
                    newChargingScheme.SabhaID = token.sabhaId;
                    newChargingScheme.CreatedBy = token.userId;
                   var createdChargingScheme = _unitOfWork.ChargingScheme.AddAsync(newChargingScheme);
                    await _unitOfWork.CommitAsync();
                    return (true, "Charging Scheme Saved Successfully");
                }
            }
            catch (Exception ex)
            {
                return (false, "Error Occured" + ex.Message);
            }
        }

        public async Task<(bool, string?)> DeleteChargingScheme(ChargingScheme newChargingScheme)
        {
            try
            {
                if (newChargingScheme.ID != null)
                {
                    var ChargingScheme = await _unitOfWork.ChargingScheme.GetByIdAsync(newChargingScheme.ID);
                    if (ChargingScheme != null)
                    {
                        ChargingScheme.Status = 0;
                        await _unitOfWork.CommitAsync();
                        return (true, "Charging Scheme Deleted Successfully");
                    }
                    else
                    {
                        return (false, "Charging Scheme Not Found");
                    }
                }
                else
                {
                    return (false, "Charging Scheme Cannot Delete");
                }
            }
            catch (Exception ex)
            {
                return (false, "Error Occured" + ex.Message);
            }
        }
    }
}

