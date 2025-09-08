using System.Reflection;

namespace PL.Infra.DefaultResult.Model
{
    internal class CustomStackFrame
    {
        public int? FileLineNumber { get; set; }
        public string? FileName { get; set; }
        public MethodBase? Method { get; set; }
        public List<string>? Messages { get; set; }
    }
}