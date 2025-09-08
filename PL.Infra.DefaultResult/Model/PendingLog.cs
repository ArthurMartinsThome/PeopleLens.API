namespace PL.Infra.DefaultResult.Model
{
    internal class PendingLog
    {
        public int id { get; set; }
        public int? attempt { get; set; }
        public DateTime? retry_at { get; set; }
        public string log_data { get; set; } = null!;
        public string type { get; set; }
    }
}