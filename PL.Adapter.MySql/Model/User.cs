using PL.Infra.Util;
using System.ComponentModel.DataAnnotations.Schema;

namespace PL.Adapter.MySql.Model
{
    [Table("user")]
    internal class User
    {
        [FilterIdentifier("Id")]
        public int? id { get; set; }
        public int? status_id { get; set; }
        [FilterIdentifier("Name")]
        public string? name { get; set; }
        [FilterIdentifier("Cellphone")]
        public string? cellphone { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}