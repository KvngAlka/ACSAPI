﻿using DataAccess.Extensions;
using DataAccess.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Executors
{
    public class NpgsqlStoredProcedureExecutor : IStoredProcedureExecutor
    {
        public NpgsqlStoredProcedureExecutor() { }

        private async Task<NpgsqlConnection> PrepConnectionAsnyc(string _connectionString)
        {
            var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }
        public async Task<List<T>> ExecuteStoredProcedure<T>(Connection connection, string storeProcedureName, List<StoredProcedureParameter> parameters)
        {
            List<T> response = default(List<T>);
            await ExecuteStoredProcedure(connection, storeProcedureName, parameters, (reader) =>response = reader.toModel<T>());

            return response;
        }
        public async Task ExecuteStoredProcedure(Connection _connection, string storeProcedureName, List<StoredProcedureParameter> parameters, Action<IDataReader> callback = null)
        {
            int index = 0;
            using (NpgsqlConnection connection = await PrepConnectionAsnyc(_connection.ConnectionString))
            {
                try
                {
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = $"{(string.IsNullOrWhiteSpace(_connection.Schema) ? "public" : _connection.Schema)}.{storeProcedureName}";
                        command.CommandType = CommandType.StoredProcedure;

                        if (parameters != null && parameters.Any())
                        {
                            foreach (StoredProcedureParameter parameter in parameters)
                            {
                                command.Parameters.Add(new NpgsqlParameter(parameter.Name, parameter.Type));
                                command.Parameters[index++].Value = parameter.Value;
                            }
                        }

                        if (callback != null)
                            using (NpgsqlDataReader reader = (NpgsqlDataReader)await command.ExecuteReaderAsync()) callback.Invoke(reader);
                        else
                            await command.ExecuteScalarAsync();

                        CloseConnection(connection, command);
                    }
                }
                catch (Exception)
                {
                    CloseConnectionOnly(connection);
                    throw;
                }

            }
        }



        private void CloseConnection(NpgsqlConnection connection, NpgsqlCommand command)
        {
            command.Dispose();
            connection.Dispose();
            connection.Close();
        }



        private void CloseConnectionOnly(NpgsqlConnection connection)
        {
            connection.Dispose();
            connection.Close();
        }
    }
}
