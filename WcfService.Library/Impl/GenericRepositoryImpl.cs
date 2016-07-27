#region Using directives

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using WcfService.Library.Impl.Interface;


#endregion

namespace WcfService.Library.Impl
{
    public class GenericRepositoryImpl<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly IDbContext _context;
        private readonly IDbSet<TEntity> _dbSet;
        private readonly Guid _instanceId;

        public GenericRepositoryImpl(IDbContext context)
        {
            _context = context;
            _dbSet = context.GetEntitySet<TEntity>();
            _instanceId = Guid.NewGuid();
        }

        public Guid InstanceId
        {
            get { return _instanceId; }
        }

        ///<summary>
        /// Save entity to the repository
        ///</summary>
        /// <param name="entity">The entity to save</param>
        public bool Save(TEntity entity)
        {
            _dbSet.Add(entity);
            SaveChanges();
            return true;
        }

        /// <summary>
        /// This method is used to count total records in entity collection
        /// </summary>
        /// <returns>returns records count</returns>
        public int Count()
        {
            IQueryable<TEntity> queryBase = _dbSet.AsQueryable();
            return queryBase.Count();
        }

        /// <summary>
        /// This method is used to count total records in entity collection on the basis of the lambda expression
        /// </summary>
        /// <param name="includeTableName">comma seprated list of tables to include</param>
        /// <param name="lambdaExpression">lambda expression</param>
        /// <returns>returns count</returns>
        public int Count(string includeTableName, Expression<Func<TEntity, bool>> lambdaExpression)
        {
            return _dbSet.Include(includeTableName).Count(lambdaExpression);
        }

        /// <summary>
        /// This method is used to count total records in entity collection on the basis of the lambda expression
        /// </summary>
        /// <param name="lambdaExpression">lambda expression</param>
        /// <returns>returns count</returns>
        public int Count(Expression<Func<TEntity, bool>> lambdaExpression)
        {
            IQueryable<TEntity> queryBase = _dbSet.AsQueryable();
            return queryBase.Count(lambdaExpression);
        }

        ///<summary>
        /// Mark entity to be deleted within the repository
        ///</summary>
        /// <param name="entity">The entity to delete</param>
        public bool Delete(TEntity entity)
        {
            var entry = _context.GetEntityEntry(entity);
            entry.State = EntityState.Deleted;
            SaveChanges();
            return true;
        }

        /// <summary>
        /// This method is used to get the list of available records in entity table from database
        /// </summary>
        /// <returns>returns list of entities</returns>
        public IQueryable<TEntity> GetList()
        {
            var result = _dbSet.AsQueryable();
            return result;
        }

        /// <summary>
        /// Gets the list of records with name of table which need to be included
        /// </summary>
        /// <param name="includeTableName">name of table to include</param>
        /// <returns>returns list of entity</returns>
        public IQueryable<TEntity> GetList(string includeTableName)
        {
            return _dbSet.Include(includeTableName).AsQueryable();
        }

        /// <summary>
        /// Gets the list of records on the basis of the criteria expression passed and name of table to include
        /// </summary>
        /// <param name="includeTableName">name of table to include</param>
        /// <param name="lambdaExpression">lambda expression for where clause</param>
        /// <returns>returns list of entities</returns>
        public IQueryable<TEntity> GetList(string includeTableName, Expression<Func<TEntity, bool>> lambdaExpression)
        {
            return _dbSet.Include(includeTableName).Where(lambdaExpression);
        }

        /// <summary>
        /// Gets the list of records on the basis of the criteria expression passed
        /// </summary>
        /// <param name="lambdaExpression">lambda expression for where clause</param>
        /// <returns>returns list of entities</returns>
        public IQueryable<TEntity> GetList(Expression<Func<TEntity, bool>> lambdaExpression)
        {
            return _dbSet.Where(lambdaExpression);
        }

        /// <summary>
        /// This method is used to get the list of records on the basis of the criteria expression passed
        /// </summary>
        /// <param name="lambdaExpression">lambda expression for where clause</param>
        /// <param name="orderbyexpr">The orderbyexpr.</param>
        /// <returns>
        /// returns list of entities
        /// </returns>
        public IQueryable<TEntity> GetList(Expression<Func<TEntity, bool>> lambdaExpression, Expression<Func<TEntity, int>> orderByexpr, bool orderByDesc = false)
        {
            if (orderByDesc)
            {
                return _dbSet.Where(lambdaExpression).OrderByDescending(orderByexpr);
            }
            else
            {
                return _dbSet.Where(lambdaExpression).OrderBy(orderByexpr);
            }
        }

        /// <summary>
        /// This method is used to get the single records from entity table 
        /// </summary>
        /// <returns>returns single entity</returns>
        public TEntity GetSingle()
        {
            return _dbSet.FirstOrDefault();
        }

        /// <summary>
        /// Gets single records with entities of include table name, condition is included table name must have reference with calling entity
        /// </summary>
        /// <param name="lambdaExpression">lambda expression</param>
        /// <param name="includeTableName">name of table to include</param>
        /// <returns></returns>
        public TEntity GetSingle(string includeTableName, Expression<Func<TEntity, bool>> lambdaExpression)
        {
            return _dbSet.Include(includeTableName).Where(lambdaExpression).FirstOrDefault();
        }

        /// <summary>
        /// Gets a single record by the specified criteria (usually the unique identifier)
        /// </summary>
        /// <param name="whereCondition">where condition to search</param>
        /// <returns>A single record that matches the specified criteria</returns>
        public TEntity GetSingle(Expression<Func<TEntity, bool>> whereCondition)
        {
            return _dbSet.Where(whereCondition).FirstOrDefault();
        }

        /// <summary>
        /// Load entity from the repository (always query store)
        /// </summary>
        /// <param name="whereCondition">where condition</param>
        /// <returns>The loaded entity</returns>
        public TEntity Load(Expression<Func<TEntity, bool>> whereCondition)
        {
            return _dbSet.Where(whereCondition).FirstOrDefault();
        }

        /// <summary>
        /// Loads the list.
        /// </summary>
        /// <param name="whereCondition">where condition.</param>
        /// <returns></returns>
        public IQueryable<TEntity> LoadList(Expression<Func<TEntity, bool>> whereCondition)
        {
            return _dbSet.Where(whereCondition);
        }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public bool Update(TEntity entity)
        {
            IDbSet<TEntity> dbset = _dbSet;
            var entry = _context.GetEntityEntry(entity);

            ////Retreive the Id through reflection
            var pkey = dbset.Create().GetType().GetProperty("id").GetValue(entity, null);

            if (entry.State == EntityState.Detached)
            {
                var set = _dbSet;

                TEntity attachedEntity = set.Find(pkey);  // You need to have access to key

                if (attachedEntity != null)
                {
                    var attachedEntry = _context.GetEntityEntry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(entity);
                }
                else
                {
                    entry.State = EntityState.Modified; // This should attach entity
                }
            }

            SaveChanges();
            return true;
        }

        public bool SaveAndUpdateList(IList<TEntity> list)
        {
            foreach (TEntity entity in list)
            {
                IDbSet<TEntity> dbset = _dbSet;
                var entry = _context.GetEntityEntry(entity);

                // Retreive the Id through reflection
                var pkey = dbset.Create().GetType().GetProperty("id").GetValue(entity, null);

                var set = dbset;

                TEntity attachedEntity = set.Find(pkey);  // You need to have access to key

                if (attachedEntity != null)
                {
                    var attachedEntry = _context.GetEntityEntry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(entity);
                }
                else
                {
                    dbset.Add(entity);
                }

                SaveChanges();
            }
            return true;
        }
    }
}