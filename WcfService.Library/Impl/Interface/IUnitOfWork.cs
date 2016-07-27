// -----------------------------------------------------------------------
// <copyright file="IUnitOfWork.cs" company="Nibs Solution Ltd">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace WcfService.Library.Impl.Interface
{
    #region Using directives

    using System;

    #endregion

    public interface IUnitOfWork : IUnitOfWorkForService
    {
        void Save();
        void Dispose(bool disposing);
    }

    // To be used in services e.g. ICustomerService, does not expose Save()
    // or the ability to Commit unit of work
    public interface IUnitOfWorkForService : IDisposable
    {
        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
    }
}