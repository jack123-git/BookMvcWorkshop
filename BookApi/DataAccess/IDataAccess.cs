using System.Data;

namespace BookApi.DataAccess
{
    public interface IDataAccess
    {
        Task<IEnumerable<T>> GetDataAsync<T, P>(string SpName, P parameters, CommandType commandType = CommandType.StoredProcedure, string connectStringId = "DbConnectString");

        Task<int> SaveDataAsync<T>(string spName, T parameters, CommandType commandType = CommandType.StoredProcedure, string connectionStringId = "DbConnectString");
    }
}
