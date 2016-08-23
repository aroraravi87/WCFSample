// -----------------------------------------------------------------------
// <copyright file="UnitOfWork.cs" company="Nibs Solution Ltd">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace WcfService.Library.Impl
{
    using System;
    using System.Collections;
    using Interface;
    using WcfService.Library.EDMX;
    public class UnitOfWork : IUnitOfWork
    {
        #region Private Fields

        public readonly IDbContext _context;
        private readonly Guid _instanceId;
        private bool _disposed;
        private Hashtable _repositories;

        private OrderRepositoryImpl orderRepository;

        #endregion Private Fields

        #region Public Properties

        public OrderRepositoryImpl OrderRepository
        {
            get
            {

                if (this.orderRepository == null)
                {
                    this.orderRepository = new OrderRepositoryImpl(_context);
                }
                return orderRepository;
            }
        }

        #endregion

        #region Constuctor/Dispose

        public UnitOfWork(IDbContext context)
        {   
            _context = context;
            _instanceId = Guid.NewGuid();
        }

        public UnitOfWork()
        {
            _context = new NorthwindEntities();
            _instanceId = Guid.NewGuid();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }

        #endregion Constuctor/Dispose

        public Guid InstanceId
        {
            get { return _instanceId; }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            var type = typeof(TEntity).Name;

            if (_repositories.ContainsKey(type))
            {
                return (IGenericRepository<TEntity>)_repositories[type];
            }

            var repositoryType = typeof(GenericRepositoryImpl<>);
            _repositories.Add(type, Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context));

            return (IGenericRepository<TEntity>)_repositories[type];
        }
    }
}