using CAT20.Common.Enums;
using CAT20.Common.Envelop;
using CAT20.Common;
using CAT20.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using CAT20.Data.Resources;
using CAT20.Data.ExceptionHandler;

namespace CAT20.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            this.Context = context;
        }
        public async Task AddAsync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
        }
        public  void AddAsync1(TEntity entity)
        {
             Context.Set<TEntity>().Add(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await Context.Set<TEntity>().AddRangeAsync(entities);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate).ToList();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public ValueTask<TEntity> GetByIdAsync(int id)
        {
            return Context.Set<TEntity>().FindAsync(id);
        }
        public ValueTask<TEntity> GetByIdAsync(int? id)
        {
            return Context.Set<TEntity>().FindAsync(id);
        }

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }

        public Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().SingleOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetManyAsync(
          Expression<Func<TEntity, bool>> filter = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          int? top = null,
          int? skip = null,
          params string[] includeProperties)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties.Length > 0)
            {
                query = includeProperties.Aggregate(query, (theQuery, theInclude) => theQuery.Include(theInclude));
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (top.HasValue)
            {
                query = query.Take(top.Value);
            }

            return await query.ToListAsync();
        }




        //newlyadded

        public virtual TEntity GetSingle(int id)
        {
            TEntity returObject = null;
            try
            {
                returObject = Context.Set<TEntity>().Find(id);
                //returObject = Context.Set<TEntity>().SingleOrDefault(t => t.ID == id);
            }
            catch (Exception exp)
            {
                // LogError(exp.Message);
            }
            return returObject;
        }


        /// <summary>
        /// Save Object TEntity to underline Database
        /// </summary>
        /// <param name="entry"></param>
        public virtual TransferObject<TEntity> Save(TEntity entry)
        {
            TransferObject<TEntity> transferObject = new TransferObject<TEntity>(entry, new StatusInfo(), string.Empty);
            try
            {
                Context.Set<TEntity>().Add(entry);
                //Context.SaveChanges();
                transferObject.StatusInfo.Status = ServiceStatus.Success;
                return transferObject;
            }
            catch (Exception exp)
            {
                // Handle exceptions and log errors as needed
                transferObject.StatusInfo.Status = ServiceStatus.DatabaseFailure;
                transferObject.StatusInfo.Message = exp.Message;
                //LogError(exp.Message);
            }
            return transferObject;
        }

        /// <summary>
        /// Return all the objects in the graph. If Any Object is changed in the UI, 
        /// those need to be updated using the property values
        /// </summary>
        /// <returns></returns>
        public virtual IList<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        /// <summary>
        /// Return the last ten modified objects in the graph. If Any Object is changed in the UI, 
        /// those need to be updated using the property values
        /// </summary>
        /// <returns></returns>
        public virtual IList<TEntity> GetLastTenModified(int domainID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return the last ten created objects in the graph. If Any Object is changed in the UI, 
        /// those need to be updated using the property values
        /// </summary>
        /// <returns></returns>
        public virtual IList<TEntity> GetLastTenCreated(int domainID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return all the objects in the graph. If Any Object is changed in the UI, 
        /// those need to be updated using the property values
        /// </summary>
        /// <returns></returns>
        public virtual IList<TEntity> GetAll(Expression<Func<TEntity, bool>> whereCondition)
        {
            return Context.Set<TEntity>().Where(whereCondition).ToList();
        }

        /// <summary>
        /// Return the object as a Querybale Collection
        /// </summary>
        /// <param name="whereCondition"></param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> whereCondition)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retun the number of stockOrderItems in the collection for the given Expression
        /// </summary>
        /// <param name="whereCondition"></param>
        /// <returns></returns>
        public virtual long Count(Expression<Func<TEntity, bool>> whereCondition)
        {
            return Context.Set<TEntity>().Count(whereCondition);
        }

        /// <summary>
        /// Retun the Count for the given Objetcs
        /// </summary>
        /// <returns></returns>
        public virtual long Count()
        {
            return Context.Set<TEntity>().Count();
        }

        /// <summary>
        /// Create an Intstance of the type T
        /// </summary>
        /// <returns></returns>
        public virtual TEntity Create()
        {
            var instance = Activator.CreateInstance(typeof(TEntity)) as TEntity;
            return instance;
        }

        /// <summary>
        /// Create an Intstance of the type TEntity for the ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntity Create(int id)
        {
            var instance = Activator.CreateInstance(typeof(TEntity)) as TEntity;
            // Assuming ID is a property in TEntity class, replace with the actual property name
            typeof(TEntity).GetProperty("Id").SetValue(instance, id);
            return instance;
        }

        /// <summary>
        /// Get proxy Objects
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntity GetProxy(int id)
        {
            TEntity returObject = null;
            try
            {
                //returObject = Context.Set<TEntity>().SingleOrDefault(t => t.ID == id);
                returObject = Context.Set<TEntity>().Find(id);
            }
            catch (Exception exp)
            {
                //LogError(exp.Message);
                Console.WriteLine(exp.Message);
            }
            return returObject;
        }

        /// <summary>
        /// Delete Entity
        /// </summary>
        /// <param name="id">Entity ID</param>
        /// <returns></returns>
        public TransferObject<bool> Delete(int id)
        {
            TransferObject<bool> transferObject = new TransferObject<bool>(false, new StatusInfo(), string.Empty);
            try
            {
                TEntity entityToDelete = Context.Set<TEntity>().Find(id);
                if (entityToDelete != null)
                {
                    Context.Set<TEntity>().Remove(entityToDelete);
                    Context.SaveChanges();
                    transferObject.StatusInfo.Status = ServiceStatus.Success;
                    transferObject.StatusInfo.Message = Resources.MessageDictionary.SUCCESSFULLY_DELETED;
                }
                else
                {
                    //transferObject.StatusInfo.Message = Resources.MessageDictionary.NOT_FOUND;
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
                transferObject = new ReferentialIntegrityExceptionHandler(exp).GetTransferObject();
            }
            return transferObject;
        }
    }
}