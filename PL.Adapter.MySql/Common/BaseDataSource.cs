using System.Data;
using PL.Infra.DefaultResult;
using PL.Infra.DefaultResult.Interface;
using PL.Infra.Model.Filter;

namespace PL.Adapter.MySql.Common
{
    public class BaseDataSource
    {
        private readonly int _DefaultTake;
        private readonly string _ConnectionString;
        public BaseDataSource(string connectionString, int defaultTake = 1000000)
        {
            _ConnectionString = connectionString;
            _DefaultTake = defaultTake;
        }

        protected async Task<IResult<IEnumerable<T>>> Query<T>(string sql, IEnumerable<string> fields, IEnumerable<ParsedFilter> filters, int skip = 0, int? take = null, IEnumerable<string> joinList = null)
        {
            try
            {
                var connector = new DapperConnector(_ConnectionString);
                var filterHandler = new FilterHandler(filters);
                sql = sql.Replace("[fields]", string.Join(",", fields));
                if (joinList != null && joinList.Any())
                    sql = sql.Replace("[join]", string.Join(" ", joinList));
                else
                    sql = sql.Replace("[join]", string.Empty);

                if (filters.Any())
                    sql = sql.Replace("[where]", $"WHERE {filterHandler.SqlFilters}");
                else
                    sql = sql.Replace("[where]", string.Empty);
                sql = sql.Replace("[limit]", $"LIMIT {skip},{take ?? _DefaultTake}");
                var data = await connector.GetAsync<T>(sql, filterHandler.Parameters);

                return DefaultResult<IEnumerable<T>>.Create(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERRO] Procurar conversas - {ex.Message}");
                return DefaultResult<IEnumerable<T>>.Error("Falha ao fazer a requisição dos dados.", ex);
            }
        }

        protected async Task<IResult<int>> InsertAsync(string sql, object parameter)
        {
            try
            {
                var connector = new DapperConnector(_ConnectionString);
                var data = await connector.InsertAsync(sql, parameter);
                return DefaultResult<int>.Create(data);
            }
            catch (Exception ex)
            {
                return DefaultResult<int>.Error("Falha ao fazer a inserção dos dados.", ex);
            }
        }

        /// <summary>
        /// Método para fazer bulk insert no banco
        /// Para utilizá-lo o SQL já deve vir montado com as colunas e os valores a serem inseridos
        /// 
        /// Esse método deve ser evitado de ser utilizado, principalmente em operações que envolvam dados inseridos manualmente por usuários 
        /// </summary>
        protected async Task<IResult<int>> InsertBulkAsync(string sql)
        {
            try
            {
                var connector = new DapperConnector(_ConnectionString);
                var data = await connector.InsertBulkAsync(sql);
                return DefaultResult<int>.Create(data);
            }
            catch (Exception ex)
            {
                return DefaultResult<int>.Error("Falha ao fazer a inserção dos dados.", ex);
            }
        }

        /// <summary>
        /// Método para fazer bulk update no banco
        /// Para utilizá-lo o SQL já deve vir montado com as colunas e os valores a serem atualizados
        /// 
        /// Esse método deve ser evitado de ser utilizado, principalmente em operações que envolvam dados inseridos/atualizados manualmente por usuários 
        /// </summary>
        protected async Task<IResult<bool>> UpdateBulkAsync(string sql)
        {
            try
            {
                var connector = new DapperConnector(_ConnectionString);
                var data = await connector.Execute(sql);
                return DefaultResult<bool>.Create(data);
            }
            catch (Exception ex)
            {
                return DefaultResult<bool>.Error("Falha ao fazer a atualização dos dados.", ex);
            }
        }

        protected async Task<IResult<int>> InsertRowsAffected(string sql, object parameter)
        {
            try
            {
                var connector = new DapperConnector(_ConnectionString);
                var data = await connector.InsertRowsAffectedAsync(sql, parameter);
                return DefaultResult<int>.Create(data);
            }
            catch (Exception ex)
            {
                return DefaultResult<int>.Error("Falha ao fazer a inserção dos dados.", ex);
            }
        }

        protected async Task<IResult<int>> InsertAsync(string sql, Dictionary<string, object> valuePairs)
        {
            try
            {
                var connector = new DapperConnector(_ConnectionString);
                sql = sql.Replace("[fields]", $"({string.Join(",", valuePairs.Where(x => x.Value != null).Select(x => x.Key))})");
                sql = sql.Replace("[values]", $"(@p_{string.Join(",@p_", valuePairs.Where(x => x.Value != null).Select(x => x.Key.Replace("`", "")))})");
                var parmList = new Dapper.DynamicParameters();
                foreach (var item in valuePairs.Where(x => x.Value != null))
                    parmList.Add($"@p_{item.Key.Replace("`", "")}", item.Value);
                var data = await connector.InsertAsync(sql, parmList);
                return DefaultResult<int>.Create(data);
            }
            catch (Exception ex)
            {
                return DefaultResult<int>.Error("Falha ao fazer a inserção dos dados.", ex);
            }
        }

        protected async Task<IResult<long>> InsertAsyncLong(string sql, Dictionary<string, object> valuePairs)
        {
            try
            {
                var connector = new DapperConnector(_ConnectionString);
                sql = sql.Replace("[fields]", $"({string.Join(",", valuePairs.Where(x => x.Value != null).Select(x => x.Key))})");
                sql = sql.Replace("[values]", $"(@p_{string.Join(",@p_", valuePairs.Where(x => x.Value != null).Select(x => x.Key.Replace("`", "")))})");
                var parmList = new Dapper.DynamicParameters();
                foreach (var item in valuePairs.Where(x => x.Value != null))
                    parmList.Add($"@p_{item.Key.Replace("`", "")}", item.Value);
                var data = await connector.InsertAsyncLong(sql, parmList);
                return DefaultResult<long>.Create(data);
            }
            catch (Exception ex)
            {
                return DefaultResult<long>.Error("Falha ao fazer a inserção dos dados.", ex);
            }
        }

        protected async Task<IResult<int>> InsertListAsync(string sql, List<Dictionary<string, object>> valuePairsList)
        {
            try
            {
                var connector = new DapperConnector(_ConnectionString);
                var sqlList = new List<string>();
                var parmList = new Dapper.DynamicParameters();
                var itemSequence = 1;
                foreach (var item in valuePairsList)
                {
                    var sqlInternal = sql.Replace("[fields]", $"({string.Join(",", item.Where(x => x.Value != null).Select(x => x.Key))})");
                    sqlInternal = sqlInternal.Replace("[values]", $"(@p{itemSequence}_{string.Join($",@p{itemSequence}_", item.Where(x => x.Value != null).Select(x => x.Key.Replace("`", "")))})");
                    sqlList.Add(sqlInternal);
                    foreach (var itemParm in item.Where(x => x.Value != null))
                        parmList.Add($"@p{itemSequence}_{itemParm.Key.Replace("`", "")}", itemParm.Value);
                    itemSequence++;
                }
                var data = await connector.InsertAsync(string.Join(";", sqlList), parmList);
                return DefaultResult<int>.Create(data);
            }
            catch (Exception ex)
            {
                return DefaultResult<int>.Error("Falha ao fazer a inserção da lista dos dados.", ex);
            }
        }

        protected async Task<IResult<bool>> UpdateAsync(string sql, Dictionary<string, object> valuePairs, IEnumerable<ParsedFilter> filters)
        {
            try
            {
                if (!filters.Any())
                    return DefaultResult<bool>.Error("Falha ao fazer a atualização dos dados", new Exception("Tentativa de update sem where"));

                if (!valuePairs.Any())
                    return DefaultResult<bool>.Create(true);

                var connector = new DapperConnector(_ConnectionString);
                var filterHandler = new FilterHandler(filters);
                var updateList = new List<string>();
                var parmList = new Dapper.DynamicParameters();
                foreach (var item in valuePairs)
                {
                    updateList.Add($"{item.Key}=@p_{item.Key.Replace("`", "")}");
                    parmList.Add($"@p_{item.Key.Replace("`", "")}", item.Value);
                }
                foreach (var item in filterHandler.Parameters)
                    parmList.Add(item.Key.Replace("`", ""), item.Value);
                if (filters.Any())
                    sql = sql.Replace("[where]", $"WHERE {filterHandler.SqlFilters}");
                else
                    sql = sql.Replace("[where]", string.Empty);
                sql = sql.Replace("[fieldsandvalues]", $"SET {string.Join(", ", updateList)}");
                var data = await connector.UpdateAsync(sql, parmList);

                return DefaultResult<bool>.Create(data);
            }
            catch (Exception ex)
            {
                return DefaultResult<bool>.Error("Falha ao fazer a atualização dos dados.", ex);
            }
        }

        protected async Task<IResult<bool>> UpdateAsync(string sql, Dictionary<string, (string, object)> valuePairs, IEnumerable<ParsedFilter> filters)
        {
            try
            {
                if (!filters.Any())
                    return DefaultResult<bool>.Error("Falha ao fazer a atualização dos dados", new Exception("Tentativa de update sem where"));
                if (!valuePairs.Any())
                    return DefaultResult<bool>.Create(true);
                var connector = new DapperConnector(_ConnectionString);
                var filterHandler = new FilterHandler(filters);
                var updateList = new List<string>();
                var parmList = new Dapper.DynamicParameters();
                foreach (var item in valuePairs)
                {
                    updateList.Add($"{item.Key}={item.Value.Item1}@p_{item.Key.Replace("`", "")}");
                    parmList.Add($"@p_{item.Key.Replace("`", "")}", item.Value.Item2);
                }
                foreach (var item in filterHandler.Parameters)
                    parmList.Add(item.Key.Replace("`", ""), item.Value);
                if (filters.Any())
                    sql = sql.Replace("[where]", $"WHERE {filterHandler.SqlFilters}");
                else
                    sql = sql.Replace("[where]", string.Empty);
                sql = sql.Replace("[fieldsandvalues]", $"SET {string.Join(", ", updateList)}");
                var data = await connector.UpdateAsync(sql, parmList);
                return DefaultResult<bool>.Create(data);
            }
            catch (Exception ex)
            {
                return DefaultResult<bool>.Error("Falha ao fazer a atualização dos dados.", ex);
            }
        }

        protected async Task<IResult<bool>> DeleteAsync(string sql, IEnumerable<ParsedFilter> filters)
        {
            try
            {
                if (!filters.Any())
                    return DefaultResult<bool>.Error("Falha ao fazer a remoção dos dados.", new Exception("Falha ao fazer a remoção dos dados. filters.Any() false"));

                var connector = new DapperConnector(_ConnectionString);
                var filterHandler = new FilterHandler(filters);
                sql = sql.Replace("[where]", $"WHERE {filterHandler.SqlFilters}");
                var data = await connector.DeleteAsync(sql, filterHandler.Parameters);
                return DefaultResult<bool>.Create(true);
            }
            catch (Exception ex)
            {
                return DefaultResult<bool>.Error("Falha ao fazer a remoção dos dados.", ex);
            }
        }

        protected async Task<IResult<int>> InsertTransactional(string sql, Dictionary<string, object> valuePairs, IDbConnection connection, IDbTransaction dbTransaction)
        {
            try
            {
                var connector = new DapperConnector(_ConnectionString);
                sql = sql.Replace("[fields]", $"({string.Join(",", valuePairs.Where(x => x.Value != null).Select(x => x.Key))})");
                sql = sql.Replace("[values]", $"(@p_{string.Join(",@p_", valuePairs.Where(x => x.Value != null).Select(x => x.Key.Replace("`", "")))})");
                var parmList = new Dapper.DynamicParameters();
                foreach (var item in valuePairs.Where(x => x.Value != null))
                    parmList.Add($"@p_{item.Key.Replace("`", "")}", item.Value);
                var data = await connector.InsertTransactionalAsync(sql, parmList, connection, dbTransaction);
                return DefaultResult<int>.Create(data);
            }
            catch (Exception ex)
            {
                return DefaultResult<int>.Error("Falha ao fazer a inserção dos dados em uma transação.", ex);
            }
        }

        protected async Task<IResult<int>> InsertTransactionalList(string sql, List<Dictionary<string, object>> valuePairsList, IDbConnection connection, IDbTransaction dbTransaction)
        {
            try
            {
                var connector = new DapperConnector(_ConnectionString);
                var sqlList = new List<string>();
                var parmList = new Dapper.DynamicParameters();
                var itemSequence = 1;
                foreach (var item in valuePairsList)
                {
                    var sqlInternal = sql.Replace("[fields]", $"({string.Join(",", item.Where(x => x.Value != null).Select(x => x.Key))})");
                    sqlInternal = sqlInternal.Replace("[values]", $"(@p{itemSequence}_{string.Join($",@p{itemSequence}_", item.Where(x => x.Value != null).Select(x => x.Key.Replace("`", "")))})");
                    sqlList.Add(sqlInternal);
                    foreach (var itemParm in item.Where(x => x.Value != null))
                        parmList.Add($"@p{itemSequence}_{itemParm.Key.Replace("`", "")}", itemParm.Value);
                    itemSequence++;
                }
                var data = await connector.InsertTransactionalAsync(string.Join(";", sqlList), parmList, connection, dbTransaction);
                return DefaultResult<int>.Create(data);
            }
            catch (Exception ex)
            {
                return DefaultResult<int>.Error("Falha ao fazer a inserção da lista dos dados.", ex);
            }
        }

        protected async Task<IResult<bool>> UpdateTransactional(string sql, Dictionary<string, object> valuePairs, IEnumerable<ParsedFilter> filters, IDbConnection connection, IDbTransaction dbTransaction)
        {
            try
            {
                if (!filters.Any())
                    return DefaultResult<bool>.Error("Falha ao fazer a atualização dos dados", new Exception("Tentativa de update sem where"));

                if (!valuePairs.Any())
                    return DefaultResult<bool>.Create(true);

                var connector = new DapperConnector(_ConnectionString);
                var filterHandler = new FilterHandler(filters);
                var updateList = new List<string>();
                var parmList = new Dapper.DynamicParameters();
                foreach (var item in valuePairs)
                {
                    updateList.Add($"{item.Key}=@p_{item.Key.Replace("`", "")}");
                    parmList.Add($"@p_{item.Key.Replace("`", "")}", item.Value);
                }
                foreach (var item in filterHandler.Parameters)
                    parmList.Add(item.Key.Replace("`", ""), item.Value);
                if (filters.Any())
                    sql = sql.Replace("[where]", $"WHERE {filterHandler.SqlFilters}");
                else
                    sql = sql.Replace("[where]", string.Empty);
                sql = sql.Replace("[fieldsandvalues]", $"SET {string.Join(", ", updateList)}");
                var data = await connector.UpdateTransactionalAsync(sql, parmList, connection, dbTransaction);
                return DefaultResult<bool>.Create(data);
            }
            catch (Exception ex)
            {
                return DefaultResult<bool>.Error("Falha ao fazer a atualização dos dados.", ex);
            }
        }
    }
}