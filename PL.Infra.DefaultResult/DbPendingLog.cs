using PL.Infra.DefaultResult.Model;

namespace PL.Infra.DefaultResult
{
    internal class DbPendingLog
    {
        public static int Insert(LogBase data)
        {
            var connectionString = "ConnectionString.GetCrLogConnectionString()";
            var sql = "insert into pending_log (`attempt`, `retry_at`, `log_data`, `type`) values(@attempt, @retry_at, @log_data, @type); SELECT LAST_INSERT_ID();";

            //int id;
            //using (var connection = new MySqlConnection(connectionString))
            //{
            //    id = connection.ExecuteScalar<int>(sql, new
            //    {
            //        attempt = data.Attempt,
            //        retry_at = data.Attempt < 10 ? DateTime.UtcNow.AddMinutes(5) : default(DateTime?),
            //        log_data = JsonConvert.SerializeObject(data),
            //        type = data.GetType().FullName,
            //    });
            //}
            //return id;
            return 0;
        }

        public static IEnumerable<PendingLog> GetPending(int take)
        {
            var connectionString = "ConnectionString.GetCrLogConnectionString()";
            var sql = $"select `id`, `attempt`, `retry_at`, `log_data`, `type` from pending_log where retry_at < @retry limit {take}";
            //IEnumerable<PendingLog> data;
            //using (var connection = new MySqlConnection(connectionString))
            //{
            //    data = connection.Query<PendingLog>(sql, new { retry = DateTime.UtcNow });
            //}
            //return data;
            return null;
        }

        public static void Delete(int[] ids)
        {
            var connectionString = "ConnectionString.GetCrLogConnectionString()";
            var sql = $"delete from pending_log where id in @ids";
            //using (var connection = new MySqlConnection(connectionString))
            //{
            //    connection.Execute(sql, new { ids });
            //}
        }
    }
}