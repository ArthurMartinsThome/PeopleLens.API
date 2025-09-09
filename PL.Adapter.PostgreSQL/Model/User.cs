using PL.Infra.Util;
using System.ComponentModel.DataAnnotations.Schema;

namespace PL.Adapter.PostgreSQL.Model
{
    [Table("user")]
    internal class User
    {
        [FilterIdentifier("Id")]
        public int? id { get; set; }
        [FilterIdentifier("PersonId")]
        public int? person_id { get; set; }
        [FilterIdentifier("StatusId")]
        public int? status_id { get; set; }
        [FilterIdentifier("RoleId")]
        public int? role_id { get; set; }
        [FilterIdentifier("Email")]
        public string? email { get; set; }
        public string? password { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}