using CAT20.Core.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CAT20.Common.Envelop;

namespace CAT20.Core.Repositories
{
    public interface IBizObjectBase<T> where T : class, IEntityBaseNew
    {
        /// <summary>
        /// Create a object Type T
        /// </summary>
        /// <returns></returns>
        T Create();

        /// <summary>
        /// Create a object Type T
        /// </summary>
        /// <returns></returns>
        T Create(int id);

        /// <summary>
        /// Get a selected extiry by the object primary key ID
        /// The Object contains the second level objects & Lists
        /// </summary>
        /// <param name="id">Primary key ID</param>
        T GetSingle(int id);

        /// <summary>
        /// Add or Edit entry depending on the entity state
        /// </summary>
        /// <param name="entry"></param>
        TransferObject<T> Save(T entry);

        /// <summary>
        /// Get all the element of this repository
        /// </summary>
        /// <returns></returns>
        IList<T> GetAll();

        /// <summary>
        /// Get the last ten modified elements of this repository
        /// </summary>
        /// <returns></returns>
        IList<T> GetLastTenModified(int domainID);

        /// <summary>
        /// Get the last ten created elements of this repository
        /// </summary>
        /// <returns></returns>
        IList<T> GetLastTenCreated(int domainID);

        /// <summary>
        /// Get all the element of this repository
        /// </summary>
        /// <returns></returns>
        IList<T> GetAll(Expression<Func<T, bool>> whereCondition);

        /// <summary>
        /// Count using a filer
        /// </summary>
        long Count(Expression<Func<T, bool>> whereCondition);

        /// <summary>
        /// Get a selected proxy by the object primary key ID
        /// Does not load the seond level objects
        /// </summary>
        /// <param name="id">Primary key ID</param>
        T GetProxy(int id);

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
