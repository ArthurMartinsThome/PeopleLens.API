using PL.Domain.Model.Enum;

namespace PL.Domain.Model.Filter
{
    public class AuthFilter
    {
        public int? Id { get; set; }
        public List<int>? Ids { get; set; }
        public List<int?> StatusId { get; set; }
        public ERole? RoleId { get; set; }
        public int? UserId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public DateTime? CreatedTo { get; set; }
        public DateTime? CreatedFrom { get; set; }

        public bool HideInactive { get; set; } = true;
        public bool HideDeleted { get; set; } = true;
    }
}