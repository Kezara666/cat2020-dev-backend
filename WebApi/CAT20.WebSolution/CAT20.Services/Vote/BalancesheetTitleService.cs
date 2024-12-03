using CAT20.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Vote;
using CAT20.Core.Services.Vote;
using DocumentFormat.OpenXml.Office2010.Excel;
using CAT20.Core.Models.Control;

namespace CAT20.Services.Vote
{
    public class BalancesheetTitleService : IBalancesheetTitleService
    {
        private readonly IVoteUnitOfWork _unitOfWork;
        public BalancesheetTitleService(IVoteUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BalancesheetTitle> CreateBalancesheetTitle(BalancesheetTitle newBalancesheetTitle)
        {
            await _unitOfWork.BalancesheetTitles
                .AddAsync(newBalancesheetTitle);
            await _unitOfWork.CommitAsync();

            return newBalancesheetTitle;
        }
        public async Task DeleteBalancesheetTitle(BalancesheetTitle balancesheetTitle)
        {
            balancesheetTitle.Status = 0;
            await _unitOfWork.CommitAsync();
        }
        public async Task<IEnumerable<BalancesheetTitle>> GetAllBalancesheetTitles()
        {
            return await _unitOfWork.BalancesheetTitles.GetAllAsync();
        }
        public async Task<BalancesheetTitle> GetBalancesheetTitleById(int id)
        {
            return await _unitOfWork.BalancesheetTitles.GetByIdAsync(id);
        }
        public async Task UpdateBalancesheetTitle(BalancesheetTitle balancesheetTitleToBeUpdated, BalancesheetTitle balancesheetTitle)
        {
            balancesheetTitleToBeUpdated.NameSinhala = balancesheetTitle.NameSinhala;
            balancesheetTitleToBeUpdated.NameTamil = balancesheetTitle.NameTamil;
            balancesheetTitleToBeUpdated.NameEnglish = balancesheetTitle.NameEnglish;
            balancesheetTitleToBeUpdated.Code = balancesheetTitle.Code;
            balancesheetTitleToBeUpdated.Balpath = balancesheetTitle.Balpath;
            balancesheetTitleToBeUpdated.ClassificationID = balancesheetTitle.ClassificationID;
            balancesheetTitleToBeUpdated.MainLedgerAccountID = balancesheetTitle.MainLedgerAccountID;
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<BalancesheetTitle>> GetAllWithBalancesheetTitleByBalancesheetTitleId(int Id)
        {
            return await _unitOfWork.BalancesheetTitles.GetAllWithBalancesheetTitleByBalancesheetTitleIdAsync(Id);
        }

        public async Task<IEnumerable<BalancesheetTitle>> GetAllBalancesheetTitleBySabhaId(int Id)
        {
            return await _unitOfWork.BalancesheetTitles.GetAllWithBalancesheetTitleBySabhaIdAsync(Id);
        }

        public async Task<IEnumerable<BalancesheetTitle>> GetAllWithBalancesheetTitleByAccountDetailId(int Id)
        {
            return await _unitOfWork.BalancesheetTitles.GetAllWithBalancesheetTitleByAccountDetailIdAsync(Id);
        }

        public async Task<IEnumerable<BalancesheetTitle>> GetAllWithBalancesheetTitleByClassificationCategoryId(int ClassificationId, int CategoryId, int sabhaid)
        {
            return await _unitOfWork.BalancesheetTitles.GetAllWithBalancesheetTitleByClassificationCategoryIdAsync(ClassificationId, CategoryId, sabhaid);
        }

        public async Task<IEnumerable<BalancesheetTitle>> GetAllWithBalancesheetTitleByClassificationId(int ClassificationId, int sabhaid)
        {
            return await _unitOfWork.BalancesheetTitles.GetAllWithBalancesheetTitleByClassificationIdAsync(ClassificationId, sabhaid);
        }

        public async Task<IEnumerable<BalancesheetTitle>> GetAllWithBalancesheetTitleByCategoryId(int CategoryId, int sabhaid)
        {
            return await _unitOfWork.BalancesheetTitles.GetAllWithBalancesheetTitleByCategoryIdAsync(CategoryId, sabhaid);
        }


    }
}