using CAT20.Core;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Services.FinalAccount;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.FinalAccount
{
    public class FinalAccountSequenceNumberService : IFinalAccountSequenceNumberService
    {
        private readonly IVoteUnitOfWork _unitOfWork;
        public FinalAccountSequenceNumberService(IVoteUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CheckIsExistingAndIfNotCreateSequenceNoForYear(int year, int sabhaId)
        {
            try{


                foreach (var moduleType in Enum.GetValues(typeof(FinalAccountModuleType)))
                {

                    var description = (DescriptionAttribute)Attribute.GetCustomAttribute(moduleType.GetType().GetField(moduleType.ToString()), typeof(DescriptionAttribute));

                    if (description != null)
                    {


                        if (!await _unitOfWork.FinalAccountSequenceNumber.HasSequenceNumberForCurrentYearAndModule(year, sabhaId, (FinalAccountModuleType)moduleType))
                        {
                            var newSqnNumber = new FinalAccountSequenceNumber
                            {
                                Year = year,
                                SabhaId = sabhaId,
                                ModuleType= (FinalAccountModuleType)moduleType,
                                Prefix = description.Description.ToString()!,
                                LastIndex = 0,
                                
                                

                            };

                            await _unitOfWork.FinalAccountSequenceNumber.AddAsync(newSqnNumber);
                            //await _unitOfWork.CommitAsync();

                        }
                        else
                        {

                        }
                    }
                }
                return true;
            }
            catch(Exception ex)
            {
                return false;

            }
        }
    }
}
