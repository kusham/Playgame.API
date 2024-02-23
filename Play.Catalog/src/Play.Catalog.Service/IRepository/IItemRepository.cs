using Play.Catalog.Service.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Play.Catalog.Service.IRepository
{
    public interface IItemRepository
    {
        Task CreateItemAsync(Item item);
        Task<IReadOnlyCollection<Item>> GetAllAsync();
        Task<Item> GetItemByIdAsync(Guid id);
        Task RemoveItemAsync(Guid id);
        Task UpdateItemAsync(Item item);
    }
}