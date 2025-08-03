using Dapper;
using PureFood.AccountCommands.Queries;
using PureFood.AccountDomains;
using PureFood.AccountReadModels;
using PureFood.AccountRepository;
using PureFood.BaseReadModels;
using PureFood.BaseRepositories;
using PureFood.Common;
using PureFood.EnumDefine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Z.Dapper.Plus;

namespace PureFood.AccountRepositorySQLImplement
{
    public class UserRepository(IDbConnectionFactory dbConnectionFactory)
    : SqlDbBaseRepository<User>(dbConnectionFactory),
        IUserRepository, ISqlDbBaseRepository<User>
    {
        #region Mapper to update field

        private const string MapperKeyUpdateState = "User_PasswordChange";
        private const string MapperKeyForgotPasswordTwoFactorVerifySuccess = "User_ForgotPasswordTwoFactorVerifySuccess";
        private const string MapperKeyProfileChangeNameAndGender = "User_ProfileChangeNameAndGender";
        private const string MapperKeyAuthenticatorSecretKeyAddOrChange = "User_AuthenticatorSecretKeyAddOrChange";

        public static void MapperUpdate()
        {
            MapperPasswordChange();
            MapperForgotPasswordTwoFactorVerifySuccess();
            MapperProfileChangeNameAndGender();
            MapperAuthenticatorSecretKeyAddOrChange();
        }

        private static void MapperPasswordChange()
        {
            DapperPlusManager.Entity<User>(MapperKeyUpdateState)
                .Key(p => p.Id)
                .Map(p => p.UpdatedDate)
                .Map(p => p.UpdatedDateUtc)
                .Map(p => p.UpdatedUid)
                .Map(p => p.LoginUid)
                .Map(p => p.Password)
                .Map(p => p.PasswordSalt)
                .Map(p => p.Version)
                ;
        }

        private static void MapperForgotPasswordTwoFactorVerifySuccess()
        {
            DapperPlusManager.Entity<User>(MapperKeyForgotPasswordTwoFactorVerifySuccess)
                .Key(p => p.Id)
                .Map(p => p.UpdatedDate)
                .Map(p => p.UpdatedDateUtc)
                .Map(p => p.UpdatedUid)
                .Map(p => p.LoginUid)
                .Map(p => p.Password)
                .Map(p => p.PasswordSalt)
                .Map(p => p.SecurityStamp)
                .Map(p => p.Version)
                ;
        }

        private static void MapperProfileChangeNameAndGender()
        {
            DapperPlusManager.Entity<User>(MapperKeyProfileChangeNameAndGender)
                .Key(p => p.Id)
                .Map(p => p.UpdatedDate)
                .Map(p => p.UpdatedDateUtc)
                .Map(p => p.UpdatedUid)
                .Map(p => p.LoginUid)
                .Map(p => p.FullName)
                .Map(p => p.Gender)
                .Map(p => p.Version)
                ;
        }

        private static void MapperAuthenticatorSecretKeyAddOrChange()
        {
            DapperPlusManager.Entity<User>(MapperKeyAuthenticatorSecretKeyAddOrChange)
                .Key(p => p.Id)
                .Map(p => p.UpdatedDate)
                .Map(p => p.UpdatedDateUtc)
                .Map(p => p.UpdatedUid)
                .Map(p => p.LoginUid)
                //.Map(p => p.auth)
                .Map(p => p.Version)
                ;
        }

        #endregion

        public async Task<RUser?> Get(string id)
        {
            return await DbConnectionFactory.WithConnection(async connection =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id, DbType.String);
                var data = await connection.QueryFirstOrDefaultAsync<RUser>("[Users_GetById]", parameters,
                    commandType: CommandType.StoredProcedure);
                return data;
            });
        }

        public async Task<RUser[]> Gets(string[] ids)
        {
            return await DbConnectionFactory.WithConnection(async connection =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Ids", ids.AsArrayJoin(), DbType.String);
                var data = await connection.QueryAsync<RUser>("[Users_GetByIds]", parameters,
                    commandType: CommandType.StoredProcedure);
                var result = data.ToArray();
                return result;
            });
        }

        public async Task<RUser?> GetByUserNameOrEmailOrPhoneNumber(string keyword)
        {
            return await DbConnectionFactory.WithConnection(async connection =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Key", keyword, DbType.String);
                var data = await connection.QueryFirstOrDefaultAsync<RUser>("[Users_GetByKey]", parameters,
                    commandType: CommandType.StoredProcedure);
                return data;
            });
        }

        public async Task<RUser?> GetByUserName(string userName)
        {
            return await DbConnectionFactory.WithConnection(async connection =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserName", userName, DbType.String);
                var data = await connection.QueryFirstOrDefaultAsync<RUser>("[Users_GetByUserName]", parameters,
                    commandType: CommandType.StoredProcedure);
                return data;
            });
        }

        public async Task ProfileChangeNameAndGender(User user)
        {
            await DbConnectionFactory.WithConnection(async (connection) =>
            {
                await connection.BulkUpdateAsync(MapperKeyProfileChangeNameAndGender, user);
            });
        }

        public async Task AddOrChange(User[] users)
        {
            await DbConnectionFactory.WithConnection(async (connection, transaction) =>
            {
                await transaction.BulkMergeAsync(users);
            });
        }

        public async Task ForgotPasswordTwoFactorVerifySuccess(User user, ForgotPassword forgotPassword)
        {
            await DbConnectionFactory.WithConnection(async (connection, transaction) =>
            {
                await transaction.BulkUpdateAsync(MapperKeyForgotPasswordTwoFactorVerifySuccess, user);
                await transaction.BulkUpdateAsync(forgotPassword);
            });
        }

        public async Task Add(IDbConnection dbConnection, IDbTransaction dbTransaction, User user)
        {
            if (!string.IsNullOrEmpty(user.Code))
            {
                DynamicParameters parametersCheckUser = new DynamicParameters();
                parametersCheckUser.Add("@Key", user.Code, DbType.String);
                var userCheck = await dbConnection.QueryFirstOrDefaultAsync<RUser>("[Users_GetByKey]",
                    parametersCheckUser, commandType: CommandType.StoredProcedure, transaction: dbTransaction);
                if (userCheck != null)
                {
                    throw new Exception("USER_EXISTED");
                }
            }

            if (!string.IsNullOrEmpty(user.EmailAddress))
            {
                DynamicParameters parametersCheckUser = new DynamicParameters();
                parametersCheckUser.Add("@Key", user.EmailAddress, DbType.String);
                var userCheck = await dbConnection.QueryFirstOrDefaultAsync<RUser>("[Users_GetByKey]",
                    parametersCheckUser, commandType: CommandType.StoredProcedure, transaction: dbTransaction);
                if (userCheck != null)
                {
                    throw new Exception("USER_EXISTED");
                }
            }

            if (!string.IsNullOrEmpty(user.PhoneNumber))
            {
                DynamicParameters parametersCheckUser = new DynamicParameters();
                parametersCheckUser.Add("@Key", user.PhoneNumber, DbType.String);
                var userCheck = await dbConnection.QueryFirstOrDefaultAsync<RUser>("[Users_GetByKey]",
                    parametersCheckUser, commandType: CommandType.StoredProcedure, transaction: dbTransaction);
                if (userCheck != null)
                {
                    throw new Exception("USER_EXISTED");
                }
            }

            await dbTransaction.BulkInsertAsync(user);
        }

        public async Task PasswordChange(User user)
        {
            await DbConnectionFactory.WithConnection(async connection =>
            {
                await connection.BulkUpdateAsync(MapperKeyUpdateState, user);
            });
        }

        public async Task TwoFactorChange(User user)
        {
            await DbConnectionFactory.WithConnection(async (connection) => { await connection.BulkUpdateAsync(user); });
        }

        public async Task PhoneNumberChange(User user)
        {
            await DbConnectionFactory.WithConnection(async (connection) => { await connection.BulkUpdateAsync(user); });
        }

        //TODO Store not support search by status and account type
        public async Task<RUser[]> Search(AccountGetsQuery query, RefSqlPaging paging)
        {
            return await DbConnectionFactory.WithConnection(async connection =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Keyword", query.KeyWord, DbType.String);
                parameters.Add("@OFFSET", paging.OffSet, DbType.Int32);
                parameters.Add("@FETCH", paging.PageSize, DbType.Int32);
                parameters.Add("@Status", query.Status, DbType.Int32);
                parameters.Add("@Type", query.AccountType, DbType.Int32);
                parameters.Add("@DepartmentIds", query.DepartmentIds.AsArrayJoin(), DbType.String);
                parameters.Add("@DepartmentIdsExclude", query.DepartmentIdsExclude.AsArrayJoin(), DbType.String);
                var data = await connection.QueryAsync<RUser>("[Users_Gets]", parameters,
                    commandType: CommandType.StoredProcedure);
                var result = data.ToArray();
                if (result.Length > 0)
                {
                    paging.TotalRow = result[0].TotalRow;
                }

                return result;
            });
        }


        public async Task AuthenticatorSecretKeyAddOrChange(User user)
        {
            await DbConnectionFactory.WithConnection(async (connection) =>
            {
                await connection.BulkUpdateAsync(MapperKeyAuthenticatorSecretKeyAddOrChange, user);
            });
        }

    }
}
