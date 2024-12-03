using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.Interfaces;
using CAT20.Core.Models.ShopRental;
using CAT20.Core.Repositories.ShopRental;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.ShopRental
{
    public class ShopRepository : Repository<Shop>, IShopRepository
    {
        public ShopRepository(DbContext context) : base(context)
        {
        }

        public async Task<Shop> GetById(int id)
        {
            return await shopRentalDbContext.Shops
                .Include(m => m.Property)
                .ThenInclude(f => f.Floor)
                .ThenInclude(r => r.RentalPlace)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Shop>> GetAll()
        {
            return
            await shopRentalDbContext.Shops
                .ToListAsync();
        }

        public async Task<IEnumerable<Shop>> GetAllForOffice(int officeId)
        {
           var result =
                await shopRentalDbContext.Shops
                .Include(m => m.Property)
                .ThenInclude(f => f.Floor)
                .ThenInclude(r => r.RentalPlace)
                .Where(p => p.OfficeId == officeId)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<Shop>> GetAllForSabha(int sabhaId)
        {
            return
                await shopRentalDbContext.Shops
                .Include(m => m.Property)
                .ThenInclude(f => f.Floor)
                .ThenInclude(r => r.RentalPlace)
                .Where(p => p.SabhaId == sabhaId)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Shop>> GetAllByPropertyTypeAndSabha(int propertytypeId, int sabhaId)
        {
            return
                await shopRentalDbContext.Shops
                .Where(m => m.SabhaId == sabhaId && m.Property.PropertyTypeId == propertytypeId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Shop>> GetAllByPropertyTypeAndOffice(int propertytypeId, int officeId)
        {
            return
                await shopRentalDbContext.Shops
                .Where(m => m.OfficeId == officeId && m.Property.PropertyTypeId == propertytypeId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Shop>> GetAllByPropertyNatureAndSabha(int propertyNatureId, int sabhaId)
        {
            return
                await shopRentalDbContext.Shops
                .Where(m => m.SabhaId == sabhaId && m.Property.PropertyNatureId == propertyNatureId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Shop>> GetAllByPropertyNatureAndOffice(int propertyNatureId, int officeId)
        {
            return
                await shopRentalDbContext.Shops
                .Where(m => m.OfficeId == officeId && m.Property.PropertyNatureId == propertyNatureId)
                .ToListAsync();
        }

        public async Task<Shop> GetByPropertyNo(string propertyNo)
        {
            return await shopRentalDbContext.Shops
                .Where(p => p.Property.PropertyNo == propertyNo)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Shop>> GetAllForProperty(int propertyid)
        {
            return
                await shopRentalDbContext.Shops
                .Include(m => m.Property)
                .ThenInclude(f => f.Floor)
                .ThenInclude(r => r.RentalPlace)
                .Where(m => m.PropertyId == propertyid)
                .ToListAsync();
        }

        public async Task<Shop> GetByAgreementNo(string agreementNo)
        {
            return await shopRentalDbContext.Shops
                .Where(p => p.AgreementNo == agreementNo)
                .FirstOrDefaultAsync();
        }

        
        //public async Task<IEnumerable<Shop>> GetAllForRentalPlace(int rentalPlaceId)
        //{
        //    return
        //        await shopRentalDbContext.Shops
        //        .Where(m => m.Property.RentalPlaceId == rentalPlaceId)
        //        .ToListAsync();
        //}

        public async Task<IEnumerable<Shop>> GetAllForFloor(int floorId)
        {
            try
            {
                return await shopRentalDbContext.Shops
                .Include(m => m.Property)
                .Where(m => m.Property.FloorId == floorId)
                .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //-----
        public async Task<IEnumerable<Shop>> GetAllZeroOpeningBlannceForFloor(int floorId)
        {
            try
            {
                return await shopRentalDbContext.Shops
                .Include(m => m.Property)
                .Where(m => m.Property.FloorId == floorId && m.VoteAssign != null && m.OpeningBalance == null && m.Status == ShopStatus.Active)
                .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Shop>> GetAllZeroVotesForFloor(int floorId)
        {
            try
            {
                return await shopRentalDbContext.Shops
                .Include(m => m.Property)
                .Where(m => m.Property.FloorId == floorId && m.VoteAssign == null && m.Status == ShopStatus.Active)
                .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Shop>> GetAllActiveShopForFloor(List<int?> propKeyIds, int floorId)
        {
            try
            {
                return await shopRentalDbContext.Shops
                .Include(m => m.Property)
                .ThenInclude(f => f.Floor)
                .ThenInclude(r => r.RentalPlace)
                .Where(m => propKeyIds.Contains(m.PropertyId) && m.Property.FloorId == floorId && m.Status == ShopStatus.Active)
                .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //-----


        //----
        public async Task<IEnumerable<Shop>> GetAllActiveShopsForOffice(int officeId)
        {
            var result =
                 await shopRentalDbContext.Shops
                 .Include(m => m.Property)
                 .ThenInclude(f => f.Floor)
                 .ThenInclude(r => r.RentalPlace)
                 .Where(m => m.OfficeId == officeId && m.Status == ShopStatus.Active)
                 .OrderByDescending(m => m.CreatedAt)
                 .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<Shop>> GetAllActiveShopsForSabha(int sabhaId)
        {
            return
                await shopRentalDbContext.Shops
                .Include(m => m.Property)
                .ThenInclude(f => f.Floor)
                .ThenInclude(r => r.RentalPlace)
                .Where(m => m.SabhaId == sabhaId && m.Status == ShopStatus.Active)
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Shop>> GetAllNotActiveShopsForSabha(int sabhaId)
        {
            return
                await shopRentalDbContext.Shops
                .Include(m => m.Property)
                .ThenInclude(f => f.Floor)
                .ThenInclude(r => r.RentalPlace)
                .Where(m => m.SabhaId == sabhaId && m.Status != ShopStatus.Active)
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Shop>> GetAllShopWithoutActiveStatusForFloor(int floorId)
        {
            try
            {
                return await shopRentalDbContext.Shops
                .Include(m => m.Property)
                .ThenInclude(f => f.Floor)
                .ThenInclude(r => r.RentalPlace)
                .Where(m => m.Property.FloorId == floorId && m.Status != ShopStatus.Active)
                .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Shop>> GetAllShopsForMonthendProcessBySabhaId(int sabhaId)
        {
            try
            {
                //need to get - all shops which have not this month balance record
                
                 return await shopRentalDbContext.Shops
                .Include(m => m.Property)
                .ThenInclude(f => f.Floor)
                .ThenInclude(r => r.RentalPlace)
                //.Where(s => s.SabhaId == sabhaId && s.OpeningBalance.ApproveStatus == 1 && !s.Balances.Any(b => b.Year == year && b.Month == month))
                .Where(s => s.SabhaId == sabhaId && s.OpeningBalance.ApproveStatus == 1)
                .Where(s =>  s.Status == ShopStatus.Active || s.Status == ShopStatus.Hold)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Shop>> GetAllShopsForFineProcessBySabha(int sabhaId, int processConfigId, int fineRateTypeId)
        {
            try
            {
                //need to get - all shops that have not completed their balances yet
                return await shopRentalDbContext.Shops
                .Include(m => m.Property)
                .ThenInclude(f => f.Floor)
                .ThenInclude(r => r.RentalPlace)
                .Where(s => s.SabhaId == sabhaId && s.ProcessConfigurationSettingAssign.ShopRentalProcessConfigaration.Id == processConfigId && s.ProcessConfigurationSettingAssign.ShopRentalProcessConfigaration.FineRateTypeId == fineRateTypeId && s.OpeningBalance.ApproveStatus == 1 && s.Status == ShopStatus.Active)
                //.Where(s => s.SabhaId == sabhaId && s.ProcessConfigurationSettingAssign.ShopRentalProcessConfigaration.Id == processConfigId && s.ProcessConfigurationSettingAssign.ShopRentalProcessConfigaration.FineRateTypeId == fineRateTypeId && s.OpeningBalance.ApproveStatus == 1 && s.Status == ShopStatus.Active && s.Balances.Any(b => b.IsCompleted == false))
                .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //----

        //---
        public async Task<IEnumerable<Shop>> GetAllSabhaForProcessConfigSettingAssignment(int sabhaId)
        {
            return await shopRentalDbContext.Shops
            .Include(m => m.Property)
            .ThenInclude(f => f.Floor)
            .ThenInclude(r => r.RentalPlace)
            .Where(s => s.SabhaId == sabhaId && s.ProcessConfigurationSettingAssign == null)
            .ToListAsync();
        }

        public async Task<IEnumerable<Shop>> GetAllForPropertyForPaymentProcess(int propertyId)
        {
            return await shopRentalDbContext.Shops
            .Where(s => s.PropertyId == propertyId && s.OpeningBalance.ApproveStatus == 1)
            .ToListAsync();
        }
        //---

        public async Task<IEnumerable<Shop>> GetAllAgreementEndedShopsForOfficeAndDate(int officeId, DateOnly enddate)
        {
            var result =
                 await shopRentalDbContext.Shops
                 .Include(m => m.Property)
                 .ThenInclude(f => f.Floor)
                 .ThenInclude(r => r.RentalPlace)
                 .Where(p => p.OfficeId == officeId && p.AgreementEndDate == enddate)
                 .OrderByDescending(p => p.CreatedAt)
                 .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<Shop>> GetAllShopsByProcessConfigId(int processConfigId)
        {
            var result =
                 await shopRentalDbContext.Shops
                 .Include(m => m.Property)
                 .ThenInclude(f => f.Floor)
                 .ThenInclude(r => r.RentalPlace)
                 .Where(s => s.ProcessConfigurationSettingAssign.ShopRentalProcessConfigaration.Id == processConfigId && s.OpeningBalance.ApproveStatus == 1 && s.Status == ShopStatus.Active)
                 .OrderByDescending(p => p.CreatedAt)
                 .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<Shop>> getAllShopsForSabhaAndPartnerId(int partnerId, int sabhaId)
        {
            return
                await shopRentalDbContext.Shops
                .Include(m => m.Property)
                .ThenInclude(f => f.Floor)
                .ThenInclude(r => r.RentalPlace)
                .Where(m => m.SabhaId == sabhaId && m.CustomerId== partnerId && m.Status == ShopStatus.Active)
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync();
        }



        public async Task<IEnumerable<Shop>> GetAllZeroExpectedIncomeVotesForFloor(int floorId)
        {
            try
            {
                return await shopRentalDbContext.Shops
                .Include(m => m.Property)
                .Where(m => m.Property.FloorId == floorId && m.RecievableIncomeVoteAssign == null && m.Status == ShopStatus.Active)
                .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Shop>> GetForUpdateVoteAssignForFinalAccounting(int sabhaId)
        {
           return await shopRentalDbContext.Shops
                .Include(m => m.VoteAssign)
                .Include(m => m.RecievableIncomeVoteAssign)
                .Where(m => m.SabhaId == sabhaId && m.Status == ShopStatus.Active)
                .ToListAsync();
        }

        public async Task<IEnumerable<Shop>> GetYearEndProcessForFinalAccount(int sabhaId)
        {
            return await shopRentalDbContext.Shops
                .Include(m => m.OpeningBalance)
                .Include(m => m.VoteAssign)
                .Where(m => m.SabhaId == sabhaId && m.Status == ShopStatus.Active)
                .ToListAsync();
        }

        public async Task<IEnumerable<Shop>> GetYearEndProcessOpenBalForFinalAccount(List<int> shopIds)
        {
            return await shopRentalDbContext.Shops
                .Include(m => m.OpeningBalance)
                .Include(m => m.VoteAssign)
                .Where(m => shopIds.Contains(m.Id.Value) && m.Status == ShopStatus.Active)
                .ToListAsync();
        }


        public async Task<Shop> GetMonthlyProcessForFinalAccount(int shopId)
        {
            return await shopRentalDbContext.Shops
                .Include(m => m.VoteAssign)
                .Include(m => m.RecievableIncomeVoteAssign)
                .Where(m => m.Id == shopId && m.Status == ShopStatus.Active)
                .FirstOrDefaultAsync();
                
        }

        private ShopRentalDbContext shopRentalDbContext
        {
            get { return Context as ShopRentalDbContext; }
        }

    }
}