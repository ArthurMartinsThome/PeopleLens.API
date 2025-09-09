namespace PL.Domain.Model
{
    public class TestQuestion : ICloneable
    {
        public int? Id { get; set; }
        public int? TestId { get; set; }
        public int? QuestionId { get; set; }

        public object Clone() => this.MemberwiseClone();
    }
}