namespace PL.Domain.Model
{
    public class Person : ICloneable
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public DateTime? Birthday { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public object Clone() => this.MemberwiseClone();
    }
}