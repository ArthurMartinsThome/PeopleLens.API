using PL.Infra.Util;
using System.ComponentModel.DataAnnotations.Schema;

namespace PL.Adapter.PostgreSQL.Model
{
    [Table("question_response_option")]
    internal class QuestionResponseOption
    {
        [FilterIdentifier("Id")]
        public int? id { get; set; }
        [FilterIdentifier("QuestionId")]
        public int? question_id { get; set; }
        [FilterIdentifier("Text")]
        public string? text { get; set; }
        [FilterIdentifier("Value")]
        public int? value { get; set; }
    }
}