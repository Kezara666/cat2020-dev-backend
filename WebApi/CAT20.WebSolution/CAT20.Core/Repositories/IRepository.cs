using CAT20.Common.Envelop;
using System.Linq.Expressions;


namespace CAT20.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        ValueTask<TEntity> GetByIdAsync(int id);
        ValueTask<TEntity> GetByIdAsync(int? id);
        
        Task<IEnumerable<TEntity>> GetAllAsync();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task AddAsync(TEntity entity);
         void AddAsync1(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> filter = null,
                                          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                          int? top = null,
                                          int? skip = null,
                                          params string[] includeProperties);


        //newly added
        /// <summary>
        /// Create a object Type T
        /// </summary>
        /// <returns></returns>
        TEntity Create();

        /// <summary>
        /// Create a objecTEntity Type T
        /// </summary>
        /// <returns></returns>
        TEntity Create(int id);

        /// <summary>
        /// Get a selected extiry by the object primary key ID
        /// The Object contains the second level objects & Lists
        /// </summary>
        /// <param name="id">Primary key ID</param>
        TEntity GetSingle(int id);

        /// <summary>
        /// Add or Edit entry depending on the entity state
        /// </summary>
        /// <param name="entry"></param>
        TransferObject<TEntity> Save(TEntity entry);

        /// <summary>
        /// Get all the element of this repository
        /// </summary>
        /// <returns></returns>
        IList<TEntity> GetAll();

        /// <summary>
        /// Get the last ten modified elements of this repository
        /// </summary>
        /// <returns></returns>
        IList<TEntity> GetLastTenModified(int domainID);

        /// <summary>
        /// Get the last ten created elements of this repository
        /// </summary>
        /// <returns></returns>
        IList<TEntity> GetLastTenCreated(int domainID);

        /// <summary>
        /// Get all the element of this repository
        /// </summary>
        /// <returns></returns>
        IList<TEntity> GetAll(Expression<Func<TEntity, bool>> whereCondition);

        /// <summary>
        /// Count using a filer
        /// </summary>
        long Count(Expression<Func<TEntity, bool>> whereCondition);

        /// <summary>
        /// Get a selected proxy by the object primary key ID
        /// Does not load the seond level objects
        /// </summary>
        /// <param name="id">Primary key ID</param>
        TEntity GetProxy(int id);

        /// <summary>
        /// All stockObj count
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        long Count();

        /// <summary>
        /// Delete Entity
        /// </summary>
        /// <param name="id">Entity ID</param>
        /// <returns></returns>
        TransferObject<bool> Delete(int id);

    }
}
