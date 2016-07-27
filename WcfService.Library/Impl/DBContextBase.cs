// -----------------------------------------------------------------------
// <copyright file="DBContextBase.cs" company="Logiciells">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace WcfService.Library
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Configuration;
    using Impl.Interface;

    public partial class NorthwindEntities : IDbContext
    {
        private readonly Guid _instanceId;

        public NorthwindEntities(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            _instanceId = Guid.NewGuid();
            Database.SetInitializer<NorthwindEntities>(null);
            Configuration.ValidateOnSaveEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            Configuration.AutoDetectChangesEnabled = false;
        }

        public Guid InstanceId
        {
            get { return _instanceId; }
        }

        public IDbSet<T> GetEntitySet<T>() where T : class
        {
            return base.Set<T>();
        }

        public override int SaveChanges()
        {
            try
            {
                var changes = base.SaveChanges();
                return changes;
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                //MessageBox.Show(exceptionMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        public DbEntityEntry GetEntityEntry(object entity)
        {
            return Entry(entity);
        }

        public Database GetDatabase()
        {
            return Database;
        }
    }
}