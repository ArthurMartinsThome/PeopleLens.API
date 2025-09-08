using PL.Infra.DefaultResult.Model;
using System.Reflection;
//using PL.Adapter.Slack;
//using PL.Adapter.Slack.Model.Enum;

namespace PL.Infra.DefaultResult
{
    internal class LogWriter
    {
        public static void Write(/*LogLevel level, string logData, int? userId = null, string? stackTrace = null, string? ip = null, string? SessionId = null, string? route = null, string? referenceId = null*/)
        {
            //if (string.IsNullOrEmpty(route))
            //    route = "crlog";

            //var assemblyName = Assembly.GetEntryAssembly().GetName().Name;
            //Task.Run(() =>
            //{
            //    var log = new TraceLog
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Level = level,
            //        AssemblyName = assemblyName,
            //        LogData = logData,
            //        TimeStamp = DateTime.UtcNow,
            //        UserId = userId,
            //        StackTrace = stackTrace,
            //        Ip = ip,
            //        SessionId = SessionId,
            //        Attempt = 1,
            //        Route = route,
            //        ReferenceId = referenceId
            //    };

            //    try
            //    {
            //        var producer = new MqProducer(Adapter.RabbitMq.Model.Enum.ERabbitQueue.LOG_STREAM);
            //        producer.Produce(Guid.NewGuid().ToString(), log);
            //    }
            //    catch
            //    {
            //        AddPendingLog(log);
            //    }
            //});
        }

        public static void Rewrite(LogBase log)
        {
            try
            {
                log.Attempt++;
                //var queue = RabbitQueue.ti_LOG_STREAM;
                //if (log is TransactionalLog)
                //    queue = RabbitQueue.ti_TRANSACTIONAL_LOG_STREAM;

                //var producer = new MqProducer(queue);
                //producer.Produce(Guid.NewGuid().ToString(), log);
            }
            catch
            {
                AddPendingLog(log);
            }
        }

        public static void AddPendingLog(LogBase log)
        {
            //try
            //{
            //    if (log.Attempt == null)
            //        log.Attempt = 1;

            //    var id = DbPendingLog.Insert(log);
            //    if (log.Attempt < 10)
            //        SlackLog.Post(
            //            $"Log inserido na fila de retentativa: pk: {id} | logId: {log.Id} | tentativa: {log.Attempt}",
            //            "RE Log",
            //            ESlackChannel.RE_LOG_ALERT_TEST,
            //            ESlackEnviroment.Development);
            //    else
            //        SlackLog.Post(
            //            $"Log desativado inserido na fila de retentativa - pk: {id} | logId: {log.Id} | tentativa: {log.Attempt}",
            //            "RE Log",
            //            ESlackChannel.RE_LOG_ALERT_TEST,
            //            ESlackEnviroment.Development);
            //}
            //catch (Exception ex)
            //{
            //    SlackLog.Post(
            //    $"Falha ao inserir log na fila de retentativa - {ex.Message} - {JsonConvert.SerializeObject(log)}",
            //    "RE Log",
            //    ESlackChannel.RE_LOG_ALERT_TEST,
            //    ESlackEnviroment.Development);
            //}
        }

        internal static void SendTransactionalLog(TransactionalLog log)
        {
            var assemblyName = Assembly.GetEntryAssembly().GetName().Name;
            log.AssemblyName = assemblyName;
            Task.Run(() =>
            {
                try
                {
                    //var producer = new MqProducer(RabbitQueue.ti_TRANSACTIONAL_LOG_STREAM);
                    //producer.Produce(Guid.NewGuid().ToString(), log);
                }
                catch
                {
                    AddPendingLog(log);
                }
            });
        }
    }
}