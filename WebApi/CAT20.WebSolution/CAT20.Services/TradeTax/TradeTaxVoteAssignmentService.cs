using CAT20.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.TradeTax;
using CAT20.Core.Services.TradeTax;
using CAT20.Core.Models.Control;

namespace CAT20.Services.TradeTax
{
    public class TradeTaxVoteAssignmentService : ITradeTaxVoteAssignmentService
    {
        private readonly IVoteUnitOfWork _unitOfWork;
        public TradeTaxVoteAssignmentService(IVoteUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<TradeTaxVoteAssignment> CreateTradeTaxVoteAssignment(TradeTaxVoteAssignment newTradeTaxVoteAssignment)
        {
            await _unitOfWork.TradeTaxVoteAssignments
                .AddAsync(newTradeTaxVoteAssignment);
            await _unitOfWork.CommitAsync();

            return newTradeTaxVoteAssignment;
        }
        public async Task DeleteTradeTaxVoteAssignment(TradeTaxVoteAssignment tradeTaxVoteAssignment)
        {
            tradeTaxVoteAssignment.ActiveStatus = 0;
            await _unitOfWork.CommitAsync();
        }
        public async Task<IEnumerable<TradeTaxVoteAssignment>> GetAllTradeTaxVoteAssignments()
        {
            return await _unitOfWork.TradeTaxVoteAssignments.GetAllAsync();
        }
        public async Task<TradeTaxVoteAssignment> GetTradeTaxVoteAssignmentById(int id)
        {
            return await _unitOfWork.TradeTaxVoteAssignments.GetByIdAsync(id);
        }
        public async Task UpdateTradeTaxVoteAssignment(TradeTaxVoteAssignment taxTypeToBeUpdated, TradeTaxVoteAssignment taxType)
        {
            taxTypeToBeUpdated.TaxTypeID = taxType.TaxTypeID;
            taxTypeToBeUpdated.VoteAssignmentDetailID = taxType.VoteAssignmentDetailID;
            //taxTypeToBeUpdated.BankAccountID = taxType.BankAccountID;
            taxTypeToBeUpdated.UpdatedBy = taxType.UpdatedBy;
            await _unitOfWork.CommitAsync();
        }
        public async Task<IEnumerable<TradeTaxVoteAssignment>> GetAllWithTradeTaxVoteAssignmentByTradeTaxVoteAssignmentId(int Id)
        {
            return await _unitOfWork.TradeTaxVoteAssignments.GetAllWithTradeTaxVoteAssignmentByTradeTaxVoteAssignmentIdAsync(Id);
        }
        public async Task<IEnumerable<TradeTaxVoteAssignment>> GetAllWithTradeTaxVoteAssignmentByTaxTypeIdandSabhaId(int TradeTaxVoteAssignmentId, int SabhaId)
        {
            return await _unitOfWork.TradeTaxVoteAssignments.GetAllWithTradeTaxVoteAssignmentByTaxTypeIdandSabhaIdAsync(TradeTaxVoteAssignmentId, SabhaId);
        }

        public async Task<IEnumerable<TradeTaxVoteAssignment>> GetAllTradeTaxVoteAssignmentsForSabhaId(int SabhaId)
        {
            return await _unitOfWork.TradeTaxVoteAssignments.GetAllTradeTaxVoteAssignmentsForSabhaIdAsync(SabhaId);
        }

        public async Task<IEnumerable<TradeTaxVoteAssignment>> GetAllTradeTaxVoteAssignmentsForTaxTypeID(int TaxTypeID)
        {
            try
            {
                return await _unitOfWork.TradeTaxVoteAssignments.GetAllTradeTaxVoteAssignmentsForTaxTypeIDAsync(TaxTypeID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                List<TradeTaxVoteAssignment> bnature = new List<TradeTaxVoteAssignment>();
                return bnature;
            }
        }

        public async Task<TradeTaxVoteAssignment> GetTradeTaxVoteAssignmentsForTaxTypeIDAndSabhaId(int TaxTypeId, int SabhaId)
        {
            try
            {
                return await _unitOfWork.TradeTaxVoteAssignments.GetTradeTaxVoteAssignmentsForTaxTypeIDAndSabhaIdAsync(TaxTypeId, SabhaId);
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                //List<TradeTaxVoteAssignment> bnature = new List<TradeTaxVoteAssignment>();
                //return bnature;
                return null;
            }
        }
        
    }
}