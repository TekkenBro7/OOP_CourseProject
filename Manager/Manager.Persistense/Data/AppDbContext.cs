using Manager.Domain;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Persistense.Data
{
    public class MongoDbContext
    {
        public readonly IMongoDatabase _database;
        public MongoDbContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }
        public IMongoCollection<Category> Categories => _database.GetCollection<Category>("Categories");
        public IMongoCollection<Priority> Priorities => _database.GetCollection<Priority>("Priorities");
        public IMongoCollection<Status> Statuses => _database.GetCollection<Status>("Statuses");
        public IMongoCollection<Comment> Comments => _database.GetCollection<Comment>("Comments");
        public IMongoCollection<Domain.Entities.Task> Tasks => _database.GetCollection<Domain.Entities.Task>("Tasks");
        public IMongoCollection<SubTask> SubTasks => _database.GetCollection<SubTask>("SubTasks");
        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
    }
}
