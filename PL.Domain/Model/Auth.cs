using PL.Domain.Model.Enum;

namespace PL.Domain.Model
{
    public class Auth : ICloneable
    {
        public int? Id { get; set; }
        public EStatus? StatusId { get; set; }
        public ERole? Role { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public object Clone() => this.MemberwiseClone();
    }
}