﻿using EzRepositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EzRepositories
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter);
        Task<TEntity> GetAsync(object id);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter);

        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(object id);
        Task<bool> DeleteAsync(TEntity entity);
        Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> filter);
    }
}
