namespace PL.Infra.DefaultResult.Model
{
    internal class LogBase
    {
        public string? Id { get; set; }
        public DateTime? TimeStamp { get; set; }
        public int? UserId { get; set; }
        public string? AssemblyName { get; set; }
        public string? Ip { get; set; }
        public string? SessionId { get; set; }
        public string? RequestId { get; set; }
        public int? Attempt { get; set; }
        public string? Route { get; set; }
        public string? LogData { get; set; }
        public string? ReferenceId { get; set; }
    }
}