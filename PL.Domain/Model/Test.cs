namespace PL.Domain.Model
{
    public class Test : ICloneable
    {
        public int? Id { get; set; }
        public int? TestTypeId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public object Clone() => this.MemberwiseClone();
    }
}