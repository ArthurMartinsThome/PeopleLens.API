using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace PL.Adapter.MySql.Common
{
    internal class DapperConnector
    {
        private readonly string _connectionString;
        public DapperConnector(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<T> Get<T>(string sql, object? parm = null)
        {
            try
            {
                var table = default(IEnumerable<T>);
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    table = connection.Query<T>(sql, parm).ToArray();
                }
                return table;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<T>> GetAsync<T>(string sql, object? parm = null)
        {
            try
            {
                var table = default(IEnumerable<T>);
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    table = await connection.QueryAsync<T>(sql, parm);
                }
                return table;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int Insert(string sql, object parm)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    return connection.ExecuteScalar<int>($"{sql}; SELECT LAST_INSERT_ID();", parm);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> InsertAsync(string sql, object parm)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    return await connection.ExecuteScalarAsync<int>($"{sql}; SELECT LAST_INSERT_ID();", parm);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<long> InsertAsyncLong(string sql, object parm)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    return await connection.ExecuteScalarAsync<long>($"{sql}; SELECT LAST_INSERT_ID();", parm);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> InsertBulkAsync(string sql)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    return await connection.ExecuteScalarAsync<int>($"{sql}; SELECT LAST_INSERT_ID();");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Execute(string sql)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    await connection.ExecuteAsync(sql);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void InsertVoid(string sql, object parm)
        {
            using (var connection = new MySqlConnection(_connectionString))
                connection.Execute(sql, parm);
        }

        public async Task<int> InsertRowsAffectedAsync(string sql, object parm)
        {
            using (var connection = new MySqlConnection(_connectionString))
                return await connection.ExecuteAsync(sql, parm);
        }

        public async Task<int> InsertTransactionalAsync(string sql, object parm, IDbConnection _connection, IDbTransaction dbTransaction)
        {
            try
            {
                return await _connection.ExecuteScalarAsync<int>($"{sql}; SELECT LAST_INSERT_ID();", parm, dbTransaction);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool Update(string sql, object parm)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    connection.Execute(sql, parm);
                    return true;
                }
            }
            catch (Exception excp)
            {
                throw;
            }
        }

        public async Task<bool> UpdateAsync(string sql, object parm)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    await connection.ExecuteAsync(sql, parm);
                    return true;
                }
            }
            catch (Exception excp)
            {
                throw;
            }
        }

        public async Task<bool> UpdateTransactionalAsync(string sql, object parm, IDbConnection _connection, IDbTransaction dbTransaction)
        {
            try
            {
                var executeTransaction = await _connection.ExecuteAsync(sql, parm, dbTransaction);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool Delete(string sql, object parm)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    connection.Execute(sql, parm);
                    return true;
                }
            }
            catch (Exception excp)
            {
                throw;
            }
        }

        public async Task<bool> DeleteAsync(string sql, object parm)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    await connection.ExecuteAsync(sql, parm);
                    return true;
                }
            }
            catch (Exception excp)
            {
                throw;
            }
        }
    }
}