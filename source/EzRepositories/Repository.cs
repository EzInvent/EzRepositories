using EzRepositories.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;

namespace EzRepositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly DbContext _db;
        private readonly DbSet<TEntity> _dbColumn;

        public Repository(DbContext db)
        {
            _db = db;
            _dbColumn = _db.Set<TEntity>();
        }

        private PropertyInfo? _idProperty => getKeyProperty<TEntity>();

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbColumn.ToListAsync();
        }

        public Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetAsync(object id)
        {
            throw new NotImplementedException();
        }

        private static PropertyInfo? getKeyProperty<TEntity>()
        {
            return typeof(TEntity).GetProperties()
                 .LastOrDefault(p => p.IsDefined(typeof(KeyAttribute), true));
        }
    }
}
