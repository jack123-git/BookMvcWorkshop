using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
namespace DapperMvcWorkshop.DataAccess
{
    public class DapperDataAccess: IDataAccess
    {
        private readonly IConfiguration _config;
        public DapperDataAccess(IConfiguration config)
        {
            this._config = config;
        }

        public async Task<IEnumerable<T>> GetDataAsync<T,P>(string SpName, P parameters, CommandType commandType = CommandType.StoredProcedure, string connectStringId= "DbConnectString")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectStringId));
            return await connection.QueryAsync<T>(SpName, parameters, commandType: commandType);
        }

        public async Task<bool> SaveDataAsync<T>(string spName, T parameters, CommandType commandType = CommandType.StoredProcedure, string connectionStringId = "DbConnectString")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionStringId));
            int num = await connection.ExecuteAsync(spName, parameters, commandType: commandType);
            return num > 0 ? true : false;
        }
    }
}
