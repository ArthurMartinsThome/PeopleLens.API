using System.ComponentModel.DataAnnotations.Schema;
using PL.Infra.Util;

namespace PL.Adapter.MySql.Model
{
    [Table("auth")]
    internal class Auth
    {
        public int? id { get; set; }
        [FilterIdentifier("StatusId")]
        public int? status_id { get; set; }
        public int? role_id { get; set; }
        public int? user_id { get; set; }
        [FilterIdentifier("Username")]
        public string? username { get; set; }
        [FilterIdentifier("Password")]
        public string? password { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}