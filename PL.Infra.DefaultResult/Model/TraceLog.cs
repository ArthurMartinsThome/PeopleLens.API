using Microsoft.Extensions.Logging;

namespace PL.Infra.DefaultResult.Model
{
    internal class TraceLog : LogBase
    {
        public LogLevel? Level { get; set; }
        public string? StackTrace { get; set; }
    }
}