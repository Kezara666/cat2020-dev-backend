using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.ShopRental;
using CAT20.Core.Repositories.ShopRental;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.ShopRental
{
    public class ShopRentalBalanceRepository: Repository<ShopRentalBalance>, IShopRentalBalanceRepository
    {
        public ShopRentalBalanceRepository(DbContext context) : base(context)
        {


        }

        private ShopRentalDbContext shopRentalDbContext
        {
            get { return Context as ShopRentalDbContext; }
        }

        public async Task<IEnumerable<ShopRentalBalance>> GetAllNotCompletedBalanceByYear(int shopId, int inputYear)
        {
            var shoprentalBalances = await shopRentalDbContext.ShopRentalBalance
            .Where(sh => sh.ShopId == shopId && sh.Year == inputYear && sh.IsCompleted == false && sh.IsHold == 0)
            .Include(pr => pr.Property)
            .Include(s => s.Shop)
            .ToListAsync();

            return shoprentalBalances;
        }
        //*****













        //---
        public async Task<ShopRentalBalance> GetNotCompletedBalanceInfoByYearMonth(int propertyId, int shopId, int year, int month)
        {
            var shoprentalYearBalance = await shopRentalDbContext.ShopRentalBalance
                .Include(pr => pr.Property)
                .Include(s => s.Shop)
                .FirstOrDefaultAsync(sh => sh.PropertyId == propertyId && sh.ShopId == shopId && sh.Year == year && sh.Month == month && sh.IsCompleted == false && sh.IsHold==0);

            return shoprentalYearBalance;
        }
        //---


        public async Task<IEnumerable<ShopRentalBalance>> GetAllNotCompletedBalanceInformationOfShop(int shopId)
        {
            var shoprentalBalances = await shopRentalDbContext.ShopRentalBalance
            .Where(sh => sh.ShopId == shopId && sh.IsCompleted == false )
            .Include(pr => pr.Property)
            .Include(s => s.Shop)
            .ToListAsync();

            return shoprentalBalances;
        }
        //*****















        public async Task<IEnumerable<ShopRentalBalance>> GetAllBalanceForShopProperty(int shopId)
        {
            var shoprentalBalances = await shopRentalDbContext.ShopRentalBalance
            .Where(sh => sh.ShopId == shopId)
            .Include(pr => pr.Property)
            .Include(s => s.Shop)
            .ToListAsync();

            return shoprentalBalances;
        }

        public async Task<ShopRentalBalance> GetCurrentMonthBalanceInfo(int shopId, int year, int monthId)
        {
            var shoprentalYearBalance = await shopRentalDbContext.ShopRentalBalance
                .Include(pr => pr.Property)
                .Include(s => s.Shop)
                .FirstOrDefaultAsync(sh => sh.ShopId == shopId && sh.Year == year && sh.Month == monthId);

            return shoprentalYearBalance;
        }

        public async Task<ShopRentalBalance> GetLastBalanceInfo(int shopId)
        {
            try
            {
                var shoprentalLastBalance = await shopRentalDbContext.ShopRentalBalance
                  .Include(pr => pr.Property)
                  .Include(s => s.Shop)
                  .Include(op => op.Shop.OpeningBalance)
                  .OrderByDescending(m => m.Id)
                  .FirstOrDefaultAsync(sh => sh.ShopId == shopId);

                return shoprentalLastBalance;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<ShopRentalBalance>> GetAllNotCompletedBalanceInformationUptoYearMonth(int propertyId, int shopId, int inputYear, int inputMonth)
        {
            var shoprentalBalances = await shopRentalDbContext.ShopRentalBalance
            .Where(sh => sh.PropertyId == propertyId && sh.ShopId == shopId && sh.IsCompleted == false && sh.IsHold ==0 && sh.Year <= inputYear && (1 <= sh.Month && sh.Month <= inputMonth))
            .Include(pr => pr.Property)
            .Include(s => s.Shop)
            .ToListAsync();

            return shoprentalBalances;
        }

        public async Task<IEnumerable<ShopRentalBalance>> GetAllNotCompletedBalanceByShopId(int shopId)
        {
            var shoprentalBalances = await shopRentalDbContext.ShopRentalBalance
            .Where(sh => sh.ShopId == shopId && sh.IsCompleted == false)
            .ToListAsync();

            return shoprentalBalances;
        }


        public async Task<IEnumerable<ShopRentalBalance>> GetAllBalanceByShopId(int shopId)
        {
            var shoprentalBalances = await shopRentalDbContext.ShopRentalBalance
            .Where(sh => sh.ShopId == shopId)
            .ToListAsync();

            return shoprentalBalances;
        }

        public async Task<IEnumerable<ShopRentalBalance>> GetForMonthendProcessBackup(int sabhaId)
        {
            var shoprentalBalances = await shopRentalDbContext.ShopRentalBalance
            .Include(s => s.Shop)
            .Where(s => s.Shop.SabhaId == sabhaId)
            .ToListAsync();

            return shoprentalBalances;
        }

        public async Task<IEnumerable<ShopRentalBalance>> GetForFineProcessBackup(int sabhaId, int ProcessConfigId)
        {
            var shoprentalBalances = await shopRentalDbContext.ShopRentalBalance
            .Include(s => s.Shop)
            .Where(s => s.Shop.SabhaId == sabhaId && s.Shop.ProcessConfigurationSettingAssign.ShopRentalProcessConfigarationId == ProcessConfigId)
            .ToListAsync();

            return shoprentalBalances;
        }

        public async Task<bool> HasShoprentalBalanceForOffice(int officeId)
        {
            
            return shopRentalDbContext.ShopRentalBalance
            .Include(s => s.Shop)
            .Any(s => s.OfficeId == officeId);
        }

        public async Task<bool> HasShoprentalBalanceForSabha(int sabhaId)
        {

            return shopRentalDbContext.ShopRentalBalance
            .Include(s => s.Shop)
            .Any(s => s.SabhaId == sabhaId);
        }



        public async Task<ShopRentalBalance> GetShopRentalBalanceByYearMonth(int year, int month, int propertyId, int shopId)
        {
            var shoprentalYearBalance = await shopRentalDbContext.ShopRentalBalance
                .Include(pr => pr.Property)
                .Include(s => s.Shop)
                .FirstOrDefaultAsync(sh => sh.Year == year && sh.Month == month && sh.PropertyId == propertyId && sh.ShopId == shopId);

            return shoprentalYearBalance;
        }
        //******************************************************************************************













        public async Task<ShopRentalBalance> GetShopRentalNotCompletedBalanceByYearMonth(int year, int month, int shopId)
        {
            var shoprentalYearBalance = await shopRentalDbContext.ShopRentalBalance
                .Include(pr => pr.Property)
                .Include(s => s.Shop)
                .FirstOrDefaultAsync(sh => sh.Year == year && sh.Month == month && sh.ShopId == shopId && sh.IsCompleted == false);

            return shoprentalYearBalance;
        }


        //----------------------update
        public async Task<IEnumerable<ShopRentalBalance>> GetAllBalancesOfYear(int propertyId, int shopId, int inputYear)
        {
            var shoprentalBalancesOfYear = await shopRentalDbContext.ShopRentalBalance
                 .Where(sh => sh.PropertyId == propertyId && sh.ShopId == shopId && sh.IsCompleted == false && sh.Year == inputYear)
                 .Include(pr => pr.Property)
                 .Include(s => s.Shop)
                 .ToListAsync();

            return shoprentalBalancesOfYear;
        }

        public async Task<IEnumerable<ShopRentalBalance>> GetAllBalanceInYearUpTOMonth(int propertyId, int shopId, int inputYear, int inputMonth)
        {
            var shoprentalBalancesInYearUpTOMonth = await shopRentalDbContext.ShopRentalBalance
             .Where(sh => sh.PropertyId == propertyId && sh.ShopId == shopId && sh.IsCompleted == false && sh.Year == inputYear && (1 <= sh.Month && sh.Month <= inputMonth))
             .Include(pr => pr.Property)
             .Include(s => s.Shop)
             .ToListAsync();

            return shoprentalBalancesInYearUpTOMonth;
        }

        //Get first record
        public async Task<ShopRentalBalance> GetBalanceInfo(int propertyId, int shopId)
        {
            var shoprentalYearBalance = await shopRentalDbContext.ShopRentalBalance
                .Include(pr => pr.Property)
                .Include(s => s.Shop)
                .FirstOrDefaultAsync(sh => sh.PropertyId == propertyId && sh.ShopId == shopId);

            return shoprentalYearBalance;
        }

        public async Task<ShopRentalBalance> GetMonthlyBalanceInfo(int propertyId, int shopId, int year, int monthId)
        {
            var shoprentalYearBalance = await shopRentalDbContext.ShopRentalBalance
                .Include(pr => pr.Property)
                .Include(s => s.Shop)
                .FirstOrDefaultAsync(sh => sh.PropertyId == propertyId && sh.ShopId == shopId && sh.Year == year && sh.Month == monthId && sh.IsCompleted == false);

            return shoprentalYearBalance;
        }

        public async Task<IEnumerable<ShopRentalBalance>> GetNotCompletedBalancesByShopIdsByYearMonth(List<int?> ShopIds, int year, int month)
        {
            return await shopRentalDbContext.ShopRentalBalance
                .Include(pr => pr.Property)
                //.ThenInclude(r => r.RentalPlace)
                .Include(s => s.Shop)
                .Where(bl => ShopIds.Contains(bl.ShopId) && bl.Year == year && bl.Month == month && bl.IsCompleted == false).ToListAsync();
        }





        public async Task<ShopRentalBalance> GetShopRentalBalanceByYearMonthForReversePayment(int year, int month, int shopId)
        {
            var shoprentalYearBalance = await shopRentalDbContext.ShopRentalBalance
            .Include(pr => pr.Property)
            .Include(s => s.Shop)
            .FirstOrDefaultAsync(sh => sh.Year == year && sh.Month == month && sh.ShopId == shopId);

            return shoprentalYearBalance;
        }

        public async Task<IEnumerable<ShopRentalBalance>> GetForOrderTransaction(int shopIds) => await shopRentalDbContext.ShopRentalBalance.Where(b => b.ShopId == shopIds).ToListAsync();
        public async Task<IEnumerable<ShopRentalBalance>> GetForOrderTransaction(List<int?> shopIds)
        {
            return await shopRentalDbContext.ShopRentalBalance.Where(b => shopIds.Contains(b.ShopId)).ToListAsync();

        }
        //----------------------------
    }
}
