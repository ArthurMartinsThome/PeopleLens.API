using PL.Domain.Model.Enum;

namespace PL.Domain.Model
{
    public class User : ICloneable
    {
        public int? Id { get; set; }
        public EStatus? StatusId { get; set; }
        public string? Name { get; set; }
        public string? Cellphone { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public object Clone() => this.MemberwiseClone();
    }
}