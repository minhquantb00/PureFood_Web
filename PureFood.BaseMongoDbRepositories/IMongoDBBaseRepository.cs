using MongoDB.Driver;
using PureFood.BaseDomains;
using PureFood.BaseReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.BaseMongoDbRepositories
{
    public interface IMongoDBBaseRepository<in T, T1>
    where T : BaseDomain
    where T1 : BaseReadModel
    {
        Task Add(T obj);
        Task Change(T obj);
        Task AddOrChange(T obj);
        Task<T1?> GetById(string id);
        Task<List<T1>> GetByIds(string[] ids);
    }

    public abstract class MongoDBBaseRepository<T, T1> : IMongoDBBaseRepository<T, T1>
        where T : BaseDomain
        where T1 : BaseReadModel
    {
        protected readonly MongoDBConnectionFactory MongoDBConnectionFactory;
        protected readonly IMongoCollection<T1> ReadCollection;
        protected readonly IMongoCollection<T> WriteCollection;

        protected MongoDBBaseRepository(MongoDBConnectionFactory dbConnectionFactory, string tableName)
        {
            MongoDBConnectionFactory = dbConnectionFactory;
            var database = dbConnectionFactory.GetConnection();
            ReadCollection = database.GetCollection<T1>(tableName);
            WriteCollection = database.GetCollection<T>(tableName);
        }

        public async Task Add(T obj)
        {
            await MongoDBConnectionFactory.MongoDBExecute(async () => { await WriteCollection.InsertOneAsync(obj); });
        }

        public async Task Change(T obj)
        {
            await MongoDBConnectionFactory.MongoDBExecute(async () =>
            {
                await WriteCollection.ReplaceOneAsync(p => p.Id == obj.Id, obj);
            });
        }

        public async Task AddOrChange(T obj)
        {
            await MongoDBConnectionFactory.MongoDBExecute(async () =>
            {
                var filter = Builders<T>.Filter.Eq(x => x.Id, obj.Id);
                var options = new ReplaceOptions { IsUpsert = true };
                await WriteCollection.ReplaceOneAsync(filter, obj, options);
            });
        }

        public async Task<T1?> GetById(string id)
        {
            return await MongoDBConnectionFactory.MongoDBExecute(async () =>
            {
                return await (await ReadCollection.FindAsync(p => p.Id == id)).FirstOrDefaultAsync();
            });
        }

        public async Task<List<T1>> GetByIds(string[] ids)
        {
            return await MongoDBConnectionFactory.MongoDBExecute(async () =>
            {
                return await (await ReadCollection.FindAsync(p => ids.Contains(p.Id))).ToListAsync();
            });
        }
    }
}
