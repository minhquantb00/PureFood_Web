using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.BaseMongoDbRepositories
{
    public class MongoDBConnectionFactory(ILogger<MongoDBConnectionFactory> logger, string connectionString, string dbName)
    {
        public IMongoDatabase GetConnection()
        {
            try
            {
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase(dbName);
                return database;
            }
            catch (Exception e)
            {
                e.Data["MongoDBConnectionFactory"] = "Not MongoDBConnection";
                logger.LogError(e, "Exception {Message}", e.Message);
                throw;
            }
        }

        public async Task<T> MongoDBExecute<T>(Func<Task<T>> action)
        {
            try
            {
                return await action();
            }
            catch (Exception ex)
            {
                ex.Data["MongoDBConnectionFactory"] = "Execute Exception";
                logger.LogError(ex, "Exception  {Type} {Message}", ex.GetType(), ex.Message);
                throw;
            }
        }

        public async Task MongoDBExecute(Func<Task> action)
        {
            try
            {
                await action();
            }
            catch (Exception ex)
            {
                ex.Data["MongoDBConnectionFactory"] = "Execute Exception";
                logger.LogError(ex, "Exception  {Type} {Message}", ex.GetType(), ex.Message);
                throw;
            }
        }
    }
}
