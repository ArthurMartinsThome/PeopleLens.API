using PL.Domain.Model.Enum;

namespace PL.Domain.Model
{
    public class User : ICloneable
    {
        public int? Id { get; set; }
        public int? PersonId { get; set; }
        public EStatus? StatusId { get; set; }
        public ERole? RoleId { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Person Person { get; set; }

        public object Clone() => this.MemberwiseClone();
    }
}