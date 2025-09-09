using PL.Infra.Util;
using System.ComponentModel.DataAnnotations.Schema;

namespace PL.Adapter.PostgreSQL.Model
{
    [Table("test_type")]
    internal class TestType
    {
        [FilterIdentifier("Id")]
        public int? id { get; set; }
        [FilterIdentifier("Name")]
        public string? name { get; set; }
        [FilterIdentifier("Description")]
        public string? description { get; set; }
    }
}