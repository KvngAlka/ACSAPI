using Core.Interface;
using DataAccess.Executors;
using DataAccess.Extensions;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public class DbHelper : IDbHelper
    {
        private readonly IStoredProcedureExecutor _storedProcedureExecutor;
        private readonly List<Connection> _connections;
        private readonly Connection _defaultConnection;

        public DbHelper(IStoredProcedureExecutor storedProcedureExecutor, List<Connection> connections)
        {
            _storedProcedureExecutor = storedProcedureExecutor;
            _connections = connections;
            _defaultConnection = GetConnection("Default");
        }

        public Connection GetConnection(string connectionName)
        {
            return _connections.FirstOrDefault(_ => _.Name?.ToLower() == connectionName.ToLower());
        }

        public async Task<List<T>> Fetch<T>(string procedureName, List<StoredProcedureParameter> parameters)
        {
            return await _storedProcedureExecutor.ExecuteStoredProcedure<T>(_defaultConnection, $"\"{procedureName}\"", parameters);
        }

        public async Task<List<T>> Fetch<T>(Connection connection, string procedureName, List<StoredProcedureParameter> parameters)
        {
            return await _storedProcedureExecutor.ExecuteStoredProcedure<T>(connection, $"\"{procedureName}\"", parameters);
        }

        public async Task<T> ExecuteRaw<T>(string procedureName, List<StoredProcedureParameter> parameters)
        {
            var t = default(T);
            await _storedProcedureExecutor.ExecuteStoredProcedure(_defaultConnection, $"\"{procedureName}\"", parameters, (reader) =>
            {
                if (reader.Read())
                    t = reader.Get<T>(procedureName);
            });

            return t;
        }

        public async Task Execute(string procedureName, List<StoredProcedureParameter> parameters, Action<IDataReader> callback = null)
        {
            await _storedProcedureExecutor.ExecuteStoredProcedure(_defaultConnection, $"\"{procedureName}\"", parameters, callback);
        }

        public async Task<T> ExecuteRaw<T>(Connection connection, string procedureName, List<StoredProcedureParameter> parameters)
        {
            var t = default(T);
            await _storedProcedureExecutor.ExecuteStoredProcedure(connection, $"\"{procedureName}\"", parameters, (reader) =>
            {
                if (reader.Read()) t = reader.Get<T>($"\"{procedureName}\"");
            });

            return t;
        }

        public async Task ExecuteRaw(string procedureName, List<StoredProcedureParameter> parameters)
        {
            await _storedProcedureExecutor.ExecuteStoredProcedure(_defaultConnection, $"\"{procedureName}\"", parameters);
        }

        public async Task ExecuteRaw(Connection connection, string procedureName, List<StoredProcedureParameter> parameters)
        {
            await _storedProcedureExecutor.ExecuteStoredProcedure(connection, $"\"{procedureName}\"", parameters);
        }

        public List<Connection> GetConnections()
        {
            throw new NotImplementedException();
        }
    }
}
