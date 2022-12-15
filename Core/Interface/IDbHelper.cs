using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface
{
    public interface IDbHelper
    {
        Task Execute(string procedureName, List<StoredProcedureParameter> parameters, Action<IDataReader> callback = null);
        Task<T> ExecuteRaw<T>(string procedureName, List<StoredProcedureParameter> parameters);
        Task<T> ExecuteRaw<T>(Connection connection, string procedureName, List<StoredProcedureParameter> parameters);
        Task ExecuteRaw(string procedureName, List<StoredProcedureParameter> parameters);
        Task ExecuteRaw(Connection connection, string procedureName, List<StoredProcedureParameter> parameters);
        Task<List<T>> Fetch<T>(string procedureName, List<StoredProcedureParameter> parameters);

        Task<List<T>> Fetch<T>(Connection connection, string procedureName, List<StoredProcedureParameter> parameters);
        Connection GetConnection(string connectionName);
        List<Connection> GetConnections();
    }
}
