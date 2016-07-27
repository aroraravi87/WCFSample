// -----------------------------------------------------------------------
// <copyright file="IDBContext.cs" company="Nibs Solution Ltd">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace WcfService.Library.Impl.Interface
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public interface IDbContext : IDisposable
    {
        Guid InstanceId { get; }
        IDbSet<T> GetEntitySet<T>() where T : class;
        int SaveChanges();
        DbEntityEntry GetEntityEntry(object entity);
        Database GetDatabase();
    }
}