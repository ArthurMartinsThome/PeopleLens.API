using PL.Domain.Model.Enum;

namespace PL.Domain.Model
{
    public class PersonCompany : ICloneable
    {
        public int? Id { get; set; }
        public int? PersonId { get; set; }
        public int? CompanyId { get; set; }
        public EStatus? StatusId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public object Clone() => this.MemberwiseClone();
    }
}