using Play.Catalog.Service.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Play.Catalog.Service.IRepository
{
    public interface IRepository<T> where T : IEntity
    {
        Task CreateEntityAsync(T entity);
        Task<IReadOnlyCollection<T>> GetAllAsync();
        Task<T> GetEntityByIdAsync(Guid id);
        Task RemoveEntityAsync(Guid id);
        Task UpdateEntityAsync(T entity);
    }
}