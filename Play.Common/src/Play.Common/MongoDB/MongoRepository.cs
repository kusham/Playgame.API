using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Play.Common
{
    public class MongoRepository<T> : IRepository<T> where T : IEntity
    {
        private readonly IMongoCollection<T> _mongoCollection;
        private readonly FilterDefinitionBuilder<T> _filterBuilder = Builders<T>.Filter;

        public MongoRepository(IMongoDatabase mongoDatabase, string collectionName)
        {
            _mongoCollection = mongoDatabase.GetCollection<T>(collectionName);
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync()
        {
            return await _mongoCollection.Find(_filterBuilder.Empty).ToListAsync();
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter)
        {
            return await _mongoCollection.Find(filter).ToListAsync();
        }

        public async Task<T> GetEntityByIdAsync(Guid id)
        {
            FilterDefinition<T> filter = _filterBuilder.Eq(entity => entity.Id, id);
            return await _mongoCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<T> GetEntityByIdAsync(Expression<Func<T, bool>> filter)
        {
            return await _mongoCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateEntityAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            await _mongoCollection.InsertOneAsync(entity);
        }

        public async Task UpdateEntityAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            FilterDefinition<T> filter = _filterBuilder.Eq(existingEntity => existingEntity.Id, entity.Id);
            await _mongoCollection.ReplaceOneAsync(filter, entity);
        }

        public async Task RemoveEntityAsync(Guid id)
        {
            FilterDefinition<T> filter = _filterBuilder.Eq(entity => entity.Id, id);
            await _mongoCollection.DeleteOneAsync(filter);
        }
    }
}
