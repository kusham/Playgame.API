using MongoDB.Driver;
using Play.Catalog.Service.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Play.Catalog.Service.Repositories
{
    public class ItemRepository
    {
        private const string _collectionName = "items";
        private readonly IMongoCollection<Item> _mongoCollection;
        private readonly FilterDefinitionBuilder<Item> _filterBuilder = Builders<Item>.Filter;

        public ItemRepository()
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017");
            var database = mongoClient.GetDatabase("Catalog");
            _mongoCollection = database.GetCollection<Item>(_collectionName);
        }

        public async Task<IReadOnlyCollection<Item>> GetAllAsync()
        {
            return await _mongoCollection.Find(_filterBuilder.Empty).ToListAsync();
        }
        public async Task<Item> GetItemByIdAsync(Guid id)
        {
            FilterDefinition<Item> filter = _filterBuilder.Eq(item => item.Id, id);
            return await _mongoCollection.Find(filter).FirstOrDefaultAsync();
        }
        public async Task CreateItemAsync(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            await _mongoCollection.InsertOneAsync(item);
        }
        public async Task UpdateItemAsync(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            FilterDefinition<Item> filter = _filterBuilder.Eq(existingItem => existingItem.Id, item.Id);
            await _mongoCollection.ReplaceOneAsync(filter, item);
        }
        public async Task RemoveItemAsync(Guid id)
        {
            FilterDefinition<Item> filter = _filterBuilder.Eq(item => item.Id, id);
            await _mongoCollection.DeleteOneAsync(filter);
        }
    }
}
