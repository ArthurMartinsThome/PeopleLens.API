using PL.Infra.Util;
using System.ComponentModel.DataAnnotations.Schema;

namespace PL.Adapter.PostgreSQL.Model
{
    [Table("question_configuration")]
    internal class QuestionConfiguration
    {
        [FilterIdentifier("Id")]
        public int? id { get; set; }
        [FilterIdentifier("QuestionId")]
        public int? question_id { get; set; }
        [FilterIdentifier("KeyConfigId")]
        public int? key_config_id { get; set; }
        [FilterIdentifier("Value")]
        public string? value { get; set; }
    }
}