using Dapper;
using PureFood.AccountDomains;
using PureFood.AccountReadModels;
using PureFood.AccountRepository;
using PureFood.BaseRepositories;
using System.Data;

namespace PureFood.AccountRepositorySQLImplement
{
    public class ForgotPasswordRepository(IDbConnectionFactory dbConnectionFactory)
    : SqlDbBaseRepository<ForgotPassword>(dbConnectionFactory),
        IForgotPasswordRepository, ISqlDbBaseRepository<ForgotPassword>
    {
        public async Task<RForgotPassword?> GetByCode(string code)
        {
            return await DbConnectionFactory.WithConnection(async connection =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Code", code, DbType.String);
                var data = await connection.QueryFirstOrDefaultAsync<RForgotPassword?>("[ForgotPassword_GetByCode]", parameters, commandType: CommandType.StoredProcedure);
                return data;
            });
        }
    }
}
