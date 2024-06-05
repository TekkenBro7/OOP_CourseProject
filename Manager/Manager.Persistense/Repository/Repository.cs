using Manager.Domain.Abstractions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Persistense.Repository
{
    public class MongoRepository<T> : IRepository<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;
        public MongoRepository(IMongoCollection<T> collection)
        {
            _collection = collection;
        }
        public async Task<T> GetByIdAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _collection.Find(Builders<T>.Filter.Empty).ToListAsync();
        }
        public async System.Threading.Tasks.Task AddAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }
        public void Update(T entity)
        {
            var filter = Builders<T>.Filter.Eq("Id", (entity as dynamic).Id);
            _collection.ReplaceOne(filter, entity);
        }
        public async System.Threading.Tasks.Task UpdateAsync(T entity)
        {
            var filter = Builders<T>.Filter.Eq("Id", (entity as dynamic).Id);
            await _collection.ReplaceOneAsync(filter, entity);
        }
        public void Delete(T entity)
        {
            var filter = Builders<T>.Filter.Eq("Id", (entity as dynamic).Id);
            _collection.DeleteOne(filter);
        }
        public async System.Threading.Tasks.Task DeleteAsync(T entity)
        {
            var filter = Builders<T>.Filter.Eq("Id", (entity as dynamic).Id);
            await _collection.DeleteOneAsync(filter);
        }
    }
}
