namespace PL.Domain.Model
{
    public class Question : ICloneable
    {
        public int? Id { get; set; }
        public int? ResponseTypeId { get; set; }
        public string? QuestionText { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public object Clone() => this.MemberwiseClone();
    }
}