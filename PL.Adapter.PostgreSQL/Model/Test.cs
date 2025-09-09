using PL.Infra.Util;
using System.ComponentModel.DataAnnotations.Schema;

namespace PL.Adapter.PostgreSQL.Model
{
    [Table("test")]
    internal class Test
    {
        [FilterIdentifier("Id")]
        public int? id { get; set; }
        [FilterIdentifier("TestTypeId")]
        public int? test_type_id { get; set; }
        [FilterIdentifier("Title")]
        public string? title { get; set; }
        [FilterIdentifier("Description")]
        public string? description { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}