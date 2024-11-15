using System.Data;

namespace DapperMvcWorkshop.DataAccess
{
    public interface IDataAccess
    {
        Task<IEnumerable<T>> GetDataAsync<T, P>(string SpName, P parameters, CommandType commandType = CommandType.StoredProcedure, string connectStringId = "DbConnectString");

        Task<bool> SaveDataAsync<T>(string spName, T parameters, CommandType commandType = CommandType.StoredProcedure, string connectionStringId = "DbConnectString");
    }
}
