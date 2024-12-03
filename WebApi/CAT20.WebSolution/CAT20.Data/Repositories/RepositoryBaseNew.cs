using CAT20.Core.Models.Interfaces;
using CAT20.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CAT20.Data.Repositories
{
    public abstract class RepositoryBaseNew<T> : IRepositoryNew<T> where T : class, IEntityBaseNew
    {
        #region Vars

        private IRepositoryContextNew _repositoryContext;
        #endregion

        #region Constructors

        //public RepositoryBaseNew() : this(new RepositoryContextNew()) { }
        public RepositoryBaseNew(IRepositoryContextNew context)
        {
            this._repositoryContext = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _repositoryContext.GetDbSet<T>();
        }

        #endregion

        #region Properties

        private DbSet<T> _dbSet;
        public DbSet<T> DbSet { get { return _dbSet; } }

        public RepositoryContextNew RepositoryContext { get { return (RepositoryContextNew)_repositoryContext; } }

        #endregion

        #region GetAll(Expression)

        public IList<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition)
        {
            return this.DbSet.Where(whereCondition).AsNoTracking().ToList<T>();
        }

        #endregion

        #region GetAll

        public IList<T> GetAll()
        {
            return this.DbSet.ToList<T>();
        }

        #endregion

        #region GetLastTenModified

        public IList<T> GetLastTenModified(int domainID)
        {
            return this.DbSet.OrderByDescending(t => t.DateModified).Take(10).ToList<T>();
        }

        #endregion

        #region GetLastTenCreated

        public IList<T> GetLastTenCreated(int domainID)
        {
            return this.DbSet.OrderByDescending(t => t.DateCreated).Take(10).ToList<T>();
        }

        #endregion

        #region GetQueryble

        public IQueryable<T> GetQueryable()
        {
            return this.DbSet.AsQueryable<T>();
        }

        #endregion

        #region Save

        private string _dataConnectionString = "name=" + ConfigSettings.DB_CONNECTION_STRING;

        public int Save(T entity)
        {
            int returnValue = 0;
            //var nonAuditableProperties = GetNonAuditableProperties();
            //var hasAuditTrail = false;
            //AuditTrail auditTrail = null;

            //using (var context = new Context.PahansoftERPDbContext(_dataConnectionString))
            //{
            //    //context.Configuration.AutoDetectChangesEnabled = false;
            //    //context.Configuration.ValidateOnSaveEnabled = false;
            //    context.Set<T>().Add(entity);

            //    foreach (var entry in context.ChangeTracker.Entries<IEntityBase>())
            //    {
            //        try
            //        {
            //            IEntityBaseNew stateInfo = entry.Entity;
            //            entry.State = ConvertState(stateInfo.State);
            //        }
            //        catch (Exception ex)
            //        {
            //            Console.WriteLine(ex.Message);
            //        }

            //        #region Get Audit Trail
            //        //if (hasAuditTrail)
            //        //{
            //        //    var name = entry.Entity.GetType().Name;

            //        //    if ((entry.State == EntityState.Added || entry.State == EntityState.Modified) && (Enum.IsDefined(typeof(AuditTrailSourceType), entry.Entity.GetType().Name)))
            //        //    {
            //        //        if (hasAuditTrail)
            //        //        {
            //        //            foreach (var auditTrailEntry in AuditTrailManager(entry, nonAuditableProperties).AuditTrailEntries)
            //        //            {
            //        //                auditTrailEntry.AuditTrail = auditTrail;
            //        //                auditTrail.AuditTrailEntries.Add(auditTrailEntry);
            //        //            }
            //        //        }
            //        //        else
            //        //        {
            //        //            auditTrail = AuditTrailManager(entry, nonAuditableProperties);
            //        //            if (auditTrail != null)
            //        //            {
            //        //                auditTrail.SourceID = entity.ID;
            //        //                hasAuditTrail = true;
            //        //            }
            //        //        }
            //        //    }
            //        //}
            //        #endregion
            //    }

            //    #region Add Audit Trail to context 
            //    //if (auditTrail != null)
            //    //{
            //    //    context.AuditTrails.Add(auditTrail);
            //    //}
            //    #endregion

            //    returnValue = context.SaveChanges();
            //    entity.State = State.Unchanged;
            //}
            return returnValue;
        }

        #endregion

        #region GetProxy

        public T GetProxy(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition)
        {
            var returnValue = this.DbSet.Where(whereCondition).FirstOrDefault<T>();
            if (returnValue != null)
                returnValue.State = State.Unchanged;
            return returnValue;
        }

        #endregion

        #region GetSingle

        public virtual T GetSingle(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition)
        {
            return DbSet.Where(whereCondition).FirstOrDefault();
        }

        #endregion

        #region Change Status

        private EntityState ConvertState(State state)
        {
            switch (state)
            {
                case State.Added:
                    return EntityState.Added;
                case State.Modified:
                    return EntityState.Modified;
                case State.Deleted:
                    return EntityState.Deleted;
                default:
                    return EntityState.Unchanged;
            }
        }

        #endregion

        #region Audit Trail Helpers

        //private List<string> GetNonAuditableProperties()
        //{
        //    return new List<string>() { "TimeStamp", "DomainID", "IsArchived", "State", "User", "CreatedDate", "DateCreated", "DateModified" };
        //}

        //private AuditTrail AuditTrailManager(DbEntityEntry<IEntityBase> entry, List<string> nonAuditableProperties)
        //{
        //    try
        //    {
        //        AuditTrailSourceType sourceType = (AuditTrailSourceType)Enum.Parse(typeof(AuditTrailSourceType), entry.Entity.GetType().Name);
        //        if (sourceType != null)
        //        {
        //            AuditTrail auditTrail = new AuditTrail();

        //            auditTrail.DomainID = entry.Entity.DomainID;
        //            auditTrail.User = entry.Entity.User;
        //            auditTrail.SourceType = sourceType;
        //            auditTrail.SourceTypeDescription = entry.Entity.GetType().Name;
        //            auditTrail.State = State.Added;
        //            auditTrail.RevisionStamp = DateTime.Now;
        //            auditTrail.DateCreated = DateTime.Now;
        //            auditTrail.EmployeeID = (new EmployeeRepository()).GetEmployeeByUserName(entry.Entity.User, entry.Entity.DomainID.Value).ID;

        //            #region Audit Trail Entries

        //            auditTrail.AuditTrailEntries = new List<AuditTrailEntry>();
        //            switch (entry.State)
        //            {
        //                case EntityState.Added:
        //                    auditTrail.Action = AuditTrailAction.Created;
        //                    GetAditTrailEntries(entry, true, nonAuditableProperties, auditTrail, sourceType);
        //                    break;
        //                case EntityState.Modified:
        //                    auditTrail.Action = AuditTrailAction.Modified;
        //                    GetAditTrailEntries(entry, false, nonAuditableProperties, auditTrail, sourceType);
        //                    break;
        //                case EntityState.Deleted:
        //                    auditTrail.Action = AuditTrailAction.Deleted;
        //                    GetAditTrailEntries(entry, false, nonAuditableProperties, auditTrail, sourceType);
        //                    break;
        //            }

        //            #endregion
        //            return auditTrail;
        //        }
        //        else
        //            return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        //private void GetAditTrailEntries(DbEntityEntry<IEntityBase> entry, bool isNew, List<string> nonAuditableProperties, AuditTrail auditTrail, AuditTrailSourceType sourceType)
        //{

        //    StringBuilder stringBuilder = new StringBuilder();
        //    StringBuilder stringTopBuilder = new StringBuilder();
        //    bool isUpdated = false;
        //    try
        //    {
        //        if (isNew)
        //        {
        //            foreach (string propertyName in entry.CurrentValues.PropertyNames)
        //            {
        //                try
        //                {
        //                    if (!nonAuditableProperties.Contains(propertyName))
        //                    {
        //                        if ((propertyName == "Code") || (propertyName == "TransactionNumber")
        //                            || (propertyName == "QuoteNumber") || (propertyName == "JobNumber") || (propertyName == "AddressCode") || (propertyName == "PrePayNumber") || (propertyName == "BillPaymentNumber") || (propertyName == "MiscellaneousPaymentNumber")
        //                             || (propertyName == "PeriodicalPaymentID") || (propertyName == "LedgerAccountID") || (propertyName == "DepartmentName") || (propertyName == "ReceiptNumber") || (propertyName == "CreditNoteNumber") || (propertyName == "MiscellaneousIncomeNumber"))

        //                        {
        //                            if ((sourceType == AuditTrailSourceType.Job) && (propertyName == "QuoteNumber"))
        //                                continue;
        //                            if ((sourceType == AuditTrailSourceType.PeriodicalPayment) && (propertyName == "Code"))
        //                            {
        //                                isUpdated = true;
        //                                stringTopBuilder.Append("[[").Append("PeriodicalPaymentNumber").Append("]] : ").Append(entry.CurrentValues[propertyName]).Append(" *** " + AuditTrailAction.Created.ToString());
        //                            }
        //                            else if ((sourceType == AuditTrailSourceType.PeriodicalPaymentDissection) && (propertyName == "PeriodicalPaymentID"))
        //                            {
        //                                isUpdated = true;
        //                                stringTopBuilder.Append("[[").Append("PeriodicalPayment").Append("]] : ").Append(entry.CurrentValues[propertyName]).Append("    ");

        //                            }
        //                            else if ((sourceType == AuditTrailSourceType.CompanyDepartment) && (propertyName == "DepartmentName"))
        //                            {
        //                                isUpdated = true;
        //                                stringTopBuilder.Append("[[").Append("Department").Append("]] : ").Append(entry.CurrentValues[propertyName]).Append(" *** " + AuditTrailAction.Created.ToString());

        //                            }
        //                            else if ((sourceType == AuditTrailSourceType.PeriodicalPaymentDissection || sourceType == AuditTrailSourceType.AccountLogEntry) && (propertyName == "LedgerAccountID"))
        //                            {
        //                                isUpdated = true;
        //                                var currentVal = entry.CurrentValues[propertyName] != null ? entry.CurrentValues[propertyName].ToString() : "";

        //                                if (propertyName == "LedgerAccountID")
        //                                {
        //                                    int id = Convert.ToInt32(currentVal);
        //                                    stringTopBuilder.Append("[[").Append("Ledger Account").Append("]] : ").Append((new LedgerAccountRepository()).GetSingle(c => c.ID == id).Code).Append(" *** " + AuditTrailAction.Created.ToString());
        //                                }
        //                            }
        //                            else
        //                            {
        //                                stringTopBuilder.Append("[[").Append(propertyName).Append("]] : ").Append(entry.CurrentValues[propertyName]).Append(" *** " + AuditTrailAction.Created.ToString());
        //                                isUpdated = true;
        //                                break;
        //                            }
        //                        }
        //                    }
        //                }
        //                catch { }
        //            }
        //        }
        //        else
        //        {
        //            foreach (string propertyName in entry.GetDatabaseValues().PropertyNames)
        //            {
        //                try
        //                {
        //                    var currentVal = entry.CurrentValues[propertyName] != null ? entry.CurrentValues[propertyName].ToString() : "";
        //                    var dbVal = entry.GetDatabaseValues()[propertyName] != null ? entry.GetDatabaseValues()[propertyName].ToString() : "";

        //                    if ((propertyName == "Code") || (propertyName == "TransactionNumber") || (propertyName == "QuoteNumber") || (propertyName == "AddressCode"))
        //                    {
        //                        stringTopBuilder.Append("[[").Append(propertyName).Append("]] : ").Append(dbVal).Append("    ");
        //                    }

        //                    // Exclude Fields
        //                    if ((propertyName == "ID") || (propertyName == "AccountCreatedByID ") || (propertyName == "AccountCreatedDate") || (propertyName == "AccountCreatedByID"))
        //                    {
        //                        continue;
        //                    }
        //                    else if (nonAuditableProperties != null)
        //                    {
        //                        if (sourceType == AuditTrailSourceType.JournalEntryItem)
        //                        {
        //                            if (propertyName == "LedgerAccountID")
        //                            {
        //                                int id = Convert.ToInt32(currentVal);
        //                                stringTopBuilder.Append("[[").Append(propertyName).Append("]] : ").Append((new LedgerAccountRepository()).GetSingle(c => c.ID == id).Code).Append("    ");
        //                            }
        //                        }
        //                        else if (sourceType == AuditTrailSourceType.BatchTransaction)
        //                        {
        //                            if (propertyName == "CashbookAccountID")
        //                            {
        //                                if (currentVal == "")
        //                                    break;
        //                                int id = Convert.ToInt32(currentVal);

        //                                stringTopBuilder.Append("[[").Append(propertyName).Append("]] : ").Append((new CashbookAccountRepository()).GetSingle(c => c.ID == id).Code).Append("    ");
        //                            }
        //                            if (propertyName == "ControlTotal")
        //                            {

        //                                stringTopBuilder.Append("[[").Append(propertyName).Append("]] : ").Append(currentVal).Append("    ");
        //                            }

        //                        }

        //                        if (!nonAuditableProperties.Contains(propertyName))
        //                        {
        //                            if (currentVal != dbVal)
        //                            {
        //                                isUpdated = true;
        //                                #region Retrive Reference Code using reference ID

        //                                switch (propertyName)
        //                                {
        //                                    case "SubAccountID":
        //                                        if (currentVal != null)
        //                                        {
        //                                            int id = Convert.ToInt32(currentVal);
        //                                            currentVal = (new SubAccountRepository()).GetSingle(c => c.ID == id).Code;
        //                                        }
        //                                        if (dbVal != null)
        //                                        {
        //                                            int id = Convert.ToInt32(dbVal);
        //                                            dbVal = (new SubAccountRepository()).GetSingle(c => c.ID == id).Code;
        //                                        }
        //                                        break;

        //                                    case "GLCodeID":
        //                                        if (currentVal != null)
        //                                        {
        //                                            int id = Convert.ToInt32(currentVal);
        //                                            currentVal = (new LedgerAccountRepository()).GetSingle(c => c.ID == id).Code;
        //                                        }
        //                                        if (dbVal != null)
        //                                        {
        //                                            int id = Convert.ToInt32(dbVal);
        //                                            dbVal = (new LedgerAccountRepository()).GetSingle(c => c.ID == id).Code;
        //                                        }
        //                                        break;

        //                                    case "LedgerAccountID":
        //                                        if (currentVal != null)
        //                                        {
        //                                            int id = Convert.ToInt32(currentVal);
        //                                            currentVal = (new LedgerAccountRepository()).GetSingle(c => c.ID == id).Code;
        //                                        }
        //                                        if (dbVal != null)
        //                                        {
        //                                            int id = Convert.ToInt32(dbVal);
        //                                            dbVal = (new LedgerAccountRepository()).GetSingle(c => c.ID == id).Code;
        //                                        }
        //                                        break;

        //                                    case "AccountOpenedDate":
        //                                        DateTime date;
        //                                        if (currentVal != null)
        //                                            currentVal = Convert.ToDateTime(currentVal).ToShortDateString();
        //                                        if (dbVal != null)
        //                                            dbVal = Convert.ToDateTime(dbVal).ToShortDateString();
        //                                        break;

        //                                    case "SalesRepID":
        //                                        if (currentVal != null)
        //                                        {
        //                                            int id = Convert.ToInt32(currentVal);
        //                                            currentVal = (new EmployeeRepository()).GetSingle(c => c.ID == id).Code;
        //                                        }
        //                                        if (dbVal != null)
        //                                        {
        //                                            int id = Convert.ToInt32(dbVal);
        //                                            dbVal = (new EmployeeRepository()).GetSingle(c => c.ID == id).Code;
        //                                        }
        //                                        break;
        //                                    case "EstimatorID":
        //                                        if (currentVal != null)
        //                                        {
        //                                            int id = Convert.ToInt32(currentVal);
        //                                            currentVal = (new EmployeeRepository()).GetSingle(c => c.ID == id).User;
        //                                        }
        //                                        if (dbVal != null)
        //                                        {
        //                                            int id = Convert.ToInt32(dbVal);
        //                                            dbVal = (new EmployeeRepository()).GetSingle(c => c.ID == id).Code;
        //                                        }
        //                                        break;
        //                                    case "CompanyID":
        //                                        if (currentVal != null)
        //                                        {
        //                                            int id = Convert.ToInt32(currentVal);
        //                                            currentVal = (new CompanyRepository()).GetSingle(c => c.ID == id).User;
        //                                        }
        //                                        if (dbVal != null)
        //                                        {
        //                                            int id = Convert.ToInt32(dbVal);
        //                                            dbVal = (new CompanyRepository()).GetSingle(c => c.ID == id).Code;
        //                                        }
        //                                        break;

        //                                    case "ContactID":
        //                                        if (currentVal != null)
        //                                        {
        //                                            int id = Convert.ToInt32(currentVal);
        //                                            currentVal = (new ContactRepository()).GetSingle(c => c.ID == id).User;
        //                                        }
        //                                        if (dbVal != null)
        //                                        {
        //                                            int id = Convert.ToInt32(dbVal);
        //                                            dbVal = (new ContactRepository()).GetSingle(c => c.ID == id).Code;
        //                                        }
        //                                        break;
        //                                    case "DepartmentID":
        //                                        if (currentVal != null)
        //                                        {
        //                                            int id = Convert.ToInt32(currentVal);
        //                                            currentVal = (new DepartmentRepository()).GetSingle(c => c.ID == id).User;
        //                                        }
        //                                        if (dbVal != null)
        //                                        {
        //                                            int id = Convert.ToInt32(dbVal);
        //                                            dbVal = (new DepartmentRepository()).GetSingle(c => c.ID == id).Code;
        //                                        }
        //                                        break;
        //                                    case "PrimaryContactID":
        //                                        if (currentVal != null)
        //                                        {
        //                                            int id = Convert.ToInt32(currentVal);
        //                                            currentVal = (new ContactRepository()).GetSingle(c => c.ID == id).Code;
        //                                        }
        //                                        if (dbVal != null)
        //                                        {
        //                                            if (dbVal == "")
        //                                                break;
        //                                            int id = Convert.ToInt32(dbVal);
        //                                            dbVal = (new ContactRepository()).GetSingle(c => c.ID == id).Code;
        //                                        }
        //                                        break;
        //                                    case "SecondaryContactID":
        //                                        if (currentVal != null)
        //                                        {
        //                                            int id = Convert.ToInt32(currentVal);
        //                                            currentVal = (new ContactRepository()).GetSingle(c => c.ID == id).Code;
        //                                        }
        //                                        if (dbVal != null)
        //                                        {
        //                                            if (dbVal == "")
        //                                                break;
        //                                            int id = Convert.ToInt32(dbVal);
        //                                            dbVal = (new ContactRepository()).GetSingle(c => c.ID == id).Code;
        //                                        }
        //                                        break;
        //                                    case "TermOfTradingID":
        //                                        if (currentVal != null)
        //                                        {
        //                                            if (currentVal == "")
        //                                                break;
        //                                            int id = Convert.ToInt32(currentVal);
        //                                            currentVal = (new TermOfTradingRepository()).GetSingle(c => c.ID == id).NumberOfDays.ToString();
        //                                        }
        //                                        if (dbVal != null)
        //                                        {
        //                                            if (dbVal == "")
        //                                                break;
        //                                            int id = Convert.ToInt32(dbVal);
        //                                            dbVal = (new TermOfTradingRepository()).GetSingle(c => c.ID == id).NumberOfDays.ToString();
        //                                        }
        //                                        break;

        //                                }
        //                                #endregion

        //                                stringBuilder.Append("[[").Append(propertyName).Append("]] : ").Append(dbVal + " > " + currentVal).Append("  |  ");
        //                            }
        //                        }

        //                    }
        //                    else
        //                    {
        //                        stringBuilder.Append(" *** [[" + AuditTrailAction.Created.ToString() + "]]");
        //                    }
        //                }
        //                catch (Exception ex) { }
        //            }
        //        }
        //    }
        //    catch (Exception ex) { }

        //    if (isUpdated)
        //    {
        //        AuditTrailEntry auditTrailEntry = new AuditTrailEntry();
        //        auditTrailEntry.AuditTrail = auditTrail;

        //        auditTrailEntry.State = State.Added;
        //        auditTrailEntry.DomainID = entry.Entity.DomainID;
        //        auditTrailEntry.User = entry.Entity.User;
        //        auditTrailEntry.DateCreated = DateTime.Now;
        //        auditTrailEntry.Description = stringTopBuilder.ToString() + stringBuilder.ToString();

        //        auditTrail.AuditTrailEntries.Add(auditTrailEntry);
        //    }
        //    else
        //    {
        //        auditTrail = null;
        //    }
        //}

        #endregion

        #region Delete

        public void Delete(int id)
        {
            var options = new TransactionOptions();
            options.Timeout = TimeSpan.FromMinutes(1);
            options.IsolationLevel = IsolationLevel.ReadCommitted;

            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required, options))
                {
                    var entity = GetProxy(t => t.ID == id);
                    DbSet.Attach(entity);
                    DbSet.Remove(entity);

                    _repositoryContext.SaveChanges();

                    #region Audit Trail

                    //try
                    //{
                    //    if (Enum.IsDefined(typeof(AuditTrailSourceType), entity.GetType().Name))
                    //    {
                    //        AuditTrailSourceType sourceType =
                    //            (AuditTrailSourceType)Enum.Parse(typeof(AuditTrailSourceType), entity.GetType().Name);
                    //        if (sourceType != null)
                    //        {
                    //            using (var context = new Context.PahansoftERPDbContext(_dataConnectionString))
                    //            {
                    //                AuditTrail auditTrail = new AuditTrail();
                    //                auditTrail.SourceID = entity.ID;

                    //                AuditTrailEntry auditTrailEntry = new AuditTrailEntry();
                    //                auditTrailEntry.AuditTrail = auditTrail;


                    //                #region Audit Trail Entry

                    //                StringBuilder stringBuilder = new StringBuilder();
                    //                try
                    //                {
                    //                    stringBuilder.Append("[[ID]]").Append(" : ").Append(entity.ID).Append("    ");
                    //                    stringBuilder.Append("[[CODE]]")
                    //                        .Append(" : ")
                    //                        .Append(entity.GetType().GetProperty("Code").GetValue(entity, null))
                    //                        .Append("    ");
                    //                    stringBuilder.Append("  ***   [[")
                    //                        .Append(AuditTrailAction.Deleted.ToString())
                    //                        .Append("]]");
                    //                }
                    //                catch (Exception ex1)
                    //                {
                    //                }

                    //                auditTrailEntry.Description = stringBuilder.ToString();
                    //                auditTrailEntry.State = State.Added;
                    //                auditTrailEntry.DomainID = entity.DomainID;
                    //                auditTrailEntry.User = entity.User;
                    //                auditTrailEntry.DateCreated = DateTime.Now;

                    //                auditTrail.AuditTrailEntries = new List<AuditTrailEntry>();
                    //                auditTrail.AuditTrailEntries.Add(auditTrailEntry);

                    //                #endregion

                    //                auditTrail.Action = AuditTrailAction.Deleted;
                    //                auditTrail.DomainID = entity.DomainID;
                    //                auditTrail.User = entity.User;
                    //                auditTrail.SourceType = sourceType;
                    //                auditTrail.SourceTypeDescription = entity.GetType().Name;
                    //                auditTrail.State = State.Added;
                    //                auditTrail.RevisionStamp = DateTime.Now;
                    //                auditTrail.DateCreated = DateTime.Now;

                    //                auditTrail.EmployeeID =
                    //                    (new EmployeeRepository()).GetEmployeeByUserName(entity.User,
                    //                        entity.DomainID.Value).ID;

                    //                context.AuditTrails.Add(auditTrail);
                    //                context.SaveChanges();
                    //            }
                    //        }
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //}

                    #endregion

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}