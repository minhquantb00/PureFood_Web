using MongoDB.Driver;
using PureFood.BaseEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.EventBus
{
    public class EventStorageRepository : IEventStorageRepository
    {
        //private readonly IMongoCollection<EventBusMessage> _collection;
        private readonly string _connectionString;
        private readonly string _dbName;
        private readonly string _tableName;

        public EventStorageRepository(string connectionString, string dbName, string tableName)
        {
            _connectionString = connectionString;
            _dbName = dbName;
            _tableName = tableName;
            /*if (connectionString.Length > 0)
            {
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase(dbName);
                string collection = $"{tableName}{DateTime.Now:yyyyMMdd}";
                _collection = database.GetCollection<EventBusMessage>(collection);
            }*/
        }

        public async Task Add(EventBusMessage message, EventStatusEnum status, string exception)
        {
            if (_connectionString?.Length > 0)
            {
                var client = new MongoClient(_connectionString);
                var database = client.GetDatabase(_dbName);
                string collection = $"{_tableName}{message.CreatedDate:yyyyMMdd}";
                IMongoCollection<EventBusMessage> writeCollection = database.GetCollection<EventBusMessage>(collection);
                await writeCollection.InsertOneAsync(message);
            }
        }
    }
}
