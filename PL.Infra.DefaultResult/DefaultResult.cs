using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PL.Infra.DefaultResult.Interface;
using PL.Infra.DefaultResult.Model;
using PL.Infra.DefaultResult.Model.Enum;
using RestSharp;
using System.Collections;
using System.Data;
using System.Net;

namespace PL.Infra.DefaultResult
{
    public class DefaultResult<T> : IResult<T>
    {
        public bool Succeded { get; set; }
        public bool HasData { get; set; }
        public T? Data { get; set; }
        private CustomStackFrame[]? Frames { get; set; }
        public string? LastMessage { get; set; }
        public string? PrivateMessage { get; set; }
        public LogLevel? LogLevel { get; set; }
        public bool GenerateLog { get; set; }
        internal List<TransactionalLog> TransactionalLog { get; set; }
        public HttpStatusCode? StatusCode { get; set; }

        /// <summary>
        /// Use TracedResult<T>.Create or TracedResult<T>.CreateError
        /// </summary>
        /// 
        private DefaultResult() { }

        public IResult<T> AddMessage(string message)
        {
            LastMessage = message;
            if (Succeded || Frames == null)
                return this;

            var frameIndex = Frames.Length - new System.Diagnostics.StackTrace(1, true).FrameCount;
            if (frameIndex < 0 || frameIndex >= Frames.Length)
                return this;

            var frame = Frames[frameIndex];
            if (frame.Messages == null)
                frame.Messages = new List<string>();

            frame.Messages.Add(message);

            return this;
        }
        public IResult<TNew> ChangeType<TNew>(TNew? data)
        {
            return new DefaultResult<TNew>
            {
                Succeded = Succeded,
                HasData = HasData,
                Frames = Frames,
                Data = data,
                GenerateLog = GenerateLog,
                LastMessage = LastMessage,
                LogLevel = LogLevel,
                PrivateMessage = PrivateMessage,
                StatusCode = StatusCode
            };
        }
        public void FinalizeResult(/*int? userId = null, LogLevel? overrideLogLevel = null*/)
        {
            try
            {
                if (GenerateLog)
                {
                    var st = BuildStackTrace();
                    //LogWriter.Write(overrideLogLevel ?? LogLevel ?? Microsoft.Extensions.Logging.LogLevel.Error,
                    //    $"{(string.IsNullOrEmpty(LastMessage) ? "LastMessage: empty" : LastMessage)} - {(string.IsNullOrEmpty(PrivateMessage) ? "PrivateMessage: empty" : PrivateMessage)}",
                    //    userId, st, "", route: Route.DefaultReLog);
                }
            }
            catch { }
            try
            {
                if (TransactionalLog == null || !TransactionalLog.Any())
                    return;

                foreach (var item in TransactionalLog)
                {
                    try
                    {
                        //item.UserId = userId;
                        item.Ip = "";
                        item.TimeStamp = DateTime.UtcNow;
                        LogWriter.SendTransactionalLog(item);
                    }
                    catch { }
                }
            }
            catch { }
        }
        private string? BuildStackTrace()
        {
            if (Frames == null)
                return null;

            var sb = new System.Text.StringBuilder();
            foreach (var item in Frames)
            {
                if (item.Messages != null && item.Messages.Any())
                    sb.AppendLine($"\"{string.Join(", ", item.Messages)}\" at {item.Method?.ReflectedType?.FullName}.[{item.Method}] in {item.FileName}:line {item.FileLineNumber}");
                else
                    sb.AppendLine($"at {item.Method?.ReflectedType?.FullName}.[{item.Method}] in {item.FileName}:line {item.FileLineNumber}");
            }
            return sb.ToString();
        }
        public void AddTransactionalLog<TLog>(/*EModule module, */int objectId, EAction action, TLog obj, EOrigin? overrideOrigin = null, EEvent? overrideEventId = null, int? referenceId = null, int? referenceModuleId = null) => AddTransactionalLog(/*module, */objectId, action, obj, default, overrideOrigin, overrideEventId, referenceId, referenceModuleId);
        public void AddTransactionalLog<TLog>(/*EModule module, */int objectId, EAction action, TLog? oldObject, TLog? newObject, EOrigin? overrideOrigin = null, EEvent? overrideEventId = null, int? referenceId = null, int? referenceModuleId = null)
        {
            try
            {
                if (TransactionalLog == null)
                    TransactionalLog = new List<TransactionalLog>();

                if (overrideOrigin == null)
                {
                    var originIdText = "Cross.Util.EnvironmentVariable.GetNoError(Cross.Util.Model.Constant.EnvironmentVariableName.ORIGIN_ID)";
                    if (!string.IsNullOrEmpty(originIdText))
                        overrideOrigin = Enum.Parse<EOrigin>(originIdText);
                }


                if (!overrideEventId.HasValue)
                    overrideEventId = EventTrigger.GetEventId(oldObject, newObject);

                string jsonData;
                if (newObject != null)
                {
                    var difference = "Infra.Cross.Util.ObjectCompare.CompareObjects(oldObject, newObject)";
                    jsonData = JsonConvert.SerializeObject(difference);
                }
                else
                    jsonData = JsonConvert.SerializeObject(oldObject);

                TransactionalLog.Add(new TransactionalLog
                {
                    Origin = overrideOrigin == null ? null : overrideOrigin.GetHashCode(),
                    Action = action.GetHashCode(),
                    //Module = module.GetHashCode(),
                    EventId = overrideEventId.GetHashCode(),
                    ObjectId = objectId,
                    LogData = jsonData,
                    ReferenceId = referenceId,
                    ReferenceModuleId = referenceModuleId
                });
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// Utilize este método para retornar resultados bem sucedidos mesmo quando não houver dados para retornar
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DefaultResult<T> Create(T? data = default, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            var hasData = GetHasData(data);
            var result = GetBaseResult();
            result.HasData = hasData;
            result.Succeded = true;
            result.Data = data;
            result.StatusCode = statusCode;
            return result;
        }
        /// <summary>
        /// Utilize este método para interromper o processo como é feito em uma falha de validação.
        /// OBS: Em caso de Exception utilize o método Error(Exception ex, string? message = null)
        /// </summary>
        /// <param name="message"></param>
        /// <param name="privateMessage"></param>
        /// <returns></returns>
        public static DefaultResult<T> Break(string publicMessage, string? privateMessage = null, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            var result = GetBaseResult();
            result.PrivateMessage = string.IsNullOrEmpty(privateMessage) ? publicMessage : privateMessage;
            result.Frames[^1].Messages = new List<string> { publicMessage };
            result.LastMessage = publicMessage;
            result.StatusCode = statusCode;
            return result;
        }

        /// <summary>
        /// Utilize este método para retornar erros apenas quando há uma Exception
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="publicMessage"></param>
        /// <returns></returns>
        public static DefaultResult<T> Error(string publicMessage, Exception ex)
        {
            var result = GetBaseResult();
            result.GenerateLog = true;
            result.PrivateMessage = ex.Message;
            result.Frames[0].Messages = new List<string> { publicMessage };
            result.LastMessage = publicMessage;
            return result;
        }
        private static bool GetHasData<TTemp>(TTemp? data)
        {
            if (data == null)
                return false;
            if (typeof(IEnumerable).IsAssignableFrom(typeof(TTemp)))
                return ((IEnumerable)data).Cast<object>().Count() > 0;
            return true;
        }
        private static DefaultResult<T> GetBaseResult()
        {
            var frames = new System.Diagnostics.StackTrace(2, true).GetFrames().Select(x => new CustomStackFrame
            {
                FileLineNumber = x.GetFileLineNumber(),
                FileName = x.GetFileName(),
                Method = x.GetMethod()
            }).ToArray();
            var result = new DefaultResult<T>
            {
                Frames = frames,
            };
            return result;
        }
        public static void FinalizeAll(IEnumerable<IResult> results, int? authId = null, LogLevel? overrideLogLevel = null)
        {
            foreach (var item in results)
            {
                if (item != null)
                    item.FinalizeResult(/*authId, overrideLogLevel*/);
            }
        }
        public static IResult<T> FromRestResponse(RestResponse response, params HttpStatusCode[] succededStatusCodes)
        {
            var succeded = succededStatusCodes.Contains(response.StatusCode);
            if (succeded)
            {
                var obj = JsonConvert.DeserializeObject<T>(response.Content);
                return DefaultResult<T>.Create(obj);
            }

            var result = DefaultResult<T>.Error("Status code diferente do esperado", new Exception(response.Content));
            return result;
        }
    }
}