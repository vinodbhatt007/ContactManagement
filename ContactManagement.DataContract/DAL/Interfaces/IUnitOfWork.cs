using ContactManagement.DataContracts.Infrastructure;
using System;
using System.Data;
using System.Data.Entity.Core.Objects;

namespace ContactManagement.DataContracts.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        bool IsInTransaction { get; }
        SaveChangeEnum SaveChanges();
        SaveChangeEnum SaveChanges(SaveOptions _SaveOption);
        void BeginTransaction();
        void BeginTransaction(IsolationLevel _IsolationLevel);
        void RollbackTransaction();
        SaveChangeEnum CommitTransaction();

    }
}
