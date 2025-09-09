using PL.Domain.Model.Enum;

namespace PL.Domain.Model
{
    public class TestApplied : ICloneable
    {
        public int? Id { get; set; }
        public EStatus? StatusId { get; set; }
        public int? CompanyTestId { get; set; }
        public int? PersonId { get; set; }
        public DateTime? DateBegin { get; set; }
        public DateTime? DateEnd { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public object Clone() => this.MemberwiseClone();
    }
}