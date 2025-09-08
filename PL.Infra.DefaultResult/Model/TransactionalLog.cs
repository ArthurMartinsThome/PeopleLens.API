namespace PL.Infra.DefaultResult.Model
{
    internal class TransactionalLog : LogBase
    {
        public int? Module { get; set; }
        public int? Action { get; set; }
        public int? EventId { get; set; }
        public int? ObjectId { get; set; }
        public int? SellerId { get; set; }
        public int? Origin { get; set; }
        public int? ReferenceId { get; set; }
        public int? ReferenceModuleId { get; set; }
    }
}