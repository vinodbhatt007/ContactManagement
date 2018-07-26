
using ContactManagement.DataContracts.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagement.DataContracts.DAL.Repository
{
    class GenericRepository : IRepository, IDisposable
    {

        #region Properties
        private readonly string _ConnectionStringName;
        protected DbContext _Context { get; set; }
        #endregion

        #region Constructor
        public GenericRepository() //: this("DefaultConnection")
        {

        }
        public GenericRepository(DbContext context, bool lazyLoading = true)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            _Context = context;
            LazyLoading(lazyLoading);
        }
        public GenericRepository(string connectionStringName) : this(connectionStringName, true)
        {

        }
        public GenericRepository(string connectionStringName, bool lazyloading = true)
        {
            this._ConnectionStringName = connectionStringName;
            this._Context = new DbContext(this._ConnectionStringName);
            LazyLoading(lazyloading);
        }
        #endregion

        private void LazyLoading(bool lazyloading = true)
        {
            ((IObjectContextAdapter)_Context).ObjectContext.ContextOptions.LazyLoadingEnabled = lazyloading;
        }
        private string GetEntityName<TEntity>() where TEntity : class
        {
            return string.Format("{0}.{1}", ((IObjectContextAdapter)_Context).ObjectContext.DefaultContainerName, typeof(TEntity).Name);
        }
        public void Dispose()
        {
            this._Context.Dispose();
        }
        public TEntity Add<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            DbContext.Set<TEntity>().Add(entity);

            return entity;

        }
        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            string eName = GetEntityName<TEntity>();
            object originalItem;
            EntityKey eKey = ((IObjectContextAdapter)_Context).ObjectContext.CreateEntityKey(eName, entity);
            if (((IObjectContextAdapter)DbContext).ObjectContext.TryGetObjectByKey(eKey, out originalItem))
            {
                ((IObjectContextAdapter)DbContext).ObjectContext.ApplyCurrentValues(eKey.EntitySetName, entity);
            }
        }
        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            DbContext.Set<TEntity>().Remove(entity);
        }
        private DbContext DbContext
        {
            get { return this._Context; }
        }
        public IUnitOfWork UnitOfWork
        {
            get
            {
                if (unitOfWork == null)
                {
                    unitOfWork = new UnitOfWork(this.DbContext);
                }
                return unitOfWork;
            }
        }

        public IUnitOfWork unitOfWork;




    }
}
