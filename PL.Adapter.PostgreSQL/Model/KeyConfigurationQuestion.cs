using PL.Infra.Util;
using System.ComponentModel.DataAnnotations.Schema;

namespace PL.Adapter.PostgreSQL.Model
{
    [Table("key_configuration_question")]
    internal class KeyConfigurationQuestion
    {
        [FilterIdentifier("Id")]
        public int? id { get; set; }
        [FilterIdentifier("KeyName")]
        public string? key_name { get; set; }
        [FilterIdentifier("Description")]
        public string? description { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}