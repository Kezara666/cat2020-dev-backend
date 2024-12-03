using CAT20.Core;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.OnlienePayment;
using CAT20.Core.Models.Vote;
using CAT20.Core.Services.Vote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.Vote
{
    public class BudgetService : IBudgetService
    {
        private readonly IVoteUnitOfWork _unitOfWork;

        public BudgetService(IVoteUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Budget>> GetAllBudgetsByVoteDetailId(int VoteDetailId)
        {
            return await _unitOfWork.Budget.GetAllBudgetForVoteDetailIdAsync(VoteDetailId);
        }

        public async Task<IEnumerable<Budget>> GetAllBudgetsBySabhaID(int SabhaID)
        {
            return await _unitOfWork.Budget.GetAllBudgetForSabhaIDAsync(SabhaID);
        }

        public async Task<Budget> GetBudgetById(int id)
        {
            return await _unitOfWork.Budget.GetBudgetByIdAsync(id);
        }

        public async Task<(bool, string?)> SaveBudget(Budget newBudget, HTokenClaim token)
        {
            try
            {
                if (newBudget.Id != null)
                {
                    var Budget = await _unitOfWork.Budget.GetByIdAsync(newBudget.Id);
                    if (Budget != null)
                    {
                        //Budget.BudgetType = newBudget.BudgetType;
                        //Budget.CustomVoteId = newBudget.CustomVoteId;
                        Budget.Q1Amount = newBudget.Q1Amount;
                        Budget.Q2Amount = newBudget.Q2Amount;
                        Budget.Q3Amount = newBudget.Q3Amount;
                        Budget.Q4Amount = newBudget.Q4Amount;
                        Budget.AnnualAmount = newBudget.AnnualAmount;
                        Budget.January = newBudget.January;
                        Budget.February = newBudget.February;
                        Budget.March = newBudget.March;
                        Budget.April = newBudget.April;
                        Budget.May = newBudget.May;
                        Budget.June = newBudget.June;
                        Budget.July = newBudget.July;
                        Budget.August = newBudget.August;
                        Budget.September = newBudget.September;
                        Budget.October = newBudget.October;
                        Budget.November = newBudget.November;
                        Budget.December = newBudget.December;
                        newBudget.UpdatedAt = DateTime.Now;
                        Budget.Status = 1;
                        Budget.UpdatedBy = token.userId;

                        await _unitOfWork.CommitAsync();
                        return (true, "Budget Updated Successfully");
                    }
                    else
                    {
                        return (false, "No Budget Found");
                    }
                }
                else
                {
                    newBudget.SabhaID = token.sabhaId;
                    newBudget.CreatedBy = token.userId;
                    newBudget.CreatedAt = DateTime.Now;
                    var createdBudget = _unitOfWork.Budget.AddAsync(newBudget);
                    await _unitOfWork.CommitAsync();
                    return (true, "Budget Saved Successfully");

                }
            }
            catch (Exception ex)
            {
                return (false, "Error Occured" + ex.Message);
            }
        }

        public async Task<(bool, string?)> SaveBudgetList(List<Budget> newBudgetList, HTokenClaim token)
        {
            try
            {
                foreach (var newBudget in newBudgetList)
                {
                    if (newBudget.Id != null)
                    {
                        var Budget = await _unitOfWork.Budget.GetByIdAsync(newBudget.Id);
                        if (Budget != null)
                        {
                            //Budget.BudgetType = newBudget.BudgetType;
                            //Budget.CustomVoteId = newBudget.CustomVoteId;
                            Budget.VoteDetailId = newBudget.VoteDetailId;
                            Budget.Q1Amount = newBudget.Q1Amount;
                            Budget.Q2Amount = newBudget.Q2Amount;
                            Budget.Q3Amount = newBudget.Q3Amount;
                            Budget.Q4Amount = newBudget.Q4Amount;
                            Budget.AnnualAmount = newBudget.AnnualAmount;
                            Budget.January = newBudget.January;
                            Budget.February = newBudget.February;
                            Budget.March = newBudget.March;
                            Budget.April = newBudget.April;
                            Budget.May = newBudget.May;
                            Budget.June = newBudget.June;
                            Budget.July = newBudget.July;
                            Budget.August = newBudget.August;
                            Budget.September = newBudget.September;
                            Budget.October = newBudget.October;
                            Budget.November = newBudget.November;
                            Budget.December = newBudget.December;
                            Budget.Status = 1;
                            Budget.UpdatedBy = token.userId;
                            Budget.UpdatedAt = DateTime.Now;

                            await _unitOfWork.CommitAsync();
                            //return (true, "Budget Updated Successfully");
                        }
                        else
                        {
                            //return (false, "No Budget Found");
                        }
                    }
                    else
                    {
                        newBudget.SabhaID = token.sabhaId;
                        newBudget.CreatedBy = token.userId;
                        newBudget.CreatedAt = DateTime.Now;
                        var createdBudget = _unitOfWork.Budget.AddAsync(newBudget);
                        await _unitOfWork.CommitAsync();
                        //return (true, "Budget Saved Successfully");
                    }
                }
                return (true, "Budget Saved Successfully");
            }
            catch (Exception ex)
            {
                return (false, "Error Occured" + ex.Message);
            }
        }

        public async Task<(bool, string?)> DeleteBudget(Budget newBudget)
        {
            try
            {
                if (newBudget.Id != null)
                {
                    var Budget = await _unitOfWork.Budget.GetByIdAsync(newBudget.Id);
                    if (Budget != null)
                    {
                        Budget.Status = 0;
                        await _unitOfWork.CommitAsync();
                        return (true, "Budget Deleted Successfully");
                    }
                    else
                    {
                        return (false, "Budget Not Found");
                    }
                }
                else
                {
                    return (false, "Budget Cannot Delete");
                }
            }
            catch (Exception ex)
            {
                return (false, "Error Occured" + ex.Message);
            }
        }
    }
}
