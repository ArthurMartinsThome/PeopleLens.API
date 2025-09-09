namespace PL.Domain.Model
{
    public class QuestionResponseOption : ICloneable
    {
        public int? Id { get; set; }
        public int? QuestionId { get; set; }
        public string? Text { get; set; }
        public int? Value { get; set; }

        public object Clone() => this.MemberwiseClone();
    }
}