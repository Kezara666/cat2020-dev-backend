using CAT20.Common.Enums;
using CAT20.Common.Envelop;
using CAT20.Common;
using CAT20.Core.Models.Interfaces;
using CAT20.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services
{
    public abstract class BizObjectBase<T> : LoggerBase, IBizObjectBase<T> where T : class, IEntityBaseNew
    {
        IRepository<T> _repository;
        public BizObjectBase() { }
        public BizObjectBase(IRepository<T> repository) { _repository = repository; }

        /// <summary>
        /// Get Object with related Objects
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T GetSingle(int id)
        {
            T returObject = null;
            try
            {
                returObject = _repository.GetSingle(t => t.ID == id);
            }
            catch (Exception exp)
            {


                LogError(exp.Message);
            }
            return returObject;
        }

        /// <summary>
        /// Save Object T to underline Database
        /// </summary>
        /// <param name="entry"></param>
        public virtual TransferObject<T> Save(T entry)
        {
            TransferObject<T> transferObject = new TransferObject<T>(entry, new StatusInfo(), string.Empty);
            try
            {
                int staus = _repository.Save(entry);
                transferObject.StatusInfo.Status = ServiceStatus.Success;
                return transferObject;
            }

            catch (Exception exp)
            {
                transferObject.StatusInfo.Status = ServiceStatus.DatabaseFailure;
                var exceptionName = exp.GetType().Name;
                switch (exceptionName)
                {
                    case "DbUpdateConcurrencyException":
                        transferObject.StatusInfo.Message = Resources.MessageDictionary.CONCURRECNCY_UPDATE_ERROR;
                        break;
                    default:
                        transferObject.StatusInfo.Message = exp.Message;
                        break;
                }


                LogError(exp.Message);
            }
            return transferObject;
        }

        /// <summary>
        /// Return all the objects in the graph. If Any Object is changed in the UI, 
        /// those need to be updated using the property values
        /// </summary>
        /// <returns></returns>
        public virtual IList<T> GetAll()
        {
            return _repository.GetAll();
        }

        /// <summary>
        /// Return the last ten modified objects in the graph. If Any Object is changed in the UI, 
        /// those need to be updated using the property values
        /// </summary>
        /// <returns></returns>
        public virtual IList<T> GetLastTenModified(int domainID)
        {
            return _repository.GetLastTenModified(domainID);
        }

        /// <summary>
        /// Return the last ten created objects in the graph. If Any Object is changed in the UI, 
        /// those need to be updated using the property values
        /// </summary>
        /// <returns></returns>
        public virtual IList<T> GetLastTenCreated(int domainID)
        {
            return _repository.GetLastTenCreated(domainID);
        }

        /// <summary>
        /// Return all the objects in the graph. If Any Object is changed in the UI, 
        /// those need to be updated using the property values
        /// </summary>
        /// <returns></returns>
        public virtual IList<T> GetAll(Expression<Func<T, bool>> whereCondition)
        {
            return _repository.GetAll(whereCondition);
        }

        /// <summary>
        /// Return the object as a Querybale Collection
        /// </summary>
        /// <param name="whereCondition"></param>
        /// <returns></returns>
        public virtual IQueryable<T> Query(Expression<Func<T, bool>> whereCondition)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retun the number of stockOrderItems in the collection for the given Expression
        /// </summary>
        /// <param name="whereCondition"></param>
        /// <returns></returns>
        public virtual long Count(Expression<Func<T, bool>> whereCondition)
        {
            return _repository.GetAll(whereCondition).Count();
        }

        /// <summary>
        /// Retun the Count for the given Objetcs
        /// </summary>
        /// <returns></returns>
        public virtual long Count()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create an Intstance of the type T
        /// </summary>
        /// <returns></returns>
        public virtual T Create()
        {
            var instance = Activator.CreateInstance(typeof(T)) as T;
            return instance;
        }

        /// <summary>
        /// Create an Intstance of the type T for the ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T Create(int id)
        {
            var instance = Activator.CreateInstance(typeof(T)) as T;
            instance.ID = id;
            return instance;
        }

        /// <summary>
        /// Get proxy Objects
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T GetProxy(int id)
        {
            T returObject = null;
            try
            {
                returObject = _repository.GetProxy(t => t.ID == id);
            }
            catch (Exception exp)
            {

                LogError(exp.Message);
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
                _repository.Delete(id);
                transferObject.StatusInfo.Status = ServiceStatus.Success;
                transferObject.StatusInfo.Message = Resources.MessageDictionary.SUCCESSFULLY_DELETED;
            }
            catch (Exception exp)
            {
                LogError(exp.Message);
                transferObject = new ReferentialIntegrityExceptionHandler(exp).GetTransferObject();
            }
            return transferObject;
        }
    }
}