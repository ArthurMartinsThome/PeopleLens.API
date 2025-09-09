namespace PL.Domain.Model
{
    public class CompanyTest : ICloneable
    {
        public int? Id { get; set; }
        public int? TestId { get; set; }
        public int? CompanyId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public object Clone() => this.MemberwiseClone();
    }
}