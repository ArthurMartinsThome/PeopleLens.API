namespace PL.Domain.Dto
{
    public class RegisterDto
    {
        public int? CompanyId { get; set; }
        public int? RoleId { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Name { get; set; }
    }
}