using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.Interfaces;
using CAT20.Core.Models.ShopRental;
using CAT20.Core.Repositories.ShopRental;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.ShopRental
{
    public class PropertyRepository : Repository<Property>, IPropertyRepository
    {
        public PropertyRepository(DbContext context) : base(context)
        {
        }

        public async Task<Property> GetById(int id)
        {
            return await shopRentalDbContext.Properties
                
                .Where(p => p.Id == id)
                .Include(f => f.Floor)
                .ThenInclude(r => r.RentalPlace)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Property>> GetAll()
        {
            return
            await shopRentalDbContext.Properties
                .Include(s => s.Shops)
                .ToListAsync();
        }

        public async Task<IEnumerable<Property>> GetAllForOffice(int officeId)
        {
           var result=
                await shopRentalDbContext.Properties
                .Include(pr => pr.PropertyType)
                .Include(prn => prn.PropertyNature)
                .Include(s => s.Shops)
                .Include(f => f.Floor)
                .ThenInclude(r => r.RentalPlace)
                .Where(m => m.OfficeId == officeId)
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Property>> GetAllForSabha(int sabhaId)
        {
            return
                await shopRentalDbContext.Properties
                .Include(pr => pr.PropertyType)
                .Include(prn => prn.PropertyNature)
                .Include(s => s.Shops)
                .Include(f => f.Floor)
                .ThenInclude(r => r.RentalPlace)
                .Where(m => m.SabhaId == sabhaId)
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Property>> GetAllByPropertyTypeAndSabha(int propertytypeId, int sabhaId)
        {
            return
                await shopRentalDbContext.Properties
                .Where(m => m.SabhaId == sabhaId && m.PropertyTypeId == propertytypeId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Property>> GetAllByPropertyTypeAndOffice(int propertytypeId, int officeId)
        {
            return
                await shopRentalDbContext.Properties
                .Where(m => m.OfficeId == officeId && m.PropertyTypeId == propertytypeId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Property>> GetAllByPropertyNature(int propertyNatureId)
        {
            return
                await shopRentalDbContext.Properties
                .Where(m =>  m.PropertyNatureId == propertyNatureId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Property>> GetAllByPropertyType(int propertyTypeId)
        {
            return
                await shopRentalDbContext.Properties
                .Where(m => m.PropertyTypeId == propertyTypeId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Property>> GetAllByPropertyNatureAndSabha(int propertyNatureId, int sabhaId)
        {
            return
                await shopRentalDbContext.Properties
                .Where(m => m.SabhaId == sabhaId && m.PropertyNatureId == propertyNatureId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Property>> GetAllByPropertyNatureAndOffice(int propertyNatureId, int officeId)
        {
            return
                await shopRentalDbContext.Properties
                .Where(m => m.OfficeId == officeId && m.PropertyNatureId == propertyNatureId)
                .ToListAsync();
        }

        public async Task<Property> GetByPropertyNo(string propertyNo)
        {
            return await shopRentalDbContext.Properties
                .Where(p => p.PropertyNo == propertyNo)
                .FirstOrDefaultAsync();
        }

        //public async Task<IEnumerable<Property>> GetAllForRentalPlace(int rentalPlaceId)
        //{
        //    return
        //        await shopRentalDbContext.Properties
        //        .Where(m => m.RentalPlaceId == rentalPlaceId)
        //        .ToListAsync();
        //}

        public async Task<IEnumerable<Property>> GetAllForFloor(int floorId)
        {
            return
                await shopRentalDbContext.Properties
                .Include(pr => pr.PropertyType)
                .Include(prn => prn.PropertyNature)
                .Include(s => s.Shops)
                .Include(f => f.Floor)
                .ThenInclude(r => r.RentalPlace)
                .Where(m => m.FloorId == floorId)
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync();
        }

        //----
        public async Task<IEnumerable<Property>> GetAllZeroOpeningBlannceForFloor(int floorId)
        {
            return
                await shopRentalDbContext.Properties
                .Include(s => s.Shops)
                .Where(m => m.FloorId == floorId && m.OpeningBalances == null)
                .ToListAsync();
        }
        //-----

        public async Task<IEnumerable<Property>> GetAllZeroVotesForFloor(int floorId)
        {
            return
                await shopRentalDbContext.Properties
                .Include(s => s.Shops)
                .Where(m => m.FloorId == floorId && m.ShopRentalVoteAssigns == null)
                .ToListAsync();
        }
        //----


        //----
        public async Task<IEnumerable<Property>> GetAllZeroShopsForFloor(int floorId)
        {
            return await shopRentalDbContext.Properties
                .Include(s => s.Shops)
                .Where(m => m.FloorId == floorId && m.Shops.All(s => s.Status != ShopStatus.Active))
                .ToListAsync();
        }

        public async Task<IEnumerable<Property>> GetByIds(List<int?> propertyKeyIds)
        {
            return await shopRentalDbContext.Properties.Where(prop => propertyKeyIds.Contains(prop.Id)).ToListAsync();
        }

        //----

        private ShopRentalDbContext shopRentalDbContext
        {
            get { return Context as ShopRentalDbContext; }
        }

    }
}