using PL.Infra.Util;
using System.ComponentModel.DataAnnotations.Schema;

namespace PL.Adapter.PostgreSQL.Model
{
    [Table("company")]
    internal class Company
    {
        [FilterIdentifier("Id")]
        public int? id { get; set; }
        [FilterIdentifier("StatusId")]
        public int? status_id { get; set; }
        public string? name { get; set; }
        public string? cnpj { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}