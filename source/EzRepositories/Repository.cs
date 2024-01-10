using EzRepositories.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;

namespace EzRepositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly DbContext _db;
        protected readonly DbSet<TEntity> _dbColumn;
        private static PropertyInfo? getKeyProperty<TEntity>()
        {
            return typeof(TEntity).GetProperties()
                 .LastOrDefault(p => p.IsDefined(typeof(KeyAttribute), true));
        }

        public Repository(DbContext db)
        {
            _db = db;
            _dbColumn = _db.Set<TEntity>();
        }

        private PropertyInfo? _idProperty => getKeyProperty<TEntity>();

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbColumn.ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _dbColumn.Where(filter).ToListAsync();
        }

        public virtual async Task<TEntity> GetAsync(object id)
        {
            if(_idProperty == null)
            {
                throw new InvalidOperationException($"Entity provided ({typeof(TEntity)}) does not have a primary key. Only entities with primary keys could use this method");
            }

            if(_idProperty.PropertyType != id.GetType())
            {
                throw new ArgumentException(
                $"Provide Id is type of '{id.GetType().Name}', Entity's Id property is of type '{_idProperty.PropertyType.Name}'. Kindly provide an argument of type '{_idProperty.PropertyType.Name}'."
                    );
            }

            return await _dbColumn.FindAsync(id);
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            var response = await _dbColumn.FirstOrDefaultAsync(filter);
            return response;
        }

        protected virtual Task<TEntity> OnBeforeCreating(TEntity entity) { return Task.FromResult(entity);}

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            entity = await OnBeforeCreating(entity);

            var createdEntity = await _dbColumn.AddAsync(entity);
            var affectedRows = await _db.SaveChangesAsync();
            if(affectedRows < 1)
            {
                return null;
            }

            return createdEntity.Entity;
        }

        protected virtual Task<TEntity> OnBeforeUpdating(TEntity entity) { return Task.FromResult(entity); }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            entity = await OnBeforeUpdating(entity);

            var updatedEntity = _dbColumn.Update(entity);
            var affectedRows = await _db.SaveChangesAsync();
            if(affectedRows < 1)
            {
                return null;
            }

            return updatedEntity.Entity;
        }
    }
}
