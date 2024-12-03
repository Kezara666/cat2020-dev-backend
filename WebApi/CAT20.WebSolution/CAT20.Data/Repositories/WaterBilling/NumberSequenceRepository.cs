using CAT20.Core.Models.WaterBilling;
using CAT20.Core.Repositories;
using CAT20.Core.Repositories.WaterBilling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.WaterBilling
{
    public class NumberSequenceRepository : Repository<NumberSequence>, INumberSequenceRepository
    {
        public NumberSequenceRepository(DbContext context) : base(context)
        {
        }

        public async Task<NumberSequence> GetNumberByOfficeId(int officeId)
        {
            return await waterBillingDbContext.NumberSequences.FirstOrDefaultAsync(ns => ns.OfficeId == officeId);
        }


        public async Task UpdateAsync(NumberSequence entity)
        {
            
            var entityToUpdate = waterBillingDbContext.NumberSequences.SingleOrDefault(e => e.Id == entity.Id);

            try
            {
                entityToUpdate.CoreNumber = entity.CoreNumber;
                //waterBillingDbContext.SaveChanges();
            }catch (Exception ex)
            {
                throw;

            }
        }

        public async Task UpdateCoreNumberAsync(NumberSequence entity)
        {
            var entityToUpdate = waterBillingDbContext.NumberSequences.SingleOrDefault(e => e.Id == entity.Id);

            try
            {
                entityToUpdate.CoreNumber = entity.CoreNumber;
                //waterBillingDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;

            }
        }

        public async Task UpdateApplicationNumberAsync(NumberSequence entity)
        {
            var entityToUpdate = waterBillingDbContext.NumberSequences.SingleOrDefault(e => e.Id == entity.Id);

            try
            {
                entityToUpdate.ApplicationNumber = entity.ApplicationNumber;
                //waterBillingDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;

            }
        }

        private WaterBillingDbContext waterBillingDbContext
        {
            get { return Context as WaterBillingDbContext; }
        }
    }
}
