namespace PL.Domain.Model
{
    public class QuestionConfiguration : ICloneable
    {
        public int? Id { get; set; }
        public int? QuestionId { get; set; }
        public int? KeyConfigurationQuestionId { get; set; }
        public string? Value { get; set; }

        public object Clone() => this.MemberwiseClone();
    }
}