using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Play.Common
{
    public interface IRepository<T> where T : IEntity
    {
        Task CreateEntityAsync(T entity);
        Task<IReadOnlyCollection<T>> GetAllAsync();
        Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter);
        Task<T> GetEntityByIdAsync(Guid id);
        Task<T> GetEntityByIdAsync(Expression<Func<T, bool>> filter);
        Task RemoveEntityAsync(Guid id);
        Task UpdateEntityAsync(T entity);
    }
}