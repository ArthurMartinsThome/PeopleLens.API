namespace PL.Domain.Model
{
    public class ResponseOption : ICloneable
    {
        public int? Id { get; set; }
        public int? ResponseId { get; set; }
        public int? QuestionResponseOptionId { get; set; }

        public object Clone() => this.MemberwiseClone();
    }
}