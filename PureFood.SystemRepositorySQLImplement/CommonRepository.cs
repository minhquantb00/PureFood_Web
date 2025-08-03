using Dapper;
using PureFood.BaseRepositories;
using PureFood.SystemRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.SystemRepositorySQLImplement
{
    public class CommonRepository(IDbConnectionFactory dbConnectionFactory) : ICommonRepository
    {
        public Task<long> GetNextValueForSequence(string pathName)
        {
            const string spName = "GetNextValueForSequence";
            return dbConnectionFactory.WithConnection(p =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@PathName", pathName, DbType.String);
                return p.ExecuteScalarAsync<long>(spName, parameters, commandType: CommandType.StoredProcedure);
            });
        }

        public async Task<long[]> GetNextMultipleValueForSequence(string pathName, int totalValue)
        {
            const string spName = "GetNextValueForSequence";
            return await dbConnectionFactory.WithConnection(async p =>
            {
                if (totalValue <= 0)
                {
                    totalValue = 1;
                }

                List<long> result = new List<long>();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@PathName", pathName, DbType.String);
                for (int i = 0; i < totalValue; i++)
                {
                    var v = await p.ExecuteScalarAsync<long>(spName, parameters, commandType: CommandType.StoredProcedure);
                    result.Add(v);
                }

                return result.ToArray();
            });
        }

        public Task CreateSequence(string pathName)
        {
            const string spName = "CreateSequenceByPathName";
            return dbConnectionFactory.WithConnection(p =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@PathName", pathName, DbType.String);
                return p.ExecuteAsync(spName, parameters, commandType: CommandType.StoredProcedure);
            });
        }

        public async Task<(long Min, long Max)> GetNextValueForSequence(string pathName, int totalValue)
        {
            return await dbConnectionFactory.WithConnection(async p =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@PathName", pathName, DbType.String);
                parameters.Add("@Total", totalValue, DbType.Int32);
                var min = await p.ExecuteAsync("[GetNextMultipleValueForSequence]", parameters,
                    commandType: CommandType.StoredProcedure);
                var max = min + totalValue - 1;
                return (min, max);
            });
        }
    }
}
