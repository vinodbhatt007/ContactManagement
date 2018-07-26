using System;

namespace ContactManagement.DataContracts.DAL.Interfaces
{
    public interface IRepository : IDisposable
    {

        TEntity Add<TEntity>(TEntity entity) where TEntity : class;
        void Update<TEntity>(TEntity entity) where TEntity : class;
        void Delete<TEntity>(TEntity entity) where TEntity : class;
        IUnitOfWork UnitOfWork { get; }

    }
}
