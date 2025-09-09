namespace PL.Domain.Model
{
    public class Response : ICloneable
    {
        public int? Id { get; set; }
        public int? TestAppliedId { get; set; }
        public int? QuestionId { get; set; }
        public string? Value { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public object Clone() => this.MemberwiseClone();
    }
}