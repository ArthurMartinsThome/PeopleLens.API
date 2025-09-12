namespace PL.Domain.Model.Filter
{
    public class QuestionConfigurationFilter
    {
        public int? Id { get; set; }
        public int? QuestionId { get; set; }
        public List<int>? QuestionIds { get; set; }
        public int? KeyConfigId { get; set; }
        public string? Value { get; set; }
    }
}