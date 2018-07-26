using System;
using System.Data;
using System.Linq;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using ContactManagement.DataContracts.Infrastructure;

namespace ContactManagement.DataContracts.DAL.Interfaces
{
    public class UnitOfWork : IUnitOfWork
    {

        private DbTransaction _transaction;
        private DbContext _dbContext;

        public UnitOfWork(DbContext context)
        {
            _dbContext = context;
        }
        public bool IsInTransaction
        {
            get { return _transaction != null; }
        }

        public void BeginTransaction()
        {
            BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public void BeginTransaction(IsolationLevel _IsolationLevel)
        {
            if (_transaction != null)
            {
                throw new ApplicationException("Cannot begin a new transaction while an existing transaction is still running." +
                                                    "Please commit or rollback the existing transaction before starting a new one.");
            }
            OpenConnection();
            _transaction = ((IObjectContextAdapter)_dbContext).ObjectContext.Connection.BeginTransaction(_IsolationLevel);
        }

        public SaveChangeEnum CommitTransaction()
        {
            SaveChangeEnum retValue = SaveChangeEnum.No_Action;
            if (_transaction == null)
            {
                throw new ApplicationException("Cannot roll back a transaction while there is no transaction running.");
            }
            try
            {
                ((IObjectContextAdapter)_dbContext).ObjectContext.SaveChanges();
                _transaction.Commit();
                ReleaseCurrentTransaction();
                retValue = SaveChangeEnum.Save_Successfull;
            }
            catch (OptimisticConcurrencyException)
            {
                retValue = SaveChangeEnum.Updated_By_Other_User;
                RollbackTransaction();
                throw;
            }
            catch (Exception)
            {
                retValue = SaveChangeEnum.No_Action;
                RollbackTransaction();
                throw;
            }

            return retValue;
        }

        private bool _disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;

            }

            if (_disposed)
            {
                return;
            }

            _disposed = true;

        }

      

        public void RollbackTransaction()
        {
            if (_transaction == null)
            {
                throw new ApplicationException("Cannot roll back a transaction while there is no transaction running.");
            }

            if (IsInTransaction)
            {
                try
                {
                    _transaction.Rollback();
                }
                catch
                {

                    throw;
                }
                finally
                {
                    ReleaseCurrentTransaction();
                }

            }
        }

        public SaveChangeEnum SaveChanges()
        {
            SaveChangeEnum retValue = SaveChangeEnum.No_Action;

            if (IsInTransaction)
            {
                throw new ApplicationException("A transaction is running. Call CommitTransaction instead.");
            }

            try
            {
                if(((IObjectContextAdapter)_dbContext).ObjectContext.SaveChanges() > 0)
                {
                    retValue = SaveChangeEnum.Save_Successfull;
                }
            }
            catch (OptimisticConcurrencyException)
            {
                retValue = SaveChangeEnum.Updated_By_Other_User;
            }
            catch (Exception)
            {
                retValue = SaveChangeEnum.No_Action;
            }

            return retValue;
        }

        public SaveChangeEnum SaveChanges(SaveOptions _SaveOption)
        {
            SaveChangeEnum retValue = SaveChangeEnum.No_Action;

            if (IsInTransaction)
            {
                throw new ApplicationException("A transaction is running. Call CommitTransaction instead.");
            }


            try
            {
                if (((IObjectContextAdapter)_dbContext).ObjectContext.SaveChanges(_SaveOption) > 0)
                {
                    retValue = SaveChangeEnum.Save_Successfull;
                }
            }
            catch (OptimisticConcurrencyException)
            {
                retValue = SaveChangeEnum.Updated_By_Other_User;
            }
            catch (Exception)
            {
                retValue = SaveChangeEnum.No_Action;
            }

            return retValue;
        }


        private void OpenConnection()
        {
            if (((IObjectContextAdapter)_dbContext).ObjectContext.Connection.State != ConnectionState.Open)
            {
                ((IObjectContextAdapter)_dbContext).ObjectContext.Connection.Open();
            }
        }

        private void ReleaseCurrentTransaction()
        {
            if (_transaction != null)
            {
                var modifiedEntities =_dbContext.ChangeTracker.Entries()
                 .Where(p => p.State == EntityState.Modified).ToList();


                foreach (var item in modifiedEntities.Where(x => x.State == EntityState.Modified))
                {
                    item.CurrentValues.SetValues(item.OriginalValues);
                    item.State = EntityState.Unchanged;
                }

                foreach (var item in modifiedEntities.Where(x => x.State == EntityState.Added))
                {
                    item.State = EntityState.Detached;
                }

                foreach (var item in modifiedEntities.Where(x => x.State == EntityState.Deleted))
                {
                    item.State = EntityState.Unchanged;
                }

                _transaction.Dispose();
                _transaction = null;
            }
        }
    }
}
