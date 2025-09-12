namespace PL.Domain.Model.Filter
{
    public class KeyConfigurationQuestionFilter
    {
        public int? Id { get; set; }
        public List<int>? Ids { get; set; }
        public int? QuestionId { get; set; }
        public int? KeyConfigId { get; set; }
        public string? Value { get; set; }
    }
}